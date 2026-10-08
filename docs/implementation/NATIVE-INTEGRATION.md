# Native integration

The source owns the implementation. The [recipe requirements](../planning/MVP-RECIPES.md)
describe player behavior; [PROJECT](../PROJECT.md) records decisions and acceptance.

## Source map

- [Plugin](../../src/DSPDarkFogIndustry/Plugin.cs) installs the Harmony patches.
- [DetectorInitializationPatch](../../src/DSPDarkFogIndustry/DetectorInitializationPatch.cs)
  initializes an empty native detector collection and skips detector creation.
  The prototype catalogue, game mode and saved abnormal history are left intact.
- [RegistrationPatch](../../src/DSPDarkFogIndustry/RegistrationPatch.cs) runs before
  native preload. [RecipeRegistration](../../src/DSPDarkFogIndustry/RecipeRegistration.cs)
  checks for conflicts, adds recipes and technology unlocks, and rebuilds the lookup.
- [SynthesisRecipes](../../src/DSPDarkFogIndustry/SynthesisRecipes.cs) defines item
  and technology IDs, recipe IDs, ingredients, durations and selector positions.
  Comments explain the timing and save-compatibility constraints.
- [SavePatch](../../src/DSPDarkFogIndustry/SavePatch.cs) and
  [SaveReconciliation](../../src/DSPDarkFogIndustry/SaveReconciliation.cs) add missing
  recipe unlocks after a full save import. Preview imports are skipped.

Production, proliferation, research discovery and handcraft execution use the
game's existing behavior. The plugin does not patch those systems.

## References and hosted compilation

The [shim declarations and ledger](../../tools/ReferenceShims/README.md) describe
every external reference. The [metadata checker](../../tools/ReferenceChecks/Program.cs)
checks declarations, compiled references, plugin identity and Harmony targets.
See [local development](../LOCAL-DEVELOPMENT.md) for real-reference checks and
[build and packaging](../BUILD-AND-PACKAGING.md) for the shared build procedure.

## Save and removal boundary

Recipe IDs and input ordering are saved by the game. Keep them stable. Saves
using these recipes require the mod; removing it does not clear recipe references
from machines. Restore a pre-mod backup when removing the mod.

## Evidence

The [detector-initialization record](HF-01-VALIDATION.md) covers the correction and
links to the rejected catalogue-edit attempt. Owner disposition is in PROJECT.

The [archived integration investigation](../archive/2026-10-08-mvp/NATIVE-INTEGRATION.md)
retains the native lifecycle findings, reference identities, rate calculations,
dependency-route proof and removal analysis. These are dated evidence, not a
second implementation specification. Owner observations are recorded in
[PROJECT](../PROJECT.md#owner-acceptance-record--2026-10-08).
