// Small data stores for registration and unlock tests. Native preload,
// production, discovery and serialization are not simulated.
public abstract class Proto
{
    public int ID;
    public string SID;
    public string Name;
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

public class ProtoSet<T> where T : Proto
{
    public T[] dataArray;
    public T Select(int id) => dataArray.SingleOrDefault(p => p.ID == id);
}

public class RecipeProtoSet : ProtoSet<RecipeProto>
{
    public int LookupRebuilds;
    public void OnAfterDeserialize()
    {
        LookupRebuilds++;
    }
}

public class ItemProtoSet : ProtoSet<ItemProto>
{
}

public class TechProtoSet : ProtoSet<TechProto>
{
}

public struct TechState
{
    public bool unlocked;
}

public class GameHistoryData
{
    public readonly HashSet<int> Researched = [];
    public readonly HashSet<int> UnlockedRecipes = [];
    public readonly List<int> UnlockCalls = [];
    public TechState TechState(int techId) => new TechState { unlocked = Researched.Contains(techId) };
    public bool RecipeUnlocked(int recipeId) => UnlockedRecipes.Contains(recipeId);
    public void UnlockRecipe(int recipeId)
    {
        UnlockCalls.Add(recipeId);
        UnlockedRecipes.Add(recipeId);
    }
}
