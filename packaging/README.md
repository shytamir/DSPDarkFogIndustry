# DSP Dark Fog Industry

Synthesize all six existing Dark Fog materials through normal industry, without
enemy drops. Use rare resources, combat components and their existing research;
native machines, technology costs and combat drops retain their usual behavior.

## Recipes

Each input count is one. Outputs are one except Energy Shard, which yields two.
The recipes occupy the rightmost six slots of the Items selector's bottom row.

| Product | Inputs | Research gate | Facility and unsprayed rate |
| --- | --- | --- | --- |
| Energy Shard | Fractal Silicon | Particle Control | Arc Smelter, 120/min |
| Dark Fog Matrix | Energy Shard + Corvette | Corvette | Assembler Mk.I, 60/min |
| Matter Recombinator | Dark Fog Matrix + Crystal Shell Set | Crystal Shell Set | Assembler Mk.I, 60/min |
| Silicon-based Neuron | Matter Recombinator + Destroyer | Destroyer | Assembler Mk.I, 60/min |
| Negenthropy Singularity | Silicon-based Neuron + Gravity Missile Set | Gravity Missile Set | Assembler Mk.I, 60/min |
| Core Element | Negenthropy Singularity + Antimatter Capsule | Antimatter Capsule | Chemical Plant, 60/min |

Higher-tier facility speed and proliferation follow native rules. Ship inputs make
Dark Fog Matrix and Silicon-based Neuron acceleration-only; the other four support
native extra products. The four assembler recipes allow handcrafting. Smelting and
chemical recipes require a machine. Rates assume full power and continuous supply.

Synthesized items participate in normal Dark Fog technology discovery and research.
Other prerequisites still apply; discovering a material does not finish research.
Existing saves receive the six recipes whose corresponding gates were researched.

## Installation

Requires the DSP BepInEx pack **5.4.17** (`xiaoye97-BepInEx-5.4.17`). No LDBTool is
required. Quit the game, install BepInEx if needed, then extract this package into
the game directory so the DLL is at
`BepInEx/plugins/DSPDarkFogIndustry/DSPDarkFogIndustry.dll`. A mod-manager profile
can instead import the local ZIP and install the declared dependency. Keep one copy
of the plugin. Its BepInEx GUID is `dark-fog-industry`.

## Saves and compatibility

Back up before testing. **Keep this mod installed for saves using synthesis.**
To roll back, quit, remove its plugin folder and restore an untouched pre-mod
checkpoint. Loading a synthesis save without the mod is unsupported; configured
machines can retain unknown recipe references. The mod does not edit saves or
automatically clear machine settings.

Recipe IDs 401–406 and grid slots 1809–1814 are fixed. Other content mods using
those IDs or slots can conflict; an occupied slot/ID causes a load error. The
baseline for owner validation is this plugin plus BepInEx, with no other mods.

Static checks target DSP **0.10.35.29104**. Hosted packages compile against reviewed
external shims; local checks compare them with actual game/library metadata. These
checks do not establish gameplay compatibility. See the repository's
[project status](https://github.com/shytamir/DSPDarkFogIndustry/blob/main/docs/PROJECT.md)
for candidate readiness, owner validation and publication state.

[Source and issue tracker](https://github.com/shytamir/DSPDarkFogIndustry).
Repository-authored code and icon are Apache-2.0 licensed.
