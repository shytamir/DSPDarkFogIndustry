# DSP Dark Fog Industry

## Recipes and configuration

Produce all six Dark Fog materials in your factories, even with Dark Fog disabled. Start with Fractal Silicon and add combat components to work your way up to Core Elements. You can also customize these six recipes, including what they produce.

Find them in the six rightmost slots of the Items tab's bottom row. Each unlocks with the research below, including when you load a save where that research is already complete.

With default settings, each batch uses **one of each listed ingredient**. Energy Shard produces two per batch; the other recipes produce one.

| Product | Ingredients | Unlocks with |
| --- | --- | --- |
| Energy Shard | Fractal Silicon | Particle Control |
| Dark Fog Matrix | Energy Shard + Corvette | Corvette |
| Matter Recombinator | Dark Fog Matrix + Crystal Shell Set | Crystal Shell Set |
| Silicon-based Neuron | Matter Recombinator + Destroyer | Destroyer |
| Negentropy Singularity | Silicon-based Neuron + Gravity Missile Set | Gravity Missile Set |
| Core Element | Negentropy Singularity + Antimatter Capsule | Antimatter Capsule |

- **Energy Shard:** 120/min in an Arc Smelter.
- **The middle four recipes:** 60/min each in an Assembler Mk.I.
- **Core Element:** 60/min in a Chemical Plant.

These rates assume full power, enough ingredients and no proliferation. Higher-tier machines run faster as usual. By default, Dark Fog Matrix and Silicon-based Neuron support proliferation for speedup only; the other four recipes also support extra products.

**Custom recipes**

Start the game once, then close it and open `BepInEx/config/dark-fog-industry.cfg` in Notepad or another text editor. With a mod manager, use the file in the profile you launch. Restart to apply changes; settings apply to every save you play with that profile.

Each numbered section lets you choose the unlock technology, machine, output item and batch count, output per minute, and ingredients with their counts. Outputs can be **other items too**, not just Dark Fog materials. You are editing the same six recipe slots.

- `TechnologyId`: the research that must be fully completed to unlock the recipe. Several recipes can share it.
- `MachineId`: a smelter, assembler or chemical plant. Any compatible tier can make the recipe.
- `OutputItemId`, `OutputCount`, `OutputPerMinute`: what to make, how many per batch, and how many per minute.
- `Input1ItemId` / `Input1Count` and `Input2ItemId` / `Input2Count`: ingredients per batch. Inputs 3–6 are in the matching **Extra inputs** section. Use different items in each slot; set an item's ID and count to `0` to leave it unused.

Fields ending in `Id` accept IDs or exact names in your current game language, ignoring capitalization. Replace the **whole value after `=`**, including any `# name` label; changing the label alone keeps the same item. Find names and IDs in `dark-fog-industry.reference.txt`, beside the config. Use an ID if a name has multiple matches. The reference refreshes at startup, including after a language change; editing it does not change recipes. Hidden or obsolete research may be unavailable in normal play.

Counts are whole numbers **per batch**. Rates are whole numbers **per minute in the basic machine**: Arc Smelter, Assembler Mk.I or Chemical Plant, before proliferation. Choosing an upgraded machine ID keeps that baseline. Extra-product availability follows the ingredients you choose.

For example, to make iron ingots in the first recipe slot, edit these fields in its section and leave the others at defaults (use your game's names if it is not in English):

```ini
[01 - Recipe 401 - Energy Shard]
OutputItemId = iron ingot
OutputCount = 1
OutputPerMinute = 60
Input1ItemId = iron ore
```

This uses one Iron Ore per ingot, producing 60/min in an Arc Smelter, unlocked by Particle Control.

After restarting, check your saved settings. If a change does not save as expected, check `BepInEx/LogOutput.log` in the same installation or profile. Keep personal notes separately, as config comments are regenerated.

## Replicator and saved games

Assembler recipes can be crafted in the Replicator; smelting and chemical recipes need machines. Bring the ingredients for handcrafting: the Replicator cannot make the smelting or chemical steps for you.

**Before changing a recipe already in use:** with the old settings active, empty affected machines, clear their selected recipes, cancel affected handcraft orders, and save. Then exit, edit the config and restart. Select the recipe again in a compatible machine. Keep backups of both your save and its matching config; skipping these steps can lose stored ingredients or leave queued crafts using the old recipe.

Changing the unlock technology does not relock recipes you already know. Very large quantities or handcraft queues can exceed the game's limits.

**Saves using these recipes need the mod installed.** Back up your save before adding the mod. If you remove it, restore a backup from before installation.

## Installation

**Mod manager:** Install **DSPDarkFogIndustry** and its dependencies through your Thunderstore-compatible mod manager, then launch from that manager.

**Manual installation:**

1. Close the game and install [BepInEx for Dyson Sphere Program](https://thunderstore.io/c/dyson-sphere-program/p/xiaoye97/BepInEx/) if needed.
2. Download and extract this mod's ZIP.
3. In Steam, right-click **Dyson Sphere Program → Manage → Browse local files**.
4. Copy the included **BepInEx** folder there, alongside **DSPGAME.exe**. Merge folders if prompted.
5. Start the game normally.
