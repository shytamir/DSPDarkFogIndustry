using System;
using System.Reflection;
[assembly: AssemblyVersion("2.5.5.0")]
namespace HarmonyLib
{
    public class Harmony
    {
        public Harmony(string id) { throw new NotSupportedException("Compile-only shim"); }
        public void PatchAll(Assembly assembly) { throw new NotSupportedException("Compile-only shim"); }
    }
}
