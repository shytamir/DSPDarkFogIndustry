# DSP Dark Fog Industry

A BepInEx plugin adding native industrial recipes for the six existing Dark Fog
materials in Dyson Sphere Program, without requiring enemy drops. Read the
[recipes and installation notes](packaging/README.md), [supplied concept](docs/concept/CONCEPT.md)
and its [evidence provenance](docs/concept/EVIDENCE.md).

[Project steering](docs/PROJECT.md) is the authority for current work and readiness.
The repository separates product source, development tools, packaging, planning,
and implementation evidence. Gameplay acceptance and publication are separate
from compilation and offline checks; consult project steering for current state.

## Start here

- [Owner validation procedure and exact candidate](docs/implementation/OWNER-VALIDATION.md)
- [Agent instructions](AGENTS.md) and [working methods](docs/WORKING-METHODS.md)
- [Local development and bootstrap](docs/LOCAL-DEVELOPMENT.md)
- [Roadmap entry point](docs/planning/ROADMAP.md)
- [Build, packaging, and versioning](docs/BUILD-AND-PACKAGING.md)

From PowerShell 7 on Windows, run `./build.ps1` to compile the plugin, run checks,
and create a validated Thunderstore-layout candidate ZIP. First restore needs NuGet
access and the .NET SDK version in `global.json`. Outputs stay in `artifacts/`.

## Layout

| Path | Responsibility |
| --- | --- |
| `src/` | BepInEx plugin and six native recipe definitions |
| `tests/` | Focused checks for executable contracts |
| `tools/` | Compile-only external shims, reference ledger and metadata checks |
| `scripts/` | Shared local and CI commands |
| `packaging/` | Explicit package inputs |
| `docs/PROJECT.md` | Steering, decisions register, state, and acceptance |
| `docs/concept/` | Supplied product intent and selected dated evidence |
| `docs/planning/` | Work definitions, scope, and acceptance criteria |
| `docs/implementation/` | Technical contracts and supporting evidence |
| `docs/archive/` | Historical plans and implementation records |
| `.local/`, `artifacts/` | Ignored machine configuration, evidence, tools, output |

Repository-authored work uses [Apache-2.0](LICENSE). See
[concept provenance](docs/concept/EVIDENCE.md) for the distinct origin of supplied
catalogue data and locally retained third-party/native evidence.
