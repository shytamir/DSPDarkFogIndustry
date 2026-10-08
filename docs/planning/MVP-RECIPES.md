# Recipe requirements

This records the owner's recipe decisions and clarifications for the first MVP.
[PROJECT.md](../PROJECT.md) owns their disposition and the roadmap's authorization.
The original [concept](../concept/CONCEPT.md) remains a dated source document;
these later decisions resolve its open recipe choices without rewriting it.

## Six fixed recipes

Each operation consumes one of each listed input. All results use existing native
items. No additional ingredients, balance multipliers, recipes, or research costs
are introduced.

| Product | Inputs per operation | Output per operation | Existing technology gate | Facility family | Handcraft |
| --- | --- | ---: | --- | --- | --- |
| Energy Shard | 1 Fractal Silicon | 2 | Particle Control | Smelter | No |
| Dark Fog Matrix | 1 Energy Shard + 1 Corvette | 1 | Corvette | Assembler | Yes |
| Matter Recombinator | 1 Dark Fog Matrix + 1 Crystal Shell Set | 1 | Crystal Shell Set | Assembler | Yes |
| Silicon-based Neuron | 1 Matter Recombinator + 1 Destroyer | 1 | Destroyer | Assembler | Yes |
| Negentropy Singularity | 1 Silicon-based Neuron + 1 Gravity Missile Set | 1 | Gravity Missile Set | Assembler | Yes |
| Core Element | 1 Negentropy Singularity + 1 Antimatter Capsule | 1 | Antimatter Capsule | Chemical plant | No |

The shortened final four rows in the owner's table are resolved using the explicit
rule that the gate is the technology unlocking the listed combat component.
Fractal Silicon is the sole input to the non-combat Energy Shard exception.

## Rate and native behavior

At full power with continuous input/output, without proliferation:

- Arc Smelter: 120 Energy Shards/minute from 60 Fractal Silicon/minute.
- Assembler Mk.I: 60 products/minute for each assembler recipe, consuming 60/minute
  of each input. The owner explicitly chose Mk.I output as the baseline.
- Chemical Plant: 60 Core Elements/minute, consuming 60/minute of each input.

Higher-tier facilities retain their usual speed bonuses. Handcrafting uses the
Replicator's usual timing; its rate is not separately normalized to the Mk.I rate.

Use normal proliferation and power behavior. Dark Fog Matrix and Silicon-based
Neuron support speedup only because of their ship ingredients. The other four
recipes also support extra products.

## Selector placement and unlocks

Use the Items recipe tab, bottom row, rightmost six slots, in the table's order
from left to right. Leave the science-cube recipes and other item positions unchanged.

Each synthesis recipe has one existing research unlock, with no new technologies
or additional prerequisites. A gate may
be researched before the previous material is available; that unlocks the recipe
but does not grant its ingredients. Manufacturing order comes from the input chain.

Existing native discovery can reveal downstream Fog technologies when an unlocked
synthesis recipe makes an item discoverable. Do not add a physical-first-production
requirement. Preserve native research prerequisites and Matrix costs: synthesis
must not automatically grant the five downstream technologies or their buildings.

## Boundaries

The complete route must be feasible without enemy drops, metadata purchases,
sandbox grants, or another mod supplying missing resources. Ordinary raw resources
and native manufacturing remain necessary. Both new games and pre-existing saves
with gates already researched are in the MVP. Existing enemy drops remain native.

IDs, durations and registration details live in the
[source](../../src/DSPDarkFogIndustry/SynthesisRecipes.cs). The
[integration guide](../implementation/NATIVE-INTEGRATION.md) links code and dated
evidence. Work definitions belong in the [roadmap](ROADMAP.md); these requirements
do not independently authorize work.
