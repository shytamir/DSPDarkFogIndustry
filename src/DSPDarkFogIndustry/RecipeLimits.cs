using System;

namespace DSPDarkFogIndustry
{
    internal sealed class RecipeLimits
    {
        private readonly RecipeCatalogue catalogue;
        private readonly ERecipeType family;
        private readonly bool extraProducts;
        internal readonly int MaxTicks;
        private readonly long rateNumerator;

        internal RecipeLimits(RecipeCatalogue catalogue, ERecipeType family, bool extraProducts)
        {
            this.catalogue = catalogue;
            this.family = family;
            this.extraProducts = extraProducts;
            rateNumerator = 3600L * catalogue.BasicSpeed(family);
            MaxTicks = int.MaxValue / 100000;
            foreach (var machine in catalogue.Machines.Values)
            {
                if (machine.Family != family) { continue; }
                // Native accumulators add before the next tick subtracts a cycle.
                MaxTicks = Math.Min(MaxTicks, (int)((int.MaxValue - 2L * machine.Speed + 1) / 10000));
                MaxTicks = Math.Min(MaxTicks, (int)((int.MaxValue - 5L * machine.Speed / 2 + 1) / 100000));
            }
            if (MaxTicks < 1) { throw new InvalidOperationException("No representable duration for " + family + " machines."); }
        }

        internal int InputMaximum(int ticks)
        {
            long maximum = int.MaxValue;
            foreach (var machine in catalogue.Machines.Values)
            {
                if (machine.Family != family) { continue; }
                long demand = Math.Max(2, 2L * machine.Speed * 180 / (ticks * 10000L) + 1);
                long capacity = Math.Max(demand, family == ERecipeType.Smelt ? 50 : 20);
                // A held inserter delivery can arrive after demand was satisfied.
                // Preserve Int32 room for four spray points per buffered item.
                long room = int.MaxValue / 4 - (long)short.MaxValue * machine.Slots;
                maximum = Math.Min(maximum, room / capacity);
            }
            return (int)Math.Max(0, maximum);
        }

        internal int OutputMaximum(int ticks)
        {
            if (family == ERecipeType.Smelt) { return 100; }
            long pending = 0;
            if (extraProducts)
            {
                foreach (var machine in catalogue.Machines.Values)
                {
                    if (machine.Family != family) { continue; }
                    long increment = 5L * machine.Speed / 2;
                    pending = Math.Max(pending, (increment + ticks * 100000L - 1) / (ticks * 100000L));
                }
            }
            return (int)(int.MaxValue / ((family == ERecipeType.Assemble ? 10 : 20) + pending));
        }

        internal int CountStep(int ticks)
        {
            if (catalogue.BasicSpeed(family) >= ticks * 10000L) { return 1; }
            long denominator = ticks * 10000L;
            return (int)(denominator / Gcd(rateNumerator, denominator));
        }

        internal int MaximumCountForRate(int ticks)
        {
            long denominator = ticks * 10000L;
            return catalogue.BasicSpeed(family) >= denominator ? int.MaxValue / 3600 :
                (int)Math.Min(int.MaxValue, int.MaxValue * denominator / rateNumerator);
        }

        internal int Rate(int ticks, int count)
        {
            long denominator = ticks * 10000L;
            return catalogue.BasicSpeed(family) >= denominator ? checked(3600 * count) :
                checked((int)(rateNumerator * count / denominator));
        }

        private static long Gcd(long a, long b)
        {
            while (b != 0) { long remainder = a % b; a = b; b = remainder; }
            return a;
        }
    }
}
