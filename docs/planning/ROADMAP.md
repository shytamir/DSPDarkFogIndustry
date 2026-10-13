# Configurable recipes roadmap

[PROJECT](../PROJECT.md) owns authorization, story/gate state, decisions and
acceptance. The [configuration contract](CONFIGURATION.md) defines the proposed
player behavior; [native and BepInEx findings](../implementation/CONFIGURATION-INVESTIGATION.md)
identify evidence and remaining limits. This plan ends with one verified candidate
ready for owner acceptance. The owner's explicit acceptance closes the roadmap;
automated checks and delivery alone do not.

## Scope and execution boundary

Configure the six synthesis recipes through BepInEx: technology, ingredient IDs
and integer quantities, one output item ID/count, machine ID and integer output
rate. Keep the six recipe identities/positions, the existing recipes as defaults,
native proliferation and ordinary facility bonuses. Provide a readable configuration
and a grouped ID reference usable in Notepad and Thunderstore Mod Manager.

The owner has selected the chosen machine as the unproliferated rate baseline,
retained assembler-only hand replication, and retained native save loading with
preparation instructions instead of resource migration. Remaining proposals and
correction semantics are explicit in the contract; approval must not be inferred
from these definitions. No product implementation is authorized by drafting them.

Agents may inspect native/dependency files read-only, edit repository-owned files
when implementation is authorized, compile against references, test mod-owned logic,
and build/inspect packages. They must not launch DSP or a substitute game harness,
execute game assemblies, deploy the mod, alter installed files/manager profiles,
or read or modify player saves. The owner performs the final runtime observations.
No new recipe slots, vanilla recipe editor, custom production loop, resource
migration, new UI, publication, tagging or version promotion is included.

## Work structure and weight

An **epic** delivers a coherent capability. A **story** has its own bounded result,
dependencies and completion evidence. Gates and milestones carry no extra weight;
an epic's weight is the sum of its stories, not additional work.

Weights are relative effort and uncertainty, not hours or completion percentages:
**1** small isolated change; **2** bounded change with few interactions;
**3** several related paths and focused checks; **5** substantial coupled behavior
or remaining uncertainty. Nothing larger than 5 is assigned to one story. If a
story grows beyond its stated boundary, split and reweight the remaining work in
this plan, recording the scope decision in PROJECT.

| Epic | Outcome | Stories and weights | Total |
| --- | --- | --- | ---: |
| E1 — Editable, valid recipes | Readable settings, specific corrections and a trustworthy ID reference | I01: 5; C01: 3; C02: 5; C03: 2 | 15 |
| E2 — Native production and progression | Settings apply consistently to machines, research and saves | I02: 3; N01: 3; N02: 3; N03: 3 | 12 |
| E3 — Candidate and owner handoff | An inspected CI package and a concise acceptance procedure | D01: 3; D02: 2 | 5 |
| **Total** | Ten stories; IDs below use the `CFG-` prefix | | **32** |

The two investigation stories remain because the brief checks reached a concrete
complexity boundary: coupled numeric limits and the lab's specialized consumer.
Routine questions already answered by inspection are not separate work items.

## Phases, gates and milestones

| Phase | Work | Exit gate and required evidence | Milestone / validator |
| --- | --- | --- | --- |
| 0. Owner review | Review this plan and the contract | **G0:** owner approves the product proposals and authorizes roadmap execution, including implementation and story closeout pushes. Record the exact authority in PROJECT. | Execution authorized / owner |
| 1. Finish the bounded native questions | CFG-I01 and CFG-I02 | **G1:** valid-domain tables, rate interpretation and correction examples are complete; no native premise required by later work remains guessed. Any conflict with the approved contract has been resolved with the owner. | **M1:** implementation evidence ready / agent |
| 2. Implement | CFG-C01–C03 and CFG-N01–N03, in dependency order | **G2:** each story meets its offline definition; default and custom settings form a coherent product; the real baseline and all external references are reviewed; affected tests pass; story closeouts are on main. | **M2:** technically complete / agent |
| 3. Deliver | CFG-D01 and CFG-D02 | **G3:** exact source, successful CI run, independently checked downloaded artifact, player instructions and owner procedure agree; no known contract defect remains. | **M3:** ready for owner acceptance / agent |
| 4. Owner acceptance | Owner observations and disposition | **G4:** explicit owner acceptance identifies the candidate and any accepted limitations. Reported defects are repaired and affected gates rechecked before a replacement handoff. | **M4:** accepted and archived / owner decision, agent records |

A gate is an evidence check, not another implementation story. G0 and G4 are human
decisions; intermediate gates require no routine owner approval. Do not manufacture
intermediate game-test requests. If new evidence changes a product decision or
requires broader access, stop the affected work and request that specific decision.

## How to execute the plan

1. Read AGENTS, PROJECT, this roadmap, the contract and the findings. Check Git
   status and preserve unrelated changes. Confirm G0 before implementation.
2. Use existing inspection results. Complete I02, then I01, close G1, then follow
   this order: **N01 → C01 → C02 → N02 → N03 → C03 → D01 → D02**. N01 adopts the
   reviewed reference baseline before later stories require real-library builds.
   The dependencies below govern if a different sequence is more useful.
3. Apply the shared completion rule to each story. Once its dependencies and gate
   are satisfied, continue without asking the owner to reauthorize ordinary work.
   Keep tests with the behavior they protect; no tests that merely assert story
   names, handoff text, source spelling or completion paperwork.
4. On a new unknown, make one bounded attempt to identify its cause. Record the
   evidence, blocked dependents and smallest next action in PROJECT. Continue
   independent authorized work. Split a genuine rabbit hole into a bounded story;
   do not weaken a requirement or extend the operational envelope to get past it.
5. Deliver the coherent candidate at G3 and stop for the owner's result. Following
   reported defects, return to their owning stories, repair within the approved
   scope, and repeat only affected checks plus artifact verification. A requested
   feature outside the contract needs separate steering.
6. At explicit acceptance, record the candidate and the actual observations in
   PROJECT, archive completed work definitions/evidence together, repair links,
   and make the authorized documentation closeout push. Leave release/publication
   unchanged unless the owner separately directs them.

### Shared story completion rule

A story is technically complete only when its stated result, focused checks and
review pass. Record its disposition, evidence links, limitations and next gate
in PROJECT; keep algorithms and maintenance constraints with source and technical
records. Static inspection cannot be labelled runtime validation.

Review the complete diff, stage named paths only, and commit the story with its
management update. Under G0's delivery authority, push to main after each story
and verify the actual CI result for that commit. A failed push or required check
leaves closeout incomplete. Report the commit and CI result in the execution
report; do not create a follow-up commit solely to record its own hash. The full
downloaded candidate verification belongs to D01, not every documentation push.

Investigation stories require documentation/evidence checks. Implementation
stories require affected behavior checks, compilation and the repository's shared
pipeline. Every new external type/member or Harmony target must be recorded in
the external-reference ledger, represented in the compile-only shims, and checked
against the deliberately adopted real references. Never package those shims or
upstream binaries. After two unsuccessful repairs of the same failure, report the
blocker and continue independent work rather than looping.

## E1 — Editable, valid recipes

### CFG-I01 — Bound numeric and recipe validity · weight 5

**Outcome:** Every accepted value has a native basis, and invalid numeric choices
have a well-defined nearest correction. **Depends on:** G0 and I02's lab findings;
use the completed brief inspection rather than restarting a general survey.

**Scope:** Trace the demonstrated duration scaling, input demand, output buffers,
per-update production limit, handcraft/raw-material count arithmetic and persisted
ID widths. Resolve empty inputs, duplicate rows and same input/output items. Cover
ordinary compatible tiers and native proliferation. Include I02's lab findings
when deriving that family's domain.

**Done/evidence:** A compact table states each actual bound or coupled constraint,
its native consumers, and boundary examples. Prove how integer rates map to native
durations and how an unattainable rate is corrected while preserving an otherwise
valid batch. Identify wider-mod assumptions explicitly. Document the conclusions
in the technical record and reconcile the contract's examples; no arbitrary
universal cap or balance-based rejection is acceptable.

**Stop/exclusions:** Stop a dependent case when its proof needs a new consumer or
runtime evidence beyond this bounded trace; name and split that case. No game
emulator, arithmetic rewrite of DSP, default rebalance or agent game execution.

### CFG-C01 — Read and write the editable recipe settings · weight 3

**Outcome:** A player can locate and edit every requested field in one BepInEx
file. **Depends on:** G1 and N01.

**Scope:** Implement the approved schema, defaults, comments, startup-only setting
application and persistence through BepInEx. Keep value correction in C02 and the
ID appendix in C03. Preserve stable recipe/section identities when output names
change. Reuse the existing plugin lifecycle; no custom loader/config framework.

**Done/evidence:** Generated default files contain all six recipes with integer
IDs/quantities/rates and clear units. Focused tests using the pinned BepInEx file
implementation cover missing keys, ordinary edits, startup reload, stable section
ordering and the approved comment/unknown-key treatment. Changes during a session
remain pending until restart. File errors are reported according to the contract;
they do not silently reset every recipe. Update external references with the code.

**Stop/exclusions:** A schema that cannot survive BepInEx serialization is a
contract issue to resolve before continuing. No game/profile writes or manager
installation; actual manager presentation remains a final owner observation.

### CFG-C02 — Correct invalid values precisely · weight 5

**Outcome:** Valid player choices survive; each invalid setting receives its
specified nearest-valid correction. **Depends on:** C01, N01 and G1.

**Scope:** Implement the approved scalar, ID, row and coupled-value rules. Preserve
raw invalid values long enough to explain a correction. Treat reference-only
entries as inert. Publish one effective configuration for registration; write
corrections consistently with what production will use.

**Done/evidence:** Tests cover valid extremes, nearest-ID ties, negative and very
large integer text, nonnumeric values, missing/default fields, unused/duplicate
rows, machine-specific constraints and rate quantization. Valid unrelated fields
survive; a second load makes no further correction; reports identify section/key,
original value, effective value and reason. Verify BepInEx round trips rather than
only a test double. Count changes cannot quietly compensate for a rate error.

**Stop/exclusions:** No fallback to an entire default recipe for a one-field error.
If the native domain or correction precedence remains ambiguous, stop that path;
no global converter replacement or reflective access to BepInEx internals without
an evidenced need and review of the narrower alternatives.

### CFG-C03 — Generate the grouped ID reference · weight 2

**Outcome:** Players can find usable IDs without leaving the configuration.
**Depends on:** C01, C02, N01.

**Scope:** Append ordinary BepInEx reference sections for items, technologies and
machines, using stable numeric IDs and native readable names. Include applicable
eligibility/level/family information and regeneration behavior from the contract.

**Done/evidence:** The reference and validator use the same catalogue snapshot.
Generated data agrees with that snapshot; editing lookup names cannot affect
recipes. Recipe sections remain first and reference groups remain last after
normal mod saves. Review the actual file in a text editor and exercise its normal
BepInEx save cycle. Supply the same file for the final Thunderstore Mod Manager
check; do not claim that app was tested through a substitute.

**Stop/exclusions:** Missing metadata is reported and resolved, not supplied from
an invented list. No custom manager extension, true nested-group promise, static
copy of game assets, or separate lookup website.

## E2 — Native production and progression

### CFG-I02 — Resolve the lab consumer constraints · weight 3

**Outcome:** Lab choices are accepted or corrected on specific native evidence.
**Entry:** G0; bounded follow-up to the matrix-specific behavior already found.

**Scope:** Trace the lab's input/output limits and the consumers of its
matrix-derived production-state value, including stacked operation. Determine
which product IDs/counts can use each lab tier without replacing native behavior.

**Done/evidence:** List supported combinations and counterexamples with exact
methods and asset identities. Feed numeric limits into I01 and machine/reference
eligibility into the contract. A native limitation explains a correction; the mere
fact that vanilla uses matrices does not prove every other product is invalid.

**Stop/exclusions:** If establishing an advertised combination requires executing
DSP or modifying native lab behavior, record that boundary and return the smallest
scope decision. Do not research all special machines: fractionation and energy
conversion already fail the fixed-rate recipe contract for identified reasons.

### CFG-N01 — Supply machine data before recipe caches · weight 3

**Outcome:** Configurable machine choices are grounded in initialized native data
at the correct startup point. **Depends on:** G1.

**Scope:** Deliberately adopt the reviewed game-reference identity, update the
existing evidence baseline, and integrate the narrow Harmony lifecycle point
identified by inspection. Resolve item/machine/tech catalogues and machine speeds
before publishing recipes, without rebuilding unrelated native caches. Keep the
current fixed definitions working at this stage; configurable loading follows in C01.

**Done/evidence:** Real-reference checks cover the changed hook and every new
member. Targeted ordering/repeat-call tests show model data is available before
configuration-dependent registration and before item/recipe links are constructed.
The catalogue accepts eligible content present at that point, including other
mods' data when the same requirements are met. Registration conflicts still name
the conflict; existing recipe IDs are never silently reassigned.

**Stop/exclusions:** If initialization order differs from the source finding, stop
registration work and resolve the specific seam. No broad load-order framework,
late hot reload, new dependency package or changes to the abnormality hotfix.

### CFG-N02 — Apply recipe contents and research gates · weight 3

**Outcome:** All six configured records and their unlocks coexist correctly.
**Depends on:** C02 and N01.

**Scope:** Register effective ingredients, output, machine family and completed-tech
gates. Accumulate all additions when recipes share one technology. Reconcile
already-completed gates on real save import while preserving previous unlocks,
preview isolation and unrelated native awards. Follow the native save policy;
no factory/queue migration is introduced.

**Done/evidence:** Behavior checks cover different recipe shapes, shared gates,
no/partial/all completed gates, already-known recipes, repeated registration/import,
previews and technology changes. Only the mod's intended additions occur. New-game
and existing-save behavior agree with the contract; unusual hidden/high-level
tech choices are not rejected merely for being inconvenient. Source review confirms
native buffers/queues are not rewritten. Record remaining game observations for D02.

**Stop/exclusions:** No replaying entire research rewards, relocking saved recipes,
resource refunds or save editor. A mismatch with the approved native-transition
policy returns to the owning decision, not an improvised migration.

### CFG-N03 — Verify native production semantics · weight 3

**Outcome:** The emitted recipes deliver the configured rates and native behavior.
**Depends on:** N02.

**Scope:** Integrate the proven timing conversion, selected-tier baseline,
assembler-only handcraft rule and input-derived proliferation. Preserve ordinary
speed differences on other compatible tiers and the original six defaults.

**Done/evidence:** Focused checks use actual emitted records and verified machine
speeds for default/custom batches, attainable and corrected rates, supported
families, and acceleration/extra-product eligibility. Native source traces cover
consumer constraints; tests do not merely compare copied constant tables. Prepare
measurable owner expectations for baseline and upgraded production and the agreed
lab cases. Changing a machine must not introduce an extra production multiplier.

**Stop/exclusions:** A demonstrated valid choice that cannot run is a defect to
resolve, not permission to reject it. No replacement production loop, changed
item productivity, balance checks or assumed runtime pass.

## E3 — Candidate and owner handoff

### CFG-D01 — Package and verify the candidate · weight 3

**Outcome:** Main and its downloadable artifact identify one coherent candidate.
**Depends on:** G2, including C03.

**Scope:** Update player-facing configuration/editing guidance, build with the
existing pipeline and inspect the exact downloaded CI package and evidence.
Keep implementation explanations in source/technical docs. Do not overwrite an
existing player's config by shipping an active generated config in the package.

**Done/evidence:** Local shared checks and real-reference validation pass; the exact
main-commit CI run succeeds. Download and verify source identity, metadata,
loader/dependencies, package contents and provider/file hashes. Check the downloaded
DLL against real references. Record the chosen source, run/attempt, version and
hashes in PROJECT. The docs explain restart behavior, file location in the chosen
profile, corrections, the ID appendix and preparation before recipe changes.

**Stop/exclusions:** A green run without inspecting its artifact does not close
this story. Do not hand off an older artifact after a failed build. No release
tag, Thunderstore publication, version promotion, new installer or dependency binaries.

### CFG-D02 — Prepare and deliver owner acceptance · weight 2

**Outcome:** The owner can judge the feature using a short procedure and one
identified candidate. **Depends on:** D01.

**Scope:** Supply the exact artifact, prerequisites, disposable-checkpoint setup,
expected observations and a compact failure-report format. Walk the instructions
against the actual package and generated config. Group observations so the owner
is not asked to repeat the same setup for each story.

**Done/evidence:** Cover Notepad and Thunderstore Mod Manager edit/search/save,
defaults and custom IDs/counts, a shared gate, corrections, native production and
proliferation, handcraft restriction, restart and the prepared save-transition
cases. Include clearing selected recipes for family changes and cancelling queued
handcrafts before saving under the old settings. Supply expected rates/counts;
keep runtime result fields blank. G3 records technical readiness and awaiting
owner acceptance, with no known contract defect hidden as an untested claim.

**Stop/exclusions:** Stop agent execution at the delivered handoff. Do not infer
acceptance from silence, partial unreported checks or CI success. The owner may
explicitly accept with stated limitations. At that decision follow G4 and the
closeout procedure above; publication still requires separate authority.
