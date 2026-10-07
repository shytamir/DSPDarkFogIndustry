namespace DSPDarkFogIndustry
{
    internal static class SaveReconciliation
    {
        internal static int Reconcile(GameHistoryData history, bool isPreview = false)
        {
            if (isPreview) return 0;
            int added = 0;
            foreach (var recipe in SynthesisRecipes.All)
            {
                if (history.TechState(recipe.Gate).unlocked && !history.RecipeUnlocked(recipe.Id))
                {
                    history.UnlockRecipe(recipe.Id);
                    added++;
                }
            }
            return added;
        }
    }
}
