using System.Collections.Generic;
using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [HarmonyPatch(typeof(AbnormalityLogic), "InitDeterminators")]
    internal static class DetectorInitializationPatch
    {
        [HarmonyPrefix]
        private static bool Prefix(ref Dictionary<int, AbnormalityDeterminator> ___determinators)
        {
            // Native tick/free callers need a valid collection. Skip detector creation
            // without changing the prototype catalogue, game mode or saved history.
            ___determinators = new Dictionary<int, AbnormalityDeterminator>();
            return false;
        }
    }
}
