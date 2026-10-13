# Configuration investigation

Static inspection, 2026-10-13. [PROJECT](../PROJECT.md) records authorization and
dispositions; the [roadmap](../planning/ROADMAP.md) defines the work and questions.
This is an initial evidence record, not a complete configuration design or a
current-game compatibility claim. The follow-up below records the bounded checks
requested when refining the roadmap; the initial observations remain identified
separately from the additional evidence.

## Sources and reproduction

Repository source inspected at `ea36542aacc810fa24aea5e9cd5f265bef4af874`:
[plugin startup](../../src/DSPDarkFogIndustry/Plugin.cs),
[recipe definitions](../../src/DSPDarkFogIndustry/SynthesisRecipes.cs),
[registration](../../src/DSPDarkFogIndustry/RecipeRegistration.cs),
[preload patch](../../src/DSPDarkFogIndustry/RegistrationPatch.cs), and
[save reconciliation](../../src/DSPDarkFogIndustry/SaveReconciliation.cs).

The session's single `Enter-Environment.ps1` attempt stopped at changed game
assembly bytes. Diagnosis found installed `Updates/Versions.txt` beginning with
`0.10.35.29131,False,2026-10-12`. The committed
[reference baseline](../../tools/ReferenceShims/reference-baseline.json) still
identifies `0.10.35.29104`; it and the local activation record were left intact.

| Inspected input | Identity |
| --- | --- |
| Current `Assembly-CSharp.dll` | SHA-256 `74ac6162cef8394a6021c8475132cae4df4f840871a99e63cc7d2c8297661bc0` |
| Current `resources.assets` | SHA-256 `459b0a04002bfc0e5be765d38f89d8103d91c0d912e2dee97826200bbabf101b` |
| Pinned BepInEx DLL | 5.4.17; SHA-256 `dc1cb6b58b962bda5aaa1d6b5f9ae14ec174f61836a1a1f96c1a040c7e8381f7`, matching the committed baseline |
| BepInEx source | Local upstream checkout at tag `v5.4.17`, commit `3c3153598b530fb85e623ba226c8cca1b3f4d262` |

After diagnosing drift, ILSpyCmd `11.1.0.9782` read selected types directly from
the changed assembly into a separate hash-keyed local directory. The inspected
types include `RecipeProto`, `ERecipeType`, `AssemblerComponent`, `ForgeTask`,
`LabComponent` and `TechProto`. The retained concept extractor, reviewed before
use, read the current item, recipe and technology assets through UnityPy and
metadata-generated type trees. It found 175 item records, 162 recipe records and
315 technology records. These counts describe catalogues, not valid setting lists.

To reproduce the bounded inspection, identify and hash the two current files,
run ILSpyCmd with `-t <type> <Assembly-CSharp.dll>`, and run the restored concept's
`scripts/extract_native_catalogue.py` with explicit game root, ignored output
directory and local UnityPy path. The full source bundle and extraction limits
are documented in [concept provenance](../concept/EVIDENCE.md). Outputs for this
inspection are under ignored `.local/configurability/<assembly-sha256>/`.
No game assembly was executed, game launched, save read or installed file changed.
The selected checks do not replace review of the baseline change before adoption.

## Existing integration

| Observation | Consequence for configurability |
| --- | --- |
| Startup installs Harmony patches; no settings are bound. Recipe definitions have fixed inputs, one output type, fixed counts/timing and gates. | Configuration loading and validation are new behavior, not an existing option to expose. |
| Registration preflights all recipes before mutating native tables, then stores the registered table identity. | Effective definitions must be ready before native recipe caches are constructed. Live replacement is not established by the current code. |
| Each recipe independently copies its gate's original unlock list during preflight. | If configurable recipes share a gate, later assignment would overwrite earlier additions. Preserve every addition to the shared gate; rejecting shared gates would hide the implementation limitation. |
| Save reconciliation adds missing recipe unlocks when the configured gate is unlocked; it never revokes unlocks. | Changing a gate does not currently relock an already-known recipe. The desired behavior on existing saves needs an explicit decision. |
| Recipe IDs and input ordering already participate in persisted state. | Retaining recipe identities does not by itself make arbitrary content changes safe. |

The current [shim inventory](../../tools/ReferenceShims/README.md) covers the
existing integration, not the new configuration API. Future implementation must
add each external type/member actually used and verify it against real metadata.

## Native findings

**Hand replication is a recipe flag.** `RecipeProto.Type` and `Handcraft` are
separate fields. In the current asset catalogue, smelting contains eight
handcraftable recipes and six machine-only recipes; assembly contains 119 and
seven respectively. Research-type recipes also contain both values. For example,
smelting recipe 1 (Iron Ingot) allows handcrafting, while assembly recipe 143
(Antimatter Capsule) does not. The plugin's existing assembler-only rule is its
current product choice, not a universal rule encoded by machine family. A machine
ID cannot determine the requested behavior without an agreed mapping.

**A machine item, recipe family and speed are distinct.** `ERecipeType` includes
smelting, chemical, refining, assembly, particle, exchange, photon storage,
fractionation and research. Merely naming an enum value does not prove a machine
accepts arbitrary recipes of that family. This inspection has not completed the
machine-prefab capability/speed map or specialized consumer checks in the initial
pass. The follow-up below supplies the ordinary-machine map and identifies the
remaining lab-specific question.

**Integer fields still have native constraints.** `RecipeProto.Preload` requires
matching item/count arrays and, for fractionation, one input and one output. It
raises a duration below one tick to one. `InitRecipeItems` multiplies duration by
10,000 and 100,000 using integer expressions; checking only for a positive Int32
would permit overflow. `AssemblerComponent.UpdateNeeds` populates six input
demand positions, and buffer arithmetic also multiplies counts. These observations
require consumer-specific limits; neither the largest vanilla recipe nor a single
arithmetic bound proves the full valid range. Output handling, queue arithmetic,
ID storage widths and rate representability need the same examination.

**Productivity follows the ingredients.** Native preload enables extra products
only when the recipe permits them and every input's item record is productive.
Changing ingredients can therefore change available proliferation behavior
without an extra plugin rule. Configuration examples and tests must account for
that distinction.

**Existing factory contents can change on import.** In the current
`AssemblerComponent.Import` path, input/output buffers are resized to the current
recipe arrays. When a saved item's ID differs from the current ID at that index,
its saved count is cleared; input proliferation counts are cleared too. Reordering
ingredients can therefore discard buffered material. Count changes and family
switches need additional tracing before a transition policy is selected.

**Handcraft queues retain saved recipe data.** `ForgeTask.Import` reads saved
item/product IDs, counts and timing directly. That method does not reconstruct
them from the newly configured recipe. This is evidence of a separate transition
path, not a conclusion about the whole queue's behavior: its callers, execution
and cancellation/refund paths still need inspection.

## BepInEx file behavior

The following findings come from pinned upstream source, principally
[ConfigFile](https://github.com/BepInEx/BepInEx/blob/3c3153598b530fb85e623ba226c8cca1b3f4d262/BepInEx/Configuration/ConfigFile.cs),
[ConfigEntryBase](https://github.com/BepInEx/BepInEx/blob/3c3153598b530fb85e623ba226c8cca1b3f4d262/BepInEx/Configuration/ConfigEntryBase.cs),
and [TomlTypeConverter](https://github.com/BepInEx/BepInEx/blob/3c3153598b530fb85e623ba226c8cca1b3f4d262/BepInEx/Configuration/TomlTypeConverter.cs).
The configuration code was read, not exercised in a running plugin.

| Native behavior | Design implication |
| --- | --- |
| Saving writes section/key entries and generated setting descriptions in UTF-8. Sections are sorted by their names; the API has flat section names. | Ordered recipe sections and final reference sections can use deliberate names. True subgroup navigation depends on the selected manager and remains unverified. |
| Saving regenerates the file. Arbitrary comments read from disk are not retained. | A manually appended comment block is not a durable reference. Generated descriptions or inert entries need a save-round-trip check. |
| Descriptions support comment lines; string values escape newlines. | A large multiline string value is not equivalent to a readable multiline reference in Notepad. Compare presentation options using a complete file. |
| `SaveOnConfigSet` defaults to true; binding and changed values can trigger writes. | Native batching is available; investigate correction/write ordering before choosing a loading sequence. |
| Unknown keys are retained as orphaned entries. | Reference placement, obsolete settings and repeated saves need explicit examples; do not promise all user-added sections will sort before the reference. |
| Numeric conversion uses parsing before acceptable-value checks. Parse failures log a warning and leave the previous/default value. | Out-of-range Int32 text and nonnumeric text do not receive nearest-value correction automatically. Missing keys and malformed values need separate handling. |
| The loader ignores lines without `=` and constructs section/key definitions before value conversion. Invalid definition names can throw. | Invalid field values, damaged file structure and filesystem failures are separate cases; a value clamp alone does not cover them. |

[AcceptableValueRange](https://github.com/BepInEx/BepInEx/blob/3c3153598b530fb85e623ba226c8cca1b3f4d262/BepInEx/Configuration/AcceptableValueRange.cs)
clamps to an interval endpoint. In contrast,
[AcceptableValueList](https://github.com/BepInEx/BepInEx/blob/3c3153598b530fb85e623ba226c8cca1b3f4d262/BepInEx/Configuration/AcceptableValueList.cs)
returns the first listed value for an invalid entry; it does not find the nearest
numeric ID. Neither mechanism defines distance between different whole recipes.

The initial pass did not establish a complete valid-ID list, universal quantity
cap, correction algorithm, mod-manager compatibility claim or safe save-transition
procedure. Requirements and proposals now live in the
[configuration contract](../planning/CONFIGURATION.md); PROJECT owns their dispositions.

## Follow-up inspection for the revised roadmap

The assembly hash was reconfirmed against the identity above. No failed activation
was retried and no baseline was refreshed. ILSpy output for `RecipeProto`,
`AssemblerComponent`, `PrefabDesc`, `GameHistoryData` and `VFPreload` is byte-identical
to the retained 29104 decompilations made with the same tool. That is a bounded
source comparison, not verification of all external references or game behavior.
Additional selected types were `ItemProto`, `MechaForge`, `FactorySystem`,
`FractionatorComponent`, `UIRecipePicker`, `UIReplicatorWindow`, `UIAssemblerWindow`,
`PowerGeneratorComponent` and `PowerExchangerComponent`.

UnityPy 1.25.3 read `AssemblerDesc`, `LabDesc`, `FractionatorDesc` and `ModelProtoSet`
from the current assets. The probe joined item `ModelIndex` to model ID and
`PrefabPath`, then matched the corresponding prefab/component. Besides the
resource hash above, the inputs were `globalgamemanagers.assets` SHA-256
`6b33628213f713bcac82a5d94f7739e42362b71ac3df881a0eefc0c26586564e`
and `sharedassets0.assets` SHA-256
`841f08598c8ab4253441e29de0d7b739d5852c4f693eec59bd86049f2feb004e`.
The ignored probe is `.local/configurability/inspect-facilities.py`; its result is
`facilities.json` in the same hash-keyed evidence directory as the other extracts.

### Machine selection and startup

| Item IDs, in tier order | Native recipe family | Base speeds, in the same order |
| --- | --- | --- |
| 2302, 2315, 2319 | Smelt | 1, 2, 3 |
| 2303, 2304, 2305, 2318 | Assemble | 0.75, 1, 1.5, 3 |
| 2309, 2317 | Chemical | 1, 2 |
| 2308 | Refine | 1 |
| 2310 | Particle | 1 |
| 2901, 2902 | Research, lab manufacturing mode | 1, 3; arbitrary product/count support is not established |

`PrefabDesc` converts assembler and lab production speeds to native integer
speed units. `UIAssemblerWindow.OnSelectRecipeClick` passes the building's recipe family
to `UIRecipePicker.Popup`; the picker filters by that family and unlock state,
not an individual machine ID. Thus selecting one tier can provide a rate baseline
while other compatible tiers continue to accept the same recipe natively.

`VFPreload.PreloadThread` calls `InvokeOnLoad` before model preloads. The current
plugin hook therefore cannot simply start reading initialized machine prefab
fields. All models finish before the first `ItemProto.Preload`; that item method
then builds recipe links. A guarded Harmony prefix before the first item preload,
reading the already-loaded model descriptors, is a source-supported candidate
for registration. `ItemProto.prefabDesc` itself is assigned inside item preload;
the candidate must not assume every item already has it. `InvokeOnLoadHalf` occurs
partway through the model loop and is insufficient. This identifies a narrow
integration route; selecting and verifying the actual hook belongs with the code
change and its external-reference ledger.

Fractionator item 2314 has no ordinary crafting speed. Its `SetRecipe` selects
the first fractionation recipe for an incoming item and uses the output/input
count ratio as probability. It does not use recipe duration to deliver a fixed
per-minute output. Ray-receiver production and energy-exchanger processing use
dedicated energy/item paths rather than the ordinary timed recipe executor.
Those are different production contracts, not interchangeable machine IDs for
the requested fixed-rate recipes.

### Shape, research and saved state

- `AssemblerComponent.SetRecipe` and lab assembly each allocate six input demand
  positions. The Replicator builds ingredient rows dynamically, but its main
  output display has two positions. The owner's subsequent choice of one output
  removes the need for a multi-output configuration design.
- Both `MechaForge.TryAddTaskIterate` and `AddTaskIterate` stop after 20 nested
  calls. `ItemProto.FindRecipes`/`ComputeRawMats` assign their collections before
  descending into ingredient recipes. The inspected code does not justify the
  initial suspicion of an unbounded handcraft cycle. Do not introduce a blanket
  acyclic-progression requirement; these observations do not prove every cyclic
  recipe is useful or every count calculation safe.
- `GameHistoryData.SetForNewGame` creates state for every technology record.
  `AddTechHash` grants `UnlockRecipes` when that record reaches its maximum level,
  not on each intermediate level. A technology ID alone therefore means completed
  technology under native semantics. Very high-level gates can be impractical
  without being invalid. The catalogue contains 304 ordinary published records,
  six obsolete records and five hidden records; those labels are not themselves
  evidence of an invalid ID.
- `MechaForge.Import`, `GameTick` and cancellation use the saved task's arrays
  and timing. Cancellation returns served items through the native inventory path.
  This supports preparing/cancelling work before an edit instead of introducing
  a queue migration. UI lookups can still use current recipe metadata, so old
  queued work should not be presented as freshly configured work.
- `FactorySystem.Import` invokes the native component imports. Assembler import
  retains its saved recipe family and attaches current execution data. Merely
  emptying buffers does not change that saved family. Preparation for a family
  switch must also clear the affected machine's selected recipe before saving;
  after restart the player chooses an appropriate facility and selects the recipe
  again. This procedure still needs the final owner observation.

### File format and manager boundary

The pinned upstream
[BaseUnityPlugin constructor](https://github.com/BepInEx/BepInEx/blob/3c3153598b530fb85e623ba226c8cca1b3f4d262/BepInEx/Contract/BaseUnityPlugin.cs)
constructs the GUID-named `ConfigFile` before the plugin's `Awake`. The config
constructor immediately reads an existing file. A malformed section/key can
therefore fail before mod-owned value correction runs. A promise to repair every
damaged INI file would require a different loading intervention; no such mechanism
is selected or authorized by these findings.

Ordinary named sections, scalar settings, generated descriptions and reference-only
string entries fit the inspected BepInEx file model. A numeric ID key and a readable
name value avoid a giant escaped multiline string. No custom subgroup extension
is required to group the reference into items, technologies and machines.
The owner selected Thunderstore Mod Manager. Its exact installed editor version
and actual rendering were not established by the bounded check; official
[Thunderstore documentation](https://wiki.thunderstore.io/) does not establish
how this proposed reference renders. r2modman or an in-game BepInEx configuration
plugin is not a substitute for validating the selected app. Keep its actual edit,
search and save round trip in the final owner procedure, without an agent changing
the owner's manager profile or launching the game.

### Investigations stopped at the brief-check boundary

Only these questions need separate follow-up investigations in the revised plan:

| Story | What the brief check established | Remaining bounded question |
| --- | --- | --- |
| CFG-I01 | Duration scaling can overflow; assembler demand multiplies input counts by a speed/time-dependent buffer factor; smelters stop when a batch would exceed 100 output items; handcraft and raw-material calculations multiply counts; production finishes at most one normal cycle per update. | Establish the supported input shapes and exact count/rate/ID bounds across those consumers, including ordinary higher-tier speeds and proliferation. Define nearest attainable integer-rate behavior without quietly changing a valid batch count. This grew beyond a simple range check, so no universal cap or formula was adopted. |
| CFG-I02 | Labs use count-independent input demand thresholds, a speed-dependent output buffer, and return a production-state value derived from `productId - firstMatrixId + 1`. | Trace that state's consumers and lab feeding/output paths to determine which non-default item/count combinations are natively valid. Do not assume every existing item works or exclude every lab recipe without that evidence. |

The other questions now have source findings, owner decisions, concrete proposals
for review, or a named final runtime observation. They do not justify retaining
the original three broad investigation stories. Native extracts and the probe
remain local; no installed file, save or product code was changed.
