using System;
using System.Collections.Generic;

namespace DSPDarkFogIndustry
{
    internal sealed class MachineChoice
    {
        internal readonly int ItemId;
        internal readonly ERecipeType Family;
        internal readonly int Speed;
        internal readonly int Slots;

        internal MachineChoice(int itemId, ERecipeType family, int speed, int slots)
        {
            ItemId = itemId;
            Family = family;
            Speed = speed;
            Slots = slots;
        }
    }

    internal sealed class RecipeCatalogue
    {
        internal readonly SortedDictionary<int, ItemProto> Items = new SortedDictionary<int, ItemProto>();
        internal readonly SortedDictionary<int, TechProto> Technologies = new SortedDictionary<int, TechProto>();
        internal readonly SortedDictionary<int, MachineChoice> Machines = new SortedDictionary<int, MachineChoice>();

        internal RecipeCatalogue(ItemProtoSet items, TechProtoSet technologies, ModelProtoSet models)
        {
            foreach (var item in items.dataArray)
            {
                // Native item-index tables have 12000 entries; zero means no item.
                if (item.ID <= 0 || item.ID >= 12000)
                {
                    continue;
                }

                Items.Add(item.ID, item);
                // ItemProto.prefabDesc is assigned later, inside item preload.
                var model = models.Select(item.ModelIndex);
                var prefab = model == null ? null : model.prefabDesc;
                if (prefab != null && prefab.isAssembler && IsAllowedFamily(prefab.assemblerRecipeType)
                    && prefab.assemblerSpeed > 0 && prefab.assemblerSpeed <= int.MaxValue / 360)
                {
                    // UpdateNeeds multiplies the accelerated speed by 180 in Int32.
                    Machines.Add(item.ID, new MachineChoice(item.ID, prefab.assemblerRecipeType, prefab.assemblerSpeed,
                        prefab.slotPoses == null ? 0 : prefab.slotPoses.Length));
                }
            }

            foreach (var technology in technologies.dataArray)
            {
                if (technology.ID > 0)
                {
                    Technologies.Add(technology.ID, technology);
                }
            }

            RequireBasicMachine(2302, ERecipeType.Smelt);
            RequireBasicMachine(2303, ERecipeType.Assemble);
            RequireBasicMachine(2309, ERecipeType.Chemical);
        }

        internal static bool IsAllowedFamily(ERecipeType family)
        {
            return family == ERecipeType.Smelt || family == ERecipeType.Assemble || family == ERecipeType.Chemical;
        }

        internal int BasicSpeed(ERecipeType family)
        {
            int id = family == ERecipeType.Smelt ? 2302 : family == ERecipeType.Assemble ? 2303 : 2309;
            return Machines[id].Speed;
        }

        private void RequireBasicMachine(int id, ERecipeType family)
        {
            MachineChoice machine;
            if (!Machines.TryGetValue(id, out machine) || machine.Family != family)
            {
                throw new InvalidOperationException("DSP Dark Fog Industry: basic machine " + id +
                    " has no initialized " + family + " production data before item preload.");
            }
        }
    }
}
