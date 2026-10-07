# Project steering

This is the sole authority for steering, decisions' dispositions, current and
historical work status, authorization, technical readiness, owner acceptance,
and publication. Other documents define contracts or evidence and link here.

## Current state

- Phase: MVP implementation.
- Authorized scope: on 2026-10-08 the owner approved the roadmap for implementation
  through completion and owner handoff, with management updates and a main push
  after each completed story.
- Product implementation: MVP-01 technically closed; MVP-02 active.
- Planning: [recipe decisions](planning/MVP-RECIPES.md) and
  [roadmap](planning/ROADMAP.md) approved for implementation.
- Owner acceptance: no MVP exists yet; gameplay validation and acceptance remain later.
- Publication: none. No package is approved for installation or release.
- Git delivery: preparation and its evidence closeout are on main through
  `db3d8ed85df251a57e817881a30d61bdad7938e0`. Story commits will include the management
  update and be pushed to main at each story closeout; no product CI result yet.

## Product basis and boundaries

The [supplied concept](concept/CONCEPT.md) is the planning input: synthesize the
six existing Dark Fog materials through rare resources and advanced industry,
using native production and combat research. Its [evidence record](concept/EVIDENCE.md)
identifies what was inspected and the limits of those conclusions.

The owner's current request authorizes the approved roadmap's implementation and
story-by-story main delivery. The [recipe contract](planning/MVP-RECIPES.md) resolves the
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

The [integration contract](implementation/NATIVE-INTEGRATION.md) resolves registration,
stable IDs, durations, shim-based hosted compilation, and the removal boundary.
Keep synthesis saves with the mod installed; dependable rollback restores an
untouched pre-mod checkpoint. This is a documented limitation, not a safe-uninstall
claim. Product implementation and runtime observations remain to be validated.

## MVP work state

The [roadmap](planning/ROADMAP.md) defines scope, sequence, completion criteria,
evidence, and stop conditions. Its [technical basis](implementation/MVP-TECHNICAL-BASIS.md)
separates inspected facts from investigations and final owner observations.

| Work | Disposition | Evidence / next gate |
| --- | --- | --- |
| MVP roadmap preparation | Approved for implementation, 2026-10-08 | Owner instruction in this task; review record below |
| MVP-01: Integration seam | Technically closed, 2026-10-08 | [Integration contract](implementation/NATIVE-INTEGRATION.md); native/static asset/catalogue checks; no runtime claim |
| MVP-02: Actual plugin build | Active | Plugin, complete external-reference shims/ledger, local binding validation; M1 |
| MVP-03: Six-recipe registration | Authorized; waiting on M1 | Native registration |
| MVP-04: Native production/progression | Authorized; waiting on MVP-03 | M2 |
| MVP-05: Existing saves/removal | Authorized; waiting on MVP-03 | M2 |
| MVP-06: Actual CI candidate | Authorized; waiting on M2 | Main delivery and downloaded artifact verification |
| MVP-07: Owner validation handoff | Authorized; waiting on MVP-06 | M3 |

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
access. The owner's later implementation authorization is recorded above. Keep the live roadmap until its work is accepted,
closed within the authorized boundary, or superseded; then archive under the
[working methods](WORKING-METHODS.md#closeout-and-archives), retaining active contracts.
