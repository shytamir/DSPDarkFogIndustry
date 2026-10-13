using System.Reflection;
using BepInEx.Configuration;
using DSPDarkFogIndustry;
using static Program;

internal static class ConfigurationChecks
{
    internal static readonly string Root = Path.GetFullPath("artifacts/configuration-checks");

    internal static void Run()
    {
        Directory.CreateDirectory(Root);
        // Only the test host substitutes the normally initialized BepInEx path.
        // No plugin host, Unity assembly or installed game code is loaded.
        typeof(BepInEx.Paths).GetProperty("BepInExConfigPath", BindingFlags.Public | BindingFlags.Static)!
            .SetValue(null, Path.Combine(Root, "BepInEx.cfg"));
        Program.Run("Compact defaults retain every setting and fit the text-editor layout", Defaults);
        Program.Run("BepInEx edits, unknown keys, comments and startup snapshot", RoundTrip);
        Program.Run("Configuration save errors are reported without resetting values", SaveError);
        Program.Run("Nearest corrections preserve unrelated settings and settle after one save", Corrections);
        Program.Run("Quantity bounds preserve native buffers and exact rate choices", Bounds);
        Program.Run("Decimal distance remains exact beyond ordinary numeric parsers", DecimalDistance);
        Program.Run("Separate reference follows the catalogue and replaces legacy lookup sections", Reference);
        Program.Run("Names resolve exactly, while invalid or ambiguous selections use defaults", Names);
        Program.Run("Extra ingredient migration preserves edits and prefers the new section", ExtraInputs);
    }

    internal static RecipeConfiguration Load(string name, string content, Action<string> report = null)
    {
        string path = Path.Combine(Root, name + ".cfg");
        File.WriteAllText(path, content);
        return new RecipeConfiguration(new ConfigFile(path, false), report ?? (_ => { }));
    }

    private static void Defaults()
    {
        var config = Load("defaults", "");
        config.Correct(Catalogue());
        config.Save();
        var read = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), _ => { });
        read.Correct(Catalogue());
        Check(read.Recipes.Select(r => r.OutputItem.Value).SequenceEqual(SynthesisRecipes.Defaults.Select(r => r.Product)), "Defaults did not survive save.");
        string text = File.ReadAllText(config.File.ConfigFilePath);
        string[] lines = File.ReadAllLines(config.File.ConfigFilePath);
        Check(lines.Length <= 200 && Array.FindIndex(lines, line => line.StartsWith("[Extra inputs")) <= 70,
            "The default file exceeds the text-editor size or core-setting budget.");
        Check(lines.Count(line => !line.StartsWith('#') && line.Contains(" = ")) == 102,
            "The compact layout lost an editable setting.");
        Check(read.CreateRecipes(Catalogue()).SelectMany(r => r.Inputs).SequenceEqual(SynthesisRecipes.Defaults.SelectMany(r => r.Inputs)),
            "Compact serialization changed default ingredients.");
    }

    private static void RoundTrip()
    {
        const string section = "[01 - Recipe 401 - Energy Shard]";
        var config = Load("roundtrip", section + "\n# personal note\nOutputItemId = 1606\nOutputCount = 7\nUnknown = retained\n");
        Check(config.Recipes[0].OutputItem.Value == 1606 && config.Recipes[0].Rate.Value == 120, "An ordinary edit or missing field was lost.");
        config.Save();
        string text = File.ReadAllText(config.File.ConfigFilePath);
        Check(text.Contains(section) && text.Contains("Unknown = retained") &&
            !File.ReadAllLines(config.File.ConfigFilePath).Contains("# personal note"), "Stable identity or BepInEx comment/key handling changed.");
        File.WriteAllText(config.File.ConfigFilePath, text.Replace("OutputCount = 7", "OutputCount = 9"));
        config.File.Reload();
        Check(config.Recipes[0].OutputCount.Value == 7, "Reload changed the startup snapshot.");
        var restarted = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), _ => { });
        Check(restarted.Recipes[0].OutputCount.Value == 9, "Restart did not read the edit.");
    }

    private static void SaveError()
    {
        var messages = new List<string>();
        var config = Load("locked", "", messages.Add);
        using (File.Open(config.File.ConfigFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
            config.Recipes[0].OutputCount.Set(5);
            config.Save();
        }
        Check(messages.Count == 1 && config.Recipes[0].OutputCount.Value == 5, "Save error was hidden or values reset.");
    }

    internal static RecipeCatalogue Catalogue()
    {
        var f = CatalogueChecks.CreateFixture();
        foreach (var item in f.items.dataArray) { item.Productive = true; }
        foreach (var model in f.models.dataArray) { model.prefabDesc.slotPoses = new object[12]; }
        return new RecipeCatalogue(f.items, f.techs, f.models);
    }

    private static void Corrections()
    {
        var messages = new List<string>();
        var catalogue = Catalogue();
        var config = Load("corrections", "[02 - Recipe 402 - Dark Fog Matrix]\n" +
            "MachineId = 2901\nTechnologyId = -9999999999999999999999999999\n" +
            "OutputItemId = 1607\nOutputCount = 1\nOutputPerMinute = 61\n" +
            "Input1ItemId = 1013\nInput1Count = 7.5\nInput2ItemId = 1013\nInput2Count = nonsense\n" +
            "Input3ItemId = 0\nInput3Count = 21474836499999999999\n", messages.Add);
        config.Correct(catalogue);
        var recipe = config.Recipes[1];
        Check(recipe.Machine.Value == 2303 && recipe.OutputItem.Value == recipe.OutputItem.Default && recipe.Technology.Value == recipe.Technology.Default,
            "Invalid selections did not use their defaults.");
        Check(recipe.InputCounts[0].Value == 7 && recipe.InputItems[1].Value == recipe.InputItems[1].Default && recipe.InputCounts[1].Value == 1,
            "Fractional count, duplicate row or nonnumeric fallback was incorrect.");
        Check(recipe.InputItems[2].Value == 0 && recipe.InputCounts[2].Value == 0, "An unused row became active.");
        Check(recipe.Rate.Value == 60 && recipe.OutputCount.Value == 1, "Rate correction changed the batch.");
        Check(config.Recipes[0].OutputCount.Value == 2 && config.Durations[0] == 60, "Unrelated defaults changed.");
        Check(messages.All(m => m.StartsWith("[") && m.Contains("] ") && m.Contains(" -> ") && m.Contains(";")), "A correction lacks field/value/reason context.");
        config.Save();
        string first = File.ReadAllText(config.File.ConfigFilePath);
        messages.Clear();
        var again = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), messages.Add);
        again.Correct(catalogue);
        again.Save();
        Check(messages.Count == 0 && first == File.ReadAllText(config.File.ConfigFilePath), "Corrections did not settle after one pass.");
    }

    private static void Bounds()
    {
        var catalogue = Catalogue();
        var config = Load("bounds", "[01 - Recipe 401 - Energy Shard]\nOutputCount = 999999999999999999999\n" +
            "OutputPerMinute = 1\nInput1Count = 99999999999999999999\n" +
            "[02 - Recipe 402 - Dark Fog Matrix]\nOutputCount = 195225786\nOutputPerMinute = 195225786\n");
        config.Correct(catalogue);
        var smelting = config.Recipes[0];
        Check(smelting.OutputCount.Value == 100 && smelting.Rate.Value == 18 && config.Durations[0] == 20000,
            "The smelter boundary or slowest nearest integer rate was incorrect.");
        int count = smelting.InputCounts[0].Value;
        Check(checked((long)count * 50 + 12L * short.MaxValue) * 4 <= int.MaxValue &&
            ((long)(count + 1) * 50 + 12L * short.MaxValue) * 4 > int.MaxValue, "Input limit is not the nearest safe quantity.");
        Check(config.Recipes[1].OutputCount.Value == 195225786, "A valid large output quantity was rejected.");
        for (int i = 0; i < config.Recipes.Length; i++)
        {
            var recipe = config.Recipes[i];
            var family = catalogue.Machines[recipe.Machine.Value].Family;
            decimal ticks = config.Durations[i];
            decimal speed = Math.Min(ticks * 10000, catalogue.BasicSpeed(family));
            Check(speed * 3600 * recipe.OutputCount.Value == recipe.Rate.Value * ticks * 10000,
                "Effective rate and native duration disagree.");
            Check(ticks * 100000 <= int.MaxValue, "Duration overflows execution data.");
        }
    }

    private static void DecimalDistance()
    {
        foreach (var sample in new[] { ("1.50000000000000000000001", 2), ("1.5", 1), ("-1e99999", 1),
            ("1e99999", 9), ("1e-99999", 1), ("2.000", 2), ("NaN", 4) })
        {
            var config = Load("decimal", "[probe]\nNumber = " + sample.Item1);
            var value = new IntegerSetting(config.File, "probe", "Number", 4, "test number");
            Check(new NumericSetting(value).Nearest(1, 9) == sample.Item2, "Numeric distance failed for " + sample.Item1);
        }
    }

    private static void Reference()
    {
        var catalogue = Catalogue();
        catalogue.Technologies[1133].MaxLevel = 20;
        catalogue.Technologies[1133].IsHiddenTech = true;
        var config = Load("reference", "[90 - Reference - Items]\n5206 = edited label\n");
        config.Correct(catalogue);
        config.Save(catalogue);
        string text = File.ReadAllText(config.File.ConfigFilePath);
        string path = ConfigurationReference.PathFor(config.File.ConfigFilePath);
        string reference = File.ReadAllText(path);
        Check(!text.Contains("[90 -") && !text.Contains("edited label"), "Legacy lookup entries remained in the config.");
        Check(reference.Contains("5206 | Translated native-5206") && reference.Contains("Maximum level 20") &&
            reference.Contains("Hidden technology"), "Reference omitted translated names or technology metadata.");
        Check(File.ReadAllLines(path).Count(line => line.Contains(" | ")) ==
            catalogue.Items.Count + catalogue.Technologies.Count + catalogue.Machines.Count, "Reference omitted catalogue records.");
        Check(config.Recipes[0].OutputItem.Value == 5206, "Editing reference data changed a recipe.");
        File.WriteAllText(path, "edited lookup");
        config.Save(catalogue);
        Check(text == File.ReadAllText(config.File.ConfigFilePath), "A normal BepInEx save changed the generated reference.");
        Check(reference == File.ReadAllText(path), "Lookup edits were not replaced independently of recipe settings.");
        var messages = new List<string>();
        var locked = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), messages.Add);
        using (File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { locked.Save(catalogue); }
        Check(messages.Count == 1 && messages[0].Contains("reference"), "Reference write failure was not reported separately.");
    }

    private static void Names()
    {
        var catalogue = Catalogue();
        var messages = new List<string>();
        var config = Load("names", "[02 - Recipe 402 - Dark Fog Matrix]\n" +
            "MachineId =  tRaNsLaTeD mAcHiNe 2318  \nOutputItemId = Translated native-1606\n" +
            "TechnologyId = Translated Technology 1133\nInput2ItemId = Translated native-1013\n", messages.Add);
        config.Correct(catalogue);
        var recipe = config.Recipes[1];
        Check(recipe.Machine.Value == 2318 && recipe.OutputItem.Value == 1606 && recipe.Technology.Value == 1133 &&
            recipe.InputItems[1].Value == 1013 && messages.Count == 0, "Exact names or case/edge-whitespace handling failed.");
        config.Save();
        string text = File.ReadAllText(config.File.ConfigFilePath);
        Check(text.Contains("MachineId = 2318 # Translated Machine 2318"), "Saved selection lacks its ID and readable name.");
        catalogue.Items[2318].Name = "new language";
        var restart = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), messages.Add);
        restart.Correct(catalogue);
        Check(restart.Recipes[1].Machine.Value == 2318 && messages.Count == 0, "A label change retargeted a saved ID.");

        catalogue.Technologies[recipe.Technology.Default].Name = catalogue.Technologies[1133].Name;
        var invalid = Load("bad-names", "[02 - Recipe 402 - Dark Fog Matrix]\n" +
            "TechnologyId = Translated Technology 1133\nMachineId = Translated Machine 2901\n" +
            "OutputItemId = Translated native-160\nInput3ItemId = no such item\n", messages.Add);
        invalid.Correct(catalogue);
        Check(invalid.Recipes[1].Technology.Value == recipe.Technology.Default && invalid.Recipes[1].Machine.Value == 2303 &&
            invalid.Recipes[1].OutputItem.Value == recipe.OutputItem.Default && invalid.Recipes[1].InputItems[2].Value == 0,
            "Unknown, ambiguous or ineligible names were guessed instead of defaulted.");
        Check(messages.Count == 4 && messages.All(message => message.Contains("default")), "Invalid name diagnostics omitted fields or defaults.");
        invalid.Save();
        messages.Clear();
        var corrected = new RecipeConfiguration(new ConfigFile(invalid.File.ConfigFilePath, false), messages.Add);
        corrected.Correct(catalogue);
        Check(messages.Count == 0, "Corrected name selections did not settle after one restart.");
    }

    private static void ExtraInputs()
    {
        const string section = "[02 - Recipe 402 - Dark Fog Matrix]";
        var config = Load("extra-inputs", section + "\nInput3ItemId = 1013\nInput3Count = 7\nCustomNote = retained\n" +
            "[Extra inputs - 02 - Recipe 402 - Dark Fog Matrix]\nInput4ItemId = 1606\nInput4Count = 3\n");
        config.Correct(Catalogue());
        config.Save();
        var read = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), _ => { });
        read.Correct(Catalogue());
        Check(read.Recipes[1].InputItems[2].Value == 1013 && read.Recipes[1].InputCounts[2].Value == 7 &&
            read.Recipes[1].InputItems[3].Value == 1606 && read.Recipes[1].InputCounts[3].Value == 3,
            "Moving extra inputs lost an active ingredient or count.");
        Check(File.ReadAllText(config.File.ConfigFilePath).Contains("CustomNote = retained"), "Layout migration removed an unrelated setting.");
        var both = Load("extra-conflict", section + "\nInput3ItemId = 1013\nInput3Count = 7\n" +
            "[Extra inputs - 02 - Recipe 402 - Dark Fog Matrix]\nInput3ItemId = 1606\nInput3Count = 4\n");
        both.Correct(Catalogue());
        Check(both.Recipes[1].InputItems[2].Value == 1606 && both.Recipes[1].InputCounts[2].Value == 4,
            "A legacy value overrode the current extra-input section.");
    }
}

