using System;
using System.Collections.Generic;

namespace DSPDarkFogIndustry
{
    internal static class RecipeRegistration
    {
        private static RecipeProtoSet registeredTable;

        internal static bool Register(RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet techs, SynthesisRecipe[] definitions = null)
        {
            if (ReferenceEquals(registeredTable, recipes))
            {
                return false;
            }

            definitions = definitions ?? SynthesisRecipes.Defaults;
            var additions = new RecipeProto[definitions.Length];
            var unlocks = new Dictionary<TechProto, List<int>>();

            // Finish preflight before touching any native table or technology.
            for (int i = 0; i < additions.Length; i++)
            {
                var definition = definitions[i];
                foreach (var existing in recipes.dataArray)
                {
                    if (existing.ID == definition.Id || existing.GridIndex == definition.Grid)
                    {
                        throw new InvalidOperationException("DSP Dark Fog Industry: recipe ID " + definition.Id +
                            " or grid " + definition.Grid + " is occupied. Resolve the content-mod conflict before loading a save.");
                    }
                }

                var product = items.Select(definition.Product);
                if (product == null)
                {
                    throw new InvalidOperationException("DSP Dark Fog Industry: missing native product " + definition.Product);
                }

                foreach (int input in definition.Inputs)
                {
                    if (items.Select(input) == null)
                    {
                        throw new InvalidOperationException("DSP Dark Fog Industry: missing native input " + input);
                    }
                }

                var gate = techs.Select(definition.Gate);
                if (gate == null)
                {
                    throw new InvalidOperationException("DSP Dark Fog Industry: missing native gate " + definition.Gate);
                }

                List<int> ids;
                if (!unlocks.TryGetValue(gate, out ids))
                {
                    ids = new List<int>(gate.UnlockRecipes ?? new int[0]);
                    unlocks.Add(gate, ids);
                }
                if (!ids.Contains(definition.Id))
                {
                    ids.Add(definition.Id);
                }

                additions[i] = definition.Create(product);
            }

            var combined = new RecipeProto[recipes.dataArray.Length + additions.Length];
            Array.Copy(recipes.dataArray, combined, recipes.dataArray.Length);
            Array.Copy(additions, 0, combined, recipes.dataArray.Length, additions.Length);
            foreach (var gate in unlocks)
            {
                gate.Key.UnlockRecipes = gate.Value.ToArray();
            }

            recipes.dataArray = combined;
            recipes.OnAfterDeserialize();
            registeredTable = recipes;
            return true;
        }
    }
}
