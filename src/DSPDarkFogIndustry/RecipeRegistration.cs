using System;
using System.Collections.Generic;

namespace DSPDarkFogIndustry
{
    internal static class RecipeRegistration
    {
        private static RecipeProtoSet registeredTable;

        internal static bool Register(RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet techs)
        {
            if (ReferenceEquals(registeredTable, recipes))
            {
                return false;
            }

            var additions = new RecipeProto[SynthesisRecipes.All.Length];
            var gates = new TechProto[additions.Length];
            var unlocks = new int[additions.Length][];

            // Finish preflight before touching any native table or technology.
            for (int i = 0; i < additions.Length; i++)
            {
                var definition = SynthesisRecipes.All[i];
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

                gates[i] = techs.Select(definition.Gate);
                if (gates[i] == null)
                {
                    throw new InvalidOperationException("DSP Dark Fog Industry: missing native gate " + definition.Gate);
                }

                var ids = new List<int>(gates[i].UnlockRecipes);
                if (!ids.Contains(definition.Id))
                {
                    ids.Add(definition.Id);
                }

                unlocks[i] = ids.ToArray();
                additions[i] = definition.Create(product);
            }

            var combined = new RecipeProto[recipes.dataArray.Length + additions.Length];
            Array.Copy(recipes.dataArray, combined, recipes.dataArray.Length);
            Array.Copy(additions, 0, combined, recipes.dataArray.Length, additions.Length);
            for (int i = 0; i < gates.Length; i++)
            {
                gates[i].UnlockRecipes = unlocks[i];
            }

            recipes.dataArray = combined;
            recipes.OnAfterDeserialize();
            registeredTable = recipes;
            return true;
        }
    }
}
