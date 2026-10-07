# MVP technical basis

Planning evidence for [MVP-01 through MVP-07](ROADMAP.md), reviewed on
2026-10-08. This is a basis for implementation, not an implemented architecture or
runtime certification. [PROJECT](../../PROJECT.md) owns decisions and work state;
the [recipe contract](../../planning/MVP-RECIPES.md) owns the owner's required behavior.
The later [integration contract](../../implementation/NATIVE-INTEGRATION.md) resolves the initial
registration, timing, ID, reference, and removal questions below. Retain this dated
planning evidence without treating its former unknowns as new blockers.

## Evidence identity and limits

The installed `Assembly-CSharp.dll` still matches the supplied DSP `0.10.35.29104`
baseline: SHA-256
`6c122e5443e6843979b4064050dfcb5e0d75577a0b64f6ae4111290238b33c12`.
Read-only environment activation passed with SDK `10.0.302`, PowerShell `7.6.5`,
and ILSpyCmd `11.1.0.9782`. This establishes inspection readiness only.

The full local ItemProtoSet, RecipeProtoSet, and TechProtoSet JSONs were rehashed
against the [supplied provenance](../../concept/evidence/provenance.json); all three
matched. Catalogue statements below come from those arrays. Selected original
records and source locators are indexed in [concept evidence](../../concept/EVIDENCE.md).
Native decompilations and full asset exports stay ignored/local.

Additional static inspection used `RecipeProto`, `PrefabDesc`, `AssemblerComponent`,
and `ERecipeType` from the identified assembly. Reproduce targeted reads through
[Inspect-NativeType.ps1](../../../scripts/Inspect-NativeType.ps1). References below name
types/methods rather than depending on machine-specific paths.

No game assembly was executed, game launched, save accessed, or plugin installed.
The supplied dependency example is also static evidence. Compilation, registration
at runtime, peaceful progression, save continuity, and removal remain untested.

## Native mapping

These are existing item/technology IDs; **new recipe IDs are not selected here**.
The component's existing recipe confirms its unlocking technology in the full
catalogue. Particle Control is the owner's explicit exception.

| New recipe's result | Result ID | Input IDs, one each | Gate ID | Existing component recipe | Recipe grid |
| --- | ---: | --- | ---: | ---: | ---: |
| Energy Shard, two outputs | 5206 | 1013 | 1133 | Not applicable | 1809 |
| Dark Fog Matrix | 5201 | 5206, 5111 | 1822 | 150 | 1810 |
| Matter Recombinator | 5203 | 5201, 1606 | 1816 | 141 | 1811 |
| Silicon-based Neuron | 5202 | 5203, 5112 | 1823 | 151 | 1812 |
| Negentropy Singularity | 5204 | 5202, 1611 | 1817 | 146 | 1813 |
| Core Element | 5205 | 5204, 1608 | 1818 | 143 | 1814 |

Use native recipe families Smelt, Assemble, and Chemical for the corresponding
contract rows. Existing native downstream research remains technologies 1509 and
1901–1904, with their existing discovery items, Matrix costs, prerequisites, and
recipes 162, 153, 154, 155, and 156. Do not substitute the concept's earlier example
combat gates for the owner's choices.

## Mechanisms established by static inspection

**Placement.** `UIRecipePicker.RefreshIcons` and
`UIReplicatorWindow.RefreshRecipeIcons` decode GridIndex as tab, one-based row, and
one-based column in an 8-by-14 grid. Items tab 1, row 8, columns 9–14 yields
1809–1814. The full native recipe catalogue has cube recipes at 1801–1806 and no
recipes at 1807–1814. ItemProto.GridIndex is a different sorting field. The recipe
picker filters by unlock state and facility type; Replicator display is separate
from whether handcrafting is enabled. Setting Handcraft=false must not hide the
smelting/chemical recipes from their normal recipe display.

**Timing.** `RecipeProto.InitRecipeItems` derives execution time from TimeSpend;
`AssemblerComponent.InternalUpdate` advances it using native machine speed and
power. `PrefabDesc` derives assemblerSpeed from the prefab's `AssemblerDesc.speedf`.
The owner requires 60/min in Mk.I, so setting all recipes to an assumed one-second
duration is not justified. Confirm the actual prefab speeds and tick conversion
in MVP-01. If Mk.I is 0.75x and Arc Smelter/Chemical Plant are 1x, the required
durations are respectively 45 and 60 ticks at 60 ticks/second; these numbers remain
conditional until those inputs are verified. A bounded resources.assets probe
matched its supplied SHA-256 but found no local AssemblerDesc records; it did not
establish prefab speeds. Do not treat that unsuccessful lookup as timing evidence.

**Proliferation.** `RecipeProto.Preload` derives productive from NonProductive and
every input item's Productive flag. Corvette 5111 and Destroyer 5112 have that flag
off; the other listed inputs have it on. `AssemblerComponent.InternalUpdate`
chooses extra products only for productive recipes outside forced acceleration
mode; otherwise it uses native acceleration. Thus Matrix and Neuron are
acceleration-only; the other four can support extra products. No native item flag,
power formula, or production loop needs changing to satisfy the owner contract.

**Registration.** `VFPreload.PreloadThread` initializes item recipe links,
recipe metadata, technologies, and execution caches in a specific order.
`ItemProto.FindRecipes` returns early when its recipe collection is already set.
Appending RecipeProto objects after those steps is not enough. Identify a supported
insertion point or narrowly refresh affected derived data. Picker icon indices use
recipe IDs, and RecipeProto declares kMaxProtoId=500: catalogue availability alone
does not prove that a chosen ID fits every relevant table or extension mechanism.

**Research and saves.** `GameHistoryData` can discover a Fog technology through an
unlocked linked recipe before physical production. It still requires ordinary
research prerequisites and costs. Import restores saved recipe unlocks; it does
not generally award newly added recipes for every previously researched gate.
Reconcile only the six new recipes, once their gates are met, without replaying
whole technology awards. Existing native materials persist as native items, but
machines and unlock lists can retain the new recipe IDs: that distinction matters
when removing the mod.

## Investigations required before dependent work

Record findings here or in a short linked implementation record when performed;
keep their disposition in PROJECT. Each question has an owning story and a concrete
exit. A negative finding blocks the dependent behavior, not unrelated authorized work.

| Owner | Question and required evidence | Exit / stop condition |
| --- | --- | --- |
| MVP-01 | Which minimal registration route initializes catalogue lookup, item recipe/handcraft/raw-material links, tech links, execution data, and icons in the right order? Inspect the current candidate source and actual dependency surface. The supplied LDBTool commit `6b84a5539d4f4bb06d0e840cc8203655e313da4e` is a candidate example only. | Name and pin one supported route and its lifecycle; stop before dependent code if it cannot satisfy the contract. No automatic broad framework fallback. |
| MVP-01 | Which six stable IDs fit native and helper capacities, configured ID behavior, and the declared minimal mod set? Are the six grid slots free in that set? | Record IDs and a bounded collision response before registration. Never silently renumber saved recipes or move the owner's grid. A conflict requiring either goes back to the owner. |
| MVP-01 | What exact reference strategy lets CI compile the actual plugin without redistributing proprietary binaries? Check licenses, target framework, dependency versions, and the native members used. | Local real-reference build plus a reproducible hosted compilation route. Any reduced compile surface must be explicitly identified and checked against real references; never package it or present it as runtime proof. If unavailable, report the specific provisioning decision before promising CI readiness. |
| MVP-01 | What are the baseline machine speeds and time units? Does the selected gate/ingredient/machine closure avoid dependence on enemy drops and downstream Fog buildings, allowing earlier synthesized materials in the chain? | Verified timing inputs and one constructive, acyclic native production/research route for all six products. An actual conflict with fixed recipes requires owner resolution, not rebalancing. |
| MVP-01; detail in MVP-05 | Where does the chosen helper reconcile saves already, and when are native tech states available? How are unknown recipe IDs handled in machine import with the mod absent? | Select one reconciliation owner and identify removal consequences early. MVP-05 tests that path; avoid duplicate patches or promises of safe removal without evidence. |
| MVP-03 | Do actual records produce all required links/caches once, retain names/icons, and respect ID/grid collisions in the declared mod set? | Check mod-owned registration behavior against the inspected native contract. Unity UI/cache behavior remains a named owner observation; a mock passing does not establish it. |
| MVP-04 | Do the fixed durations and native productivity flags yield the requested rates, facility bonuses, discovery, and research route? | Focused arithmetic/data checks plus native source trace. No implementation-specific snapshot that merely repeats constants; final machine/UI/research observations remain explicit. |
| MVP-05 | Are no/partial/all researched gates handled idempotently, across new and loaded sessions, without unrelated awards? What does removal require? | Mod-owned reconciliation checks and an evidence-backed disposable-save procedure. No silent deletion of saved machine recipes to make removal appear safe. |

## Final owner observation coverage

MVP-07 turns this coverage into **one** short runnable procedure for the exact
MVP-06 artifact. It must supply precise setup, expected values, and a simple result
record. This table is not an instruction to access the game now.

| Group | Required observations | Prepared by |
| --- | --- | --- |
| Peaceful route | A Dark Fog-disabled game; gates absent/present as applicable; all six materials manufactured from the stated inputs; native discovery and all five downstream research requirements retained; manufactured Matrix can be consumed by research. | MVP-03, MVP-04 |
| Native presentation and production | Six agreed slots/order, icons/tooltips, correct facility filters, four handcraftable and two machine-only recipes; all six baseline rates/counts; native statistics, both proliferation behaviors, and higher-tier facility scaling. | MVP-03, MVP-04 |
| Save continuity | A pre-mod save with already researched gates, a partial-gate checkpoint, and newly researched gates; no unrelated unlocks; repeated load and session changes; configured synthesis machines and native research state survive save/reload. | MVP-05 |
| Combat coexistence | The same synthesis route in a combat-enabled context, with ordinary native drops still available; use a bounded observation supported by a review of unchanged combat/drop code, not a statistical drop-rate study. | MVP-04 |
| Removal | Disposable copy only, exact helper/dependency conditions, expected handling of saved recipe references and native materials, restore/re-enable steps; record adverse behavior rather than claiming seamless uninstall. | MVP-05 |

Group checks around a few reusable disposable checkpoints, not repeated full
playthroughs. Controlled test setup must be labelled: granted items/research can
exercise UI and persistence, but cannot prove the no-grants progression claim.
The offline dependency proof and actual manufacturing observations are distinct
evidence. No unreported owner's save state is a prerequisite for a runnable handoff.

Do not require a large compatibility, performance, multiplayer, localization, or
balance campaign. Use native names/icons; inspect the tested baseline and declared
dependencies. An observation that would change the fixed contract or require a new
feature returns to owner steering before that work is added.
