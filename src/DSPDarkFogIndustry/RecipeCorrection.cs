using System;
using System.Collections.Generic;
using System.Linq;

namespace DSPDarkFogIndustry
{
    internal static class RecipeCorrection
    {
        internal static int[] Apply(RecipeConfiguration configuration, RecipeCatalogue catalogue, Action<string> report)
        {
            var durations = new int[configuration.Recipes.Length];
            for (int recipeIndex = 0; recipeIndex < durations.Length; recipeIndex++)
            {
                var recipe = configuration.Recipes[recipeIndex];
                CorrectId(recipe.Machine, catalogue.Machines.Keys, report);
                CorrectId(recipe.Technology, catalogue.Technologies.Keys, report);
                CorrectId(recipe.OutputItem, catalogue.Items.Keys, report);
                var used = new HashSet<int>();
                bool productive = true;
                for (int row = 0; row < recipe.InputItems.Length; row++)
                {
                    var item = recipe.InputItems[row];
                    var number = new NumericSetting(item);
                    // Missing unused rows and explicit numeric zero both stay unused.
                    if (number.IsZero || (!number.IsNumeric && item.Default == 0))
                    {
                        Set(item, 0, "unused ingredient row", report);
                    }
                    else
                    {
                        var available = catalogue.Items.Keys.Where(id => !used.Contains(id)).ToArray();
                        if (available.Length == 0) { Set(item, 0, "no distinct ingredient ID remains", report); }
                        else { CorrectId(item, available, report); used.Add(item.Value); productive &= catalogue.Items[item.Value].Productive; }
                    }
                    if (item.Value == 0) { Set(recipe.InputCounts[row], 0, "unused ingredient row", report); }
                }

                var limits = new RecipeLimits(catalogue, catalogue.Machines[recipe.Machine.Value].Family, productive && used.Count > 0);
                int inputMaximum = limits.InputMaximum(limits.MaxTicks);
                if (used.Count > 0 && inputMaximum < 1) { throw new InvalidOperationException("No representable input buffer for " + recipe.Machine.Value); }
                int largestInput = 0;
                for (int row = 0; row < recipe.InputItems.Length; row++)
                {
                    if (recipe.InputItems[row].Value == 0) { continue; }
                    var count = recipe.InputCounts[row];
                    Set(count, new NumericSetting(count).Nearest(1, inputMaximum), "representable ingredient quantity", report);
                    largestInput = Math.Max(largestInput, count.Value);
                }

                // Quantities precede rate. Search the finite native duration domain
                // for a quantity that can produce an integral Int32 rate.
                var output = new NumericSetting(recipe.OutputCount);
                int bestCount = 0;
                for (int ticks = 1; ticks <= limits.MaxTicks; ticks++)
                {
                    if (largestInput > limits.InputMaximum(ticks)) { continue; }
                    int step = limits.CountStep(ticks);
                    int max = Math.Min(limits.OutputMaximum(ticks), limits.MaximumCountForRate(ticks));
                    max -= max % step;
                    if (max < step) { continue; }
                    int candidate = output.Nearest(step, max, step);
                    if (bestCount == 0 || output.Distance(candidate) < output.Distance(bestCount) ||
                        (output.Distance(candidate) == output.Distance(bestCount) && candidate < bestCount)) { bestCount = candidate; }
                }
                if (bestCount == 0) { throw new InvalidOperationException("No representable recipe for " + recipe.Machine.Value); }
                Set(recipe.OutputCount, bestCount, "representable output quantity and integer rate", report);
                var requestedRate = new NumericSetting(recipe.Rate);
                int bestRate = 0;
                for (int ticks = 1; ticks <= limits.MaxTicks; ticks++)
                {
                    if (largestInput > limits.InputMaximum(ticks) || bestCount > limits.OutputMaximum(ticks) ||
                        bestCount > limits.MaximumCountForRate(ticks) || bestCount % limits.CountStep(ticks) != 0) { continue; }
                    int candidate = limits.Rate(ticks, bestCount);
                    if (candidate < 1) { continue; }
                    if (bestRate == 0 || requestedRate.Distance(candidate) < requestedRate.Distance(bestRate) ||
                        (requestedRate.Distance(candidate) == requestedRate.Distance(bestRate) && candidate < bestRate))
                    {
                        bestRate = candidate;
                        durations[recipeIndex] = ticks;
                    }
                }
                if (bestRate == 0) { throw new InvalidOperationException("No attainable integer rate for " + recipe.Machine.Value); }
                Set(recipe.Rate, bestRate, "attainable basic-machine products/minute", report);
            }
            return durations;
        }

        private static void CorrectId(IntegerSetting setting, IEnumerable<int> eligible, Action<string> report)
        {
            var requested = new NumericSetting(setting);
            int best = 0;
            foreach (int id in eligible)
            {
                if (best == 0 || requested.Distance(id) < requested.Distance(best) ||
                    (requested.Distance(id) == requested.Distance(best) && id < best)) { best = id; }
            }
            if (best == 0) { throw new InvalidOperationException("No eligible IDs for " + setting.Key); }
            Set(setting, best, "nearest eligible ID (distinct for active ingredients)", report);
        }

        private static void Set(IntegerSetting setting, int value, string reason, Action<string> report)
        {
            var requested = new NumericSetting(setting);
            setting.Set(value);
            if (!requested.IsNumeric || !requested.Equals(value))
            {
                report("[" + setting.Section + "] " + setting.Key + ": '" + setting.Raw + "' -> " + value +
                    "; " + (requested.IsNumeric ? reason : "not numeric; documented default followed by " + reason) + ".");
            }
        }
    }
}
