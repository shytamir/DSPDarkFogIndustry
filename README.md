# DSP Dark Fog Industry

An industrial synthesis concept for the six existing Dark Fog materials in
Dyson Sphere Program. Read the [supplied concept](docs/concept/CONCEPT.md) and
its [evidence provenance](docs/concept/EVIDENCE.md) before planning changes.

[Project steering](docs/PROJECT.md) is the authority for current work and readiness.
The repository separates product source, development tools, packaging, planning,
and implementation evidence. The build fixture exercises the delivery tooling;
it has no BepInEx entry point or gameplay behavior and must not be installed.

## Start here

- [Agent instructions](AGENTS.md) and [working methods](docs/WORKING-METHODS.md)
- [Local development and bootstrap](docs/LOCAL-DEVELOPMENT.md)
- [Roadmap entry point](docs/planning/ROADMAP.md)
- [Build, packaging, and versioning](docs/BUILD-AND-PACKAGING.md)

From PowerShell 7 on Windows, run `./build.ps1` to compile the fixture, create a
Thunderstore-shaped scaffold ZIP, and validate it. First restore needs NuGet
access and the .NET SDK version in `global.json`. Outputs stay in `artifacts/`.

## Layout

| Path | Responsibility |
| --- | --- |
| `src/` | Future product source, organized when implementation is approved |
| `tests/` | Focused checks for executable contracts |
| `tools/` | Development fixtures, never product architecture |
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
