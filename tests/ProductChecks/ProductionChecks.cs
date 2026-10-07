using System.Text.Json;

internal static class ProductionChecks
{
    internal static void Run(RecipeProtoSet recipes)
    {
        using var document = JsonDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "native-rules.json")));
        var facts = document.RootElement;
        int ticks = facts.GetProperty("ticks_per_second").GetInt32();
        var flags = facts.GetProperty("productive_inputs");
        var seen = new HashSet<int>();
        for (int id = 401; id <= 406; id++)
        {
            var recipe = recipes.Select(id);
            bool assembler = id > 401 && id < 406;
            var expectedFamily = id == 401 ? ERecipeType.Smelt : assembler ? ERecipeType.Assemble : ERecipeType.Chemical;
            Program.Check(recipe.Type == expectedFamily && recipe.TimeSpend > 0, "Recipe cannot execute in its specified facility.");
            string family = id == 401 ? "smelter" : assembler ? "assembler" : "chemical";
            double[] speeds = facts.GetProperty("facility_speeds").GetProperty(family).EnumerateArray().Select(v => v.GetDouble()).ToArray();
            double[] expectedRates = id == 401 ? [120, 240, 360] : assembler ? [60, 80, 120, 240] : [60, 120];
            for (int tier = 0; tier < speeds.Length; tier++)
            {
                double rate = ticks * 60 * speeds[tier] * recipe.ResultCounts[0] / recipe.TimeSpend;
                Program.Check(Math.Abs(rate - expectedRates[tier]) < 0.0001, "Native baseline/higher-tier rate mismatch for " + id);
            }
            // Eligibility follows the inspected native rule; the test does not
            // run the native producer or simulate spraying/extra-product timing.
            bool productive = !recipe.NonProductive && recipe.Items.All(input => flags.GetProperty(input.ToString()).GetBoolean());
            Program.Check(productive == (id != 402 && id != 404), "Native proliferation eligibility mismatch.");
            foreach (int input in recipe.Items.Where(item => item >= 5201 && item <= 5206))
                Program.Check(seen.Contains(input), "Fog-input dependency cycle or unavailable earlier tier.");
            seen.Add(recipe.Results[0]);
        }
        var downstream = facts.GetProperty("downstream_research").EnumerateArray().ToArray();
        Program.Check(downstream.Length == 5, "Missing downstream research coverage.");
        foreach (var tech in downstream)
        {
            Program.Check(tech.GetProperty("Items").EnumerateArray().Any(i => i.GetInt32() == 5201), "Native research does not consume Matrix.");
            Program.Check(tech.GetProperty("PreItem").EnumerateArray().All(i => seen.Contains(i.GetInt32())), "Synthesis does not supply a discovery material.");
        }
    }
}
