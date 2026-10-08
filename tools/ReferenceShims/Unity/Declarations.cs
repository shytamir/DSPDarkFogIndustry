using System;
using System.Reflection;
[assembly: AssemblyVersion("0.0.0.0")]
// Compile-only declarations. These assemblies must never be installed.
namespace UnityEngine
{
    public class Object
    {
    }
    public class Component : Object
    {
    }
    public class Behaviour : Component
    {
    }
    public class MonoBehaviour : Behaviour
    {
    }
    public class ScriptableObject : Object
    {
    }
    public interface ISerializationCallbackReceiver
    {
        void OnBeforeSerialize();
        void OnAfterDeserialize();
    }
}
