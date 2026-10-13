namespace DSPDarkFogIndustry
{
    internal static class RecipeStartup
    {
        private static RecipeProtoSet preparedTable;
        internal static RecipeCatalogue Catalogue { get; private set; }

        internal static void Prepare(RecipeProtoSet recipes, ItemProtoSet items, TechProtoSet technologies, ModelProtoSet models)
        {
            if (ReferenceEquals(preparedTable, recipes))
            {
                return;
            }

            var catalogue = new RecipeCatalogue(items, technologies, models);
            RecipeRegistration.Register(recipes, items, technologies);
            Catalogue = catalogue;
            preparedTable = recipes;
        }
    }
}
