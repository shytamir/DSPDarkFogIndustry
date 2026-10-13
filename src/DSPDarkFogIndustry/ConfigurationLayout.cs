using System.Collections.Generic;
using System.IO;
using System.Text;
using BepInEx.Configuration;

namespace DSPDarkFogIndustry
{
    internal static class ConfigurationLayout
    {
        internal static void Compact(ConfigFile file)
        {
            // Keep BepInEx's serialization and unknown settings. Its mandatory
            // per-entry metadata is too verbose for the text-editor layout.
            var lines = new List<string>();
            bool oldReference = false;
            foreach (string raw in File.ReadAllLines(file.ConfigFilePath))
            {
                string line = raw.Trim();
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    oldReference = line == "[90 - Reference - Items]" ||
                        line == "[91 - Reference - Technologies]" || line == "[92 - Reference - Machines]";
                    if (!oldReference && lines.Count > 0) { lines.Add(""); }
                }
                if (!oldReference && line.Length > 0 && !line.StartsWith("#")) { lines.Add(raw); }
            }
            lines.Add("");
            lines.Add("# Edit with the game closed; restart to apply to all saves in this profile.");
            lines.Add("# Id fields accept an ID or an exact name in the game's current language (case-insensitive).");
            lines.Add("# Saved selections show ID # name. Replace the whole value to enter a name; ID wins if both remain.");
            lines.Add("# Unknown, ambiguous or ineligible selections use that field's default and are logged.");
            lines.Add("# Look up IDs in " + Path.GetFileName(ConfigurationReference.PathFor(file.ConfigFilePath)) + ".");
            lines.Add("# Counts are items per batch. Input ID 0 disables that slot; active inputs must be distinct.");
            lines.Add("# Extra inputs 3-6 are optional and unused by the defaults.");
            lines.Add("# OutputPerMinute uses Arc Smelter, Assembling Machine Mk.I or Chemical Plant, without proliferation.");
            lines.Add("# Only assembler recipes allow handcrafting. Higher machine tiers retain their normal speed bonuses.");
            lines.Add("# Before editing a recipe in use: empty machines, clear their recipes, cancel handcrafts, save and exit.");
            lines.Add("# Saved unlocks remain unlocked. Numeric quantity/rate corrections are logged; check values after restart.");
            lines.Add("# Comments are regenerated; keep personal notes separately.");
            File.WriteAllLines(file.ConfigFilePath, lines, new UTF8Encoding(false));
            // Forget removed legacy reference entries so later native saves do
            // not restore them. Recipe values remain the startup snapshot.
            file.Reload();
        }
    }
}
