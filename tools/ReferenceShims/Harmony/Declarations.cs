using System;
using System.Reflection;
[assembly: AssemblyVersion("2.5.5.0")]
namespace HarmonyLib
{
    public class HarmonyAttribute : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class HarmonyPatch : HarmonyAttribute
    {
        public HarmonyPatch(Type declaringType, string methodName)
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class HarmonyPrefix : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class HarmonyPostfix : Attribute
    {
    }
    public class Harmony
    {
        public Harmony(string id)
        {
            throw new NotSupportedException("Compile-only shim");
        }
        public void PatchAll(Assembly assembly)
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
}
