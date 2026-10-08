# Tests

Run `./build.ps1` from the repository root for the standard suite.

- **ProductChecks:** exercises the mod's registration and save-reconciliation
  code with small data-store fakes. It checks registration conflicts and atomicity,
  stable save IDs, native recipe field construction, recipe rates and proliferation
  eligibility, and unlock behavior across all research combinations. It also calls
  the linked preload callback to check that prototype detectors are disconnected
  before recipe access, while retaining category records and other detectors.
- **Package.Tests.ps1:** changes a valid ZIP to ensure the package validator rejects
  invalid contents, metadata, encoding and versions.
- **References.Tests.ps1:** local negative checks for ledger changes, reference
  drift and missing real assemblies. Run after building both reference modes:

  ```powershell
  ./tests/References.Tests.ps1 -ShimPath artifacts/reference-shims -PluginPath artifacts/product-real/DSPDarkFogIndustry.dll
  ```

The native-rules fixture contains dated machine speeds and item flags, with source
hashes. Rate and eligibility checks apply those facts to the mod's recipe output;
they do not execute the game's production code. Fakes do not reproduce native
preload, research discovery or serialization.
The test-only patch attributes do not apply Harmony patches; native target metadata
is validated separately against real libraries.

Keep expectations independent where regressions matter, such as saved recipe IDs
and intended production rates. Do not add checks for document wording, work-item
completion or constants in a reference-data fixture. Native investigation and
gameplay observations belong in the supporting evidence.
