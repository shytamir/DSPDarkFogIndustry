using System;
using System.Collections.Generic;
using System.Linq;

namespace DSPDarkFogIndustry
{
    internal static class RecipeCorrection
    {
        internal static int[] Apply(RecipeConfiguration configuration, RecipeCatalogue catalogue, Action<string> report)
        {
            var itemNames = catalogue.Items.ToDictionary(pair => pair.Key, pair => Localization.Translate(pair.Value.Name).Trim());
            var technologyNames = catalogue.Technologies.ToDictionary(pair => pair.Key, pair => Localization.Translate(pair.Value.Name).Trim());
            var machineNames = catalogue.Machines.ToDictionary(pair => pair.Key, pair => itemNames[pair.Key]);
            var durations = new int[configuration.Recipes.Length];
            for (int recipeIndex = 0; recipeIndex < durations.Length; recipeIndex++)
            {
                var recipe = configuration.Recipes[recipeIndex];
                recipe.Machine.Resolve(machineNames, false, report);
                recipe.Technology.Resolve(technologyNames, false, report);
                recipe.OutputItem.Resolve(itemNames, false, report);
                var used = new HashSet<int>();
                bool productive = true;
                for (int row = 0; row < recipe.InputItems.Length; row++)
                {
                    var item = recipe.InputItems[row];
                    item.Resolve(itemNames, true, report);
                    if (item.Value != 0 && used.Contains(item.Value))
                    {
                        // A later duplicate uses its own default if that is still
                        // available; otherwise leave the optional slot unused.
                        int fallback = used.Contains(item.Default) ? 0 : item.Default;
                        Set(item, fallback, "duplicate ingredient; default or unused slot", report);
                    }
                    if (item.Value != 0)
                    {
                        used.Add(item.Value);
                        productive &= catalogue.Items[item.Value].Productive;
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
                recipe.Machine.Label(machineNames);
                recipe.Technology.Label(technologyNames);
                recipe.OutputItem.Label(itemNames);
                foreach (var item in recipe.InputItems) { item.Label(itemNames); }
            }
            return durations;
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
