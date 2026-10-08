# Prototype detector hotfix: evidence and validation

For scope see [HF-01](STORY.md); for current state
and acceptance see [PROJECT](../../PROJECT.md).

## Evidence basis

Inspected on 2026-10-08 against DSP 0.10.35.29104, `Assembly-CSharp.dll` SHA-256
`6c122e5443e6843979b4064050dfcb5e0d75577a0b64f6ae4111290238b33c12`.
The owner's `_autosave_0.analysis.json` has SHA-256
`a7f885bfcbd1feecd5accac9d3bebf12a60d335d50e6af57af37a264ed043a13`.
It records 34 findings: 17 technology-signature entries
(category 3, evidence 2), and 17 recipe-signature entries (category 4, evidence 3).
Its offline checks report no current-state findings; it explicitly cannot evaluate
runtime prototype signatures. Its `changes_planned` state is not evidence that a
save was repaired. Only this supplied report was read, not its source save.

The owner-referenced `DSPBlueprintFixer/tools/save-normalizer/INVESTIGATION.md`
(2026-10-03) describes the same assembly hash and categories. It is supporting
evidence, not product code or task authority. Current static inspection confirms:

- `AbnormalityLogic.InitDeterminators` creates its dictionary, then instantiates
  only catalogue rows with a nonempty `DeterminatorName`.
- `LDB.abnormalities` uses the ordinary cached `LoadTable` accessor, so later
  accesses retain the edited registrations rather than reloading the catalogue.
- `ABN_ProtoData` binds to item, technology, recipe, vegetation or vein tables and
  subscribes `CheckProto` to game-begin and before-save events. A signature mismatch
  writes an abnormality record. The base has no independent prototype check.
- `VFPreload.InvokeOnLoad` precedes native preload and the menu demo game. The mod's
  existing prefix runs before that callback, so detector registration can be
  disconnected before recipes or technology unlock arrays are changed.

The source owns the fix: [PrototypeDetection](https://github.com/shytamir/DSPDarkFogIndustry/blob/0f329bbafdf992b537118233d9340ba61118f016/src/DSPDarkFogIndustry/PrototypeDetection.cs)
and [RegistrationPatch](https://github.com/shytamir/DSPDarkFogIndustry/blob/0f329bbafdf992b537118233d9340ba61118f016/src/DSPDarkFogIndustry/RegistrationPatch.cs).
It leaves the catalogue rows/IDs available for interpreting old records and leaves
non-prototype detector names intact. It does not erase existing history, bypass
eligibility gates, modify installed assemblies or add a save repair path.

## Validation procedure

Run the shared build, shim and real-reference checks per [local development](../../LOCAL-DEVELOPMENT.md).
Verify the actual hosted artifact per [the package contract](../../BUILD-AND-PACKAGING.md#hosted-verification-and-publication-boundary).
Automated tests use authored data and do not execute game assemblies.

## Local results — 2026-10-08

- `./build.ps1`: all 14 named product checks passed, including the two new detector
  regressions; valid package, 13 malformed packages, three malformed versions and
  repository checks passed. Build completed with no warnings or errors.
- `Build-Plugin.ps1 -ReferenceMode Shims -OutputPath artifacts/reference-shims
  -UpdateLedger`, followed by `-ReferenceMode Real -OutputPath artifacts/product-real`:
  declaration inventory and real-library compilation passed. The checker resolved
  65 compiled external references, 66 shim members and the two existing Harmony
  targets. The library baseline and target ledger are unchanged.
- Direct calls to the linked preload callback verify disconnection before recipe
  access and idempotent registration. Independent category fixtures verify that
  unrelated detector names and category identities survive repeated calls. The
  test attributes do not simulate Harmony patch dispatch.
- In ignored scratch copies, removing disconnection and moving it after recipe
  access both failed with the intended ordering-regression message. Product source
  was not mutated for these checks. Evidence: `artifacts/hf01-mutations/`.
- Static IL comparison with published candidate `0.9.14` from run `37708171164`
  found all eight existing method bodies other than the changed preload prefix
  identical after normalizing startup version text. Recipe definitions,
  registration, save reconciliation and existing patch installation are unchanged.
  Evidence: `artifacts/hf01-il-comparison.json`.
- Local candidate: `artifacts/runs/57263668672e4176ba00864e513cf547/`, version
  `0.9.0.b6a5d945d2f4.dirty`; ZIP SHA-256
  `b5c1d25eaef25f11cc4ebcf4f9dfc6d6bccecafbe4ed6532e4b35f652d1f08df`.
  This is local validation evidence, not the committed owner-test candidate.

No game process, save, installed plugin or game assembly was changed or executed.
The supplied analysis and referenced investigation were read-only inputs.

## Hosted results — 2026-10-08

[Run 37758009481](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37758009481),
attempt 1, built `0.9.16` from `0f329bbafdf992b537118233d9340ba61118f016` and passed
the shared checks and both artifact uploads. Independent download verification
matched GitHub's two artifact digests and all 96 source hashes against an export
of that exact commit. Package paths, manifest, plugin/file/informational versions,
README, icon, license and DLL identity matched the build reports. The downloaded
DLL resolved all 65 references and two hooks against the pinned real libraries;
all 66 declared shim members matched. No upstream binary is packaged.

Retained downloads and reports: `artifacts/ci/37758009481/`. PROJECT records the
selected candidate and hashes; the local dirty build is not the handoff artifact.

## Owner check

Use the exact downloaded CI candidate identified in PROJECT. Restart the game
with that candidate installed. In a new save or a backed-up save known to have
no recorded abnormal findings, confirm the recipes still unlock and produce as
before. Save, reload, and save again; check that no new prototype-data findings
are recorded. Include an autosave (the supplied report shows repeated save-time
findings). Report the candidate version, starting history state and observations.

An already flagged save can remain flagged: this fix deliberately preserves old
history. To test such a save, compare the count/ticks of prototype findings before
and after instead of expecting its abnormal status to disappear. Other native
detectors remain active. Offline validation is not an in-game or online-eligibility
claim; owner acceptance and publication are separate.
