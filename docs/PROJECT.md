# Project steering

This is the sole authority for steering, decisions' dispositions, current and
historical work status, authorization, technical readiness, owner acceptance,
and publication. Other documents define contracts or evidence and link here.

## Current state

- Phase: pre-release planning, owner-directed on 2026-10-08.
- Authorized scope: record the owner's acceptance of the MVP as is, close the
  completed roadmap as accepted, transition to pre-release planning, and promote
  the minor version to 3.
- Build version line: `0.3.x`, owner-directed on 2026-10-08; the patch remains the
  CI run number (0 for local builds). The historical MVP candidate remains `0.1.8`.
- Product implementation: MVP-01 through MVP-07 completed and accepted as the MVP;
  M3 satisfied. No implementation story remains active. The
  [owner handoff](archive/2026-10-08-mvp/OWNER-VALIDATION.md) is archived.
- Planning: [recipe decisions](planning/MVP-RECIPES.md) and
  [completed roadmap](archive/2026-10-08-mvp/ROADMAP.md) retained; the live
  [roadmap entry point](planning/ROADMAP.md) is reserved for pre-release planning;
  no pre-release work items have been defined yet.
- Owner acceptance: accepted as is on 2026-10-08 after the owner-reported checks
  [recorded below](#owner-acceptance-record--2026-10-08). Completion of the full
  proposed validation procedure is not a condition of this acceptance.
- Publication: none. No release tag or store publication has been authorized.
- Git delivery: preparation and its evidence closeout are on main through
  `db3d8ed85df251a57e817881a30d61bdad7938e0`; MVP-01 is `89d40d5`, MVP-02 is `2ebab16`,
  MVP-03 is `bfac10d`, MVP-04 is `0be4006`, MVP-05 is `5a8e01f`;
  MVP-06 source/closeout are `1fabb25` / `05b1130`; MVP-07 is `cb9456d`.
  Each story commit includes its management update and is pushed to main at closeout.
  Candidate source is `1fabb2543d56d90d5707863c234c18c33ba93f02`; downloaded package
  `0.1.8` from [run 37699427929](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37699427929)
  passed independent verification. Later documentation commits retain this candidate.

## Product basis and boundaries

The [supplied concept](concept/CONCEPT.md) is the planning input: synthesize the
six existing Dark Fog materials through rare resources and advanced industry,
using native production and combat research. Its [evidence record](concept/EVIDENCE.md)
identifies what was inspected and the limits of those conclusions.

The owner authorized the MVP roadmap's implementation and story-by-story main
delivery, subsequently accepted that MVP, and moved the project to pre-release
planning. The [recipe contract](planning/MVP-RECIPES.md) resolves the
concept's open recipe and gate choices; the original concept remains unchanged.
Imperative wording in that imported handoff does not independently authorize work.
Reference repositories supplied conventions only, not product code or identities.

## Preparation record

| Work | State | Evidence |
| --- | --- | --- |
| Structure, governance, concept intake | Technically complete | [Preparation evidence](archive/2026-10-07-repository-preparation/VALIDATION.md) |
| Local agent bootstrap | Verified | [Preparation evidence](archive/2026-10-07-repository-preparation/VALIDATION.md) |
| Local compilation and package pipeline | Verified with scaffold only | [Preparation evidence](archive/2026-10-07-repository-preparation/VALIDATION.md) |
| GitHub Actions workflow | Hosted build and both artifact uploads passed; downloaded bytes independently verified | [Run 1](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37689808206) and [evidence](archive/2026-10-07-repository-preparation/VALIDATION.md#hosted-execution-and-download-verification) |

The initial local-only closeout was premature: workflow linting did not establish
the requested remote pipeline. The owner corrected this on 2026-10-07. Main delivery,
hosted execution, and independent artifact verification now satisfy that preparation
gate. Repository preparation is technically complete. The owner subsequently asked
for this first MVP roadmap and has now approved implementation. Preparation evidence
does not establish product runtime behavior or acceptance.

Verified scaffold: `0.1.1`, source `99232fd`, workflow run `37689808206`, attempt `1`.
All 42 source-file hashes matched the committed source export; the package and
evidence archives matched GitHub's digests. Exact package/DLL identities and evidence
limits are retained in the linked validation record. This is pipeline evidence,
not an installable product or a release.

Installed game files and reference repositories are read-only inputs. No game
launch, save access, deployment, runtime test, tag, or store publication is part
of this preparation. Source delivery does not authorize installing or publishing
the scaffold package.

## Decision register

| ID | Disposition | Decision and rationale |
| --- | --- | --- |
| PREP-01 | Adopted within repository-preparation authority | One state authority; plans and technical records have separate responsibilities. See [working methods](WORKING-METHODS.md). |
| PREP-02 | Adopted for tooling only | Use Windows/PowerShell 7 and a pinned .NET SDK. Compile a non-plugin fixture to verify the pipeline without selecting product architecture. |
| PREP-03 | Adopted for tooling only | Keep the full source-evidence bundle local; retain concept and selected catalogue/provenance records with exact supplied bytes. See [evidence policy](concept/EVIDENCE.md). |
| PREP-04 | Adopted for tooling only | Build scaffold packages only, with explicit fixture identity and no release automation. Product package dependencies and branding require later decisions. |

The PREP choices govern repository tooling only. Later owner product decisions,
recorded on 2026-10-08 from instructions and clarification answers in this task:

| ID | Disposition | Decision and rationale |
| --- | --- | --- |
| MVP-D01 | Owner-directed | Use the six-step synthesis chain, one of each listed input, one output except two Energy Shards from Fractal Silicon alone. These choices supersede the concept's unresolved balance examples. See [recipe contract](planning/MVP-RECIPES.md). |
| MVP-D02 | Owner-directed | Gate each recipe by the existing technology unlocking its combat component; Energy Shard uses Particle Control. No new synthesis technology or additional AND gate. |
| MVP-D03 | Owner-directed | Target 60 products/minute specifically in Assembler Mk.I; Core Element uses the ordinary Chemical Plant at 60/minute; Energy Shard uses Arc Smelter at 120/minute. Keep native higher-tier bonuses. |
| MVP-D04 | Owner-directed | Use the rightmost six slots of the Items selector's cube row, in chain order. Static native mapping gives 1809–1814. Preserve native item sorting and other recipes. |
| MVP-D05 | Owner-directed | Follow usual facility handcraft distinctions and regular native proliferation. Four assembler recipes allow handcrafting; smelt/chemical do not. Native ship-input productivity makes Matrix/Neuron acceleration-only; do not override it. |
| MVP-D06 | Owner-directed planning boundary | First roadmap ends at a final owner-validation-ready MVP delivering the concept's peaceful industrial route. Owner reviews before giving the implementation green light. Game acceptance and publication are separate. |
| MVP-D07 | Owner-authorized, 2026-10-08 | Implement all approved stories through owner handoff; update management records and push to main after each completed story. This supersedes the pending implementation decision, not the separate owner gameplay/publication gates. |
| MVP-D08 | Owner-directed, 2026-10-08 | Use BepInEx's patching capabilities and established native/library mechanisms where useful. Avoid a custom loader or injection framework. |
| MVP-D09 | Owner-directed, 2026-10-08 | Remote product builds use shims for all external game/mod library types, including BepInEx, Harmony, and Unity. Maintain a meticulous external type/member ledger and verify the shim surface and product references against real local metadata. |
| MVP-D10 | Adopted under implementation authority, 2026-10-08 | Use BepInEx's bundled Harmony for early native-table registration and post-import reconciliation; no LDBTool dependency. IDs 401–406, durations 60/45/45/45/45/60 ticks, existing native preload/cache lifecycle. See [integration contract](implementation/NATIVE-INTEGRATION.md). |
| MVP-D11 | Owner-directed, 2026-10-08 | Thunderstore package name `DSPDarkFogIndustry` (owner correction supersedes the spaced package name); BepInEx GUID `dark-fog-industry`. Human-readable plugin title remains DSP Dark Fog Industry. |
| MVP-D12 | Owner-accepted and directed, 2026-10-08 | Accept the MVP as is following partial owner validation; mark the MVP roadmap completed and accepted and move to pre-release planning. Unreported checks remain unverified, not acceptance blockers or automatically activated work. Publication remains separate. |
| REL-D01 | Owner-directed, 2026-10-08 | Promote MINOR to 3 in VERSION, retaining MAJOR=0 and the existing build-number patch scheme. New builds use 0.3.x; this does not relabel the accepted historical MVP artifact or authorize publication. |

The [integration contract](implementation/NATIVE-INTEGRATION.md) resolves registration,
stable IDs, durations, shim-based hosted compilation, and the removal boundary.
Keep synthesis saves with the mod installed; dependable rollback restores an
untouched pre-mod checkpoint. This is a documented limitation, not a safe-uninstall
claim. The owner report below supplements the technical evidence; acceptance is
the owner's explicit decision, not an inference that every runtime path was tested.

## MVP work state

The [archived roadmap](archive/2026-10-08-mvp/ROADMAP.md) defines scope, sequence, completion criteria,
evidence, and stop conditions. Its [technical basis](archive/2026-10-08-mvp/MVP-TECHNICAL-BASIS.md)
separates inspected facts from investigations and final owner observations.

| Work | Disposition | Evidence / next gate |
| --- | --- | --- |
| MVP roadmap preparation | Approved for implementation, 2026-10-08 | Owner instruction in this task; review record below |
| MVP delivery roadmap | Completed and accepted, 2026-10-08 | Explicit owner acceptance as is; all seven stories completed; archived definitions retained |
| MVP-01: Integration seam | Technically closed, 2026-10-08 | [Integration contract](implementation/NATIVE-INTEGRATION.md); native/static asset/catalogue checks; no runtime claim |
| MVP-02: Actual plugin build | Technically closed, 2026-10-08; M1 satisfied | [Validation](archive/2026-10-08-mvp/VALIDATION.md#mvp-02--plugin-and-external-references); shim/real compilation, ledger/binding checks, negative cases, shared build |
| MVP-03: Six-recipe registration | Technically closed, 2026-10-08 | [Validation](archive/2026-10-08-mvp/VALIDATION.md#mvp-03--registration); 50 product assertions, real/shim compilation and metadata checks; UI remains owner-observed |
| MVP-04: Native production/progression | Technically closed, 2026-10-08 | [Validation](archive/2026-10-08-mvp/VALIDATION.md#mvp-04--native-production-and-progression); 99 assertions and static dependency/native behavior evidence |
| MVP-05: Existing saves/removal | Technically closed, 2026-10-08; M2 satisfied | [Validation](archive/2026-10-08-mvp/VALIDATION.md#mvp-05--save-reconciliation); 487 assertions; real/shim bindings; runtime persistence/removal not specifically reported |
| MVP-06: Actual CI candidate | Technically closed, 2026-10-08 | [Validation](archive/2026-10-08-mvp/VALIDATION.md#mvp-06--candidate-packaging); source `1fabb25`, hosted run success, both provider digests and 87 source hashes matched, downloaded DLL real bindings passed |
| MVP-07: Owner validation handoff | Technically closed, 2026-10-08; M3 satisfied | [Archived procedure](archive/2026-10-08-mvp/OWNER-VALIDATION.md) and [review evidence](archive/2026-10-08-mvp/VALIDATION.md#mvp-07--owner-handoff-review); original blank checklist preserved; subsequent owner report below |
| Final owner validation and acceptance | Accepted as is, 2026-10-08 | Owner reported recipe unlocks and production in different facilities with/without proliferation, with no observed problems or side effects; full checklist not completed |
| Pre-release planning | Current phase, 2026-10-08 | Owner-directed transition; scope and work items to be defined with the owner |

### Owner acceptance record — 2026-10-08

Source: the owner's message in this task. The owner reported validating recipe
unlock behavior and actual production in different production facilities, both
with and without proliferation. Operation seemed smooth, with no side effects or
problems observed during those checks.

The owner explicitly stated that the entire proposed procedure was not completed
and accepted the MVP as is. The report does not enumerate every gate, facility,
recipe or measured rate; it is not an exhaustive pass of the archived checklist.
Other observations, including save/reload, rollback and the full downstream
research/combat checks, remain unverified where not specifically reported. They
do not block the owner's acceptance and are not automatically carried into a new
backlog. The handoff's candidate identity remains `0.1.8`; no different build was
identified in the owner report.

The MVP roadmap is completed and accepted. The next phase is pre-release planning,
with publication and its work scope still separate from this acceptance.

### Planning review record

The requested review passes revise the same roadmap, without generating parallel
plans or separate sources of work state:

1. Technical accuracy: checked native IDs, gate records, 14-column placement,
   handcraft/display distinction, input-derived proliferation, preload order, and
   native discovery. Kept the Mk.I-specific rate distinct from native 1x timing
   and documented the remaining prefab-speed evidence gap.
2. Investigation: assigned each remaining native/dependency question to a story,
   with evidence outputs and stop conditions before dependent work. Save/removal
   and hosted references are considered at the integration gate, not at delivery.
3. Scope: retained the complete peaceful route and all five downstream research
   relationships; excluded balance changes, custom UI, broad compatibility work,
   game/save access, and publication. Grouped owner observations into one handoff.
4. Project management: gave seven bounded stories dependencies, outcomes,
   exclusions, completion evidence, and stop conditions. Kept tests with behavior,
   required downloaded CI-artifact verification, and separated approval/readiness/
   owner acceptance. PROJECT remains the sole state authority.
5. Final review: checked the four documents against the owner's recipe and endpoint
   instructions. Clarified the registration/production story boundary, allowed
   earlier synthesized inputs in the acyclic-route criterion, and required a static
   real-reference check of the downloaded CI plugin. Seven story IDs and seven
   evidence/stop sections are present; no implementation or runtime pass is implied.

Planning validation passed: repository checks covered PowerShell syntax, 68 local
documentation links, exact retained concept hashes, source hygiene, and tracked
diff whitespace. The four changed/new documents also passed whitespace/conflict
marker checks. No build was rerun for these documentation changes; prior scaffold
pipeline results remain historical evidence only.

Planning itself involved no product implementation, game launch, deployment, or save
access. The owner's later implementation authorization is recorded above. That work
reached its owner-handoff endpoint and was subsequently accepted as is. Completed
definitions, superseded planning evidence and dated offline results are archived under the
[working methods](WORKING-METHODS.md#closeout-and-archives); active recipe/integration
contracts remain active and the owner procedure is retained in the archive.
Findings do not activate new work without owner steering.
