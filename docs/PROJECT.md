# Project steering

This is the sole authority for steering, decisions' dispositions, current and
historical work status, authorization, technical readiness, owner acceptance,
and publication. Other documents define contracts or evidence and link here.

## Current state

- Phase: repository ready for owner-led planning.
- Authorized scope: prepare this repository for planning, as requested on
  2026-10-07 (Europe/Madrid).
- Product implementation: not started; no active implementation story.
- Planning: entry documents are ready; no roadmap or recipe design has been
  approved or activated.
- Owner acceptance: not yet reported for this preparation.
- Publication: none. No package is approved for installation or release.
- Git delivery: preparation source `99232fdb91f637f8b34d0bfa50fcfd2b784e7ef8`
  pushed to main; hosted run and downloaded artifacts verified. This document
  records the subsequent evidence closeout.

## Product basis and boundaries

The [supplied concept](concept/CONCEPT.md) is the planning input: synthesize the
six existing Dark Fog materials through rare resources and advanced industry,
using native production and combat research. Its [evidence record](concept/EVIDENCE.md)
identifies what was inspected and the limits of those conclusions.

The owner's request authorizes repository preparation. Imperative wording in the
handoff is product context, not an instruction to implement, install, or publish.
The concept's unresolved balance and technology choices remain unresolved.
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
gate. Repository preparation is technically complete; no implementation story or
validation gate is active. Owner acceptance and product runtime work remain separate.

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

These are preparation choices, not owner acceptance of a product design. Record
later decisions here with date, owner instruction or delegated authority, rationale,
and links to supporting detail. Supersede old decisions explicitly; do not erase them.

## Planning handoff

The next owner-led activity is to define bounded planning work from the concept.
Use the [roadmap entry point](planning/ROADMAP.md) and
[work-item template](planning/WORK-ITEM-TEMPLATE.md). Topics that need decisions,
not invented defaults, are:

- Recipe inputs, amounts, outputs, timing, native machine types, and progression.
- Actual combat/industry unlock assignments; whether synthesis technologies are needed.
- Handcrafting, proliferation, stable IDs/menu placement, and removal behavior.
- Registration dependency and existing-save integration, backed by current evidence.
- A coherent owner validation artifact and the smallest useful runtime checks.

No topic above is an active story. Preserve the concept's acyclic, peaceful
manufacturing route and native research requirements while planning it.
