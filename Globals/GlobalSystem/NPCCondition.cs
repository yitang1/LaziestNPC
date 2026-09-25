using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LaziestNPC.Globals.GlobalSystem
{
    public class NPCCondition : ModSystem
    {
        public static bool downedSplinterling;

        public override void OnWorldLoad() => downedSplinterling = false;
        public override void OnWorldUnload() => downedSplinterling = false;
        public override void SaveWorldData(TagCompound tag) => tag["downedSplinterlingling"] = downedSplinterling;
        public override void LoadWorldData(TagCompound tag) => downedSplinterling = tag.GetBool("downedSplinterlingling");
    }

    public class DownedNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.Splinterling)
                NPCCondition.downedSplinterling = true;
        }
    }
}

