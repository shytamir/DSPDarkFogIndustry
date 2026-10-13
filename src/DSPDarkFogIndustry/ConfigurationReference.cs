using System.Globalization;
using System.IO;
using System.Text;
using BepInEx.Configuration;

namespace DSPDarkFogIndustry
{
    internal static class ConfigurationReference
    {
        internal static string PathFor(string configPath) { return Path.ChangeExtension(configPath, "reference.txt"); }

        internal static void Write(ConfigFile file, RecipeCatalogue catalogue)
        {
            using (var writer = new StreamWriter(PathFor(file.ConfigFilePath), false, new UTF8Encoding(false)))
            {
                writer.WriteLine("DSP Dark Fog Industry - names and IDs (lookup only; regenerated at startup)");
                writer.WriteLine("Use an ID or an exact name from this list. Names ignore case; repeated names require an ID.");
                writer.WriteLine("This file does not change recipes. Names follow the game's current language.");
                writer.WriteLine();
                writer.WriteLine("[Items]");
                foreach (var item in catalogue.Items.Values)
                {
                    Put(writer, item.ID, Localization.Translate(item.Name),
                        item.Productive ? "extra products eligible" : "acceleration only");
                }
                writer.WriteLine();
                writer.WriteLine("[Technologies - unlock after fully completing the selected record]");
                foreach (var technology in catalogue.Technologies.Values)
                {
                    string notes = "Maximum level " + technology.MaxLevel + ".";
                    if (!technology.Published) { notes += " Not published in normal progression."; }
                    if (technology.IsHiddenTech) { notes += " Hidden technology."; }
                    if (technology.IsObsolete) { notes += " Obsolete technology."; }
                    Put(writer, technology.ID, Localization.Translate(technology.Name), notes);
                }
                writer.WriteLine();
                writer.WriteLine("[Machines - only these IDs are eligible as production facilities]");
                foreach (var machine in catalogue.Machines.Values)
                {
                    string baseline = machine.Family == ERecipeType.Smelt ? "Arc Smelter (2302)" :
                        machine.Family == ERecipeType.Assemble ? "Assembling Machine Mk.I (2303)" : "Chemical Plant (2309)";
                    string speed = ((decimal)machine.Speed / catalogue.BasicSpeed(machine.Family)).ToString("0.####", CultureInfo.InvariantCulture);
                    string family = machine.Family == ERecipeType.Smelt ? "Smelting" : machine.Family == ERecipeType.Assemble ? "Assembly" : "Chemical production";
                    Put(writer, machine.ItemId, Localization.Translate(catalogue.Items[machine.ItemId].Name),
                        family + "; baseline: " + baseline + "; relative speed: " + speed + "x");
                }
            }
        }

        private static void Put(StreamWriter writer, int id, string name, string description)
        {
            writer.WriteLine(id.ToString(CultureInfo.InvariantCulture) + " | " +
                name.Replace('\r', ' ').Replace('\n', ' ') + " | " + description);
        }
    }
}
