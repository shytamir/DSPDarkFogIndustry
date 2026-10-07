namespace DSPDarkFogIndustry
{
    internal sealed class SynthesisRecipe
    {
        internal readonly int Id, Product, Gate, Grid, Ticks, OutputCount;
        internal readonly ERecipeType Family;
        internal readonly int[] Inputs;

        internal SynthesisRecipe(int id, int product, int gate, int grid, int ticks, int outputCount, ERecipeType family, params int[] inputs)
        {
            Id = id; Product = product; Gate = gate; Grid = grid;
            Ticks = ticks; OutputCount = outputCount; Family = family; Inputs = inputs;
        }

        internal RecipeProto Create(ItemProto product)
        {
            var counts = new int[Inputs.Length];
            for (int i = 0; i < counts.Length; i++) counts[i] = 1;
            return new RecipeProto
            {
                ID = Id, SID = "", Name = product.Name,
                Type = Family, Handcraft = Family == ERecipeType.Assemble,
                Explicit = true, TimeSpend = Ticks, GridIndex = Grid,
                Items = (int[])Inputs.Clone(), ItemCounts = counts,
                Results = new[] { Product }, ResultCounts = new[] { OutputCount },
                IconPath = "", IconTag = "", Description = "", NonProductive = false
            };
        }
    }

    internal static class SynthesisRecipes
    {
        // IDs and input ordering are persisted by native saves. Keep them stable.
        internal static readonly SynthesisRecipe[] All =
        {
            new SynthesisRecipe(401, 5206, 1133, 1809, 60, 2, ERecipeType.Smelt, 1013),
            new SynthesisRecipe(402, 5201, 1822, 1810, 45, 1, ERecipeType.Assemble, 5206, 5111),
            new SynthesisRecipe(403, 5203, 1816, 1811, 45, 1, ERecipeType.Assemble, 5201, 1606),
            new SynthesisRecipe(404, 5202, 1823, 1812, 45, 1, ERecipeType.Assemble, 5203, 5112),
            new SynthesisRecipe(405, 5204, 1817, 1813, 45, 1, ERecipeType.Assemble, 5202, 1611),
            new SynthesisRecipe(406, 5205, 1818, 1814, 60, 1, ERecipeType.Chemical, 5204, 1608)
        };
    }
}
