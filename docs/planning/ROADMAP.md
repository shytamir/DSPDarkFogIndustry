# Configurable recipes roadmap

[PROJECT](../PROJECT.md) owns authorization, work state and decisions. This
skeleton orders the investigations needed for a reviewable configuration contract
and identifies the delivery work that would follow implementation approval.
[Technical findings](../implementation/CONFIGURATION-INVESTIGATION.md) contain the
source evidence and its limits. The [MVP recipe contract](MVP-RECIPES.md) is the
current behavior against which defaults can be compared.

## Player outcome and boundaries

Players can edit the mod's recipe unlock technologies, ingredient and product
item IDs, integer quantities, integer production rates and manufacturing machine
selection by ID in its BepInEx configuration file. Machine selection must have a
clear hand replication rule grounded in native behavior. Ordinary proliferation and
facility bonuses continue to apply.

The file must be understandable in Windows Notepad: recognizable recipe sections,
plain descriptions, units beside rates, and a reference at the end grouped into
items, technologies and machines. Reference-only settings may make that same
information available in a mod manager. Editing reference information must have
no effect on recipes.

Accept every configuration within the established native validity constraints.
Do not impose balance preferences as validation rules. Correct invalid settings
to specific nearest-valid values, preserving unrelated valid choices and making
each correction understandable to the player. Technical validity, an easy
progression route and the author's preferred balance are different things.

The starting proposal keeps six recipe identities and their selector positions,
with the accepted MVP recipes as defaults. This is configuration of the mod's
recipes, not an editor for all vanilla recipes or research costs. A custom UI,
additional recipe slots, save repair, release promotion and publication are not
included. Existing hotfix acceptance remains separate; see
[HF-01](HF-01-PROTOTYPE-DETECTION.md) and PROJECT.

## Decisions to settle

These questions define the planning work; their dispositions belong in PROJECT.
An example or suggested starting point below is not an adopted rule.

| Topic | Decision needed | Evidence or comparison needed |
| --- | --- | --- |
| Recipe shape | How are variable ingredients and products entered? Are empty inputs, multiple outputs, repeated items, catalysts and disabled recipes supported, and what does zero mean for each field? | Native consumers and editable examples, including a recipe beyond the current one/two-input shape. Keep output item IDs editable; do not silently freeze them to the original six products. |
| Rate meaning | Products per minute in the selected machine, or a common base speed? Which product defines the rate for multiple outputs? What happens when an integer rate cannot be represented exactly? | Compare ordinary and upgraded facilities, batch sizes and slow/fast rates. A selected-machine, unproliferated baseline is a candidate because current defaults use actual facility rates. |
| Machine and hand replication | Does an item ID select a compatible facility family or restrict production to that exact machine? Which handcraft rule applies to each supported family? | Native machine capabilities and recipe flags. A universal machine-derived handcraft rule cannot be assumed. |
| IDs and valid recipes | Which technology records can gate recipes, including upgrades/repeatable research or an optional no-gate value? Which item/machine IDs are usable? How do content added by other mods and recipe dependencies affect validity? | Catalogue existence is necessary but not sufficient. Investigate unsafe recursion separately from merely difficult or circular progression; do not reject valid choices for balance reasons. |
| Nearest corrections | Define distance and tie-breaking for absent IDs, bounds and coupled settings; define fallbacks where no numeric distance exists. Which field takes precedence when several settings conflict? | Concrete examples for unknown IDs, overflow, fractions/text in integer fields, duplicates and machine/recipe conflicts. Decide how corrections appear in the file and log, and how malformed file structure differs from invalid values. |
| File and reference | Choose section/key layout, list syntax, reference grouping/language and the mod-manager app/version to support. | Review a complete sample in Notepad and the selected manager; compare comments with inert lookup entries. Include ID/name pairs and any eligibility distinctions. Prove the reference remains at the end after normal saves. |
| Applying edits | When do edits take effect, and what happens to existing unlocks, occupied facilities and queued handcrafts after a configuration change? | Startup-only application is a candidate. Examine changes between sessions and saves, family switches, buffer contents and unfinished work before promising safe transitions. |

## Milestones

1. **Configuration contract ready for owner review:** the valid domain, file
   examples, correction rules and save/session behavior are supported by evidence;
   remaining implementation stories have definite boundaries. Owner approval of
   that contract and authorization to implement are separate decisions.
2. **Technical candidate:** approved behavior works through the existing native
   integration, with focused tests, reviewed external references and a verified
   downloadable CI artifact.
3. **Owner handoff:** one identified candidate, player documentation and a short
   in-game procedure cover the behavior offline checks cannot establish. Owner
   acceptance and publication remain separate from technical completion.

## CFG-01 — Establish the valid recipe domain

**Outcome:** an evidence-backed account of what players can configure, without
arbitrary balance restrictions.

Scope includes item/technology eligibility, machine capabilities and speeds,
handcraft rules, input/output shapes, integer arithmetic and timing limits,
proliferation, and dependencies that could break native consumers. Review the
detected game-reference change and propose which baseline to adopt; leave the
reference files unchanged during planning. Use static source and asset inspection;
do not execute the game or change product code.

**Dependencies and completion:** build on the initial investigation. Deliver a
capability/constraint table with source identities, a rate interpretation proposal,
and specific owner choices where native behavior does not provide one answer.
Distinguish observed examples from proven limits. No broad cross-mod certification
or balance redesign is included. Stop dependent design where the native consumer
or numerical boundary is still unknown; identify the exact missing evidence.

## CFG-02 — Define the file and correction contract

**Outcome:** a configuration players can navigate and recover from mistakes.

Use CFG-01's domain to prepare a full default-file example and representative
custom edits. Define the meaning and format of each field, reference groups,
nearest-value rules and correction messages. Include missing values, unknown IDs,
invalid numbers, coupled settings, malformed structure and file-access failures
as distinct cases. Reuse BepInEx facilities where they meet the contract; no new
configuration framework or custom UI is part of this story.

**Dependencies and completion:** presentation exploration can overlap CFG-01;
final correction rules depend on its valid domain. Review the sample in Notepad
and the chosen mod manager. Demonstrate how normal serialization retains readable
ordering, explanatory comments and the final reference. Define how player-written
comments, unknown keys and changed item names are treated. Specify deterministic
corrections that leave valid choices intact and stabilize after one correction
pass. Stop for owner choice when equally near repairs change different player choices; do
not silently pick a policy. No product implementation belongs in this story.

## CFG-03 — Resolve configuration changes and complete the plan

**Outcome:** a clear contract for editing recipes after players have used them,
and an implementation roadmap the owner can approve.

Trace registration, research unlocks, factory import and handcraft queue import
for the agreed schema. Cover shared unlock technologies, changed gates, changed
items/counts, changed machine family, occupied buffers, and loading different
saves with one configuration. Define when settings apply and what preparation a
player must perform before editing. Do not introduce save repair or automatic
resource refunds without a separate owner decision.

**Dependencies and completion:** CFG-01 and CFG-02 supply the proposed contract.
Record source-backed transition behavior and any consequences the owner must
accept. Revise CFG-04 through CFG-06 into bounded implementation stories with
tests and human observations tied to actual risks. Present the file example,
remaining choices and revised plan together. Stop at that review; unresolved
resource loss or inconsistent save behavior cannot be labelled safe or solved by
an unapproved restriction on otherwise valid configurations.

## CFG-04 — Load settings and supply the ID reference

**Outcome:** the agreed configuration produces validated effective recipes and a
usable lookup reference. This is a proposed implementation story.

Dependencies are the completed contract and explicit implementation authority.
Scope covers BepInEx loading/writing, defaults, corrections and the grouped
reference. Its IDs and eligibility labels must agree with the data used to
validate settings, including the agreed handling of other mods' content.
Keep prototype registration changes in CFG-05. Inventory every new
external API and update the compile-only shims and real-reference checks with
the change; do not distribute upstream libraries.

**Completion evidence:** focused tests show valid boundary values survive,
invalid values receive the agreed nearest corrections, unrelated choices survive,
reloading corrected settings is stable, and reference edits cannot alter recipes.
Use the actual pinned BepInEx serializer for file round trips and readability
checks, with no installed-game execution. Cover the agreed failure behavior when
the file cannot be read/written. Stop if the approved schema or correction policy
cannot be expressed faithfully; do not hide the mismatch behind defaults.

## CFG-05 — Apply configurable recipes through native behavior

**Outcome:** the effective settings control production and unlocks consistently.
This is a proposed implementation story depending on CFG-04.

Scope covers recipe registration, native cache timing, shared technology gates,
machine selection, hand replication, proliferation and the approved save/session
behavior. Preserve unrelated native recipes and technology unlocks. Do not add
custom production scaling, unrelated hotfix changes or unapproved migration work.

**Completion evidence:** behavior tests cover several recipes sharing one gate,
custom items/counts/rates, supported machine choices, repeat registration, and
relevant save transitions. Default settings preserve the accepted recipe behavior.
Compile against shims and the deliberately adopted real baseline, maintaining the
external-reference ledger. Keep runtime-only expectations for the owner procedure.
Stop if a supported valid choice cannot run through the inspected native path;
resolve the contract or implementation rather than quietly rejecting that choice.

## CFG-06 — Deliver the candidate and owner procedure

**Outcome:** a reproducible artifact and concise instructions make the new
behavior ready for owner validation. This is a proposed delivery story.

After CFG-05, update player-facing configuration guidance and exercise the shared
build/package pipeline. Download the artifact from its exact main commit; verify
metadata, package contents, source identity, digests and real-library references.
Ensure raw extracted assets and dependency binaries are absent from the package.

**Completion evidence:** identify the candidate version and hashes, report offline
results and limitations, and provide one owner-run procedure covering default
recipes, custom recipes/gates, corrections, file/reference usability, native
production/handcraft/proliferation, and the agreed edit/save transitions. The
procedure must state preparation, expected observations and how to report a
failure. No agent game launch, owner acceptance, version promotion or publication
is implied. An artifact mismatch or unresolved product failure stops handoff.

## Review and closeout

Revise the plan as investigations resolve the decision agenda. Review technical
accuracy, coverage of remaining unknowns, scope/authority and readability before
requesting implementation. Record decisions and delivery evidence in PROJECT,
keep implementation details with source and technical evidence, and archive
completed definitions according to the [working methods](../WORKING-METHODS.md).
