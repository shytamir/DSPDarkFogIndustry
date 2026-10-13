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
        public Configuration.ConfigFile Config
        {
            get { throw new NotSupportedException("Compile-only shim"); }
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
        public void LogWarning(object data)
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
}
namespace BepInEx.Configuration
{
    public class ConfigDefinition
    {
        public ConfigDefinition(string section, string key) { throw new NotSupportedException("Compile-only shim"); }
    }
    public abstract class ConfigEntryBase
    {
        internal ConfigEntryBase() { }
    }
    public sealed class ConfigEntry<T> : ConfigEntryBase
    {
        private ConfigEntry() { }
        public T Value
        {
            get { throw new NotSupportedException("Compile-only shim"); }
            set { throw new NotSupportedException("Compile-only shim"); }
        }
    }
    public class ConfigFile
    {
        private ConfigFile() { }
        public bool SaveOnConfigSet
        {
            get { throw new NotSupportedException("Compile-only shim"); }
            set { throw new NotSupportedException("Compile-only shim"); }
        }
        public ConfigEntry<T> Bind<T>(string section, string key, T defaultValue, string description)
        { throw new NotSupportedException("Compile-only shim"); }
        // The upstream dictionary implementation emits this member as virtual.
        public virtual bool Remove(ConfigDefinition key) { throw new NotSupportedException("Compile-only shim"); }
        public void Save() { throw new NotSupportedException("Compile-only shim"); }
        public void Reload() { throw new NotSupportedException("Compile-only shim"); }
        public string ConfigFilePath { get { throw new NotSupportedException("Compile-only shim"); } }
    }
}
