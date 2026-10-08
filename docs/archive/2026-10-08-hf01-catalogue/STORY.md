# HF-01: Prevent synthesis recipes from triggering prototype detection

## Outcome and scope

While the mod is installed, adding its recipes and technology unlocks must not
create new abnormal-history entries through the native prototype detector.
Investigate the detector's registration, event timing and reporting path, then
disconnect it before recipe registration using the smallest supported change.
The owner permits disabling the whole detector system if that is simpler.

Include external-reference declarations, meaningful regression checks, local and
hosted validation, and a short handoff for the owner's in-game test. Preserve
recipe identities, unlocks, production and existing saved history. Exclude save
repair, changes to achievement/online eligibility, installation, publication,
version-line promotion and 1.0 planning.

## Investigation and delivery

1. Compare the supplied analysis and the referenced native investigation with
   the installed assembly. Establish which detector reports the changes, when it
   subscribes and whether disabling registration leaves save lifecycle intact.
2. Implement the narrowest adequate disconnection before recipe mutation.
   Keep implementation and maintenance constraints in source; record the native
   evidence and its limits in the technical record.
3. Exercise selective disconnection, repeated preload and ordering, retain recipe
   regression checks, validate shim/real references and the package, then review
   the diff and push the story with its management update to main.
4. Verify the hosted run and downloaded artifact, including source identity,
   package hashes and real-library bindings. Hand that exact candidate to the owner.

## Definition of done and limits

Technical completion requires evidence that the installed detector factory cannot
instantiate the disconnected prototype checks after this mod's preload callback;
other detector registrations and category records remain available. Regression
checks must fail if the disconnection is removed or moved after recipe access.
Local and downloaded hosted checks must pass, with a reproducible in-game handoff.
Only the owner can accept the actual new-game/load/save result.

Stop and report if native timing allows reporting before the existing preload
hook, if the smallest fix requires rewriting existing saves or broad unrelated
systems, or if a repeated validation failure cannot be resolved within the
working-methods repair limit. Do not treat an offline pass as a clean-save or
online-eligibility guarantee.

Work state and authorization belong in [PROJECT](../../PROJECT.md); evidence and the
owner procedure belong in [the technical record](VALIDATION.md).
