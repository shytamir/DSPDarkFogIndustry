# DSP Dark Fog Industry

A BepInEx mod for Dyson Sphere Program that lets you produce all six Dark Fog
materials in your factories, even with the Dark Fog disabled.

See the [player guide](packaging/README.md) for recipes and installation.

## Documentation

- [Project steering](docs/PROJECT.md): scope, decisions and current state.
- [Roadmap](docs/planning/ROADMAP.md): planned work.
- [Working methods](docs/WORKING-METHODS.md): document responsibilities and contribution workflow.
- [Local development](docs/LOCAL-DEVELOPMENT.md): environment setup and reference checks.
- [Build and packaging](docs/BUILD-AND-PACKAGING.md): build options, versioning and package requirements.

## Build

Use Windows, PowerShell 7, Git and the .NET SDK pinned in [global.json](global.json).
The first build needs NuGet access. From the repository root, run:

```powershell
./build.ps1
```

This builds the plugin, runs the checks and creates a validated package under
`artifacts/runs/`. A standard build does not require an installed copy of the game.
See [local development](docs/LOCAL-DEVELOPMENT.md) for additional development setup.

## Contributing

Bug reports, suggestions and pull requests are welcome. For bugs, include the mod
and game versions and steps to reproduce the problem. Please discuss larger changes
in an issue first, keep pull requests focused and include relevant validation.
Read the [working methods](docs/WORKING-METHODS.md) before contributing and
[AGENTS.md](AGENTS.md) when working with an agent.

## License

Repository-authored work is licensed under [Apache 2.0](LICENSE).
See [source provenance](docs/concept/EVIDENCE.md) for supplied reference material.
