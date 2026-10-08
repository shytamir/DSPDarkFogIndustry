using System;
using System.Reflection;
[assembly: AssemblyVersion("5.4.17.0")]
namespace BepInEx
{
    public abstract class BaseUnityPlugin : UnityEngine.MonoBehaviour
    {
        protected BaseUnityPlugin()
        {
            throw new NotSupportedException("Compile-only shim");
        }
        protected Logging.ManualLogSource Logger
        {
            get
            {
                throw new NotSupportedException("Compile-only shim");
            }
        }
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class BepInPlugin : Attribute
    {
        public BepInPlugin(string guid, string name, string version)
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BepInProcess : Attribute
    {
        public BepInProcess(string processName)
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
}
namespace BepInEx.Logging
{
    public class ManualLogSource
    {
        private ManualLogSource()
        {
        }
        public void LogInfo(object data)
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
}
