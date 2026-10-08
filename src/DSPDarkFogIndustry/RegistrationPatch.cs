using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoad")]
    internal static class RegistrationPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            // Run before native preload builds recipe links, icons and productivity.
            RecipeRegistration.Register(LDB.recipes, LDB.items, LDB.techs);
        }
    }
}
