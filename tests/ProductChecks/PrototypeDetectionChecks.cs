using DSPDarkFogIndustry;

internal static class PrototypeDetectionChecks
{
    internal static void Run()
    {
        Program.Run(nameof(PreservesCategoriesAndOtherDetectors), PreservesCategoriesAndOtherDetectors);
        Program.Run(nameof(DisconnectsBeforeRecipeAccess), DisconnectsBeforeRecipeAccess);
    }

    private static void PreservesCategoriesAndOtherDetectors()
    {
        // Detector names, not category numbers, are the native factory's dispatch key.
        var rows = new[]
        {
            new AbnormalityProto { ID = 3, Name = "tech", DeterminatorName = "ABN_ProtoData" },
            new AbnormalityProto { ID = 4, Name = "recipe", DeterminatorName = "ABN_ProtoData" },
            new AbnormalityProto { ID = 91, Name = "another prototype", DeterminatorName = "ABN_ProtoData" },
            new AbnormalityProto { ID = 16, Name = "unlock", DeterminatorName = "ABN_RecipeUnlockCondition" },
            new AbnormalityProto { ID = 92, Name = "other", DeterminatorName = "AnotherDetector" },
            new AbnormalityProto { ID = 7, Name = "inactive", DeterminatorName = "" },
            new AbnormalityProto { ID = 19, Name = "unused", DeterminatorName = null }
        };
        var categories = rows.Select(row => (row.ID, row.Name)).ToArray();
        var table = new AbnormalityProtoSet { dataArray = rows };

        PrototypeDetection.Disable(table);
        PrototypeDetection.Disable(table);

        Program.Check(ReferenceEquals(table.dataArray, rows)
            && table.dataArray.Select(row => (row.ID, row.Name)).SequenceEqual(categories),
            "Disconnection changed category records needed to interpret saved history.");
        Program.Check(rows.Where(row => !string.IsNullOrEmpty(row.DeterminatorName))
            .Select(row => row.DeterminatorName).SequenceEqual(["ABN_RecipeUnlockCondition", "AnotherDetector"]),
            "Prototype detectors remain eligible for creation, or another detector was disconnected.");
        Program.Check(rows[5].DeterminatorName == "" && rows[6].DeterminatorName == null,
            "Disconnection changed an already inactive category.");
    }

    private static void DisconnectsBeforeRecipeAccess()
    {
        var fixture = RegistrationChecks.CreateFixture();
        LDB.abnormalities = new AbnormalityProtoSet
        {
            dataArray = [new AbnormalityProto { ID = 4, DeterminatorName = "ABN_ProtoData" }]
        };
        LDB.Recipes = fixture.recipes;
        LDB.items = fixture.items;
        LDB.techs = fixture.techs;
        LDB.BeforeRecipeAccess = () => Program.Check(
            LDB.abnormalities.dataArray.All(row => string.IsNullOrEmpty(row.DeterminatorName)),
            "Recipe access occurred before prototype detectors were disconnected.");
        try
        {
            RegistrationPatch.Prefix();
            RegistrationPatch.Prefix();
            Program.Check(fixture.recipes.dataArray.Length == 7 && fixture.recipes.LookupRebuilds == 1,
                "Detector disconnection prevented or duplicated recipe registration.");
        }
        finally
        {
            LDB.BeforeRecipeAccess = null;
        }
    }
}
