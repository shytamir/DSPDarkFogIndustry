# DSP Dark Fog Industry

## Recipes and configuration

Produce all six Dark Fog materials in your factories, even with the Dark Fog disabled. Start with Fractal Silicon and add combat components to work your way from Energy Shards to Core Elements.

Find the recipes in the six rightmost slots of the Items tab's bottom row. Each unlocks with the research shown below. If you have already completed that research, the recipe will be available when you load your save.

With the default settings, each recipe consumes one of each listed ingredient. Energy Shard produces two per craft; all other recipes produce one.

| Product | Ingredients | Unlocks with |
| --- | --- | --- |
| Energy Shard | Fractal Silicon | Particle Control |
| Dark Fog Matrix | Energy Shard + Corvette | Corvette |
| Matter Recombinator | Dark Fog Matrix + Crystal Shell Set | Crystal Shell Set |
| Silicon-based Neuron | Matter Recombinator + Destroyer | Destroyer |
| Negentropy Singularity | Silicon-based Neuron + Gravity Missile Set | Gravity Missile Set |
| Core Element | Negentropy Singularity + Antimatter Capsule | Antimatter Capsule |

- **Energy Shard:** 120/min in an Arc Smelter.
- **Dark Fog Matrix, Matter Recombinator, Silicon-based Neuron and Negentropy Singularity:** 60/min each in an Assembler Mk.I.
- **Core Element:** 60/min in a Chemical Plant.

These rates assume full power, a steady supply of ingredients and no proliferation. Higher-tier facilities produce faster as usual. Dark Fog Matrix and Silicon-based Neuron support proliferation for production speedup only; the other four recipes also support extra products.

**Custom recipes**

After starting the game once, close it and edit `BepInEx/config/dark-fog-industry.cfg` with Notepad or Thunderstore Mod Manager's config editor. For a managed installation, use the file in the profile you launch. Restart the game to apply changes. Settings apply to every save loaded with that profile.

Each of the six numbered recipe sections lets you choose:

- The unlock technology ID. The technology must be fully completed; several recipes can share it.
- A smelter, assembler or chemical plant item ID. All compatible tiers remain usable.
- One output item ID, its count per batch and its products per minute.
- Up to six different ingredient IDs and their counts per batch. Set an ingredient ID to zero to leave that row unused. Recipes can also have no ingredients.

Use whole numbers. Rates always refer to the **basic machine**: Arc Smelter, Assembler Mk.I or Chemical Plant, before proliferation. Choosing an upgraded machine ID does not change that baseline. For example, 60/min in the assembly family means 60/min in Mk.I and 80/min in Mk.II. Counts are per batch; they are separate from the production rate. With custom ingredients, extra-product eligibility follows those ingredients' normal rules.

The final three sections list item, technology and eligible machine IDs with names in the game's active language. They are lookup lists: editing a name does not change a recipe, and names refresh at startup. Hidden and obsolete technologies are labelled; selecting one may leave a recipe unavailable in ordinary play.

Invalid values are corrected individually and saved, with details in the BepInEx log. Numeric values use the nearest valid choice; equal distances use the lower value. Unattainable rates use the nearest attainable whole-number rate. Missing or nonnumeric fields use their defaults. Check the saved values after restarting. BepInEx regenerates comments when saving, so keep personal notes separately. Damaged file syntax or a file that cannot be read or written needs to be fixed before settings can be saved normally.

## Replicator and saved games

Recipes configured for assemblers can be crafted in the Replicator; smelting and chemical recipes must be made in machines. With the defaults, this means the middle four recipes allow handcrafting while Energy Shards and Core Elements are machine-only. Bring the required ingredients when crafting by hand: the Replicator cannot make the smelting or chemical steps for you.

**Before changing a recipe already in use:** with the old settings still active, stop and empty affected machines, clear their selected recipes, cancel affected handcraft orders, and save. Then exit the game, edit the configuration and restart. Choose the recipes again in compatible facilities. Keep a backup of both the save and its matching configuration.

Already unlocked recipes stay unlocked when their technology gate changes. Changing a machine ID does not convert buildings. Skipping the preparation above can clear machine contents or leave queued work using its old recipe. The mod keeps the game's normal save-loading and crafting behavior; it does not migrate stored resources or queued work. Extremely large quantities across crafting chains or long handcraft queues can exceed the game's native arithmetic limits, even when each recipe's machine settings are valid.

**Saves that use these recipes are not compatible with an unmodded game.** Keep the mod installed for those saves. Back up your save before adding the mod; if you remove it, restore a backup made before you installed it.

## Installation

**Mod manager:** Install **DSPDarkFogIndustry** and its dependencies through your Thunderstore-compatible mod manager, then launch the game from the manager.

**Manual installation:**

1. Close the game and install [BepInEx for Dyson Sphere Program](https://thunderstore.io/c/dyson-sphere-program/p/xiaoye97/BepInEx/) if you do not already have it.
2. Download and extract this mod's ZIP file.
3. In your Steam library, right-click **Dyson Sphere Program**, then choose **Manage → Browse local files**.
4. Copy the included **BepInEx** folder into that game installation folder, alongside **DSPGAME.exe**. Merge the folders if prompted.
5. Start the game normally.
