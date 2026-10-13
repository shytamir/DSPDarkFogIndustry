# Project steering

This is the sole authority for steering, decisions' dispositions, current and
historical work status, authorization, technical readiness, owner acceptance,
and publication. Other documents define contracts or evidence and link here.

## Current state

- Phase: **maintenance**, owner-directed on 2026-10-13 after acceptance and
  publication of **1.0.40**. No active story, roadmap or validation gate.
- Authorized scope: record acceptance/publication, archive the completed roadmap
  and handoff, and push this management closeout. Future planning or implementation
  requires owner steering; no ongoing monitoring is authorized. Game execution,
  installed-file/manager-profile changes and save access remain outside the
  agent's operational envelope. Completed work stays closed; the separate
  [HF-01 record](#hf-01-maintenance-hotfix--2026-10-08) is unchanged.
- Build version line: `1.0.x`, owner-directed for this prerelease; the patch remains the
  CI run number (0 for local builds). The historical MVP candidate remains `0.1.8`.
- Product implementation: MVP-01 through MVP-07 completed and accepted as the MVP;
  the MVP's M3 satisfied. Configurability and the owner-directed usability revision
  are completed and accepted; G4/M4 are satisfied.
- Planning: the [live roadmap](planning/ROADMAP.md) is an empty maintenance
  placeholder. The [1.0 prerelease roadmap](archive/2026-10-13-prerelease-1.0/ROADMAP.md),
  [configurability roadmap](archive/2026-10-13-configurability/ROADMAP.md)
  and [MVP roadmap](archive/2026-10-08-mvp/ROADMAP.md) are completed and archived. The
  [configuration contract](planning/CONFIGURATION.md) and
  [default recipes](planning/MVP-RECIPES.md) remain active contracts. No next release
  scope or schedule is selected.
- Owner acceptance: **1.0.40 accepted on 2026-10-13**; prerelease G3/M3 are satisfied.
  The [acceptance and publication record](#10-acceptance-and-publication--2026-10-13)
  preserves the owner's decision. Accepted 0.9.33 gameplay results and the earlier
  MVP acceptance remain historical evidence.
- Publication: **1.0.40 is published on Thunderstore**. The public ZIP matches the
  accepted CI artifact byte for byte; evidence is recorded below. Later CI builds
  do not supersede this published baseline.
- Git delivery: all four prerelease stories are on main through `2372c95`, with
  successful CI for each story. The package source remains `c79c3cf`; this management
  closeout does not produce a new selected release.

## Prerelease 1.0 planning — 2026-10-13

The owner requests a full roadmap draft before implementation: review the package
description and README for concise, approachable player guidance; add a Simplified
Chinese summary at the end with a brief notice near the start; and promote the
version to 1.0. The owner subsequently approved the revised roadmap and directed:
"Please see it through to owner acceptance, pushing to main with appropriate
management docs updates after each story is done." G0 authorizes execution through
handoff. The owner subsequently accepted 1.0.40 and requested archival and
maintenance mode; the closeout is recorded below.

| Decision / work | Disposition | Basis / boundary |
| --- | --- | --- |
| PRE-P01: Player-copy review | Approved at G0 | Remove irrelevant implementation explanations, keep useful examples sparse, and preserve complete usage/install instructions. Replace correction mechanics with simple log-based troubleshooting guidance. |
| PRE-P02: Chinese summary | Approved at G0 | Simplified Chinese, native game terminology and player-facing content. Brief notice near the start; summary appended at the end. The owner confirms English config text and the unchanged mod name are acceptable; full config localization is outside this work. |
| PRE-P03: Bilingual manifest description | Defined within the copy stories | Produce the short English/Simplified Chinese description during the work, within the existing 250-character total limit. The owner reviews the finished wording at handoff; it is not a separate G0 approval or prerequisite. |
| PRE-P04: Version 1.0 | Applied after G1 | MAJOR=1/MINOR=0; retain workflow-run patch numbering, producing 1.0.x. No run-counter reset, dependency/identity change, tag or publication. |
| PRE-P05: Existing configurability | Retained | The owner sees configurable outputs as a useful distinction. Keep all accepted controls and default recipes; explain that outputs can be changed within the six added recipes. No new slots or feature expansion. |
| PRE-P06: Localization verification | Static review complete | [Evidence](implementation/NATIVE-INTEGRATION.md#configuration-language-evidence--2026-10-13): current-language names are used in all three reference groups and name selectors; startup language selection precedes generation. Help/headings remain English. Chinese game strings were checked; no Chinese runtime session is claimed. |
| PRE-P07: Proportionate prerelease scope | Owner-directed revision | Remove the Chinese startup/name-input check; the owner will not perform it. Keep this a package-copy/version pass with a simple boundary against code changes. Remove generic implementation/investigation instructions and redundant approval dependencies. |
| Prerelease roadmap | Completed, accepted and archived, 2026-10-13 | Two epics, four stories, nine relative work units; definitions and gates in the [archived roadmap](archive/2026-10-13-prerelease-1.0/ROADMAP.md). Product code and tests remained unchanged. |
| G0 / execution | Owner-approved, 2026-10-13 | Approval covers this package-copy/version work, story-by-story main pushes and CI verification through the owner handoff. Description wording is decided during the work and reviewed at handoff. |

| Story / gate | State | Next action |
| --- | --- | --- |
| PRE-C01: English package copy | Delivered | Main `2a2fa1e`; [CI 38433971869](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38433971869) passed. English copy checked against the configuration/default-recipe contracts; one valid custom-output example retained. Repository checks passed. |
| PRE-C02: Simplified Chinese summary | Delivered | Main `5d2d80e`; [CI 38434862826](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38434862826) passed. Native terminology, bilingual meaning, UTF-8, 143-character description and website rendering checked; evidence below. |
| G1 / M1: Copy ready | Satisfied, 2026-10-13 | Both languages agree; notice, two tables and example render correctly in Thunderstore's website preview. |
| PRE-V01: Version promotion | Delivered | Main `c79c3cf`; [CI 38435202010](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38435202010) passed. MAJOR=1/MINOR=0; local build and real-reference checks passed. |
| PRE-D01: Verified candidate and handoff | Delivered | Main `2372c95`; [CI 38435808917](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38435808917) passed. Downloaded 1.0.40 independently verified; identity/evidence below and [archived owner checklist](archive/2026-10-13-prerelease-1.0/PRERELEASE-OWNER-REVIEW.md) retained. |
| G2 / M2: Owner review ready | Satisfied, 2026-10-13 | Exact 1.0.40 artifact independently verified and prepared for copy review. |
| G3 / M3: Acceptance and archival | Satisfied, 2026-10-13 | Owner explicitly accepted 1.0.40; roadmap and handoff archived, live roadmap reset, and maintenance mode restored. Publication verified separately below. |

### Planning basis and review

Read the [recently completed roadmap](archive/2026-10-13-configurability/ROADMAP.md)
for story weights, epic boundaries, phase gates, evidence and owner handoff rules.
The owner review covers the finished package copy and candidate. No game startup,
Chinese name-input check or repeat of the accepted 0.9.33 gameplay checks is required.
The static localization evidence remains valid within its recorded limits; the
omitted runtime check is not represented as a pass.

The supplied [DarkFogWorkshop listing](https://thunderstore.io/c/dyson-sphere-program/p/TTenYX/DarkFogWorkshop/)
and [README at inspected revision 2c5d73d](https://github.com/TTenYX/TTenYX-DSPMods/blob/2c5d73d8a8a44170a9b082272a4811d98f103c71/DarkFogWorkshop/README.md)
were reviewed only as public player-presentation context. They describe
logistics-station conversion and include an English summary. Their documentation
allows both matrix and Dark Fog material value settings, rather than matrix values
alone. No implementation was imported, interoperability tested, or explanation
for relative downloads or social reach established. Our roadmap does not depend
on competitive or popularity claims.

The initial draft was overdocumented for this work. The owner-directed revision
removes the runtime checklist, advance description approval and generic code-change,
investigation and repair procedures. Review retains the necessary story scopes,
weights, exclusions, package checks and owner handoff without reopening feature work.
Planning validation covers repository syntax, local documentation links, retained
concept hashes, source hygiene and diff whitespace. Product builds and gameplay
checks were not rerun for the document-only draft. G0 was subsequently approved.

### Package copy review — 2026-10-13

The English guidance and Chinese summary were checked for supported claims,
complete player actions and restrained wording. The bilingual description is
143 characters. Recipe, technology, machine and example names were compared with
the adopted native Chinese locale; `合成器` is the native Replicator label. No
product behavior changed and no Chinese runtime session is claimed.

The complete [packaged README](../packaging/README.md), SHA-256
`6d6ed39f02e61a86bc218247bced76eb9ff588f4c0065b534c87f9a404165a4a`,
passed [Thunderstore's Markdown preview](https://thunderstore.io/tools/markdown-preview/).
Both six-row tables, the example and Chinese text were inspected in the rendered
output. The renderer strips section anchors, so the opening notice directs readers
to the end without a broken link. Chinese punctuation was moved outside emphasis
markers after the preview exposed literal asterisks; the final rendered text has
none. A screenshot is retained locally at
`artifacts/prerelease/chinese-preview.png`. Repository checks and strict UTF-8
decoding of both copy files passed.

### Version validation — 2026-10-13

`./build.ps1` passed with SDK 10.0.302: shim compilation and ledger checks,
27 behavior checks, package inspection, 13 malformed-package cases, three malformed
version cases and repository checks. Local package/plugin version is `1.0.0`,
assembly/file version `1.0.0.0`; the diagnostic label identifies the dirty local
build. Evidence is under `artifacts/runs/c1451bd57cee46c99097aa43a95dba6c/`.
This is local validation, not the selected committed candidate.

The sandboxed restore initially exited without a reported build error. The same
standard build succeeded through the supported permission route; no source or
tool configuration workaround was made. Real-reference compilation then passed
with no warnings/errors, validating 95 external references, three declared Harmony
targets and 91 shim members against the adopted metadata. No game code was executed.
The prerelease diff is limited to package text, VERSION and documentation.

### Verified 1.0 candidate — 2026-10-13

- Package: **1.0.40**, `DSPDarkFogIndustry-1.0.40.c79c3cf2ff79.zip`.
- Source: `c79c3cf2ff797d352b0c91215cdeb6c2593c2c20`.
- [CI run 38435202010](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38435202010),
  run number 40, attempt 1, successful main build.
- [Package artifact 11832781564](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38435202010/artifacts/11832781564);
  separate evidence artifact `plugin-evidence-40-1`.
- ZIP SHA-256: `e2b7aefca221ad5ff3d805dd8b06ad6f98d5e89cc1328594aa88bf92f00f7e7e`.
- DLL SHA-256: `e8e1e8af3ff21459a287c2434b95464bbc9a7ebcce1a3d9d9a84da0542ed5e8a`.

Both actual downloads matched GitHub's artifact digests. All 110 source-file hashes
matched an export of that commit. Independent package inspection confirmed the
five required entries, source README/icon/license bytes, strict UTF-8 and metadata.
The packaged bilingual description and Chinese README text match the reviewed copy.
Package/plugin version is `1.0.40`, assembly/file version `1.0.40.0`, informational
version `1.0.40.c79c3cf2ff79`. The downloaded DLL passed real-metadata validation:
95 external references, three Harmony targets and 91 shim members; hosted and local
reference inventories agree.

The sandbox artifact request returned HTTP 401; authenticated download through the
supported permission route succeeded. Only the successful downloads were verified.
The package, provider metadata, exported source and independent verification report
are retained under ignored `artifacts/ci/38435202010/`.

Final scope review confirmed no changes to product code, tests, tools, dependencies,
pipeline or icon. No game was launched, installed file/profile changed, or save
accessed. At handoff, G3 remained open for the owner's review of the finished copy
and candidate; no repeat gameplay or Chinese runtime validation was requested.
The subsequent acceptance and publication are recorded below. Later documentation
builds do not supersede 1.0.40.

### 1.0 acceptance and publication — 2026-10-13

The owner stated: "Looks good, I accept 1.0.40," reported publishing it on
Thunderstore, and requested roadmap archival, a management push and maintenance
mode. This accepts the finished copy and identified candidate; it does not claim
additional gameplay or Chinese runtime checks.

The [published listing](https://thunderstore.io/c/dyson-sphere-program/p/DSPDarkFogIndustry/DSPDarkFogIndustry/)
shows version 1.0.40. Its public download was retrieved independently and has
SHA-256 `e2b7aefca221ad5ff3d805dd8b06ad6f98d5e89cc1328594aa88bf92f00f7e7e`,
identical to the accepted CI package above. Package inspection against the exact
exported source and build evidence passed, including all five entries and DLL
identity. The public ZIP is retained under `artifacts/releases/1.0.40/`.
An initial comparison with the working checkout found only its CRLF-normalized
LICENSE; verification against the actual LF source export resolved that mismatch.
No package or source changes were needed.

The completed roadmap and original blank review checklist are archived under
`docs/archive/2026-10-13-prerelease-1.0/`. The live roadmap has no active work.
Current recipe/configuration and integration contracts remain in place. The owner
also requested Discord announcement copy; this authorizes drafting, not posting.

## Configurability — 2026-10-13

The owner requests control of recipe unlock technologies, items, quantities,
production rates and manufacturing machines in the BepInEx configuration file,
with hand replication following the agreed native behavior. The file must be
usable in Notepad. The original embedded ID reference and nearest-ID selection
are superseded by CFG-D14/D15 below. Quantity/rate corrections remain unchanged.

| Work / decision | Disposition | Basis / next gate |
| --- | --- | --- |
| CFG-D01: Reopen planning | Owner-directed; initial planning scope | Superseded maintenance deferral for this feature. Initially authorized documentation only; G0 below subsequently authorizes implementation. |
| Initial investigation and roadmap skeleton | Delivered; draft definitions superseded by refinement | Main `fa42c40bf1315d6a3e2268b9dee6160b1eda4ab9`; [CI run 38388888854](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38388888854) passed. Final definitions are in the [archived roadmap](archive/2026-10-13-configurability/ROADMAP.md). |
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
| CFG-D14: Text-editor usability | Owner-directed, 2026-10-13 | The approximately 3,500-line config is impractical. Keep generated content below 200 lines; target core controls in the first 60, with flexibility to avoid overengineering. Move optional inputs 3–6 later and the catalogue to a separate text reference. Dropdowns are out of scope. Keep all output controls and values pending a later decision. |
| CFG-D15: Name inputs | Owner-directed, 2026-10-13 | Include name inputs; "Just replace invalid inputs with defaults and don't make a guesser." Selectors accept exact current-language names ignoring case/edge whitespace, or IDs. Invalid/ambiguous/ineligible selections use their field defaults. Stored IDs with readable labels preserve identity across language changes. No cross-language alias system or fuzzy matching. |
| Configuration layout review | Owner-accepted, 2026-10-13 | Owner: "I checked the new format and it's perfectly fine as it is." This initial acceptance covered presentation; the full checklist and G4 were subsequently completed below. The checklist was checked against the new section/value format and clarified before gameplay validation. |
| Revised roadmap and contract / G0 | Owner-approved, 2026-10-13 | Owner: "I'm approving G0. Implement through to owner handoff please. Push to main when each story is done alongside a management docs update where appropriate." Three epics, nine stories, 29 relative work units; execute through G3, then wait for acceptance. |
| Current native reference | Adopted for CFG-N01 | DSP 0.10.35.29131, assembly SHA-256 `74ac6162cef8394a6021c8475132cae4df4f840871a99e63cc7d2c8297661bc0`. Deliberate bootstrap refresh passed after the earlier drift diagnosis; real-library compilation and binding validation passed. This is not in-game compatibility acceptance. |
| Implementation, gameplay acceptance and publication | Configurability completed and owner-accepted, 2026-10-13 | The owner completed every checklist check and explicitly accepted 0.9.33, closing the roadmap. Prerelease planning is next. The separate HF-01 record and published baseline are unchanged; no version promotion or publication is included. |

| Story / gate | State | Evidence / next action |
| --- | --- | --- |
| CFG-I01 | Delivered | Main `5b2303a638dbe234deb76d69b18e44b589f3f78a`; [CI run 38402139496](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38402139496) passed. [Numeric/shape findings](archive/2026-10-13-configurability/INVESTIGATION.md#numeric-and-shape-follow-up) establish per-recipe domains, duration/rate conversion, duplicate-row correction and the owner-selected recursive-arithmetic boundary. No game code was executed. |
| G1 | Passed within CFG-D13's boundary | Direct native consumers and correction examples are recorded. Cross-recipe/queue safety is explicitly retained as native behavior, not claimed as proven. |
| CFG-N01 | Delivered | Main `2a5ad341c37966c9de02d0d2f436a7b238ccbc75`; [CI run 38400259645](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38400259645) passed. Shared pipeline passed 15 behavior checks, package checks and 164 local links; real-reference build verified 73 external references and three Harmony targets. Its G0 dependency permits catalogue/lifecycle work independently of numeric proofs. |
| CFG-C01 | Delivered | Main `df0c3254e0600efb73092097b855569c4e4da236`; [CI run 38402800038](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38402800038) passed. Six integer-field sections, startup snapshots and persistence use public BepInEx APIs. Pinned-library tests cover defaults, edits, missing/unknown keys, comments, ordering, restart and save failure. Shared pipeline passed 18 behavior checks; real-reference validation passed. |
| CFG-C02 | Delivered | Main `0136159d9f57831d05ab8cff5b03176402845af4`; [CI run 38403744264](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38403744264) passed. Corrections preserve raw numeric distance and valid unrelated fields; actual BepInEx round trips settle after one save. Shared pipeline passed 21 behavior checks; real metadata verified 88 external references and three hooks. CFG-D13 remains the recursive-crafting boundary. |
| CFG-N02 | Delivered | Main `8eeb5ac8a413f3936b746492803c503ada038e96`; [CI run 38404231424](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38404231424) passed. Corrected contents and gates populate native records. Shared gates accumulate; import uses native full completion and preserves saved unlocks. Shared pipeline passed 22 checks; real metadata verified 86 references and three hooks. |
| CFG-N03 | Delivered | Main `2a05f5e89d302ff1062325aa25631e80c38e7384`; [CI run 38404589354](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38404589354) passed. Emitted-record tests preserve defaults and verify custom batches, family changes, tier-ID invariance, native ceilings and lower-rate ties. Shared pipeline passed 24 checks. [Production evidence](implementation/NATIVE-INTEGRATION.md#configured-production-checks) gives owner expectations. |
| CFG-C03 | Delivered | Main `6fde23f0b6a5a929864c7897bfc0b051d18e1fb8`; [CI run 38405244674](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38405244674) passed. References use the validator's catalogue and native translation/metadata; edits are inert. Generated text and round trips were reviewed. Shared pipeline passed 25 checks; real metadata verified 93 references and three hooks. Actual editor interaction remains an owner observation. |
| G2 | Passed, including CFG-U01 | All implementation stories are on main with successful CI. Defaults, custom settings, native integration and file persistence pass the offline checks; no game validation is inferred. |
| CFG-D01 | Delivered and independently verified | Candidate source `5c0cdda26e5f08d378795c604afc83787a046617`; [CI run 38405672271](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38405672271), attempt 1, passed. Downloaded 0.9.30 matched both provider digests, all 107 committed source hashes, package contents and version metadata. Its DLL passed real-reference validation for 93 references and three Harmony targets. Details below. |
| CFG-D02 | Delivered; owner checklist completed | [Archived procedure](archive/2026-10-13-configurability/OWNER-VALIDATION.md) identifies the candidate, editing checks, defaults/custom rates, shared gates, corrections, native behavior and prepared save changes. Final checklist revision: `262d70ad6b47d607488bc70385b40e253f44b29b`. The owner reports completing it with no skipped tests; results are recorded below. |
| G3 | Passed for 0.9.33 | The original nine stories and CFG-U01 were delivered through technical validation and the independently verified candidate below. No game launch, save access, installed-file change or manager interaction was performed by the agent. |
| G4 / M4 | Owner-accepted; completed and archived, 2026-10-13 | Explicit acceptance of 0.9.33 after the full checklist, with no significant regression or errors observed. The roadmap, owner procedure and investigation are archived; prerelease planning is next. |
| CFG-U01 | Delivered and owner-accepted | Main `7f2598debeabf303842fdbab339b041f350529d4`; [CI run 38419021406](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38419021406) passed. [Bounded revision](archive/2026-10-13-configurability/ROADMAP.md#cfg-u01--text-editor-usability-after-owner-review). Generated config: 138 lines, core controls through line 65. All 27 behavior checks and the shared build/package/repository checks pass. Real/shim validation verifies 95 external references and three unchanged Harmony targets. Downloaded 0.9.33 passed the checks below and is included in the owner's acceptance. |

### Accepted configurability artifact

- **0.9.33**, label `0.9.33.7f2598debeab`, from CFG-U01's run above, attempt 1.
  Source: `7f2598debeabf303842fdbab339b041f350529d4`. Subsequent documentation-only
  builds do not replace or relabel this accepted package.
- ZIP SHA-256: `6289e433e263ed44ebb5b8af6ba35d03c5590f8195f072555d0a0bdf56a6e408`.
- DLL SHA-256: `8c061e621f154a7ef39c89e7e79709d195b50e564926c0513680133b02b6a4a5`.
- Independent download inspection verified both provider digests, all 109 source
  hashes against the committed export, package contents/metadata and the downloaded
  DLL against real external metadata. Evidence: `artifacts/ci/38419021406/`.
- Offline BepInEx checks verify the 138-line layout, migration of inputs 3–6,
  exact names/IDs, default fallback, stable corrected saves and the separate
  reference. No production defaults or configurable output controls were removed.

### Owner acceptance record — 2026-10-13

Source: the owner's report in this task for the 0.9.33 handoff above. The owner
completed the entire [checklist](archive/2026-10-13-configurability/OWNER-VALIDATION.md),
with no test skipped, and reported:

- No significant regression or errors observed.
- Good logging behavior.
- All expected behavior present without variation.

The owner supplied these correction log excerpts:

```text
[Warning:DSP Dark Fog Industry] [02 - Recipe 402 - Dark Fog Matrix] Input2ItemId: 'fractal silico' -> 5111; unknown, ambiguous or ineligible selection; using the documented default.
[Warning:DSP Dark Fog Industry] [Extra inputs - 02 - Recipe 402 - Dark Fog Matrix] Input3Count: '8' -> 0; unused ingredient row.
[Warning:DSP Dark Fog Industry] [02 - Recipe 402 - Dark Fog Matrix] OutputCount: 'nonsense' -> 1; not numeric; documented default followed by representable output quantity and integer rate.
[Warning:DSP Dark Fog Industry] [02 - Recipe 402 - Dark Fog Matrix] OutputPerMinute: '61' -> 60; attainable basic-machine products/minute.
```

The owner explicitly accepted this version and completed the roadmap. G4/M4 are
satisfied, including CFG-U01. These are owner-reported observations; the agent did
not run the game or independently capture its logs. The archived checklist is
preserved as issued, with this report recording the results.

At acceptance, the project moved to prerelease planning without selecting its
scope, release version or schedule. The owner's subsequent direction is recorded
above; neither decision changes publication.

### Earlier configurability candidate — 0.9.30

- Version **0.9.30**, build label `0.9.30.5c0cdda26e5f`, from the D01 run above.
  This was the original handoff artifact, now superseded by 0.9.33 above.
- Package SHA-256: `d9556239e5c4219f9397144df1665d03be7eb0a2dd13a82f384a0351a41c6243`.
- Plugin SHA-256: `fd53b689928d8f291cdf53d295b9f803aea14a17d379a3652f723c0c5838c14e`.
- Retained download/evidence: `artifacts/ci/38405672271/`, including source export,
  provider metadata, independent package inspection and downloaded-DLL real binding report.
- Shared checks passed: 25 behavior checks, 13 malformed-package cases, three
  malformed-version cases, source/document hygiene; local negative reference checks
  rejected altered ledgers, reference drift and missing references. Builds reported
  no compiler warnings. The installable ZIP contains only the five contracted files.
- Final review covered native startup ordering, correction precedence and arithmetic,
  shared gates/import isolation, readable BepInEx output, player wording, tests and
  story/delivery records. Recursive crafting/queue arithmetic remains native under
  CFG-D13. Gameplay and editor interaction were unobserved at that handoff;
  subsequent owner validation and acceptance apply to 0.9.33 above.

The [archived investigation](archive/2026-10-13-configurability/INVESTIGATION.md) maps machine
IDs/speeds, family filtering, startup timing, technology completion, bounded
handcraft recursion and native save/queue transitions. The brief numeric checks
exposed coupled consumers resolved by CFG-I01 within CFG-D13's boundary. The earlier lab findings are retained
as evidence, but CFG-D10 removes the need for further lab investigation. Native
extracts remain ignored.
The owner's subsequent game observations are recorded above; static evidence
alone did not establish those results.

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

Historical Git delivery: preparation and its evidence closeout reached main at
`db3d8ed85df251a57e817881a30d61bdad7938e0`; MVP-01 is `89d40d5`, MVP-02 is `2ebab16`,
MVP-03 is `bfac10d`, MVP-04 is `0be4006`, MVP-05 is `5a8e01f`;
MVP-06 source/closeout are `1fabb25` / `05b1130`; MVP-07 is `cb9456d`.
Each story included its management update and main push. The historical MVP
candidate source is `1fabb2543d56d90d5707863c234c18c33ba93f02`; downloaded package
`0.1.8` from [run 37699427929](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37699427929)
passed independent verification. The current published baseline is recorded above.

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
