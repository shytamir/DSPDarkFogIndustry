using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoad")]
    internal static class RegistrationPatch
    {
        [HarmonyPrefix]
        internal static void Prefix()
        {
            // Disconnect signature checks before changing recipes or tech unlocks.
            PrototypeDetection.Disable(LDB.abnormalities);
            // Run before native preload builds recipe links, icons and productivity.
            RecipeRegistration.Register(LDB.recipes, LDB.items, LDB.techs);
        }
    }
}
