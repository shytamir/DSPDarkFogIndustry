# Project steering

This is the sole authority for steering, decisions' dispositions, current and
historical work status, authorization, technical readiness, owner acceptance,
and publication. Other documents define contracts or evidence and link here.

## Current state

- Phase: configurability implementation, authorized by the owner on 2026-10-13.
- Authorized scope: execute the approved configurability roadmap through owner
  handoff, committing and pushing each completed story with its management update.
  G0 is approved. Game execution, installed-file/manager-profile changes and save
  access remain outside the agent's operational envelope. Completed
  MVP and cleanup remain closed; the corrected [HF-01](planning/HF-01-PROTOTYPE-DETECTION.md)
  candidate still awaits owner validation independently of this planning work.
- Build version line: `0.9.x`, owner-directed on 2026-10-08; the patch remains the
  CI run number (0 for local builds). The historical MVP candidate remains `0.1.8`.
- Product implementation: MVP-01 through MVP-07 completed and accepted as the MVP;
  the MVP's M3 satisfied. Configurability implementation is active. The
  [owner handoff](archive/2026-10-08-mvp/OWNER-VALIDATION.md) is archived.
- Planning: the [live roadmap](planning/ROADMAP.md) defines the configurability
  investigation and approved delivery sequence. G0 approves the configuration
  contract and implementation through G3; G4 remains the owner's acceptance. The
  [MVP recipe contract](planning/MVP-RECIPES.md) describes current behavior;
  the [completed MVP roadmap](archive/2026-10-08-mvp/ROADMAP.md) remains archived.
  No release version or schedule has been selected for configurability; this
  request does not itself establish the scope of version 1.0.
- Owner acceptance: accepted as is on 2026-10-08 after the owner-reported checks
  [recorded below](#owner-acceptance-record--2026-10-08). Completion of the full
  proposed validation procedure is not a condition of this acceptance.
- Publication: the owner reports publishing the latest artifact, `0.9.14`, on
  2026-10-08. See the [publication record](#publication-record--2026-10-08).
  Later CI builds do not supersede that published baseline.
- Git delivery: preparation and its evidence closeout are on main through
  `db3d8ed85df251a57e817881a30d61bdad7938e0`; MVP-01 is `89d40d5`, MVP-02 is `2ebab16`,
  MVP-03 is `bfac10d`, MVP-04 is `0be4006`, MVP-05 is `5a8e01f`;
  MVP-06 source/closeout are `1fabb25` / `05b1130`; MVP-07 is `cb9456d`.
  Each story commit includes its management update and is pushed to main at closeout.
  Candidate source is `1fabb2543d56d90d5707863c234c18c33ba93f02`; downloaded package
  `0.1.8` from [run 37699427929](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37699427929)
  passed independent verification. This remains the historical MVP candidate;
  the published baseline is recorded below.

## Configurability planning — 2026-10-13

The owner requests control of recipe unlock technologies, items, quantities,
production rates and manufacturing machines in the BepInEx configuration file,
with hand replication following the agreed native behavior. The file must be
usable in Notepad and end with an ID reference grouped by type. Reference-only
settings are permitted to support mod-manager lookup. Valid configurations must
be accepted; invalid values must receive specific nearest-valid corrections.

| Work / decision | Disposition | Basis / next gate |
| --- | --- | --- |
| CFG-D01: Reopen planning | Owner-directed; initial planning scope | Superseded maintenance deferral for this feature. Initially authorized documentation only; G0 below subsequently authorizes implementation. |
| Initial investigation and roadmap skeleton | Delivered; draft definitions superseded by refinement | Main `fa42c40bf1315d6a3e2268b9dee6160b1eda4ab9`; [CI run 38388888854](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38388888854) passed. Current definitions are in the [roadmap](planning/ROADMAP.md). |
| CFG-D02: Existing defaults and recipe identity | Approved at G0, 2026-10-13 | Preserve the accepted recipes as defaults and retain the six registered recipe IDs/selector positions while exposing their contents. |
| CFG-D03: Configuration contract | Approved at G0, 2026-10-13 | [Contract](planning/CONFIGURATION.md): file/ingredient layout, native completed-tech meaning, correction distance/ties/precedence, startup application, reference presentation and malformed-file boundary. Numeric/shape proofs remain CFG-I01 within the three allowed machine families; no invented balance restrictions. |
| CFG-D04: Selected-machine rate baseline | Superseded by CFG-D11, 2026-10-13 | The earlier selection used the chosen machine's speed. The owner now requires each family's basic-machine baseline, regardless of the configured tier ID. |
| CFG-D05: Hand replication | Owner-selected, 2026-10-13 | Retain the current family rule: assembler recipes allow handcrafting; other families do not. No independent handcraft toggle. |
| CFG-D06: Configuration changes and saves | Owner-selected, 2026-10-13 | Keep native loading behavior and document preparation, including emptying/clearing affected machines and cancelling queued handcrafts. No resource-preserving migration. |
| CFG-D07: Output shape | Owner-selected, 2026-10-13 | One output item type per recipe, with editable ID and count. |
| CFG-D08: Config editor target | Owner-selected, 2026-10-13 | Thunderstore Mod Manager, alongside Notepad. Actual manager presentation remains an owner observation; no substitute editor establishes it. |
| CFG-D09: Roadmap refinement | Owner-directed, 2026-10-13 | Investigate routine uncertainties now within the existing envelope; retain bounded investigation stories only for questions that exceed it or become rabbit holes. Add relative work weights, epic/story separation, explicit phase gates and executable closeout/handoff instructions. |
| First roadmap refinement | Delivered; scope superseded by CFG-D10/D11 | Main `4c69b1eb5a33ea715fa135448c26f2647e66aabb`; [CI run 38393118772](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38393118772) passed. The prior draft had ten stories and 32 relative work units. |
| CFG-D10: Allowed production machines | Owner-directed, 2026-10-13 | Only smelters, chemical plants and assemblers. Labs and all other families are outside this roadmap. Remove CFG-I02 and its dependent lab work; this is a scope decision, not a native incompatibility finding. |
| CFG-D11: Basic-machine rate baseline | Owner-directed, 2026-10-13 | Products/minute before proliferation in Arc Smelter, Chemical Plant or Assembling Machine Mk.I. A configured higher-tier ID in the same family does not change this baseline; native tier speed differences remain. Supersedes CFG-D04. |
| CFG-D12: Explicit story exclusions | Owner-directed, 2026-10-13 | Every story must state its out-of-scope work separately from its stop condition before the final draft is resubmitted. |
| CFG-D13: Native recursive crafting limits | Owner-selected, 2026-10-13 | "Preserve native recursive crafting limits; correct recipe and machine fields." Document cross-recipe raw-material/handcraft/queue arithmetic limits; do not add chain validation or runtime queue safeguards. Per-recipe field and machine-buffer corrections remain required. |
| Revised roadmap and contract / G0 | Owner-approved, 2026-10-13 | Owner: "I'm approving G0. Implement through to owner handoff please. Push to main when each story is done alongside a management docs update where appropriate." Three epics, nine stories, 29 relative work units; execute through G3, then wait for acceptance. |
| Current native reference | Adopted for CFG-N01 | DSP 0.10.35.29131, assembly SHA-256 `74ac6162cef8394a6021c8475132cae4df4f840871a99e63cc7d2c8297661bc0`. Deliberate bootstrap refresh passed after the earlier drift diagnosis; real-library compilation and binding validation passed. This is not in-game compatibility acceptance. |
| Implementation, gameplay acceptance and publication | Implementation through handoff authorized; acceptance/publication separate | Story pushes are authorized. HF-01 acceptance and the published baseline are unchanged; no version promotion or publication is included. |

| Story / gate | State | Evidence / next action |
| --- | --- | --- |
| CFG-I01 | Technically complete; delivery in this story commit | [Numeric/shape findings](implementation/CONFIGURATION-INVESTIGATION.md#numeric-and-shape-follow-up) establish per-recipe domains, duration/rate conversion, duplicate-row correction and the owner-selected recursive-arithmetic boundary. No game code was executed. |
| G1 | Passed within CFG-D13's boundary | Direct native consumers and correction examples are recorded. Cross-recipe/queue safety is explicitly retained as native behavior, not claimed as proven. |
| CFG-N01 | Delivered | Main `2a5ad341c37966c9de02d0d2f436a7b238ccbc75`; [CI run 38400259645](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38400259645) passed. Shared pipeline passed 15 behavior checks, package checks and 164 local links; real-reference build verified 73 external references and three Harmony targets. Its G0 dependency permits catalogue/lifecycle work independently of numeric proofs. |
| CFG-C01, C02, N02, N03, C03, D01, D02 | Approved, not started | Follow the roadmap's dependency order and per-story delivery rule. |
| G2, G3, G4 | Pending | Technical completion, verified handoff and owner acceptance remain separate. |

The [investigation](implementation/CONFIGURATION-INVESTIGATION.md) now maps machine
IDs/speeds, family filtering, startup timing, technology completion, bounded
handcraft recursion and native save/queue transitions. The brief numeric checks
exposed coupled consumers resolved by CFG-I01 within CFG-D13's boundary. The earlier lab findings are retained
as evidence, but CFG-D10 removes the need for further lab investigation. Native
extracts remain ignored.
Current-game compatibility and configurable runtime behavior still require the
owner's game observations; static evidence does not establish those results.

Initial draft review covered technical claims against source/assets, investigation coverage,
scope and document authority, then wording. It kept machine family, handcraft
eligibility and rate interpretation as separate questions; required the reference
to match the accepted ID domain; and left default preservation and save-transition
policy for owner review. The pending hotfix and published release were preserved.
Initial draft validation passed: PowerShell syntax, 158 local links, retained
concept hashes, source hygiene and diff whitespace. No local product build or
runtime test was performed for this documentation change.

The first refinement review checked the new facts against their native consumers, then
scope, dependencies, weights and handoff instructions against the completed MVP's
delivery standard. It removed routine investigation stories, put reference adoption
before the first configuration build, made C02's catalogue dependency explicit,
distinguished value correction from damaged
file syntax, and kept pending native bounds out of assumed implementation facts.
That draft's epic totals agreed with its ten story weights (32); gates added no work weight.
Repository validation passed with 162 local links, retained concept hashes,
PowerShell syntax, source hygiene and diff whitespace. Only planning/management
and evidence documents changed; product code, installed files and saves were untouched.

The scope revision removed the lab story and its dependencies, restricted the
machine reference and correction domain to the three allowed families, and applied
the basic-machine rate rule throughout the contract, implementation stories and
owner handoff. Each of the nine remaining stories now has a separate out-of-scope
statement and stop condition. Review checked the remaining dependencies, the
15 + 9 + 5 weight totals, unchanged default rates and the distinction between
machine choices and building items used as ingredients/outputs. No additional
native investigation or product change was needed for this revision.
Repository checks passed, including all 162 local links and diff whitespace.

## HF-01 maintenance hotfix — 2026-10-08

- **Failed implementation / owner rejection:** the owner reports that loading with
  the first fix immediately puts the game in sandbox mode. Candidate `0.9.16` is
  withdrawn. Its offline passes did not establish safe game-mode behavior; the
  catalogue-edit approach and its tests are reverted. The original
  [story](archive/2026-10-08-hf01-catalogue/STORY.md) and
  [evidence](archive/2026-10-08-hf01-catalogue/VALIDATION.md) are retained as history.
- **Steering decision HF-01-D02:** the owner directs use of the existing local
  patcher's empty-collection approach, replacing the agent's selective catalogue
  alternative. The referenced implementation initializes an empty dictionary in
  `AbnormalityLogic.InitDeterminators` and returns. Apply that behavior through the
  plugin's existing Harmony setup. All native detector creation is disabled;
  prototype catalogue, game mode and saved history are not modified.
- Correction state: offline and hosted validation passed; candidate `0.9.18` is
  handed off for owner validation. The shared build passed all 12 retained product checks and package tests.
  Real-library validation covered 62 external references, 65 shim members, three
  Harmony targets and the private dictionary binding. An authored local fixture
  using real Harmony confirmed factory suppression and valid tick/free behavior.
  All nine pre-hotfix method bodies match the published plugin after normalizing
  version text. [Current evidence and handoff](implementation/HF-01-VALIDATION.md).
- Corrected delivery: main source `429ed652a3713b66d48084f8f1a44551204aa0f8`;
  [CI run 37759891486](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37759891486),
  attempt 1, passed. Independently downloaded `0.9.18` matched both provider
  digests, all 96 committed source hashes, metadata, package contents and real
  references, including the private field. This is the selected candidate even
  if a later documentation-only run receives a higher build number.
  ZIP SHA-256: `1b92039f55b3720f7878d38a28489edf44eb7469ca4cfbebeabcf317c8e7f21c`.
  DLL SHA-256: `b96a02e88a3ae997363d4ef9d49f581a99a9ba75735ab3819d0611ce21efb7ea`.
  Owner in-game acceptance and publication remain pending.

The following records describe the rejected attempt, not current readiness:

- Authorization: owner requested a bounded investigation story and implementation
  through push to main, then owner testing of the CI artifact. The later instruction
  allows whole-system detector disconnection if simpler.
- Superseded decision HF-01-D01: disconnect only `ABN_ProtoData` catalogue registrations through the
  existing preload prefix, before recipe access. This is a comparably small native
  mechanism, leaves other detectors active and preserves existing history. No new
  Harmony target or runtime dependency is required.
- Local readiness: shared build and real-reference build passed without warnings;
  all 14 product checks and the package/repository checks passed. Both omitted and
  late-disconnection mutations were rejected by the ordering regression. Eight
  unchanged existing method bodies match the published plugin after normalizing
  its version text. See [historical evidence](archive/2026-10-08-hf01-catalogue/VALIDATION.md).
- Delivery: implementation and local evidence pushed to main in
  `0f329bbafdf992b537118233d9340ba61118f016`.
  [CI run 37758009481](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37758009481),
  attempt 1, passed. Downloaded candidate `0.9.16` matched both provider digests,
  all 96 committed source hashes, version metadata and package contents. Its DLL
  passed real-reference validation (65 references, 66 shim members, two hooks).
- Withdrawn candidate: **0.9.16**, build label `0.9.16.0f329bbafdf9`, from that run.
  ZIP SHA-256: `769fe3eea2cb845dba05a6c51db8c6490f1fd6a0014dd41ac5ebacc59501e792`.
  DLL SHA-256: `0b88fdc2bf1c50a0fb680ffe8f74de7ccb394463e0ecbf1a8ebf63f6e11be031`.
  Owner rejected its behavior; it is not a candidate for further acceptance.
  The published baseline below remains unchanged.
- Review: checked the story for bounded scope and owner gate, confirmed native
  registration/timing against the installed assembly, and reviewed source, tests
  and documentation for minimality and authority separation. Existing-history
  cleanup is explicitly outside this fix.

## Publication record — 2026-10-08

The owner reported publishing the latest artifact and requested maintenance mode
until they are ready to plan 1.0. In this conversation, that artifact is `0.9.14`
from source `edb43256911e93c76bb7450a6229d328dbf9d286`,
[CI run 37708171164](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37708171164),
attempt 1. Its downloaded CI package passed independent verification of both
provider digests, all 91 source hashes, version metadata, package contents and
real-library bindings.

- Package SHA-256: `a56a39154564ed54faddcf9048f06bf71f501d5cb565c069b77830db185e49dc`.
- Plugin SHA-256: `ea832bf3e2061b0691b7661f6555d3e248d54fed5598a2a6551f9bd61ecfd3d1`.
- Publication evidence: owner's report. Public download bytes have not been
  independently verified; the CI verification above is a separate observation.

That maintenance transition did not start a release, version promotion,
feedback-monitoring task or 1.0 work. Subsequent owner-authorized work is recorded
in the current state above.

## Pre-release repository work

- Package presentation and root README: owner approved on 2026-10-08. The supplied
  256x256 icon is unchanged. The player README passed Thunderstore's website
  Markdown preview; the local build passed product, package and repository checks.
  Delivered as `589c787`; [run 37706891094](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37706891094)
  passed. Downloaded `0.3.12` matched both provider digests, all 88 committed source
  hashes and the package contract; its DLL passed real-reference metadata checks.
- Repository cleanup: complete, delivered as `e2dc896`. Recipe definitions use
  named item/technology constants; source comments
  explain timing and lifecycle constraints. Tests are grouped by behavior, with
  repeated fixture assertions removed and save/registration regressions retained.
  The metadata checker is split by responsibility and C#/project formatting is
  consistent. Detailed investigation is archived; current docs link to source.
  The owner requested minor version 9 for this delivery; direct reference builds
  now also derive their default version from VERSION.
- Cleanup validation, 2026-10-08: shared build and real-reference compilation passed
  with no warnings; all 12 named product checks, 13 malformed-package cases, three
  malformed-version cases and three reference-rejection cases passed. The external
  ledger is unchanged (60 compiled references, two Harmony targets). Static IL
  comparison against `589c787`'s downloaded plugin found all nine existing method
  bodies identical after normalizing the startup version message. No new gameplay
  test was performed.
- Hosted cleanup verification: [run 37707934848](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37707934848)
  passed. Downloaded `0.9.13` matched both provider digests, all 91 committed source
  hashes, version metadata and package contents. Its DLL passed the real-reference
  check, including all 62 shim members. ZIP SHA-256:
  `e8b663991367d7c5d97b2ab104a2264b8e4277d7c460f873b27ad9b8139cb450`.
  This documentation-only closeout retains that verified source/artifact identity.

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
| REL-D02 | Owner-directed, 2026-10-08 | Promote MINOR to 9 with the repository cleanup delivery. This supersedes REL-D01 for new builds; patch numbering and publication authority are unchanged. |
| REL-D03 | Owner-reported and directed, 2026-10-08 | The owner published the latest artifact (`0.9.14`) and requested maintenance until ready to plan 1.0, likely after feedback. No 1.0 scope, deadline or ongoing monitoring is authorized. |

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
| Pre-release preparation | Closed, 2026-10-08 | Presentation and repository cleanup delivered; owner reports publishing 0.9.14 |
| Maintenance | Superseded as current phase, 2026-10-13 | Owner reopened configurability planning; completed MVP acceptance and release records remain unchanged |

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

At acceptance, the MVP roadmap was completed and the owner moved the project to
pre-release planning. The later publication and maintenance transition are recorded
above; neither changes the scope of the original gameplay observations.

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
