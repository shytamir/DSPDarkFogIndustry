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
    public int ModelIndex;
    public bool Productive;
}

public class ModelProtoSet : ProtoSet<ModelProto>
{
}

public class ModelProto : Proto
{
    public PrefabDesc prefabDesc;
}

public class PrefabDesc
{
    public UnityEngine.Pose[] slotPoses;
    public bool isAssembler;
    public int assemblerSpeed;
    public ERecipeType assemblerRecipeType;
}

public class TechProto : Proto
{
    public int[] UnlockRecipes;
    public int MaxLevel;
    public bool Published;
    public bool IsHiddenTech;
    public bool IsObsolete;
}

public static class Localization
{
    public static string Translate(string s) { throw new NotSupportedException("Compile-only shim"); }
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

public class AbnormalityLogic
{
}

public class DeterminatorBase
{
}

public class AbnormalityDeterminator : DeterminatorBase
{
}

public class GameHistoryData
{
    public bool TechUnlocked(int techId, bool max)
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
    public static ModelProtoSet models
    {
        get
        {
            throw new NotSupportedException("Compile-only shim");
        }
    }
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
