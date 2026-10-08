# External reference shims

CI compiles the product against these hand-authored declarations for BepInEx,
Harmony, Unity, and the game. They contain no upstream implementation and are
never packaged or installed. Framework reference packs are ordinary locked build
inputs. See [integration contract](../../docs/implementation/NATIVE-INTEGRATION.md).

The checked-in `reference-ledger.json` inventories the declared type/member surface
and forwarding relationships. The metadata checker compares it to each shim build,
then validates it against identified real assemblies locally. It also verifies the
product's actual external references, plugin attributes, and string-named Harmony
targets. New references require both a matching declaration and ledger update.
`patch-targets.json` records the private/public native methods named by Harmony,
including complete signatures and parameter names used by patch injection. They
are validated separately because string-named targets are not compiler member refs.
It also records Harmony's private-field injection into `AbnormalityLogic.determinators`;
the checker verifies its name, exact dictionary type, visibility and instance binding.

Test fakes, where needed, live in tests. A successful shim build is not runtime
compatibility evidence. No game assembly is executed by the metadata checker.
