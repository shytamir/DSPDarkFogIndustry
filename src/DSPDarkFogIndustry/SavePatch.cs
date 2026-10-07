using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [HarmonyPatch(typeof(GameHistoryData), "Import")]
    internal static class SavePatch
    {
        [HarmonyPostfix]
        private static void Postfix(GameHistoryData __instance, bool isPreview)
        {
            SaveReconciliation.Reconcile(__instance, isPreview);
        }
    }
}
