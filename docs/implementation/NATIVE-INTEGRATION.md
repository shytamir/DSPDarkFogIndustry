# Native integration

The source owns the implementation. The [recipe requirements](../planning/MVP-RECIPES.md)
describe player behavior; [PROJECT](../PROJECT.md) records decisions and acceptance.

## Source map

- [Plugin](../../src/DSPDarkFogIndustry/Plugin.cs) installs the Harmony patches.
- [DetectorInitializationPatch](../../src/DSPDarkFogIndustry/DetectorInitializationPatch.cs)
  initializes an empty native detector collection and skips detector creation.
  The prototype catalogue, game mode and saved abnormal history are left intact.
- [RegistrationPatch](../../src/DSPDarkFogIndustry/RegistrationPatch.cs) runs before
  the first item preload, after models have initialized their prefab descriptors.
  [RecipeStartup](../../src/DSPDarkFogIndustry/RecipeStartup.cs) prepares one
  [catalogue](../../src/DSPDarkFogIndustry/RecipeCatalogue.cs) per recipe table,
  using model descriptors because item descriptors are assigned during preload.
  [RecipeRegistration](../../src/DSPDarkFogIndustry/RecipeRegistration.cs)
  checks for conflicts, adds recipes and technology unlocks, and rebuilds the lookup.
- [SynthesisRecipes](../../src/DSPDarkFogIndustry/SynthesisRecipes.cs) defines item
  and technology defaults, stable recipe IDs and selector positions. Corrected
  startup settings produce the effective records used by registration and save import.
  Comments explain the timing and save-compatibility constraints.
- [SavePatch](../../src/DSPDarkFogIndustry/SavePatch.cs) and
  [SaveReconciliation](../../src/DSPDarkFogIndustry/SaveReconciliation.cs) add missing
  recipe unlocks after a full save import. Preview imports are skipped.

Technology additions accumulate by gate before any native table is changed, so
multiple configured recipes can share a gate. Save reconciliation calls native
`TechUnlocked(id, true)` and awards only missing recipe IDs from the effective
settings. The inspected `AddTechHash` path awards `UnlockRecipes` only on full
record completion. No relock, research replay, machine conversion or queue migration
is performed.

Production, proliferation, research discovery and handcraft execution use the
game's existing behavior. The plugin does not patch those systems.

## References and hosted compilation

[RecipeConfiguration](../../src/DSPDarkFogIndustry/RecipeConfiguration.cs) binds
the six stable recipe sections through BepInEx. A temporary string binding retains
raw numeric text before the public integer binding is created; no global converter
or private config API is changed. Settings are captured at startup, with batched
saving and explicit persistence-error reporting. The file tests use the pinned
public BepInEx binary in an isolated test host; product compilation still uses
shims, and the package contains neither dependency nor generated configuration.

[RecipeCorrection](../../src/DSPDarkFogIndustry/RecipeCorrection.cs) applies the
contract's field precedence. [NumericSetting](../../src/DSPDarkFogIndustry/NumericSetting.cs)
compares decimal distances exactly, including overflow text.
[RecipeLimits](../../src/DSPDarkFogIndustry/RecipeLimits.cs) keeps native duration,
input demand, insertion headroom and output buffers representable, then searches
the finite duration domain for an integral basic-machine rate. Its quantity search
does not change a valid count to accommodate a requested rate. Recursive crafting
and queue arithmetic remain native, as selected in PROJECT.

Machine descriptors whose accelerated speed would overflow native `speedOverride * 180`
are ineligible independently of recipe duration. Native catalogue speeds are far
below that bound. The four-point spray/acceleration assumptions are native values;
mods replacing those tables need separate compatibility evidence.

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

The current reference baseline adopts DSP 0.10.35.29131. Selected native lifecycle
and production methods matched the previously inspected 29104 output; external
bindings are checked against the new assembly rather than inferred from that
comparison. The earlier baseline identity remains in the dated evidence/archive.
The catalogue admits only smelting, assembly and chemical machine families and
uses the basic machine's native speed for each family. A missing basic descriptor
stops registration before recipe mutation.

The [detector-initialization record](HF-01-VALIDATION.md) covers the correction and
links to the rejected catalogue-edit attempt. Owner disposition is in PROJECT.

The [archived integration investigation](../archive/2026-10-08-mvp/NATIVE-INTEGRATION.md)
retains the native lifecycle findings, reference identities, rate calculations,
dependency-route proof and removal analysis. These are dated evidence, not a
second implementation specification. Owner observations are recorded in
[PROJECT](../PROJECT.md#owner-acceptance-record--2026-10-08).
