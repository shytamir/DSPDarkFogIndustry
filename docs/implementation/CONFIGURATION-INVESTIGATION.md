# Configuration investigation

Static inspection, 2026-10-13. [PROJECT](../PROJECT.md) records authorization and
dispositions; the [roadmap](../planning/ROADMAP.md) defines the work and questions.
This is an initial evidence record, not a complete configuration design or a
current-game compatibility claim.

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
machine-prefab capability/speed map or specialized consumer checks. Those must
precede the valid-machine list and rate semantics, including higher-tier machines.

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

The remaining policy questions are in the roadmap, rather than embedded here as
implementation choices. In particular, no complete valid-ID list, universal
quantity cap, correction algorithm, mod-manager compatibility claim or safe
save-transition procedure has been established by this initial inspection.
