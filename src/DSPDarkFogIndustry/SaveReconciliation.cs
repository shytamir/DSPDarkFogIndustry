namespace DSPDarkFogIndustry
{
    internal static class SaveReconciliation
    {
        internal static int Reconcile(GameHistoryData history, bool isPreview = false, SynthesisRecipe[] definitions = null)
        {
            // The save selector imports previews too; it must not award anything.
            if (isPreview)
            {
                return 0;
            }

            int added = 0;
            foreach (var recipe in definitions ?? SynthesisRecipes.Defaults)
            {
                if (history.TechUnlocked(recipe.Gate, true) && !history.RecipeUnlocked(recipe.Id))
                {
                    history.UnlockRecipe(recipe.Id);
                    added++;
                }
            }

            return added;
        }
    }
}
