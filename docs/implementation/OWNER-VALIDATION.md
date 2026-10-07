# Owner validation: DSP Dark Fog Industry

Use the candidate below for the first gameplay validation. [PROJECT](../PROJECT.md)
owns readiness and acceptance; all result fields here deliberately remain unfilled.

## Exact candidate

- Package: **DSPDarkFogIndustry-0.1.8.1fabb2543d56.zip**.
- [Successful build and artifacts](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37699427929), attempt 1; source `1fabb2543d56d90d5707863c234c18c33ba93f02` on main.
- ZIP SHA-256: `e4074f0c8b20c7af9bcec50bf7e85c58acba1e8e62c7eab19802aa234ab9c9a1`.
- DLL SHA-256: `a0aba9ed6a86b08aca47a668b27632a3c2baf8c45dcb60907476a171495331f3`.
- Local retained ZIP: `artifacts/ci/37699427929/`, beside its inspection evidence.
  GitHub artifacts expire after 14 days; keep the downloaded copy.
- Baseline: DSP **0.10.35.29104**, DSP BepInEx pack **5.4.17**, this plugin only.
  GUID: `dark-fog-industry`. No LDBTool or other content mods.

The [offline evidence](../archive/2026-10-08-mvp/VALIDATION.md#mvp-06--candidate-packaging) already covers
487 assertions, real and shim compilation, reference ledgers, all downloaded bytes,
source hashes and package contents. There is no need to repeat those build checks.
**No game startup, UI, production, save or combat observation has yet been run.**
Later documentation-only main builds do not replace this identified candidate.

## Prepare once

1. Quit the game. Use a separate minimal mod profile or isolated installation;
   retain untouched copies of any saves you choose to use. Do not overwrite originals.
2. Before adding this plugin, create a disposable Dark Fog-disabled checkpoint
   with Particle Control researched but Corvette unresearched. A new game can
   supply this checkpoint; an existing save is optional. Save an untouched copy,
   then prepare a second pre-mod checkpoint with all six gates researched. Both
   may use the controlled setup below. Record researched gates and a few unrelated
   research/unlock states. Keep these checkpoints separate from later test saves.
3. For a short controlled test, native sandbox tools may supply ordinary research,
   resources, machines and combat components. Label this setup **sandbox/granted**;
   do not grant any of the six Fog materials or complete the five Fog technologies.
   This tests manufacturing and persistence, not a normal no-grants playthrough.
   Normal progression evidence remains the separate static dependency witness and
   any normal-game observations you actually make.
4. With the game closed, import the local candidate ZIP into the test profile (or
   extract it into the game directory). Confirm one DLL at
   `BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.dll` and the declared
   BepInEx dependency. Start the game; its log should identify DSP Dark Fog Industry
   **0.1.8** without plugin/Harmony exceptions. On an error, stop and retain the log.

## Check unlocks, presentation and the complete chain

Load the pre-mod partial checkpoint. Energy Shard should be available; recipes
whose gates are absent should remain locked. Inspect the ordinary recipes and
recorded unrelated research: neither should change. Research Corvette through
normal research and confirm Matrix becomes available without reloading. Continue
or use the labelled controlled setup to obtain the remaining ordinary gates and
inputs. A fresh new game with the plugin must begin with the same native gates
locked; it must not inherit another save's unlocks.

Use the [six-recipe table](../../packaging/README.md#recipes). In the Replicator's
Items tab the bottom row's rightmost six positions should show the outputs in
chain order, using native icons/names/tooltips. Cube recipes on the left and the
intervening two positions stay unchanged. Facility pickers filter by native family.
The four assembler recipes allow handcrafting; Energy Shard/Core Element are
visible in their normal display but cannot be handcrafted.

Build the chain in order from Fractal Silicon and the five combat inputs, with no
enemy drops or granted Fog outputs. Retain a sample of each manufactured material.
Every operation consumes one of each listed input and yields one product, except
one Fractal Silicon yields two Shards. Isolate one fully powered machine per recipe,
keep inputs supplied/output clear, and use a settled two-minute observation window
or native production statistics. Allow only the small start/end cycle rounding.

| Product | Baseline machine | Unsprayed products/min | With all inputs sprayed Mk.III |
| --- | --- | ---: | ---: |
| Energy Shard | Arc Smelter | 120 | 150, extra-products mode |
| Dark Fog Matrix | Assembler Mk.I | 60 | 120, acceleration only |
| Matter Recombinator | Assembler Mk.I | 60 | 75, extra-products mode |
| Silicon-based Neuron | Assembler Mk.I | 60 | 120, acceleration only |
| Negentropy Singularity | Assembler Mk.I | 60 | 75, extra-products mode |
| Core Element | Chemical Plant | 60 | 75, extra-products mode |

Clear unsprayed input buffers before the sprayed run. Extra-products mode retains
baseline input consumption (60/min each here); acceleration consumes inputs twice
as fast. Forced acceleration on the four productive recipes also doubles their
baseline rate. At Mk.III spray the native operating power requirement is 2.5 times
unsprayed power; maintain full supply. Matrix/Neuron must not gain extra products.

Without spray, the same recipe gives **80/120/240 per minute** in Assembler
Mk.II/Mk.III/Re-composing Assembler; Shards give **240/360** in Plane/Negentropy
Smelters; Core gives **120** in Quantum Chemical Plants. Reuse the same test line
for facility swaps. Confirm production/consumption statistics count the native
items normally. These are native rate expectations, not measured results.

## Check native research, persistence and combat

As the synthesis recipes unlock, Fog technologies may be revealed even before a
physical item is made. This early discovery is allowed. Check all five links:

| Native technology | Discovery material | Unlock result |
| --- | --- | --- |
| Dark Fog Graviton Lens | Dark Fog Matrix | Dark Fog Graviton Lens |
| Digital Analog Computation | Silicon-based Neuron | Self-evolution Lab |
| Matter Recombination | Matter Recombinator | Re-composing Assembler |
| Negentropy Recursion | Negentropy Singularity | Negentropy Smelter |
| Strange Annihilation Fuel Rod | Core Element | Strange Annihilation Fuel Rod |

Each must retain its ordinary prerequisite and require Dark Fog Matrix research,
with no automatic technology completion. Feed **manufactured** Matrix into normal
research and confirm consumption/progress; complete the five technologies using
that supply and check their native recipes. Use unlocked advanced facilities for
the corresponding rate checks above. Controlled setup must not bypass these five
research checks. The retained native cost/prerequisite facts are in the linked
[validation evidence](../archive/2026-10-08-mvp/VALIDATION.md#mvp-04--native-production-and-progression).

Save the configured six-machine line into a new test slot. Quit/reload twice:
recipes, inputs/output buffers, inventories, research and production must persist
without duplicate grants or errors. Switch to the fresh unresearched game and back;
unlocks must remain save-specific. Also load a pre-mod disposable checkpoint with
all six gates researched to verify all six recipes appear; it can be prepared in
the same labelled controlled setup before installing the plugin.

In a separate disposable combat-enabled game, confirm synthesis still works and
observe ordinary native Dark Fog combat/drop behavior. A short representative
observation suffices; do not interpret it as a statistical drop-rate study or broad
compatibility certification.

## Rollback and report

Keep the mod for synthesis saves. **Loading a synthesis save without it is
unsupported**: native machines can retain unknown recipe IDs/buffers without
execution data. Do not use that as a safe-uninstall test or overwrite such a save.
To validate dependable rollback, quit, remove only this plugin folder, and load the
untouched pre-mod checkpoint. Its native recipes/research should remain as before.
To resume the synthesis test, quit, reinstall this exact candidate and load the
separate synthesis test slot. No save editing or machine-clearing service is used.

Record pass/fail/not-run and one concrete observation for each row. For a failure,
include the first reproduction step, screenshot/log excerpt, game/build version,
mod list and whether setup was normal or sandbox/granted. Keep a failing test save
separate if you choose to retain it; do not send or overwrite personal saves by default.

| Observation | Result | Notes |
| --- | --- | --- |
| Startup, fresh/partial/all researched gates, no unrelated awards | | |
| Six slots, native icons/tooltips, facility filters and handcrafting | | |
| Full six-step manufacturing chain and baseline statistics/rates | | |
| Proliferation modes/power and higher-tier rates | | |
| All five discoveries, prerequisites and manufactured-Matrix research | | |
| Save/reload, configured machines and session isolation | | |
| Combat-enabled synthesis and ordinary drops | | |
| Pre-mod rollback and synthesis resume | | |

Owner disposition: **not entered**. Acceptance and any publication decision are
separate from this procedure; report gaps honestly rather than treating not-run as pass.
