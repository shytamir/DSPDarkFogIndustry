# DSP Dark Fog Industry

**简体中文说明见文末。**

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

## 简体中文说明

**不开黑雾，也能量产全部六种黑雾材料。** 从分形硅石起步，搭配战斗组件，逐步生产到核心素。还可以自定义这六个配方，改为生产其他物品。

### 默认配方

配方位于「物品」页最下排的最右侧六格。完成对应科技即可解锁，已完成研究的旧存档也会解锁。

每批消耗表中**每种原料各 1 个**；能量碎片每批产出 2 个，其余每批产出 1 个。

| 产物 | 原料 | 解锁科技 |
| --- | --- | --- |
| 能量碎片 | 分形硅石 | 粒子可控 |
| 黑雾矩阵 | 能量碎片 + 护卫舰 | 护卫舰 |
| 物质重组器 | 黑雾矩阵 + 晶石炮弹组 | 晶石炮弹组 |
| 硅基神经元 | 物质重组器 + 驱逐舰 | 驱逐舰 |
| 负熵奇点 | 硅基神经元 + 引力导弹组 | 引力导弹组 |
| 核心素 | 负熵奇点 + 反物质胶囊 | 反物质胶囊 |

- **能量碎片**：电弧熔炉，120 个/分钟。
- **中间四种产物**：制造台 Mk.I，各 60 个/分钟。
- **核心素**：化工厂，60 个/分钟。

以上为供电、供料充足且不使用增产剂时的产量。高级设备按原版速度加成生产。默认配方中，黑雾矩阵和硅基神经元只能享受增产剂的「生产加速」，其余四种也支持「额外产出」。

### 自定义配方

启动一次游戏后退出，用记事本打开 `BepInEx/config/dark-fog-industry.cfg`。通过模组管理器启动时，请编辑所用配置档案（profile）中的文件。修改后重启游戏生效，并作用于该档案下游玩的所有存档。

每个编号分组对应一个配方，可修改以下内容：

- `TechnologyId`：解锁科技，需完成全部等级；多个配方可以共用。隐藏或废弃科技在正常游玩中可能无法研究。
- `MachineId`：熔炉、制造台或化工厂。同类的其他等级设备也可生产。
- `OutputItemId`、`OutputCount`、`OutputPerMinute`：产物、每批产出数量、每分钟产量。产物不限于黑雾材料，仍使用这六个配方位置。
- `Input1ItemId` / `Input1Count`、`Input2ItemId` / `Input2Count`：前两种原料及每批用量。第 3–6 种在对应的 **Extra inputs** 分组。原料不可重复；不用的原料栏将 ID 和数量均设为 `0`。

以 `Id` 结尾的选项可填 ID 或**当前游戏语言中的完整名称**，英文字母不区分大小写。请替换 `=` 后的整个值，包括原有的 `# 名称`；只改标签不会更换物品。同目录的 `dark-fog-industry.reference.txt` 列出了物品、科技和设备的名称与 ID，可用记事本搜索；重名时请用 ID。该文件随游戏启动刷新，切换语言后也请重启；修改参考文件不会改变配方。分组标题和说明仍为英文。

数量和产量均填整数。`Count` 是**每批数量**，`OutputPerMinute` 是**基础设备不使用增产剂时的每分钟产量**，以电弧熔炉、制造台 Mk.I 或化工厂为准；填写高级设备 ID 不会改变这个基准。能否使用「额外产出」取决于所选原料。

例如，沿用第一个配方的其他默认设置，将 `OutputItemId` 改为 `铁块`、`OutputCount` 改为 `1`、`OutputPerMinute` 改为 `60`、`Input1ItemId` 改为 `铁矿`，就能在电弧熔炉中以每分钟 60 个的速度用铁矿生产铁块，仍由「粒子可控」解锁。

重启后检查配置是否符合预期；如修改未按预期保存，请查看同一安装目录或配置档案中的 `BepInEx/LogOutput.log`。个人笔记请另存，配置文件的注释会重新生成。

### 手动合成与存档

制造台类配方可在合成器中手搓；冶炼和化工配方只能交给设备。手搓时需备齐原料，合成器不会代做冶炼、化工步骤。

**修改正在使用的配方前**：先用旧配置进入游戏，取出相关设备中的物品、清除已选配方、取消相关手动合成队列，然后保存。退出后再改配置并重启，在兼容的设备中重新选择配方。请同时备份存档和对应配置，否则可能丢失设备内原料，或让已有队列继续沿用旧配方。更换解锁科技不会重新锁定已解锁的配方；过大的数量或手搓队列可能超出游戏限制。

**使用这些配方的存档需要保留本模组。** 安装前请备份存档；卸载时请恢复安装前的备份。

### 安装

**模组管理器**：在 Thunderstore Mod Manager 或其他兼容管理器中安装 **DSPDarkFogIndustry** 及其依赖，并通过管理器启动游戏。

**手动安装**：关闭游戏，先安装 [戴森球计划用 BepInEx](https://thunderstore.io/c/dyson-sphere-program/p/xiaoye97/BepInEx/)（若尚未安装）。解压本模组 ZIP，在 Steam 中右键「Dyson Sphere Program → 管理 → 浏览本地文件」，把包内的 **BepInEx** 文件夹复制到 **DSPGAME.exe** 所在目录，按提示合并文件夹，再正常启动游戏。
