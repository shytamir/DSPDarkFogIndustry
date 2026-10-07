using DSPDarkFogIndustry;

internal static class SaveChecks
{
    internal static void Run()
    {
        int[] gates = [1133, 1822, 1816, 1823, 1817, 1818];
        for (int mask = 0; mask < 64; mask++)
        {
            var history = new GameHistoryData();
            history.UnlockedRecipes.Add(1);
            for (int i = 0; i < gates.Length; i++)
                if ((mask & (1 << i)) != 0) history.Researched.Add(gates[i]);
            var expected = Enumerable.Range(0, 6).Where(i => (mask & (1 << i)) != 0).Select(i => i + 401).ToArray();
            Program.Check(SaveReconciliation.Reconcile(history, true) == 0 && history.UnlockCalls.Count == 0 && history.UnlockedRecipes.SetEquals([1]), "Save preview awarded recipes.");
            Program.Check(SaveReconciliation.Reconcile(history) == expected.Length, "Wrong number of grants for gate subset.");
            Program.Check(history.UnlockCalls.SequenceEqual(expected), "Reconciliation grants a wrong or unrelated recipe.");
            Program.Check(history.UnlockedRecipes.SetEquals(expected.Append(1)), "Reconciliation changed existing unlocks.");
            Program.Check(history.Researched.Count == expected.Length, "Reconciliation changed research state.");
            Program.Check(SaveReconciliation.Reconcile(history) == 0 && history.UnlockCalls.Count == expected.Length, "Repeated import duplicated awards.");
        }
        var partial = new GameHistoryData();
        partial.Researched.UnionWith([1133, 1816]);
        partial.UnlockedRecipes.UnionWith([401, 406, 1]);
        Program.Check(SaveReconciliation.Reconcile(partial) == 1 && partial.UnlockCalls.SequenceEqual([403]), "Existing unlocks were re-awarded or an earlier-chain gate was incorrectly added.");
        var otherSave = new GameHistoryData();
        Program.Check(SaveReconciliation.Reconcile(otherSave) == 0 && otherSave.UnlockedRecipes.Count == 0, "Research leaked into another save/session.");
        Program.Check(SaveReconciliation.Reconcile(partial) == 0 && partial.UnlockedRecipes.SetEquals([1, 401, 403, 406]), "Returning to a save changed valid existing state.");
        partial.Researched.Add(1822);
        Program.Check(SaveReconciliation.Reconcile(partial) == 1 && partial.RecipeUnlocked(402), "A newly researched gate was missed at later import.");
    }
}
