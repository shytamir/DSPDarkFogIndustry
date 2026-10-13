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
        Check(read.Recipes.Select(r => r.OutputItem.Value).SequenceEqual(SynthesisRecipes.All.Select(r => r.Product)), "Defaults did not survive save.");
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
}

