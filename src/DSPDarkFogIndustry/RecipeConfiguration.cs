using System;
using System.Globalization;
using System.IO;
using BepInEx.Configuration;

namespace DSPDarkFogIndustry
{
    internal sealed class IntegerSetting
    {
        internal readonly string Section;
        internal readonly string Key;
        internal readonly string Raw;
        internal readonly int Default;
        private readonly ConfigEntry<int> entry;
        internal int Value { get; private set; }

        internal IntegerSetting(ConfigFile file, string section, string key, int value, string description)
        {
            Section = section;
            Key = key;
            Default = value;
            // Read through BepInEx before binding Int32: its integer converter loses
            // overflow/fractional text. Only public, per-entry APIs are needed.
            Raw = file.Bind(section, key, value.ToString(CultureInfo.InvariantCulture), "").Value;
            file.Remove(new ConfigDefinition(section, key));
            entry = file.Bind(section, key, value, description);
            int parsed;
            Set(int.TryParse(Raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed) ? parsed : value);
        }

        internal void Set(int value)
        {
            Value = value;
            entry.Value = value;
        }
    }

    internal sealed class RecipeSettings
    {
        internal readonly IntegerSetting Technology, Machine, OutputItem, OutputCount, Rate;
        internal readonly IntegerSetting[] InputItems = new IntegerSetting[6];
        internal readonly IntegerSetting[] InputCounts = new IntegerSetting[6];

        internal RecipeSettings(ConfigFile file, string section, SynthesisRecipe recipe)
        {
            Technology = new IntegerSetting(file, section, "TechnologyId", recipe.Gate,
                "Fully completed technology required to unlock this recipe. See the ID references below. " +
                "Changes apply after restarting DSP, to all saves in this profile. " +
                "BepInEx regenerates comments when saving; handwritten comments are not preserved.");
            Machine = new IntegerSetting(file, section, "MachineId", recipe.Family == ERecipeType.Smelt ? 2302 :
                recipe.Family == ERecipeType.Assemble ? 2303 : 2309,
                "Smelter, assembler or chemical plant item ID. Selects a family; all its tiers remain usable. " +
                "Only assembler recipes allow hand replication. Rates use the basic machine, even for an upgraded ID.");
            OutputItem = new IntegerSetting(file, section, "OutputItemId", recipe.Product, "Item produced. See the item reference below.");
            OutputCount = new IntegerSetting(file, section, "OutputCount", recipe.OutputCount, "Items produced per batch. Positive integer.");
            Rate = new IntegerSetting(file, section, "OutputPerMinute", recipe.Id == 401 ? 120 : 60,
                "Products per minute before proliferation in Arc Smelter, Assembling Machine Mk.I or Chemical Plant. " +
                "Unattainable integer rates are corrected to the nearest attainable rate; ties use the lower rate.");
            for (int i = 0; i < InputItems.Length; i++)
            {
                bool active = i < recipe.Inputs.Length;
                InputItems[i] = new IntegerSetting(file, section, "Input" + (i + 1) + "ItemId", active ? recipe.Inputs[i] : 0,
                    "Ingredient item ID. Zero disables this row. Active ingredients must have distinct IDs.");
                InputCounts[i] = new IntegerSetting(file, section, "Input" + (i + 1) + "Count", active ? 1 : 0,
                    "Items consumed per batch. Positive for an active row; zero for an unused row.");
            }
        }
    }

    internal sealed class RecipeConfiguration
    {
        internal readonly RecipeSettings[] Recipes = new RecipeSettings[6];
        internal readonly ConfigFile File;
        private readonly Action<string> report;

        internal RecipeConfiguration(ConfigFile file, Action<string> report)
        {
            File = file;
            this.report = report;
            file.SaveOnConfigSet = false;
            string[] names = { "Energy Shard", "Dark Fog Matrix", "Matter Recombinator", "Silicon-based Neuron", "Negentropy Singularity", "Core Element" };
            for (int i = 0; i < Recipes.Length; i++)
            {
                var recipe = SynthesisRecipes.All[i];
                Recipes[i] = new RecipeSettings(file, (i + 1).ToString("00") + " - Recipe " + recipe.Id + " - " + names[i], recipe);
            }
        }

        internal void Save()
        {
            try
            {
                File.Save();
            }
            catch (IOException error)
            {
                report("Could not save effective recipe settings: " + error.Message);
            }
            catch (UnauthorizedAccessException error)
            {
                report("Could not save effective recipe settings: " + error.Message);
            }
        }
    }
}
