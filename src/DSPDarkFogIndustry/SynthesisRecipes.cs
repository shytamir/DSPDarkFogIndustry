namespace DSPDarkFogIndustry
{
    internal sealed class SynthesisRecipe
    {
        internal readonly int Id;
        internal readonly int Product;
        internal readonly int Gate;
        internal readonly int Grid;
        internal readonly int Ticks;
        internal readonly int OutputCount;
        internal readonly ERecipeType Family;
        internal readonly int[] Inputs;

        internal SynthesisRecipe(
            int id, int product, int gate, int grid, int ticks,
            int outputCount, ERecipeType family, params int[] inputs)
        {
            Id = id;
            Product = product;
            Gate = gate;
            Grid = grid;
            Ticks = ticks;
            OutputCount = outputCount;
            Family = family;
            Inputs = inputs;
        }

        internal RecipeProto Create(ItemProto product)
        {
            var counts = new int[Inputs.Length];
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = 1;
            }

            return new RecipeProto
            {
                ID = Id,
                SID = "",
                Name = product.Name,
                Type = Family,
                Handcraft = Family == ERecipeType.Assemble,
                Explicit = true,
                TimeSpend = Ticks,
                GridIndex = Grid,
                Items = (int[])Inputs.Clone(),
                ItemCounts = counts,
                Results = new[] { Product },
                ResultCounts = new[] { OutputCount },
                // Empty icon fields use the product icon during native preload.
                IconPath = "",
                IconTag = "",
                Description = "",
                // Native preload also checks whether every input permits extra products.
                NonProductive = false
            };
        }
    }

    internal static class SynthesisRecipes
    {
        // IDs and input ordering are persisted by native saves. Keep them stable.
        // Grid 1809-1814 is the right end of the Items tab's bottom row.
        // At 60 ticks/second, 45 ticks gives 60/min in the 0.75x Assembler Mk.I.
        internal static readonly SynthesisRecipe[] All =
        {
            new SynthesisRecipe(
                id: 401, product: Item.EnergyShard, gate: Technology.ParticleControl, grid: 1809,
                ticks: 60, outputCount: 2, family: ERecipeType.Smelt,
                inputs: new[] { Item.FractalSilicon }),
            new SynthesisRecipe(
                id: 402, product: Item.DarkFogMatrix, gate: Technology.Corvette, grid: 1810,
                ticks: 45, outputCount: 1, family: ERecipeType.Assemble,
                inputs: new[] { Item.EnergyShard, Item.Corvette }),
            new SynthesisRecipe(
                id: 403, product: Item.MatterRecombinator, gate: Technology.CrystalShellSet, grid: 1811,
                ticks: 45, outputCount: 1, family: ERecipeType.Assemble,
                inputs: new[] { Item.DarkFogMatrix, Item.CrystalShellSet }),
            new SynthesisRecipe(
                id: 404, product: Item.SiliconBasedNeuron, gate: Technology.Destroyer, grid: 1812,
                ticks: 45, outputCount: 1, family: ERecipeType.Assemble,
                inputs: new[] { Item.MatterRecombinator, Item.Destroyer }),
            new SynthesisRecipe(
                id: 405, product: Item.NegentropySingularity, gate: Technology.GravityMissileSet, grid: 1813,
                ticks: 45, outputCount: 1, family: ERecipeType.Assemble,
                inputs: new[] { Item.SiliconBasedNeuron, Item.GravityMissileSet }),
            new SynthesisRecipe(
                id: 406, product: Item.CoreElement, gate: Technology.AntimatterCapsule, grid: 1814,
                ticks: 60, outputCount: 1, family: ERecipeType.Chemical,
                inputs: new[] { Item.NegentropySingularity, Item.AntimatterCapsule })
        };

        private static class Item
        {
            internal const int FractalSilicon = 1013;
            internal const int CrystalShellSet = 1606;
            internal const int AntimatterCapsule = 1608;
            internal const int GravityMissileSet = 1611;
            internal const int Corvette = 5111;
            internal const int Destroyer = 5112;
            internal const int DarkFogMatrix = 5201;
            internal const int SiliconBasedNeuron = 5202;
            internal const int MatterRecombinator = 5203;
            internal const int NegentropySingularity = 5204;
            internal const int CoreElement = 5205;
            internal const int EnergyShard = 5206;
        }

        private static class Technology
        {
            internal const int ParticleControl = 1133;
            internal const int CrystalShellSet = 1816;
            internal const int GravityMissileSet = 1817;
            internal const int AntimatterCapsule = 1818;
            internal const int Corvette = 1822;
            internal const int Destroyer = 1823;
        }
    }
}
