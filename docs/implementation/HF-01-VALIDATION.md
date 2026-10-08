# Detector initialization correction

[PROJECT](../PROJECT.md) owns rejection, steering, readiness and acceptance.
The [current story](../planning/HF-01-PROTOTYPE-DETECTION.md) defines scope;
the [first implementation's evidence](../archive/2026-10-08-hf01-catalogue/VALIDATION.md)
is archived. Its catalogue fixtures did not test actual startup/game-mode behavior.
The owner's report of immediate sandbox mode invalidates its readiness conclusion.
That symptom has not been independently reproduced by the agent.

## Native basis

DSP 0.10.35.29104, `Assembly-CSharp.dll` SHA-256:
`6c122e5443e6843979b4064050dfcb5e0d75577a0b64f6ae4111290238b33c12`.
Read-only inspection on 2026-10-08 confirms that `AbnormalityLogic.InitDeterminators`
first allocates a dictionary, then constructs detectors. Tick and free callers
enumerate that dictionary. It must remain valid even when no detectors are created.

The owner-directed reference is `DSPNativeTestPatcher/Patcher.cs`, under the local
shared resources. Its detector change replaces the method body with empty-dictionary
initialization and return. The [plugin patch](../../src/DSPDarkFogIndustry/DetectorInitializationPatch.cs)
applies the same behavior using Harmony's field injection and original-method skip.
It is installed by `Plugin.Awake` with the other patches, before the recipe preload
callback. It disables all native detectors and does not touch the catalogue,
game-mode flags, saved records, or account/network methods from the reference patcher.

The [target ledger](../../tools/ReferenceShims/patch-targets.json) records the method
and injected private dictionary; real-reference checks validate both. The normal
type/member ledger records the three game types and their inheritance. Removed
catalogue references and tests are not retained as active product checks.

## Offline results — 2026-10-08

- Shim and real-reference builds completed without warnings. The real check resolved
  62 compiled external references, 65 shim members, three Harmony targets and the
  injected private dictionary. Deliberately wrong field type and visibility entries
  were both rejected by the checker.
- The shared build passed all 12 retained product checks, valid packaging, 13
  malformed-package cases, three malformed-version cases and repository checks.
- An authored local .NET Framework fixture linked the actual patch source and used
  BepInEx's pinned Harmony 2.5.5 library. An unpatched control created a detector;
  patched initialization skipped the factory and assigned an empty private
  collection on independent and reused instances. Tick/free iteration remained
  valid. This tests actual Harmony dispatch/injection, not Unity or a saved game.
  Fixture sources and output remain ignored under `.local/detector-fixture/`.
- Static comparison against published `0.9.14` found all nine existing product
  method bodies identical after normalizing startup version text. The sole added
  behavior is the new initialization prefix. Local evidence:
  `artifacts/hf01-correction-il-comparison.json`.
- Local dirty build evidence: `artifacts/runs/61825c91c58446b8a04fc6d82d1200dd/`,
  ZIP SHA-256 `893daf7be2673e93f86b1612a72d8cd2c4ae771ac8f0cd4019b58ed947b4d5cb`.
  This is not the committed owner-test artifact; PROJECT will identify that candidate.

## Owner handoff

For the in-game check, restart with that candidate and use a new normal-mode game
or an untouched normal-mode backup. Check that the game remains outside sandbox
mode, recipes still work, and no new abnormal findings appear after manual save,
autosave and reload. A save already written in sandbox mode or containing abnormal
history is not repaired. Existing abnormal records should remain unchanged.

No game launch, save access, installation or publication is part of agent validation.
