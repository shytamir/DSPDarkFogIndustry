# MVP delivery roadmap

[PROJECT.md](../PROJECT.md) owns approval, activation, progress, and acceptance.
This roadmap defines bounded work; authorization is recorded in PROJECT.
Read the [recipe contract](MVP-RECIPES.md), original
[concept](../concept/CONCEPT.md), and [technical basis](../implementation/MVP-TECHNICAL-BASIS.md).

## Delivery outcome

Deliver an identified BepInEx MVP and a concise owner-validation handoff that
implements all six synthesis recipes through native machines and research. The
route supplies the native Dark Fog industrial progression without enemy drops,
while retaining ordinary research requirements and coexistence with combat games.

The exit is **ready for final owner validation**. The owner's actual game tests,
acceptance, public release, and publication are subsequent actions, not inferred
from compilation or this roadmap's completion.

The recipe contract fixes the six inputs/outputs, component gates, selector slots,
Mk.I baseline rates, handcrafting, and native proliferation. Implementation may
choose the narrowest proven registration/build mechanism; changing those product
decisions requires owner steering. Preserve native resource generation, mining,
combat/drops, existing recipes, technology costs, and downstream building research.

## Work sequence

| Epic | Outcome | Stories | Exit gate |
| --- | --- | --- | --- |
| MVP-A: Native integration foundation | One evidenced extension route and a real plugin build | MVP-02 | M1: Integration choices and compilation are evidenced |
| MVP-B: Complete synthesis route | Six correctly placed/gated recipes with native behavior and save continuity | MVP-03, MVP-04, MVP-05 | M2: Feature contracts pass offline checks; runtime observations are explicit |
| MVP-C: Owner handoff | The actual CI artifact and a bounded validation procedure | MVP-06, MVP-07 | M3: Owner-validation-ready MVP |

Sequence: MVP-02 → MVP-03; MVP-04 and MVP-05 then complete M2;
MVP-06 → MVP-07 completes M3. Each story includes its own checks. These are bounded
work definitions, not time estimates or automatic activation of every story.

Historical work definitions: [archive](../archive/2026-10-08-mvp/ROADMAP.md).

### MVP-02 — Establish the actual plugin build

**Depends on:** MVP-01.

**Outcome:** Compile the product against identified real game/dependency references.

**Scope:** Introduce the minimal plugin project, its identity and lifecycle, pinned
dependencies, local command, focused test seam, and the chosen hosted-build path.

**Definition of done:** Local compilation and focused binding/metadata checks pass;
missing references fail clearly; the product has valid loader metadata; evidence
identifies source and references. The shared build command is wired to compile the
actual product in the selected CI environment. The eventual downloaded artifact is
verified in MVP-06; fixture CI success cannot satisfy this story. No proprietary
game references or scaffold payload enter the product package; runtime dependencies
use the selected dependency distribution contract.

**Evidence / stop:** Preserve compile logs and reference/binding checks. Tests run
mod-owned logic without loading the game into an unauthorized test host. Stop if
the actual dependency API differs from the planned surface; repair the narrow
binding issue or return the changed integration decision to MVP-01.

**Exclusions:** New panels, configuration systems, runtime harnesses, installation,
and speculative compatibility layers.

### MVP-03 — Register and expose the six recipes

**Depends on:** MVP-02.

**Outcome:** Expose the exact recipe contract through native crafting and machine UIs.

**Scope:** Register six records once, link native items and technology unlocks,
initialize the affected caches, retain icons, and use the agreed grid positions.

**Definition of done:** Offline checks compare all six production records with the
owner's contract, detect duplicates/collisions, and exercise registration ordering
and repeat calls. Inspect the registration path for catalogue lookup, item links,
execution data, icon indices, and technology recipe links. Only researched gates
grant the recipes; machine filtering and handcraft flags match the contract.

**Evidence / stop:** Check the mod's emitted records and mutations rather than a
second manually maintained recipe table. Retain a native source trace for derived
links and explicit owner observations for actual UI/cache behavior. Stop on ID/grid
conflicts or stale-link risk; do not silently renumber, relocate, or broaden patches.

**Exclusions:** Native item reordering, new items/technologies, custom recipe picker,
automatic downstream research awards, and extra ingredient requirements.

### MVP-04 — Preserve native production and progression

**Depends on:** MVP-03.

**Outcome:** Express the requested rates and complete industrial route with native execution.

**Scope:** Verify the registered durations/output counts, native proliferation
eligibility and facility bonuses, and the recipe/input and technology dependencies.
Resolve integration defects within the fixed contract while preserving native
discovery, research consumption, drops, and downstream recipes.

**Definition of done:** Focused checks cover all six baseline recipes, the two ship
input acceleration-only cases, ordinary extra-product eligibility, and the recipe
dependency route. Static boundaries show no replacement of native production,
research, statistics, mining, or combat. The final owner procedure covers behavior
that these checks cannot establish.

**Evidence / stop:** Derive rates from the verified machine speeds and actual recipe
records. Include higher-tier native scaling and the different Replicator timing;
trace all five downstream technologies' discovery and Matrix consumption. Stop if
native behavior conflicts with the fixed contract. Do not force extra-product
eligibility on ship inputs or add a physical-production gate to discovery.

**Exclusions:** Rate normalization for higher tiers, changes to item productivity,
custom research/discovery logic, rebalance, and new execution loops.

### MVP-05 — Reconcile existing saves and define removal

**Depends on:** MVP-03; integrate with MVP-04 before M2.

**Outcome:** Existing researched gates acquire only their rightful recipes and
configured machines retain stable references across reloads.

**Scope:** Reconcile the six recipes at the selected native lifecycle point and
document the actual persistence/removal implications of the registration mechanism.

**Definition of done:** Tests cover no/partial/all gates, already unlocked recipes,
repeated reconciliation, save/session changes, and no unrelated awards. Unlocking
never replays an entire technology. Use the chosen helper's correct behavior where
it already provides reconciliation; add code only for an evidenced gap. Stable IDs
and configured-machine persistence have explicit runtime observations to perform.
A concrete removal procedure and limits are ready to validate on a disposable copy.

**Evidence / stop:** Check the actual reconciliation owner and native import path,
not a full simulated save engine. Record how unknown recipe references are handled
and how the owner restores the test checkpoint. If safe removal is not supported,
state the actual restriction and return any product-policy decision to the owner;
never clear machine configurations or promise a lossless uninstall without evidence.

**Exclusions:** Generic migrations, save editors, automatic backups, conversion of
resources, uninstall services, or guaranteed support for arbitrary mod combinations.

### MVP-06 — Build and inspect the owner candidate

**Depends on:** M2.

**Outcome:** Produce one trustworthy MVP artifact through the real delivery pipeline.

**Scope:** Replace scaffold packaging with the actual plugin/dependency declaration,
appropriate README and simple icon, preserve identity/evidence conventions, and
verify the selected source on main and its downloaded CI outputs.

**Definition of done:** Local real-reference validation passes; the actual hosted
run succeeds; downloaded package and evidence match source, references, version,
loader identity, and hashes. Malformed-package checks reflect the product contract.
No scaffold DLL, proprietary reference, cache, or maintainer report enters the ZIP.
Statically check the downloaded plugin's referenced members against the identified
real game/dependency assemblies, especially if CI uses a reduced compile surface.

**Evidence / stop:** Record the exact main revision, run/attempt URL, dependency
versions, downloaded package hash, and independent inspection result. Include the
reference strategy's limits. Main delivery requires the applicable owner authority;
a local build, configured workflow, or green run without artifact inspection cannot
close this gate. Fix packaging failures and rerun only affected checks. Do not hand
off an older successful artifact as the result of a failed candidate build.

**Exclusions:** Store publication, release tags, marketing campaign, installer,
auto-updater, or speculative build infrastructure.

### MVP-07 — Prepare the final owner validation handoff

**Depends on:** MVP-06.

**Outcome:** Give the owner the identified candidate and a short, reproducible
procedure for judging the complete MVP promise.

**Scope:** Prepare installation/cleanup instructions, exact setup and expectations,
new-game and existing-save observations, save/reload/removal precautions, and a
compact result format. Consolidate related observations around one coherent build.
Use the [owner observation coverage](../implementation/MVP-TECHNICAL-BASIS.md#final-owner-observation-coverage)
to cover the complete promise without a full playthrough for each test.

**Definition of done:** Every runtime acceptance claim maps to a concrete owner
observation; offline checks are supplied as evidence rather than owner homework.
The handoff identifies the exact package/source/dependencies/game baseline and
states untested claims. PROJECT records readiness without marking owner acceptance.

**Evidence / stop:** Walk through the instructions against the actual ZIP and
declared dependency set. Supply preparation steps for reusable disposable checkpoints;
do not assume access to the owner's existing saves. Leave runtime result fields
unfilled. If the artifact has a known contract failure, repair it in its owning
story before handing it off; an untested runtime claim is explicitly awaiting
observation, not a pass or an excuse to conceal a known defect.

**Exclusions:** Agent game execution, player save access, assuming acceptance from
silence, publication, and unrelated feature improvements.

## Gates and delivery boundary

- **M1:** MVP-01 and MVP-02 meet their definitions; no unresolved integration blocker
  is silently carried into registration work.
- **M2:** MVP-03 through MVP-05 meet their offline definitions; all six recipes and
  save/research behavior form one coherent candidate. Required in-game observations
  are retained for owner validation, not marked passed.
- **M3:** MVP-06 and MVP-07 meet their definitions; main source, hosted run, downloaded
  artifact, and owner procedure agree. Stop at owner-validation readiness.

For each story, record the implementation revision, checks, limitations, and
disposition in PROJECT with links to supporting technical evidence. Readiness means
all feasible offline checks pass, no known contract defect remains, and the remaining
in-game observations are clearly assigned to the owner. Compilation and test doubles
do not certify native runtime behavior. No automatic acceptance or release follows.

Definitions and technical records are archived on completion/supersession following
[working methods](../WORKING-METHODS.md#closeout-and-archives). State remains in PROJECT.
