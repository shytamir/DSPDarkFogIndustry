# Configurable recipes: owner checklist for 0.9.30

Use a **test save/profile with 0.9.30** from [run 38405672271](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38405672271). Launch once to generate the config, then exit. Before changing recipes already in use: **empty affected machines, clear their recipes, cancel queued handcrafts, save, then exit.**

In **Thunderstore Mod Manager**:

- [ ] Find `dark-fog-industry.cfg`: six recipe groups first, then item, technology and machine references.
- [ ] Look up an item by name and ID. Machine choices should cover only assemblers, smelters and chemical plants.
- [ ] Edit and save a value; reopen it and confirm Notepad shows the same value.
- [ ] Edit a reference name. After restarting DSP, its original label should return without changing any recipe.

In-game, first check the defaults:

- [ ] Energy Shards produce **120/min in Arc Smelter**; the middle four recipes **60/min in Assembler Mk.I**; Core Elements **60/min in Chemical Plant**.
- [ ] Only assembler recipes allow handcrafting. No unexpected sandbox mode or errors.

Then use **recipe 02 — Dark Fog Matrix** for one compact custom test. Leave other fields at defaults:

```ini
TechnologyId = 1133
MachineId = 2318
OutputItemId = 1606
OutputCount = 2
OutputPerMinute = 90
Input2ItemId = 1013
Input2Count = 3
```

- [ ] After restart, each batch consumes **1 Energy Shard + 3 Fractal Silicon**, producing **2 Crystal Shell Sets**.
- [ ] Check these family changes, preparing machines and restarting between changes. Rates assume full power, continuous supply and no proliferation:

| MachineId | Basic machine | One upgraded machine | Handcraft |
| --- | --- | --- | --- |
| `2318` | Assembler Mk.I: **90/min** | Mk.II: **120/min** | Yes |
| `2319` | Arc Smelter: **90/min** | Plane Smelter: **180/min** | No |
| `2317` | Chemical Plant: **90/min** | Quantum Plant: **180/min** | No |

- [ ] Fully spray both ingredients with Mk.III: basic-machine output becomes **112.5/min with extra products**, or **180/min with acceleration**.
- [ ] Recipes 01 and 02 now share Particle Control: both unlock upon completion, including when loading an already-researched save. Moving the gate afterward must preserve saved unlocks.

Finally, with DSP closed, test corrections using Notepad:

- [ ] Restore recipe 02's defaults, then set rate to `61`: restart → **60**, output count still **1**.
- [ ] Set its output count to `nonsense`: restart → **1**, with a field-specific log message.
- [ ] Set an unused ingredient row to ID `0`, count `8`: restart → count **0**.
- [ ] Restart again: corrected values stay unchanged, with no repeated corrections.

Report failures with the edited section, observed result and relevant log excerpt; mark anything skipped.
