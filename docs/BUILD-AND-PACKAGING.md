# Build and packaging contract

[PROJECT.md](PROJECT.md) owns readiness, approvals, acceptance, and release state.

## What is built

`./build.ps1 [-BuildNumber N]` compiles `tools/ScaffoldFixture` against pinned public
.NET Framework reference assemblies, then creates and inspects a scaffold ZIP.
The fixture targets net472/C# 7.3 solely to exercise a conventional managed-library
build. It is not a BepInEx plugin; it has no entry point, game/Unity references,
Harmony patches, registration dependency, or product implementation. These fixture
settings do not decide the future product's architecture or target framework.

The build also compiles `src/DSPDarkFogIndustry` against the complete external
shim surface and checks its ledger. Local development additionally compiles against
real references and validates metadata; see [local development](LOCAL-DEVELOPMENT.md).
The ZIP stage still contains only the labelled fixture until MVP-06 replaces it.
Do not install that scaffold or ship shim/reference DLLs.

## Identity and output

`VERSION` supplies `MAJOR` and `MINOR`; a local build defaults to patch `0`, and
GitHub Actions uses its workflow run number. Reruns retain that number and record
the run attempt separately. Package version is `M.m.N`, assembly/file version is
`M.m.N.0`, and diagnostic identity is `M.m.N.<12-character-commit>[.dirty]`.
Keep this workflow's run-number sequence if it later supplies release versions.
Owner-authorized version promotion changes VERSION, not generated manifests.

Each invocation creates a unique folder under ignored `artifacts/runs/`. A final
ZIP is named only after all checks pass. A failed run retains diagnostic files
and possibly `package.pending.zip`; neither is a deliverable. Prior runs are never
presented as the result of a failed invocation. Run one build at a time per checkout.

`build-info.json` identifies the source revision, dirty state, source-file hashes,
SDK, version, run attempt, and compiled DLL hash. `package-inspection.json` records
the actual ZIP entries and hashes. Source inventories include untracked source;
a dirty build is local evidence, not a reproducible committed release identity.

## Package contract

The scaffold ZIP contains exactly these five files, with case-sensitive paths:

```text
manifest.json
README.md
icon.png
LICENSE
BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.Scaffold.dll
```

The manifest and README explicitly say scaffold only. Dependencies are empty
because there is no plugin; this is not the future product dependency declaration.
The 256x256 PNG is a deliberately plain tooling placeholder, not approved branding.
The DLL retains its compiled timestamp. Tools, PDBs, references, private evidence,
management documents, and build reports never enter this ZIP.

Validation checks exact layout, UTF-8 text, metadata/version, source text/icon
identity, decoded PNG dimensions, payload hash, and timestamp. Failure fixtures
exercise extra/missing/duplicate/unsafe paths, casing, version, text, icon, payload,
timestamp and encoding. This implements the relevant
[Thunderstore package rules](https://wiki.thunderstore.io/mods/creating-a-package)
and [BepInEx folder routing](https://wiki.thunderstore.io/mods/packaging-your-mods);
passing these checks does not establish moderation acceptance or a usable mod.

Inspect a specific artifact with its adjacent build evidence:

```powershell
./scripts/Test-Package.ps1 -Path '<ZIP>' -BuildInfoPath '<build-info.json>'
```

## Hosted pipeline and release boundary

The [workflow](../.github/workflows/build.yml) runs on main pushes, pull requests to
main, and manual dispatch. It uses read-only repository permissions, disables
persisted checkout credentials, pins actions by commit, installs the pinned SDK,
and calls the same local build entry point. No game binaries, installed paths, or
private secrets are needed. The ZIP is uploaded directly using
[upload-artifact](https://github.com/actions/upload-artifact); maintainer evidence
is a separate artifact. It does not create tags, GitHub releases, or store uploads.

Remote execution can be verified only after authorized Git delivery. Record the
run URL, source revision, downloaded artifact identity, and inspection results in
PROJECT. A local success is not a remote success. Before any future publication,
obtain the required owner acceptance and release authority and verify the exact
public bytes against the accepted CI artifact.
