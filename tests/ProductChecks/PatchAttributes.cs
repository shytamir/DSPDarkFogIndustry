// Allow direct calls to the linked preload callback without loading Harmony.
// These attributes do not apply patches; real hook metadata is checked separately.
namespace HarmonyLib
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class HarmonyPatch : Attribute
    {
        public HarmonyPatch(Type declaringType, string methodName)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class HarmonyPrefix : Attribute
    {
    }
}
