using DSPDarkFogIndustry;

internal static class RegistrationChecks
{
    internal static void Run()
    {
        Program.Run(nameof(PreservesNativeRecipesAndUnlocks), PreservesNativeRecipesAndUnlocks);
        Program.Run(nameof(BuildsNativeRecipeFields), BuildsNativeRecipeFields);
        Program.Run(nameof(RegistersEachTableOnce), RegistersEachTableOnce);
        Program.Run(nameof(RejectsConflictsBeforeMutation), RejectsConflictsBeforeMutation);
        Program.Run(nameof(RejectsMissingRecordsBeforeMutation), RejectsMissingRecordsBeforeMutation);
    }

    internal static (RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet techs) CreateFixture()
    {
        int[] itemIds = [1013, 5111, 5112, 1606, 1611, 1608, 5201, 5202, 5203, 5204, 5205, 5206];
        int[] techIds = [1133, 1822, 1816, 1823, 1817, 1818];
        return (
            new RecipeProtoSet { dataArray = [new RecipeProto { ID = 1, GridIndex = 1001 }] },
            new ItemProtoSet
            {
                dataArray = itemIds.Select(id => new ItemProto { ID = id, Name = "native-" + id }).ToArray()
            },
            new TechProtoSet
            {
                dataArray = techIds.Select(id => new TechProto { ID = id, UnlockRecipes = [id + 10000] }).ToArray()
            });
    }

    private static void PreservesNativeRecipesAndUnlocks()
    {
        var (recipes, items, techs) = CreateFixture();
        var existing = recipes.dataArray[0];
        Program.Check(RecipeRegistration.Register(recipes, items, techs), "First registration was skipped.");
        Program.Check(ReferenceEquals(existing, recipes.dataArray[0]), "Native recipe was replaced.");

        // Saved machines refer to these IDs. Changing them breaks existing saves.
        Program.Check(recipes.dataArray.Skip(1).Select(recipe => recipe.ID).SequenceEqual([401, 402, 403, 404, 405, 406]),
            "Saved recipe identities changed.");
        Program.Check(recipes.LookupRebuilds == 1, "Recipe lookup was not rebuilt exactly once.");

        foreach (var definition in SynthesisRecipes.All)
        {
            Program.Check(techs.Select(definition.Gate).UnlockRecipes.SequenceEqual([definition.Gate + 10000, definition.Id]),
                $"Registration changed an existing unlock or missed recipe {definition.Id}.");
        }
    }

    private static void BuildsNativeRecipeFields()
    {
        var (recipes, items, techs) = CreateFixture();
        RecipeRegistration.Register(recipes, items, techs);

        foreach (var definition in SynthesisRecipes.All)
        {
            var recipe = recipes.Select(definition.Id);
            Program.Check(recipe.Items.SequenceEqual(definition.Inputs) && !ReferenceEquals(recipe.Items, definition.Inputs),
                $"Recipe {recipe.ID} must own a copy of its ingredients.");
            Program.Check(recipe.ItemCounts.Length == recipe.Items.Length && recipe.ItemCounts.All(count => count == 1),
                $"Recipe {recipe.ID} has incorrect ingredient quantities.");
            Program.Check(recipe.Results.SequenceEqual([definition.Product]) && recipe.ResultCounts.SequenceEqual([definition.OutputCount]),
                $"Recipe {recipe.ID} did not receive its output.");
            Program.Check(recipe.Handcraft == (recipe.Type == ERecipeType.Assemble),
                $"Recipe {recipe.ID} has the wrong Replicator restriction.");
            Program.Check(recipe.Name == items.Select(definition.Product).Name && recipe.IconPath == "",
                $"Recipe {recipe.ID} does not use the native product name and icon fallback.");
        }
    }

    private static void RegistersEachTableOnce()
    {
        var (recipes, items, techs) = CreateFixture();
        RecipeRegistration.Register(recipes, items, techs);
        var registered = recipes.dataArray;

        Program.Check(!RecipeRegistration.Register(recipes, items, techs)
            && ReferenceEquals(registered, recipes.dataArray) && recipes.LookupRebuilds == 1,
            "Repeated callback duplicated registration.");

        var other = CreateFixture();
        Program.Check(RecipeRegistration.Register(other.recipes, other.items, other.techs),
            "A new table was incorrectly skipped.");
    }

    private static void RejectsConflictsBeforeMutation()
    {
        RejectBeforeMutation((recipes, _, _) => recipes.dataArray[0].ID = 406, "occupied");
        RejectBeforeMutation((recipes, _, _) => recipes.dataArray[0].GridIndex = 1814, "occupied");
    }

    private static void RejectsMissingRecordsBeforeMutation()
    {
        RejectBeforeMutation((_, items, _) => items.dataArray = items.dataArray.Where(item => item.ID != 1608).ToArray(),
            "missing native input");
        RejectBeforeMutation((_, items, _) => items.dataArray = items.dataArray.Where(item => item.ID != 5205).ToArray(),
            "missing native product");
        RejectBeforeMutation((_, _, techs) => techs.dataArray = techs.dataArray.Where(tech => tech.ID != 1818).ToArray(),
            "missing native gate");
    }

    private static void RejectBeforeMutation(Action<RecipeProtoSet, ItemProtoSet, TechProtoSet> arrange, string expectedError)
    {
        var (recipes, items, techs) = CreateFixture();
        arrange(recipes, items, techs);
        var originalRecipes = recipes.dataArray;
        var originalUnlocks = techs.dataArray.Select(tech => tech.UnlockRecipes).ToArray();

        bool rejected = false;
        try
        {
            RecipeRegistration.Register(recipes, items, techs);
        }
        catch (InvalidOperationException error)
        {
            rejected = error.Message.Contains(expectedError);
        }

        Program.Check(rejected, "Expected rejection: " + expectedError);
        Program.Check(ReferenceEquals(originalRecipes, recipes.dataArray) && recipes.LookupRebuilds == 0,
            "Rejected registration changed the recipe table.");
        Program.Check(techs.dataArray.Select((tech, index) => ReferenceEquals(tech.UnlockRecipes, originalUnlocks[index])).All(same => same),
            "Rejected registration changed a technology's unlocks.");
    }
}
