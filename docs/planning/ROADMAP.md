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
| MVP-C: Owner handoff | The actual CI artifact and a bounded validation procedure | MVP-06, MVP-07 | M3: Owner-validation-ready MVP |

Sequence: MVP-06 → MVP-07 completes M3. Each story includes its own checks. These are bounded
work definitions, not time estimates or automatic activation of every story.

Historical work definitions: [archive](../archive/2026-10-08-mvp/ROADMAP.md).

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
