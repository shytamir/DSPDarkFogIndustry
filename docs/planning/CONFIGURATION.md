# Configuration contract

[PROJECT](../PROJECT.md) records which requirements the owner has selected and
which proposals still await approval. This document defines behavior for the
[roadmap](ROADMAP.md), not work status or implementation algorithms. Native facts
and remaining proofs are in the [investigation](../implementation/CONFIGURATION-INVESTIGATION.md).

## Owner-selected behavior

- Each recipe has one output item type, with an editable item ID and integer
  count. Ingredient item IDs/counts, unlock technology, machine and output rate
  are also configurable. Settings use IDs rather than localized names as identity.
- Only smelters, chemical plants and assemblers are selectable production
  machines. Labs, refineries, particle colliders and all other families are excluded.
- The integer output rate means products per minute in the selected family's
  basic machine, without proliferation and at full power/supply: Arc Smelter,
  Chemical Plant or Assembling Machine Mk.I. Selecting an upgraded tier's ID does
  not change the rate baseline. Compatible tiers retain their normal speed
  differences. Hand replication keeps its own native timing.
- Assembler-family recipes permit hand replication; all other families are
  machine-only. This retains the mod's rule; it is not a claim that every vanilla
  recipe follows it. There is no independent handcraft toggle.
- Keep native save loading. Provide preparation instructions; do not add resource
  migration, queue rewriting or automatic refunds when recipes change.
- The config must be usable in Notepad, end with a reference grouped by ID type,
  and support lookup in Thunderstore Mod Manager. Reference-only settings are
  allowed. Valid settings must survive; invalid settings receive specific
  automatic nearest-valid corrections.

## Recipe and file contract

Keep recipe IDs 401–406 and selector positions 1809–1814. Defaults reproduce the
[MVP recipes](MVP-RECIPES.md), including the two Energy Shards per batch. These
identities are not user-configurable. Item output choices are configurable; the
six defaults do not become a restriction on selectable output items.

Use the plugin's ordinary `BepInEx/config/dark-fog-industry.cfg`, in the selected
mod-manager profile when applicable. One numbered section identifies each recipe
by stable recipe ID and original/default name. An output change does not rename
its section and strand the player's settings. Use ordinary scalar entries and
short generated descriptions, not JSON, an external recipe file or a custom UI.

| Field | Meaning |
| --- | --- |
| `TechnologyId` | An existing technology record. The recipe unlocks when that record is fully completed under native research rules. Multiple recipes may use the same ID. No zero/no-research sentinel is proposed. |
| `MachineId` | A smelter, chemical plant or assembler item ID. It selects the native recipe family. Rates always use that family's basic machine, regardless of the selected tier; other compatible tiers remain usable. |
| `OutputItemId`, `OutputCount` | One existing output item and its positive integer quantity per completed batch. |
| `OutputPerMinute` | Positive integer products/minute in the selected family's basic machine, before proliferation; count and rate are distinct. Native timing may make an entered rate unattainable; the correction rule below covers that case. |
| `Input1ItemId`, `Input1Count` through `Input6ItemId`, `Input6Count` | Up to six ordered ingredient rows. An unused row has both values zero. Active rows use an existing item ID and a positive integer count. |

Six rows reflect native input demand positions. The investigation records their
consumer bounds. Zero through six distinct active
inputs are allowed, including an input that is also the output. For a duplicate
active ID, keep the earlier row and correct the later ID to the nearest eligible
item not already used, choosing the lower ID on a tie. If no unused eligible item
exists, clear that row. There is no separate recipe-disable switch.

### Allowed machines and rate baselines

| Family | Basic machine used for `OutputPerMinute` | Eligible native machine item IDs, in tier order |
| --- | --- | --- |
| Smelting | Arc Smelter — 2302 | 2302, 2315, 2319 |
| Chemical production | Chemical Plant — 2309 | 2309, 2317 |
| Assembly | Assembling Machine Mk.I — 2303 | 2303, 2304, 2305, 2318 |

These IDs are backed by the inspected native catalogue; they are not a manually
maintained substitute for the implementation's validated catalogue. Every allowed
tier selects its family, not an exclusive building requirement or a different
rate baseline. For example, setting an assembly recipe to 60/min yields 60/min in
Mk.I and 80/min in Mk.II before proliferation, even if `MachineId` names Mk.II.
The Mk.I's native 0.75 speed must not be mistaken for a 1× baseline.

Only these three families are in scope. Do not investigate or implement labs,
refineries, particle colliders, fractionators, ray receivers, exchangers or other
production families for this roadmap. Their building items may still be eligible
ingredients or outputs; exclusion applies to the `MachineId` setting.

An item/technology record is not rejected merely because it is hidden, obsolete,
late-game, hard to obtain or creates an inconvenient progression route. Within
the agreed product scope, apply only proven native constraints. Eligible content
supplied by another mod is usable if present at the registration point and satisfies
the same constraints; a machine must belong to one of the three allowed families.
This does not promise arbitrary load-order or mod-combination compatibility.

Automatic correction covers recipe fields and machine operation. Recursive
handcrafting, raw-material previews and accumulated queues retain their native
arithmetic limits. Large quantities across recipe chains can exceed those limits;
the mod does not analyze or rewrite chains/queues to prevent that. This boundary
does not weaken the per-recipe duration, machine-buffer or item-ID checks.

### Readability example

This excerpt illustrates the proposed layout and current default values. The
implementation must produce all six sections, all ingredient rows and normal
BepInEx metadata; this is not an installable config or a completed file-format test.

```ini
[01 - Recipe 401 - Energy Shard]

## Technology required to unlock this recipe. See the technology reference below.
TechnologyId = 1133

## Machine family: smelting. Rates use Arc Smelter even if an upgraded ID is selected.
MachineId = 2302

OutputItemId = 5206
OutputCount = 2

## Products per minute in this family's basic machine, before proliferation.
OutputPerMinute = 120

Input1ItemId = 1013
Input1Count = 1

## Zero in both fields means this ingredient row is unused.
Input2ItemId = 0
Input2Count = 0
```

Generated descriptions explain units, zero rows, restart requirements and how to
find IDs. Keep recipe settings before reference sections. Preserve valid unknown
keys through ordinary BepInEx handling. BepInEx regenerates comments when saving;
tell players that handwritten comments are not preserved. The mod's descriptions
must return on normal saves. Reference ordering refers to mod-owned sections;
arbitrary user-created sections are not silently deleted to force an order.

## Correction policy

These rules were approved at G0. CFG-I01 supplies domains within CFG-D13's scope
and numerical examples, not new balance choices. Corrections must settle after
one pass and leave an already valid configuration unchanged.

| Situation | Behavior |
| --- | --- |
| Missing setting | Supply that setting's documented default. This is normal initialization, not a reason to replace an entire recipe. |
| Numeric item/technology/machine ID that is absent or ineligible | Choose the eligible ID with minimum absolute numeric distance; choose the lower ID on a tie. An intentional zero ingredient row remains distinct from an invalid real-item selection. |
| Numeric count outside the proven domain | Use the closest valid integer for the selected recipe/machine; choose the lower value on a tie. Preserve other valid fields. |
| Integer text larger than the parser's ordinary integer range | Correct by numeric magnitude to the nearest valid boundary. Do not silently treat overflow as a request for the default. |
| Fractional numeric value in an integer field | Choose the nearest valid integer, with the same lower-value tie rule. |
| Empty or nonnumeric value in a required numeric field | Use that field's documented default and explain that no numeric distance exists. Optional unused ingredient rows still have their explicit zero representation. |
| Rate not exactly attainable | Choose the closest attainable integer products/minute value in the selected family's basic machine for the output count; choose the lower rate on a tie. Report requested and effective rates. Do not silently change output or ingredient counts to force a rate. |
| Conflicting fields or ingredient rows | Preserve an eligible selected machine first; choose output/input IDs within its eligible domain, then repair row shape and quantities, then rate. An intentional unused row keeps item ID zero and receives count zero. Define duplicate/empty-row treatment from CFG-I01 before coding, with an example for each repair; avoid repeatedly correcting one field back and forth. |
| Lookup/reference edit | Never use lookup names or values to define recipes. Regenerate native labels on the next ordinary mod save. |

This is field-by-field correction under the stated precedence, not a claim of a
unique nearest whole recipe. If a native constraint requires a different priority,
return that product choice to the owner rather than choosing it in implementation.
Repair only the affected fields, write back the effective configuration, and log
section/key, original value, effective value and reason. Preserve valid content
from the same file. Defaults used for missing or nonnumeric fields still pass
through the same constraints before the effective configuration is published.
An inability to persist corrections must be clearly reported;
it is not successful persistence or justification to overwrite another file.

Automatic correction applies to setting values in a parseable
BepInEx file. Damaged section/key syntax and filesystem failures may occur before
the plugin is constructed. They remain explicit file errors rather than a promise
of automatic file reconstruction, as approved at G0. A broader repair guarantee
would need a separate, justified loading design.

## ID reference

Append ordinary sections named so they sort after recipe settings:
`90 - Reference - Items`, `91 - Reference - Technologies` and
`92 - Reference - Machines`. Use an inert entry per ID, with the numeric ID as key
and a native readable name as value. Each group explains that it is lookup data,
not editable recipe settings. Where relevant, include family/speed, maximum-level
or eligibility notes without presenting obsolete or hidden records as ordinary
research progression. The machine group contains only eligible smelters, chemical
plants and assemblers; identify each family's basic rate baseline and each tier's
speed relative to it. Keep the wider item reference distinct from machine eligibility.
Do not invent true subgroups that BepInEx does not provide.

Generate the reference from the same native snapshot used for validation, using
the game's active language for names. IDs remain stable when language changes.
Label conditional restrictions rather than implying that every ID works in every
field. Verify completeness against the accepted domain and keep the reference
out of the correction inputs. The exact manager version and observed rendering
are recorded at final handoff; Notepad or r2modman alone cannot establish that result.

## Applying settings and changing existing saves

Settings apply once per game process at startup, before recipe caches are built.
An edit while DSP is running takes effect after a full restart. A configuration
applies to every save loaded with that profile; it is not embedded independently
in each save. Existing unlocked recipes stay unlocked if the gate changes. A new
or previously locked recipe follows its currently configured completed-technology
gate; no other technology awards are replayed.

Before changing the ingredients, output, quantities or machine family of a recipe
already in use, the player should use the old configuration to stop and empty the
affected facilities, clear their selected recipes, cancel affected handcrafts,
and save. Then exit DSP, edit the config, restart and select the recipes in
compatible facilities. Keep a checkpoint with its matching configuration when
reversibility matters. The final owner procedure must verify these steps on a
disposable checkpoint; the agent must not access saves to prepare it.

Native import may clear buffers when item IDs/order change and may retain queued
work's old contents/timing. No lossless migration is promised if preparation is
skipped. Changing a gate does not revoke saved unlocks, and changing the configured
machine does not convert an existing building. Documentation must distinguish
these consequences from invalid settings or a failed correction.
