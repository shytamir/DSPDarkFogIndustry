using System;
using System.Reflection;
[assembly: AssemblyVersion("0.0.0.0")]
// Compile-only declarations; native behavior is not implemented here.
public abstract class Proto
{
    public string Name;
    public int ID;
    public string SID;
}

public abstract class ProtoTable : UnityEngine.ScriptableObject
{
}

public class ProtoSet<T> : ProtoTable, UnityEngine.ISerializationCallbackReceiver where T : Proto
{
    public T[] dataArray;
    public T Select(int id)
    {
        throw new NotSupportedException("Compile-only shim");
    }
    public virtual void OnBeforeSerialize()
    {
        throw new NotSupportedException("Compile-only shim");
    }
    public virtual void OnAfterDeserialize()
    {
        throw new NotSupportedException("Compile-only shim");
    }
}

public class RecipeProtoSet : ProtoSet<RecipeProto>
{
}

public class ItemProtoSet : ProtoSet<ItemProto>
{
}

public class TechProtoSet : ProtoSet<TechProto>
{
}

public class ItemProto : Proto
{
}

public class TechProto : Proto
{
    public int[] UnlockRecipes;
}

public enum ERecipeType : byte
{
    None = 0, Smelt = 1, Chemical = 2, Assemble = 4
}

public class RecipeProto : Proto
{
    public ERecipeType Type;
    public bool Handcraft;
    public bool Explicit;
    public bool NonProductive;
    public int TimeSpend;
    public int GridIndex;
    public int[] Items;
    public int[] ItemCounts;
    public int[] Results;
    public int[] ResultCounts;
    public string IconPath;
    public string IconTag;
    public string Description;
}

public class VFPreload : UnityEngine.MonoBehaviour
{
}

public struct TechState
{
    public bool unlocked;
}

public class GameHistoryData
{
    public TechState TechState(int techId)
    {
        throw new NotSupportedException("Compile-only shim");
    }
    public bool RecipeUnlocked(int recipeId)
    {
        throw new NotSupportedException("Compile-only shim");
    }
    public void UnlockRecipe(int recipeId)
    {
        throw new NotSupportedException("Compile-only shim");
    }
}

public static class LDB
{
    public static RecipeProtoSet recipes
    {
        get
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
    public static ItemProtoSet items
    {
        get
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
    public static TechProtoSet techs
    {
        get
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
}
