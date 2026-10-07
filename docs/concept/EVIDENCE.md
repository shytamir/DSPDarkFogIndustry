# Concept intake and evidence provenance

The owner supplied `DSP-DarkFog-Material-Synthesis-Concept-2026-10-07.zip` as the
project's concept documentation. [source.json](source.json) identifies the exact
ZIP and the files retained here without byte changes. This page is a repository
index, not a verbatim copy of the bundle's longer `EVIDENCE.md`.

Archive SHA-256:
`e56744b2780188e9ca641b57044161228060ad886c3aafe9c3bf239cd4203ca7`.

## Authority and retained material

[CONCEPT.md](CONCEPT.md) preserves the supplied product intent, alternatives,
constraints, and open design choices. Its imperative wording does not authorize
implementation, game access, deployment, or release. [PROJECT.md](../PROJECT.md)
records the actual requested work and decisions. Mentions of another project in
the original concept are source provenance, not this project's identity or design.

Retained evidence:

- [Exact selected catalogue records](evidence/data/catalogue.json)
- [Dependency-query results](evidence/data/dependency-summary.json)
- [Earlier bounded rarity sample](evidence/data/rarity-selected-eight-clusters.tsv)
- [Native and third-party source provenance](evidence/provenance.json)

These are supplied research records, not newly independently established gameplay
results. The native investigation identifies DSP `0.10.35.29104`, assembly SHA-256
`6C122E5443E6843979B4064050DFCB5E0D75577A0B64F6AE4111290238B33C12`, and ILSpyCmd
`11.1.0.9782`. The rarity sample separately identifies `0.10.35.29057`, eight selected
64-star/1x clusters; it does not establish representative rarity or recipe balance.

## Full local evidence

Use [Import-ConceptEvidence.ps1](../../scripts/Import-ConceptEvidence.ps1) as described
in [local development](../LOCAL-DEVELOPMENT.md) to restore the exact full bundle to
ignored `.local/concept/`. There, read the original `EVIDENCE.md`, source excerpts,
and reproduction instructions. The importer checks all supplied SHA-256 entries
and executes no scripts. The repository check separately verifies retained bytes.

Native decompilations, raw game assets, dependency binaries, and machine-specific
research output remain local. The repository license does not relicense game
catalogue data or third-party material. The supplied LDBTool excerpts retain their
MIT notice in the local bundle. They are an inspected example, not an adopted
dependency or a current compatibility certification.

The bundle's `scripts/verify_catalogue.py` can optionally reproduce its catalogue
assertions using Python's standard library. Review it before running. It reads
the bundle only unless an explicit catalogue directory is supplied. Full fresh
asset extraction additionally requires UnityPy and separate task scope; neither
Python nor UnityPy is required by this repository's build or agent bootstrap.

## Evidence limits for planning

The supplied static records support the native research/discovery relationships,
recipe registration/cache concerns, existing-save reconciliation requirement,
and absence of Fog prerequisites in the bounded combat-tech closure. They do not
establish a functioning plugin, compatible registration dependency, recipe balance,
in-game peaceful progression, save/removal safety, or cross-mod compatibility.

Use the actual records and current native mechanisms when making those decisions.
Keep new dated evidence distinct rather than rewriting these source identities.
