using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoad")]
    internal static class RegistrationPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            RecipeRegistration.Register(LDB.recipes, LDB.items, LDB.techs);
        }
    }
}
