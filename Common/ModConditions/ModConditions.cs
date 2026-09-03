using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using LaziestNPC.Common;
using LaziestNPC.Common.ModConditions;

namespace LaziestNPC.Common.ModBossess
{
    public class ModConditions : ModSystem
    {
        public override void Load()
        {
            AllModBossLists.RegisterAll();
        }

        public override void OnWorldLoad()
        {
            ModBosses.ResetAll();
        }

        public override void OnWorldUnload()
        {
            ModBosses.ResetAll();
        }

        public override void SaveWorldData(TagCompound tag)
        {
            ModBosses.SaveData(tag);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            ModBosses.LoadData(tag);
        }
    }

    public class ModBossesNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            ModBosses.IsKilled(npc);
        }
    }
}
