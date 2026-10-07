# MVP technical validation

[PROJECT](../PROJECT.md) owns story disposition, readiness, acceptance, and release.
This record contains dated checks and their limits. Product runtime results belong
to the eventual owner procedure, not inferred from these checks.

## MVP-02 — Plugin and external references

2026-10-08: net472 plugin compiled with zero warnings/errors against both the full
external shim set and real local assemblies. The checked-in
[declaration ledger](../../tools/ReferenceShims/reference-ledger.json) and
[reference baseline](../../tools/ReferenceShims/reference-baseline.json) identify
types, members, forwarding, assembly identities, hashes, and source versions.
The metadata validator checked 14 shim members and 12 actual external references
against the real baseline, including the shim-built DLL. BepInEx GUID, process
filter, and version are product-owned; no recipe hooks exist at this stage.

Local evidence: `artifacts/mvp02-real/reference-validation.json` and
`artifacts/reference-shims/reference-validation.json`. The latter inventories the
actual shim-built product's reference use. Neither check executes these assemblies.
The shared build now compiles the product and validates its declaration ledger;
its packaging stage remains labelled scaffold until MVP-06.

The first checker run exposed a Windows newline-normalization error in the ledger
comparison. Normalizing both sides repaired that tooling defect; real metadata
validation then passed. No baseline or signature was weakened to obtain a pass.

Altered-ledger, changed-reference-hash, and missing-real-reference cases were
rejected for their expected reasons by `tests/References.Tests.ps1`. The shared
build passed product compilation, ledger comparison, scaffold compilation/package
inspection, all 11 malformed-package and three version rejection cases, and
repository checks. This stage does not deliver an installable product ZIP.
