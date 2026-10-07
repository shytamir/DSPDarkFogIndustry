# Native integration contract

MVP-01 decision, 2026-10-08. State and approval live in [PROJECT](../PROJECT.md).
The [recipe contract](../planning/MVP-RECIPES.md) remains the product authority.

## Minimal extension route

Use a normal BepInEx 5 plugin and its bundled Harmony library. Two method patches
are sufficient: a prefix on `VFPreload.InvokeOnLoad` for registration, and a postfix
on `GameHistoryData.Import` for existing-save unlock reconciliation. No custom
loader, injection system, production loop, transpiler, or runtime framework.

Native `VFPreload.PreloadThread` invokes the first hook before model/item/recipe/
technology preload and icon creation. `LDB` loads prototype tables on demand. Add
the six recipes and append each ID to its one gate's UnlockRecipes array there;
call the existing recipe table's `OnAfterDeserialize` to rebuild its lookup. Native
preload then builds item recipe/handcraft/raw-material links, translated names,
icons, productivity, preTech, execution data, and technology recipe arrays normally.
Register once per table; repeated startup callbacks must not duplicate anything.

Use native item names and result-icon fallback. Preserve all existing records and
unlock entries. Validate all six IDs, positions, items, and gates before mutating
the catalogue. A collision produces an actionable load error, not silent overwriting,
renumbering, or relocation. The declared baseline is this plugin plus BepInEx; no
claim about a different content mod occupying the same IDs/grid.

The LDBTool 3.0.3 package was inspected (SHA-256
`71ec2d01ad484422c5c64385aea783ced81800e0f4f97bcf6628599c495c17e6`).
Its published source HEAD is still `6b84a5539d4f4bb06d0e840cc8203655e313da4e`.
Its registration runs at InvokeOnLoadWorkEnded, after native preload, and rebuilds
several caches. Its configurable IDs/grid and extra integration work are unnecessary
for this fixed six-recipe contract. It is **not a dependency**. This choice follows
the owner's request to use BepInEx patching and existing native mechanisms.
Sources: [LDBTool source](https://github.com/xiaoye97/DSP_LDBTool),
[published package](https://thunderstore.io/c/dyson-sphere-program/p/xiaoye97/LDBTool/).

## Stable identity and timing

Reserve recipe IDs 401–406 in chain order, matching grid 1809–1814. They are free
in the identified complete native catalogue, below RecipeProto.kMaxProtoId=500,
within IconSet's 12000-entry recipe array, and within the signed-short saved recipe
field. Keep IDs and input ordering stable across versions. No configurable remap.

Static asset inspection resolved AssemblerDesc scripts through the external
globalgamemanagers.assets pointer that the planning probe had omitted. Resources
still match SHA-256 `2839ea86d64c742539410c5e7be005b65ff871212e9d7957c5b015ecff2fd7d2`.
Prefab speedf values are Mk.I/II/III/IV = 0.75/1/1.5/3; smelters = 1/2/3;
chemical plants = 1/2. GameMain.tickPerSecI is 60. Native time advancement and
RecipeProto's execution-data conversion therefore give:

| Recipe IDs | TimeSpend | Baseline | Native higher facilities, no spray |
| --- | ---: | --- | --- |
| 401 | 60 | Arc Smelter: 120 shards/min, two per operation | Plane 240/min; Negentropy 360/min |
| 402–405 | 45 | Assembler Mk.I: 60/min, one per operation | Mk.II 80/min; Mk.III 120/min; Re-composing 240/min |
| 406 | 60 | Chemical Plant: 60/min, one per operation | Quantum Chemical Plant 120/min |

These are full-power steady-state rates with adequate inputs/output capacity;
startup, starvation, transport, and incomplete measurement windows affect samples.
No machine-speed correction is added. Replicator uses native timing for 45-tick
recipes; it is not normalized to the Mk.I machine rate.

The local constructive dependency check found a route for all five combat inputs,
Fractal Silicon, the three baseline facilities, and gate research ingredients,
excluding technologies 1509 and 1901–1904 and all their Fog-consuming recipes.
The witness uses 60 native recipes and a 67-technology closure, with ordinary native
resources/gases and ray-receiver critical photons as source terms. Photons still
need native solar collection/research; they are not free inputs or grants. The six
new recipes then form a simple forward chain. This proves no Fog bootstrap cycle
in that witness, not travel, research timing, power availability, or a playthrough.
Private reproduction: `.local/planning/check-route.py` and its JSON output.

## References and hosted compilation

Target net472/C# 7.3, using pinned .NET Framework reference package 1.0.3. Fetch the
public DSP BepInEx 5.4.17 pack by exact URL/hash into ignored local tooling for
local compilation and metadata validation. Declare the Thunderstore
dependency `xiaoye97-BepInEx-5.4.17`; do not redistribute it in the plugin ZIP.
Pack SHA-256: `ea24d1c33fa63be98768d65057a79002a77a5fb20dab3c7e072cbd3044a1b9fe`.
[BepInEx source](https://github.com/BepInEx/BepInEx/tree/v5.4.17) supplies API/license
context; the downloaded assembly is the binding reference.

Local verification compiles against the identified real game, Unity, BepInEx, and
Harmony assemblies. Per the owner's explicit follow-up, CI compiles the same
product sources against hand-authored, compile-only shims for **all four external
library families**, including BepInEx and Harmony. Only standard target-framework
reference packs and build-tool dependencies use ordinary locked restore. No real
game/mod dependency DLL is required by CI or committed to the repository.

Maintain a type/member reference ledger alongside the shim declarations: declaring
assembly, type/base type, member kind, visibility/staticness, return/field type,
parameter types, and inspected source identity. Account for attributes, constructors,
generic bases, and type forwarding as well as calls. Validate the ledger and shim
surface against real metadata locally, and inventory the product's actual external
references so a new reference cannot silently escape coverage. The declarations
have no native implementation, are never packaged, and are not a runtime test
double. Record which reference mode produced each artifact.

MVP-02 validates local metadata/bindings. MVP-06 must inspect the downloaded CI DLL's
actual external member references and Harmony target signatures against the real
identified assemblies, using metadata only. Any discrepancy fails the candidate.
Only the product DLL enters the package. This avoids shipping proprietary binaries
while keeping CI useful and its limitations explicit.

## Save and removal boundary

The Import postfix consults native TechState/RecipeUnlocked and calls UnlockRecipe
only for a missing one of these six whose gate is already researched. Ordinary
new research uses the appended native gate list. No whole-tech replay, save-format
extension, global unlock scan, per-frame reconciliation, or duplicate helper patch.

Native AssemblerComponent.Import keeps saved recipe IDs and buffers, but returns
without execution data when an ID is missing. That is not a verified safe uninstall
path. Keep the mod installed for saves using synthesis. The dependable rollback
procedure is to remove this plugin and restore an untouched pre-mod checkpoint;
do not overwrite the working save during removal experiments. MVP-05 supplies the
focused reconciliation tests and disposable-copy observation steps. No automatic
clearing of machine recipes or editing of saves is included.

## Evidence limits

Assembly baseline and source provenance are in the [planning technical basis](MVP-TECHNICAL-BASIS.md).
All findings above are static metadata/source/asset/catalogue inspection. The
registration and save patches are selected, not yet runtime-observed. Game startup,
native UI, production, research, persistence, and removal require the final owner
observations. No game execution, save access, installation, or publication occurred.
