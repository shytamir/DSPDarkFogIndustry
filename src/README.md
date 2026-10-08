# Product source

The plugin entry point is [Plugin.cs](DSPDarkFogIndustry/Plugin.cs).
[Recipe definitions](DSPDarkFogIndustry/SynthesisRecipes.cs),
[registration](DSPDarkFogIndustry/RecipeRegistration.cs) and
[save reconciliation](DSPDarkFogIndustry/SaveReconciliation.cs) contain the mod's
behavior. The two Harmony patches connect it to native startup and save loading.

See [local development](../docs/LOCAL-DEVELOPMENT.md) for building and
[tests](../tests/README.md) for regression coverage.
