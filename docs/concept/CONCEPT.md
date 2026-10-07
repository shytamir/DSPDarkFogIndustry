# Dark Fog Material Synthesis Mod Concept

This mod gives Dyson Sphere Program players an industrial route to the six native Dark Fog materials. Players synthesize them from rare resources and advanced manufactured materials, with combat research unlocking the route. A game with Dark Fog disabled can therefore retain the industrial benefits of its materials and give combat research an economic purpose.

The concept uses existing items and native production machinery. It adds recipes and their research integration while preserving resource generation, mining, existing recipes, technology costs and enemy drops. Its technical basis is the installed DSP `0.10.35.29104` code and catalogue described in [Evidence](EVIDENCE.md).

## Player experience

The player researches combat technologies, finds rare resources and builds the required industrial supply chains. Synthesis recipes appear in the normal crafting and machine recipe interfaces when their research requirements are satisfied. Production then uses ordinary machines, belts, sorters and logistics.

Manufactured materials open the native Dark Fog research and building progression. Dark Fog Matrix remains a consumable research ingredient for the relevant native technologies; the mod provides a way to manufacture it rather than granting those technologies or buildings automatically.

The same industrial route can supplement drops in a game with Dark Fog enabled. Enemy combat, drop rates and loot progression remain native. The synthesis route must be complete without enemy kills, loot, metadata buyouts, sandbox grants or another mod supplying missing ingredients.

## Materials supplied

The baseline concept provides a synthesis recipe for each existing material. Display names below are the familiar English names; numeric IDs and the exact native catalogue records are the implementation authority.

| Existing item | Native item ID | Role in the native industrial progression |
| --- | ---: | --- |
| Dark Fog Matrix | 5201 | Research ingredient for five Fog-related technologies; ingredient for the Self-evolution Lab and Dark Fog Graviton Lens |
| Silicon-based Neuron | 5202 | Discovery requirement and building ingredient for the Self-evolution Lab |
| Matter Recombinator | 5203 | Discovery requirement and building ingredient for the Re-composing Assembler |
| Negentropy Singularity | 5204 | Discovery requirement and building ingredient for the Negentropy Smelter |
| Core Element | 5205 | Discovery requirement and ingredient for the Strange Annihilation Fuel Rod |
| Energy Shard | 5206 | Ingredient for the Re-composing Assembler and Negentropy Smelter |

These roles describe the inspected research and recipe relationships, not an exhaustive list of every direct use of each item. [Catalogue records](evidence/data/catalogue.json) contain the actual arrays and quantities.

## Industrial and research progression

Rare resources should contribute materially to synthesis rather than serve as token ingredients. Advanced components should establish the industrial investment required for each tier. Different recipes can draw on different rare resources so that the route rewards resource exploration and several production chains. Exact inputs, quantities, outputs and processing times are balance decisions to be recorded before implementation.

Combat research must be an actual unlock requirement. Existing combat technologies can directly unlock recipes. Where a recipe must require both a combat technology and an industrial technology, a synthesis technology can use both as native prerequisites. Putting the recipe in both technologies' unlock lists would grant it when either completes and would not implement the intended conjunction.

There is no technical requirement to change combat research costs. The existing energy weapon damage branch illustrates available progression gates:

| Existing upgrade | Native tech ID | Required science matrices |
| --- | ---: | --- |
| Energy weapon damage I | 5101 | Blue |
| Energy weapon damage II | 5102 | Blue and red |
| Energy weapon damage III | 5103 | Blue, red and yellow |
| Energy weapon damage IV | 5104 | Blue, red, yellow and purple |
| Energy weapon damage V | 5105 | Blue, red, yellow, purple and green |
| Energy weapon damage VI and subsequent levels | 5106 | White |

These are verified candidate gates, not assigned recipe unlocks. Other inspected combat gates include technologies 1807, 1815, 1811 and 1818. The implementation should choose gates that fit the intended progression and their industrial prerequisites.

## Bootstrap requirements

Dark Fog Matrix must have a manufacturing path independent of the native Fog-related technologies that consume it for research. The other discovery materials must likewise become available without their own dependent technologies.

The native relationships are:

| Technology | Native tech ID | Material required for discovery | Research ingredient | Recipe unlocked |
| --- | ---: | --- | --- | ---: |
| Dark Fog Graviton Lens | 1509 | Dark Fog Matrix | Dark Fog Matrix | 162 |
| Digital Analog Computation | 1901 | Silicon-based Neuron | Dark Fog Matrix | 153 |
| Matter Recombination | 1902 | Matter Recombinator | Dark Fog Matrix | 154 |
| Negentropy Recursion | 1903 | Negentropy Singularity | Dark Fog Matrix | 155 |
| Strange Annihilation Fuel Rod technology | 1904 | Core Element | Dark Fog Matrix | 156 |

Retain their native ordinary technology prerequisites and research costs. For example, synthesizing Matter Recombinators cannot require the Re-composing Assembler as the only machine capable of running the recipe. Equivalent cycles through ingredients or research must also be excluded.

The baseline manufacturing path for each material must be executable in ordinary machines available before its dependent Fog buildings. Earlier synthesized Fog materials may be considered as ingredients in later tiers only if the full route remains acyclic and self-sufficient. No recipe ratios or cross-material chain have been selected here.

## Native behavior to retain

The mod should reuse existing item IDs, icons, storage behavior and downstream recipes. Synthesis uses native machine recipe execution and production statistics. It must not replace native crafting, mining, recipe selection or research queue logic merely to add these recipes.

Native discovery accepts an unlocked manufacturing recipe when the item has correctly initialized recipe links. Picking up a manufactured special item also triggers the existing discovery path. Consequently, a hidden Fog technology may become visible when its synthesis recipe unlocks, before the player physically manufactures the item. That is consistent with the inspected native item-unlock logic and does not require an enemy kill.

Handcrafting and proliferation are recipe settings. Whether synthesis permits handcrafting, extra products or acceleration is a balance choice; do not assume a default from the concept. Recipe registration must apply the selected settings consistently to tooltips and execution.

## Implementation constraints

Register stable, non-conflicting recipe IDs and valid menu positions. Populate the recipe catalogue, lookup indices, execution data, existing items' recipe links and technology unlock arrays. Establish the links before the relevant native caches are built, or rebuild only the directly affected caches at the appropriate initialization point. Appending records alone is insufficient.

The native recipe picker uses recipe unlock state and machine type. Properly registered recipes can therefore use the existing interface without a custom production panel. Existing item icons can supply recipe icons.

For existing saves, reconcile the mod's recipes against their researched unlock technologies. Native save loading restores saved recipe unlock IDs; it does not generally award newly added recipes because an older technology was already researched. Reconciliation must be idempotent and grant only recipes whose actual requirements have been met. Do not replay an entire native technology unlock and its unrelated awards or effects.

If new synthesis technologies are introduced, initialize their state for existing saves through the chosen registration mechanism and validate that separate case. The inspected LDBTool source demonstrates recipe registration and existing-save recipe reconciliation, but its current runtime compatibility has not been certified by this investigation. A dependency choice is an implementation decision, not a requirement of this concept.

Keep recipe IDs stable across releases and define removal behavior. Existing machines can retain recipe IDs in saves; reloading a save without the mod or with an incompatible recipe table must be evaluated explicitly. This route avoids persisting altered vein products, but it still adds saved recipe references.

## Scope boundaries

The concept does not introduce new resource items, vein types, planet themes or deposit models. It does not alter generator probabilities, native deposit quantities, mining throughput or Veins Utilization. It does not remove the native Fog building research requirements, grant loot, change combat strength or rebalance existing recipes.

Implementation is a separate mod, not a change to DSP Seed Scanner. The scanner repository remains in its existing maintenance state. This handoff supplies a concept and technical evidence; it does not constitute a deployed implementation or a release plan.

## Alternatives considered

Replacing fractal silicon's mined output with one existing Fog material is technically feasible because native veins separate type and appearance from the produced item. However, it requires generator or deposit conversion work, resource-label integration and an existing-save policy. The changed product ID is saved and would persist after removing that mod.

Assigning different materials across fractal deposits adds mining constraints. Native miners buffer one product, and advanced miners initialize one storage item. Uniform output within each group fits that design better than mixing items among crystals, but deposits close enough for one miner to cover several groups still require attention.

A single mined precursor with conversion recipes removes the mixed-output problem, but retains the generator and save-conversion work. Manufacturing Fog materials directly from existing resources achieves the intended peaceful industrial progression with a narrower change.

Fractal silicon was initially considered because of its low total quantity in the inspected sample. The eight selected 64-star, 1x clusters averaged about 100 million fractal silicon, 112 million organic crystal and 129 million spiniform crystal units. These were deliberately selected test seeds on DSP `0.10.35.29057`, not a representative population; they do not establish a universal rarity ranking or justify recipe ratios. The synthesis concept does not depend on that ranking.

## Implementation validation

Validate a complete manufacturing route with Dark Fog disabled, then verify the same recipes coexist with ordinary drops in a combat game. Use both a fresh game and an existing save with selected combat gates already researched.

The completed implementation should demonstrate:

- Each synthesis recipe unlocks at its selected gate and remains unavailable beforehand. Any combined prerequisite really requires both technologies.
- All six materials can be manufactured using accessible inputs and machines, without enemy drops or circular dependencies.
- The five native hidden technologies appear under the native discovery rules, consume manufactured Dark Fog Matrix for research and retain their ordinary prerequisites.
- Native machines, output logistics, production statistics, icons, tooltips, processing times and the chosen proliferation settings work for every recipe.
- Recipe availability, researched states and configured machines survive save and reload. Existing-save reconciliation does not duplicate awards or grant unrelated recipes.
- Recipe IDs and menu positions do not collide with the tested mod environment, and the documented removal behavior matches an actual save-loading test.

Static evidence supports the concept's mechanics. It does not establish plugin compilation, in-game behavior, performance, mod compatibility or balance. Exact recipes and research assignments remain intentional design choices; the easily inspected native-mechanism questions are answered in the evidence bundle.
