using DSPDarkFogIndustry;

internal static class Program
{
    private static int checks;
    internal static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
        checks++;
    }
    private static (RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet techs) Fixture()
    {
        return (new RecipeProtoSet { dataArray = [new RecipeProto { ID = 1, GridIndex = 1001 }] },
            new ItemProtoSet { dataArray = new[] { 1013, 5111, 5112, 1606, 1611, 1608, 5201, 5202, 5203, 5204, 5205, 5206 }.Select(id => new ItemProto { ID = id, Name = "native-" + id }).ToArray() },
            new TechProtoSet { dataArray = new[] { 1133, 1822, 1816, 1823, 1817, 1818 }.Select(id => new TechProto { ID = id, UnlockRecipes = [id + 10000] }).ToArray() });
    }
    private static void RejectBeforeMutation(Action<RecipeProtoSet, ItemProtoSet, TechProtoSet> arrange, string expected)
    {
        var f = Fixture();
        arrange(f.recipes, f.items, f.techs);
        var recipes = f.recipes.dataArray;
        var unlocks = f.techs.dataArray.Select(t => t.UnlockRecipes).ToArray();
        bool rejected = false;
        try { RecipeRegistration.Register(f.recipes, f.items, f.techs); }
        catch (InvalidOperationException e) { rejected = e.Message.Contains(expected); }
        Check(rejected, "Expected rejection: " + expected);
        Check(ReferenceEquals(recipes, f.recipes.dataArray) && f.recipes.LookupRebuilds == 0, "Rejected registration changed the recipe table.");
        Check(f.techs.dataArray.Select((t, i) => ReferenceEquals(t.UnlockRecipes, unlocks[i])).All(x => x), "Rejected registration changed a gate.");
    }
    public static void Main()
    {
        var f = Fixture();
        var existing = f.recipes.dataArray[0];
        Check(RecipeRegistration.Register(f.recipes, f.items, f.techs), "First registration skipped.");
        Check(f.recipes.dataArray.Length == 7 && ReferenceEquals(existing, f.recipes.dataArray[0]), "Native record lost or additions wrong.");
        Check(f.recipes.LookupRebuilds == 1, "Native lookup rebuild was not requested exactly once.");
        int[] products = [5206, 5201, 5203, 5202, 5204, 5205];
        int[][] inputs = [[1013], [5206, 5111], [5201, 1606], [5203, 5112], [5202, 1611], [5204, 1608]];
        int[] gates = [1133, 1822, 1816, 1823, 1817, 1818];
        for (int i = 0; i < 6; i++)
        {
            var r = f.recipes.Select(401 + i);
            Check(r.Results.SequenceEqual([products[i]]) && r.ResultCounts.SequenceEqual([i == 0 ? 2 : 1]), "Wrong output contract.");
            Check(r.Items.SequenceEqual(inputs[i]) && r.ItemCounts.All(n => n == 1), "Wrong input contract.");
            Check(r.GridIndex == 1809 + i && r.Handcraft == (i > 0 && i < 5), "Placement/handcraft mismatch.");
            Check(r.Name == f.items.Select(products[i]).Name && r.IconPath == "" && !r.NonProductive, "Native name/icon/productivity contract changed.");
            Check(f.techs.Select(gates[i]).UnlockRecipes.SequenceEqual([gates[i] + 10000, 401 + i]), "Gate or native award changed.");
        }
        var registered = f.recipes.dataArray;
        Check(!RecipeRegistration.Register(f.recipes, f.items, f.techs) && ReferenceEquals(registered, f.recipes.dataArray) && f.recipes.LookupRebuilds == 1, "Repeated callback duplicated registration.");
        var other = Fixture();
        Check(RecipeRegistration.Register(other.recipes, other.items, other.techs), "New table incorrectly skipped.");
        RejectBeforeMutation((r, _, _) => r.dataArray[0].ID = 406, "occupied");
        RejectBeforeMutation((r, _, _) => r.dataArray[0].GridIndex = 1814, "occupied");
        RejectBeforeMutation((_, items, _) => items.dataArray = items.dataArray.Where(i => i.ID != 1608).ToArray(), "missing native input");
        RejectBeforeMutation((_, items, _) => items.dataArray = items.dataArray.Where(i => i.ID != 5205).ToArray(), "missing native product");
        RejectBeforeMutation((_, _, techs) => techs.dataArray = techs.dataArray.Where(t => t.ID != 1818).ToArray(), "missing native gate");
        Console.WriteLine($"Product checks passed: {checks} assertions; mod-owned logic only, no game execution.");
    }
}
