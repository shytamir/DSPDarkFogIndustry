# Configurable recipes: owner checklist for 0.9.33

Use **0.9.33** from [run 38419021406](https://github.com/shytamir/DSPDarkFogIndustry/actions/runs/38419021406) in a test save/profile; [PROJECT](../PROJECT.md#selected-configurability-candidate) records the verified artifact. Launch once to generate the config, then exit. Before changing recipes already in use: **empty affected machines, clear their recipes, cancel queued handcrafts, save, then exit.**

Start with default recipes and keep a copy of the config for the reset step. In **Notepad or another text editor**, open `BepInEx/config/dark-fog-industry.cfg` in the profile you launch. Core sections `01`–`06` come first; `Extra inputs - ...` sections follow.

- [ ] Find the separate `dark-fog-industry.reference.txt`; use Find to look up an item or technology. Machine choices cover only assemblers, smelters and chemical plants.

**Editing rule:** replace the value after `=`, including the existing `# name` label. Changing only that label keeps the same ID. Edit existing keys inside the named section; do not append a second copy.

In-game, first check the defaults:

- [ ] Energy Shards produce **120/min in Arc Smelter**; the middle four recipes **60/min in Assembler Mk.I**; Core Elements **60/min in Chemical Plant**.
- [ ] Only assembler recipes allow handcrafting. No unexpected sandbox mode or errors.

Then edit **`[02 - Recipe 402 - Dark Fog Matrix]`** as follows; leave other fields at defaults. The lowercase ingredient name below tests case-insensitive input. If your game uses another language, use the Fractal Silicon name from the reference file.

```ini
TechnologyId = 1133
MachineId = 2318
OutputItemId = 5201
OutputCount = 2
OutputPerMinute = 90
Input2ItemId = fractal silicon
Input2Count = 3
```

- [ ] After restart, `Input2ItemId` is saved as `1013 # <current name>`, with no correction warning. Each batch consumes **1 Energy Shard + 3 Fractal Silicon**, producing **2 Dark Fog Matrices**.
- [ ] Check these family changes, preparing machines and restarting between changes. Rates assume full power, continuous supply and no proliferation:

| MachineId | Basic machine | One upgraded machine | Handcraft |
| --- | --- | --- | --- |
| `2318` | Assembler Mk.I: **90/min** | Mk.II: **120/min** | Yes |
| `2319` | Arc Smelter: **90/min** | Plane Smelter: **180/min** | No |
| `2317` | Chemical Plant: **90/min** | Quantum Plant: **180/min** | No |

- [ ] Fully spray both ingredients with Mk.III: basic-machine output becomes **112.5/min with extra products**, or **180/min with acceleration**.
- [ ] Recipes 01 and 02 now share Particle Control: both unlock upon completion, including when loading an already-researched save. Moving the gate afterward must preserve saved unlocks.

Test the optional slots in **`[Extra inputs - 02 - Recipe 402 - Dark Fog Matrix]`**:

- [ ] Set `Input3ItemId = 1606` and `Input3Count = 1`. Prepare/restart → the same recipe also requires **1 Crystal Shell Set per batch**. Prepare again, restore both fields to `0`, and restart → that ingredient disappears.

Finally, prepare the affected machines/handcrafts again, exit DSP, and restore your saved default config before testing corrections:

- [ ] In recipe 02's core section, set `Input2ItemId = fractal silico`: restart → default **5111 (Corvette)**, with a field-specific warning. It must not select Fractal Silicon.
- [ ] In that core section, set `OutputPerMinute = 61`: restart → **60**, `OutputCount` still **1**.
- [ ] Set `OutputCount = nonsense`: restart → **1**, with a field-specific log message.
- [ ] In recipe 02's extra-input section, set `Input3ItemId = 0`, `Input3Count = 8`: restart → count **0**.
- [ ] Restart again: corrected values stay unchanged, with no repeated corrections.

Report failures with the edited section, observed result and relevant log excerpt; mark anything skipped.
