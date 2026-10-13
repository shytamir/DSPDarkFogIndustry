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
        Program.Run("BepInEx defaults, integer metadata and section ordering", Defaults);
        Program.Run("BepInEx edits, unknown keys, comments and startup snapshot", RoundTrip);
        Program.Run("Configuration save errors are reported without resetting values", SaveError);
        Program.Run("Nearest corrections preserve unrelated settings and settle after one save", Corrections);
        Program.Run("Quantity bounds preserve native buffers and exact rate choices", Bounds);
        Program.Run("Decimal distance remains exact beyond ordinary numeric parsers", DecimalDistance);
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
        config.Save();
        var read = new RecipeConfiguration(new ConfigFile(config.File.ConfigFilePath, false), _ => { });
        Check(read.Recipes.Select(r => r.OutputItem.Value).SequenceEqual(SynthesisRecipes.Defaults.Select(r => r.Product)), "Defaults did not survive save.");
        string text = File.ReadAllText(config.File.ConfigFilePath);
        Check(text.Split("# Setting type: Int32").Length - 1 == 102, "Not all editable fields have integer metadata.");
        Check(text.IndexOf("[01 -") < text.IndexOf("[06 -"), "Recipe sections are not ordered.");
    }

    private static void RoundTrip()
    {
        const string section = "[01 - Recipe 401 - Energy Shard]";
        var config = Load("roundtrip", section + "\n# personal note\nOutputItemId = 1606\nOutputCount = 7\nUnknown = retained\n");
        Check(config.Recipes[0].OutputItem.Value == 1606 && config.Recipes[0].Rate.Value == 120, "An ordinary edit or missing field was lost.");
        config.Save();
        string text = File.ReadAllText(config.File.ConfigFilePath);
        Check(text.Contains(section) && text.Contains("Unknown = retained") && !text.Contains("personal note"), "Stable identity or BepInEx comment/key handling changed.");
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
        Check(recipe.Machine.Value == 2309 && recipe.OutputItem.Value == 1606 && recipe.Technology.Value == 1133,
            "Nearest eligible IDs or lower tie were incorrect.");
        Check(recipe.InputCounts[0].Value == 7 && recipe.InputItems[1].Value == 1606 && recipe.InputCounts[1].Value == 1,
            "Fractional count, duplicate row or nonnumeric fallback was incorrect.");
        Check(recipe.InputItems[2].Value == 0 && recipe.InputCounts[2].Value == 0, "An unused row became active.");
        Check(recipe.Rate.Value == 60 && recipe.OutputCount.Value == 1, "Rate correction changed the batch.");
        Check(config.Recipes[0].OutputCount.Value == 2 && config.Durations[0] == 60, "Unrelated defaults changed.");
        Check(messages.All(m => m.Contains("[0") && m.Contains(" -> ") && m.Contains(";")), "A correction lacks field/value/reason context.");
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
}

