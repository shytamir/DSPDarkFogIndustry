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

[Hosted run 37697277754](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37697277754)
passed for MVP-02 source `2ebab16011f0e8a8ce6849aac9319eb063761ced`.
It compiles the real product source against shims; its ZIP is still the scaffold.

## MVP-03 — Registration

2026-10-08: 50 assertions passed against the actual mod-owned definition and
registration source, with small in-memory data stores. Checks cover the six output/
input/gate/grid/handcraft/name/icon contracts, preservation of native records and
awards, one lookup rebuild request, repeated callback, a new table, and late ID,
grid, input, output, and gate failures before any mutation. Fakes do not implement
native preload, rendering, crafting, or discovery.

Both shim and real-reference builds passed without warnings/errors. Static real
metadata checks passed for 56 shim members, 52 external references, and the exact
private zero-argument `VFPreload.InvokeOnLoad` Harmony target, including the
shim-built DLL. Evidence is in `artifacts/mvp03-real/reference-validation.json`
and the updated `artifacts/reference-shims/reference-validation.json`.

Native source trace: InvokeOnLoad precedes preload; ProtoSet.OnAfterDeserialize
rebuilds lookup; ItemProto.FindRecipes computes item links and recursively resolves
ingredient recipe data even when an ingredient appears later in the item array;
RecipeProto.Preload derives icons/productivity/preTech; technology and execution
initialization follow. No late cache repair or private-field injection is added.
Actual Unity display/cache behavior remains a final owner observation.

[Hosted run 37697714260](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37697714260)
passed for MVP-03 source `bfac10d776675ee0fe2b22005f79802131fea8d6`.

## MVP-04 — Native production and progression

2026-10-08: product checks increased to 99 passing assertions. New checks derive
all six baseline rates and every higher-tier family rate from the actual emitted
recipes and independently captured native speed/time facts. They check machine
families, positive durations, the two ship-input acceleration-only cases, the
acyclic synthesis order, and supply of all five native discovery materials and
their Matrix research ingredient. `tests/ProductChecks/native-rules.json` retains
the exact fact inputs and their source hashes, including unaltered downstream
cost/prerequisite records. No native production simulation is claimed.

The [dependency witness](progression-witness.json) retains the constructive native
route found in MVP-01. Static review confirmed ordinary recipe icon fallback matches
native recipes, handcraft timing remains native, discovery can precede physical
production, and no mod write changes item Productive flags, research costs, combat,
mining, production statistics, or machine execution. No additional production patch
was needed. Runtime rates, proliferation effects/power, statistics, discovery,
research, and combat coexistence remain owner observations.

[Hosted run 37698031128](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/37698031128)
passed for MVP-04 source `0be4006e58804244b3e0f773c3ac0869956dbc67`.

## MVP-05 — Save reconciliation

2026-10-08: 487 assertions passed, including all 64 combinations of the six gates,
preview no-ops, no/partial/all unlocks, previously unlocked recipes, repeated import,
session changes, later research, and preservation of unrelated research/recipes.
Only the six missing rightful recipes are awarded; no whole technology is replayed.
The native gate list still handles ordinary new research.

Both real and shim builds passed without warnings/errors. Metadata validation of
both DLLs passed for 62 shim members, 60 external references and two Harmony targets.
The declaration ledger now includes generic constraints, type/method modifiers and
enum constants; the separate patch-target ledger covers full signatures and argument
names. Negative checks still reject changed ledgers/baselines and absent references.
Evidence: `artifacts/mvp05-real/reference-validation.json` and
`artifacts/reference-shims/reference-validation.json`.

The first target check correctly rejected the assumed one-argument Import signature:
the real method includes `bool isPreview`. The ledger was corrected from native
metadata and the product explicitly skips previews. No baseline check was bypassed.
The owner-specified GUID is `dark-fog-industry`.

Native saved machine IDs remain stable; unknown recipe imports can retain buffers
without execution data. The [removal boundary](NATIVE-INTEGRATION.md#save-and-removal-boundary)
therefore requires keeping the plugin for synthesis saves, or restoring an untouched
pre-mod checkpoint after removal. Real save/reload and rollback are unrun owner
observations; these tests neither execute Import nor simulate the native save engine.
