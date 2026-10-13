using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BepInEx.Configuration;

namespace DSPDarkFogIndustry
{
    internal sealed class IntegerSetting
    {
        internal readonly string Section;
        internal readonly string Key;
        internal readonly string Raw;
        internal string NumericText { get; private set; }
        internal readonly int Default;
        private readonly ConfigEntry<int> entry;
        private readonly ConfigEntry<string> selection;
        internal int Value { get; private set; }

        internal IntegerSetting(ConfigFile file, string section, string key, int value, string description,
            bool acceptsName = false, string oldSection = null)
        {
            Section = section;
            Key = key;
            Default = value;
            // Read through BepInEx before binding Int32: its integer converter loses
            // overflow/fractional text. Only public, per-entry APIs are needed.
            string initial = value.ToString(CultureInfo.InvariantCulture);
            if (oldSection != null)
            {
                initial = file.Bind(oldSection, key, initial, "").Value;
                file.Remove(new ConfigDefinition(oldSection, key));
            }
            var text = file.Bind(section, key, initial, description);
            Raw = text.Value;
            NumericText = Raw;
            if (acceptsName) { selection = text; }
            else
            {
                file.Remove(new ConfigDefinition(section, key));
                entry = file.Bind(section, key, value, description);
            }
            int parsed;
            Set(int.TryParse(Raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed) ? parsed : value);
        }

        internal void Set(int value)
        {
            Value = value;
            if (entry != null) { entry.Value = value; }
            else { selection.Value = value.ToString(CultureInfo.InvariantCulture); }
        }

        internal void Resolve(IDictionary<int, string> names, bool allowZero, Action<string> report)
        {
            string text = Raw.Trim();
            int marker = text.IndexOf('#');
            int id;
            bool numeric = int.TryParse(marker < 0 ? text : text.Substring(0, marker).Trim(),
                NumberStyles.Integer, CultureInfo.InvariantCulture, out id);
            bool valid = numeric && (names.ContainsKey(id) || (allowZero && id == 0));
            if (!numeric)
            {
                var matches = names.Where(pair => string.Equals(pair.Value, text, StringComparison.OrdinalIgnoreCase)).Take(2).ToArray();
                valid = matches.Length == 1;
                if (valid) { id = matches[0].Key; }
            }
            if (!valid)
            {
                id = Default;
                report("[" + Section + "] " + Key + ": '" + Raw + "' -> " + id +
                    "; unknown, ambiguous or ineligible selection; using the documented default.");
            }
            NumericText = id.ToString(CultureInfo.InvariantCulture);
            Set(id);
        }

        internal void Label(IDictionary<int, string> names)
        {
            string name;
            selection.Value = Value.ToString(CultureInfo.InvariantCulture) + " # " +
                (names.TryGetValue(Value, out name) ? name.Replace('\r', ' ').Replace('\n', ' ') : "unused");
        }
    }

    internal sealed class RecipeSettings
    {
        internal readonly IntegerSetting Technology, Machine, OutputItem, OutputCount, Rate;
        internal readonly IntegerSetting[] InputItems = new IntegerSetting[6];
        internal readonly IntegerSetting[] InputCounts = new IntegerSetting[6];

        internal RecipeSettings(ConfigFile file, string section, SynthesisRecipe recipe)
        {
            string extraSection = "Extra inputs - " + section;
            Technology = new IntegerSetting(file, section, "TechnologyId", recipe.Gate,
                "Fully completed technology required to unlock this recipe. Accepts a name or ID.\n" +
                "Changes apply after restarting DSP, to all saves in this profile.\n" +
                "BepInEx regenerates comments when saving; handwritten comments are not preserved.", true);
            Machine = new IntegerSetting(file, section, "MachineId", recipe.Family == ERecipeType.Smelt ? 2302 :
                recipe.Family == ERecipeType.Assemble ? 2303 : 2309,
                "Smelter, assembler or chemical plant name or ID. All tiers in the family remain usable.\n" +
                "Only assembler recipes allow hand replication.\nRates use the basic machine, even for an upgraded ID.", true);
            OutputItem = new IntegerSetting(file, section, "OutputItemId", recipe.Product, "Item produced. Accepts a name or ID.", true);
            OutputCount = new IntegerSetting(file, section, "OutputCount", recipe.OutputCount, "Items produced per batch. Positive integer.");
            Rate = new IntegerSetting(file, section, "OutputPerMinute", recipe.Id == 401 ? 120 : 60,
                "Products per minute before proliferation in this family's basic machine:\n" +
                "Arc Smelter, Assembling Machine Mk.I or Chemical Plant.\n" +
                "Unattainable integer rates are corrected to the nearest attainable rate; ties use the lower rate.");
            for (int i = 0; i < InputItems.Length; i++)
            {
                bool active = i < recipe.Inputs.Length;
                string rowSection = i < 2 ? section : extraSection;
                string oldSection = i < 2 ? null : section;
                InputItems[i] = new IntegerSetting(file, rowSection, "Input" + (i + 1) + "ItemId", active ? recipe.Inputs[i] : 0,
                    "Ingredient name or ID. Zero disables this row. Active ingredients must have distinct IDs.", true, oldSection);
                InputCounts[i] = new IntegerSetting(file, rowSection, "Input" + (i + 1) + "Count", active ? 1 : 0,
                    "Items consumed per batch. Positive for an active row; zero for an unused row.", false, oldSection);
            }
        }
    }

    internal sealed class RecipeConfiguration
    {
        internal readonly RecipeSettings[] Recipes = new RecipeSettings[6];
        internal readonly ConfigFile File;
        private readonly Action<string> report;
        internal int[] Durations { get; private set; }

        internal RecipeConfiguration(ConfigFile file, Action<string> report)
        {
            File = file;
            this.report = report;
            file.SaveOnConfigSet = false;
            string[] names = { "Energy Shard", "Dark Fog Matrix", "Matter Recombinator", "Silicon-based Neuron", "Negentropy Singularity", "Core Element" };
            for (int i = 0; i < Recipes.Length; i++)
            {
                var recipe = SynthesisRecipes.Defaults[i];
                Recipes[i] = new RecipeSettings(file, (i + 1).ToString("00") + " - Recipe " + recipe.Id + " - " + names[i], recipe);
            }
        }

        internal void Save(RecipeCatalogue catalogue = null)
        {
            try
            {
                File.Save();
                ConfigurationLayout.Compact(File);
            }
            catch (IOException error)
            {
                report("Could not save effective recipe settings: " + error.Message);
            }
            catch (UnauthorizedAccessException error)
            {
                report("Could not save effective recipe settings: " + error.Message);
            }
            if (catalogue == null) { return; }
            try { ConfigurationReference.Write(File, catalogue); }
            catch (IOException error) { report("Could not write the name/ID reference: " + error.Message); }
            catch (UnauthorizedAccessException error) { report("Could not write the name/ID reference: " + error.Message); }
        }

        internal void Correct(RecipeCatalogue catalogue)
        {
            Durations = RecipeCorrection.Apply(this, catalogue, report);
        }

        internal SynthesisRecipe[] CreateRecipes(RecipeCatalogue catalogue)
        {
            var definitions = new SynthesisRecipe[Recipes.Length];
            for (int i = 0; i < Recipes.Length; i++)
            {
                var settings = Recipes[i];
                var identity = SynthesisRecipes.Defaults[i];
                var rows = Enumerable.Range(0, settings.InputItems.Length).Where(row => settings.InputItems[row].Value != 0).ToArray();
                definitions[i] = new SynthesisRecipe(identity.Id, settings.OutputItem.Value, settings.Technology.Value,
                    identity.Grid, Durations[i], settings.OutputCount.Value, catalogue.Machines[settings.Machine.Value].Family,
                    rows.Select(row => settings.InputItems[row].Value).ToArray(), rows.Select(row => settings.InputCounts[row].Value).ToArray());
            }
            return definitions;
        }
    }
}
