# 1.0 prerelease roadmap

[PROJECT](../PROJECT.md#prerelease-10-planning--2026-10-13) owns authorization,
work state and acceptance, including the owner's G0 approval.

## Scope

Refine the packaged README and short description, append a Simplified Chinese
summary with a brief notice near the start, and promote the version line to 1.0.
The stories produce the copy; the owner reviews the finished result at handoff.

Changes are limited to package copy, VERSION and accompanying documentation.
No product code, tests, dependencies or build-pipeline changes are included.
Game execution, installed files, manager profiles and saves remain outside the
agent's envelope. Publication is separate.

Use the [configuration contract](CONFIGURATION.md), [default recipes](MVP-RECIPES.md)
and accepted 0.9.33 behavior as the basis for the copy. The existing
[localization evidence](../implementation/NATIVE-INTEGRATION.md#configuration-language-evidence--2026-10-13)
confirms that names use the game's selected language at startup. English config
headings and the mod name remain unchanged. No Chinese startup or name-input
validation is required for this work.

## Epics and work weights

Epics group related stories. Weights are relative effort: **1** small, **2** bounded
editorial work, **3** several related reviews or delivery checks. Gates add no weight.

| Epic | Outcome | Stories | Weight |
| --- | --- | --- | ---: |
| E1 — Player information | Useful English and Simplified Chinese package copy | PRE-C01: 2; PRE-C02: 3 | 5 |
| E2 — 1.0 delivery | A verified 1.0.x package for owner review | PRE-V01: 1; PRE-D01: 3 | 4 |
| **Total** | Four stories | | **9** |

## Gates and execution

| Gate | Required result | Milestone |
| --- | --- | --- |
| **G0 — Approval** | Owner approves this roadmap and authorizes execution, including each story's management update and main push. | Work authorized |
| **G1 — Copy ready** | C01/C02 complete: English and Chinese copy agree and the README passes Thunderstore's website Markdown preview. | M1: package copy ready |
| **G2 — Handoff ready** | V01/D01 complete: one identified 1.0.x CI package is independently verified and ready for owner review. | M2: candidate delivered |
| **G3 — Acceptance** | Owner accepts the finished copy and candidate; record the result and archive completed work. | M3: roadmap closed |

After G0, follow **C01 → C02 → G1 → V01 → D01 → G2 → owner review**.
G1/G2 are agent checks, not additional permission requests. The short description
is written during the copy stories and reviewed with the rest of the package at
handoff; no advance approval of its wording is required.

Follow [AGENTS](../../AGENTS.md) and the [working methods](../WORKING-METHODS.md).
After each completed story, update PROJECT, review and commit the named changes,
push to main and verify that commit's CI result. Use existing validation procedures;
do not add tests for prose or roadmap completion. At handoff, stop for the owner's
review. After acceptance, archive the roadmap and dated delivery evidence, repair
links and push the documentation closeout. Do not publish the package.

## E1 — Player information

### PRE-C01 — Refine English package copy · weight 2

**Outcome:** Players can understand the mod and configure its recipes from clear,
concise instructions. **Depends on:** G0.

**Scope:** Review the English README and short manifest description. Use friendly,
restrained language; remove implementation explanations and correction algorithms.
Keep examples sparse and useful. Preserve the three English sections covering
recipes/configuration, Replicator/saves and installation, including:

- Default recipes, unlocks, rates and where to find them.
- Configurable choices, including output items within the six added recipes;
  batch counts versus basic-machine rates; names/IDs and optional ingredients.
- Config/reference locations, editing the whole value, restart/profile behavior,
  and a simple instruction to check the log when changes do not save as expected.
- Handcraft and save precautions, native tier/proliferation behavior, and both
  mod-manager installation and manual copying of the included BepInEx folder.

**Done/evidence:** Copy agrees with the current product contracts. Any example
uses existing fields and valid values. Instructions are complete without the
implementation detail; repository checks pass. C02 reviews the combined rendering.

**Out of scope:** Chinese prose, root README changes, version promotion and changes
to the features being described.

**Stop condition:** A necessary claim cannot be supported by the existing evidence.
Resolve that wording before completing the story.

### PRE-C02 — Add Simplified Chinese package copy · weight 3

**Outcome:** Chinese-speaking players can understand, install and configure the
mod from its package description and README. **Depends on:** C01.

**Scope:** Append a concise Simplified Chinese summary and add a brief notice/link
near the README's start. Finish the bilingual short description within the existing
250-character total limit. Use actual Chinese game names and familiar player terms;
keep mod names, configuration keys and paths intact.

Cover the mod's purpose, default recipes/unlocks/rates, customization, config and
reference use, restart, save/handcraft precautions and installation. Condense the
English explanation while preserving the actions a player needs to take.

**Done/evidence:** Check terminology against the native language evidence and
review the Chinese for natural phrasing. Compare both languages for meaning,
numbers, paths and completeness. Check the description length and UTF-8 encoding;
verify the complete README in Thunderstore's website Markdown preview, including
the notice/link, tables and code. Record the preview result for G1.

**Out of scope:** Translating config/reference headings or logs, mod renaming,
other languages, runtime language validation and external promotion.

**Stop condition:** A translation or rendering issue leaves an instruction unclear
or unusable. Resolve the affected text or layout before G1.

## E2 — 1.0 delivery

### PRE-V01 — Promote the version line · weight 1

**Outcome:** New builds consistently identify as 1.0.x. **Depends on:** G1.

**Scope:** Set VERSION to MAJOR=1 and MINOR=0. Retain the existing workflow-run patch
numbering and update the current version state in PROJECT.

**Done/evidence:** Run the existing [build and validation procedure](../LOCAL-DEVELOPMENT.md#build-and-checks).
Confirm package/plugin version metadata agrees with the build identity. Existing
checks pass; the diff contains only copy, version and documentation changes.

**Out of scope:** Versioning-scheme changes, historical artifact changes, tags and
publication.

**Stop condition:** Required checks fail or the generated versions disagree.
Report the failure before handing off the candidate.

### PRE-D01 — Verify the package and hand it off · weight 3

**Outcome:** The owner receives one verified 1.0.x package and a short copy-review
checklist. **Depends on:** V01 and successful CI for the final package source.

**Scope:** Download the package and evidence from that run and follow the existing
[hosted verification procedure](../BUILD-AND-PACKAGING.md#hosted-verification-and-publication-boundary).
Record source revision, run/attempt, version and ZIP/DLL hashes in PROJECT. Confirm
the packaged README and description are the reviewed text, with intact Chinese
characters and the expected version metadata.

**Done/evidence:** Package verification passes. Hand off the exact artifact with a
brief checklist for wording, presentation and completeness of usage/installation
instructions. Description wording is reviewed here alongside the README. No game
startup, name-input check or repeat of 0.9.33's gameplay validation is required.
Keep the selected candidate identified through subsequent documentation-only builds.

**Out of scope:** Runtime validation, further product work, publication and inferred
acceptance before the owner's review.

**Stop condition:** The downloaded artifact fails verification or differs from the
reviewed copy. Keep G2 open until the package issue is resolved.
