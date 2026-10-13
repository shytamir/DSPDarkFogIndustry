using DSPDarkFogIndustry;

internal static class CatalogueChecks
{
    internal static void Run()
    {
        Program.Run(nameof(SelectsNativeFamiliesWithBasicRates), SelectsNativeFamiliesWithBasicRates);
        Program.Run(nameof(RequiresModelsBeforeMutatingRecipes), RequiresModelsBeforeMutatingRecipes);
        Program.Run(nameof(PreparesOneCataloguePerRecipeTable), PreparesOneCataloguePerRecipeTable);
    }

    internal static (RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet techs, ModelProtoSet models) CreateFixture()
    {
        var (recipes, items, techs) = RegistrationChecks.CreateFixture();
        (int id, ERecipeType family, int speed)[] machines =
        [
            (2302, ERecipeType.Smelt, 10000), (2303, ERecipeType.Assemble, 7500),
            (2309, ERecipeType.Chemical, 10000), (2304, ERecipeType.Assemble, 10000),
            (2310, (ERecipeType)5, 10000), (2901, (ERecipeType)15, 10000),
            (8000, ERecipeType.Chemical, 25000)
        ];
        items.dataArray = items.dataArray.Concat(machines.Select(m =>
            new ItemProto { ID = m.id, Name = "Machine " + m.id, ModelIndex = m.id + 100 })).ToArray();
        var models = new ModelProtoSet
        {
            dataArray = machines.Select(m => new ModelProto
            {
                ID = m.id + 100,
                prefabDesc = new PrefabDesc { isAssembler = true, assemblerRecipeType = m.family, assemblerSpeed = m.speed }
            }).ToArray()
        };
        return (recipes, items, techs, models);
    }

    private static void SelectsNativeFamiliesWithBasicRates()
    {
        var f = CreateFixture();
        var catalogue = new RecipeCatalogue(f.items, f.techs, f.models);
        Program.Check(!catalogue.Machines.ContainsKey(2310) && !catalogue.Machines.ContainsKey(2901),
            "An excluded family became a machine choice.");
        Program.Check(catalogue.Items.ContainsKey(2901), "Machine eligibility removed an ingredient/output item.");
        Program.Check(catalogue.Machines[8000].Speed == 25000, "Eligible additional machine data was discarded.");
        Program.Check(catalogue.BasicSpeed(catalogue.Machines[2304].Family) == 7500,
            "An upgraded assembler changed the basic-machine baseline.");
    }

    private static void RequiresModelsBeforeMutatingRecipes()
    {
        var f = CreateFixture();
        f.models.Select(2402).prefabDesc = null;
        var original = f.recipes.dataArray;
        bool rejected = false;
        try { RecipeStartup.Prepare(f.recipes, f.items, f.techs, f.models); }
        catch (InvalidOperationException error) { rejected = error.Message.Contains("2302"); }
        Program.Check(rejected && ReferenceEquals(original, f.recipes.dataArray),
            "Missing initialized machine data did not stop registration before mutation.");
    }

    private static void PreparesOneCataloguePerRecipeTable()
    {
        var f = CreateFixture();
        RecipeStartup.Prepare(f.recipes, f.items, f.techs, f.models);
        var catalogue = RecipeStartup.Catalogue;
        RecipeStartup.Prepare(f.recipes, f.items, f.techs, f.models);
        Program.Check(ReferenceEquals(catalogue, RecipeStartup.Catalogue) && f.recipes.LookupRebuilds == 1,
            "A later item preload repeated catalogue construction or registration.");
        var next = CreateFixture();
        RecipeStartup.Prepare(next.recipes, next.items, next.techs, next.models);
        Program.Check(!ReferenceEquals(catalogue, RecipeStartup.Catalogue), "A new native table reused stale catalogue data.");
    }
}
