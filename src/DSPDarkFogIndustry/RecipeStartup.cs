namespace DSPDarkFogIndustry
{
    internal static class RecipeStartup
    {
        private static RecipeProtoSet preparedTable;
        internal static RecipeCatalogue Catalogue { get; private set; }
        internal static RecipeConfiguration Configuration { get; set; }
        internal static SynthesisRecipe[] EffectiveRecipes { get; private set; } = SynthesisRecipes.Defaults;

        internal static void Prepare(RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet technologies, ModelProtoSet models)
        {
            if (ReferenceEquals(preparedTable, recipes))
            {
                return;
            }

            var catalogue = new RecipeCatalogue(items, technologies, models);
            Configuration?.Correct(catalogue);
            if (Configuration != null) { ConfigurationReference.Write(Configuration.File, catalogue); }
            Configuration?.Save();
            var definitions = Configuration == null ? SynthesisRecipes.Defaults : Configuration.CreateRecipes(catalogue);
            RecipeRegistration.Register(recipes, items, technologies, definitions);
            EffectiveRecipes = definitions;
            Catalogue = catalogue;
            preparedTable = recipes;
        }
    }
}
