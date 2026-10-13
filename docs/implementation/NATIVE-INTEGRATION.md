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
the six stable recipe sections and six extra-input sections through BepInEx.
Selectors use string bindings for exact current-language names or IDs; quantities
retain raw numeric text before the public integer binding is created. No global
converter or private config API is changed. Settings are captured at startup.
[ConfigurationLayout](../../src/DSPDarkFogIndustry/ConfigurationLayout.cs) compacts
the ordinary BepInEx save, retaining keys and values while replacing verbose
metadata with shared instructions. Removed legacy reference sections are forgotten
through the public reload API; active recipe snapshots are unchanged.
[ConfigurationReference](../../src/DSPDarkFogIndustry/ConfigurationReference.cs)
writes a separate grouped text reference. Config/reference write failures are
reported independently. The file tests use the pinned
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

[ConfigurationReference](../../src/DSPDarkFogIndustry/ConfigurationReference.cs)
appends native item, technology and eligible-machine names through the ordinary
BepInEx string entries. `Localization.Translate` supplies the active-language
label before item preload has assigned `Proto.name`. Technology visibility and
maximum-level metadata explain unusual choices without rejecting them. Reference
values never feed recipe settings. Generated fixture text was reviewed for section
order, spacing, comments and integer fields; Notepad and Thunderstore Mod Manager
interaction remain owner observations.

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

The [configuration investigation](../archive/2026-10-13-configurability/INVESTIGATION.md)
retains the later native and BepInEx findings. Configurability observations and
acceptance are recorded in [PROJECT](../PROJECT.md#owner-acceptance-record--2026-10-13).

## Configuration language evidence — 2026-10-13

Read-only review at source `00967e34b6355ba78c86b2e2d8ff4dcff2aa04a6`, against
the adopted DSP 0.10.35.29131 assembly SHA-256
`74ac6162cef8394a6021c8475132cae4df4f840871a99e63cc7d2c8297661bc0`.
Environment activation verified all three existing static reference identities.

- [ConfigurationReference.Write](../../src/DSPDarkFogIndustry/ConfigurationReference.cs)
  writes UTF-8 and calls `Localization.Translate` on every item, technology and
  machine name. Introductory text, headings, annotations and baseline-machine
  descriptions are English literals; the file is not wholly translated.
- [RecipeCorrection.Apply](../../src/DSPDarkFogIndustry/RecipeCorrection.cs) builds
  selector dictionaries through the same translation API.
  [IntegerSetting.Resolve/Label](../../src/DSPDarkFogIndustry/RecipeConfiguration.cs)
  matches exact trimmed names case-insensitively and saves an ID with its translated
  label. Duplicate names require IDs. This uses the current language, not an
  English/Chinese alias list.
- Native `GlobalObject.Initialize`, marked `BeforeSceneLoad`, loads localization
  and selects the saved language before scene startup. `VFPreload` later preloads
  items, invoking the existing registration prefix. `RecipeStartup.Prepare` corrects
  settings and writes the reference there. Native `Localization.Translate` looks
  up the selected language's `currentStrings`, falling back to the original key
  if the key/table is unavailable. Chinese (Simplified) uses LCID 2052.
- The reference regenerates during startup; there is no language-change event
  hook that rewrites it while playing. Restart after changing the game language.
  Existing saved numeric IDs retain identity across languages.

Selected native display names were checked directly in `Locale/2052/prototype.txt`
(SHA-256 `350b79de6eb5741372c887d168901221fe9102953ccc4c33432c4e7b7e9a62f8`):

| Type / ID | Simplified Chinese display name |
| --- | --- |
| Item 1013 | 分形硅石 |
| Item 5201 | 黑雾矩阵 |
| Item 5205 | 核心素 |
| Technology 1133 | 粒子可控 |
| Machine 2302 | 电弧熔炉 |
| Machine 2309 | 化工厂 |

These are display strings, not assumed translations of prototype keys: for
example, item 5201's key is `存储单元`, and item 5205's key is `虚粒子`.
Use the native display strings when preparing Chinese player copy. Native source
extracts remain under ignored `.local/inspection/`; no native code or complete
language files are added to tracked source.

The existing [configuration checks](../../tests/ProductChecks/ConfigurationChecks.cs)
cover translated reference entries, exact-name input and stable IDs after label
changes, but their translator is a test double. This review establishes the source
path and native strings, not an observed Chinese-language game session or a captured
game-generated reference. No game was launched, installed file changed or save read.

## Configured production checks

The current static asset probe reconfirmed speeds of 1/2/3 for smelters,
0.75/1/1.5/3 for assemblers and 1/2 for chemical plants. The existing dated
`native-rules.json` retains its original identity; the new checks exercise emitted
configured records against those same observed speeds, not a replaced native loop.
Default configuration reproduces the fixed recipes' arrays, counts, family,
handcraft flag, productivity policy and timing.

Two output items per batch at 90/min in the assembly family emits 60 ticks and
predicts 90/120/180/360 per minute across its four native tiers. Three per batch
at 90/min in the chemical family emits 120 ticks and predicts 90/180. A change
between tier IDs leaves timing unchanged; a family change changes the basic rate
conversion and handcraft eligibility. Higher tiers and acceleration still share
the native one-normal-cycle-per-update ceiling. The source-derived expectations
assume continuous supply, output clearance and full power. The owner report above
records the checks performed in-game.
