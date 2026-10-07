# Repository preparation evidence - 2026-10-07

[PROJECT.md](../../PROJECT.md) owns disposition, readiness, acceptance, and next work.
This record describes the technical evidence and its limits; it is not a roadmap.

## Source and scope

Starting branch `main`, revision `e10cc6dec4cecf060ef214c4940f86bccb822a0a`.
Initial checkout was clean with only README and Apache-2.0 LICENSE. A read-only
remote query returned the same main revision. Preparation changes were local and
uncommitted when these checks ran; generated evidence marks that dirty state.

The owner authorized structure, governance, bootstrap, packaging, and build
pipelines so planning could begin. Six local reference repositories were consulted
for conventions; none supplied product code, identifiers, architecture, or scope.
Their AGENTS files were evaluated as content, not adopted as task instructions.
The existing LICENSE was preserved.

## Concept intake

The [source record](../../concept/source.json) identifies the supplied ZIP and five
retained files. Import verified all 28 bundled file checksums. Retained concept/data
files preserve their exact bytes. Native source excerpts and the complete bundle
remain under ignored `.local/concept/`.

After source review, the bundle's standard-library verifier ran using the available
Python runtime. Its assertions passed: 77 selected combat technologies, 116 nodes
in their prerequisite closure, the six material records, five direct Fog-consuming
recipes, five Matrix-consuming technologies, and the recorded energy-weapon science
tiers. Bundle hashes and the earlier eight-cluster sample checks also passed.
This verifies the supplied subset's internal assertions, not fresh catalogue-wide
completeness or gameplay behavior.

## Local environment

- PowerShell 7.6.5; .NET SDK 10.0.302; ILSpyCmd 11.1.0.9782.
- Bootstrap recorded three assembly identities without executing game code.
- Native assembly SHA-256:
  `6c122e5443e6843979b4064050dfcb5e0d75577a0b64f6ae4111290238b33c12`, matching
  the supplied DSP 0.10.35.29104 identity.
- Session activation passed. A targeted `RecipeProto` decompilation populated
  the ignored hash-keyed inspection cache and exposed the requested type/method.
- The sandboxed .NET NuGet client initially failed despite direct HTTPS success.
  Scoped desktop-context tool installation and package restore succeeded without
  changing pins, certificate checks, audit settings, or warning policy. Bootstrap
  then completed with the installed tool. The build required the same context.

This establishes tooling readiness for static research; it is not plugin runtime
readiness or proof that a future dependency works with the game.

## Build and package

`./build.ps1` compiled the non-plugin fixture with zero warnings/errors. Local
version was `0.1.0`, assembly/file version `0.1.0.0`, diagnostic label
`0.1.0.e10cc6dec4ce.dirty`. Locked package restore succeeded.

The ZIP inspection checked exact layout, source text/icon identity, UTF-8,
manifest/version, decoded 256x256 PNG, compiled payload hash and timestamp.
The placeholder icon was visually inspected. Eleven malformed-package fixtures
and three malformed-VERSION cases were all rejected for their expected reasons.
PowerShell parsing, local documentation links, retained concept hashes, source
hygiene, and tracked diff whitespace checks passed.

First passing local package SHA-256:
`0bee5544942b45b8ecd4e06c39e0743094b0496afb515fa625c5fc37117eb846`.
Its run folder is `artifacts/runs/20f4dcc22129446fa80ac2786278efff/`, containing
the ZIP, `build-info.json` source inventory and DLL hash, and
`package-inspection.json`. This identifies that local run, not a future rebuild
or a release. Subsequent documentation closeout does not change those bytes.

A separate `-BuildNumber 7` run with workflow attempt `2` also passed. It verified
nonzero version propagation (`0.1.7` / `0.1.7.0`), the three GitHub output fields,
and the repeated package checks against that different compiled identity. Evidence
is under `artifacts/runs/ba2f687fa56445ff8a40fc931848f681/`; ZIP SHA-256 is
`10379efdbbf514067db2199f4495f1ac0367015a834908e094e3ad17f1ceacb1`.
The resulting build record retained run attempt `2`. This locally exercises the
workflow handoff without asserting a hosted run.

## Hosted configuration

The workflow uses the same build entry point and uploads a scaffold ZIP plus
separate maintainer evidence. Action commit pins were resolved from the official
repositories. `actionlint` 1.7.12 reported no workflow errors; its Windows download
was verified against the official release digest
`6e7241b51e6817ea6a047693d8e6fed13b31819c9a0dd6c5a726e1592d22f6e9`.
This was a local workflow check, not a GitHub-hosted run. The temporary validator
is kept only in ignored `.local/actionlint/`.

No product plugin was compiled or run. No game launch, save access, deployment,
owner gameplay validation, commit/push, tag, or public release occurred.
