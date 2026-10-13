using System.Globalization;
using BepInEx.Configuration;

namespace DSPDarkFogIndustry
{
    internal static class ConfigurationReference
    {
        internal static void Write(ConfigFile file, RecipeCatalogue catalogue)
        {
            foreach (var item in catalogue.Items.Values)
            {
                Put(file, "90 - Reference - Items", item.ID, Localization.Translate(item.Name),
                    "Lookup only; regenerated at startup. Ingredient/output ID, not necessarily a production machine.\n" +
                    (item.Productive ? "This ingredient permits extra products when all ingredients do." : "This ingredient permits acceleration only."));
            }
            foreach (var technology in catalogue.Technologies.Values)
            {
                string notes = "Maximum level " + technology.MaxLevel + ".";
                if (!technology.Published) { notes += " Not published in normal progression."; }
                if (technology.IsHiddenTech) { notes += " Hidden technology."; }
                if (technology.IsObsolete) { notes += " Obsolete technology."; }
                Put(file, "91 - Reference - Technologies", technology.ID, Localization.Translate(technology.Name),
                    "Lookup only; regenerated at startup. The recipe requires full completion of this record.\n" + notes);
            }
            foreach (var machine in catalogue.Machines.Values)
            {
                string baseline = machine.Family == ERecipeType.Smelt ? "Arc Smelter (2302)" :
                    machine.Family == ERecipeType.Assemble ? "Assembling Machine Mk.I (2303)" : "Chemical Plant (2309)";
                string speed = ((decimal)machine.Speed / catalogue.BasicSpeed(machine.Family)).ToString("0.####", CultureInfo.InvariantCulture);
                string family = machine.Family == ERecipeType.Smelt ? "Smelting" : machine.Family == ERecipeType.Assemble ? "Assembly" : "Chemical production";
                Put(file, "92 - Reference - Machines", machine.ItemId, Localization.Translate(catalogue.Items[machine.ItemId].Name),
                    "Lookup only; regenerated at startup. " + family + ".\nRate baseline: " + baseline +
                    ".\nThis machine's relative speed: " + speed + "x. All compatible tiers remain usable.");
            }
        }

        private static void Put(ConfigFile file, string section, int id, string name, string description)
        {
            // A user's label edit never participates in validation or registration.
            file.Bind(section, id.ToString(CultureInfo.InvariantCulture), name, description).Value = name;
        }
    }
}
