using BepInEx;
using HarmonyLib;

namespace DSPDarkFogIndustry
{
    [BepInPlugin(Id, "DSP Dark Fog Industry", BuildIdentity.Version)]
    [BepInProcess("DSPGAME.exe")]
    public sealed class Plugin : BaseUnityPlugin
    {
        public const string Id = "shytamir.dsp.darkfogindustry";

        private void Awake()
        {
            new Harmony(Id).PatchAll(typeof(Plugin).Assembly);
            Logger.LogInfo("DSP Dark Fog Industry " + BuildIdentity.Version + " loaded.");
        }
    }
}
