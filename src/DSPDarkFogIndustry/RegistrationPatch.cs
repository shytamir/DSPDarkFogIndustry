using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [HarmonyPatch(typeof(ItemProto), "Preload")]
    internal static class RegistrationPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            // Models are ready; the first item has not built its recipe links yet.
            // Prepare guards repeated callbacks for the rest of the item table.
            RecipeStartup.Prepare(LDB.recipes, LDB.items, LDB.techs, LDB.models);
        }
    }
}
