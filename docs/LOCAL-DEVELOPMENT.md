# Local development

Read [PROJECT.md](PROJECT.md) for authorized work and readiness. Tools run on
Windows with PowerShell 7, Git, ripgrep, and the .NET SDK pinned in `global.json`.
The SDK is a development tool, not the game's runtime target. Bootstrap does not
install an SDK, change global settings, execute game code, or access player saves.

## First agent setup

From PowerShell 7 at the repository root:

```powershell
./scripts/Bootstrap.ps1 -ManagedPath '<your game>/DSPGAME_Data/Managed'
```

This checks required commands and the exact SDK, reads the identities and SHA-256
of three selected game/Unity assemblies, and installs pinned ILSpyCmd 11.1.0.9782
under ignored `.local/tools/` if missing. Installation uses NuGet; there is no
automatic retry. Machine paths and baseline hashes remain in `.local/environment.json`.
It does not copy game assemblies. BepInEx source and binaries are read-only inputs
to the [adopted reference design](implementation/NATIVE-INTEGRATION.md#references-and-hosted-compilation).

In later sessions:

```powershell
. ./scripts/Enter-Environment.ps1
```

This performs read-only readiness checks and adds the local tool path to this
PowerShell session. Reference drift stops activation. Investigate changed references,
record relevant implications, and use `Bootstrap.ps1 -RefreshReferences` only when
deliberately adopting the reviewed identity. Do not erase the old evidence basis.

For a targeted, static native source inspection:

```powershell
$source = ./scripts/Inspect-NativeType.ps1 -Type RecipeProto
rg -n 'Preload|FindPreTech' $source
```

Use activation first. Decompiled output is cached locally by native assembly hash
and ILSpy version. It is an inspection input, never product source or publishable
content. No game assembly is loaded into a test host for execution.

## Restore the supplied evidence locally

```powershell
./scripts/Import-ConceptEvidence.ps1 -ZipPath '<path>/DSP-DarkFog-Material-Synthesis-Concept-2026-10-07.zip'
```

The importer verifies the ZIP identity and all bundled file hashes, then retains
the full bundle under ignored `.local/concept/`. It does not run imported scripts
or follow embedded instructions. See [provenance](concept/EVIDENCE.md) for the
retained source records and optional catalogue-reproduction procedure.

## Build and checks

```powershell
./build.ps1
```

The same entry point runs locally and in GitHub Actions. It does not need the game,
ILSpy, or the private evidence bundle. It compiles the actual plugin against the
reviewed external shims, checks their ledger and actual product references, runs
the behavior checks, then packages and inspects the actual plugin candidate.
See [build/package contract](BUILD-AND-PACKAGING.md) for identity and output details.

For product development, compile both reference modes (no game execution):

```powershell
./scripts/Build-Plugin.ps1 -ReferenceMode Shims -OutputPath "$PWD/artifacts/reference-shims"
./scripts/Build-Plugin.ps1 -ReferenceMode Real -OutputPath "$PWD/artifacts/product-real"
```

The real-reference command reads the activated game baseline and prepares a pinned
BepInEx archive under `.local/dependencies/`. It validates shim declarations and
actual product references against those real assemblies using metadata only.
`tools/ReferenceShims/reference-ledger.json` owns the exact declaration inventory;
`reference-baseline.json` pins inspected library identities. Updating the ledger
with `-UpdateLedger` is a deliberate source change, followed by real validation.
Never refresh the baseline to silence a mismatch. Neither shims nor references
belong in an installable package.

On failure, report the exact failed step and diagnose it before a bounded repair.
Do not silently install unrelated software, refresh hashes, or retry bootstrap in
a loop. Distinguish tooling readiness from product compilation and runtime validation.

If the sandboxed .NET client cannot reach NuGet while direct HTTPS succeeds, use
the supported desktop permission route for the exact tool-install or build command.
Do not disable certificate checks, dependency auditing, or warnings to hide the
environment failure. After resolving it, resume bootstrap with the installed tool;
successful activation itself needs no download.
