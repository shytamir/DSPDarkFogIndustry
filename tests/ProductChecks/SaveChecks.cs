using DSPDarkFogIndustry;

internal static class SaveChecks
{
    // Independent save compatibility expectations: technology -> saved recipe ID.
    private static readonly (int Technology, int Recipe)[] Unlocks =
    [
        (1133, 401), (1822, 402), (1816, 403),
        (1823, 404), (1817, 405), (1818, 406)
    ];

    internal static void Run()
    {
        Program.Run(nameof(SkipsSavePreviews), SkipsSavePreviews);
        Program.Run(nameof(GrantsOnlyResearchedRecipes), GrantsOnlyResearchedRecipes);
        Program.Run(nameof(PreservesExistingUnlocks), PreservesExistingUnlocks);
        Program.Run(nameof(KeepsSavesIndependent), KeepsSavesIndependent);
    }

    private static void SkipsSavePreviews()
    {
        var history = new GameHistoryData();
        history.Researched.UnionWith(Unlocks.Select(unlock => unlock.Technology));
        history.UnlockedRecipes.Add(1);

        Program.Check(SaveReconciliation.Reconcile(history, isPreview: true) == 0
            && history.UnlockCalls.Count == 0 && history.UnlockedRecipes.SetEquals([1]),
            "Save preview awarded recipes.");
    }

    private static void GrantsOnlyResearchedRecipes()
    {
        // Check every research combination, including later gates without earlier ones.
        for (int mask = 0; mask < (1 << Unlocks.Length); mask++)
        {
            var researched = Unlocks.Where((_, index) => (mask & (1 << index)) != 0).ToArray();
            var expectedRecipes = researched.Select(unlock => unlock.Recipe).ToArray();
            var history = new GameHistoryData();
            history.Researched.UnionWith(researched.Select(unlock => unlock.Technology));
            history.UnlockedRecipes.Add(1);

            Program.Check(SaveReconciliation.Reconcile(history) == expectedRecipes.Length,
                $"Research combination {mask}: wrong number of recipe grants.");
            Program.Check(history.UnlockCalls.SequenceEqual(expectedRecipes),
                $"Research combination {mask}: granted an unresearched or unrelated recipe.");
            Program.Check(history.UnlockedRecipes.SetEquals(expectedRecipes.Append(1)),
                $"Research combination {mask}: changed existing unlocks.");
            Program.Check(history.Researched.SetEquals(researched.Select(unlock => unlock.Technology)),
                $"Research combination {mask}: changed research state.");
            Program.Check(SaveReconciliation.Reconcile(history) == 0 && history.UnlockCalls.Count == expectedRecipes.Length,
                $"Research combination {mask}: repeated import duplicated awards.");
        }
    }

    private static void PreservesExistingUnlocks()
    {
        var history = new GameHistoryData();
        history.Researched.UnionWith([1133, 1816]);
        history.UnlockedRecipes.UnionWith([401, 406, 1]);

        Program.Check(SaveReconciliation.Reconcile(history) == 1 && history.UnlockCalls.SequenceEqual([403]),
            "Existing unlocks were re-awarded or an unresearched gate was added.");
        Program.Check(history.UnlockedRecipes.SetEquals([1, 401, 403, 406]),
            "Existing recipe unlocks were removed.");

        history.Researched.Add(1822);
        Program.Check(SaveReconciliation.Reconcile(history) == 1 && history.RecipeUnlocked(402),
            "A newly researched gate was missed at a later import.");
    }

    private static void KeepsSavesIndependent()
    {
        var firstSave = new GameHistoryData();
        firstSave.Researched.Add(1133);
        SaveReconciliation.Reconcile(firstSave);

        var otherSave = new GameHistoryData();
        Program.Check(SaveReconciliation.Reconcile(otherSave) == 0 && otherSave.UnlockedRecipes.Count == 0,
            "Research leaked into another save.");
        Program.Check(SaveReconciliation.Reconcile(firstSave) == 0 && firstSave.UnlockedRecipes.SetEquals([401]),
            "Returning to a save changed its existing unlocks.");
    }
}
