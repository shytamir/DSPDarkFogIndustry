# Build and packaging contract

[PROJECT.md](PROJECT.md) owns readiness, approvals, acceptance and release state.

## Build and references

`./build.ps1 [-BuildNumber N]` compiles the net472 BepInEx plugin from `src/`, checks
its external declaration ledger and compiled metadata, runs the mod-owned behavior
checks, and packages only the actual plugin. All game, Unity, BepInEx and Harmony
references are compile-only shims; CI needs no installed game or private binaries.
The pinned framework reference pack and metadata tooling use locked NuGet restore.
Local [real-reference verification](LOCAL-DEVELOPMENT.md#build-and-checks) additionally
compiles against and checks metadata from the identified actual libraries.

`tools/ReferenceShims/reference-ledger.json` owns the declaration inventory;
`patch-targets.json` records string-named Harmony targets and `reference-baseline.json`
pins the real library identities/hashes. CI resolves every compiled external member
against shims and checks plugin identity/version and declared hooks. Only local
real-reference validation proves those hook signatures and declarations match the
identified native metadata. Neither path executes upstream assemblies or the plugin.

## Identity and output

Thunderstore name: `DSPDarkFogIndustry`. BepInEx GUID: `dark-fog-industry`.
`VERSION` supplies MAJOR/MINOR; local builds default to patch 0, while GitHub Actions
uses its workflow run number. Reruns retain the number and record their attempt.
Package/plugin version is `M.m.N`, assembly/file version `M.m.N.0`, diagnostic
identity `M.m.N.<12-character-commit>[.dirty]`. Preserve the workflow run sequence.

Each invocation uses a unique ignored `artifacts/runs/` directory. Only a successful
run produces a final `DSPDarkFogIndustry-<identity>.zip`; a pending ZIP or earlier
run is never substituted for a failure. Run one build at a time per checkout.

Separate maintainer evidence files are:

- `build-info.json`: source revision, dirty state, every source-file hash, SDK,
  version/attempt, reference mode, DLL hash and reference-report hash.
- `reference-validation.json`: actual external types/members, assembly references,
  plugin metadata, declared hooks and declaration-ledger hashes.
- `package-inspection.json`: exact ZIP entries and ZIP/DLL hashes.

Source inventories include untracked source. A dirty build is local evidence, not
a reproducible committed candidate. These reports and all shim/reference DLLs stay
outside the installable ZIP.

## Package contract

The package contains exactly five case-sensitive paths:

```text
manifest.json
README.md
icon.png
LICENSE
BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.dll
```

The manifest declares only `xiaoye97-BepInEx-5.4.17`. The simple 256x256 PNG identifies
the product. The DLL retains its build timestamp. No fixture, shim, dependency,
PDB, source, cache, private evidence or management report is included.

Validation checks exact layout, strict UTF-8 text, manifest identity/dependency/
version, source README/icon/license hashes, decoded PNG dimensions, DLL hash and
timestamp. Thirteen malformed-package cases cover extra/missing/duplicate/unsafe
paths, casing, wrong name/version, missing dependency, text/icon/payload changes,
timestamp and encoding. Three malformed version inputs are rejected as well.
Rules follow [Thunderstore packaging](https://wiki.thunderstore.io/mods/creating-a-package)
and [folder routing](https://wiki.thunderstore.io/mods/packaging-your-mods); passing
does not establish moderation acceptance or gameplay behavior.

```powershell
./scripts/Test-Package.ps1 -Path '<ZIP>' -BuildInfoPath '<build-info.json>'
```

## Hosted verification and publication boundary

The [workflow](../.github/workflows/build.yml) runs on main pushes, PRs to main and
manual dispatch. It has read-only permissions, no persisted checkout credentials,
commit-pinned actions, the pinned SDK and the same build entry point. The plugin ZIP
is uploaded directly; the three evidence files form a separate artifact. Artifacts
expire after 14 days, so retain the selected candidate locally for owner validation.
No release tag, GitHub release or store publication is created.

After main delivery, verify the actual hosted run, download package/evidence, compare
GitHub artifact digests, inspect all package entries, compare every source hash with
an export of that exact commit, and validate the downloaded DLL against real local
metadata. Record source, run/attempt, hashes and limitations in the validation record;
PROJECT owns disposition. A green run alone does not close this gate. Publication
requires separate owner acceptance/authority and verification of the public bytes.
