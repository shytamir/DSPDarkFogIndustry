# Configuration contract

[PROJECT](../PROJECT.md) records decisions and acceptance. This document defines
the configuration behavior, not work status or implementation algorithms. The
[archived investigation](../archive/2026-10-13-configurability/INVESTIGATION.md)
records native findings and their evidence limits;
[native integration](../implementation/NATIVE-INTEGRATION.md) points to the implementation.

## Owner-selected behavior

- Each recipe has one output item type, with an editable item ID and integer
  count. Ingredient item IDs/counts, unlock technology, machine and output rate
  are also configurable. Selectors accept IDs or exact names in the game's current
  language, ignoring case and surrounding whitespace. Saved IDs retain identity.
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
- The generated config must stay within 200 lines. Aim to keep all six recipes'
  core controls in the first 60 lines; this is a usability target, not a reason
  for complex formatting. Inputs 3–6 follow in extra-input sections. Keep the
  grouped reference in a separate text file. Custom dropdowns and manager lookup
  features are excluded. Preserve every current configurable field and default.

## Recipe and file contract

Keep recipe IDs 401–406 and selector positions 1809–1814. Defaults reproduce the
[MVP recipes](MVP-RECIPES.md), including the two Energy Shards per batch. These
identities are not user-configurable. Item output choices are configurable; the
six defaults do not become a restriction on selectable output items.

Use the plugin's ordinary `BepInEx/config/dark-fog-industry.cfg`, in the selected
mod-manager profile when applicable. One numbered section identifies each recipe
by stable recipe ID and original/default name. An output change does not rename
its section and strand the player's settings. Use ordinary scalar entries and
shared instructions, not JSON, an external recipe file or a custom UI. Fields keep
their existing keys, including the `Id` suffix, to preserve existing configurations.
Inputs 3–6 move to `Extra inputs - <original section>`; preserve old values and
prefer the new location if both locations contain a value.

| Field | Meaning |
| --- | --- |
| `TechnologyId` | An existing technology record. The recipe unlocks when that record is fully completed under native research rules. Multiple recipes may use the same ID. Zero is not a no-research sentinel. |
| `MachineId` | A smelter, chemical plant or assembler item ID. It selects the native recipe family. Rates always use that family's basic machine, regardless of the selected tier; other compatible tiers remain usable. |
| `OutputItemId`, `OutputCount` | One existing output item and its positive integer quantity per completed batch. |
| `OutputPerMinute` | Positive integer products/minute in the selected family's basic machine, before proliferation; count and rate are distinct. Native timing may make an entered rate unattainable; the correction rule below covers that case. |
| `Input1ItemId`, `Input1Count` through `Input6ItemId`, `Input6Count` | Up to six ordered ingredient rows. An unused row has both values zero. Active rows use an existing item ID and a positive integer count. |

Six rows reflect native input demand positions. The investigation records their
consumer bounds. Zero through six distinct active
inputs are allowed, including an input that is also the output. For a duplicate
active ID, keep the earlier row and restore the later row's default ID. If that
default is already used, clear the later row. Do not choose an unrelated ingredient.
There is no separate recipe-disable switch.

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

This excerpt illustrates the layout and current default values. The
implementation must retain every field. Short labels share the selector's line;
shared instructions follow the extra inputs.

```ini
[01 - Recipe 401 - Energy Shard]
TechnologyId = 1133 # Particle Control
MachineId = 2302 # Arc Smelter
OutputItemId = 5206 # Energy Shard
OutputCount = 2
OutputPerMinute = 120
Input1ItemId = 1013 # Fractal Silicon
Input1Count = 1
Input2ItemId = 0 # unused
Input2Count = 0
```

To enter a name, replace the whole selector value, for example
`Input1ItemId = fractal silicon`. If the value contains an integer followed by
`#`, that integer is authoritative and the following label is informational.
After resolution, save `ID # current name` so changing the game language does not
retarget a selection. Name matching uses only the current game language; there
is no translation service, spelling correction, partial matching or invented alias.
Repeated technology names require IDs. Unknown, ambiguous or ineligible selections
restore their own documented defaults and log the field, original value and result.

Shared comments explain units, zero rows, restart requirements and the reference
filename. Preserve unrelated unknown keys. Only the obsolete mod-owned reference
sections are removed. The 200-line limit applies to generated mod content, not
arbitrary additions by the player. Handwritten comments are not retained.

## Correction policy

The owner review adjustment supersedes nearest-ID selection with default fallback.
Quantity/rate correction and CFG-D13's native boundary remain unchanged.
Corrections must settle after one pass and preserve already valid selections and quantities.

| Situation | Behavior |
| --- | --- |
| Missing setting | Supply that setting's documented default. This is normal initialization, not a reason to replace an entire recipe. |
| Unknown, ambiguous or ineligible item/technology/machine selection | Restore that field's documented default and log the correction. No name or numeric-ID guessing. Ingredient ID zero remains an explicit unused slot. |
| Numeric count outside the proven domain | Use the closest valid integer for the selected recipe/machine; choose the lower value on a tie. Preserve other valid fields. |
| Quantity/rate text larger than the parser's ordinary integer range | Correct by numeric magnitude to the nearest valid boundary. |
| Fractional quantity/rate | Choose the nearest valid integer, with the same lower-value tie rule. Selectors require an integer ID or exact name. |
| Empty or nonnumeric quantity/rate | Use that field's documented default and explain that no numeric distance exists. Optional unused ingredient rows still have their explicit zero representation. |
| Rate not exactly attainable | Choose the closest attainable integer products/minute value in the selected family's basic machine for the output count; choose the lower rate on a tie. Report requested and effective rates. Do not silently change output or ingredient counts to force a rate. |
| Conflicting fields or ingredient rows | Resolve selections, then repair duplicate rows and quantities, then rate. An unused row keeps item ID zero and receives count zero. Duplicates use the later row's default or zero as described above. |
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

Write `dark-fog-industry.reference.txt` beside the config, grouped into items,
technologies and eligible machines. Use one line per record: ID, native readable
name and short notes. This is lookup data, not editable recipe settings. Include
family/speed, maximum-level
or eligibility notes without presenting obsolete or hidden records as ordinary
research progression. The machine group contains only eligible smelters, chemical
plants and assemblers; identify each family's basic rate baseline and each tier's
speed relative to it. Keep the wider item reference distinct from machine eligibility.

Generate the reference from the same native snapshot used for validation, using
the game's active language for names. IDs remain stable when language changes.
Label conditional restrictions rather than implying that every ID works in every
field. Verify completeness against the accepted domain and keep the reference
out of the correction inputs. The full catalogue must not be restored as config
settings. No mod-manager dropdown or grouped lookup behavior is promised.

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
