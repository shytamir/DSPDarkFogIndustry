using DSPDarkFogIndustry;
using System.Text.Json;

internal static class ProductionChecks
{
    internal static void Run()
    {
        Program.Run(nameof(ProducesAtExpectedFacilityRates), ProducesAtExpectedFacilityRates);
        Program.Run(nameof(RespectsNativeProliferationEligibility), RespectsNativeProliferationEligibility);
        Program.Run(nameof(UsesOnlyEarlierSynthesisProducts), UsesOnlyEarlierSynthesisProducts);
    }

    private static RecipeProtoSet RegisteredRecipes()
    {
        var (recipes, items, techs) = RegistrationChecks.CreateFixture();
        RecipeRegistration.Register(recipes, items, techs);
        return recipes;
    }

    private static JsonDocument ReadNativeRules()
    {
        return JsonDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "native-rules.json")));
    }

    private static void ProducesAtExpectedFacilityRates()
    {
        var recipes = RegisteredRecipes();
        using var document = ReadNativeRules();
        var facts = document.RootElement;
        int ticksPerSecond = facts.GetProperty("ticks_per_second").GetInt32();

        foreach (var definition in SynthesisRecipes.All)
        {
            var recipe = recipes.Select(definition.Id);
            bool assembler = recipe.ID > 401 && recipe.ID < 406;
            var expectedFamily = recipe.ID == 401 ? ERecipeType.Smelt
                : assembler ? ERecipeType.Assemble : ERecipeType.Chemical;
            Program.Check(recipe.Type == expectedFamily && recipe.TimeSpend > 0,
                $"Recipe {recipe.ID} cannot run in its intended facility.");

            string family = recipe.ID == 401 ? "smelter" : assembler ? "assembler" : "chemical";
            double[] speeds = facts.GetProperty("facility_speeds").GetProperty(family)
                .EnumerateArray().Select(value => value.GetDouble()).ToArray();
            double[] expectedRates = recipe.ID == 401 ? [120, 240, 360]
                : assembler ? [60, 80, 120, 240] : [60, 120];

            for (int tier = 0; tier < speeds.Length; tier++)
            {
                double rate = ticksPerSecond * 60 * speeds[tier] * recipe.ResultCounts[0] / recipe.TimeSpend;
                Program.Check(Math.Abs(rate - expectedRates[tier]) < 0.0001,
                    $"Recipe {recipe.ID}, {family} tier {tier + 1}: expected {expectedRates[tier]}/min, got {rate}/min.");
            }
        }
    }

    private static void RespectsNativeProliferationEligibility()
    {
        var recipes = RegisteredRecipes();
        using var document = ReadNativeRules();
        var flags = document.RootElement.GetProperty("productive_inputs");

        // Apply the inspected eligibility rule to our recipes; this is not a simulation
        // of spraying or the native producer's extra-product timing.
        foreach (var definition in SynthesisRecipes.All)
        {
            var recipe = recipes.Select(definition.Id);
            bool productive = !recipe.NonProductive
                && recipe.Items.All(input => flags.GetProperty(input.ToString()).GetBoolean());
            Program.Check(productive == (recipe.ID != 402 && recipe.ID != 404),
                $"Recipe {recipe.ID} has incorrect extra-product eligibility.");
        }
    }

    private static void UsesOnlyEarlierSynthesisProducts()
    {
        var recipes = RegisteredRecipes();
        var availableProducts = new HashSet<int>();
        foreach (var definition in SynthesisRecipes.All)
        {
            var recipe = recipes.Select(definition.Id);
            foreach (int input in recipe.Items.Where(item => item >= 5201 && item <= 5206))
            {
                Program.Check(availableProducts.Contains(input),
                    $"Recipe {recipe.ID} needs Dark Fog material {input} before it can be produced.");
            }

            availableProducts.Add(recipe.Results[0]);
        }
    }
}
