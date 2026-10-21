using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LaziestNPC.Globals.GlobalNPCs
{
    public class NPCCondition : ModSystem
    {
        public static bool downedSplinterling;

        public static bool downedBrainOfCthulhu;
        public static bool downedEaterOfWorlds;

        public override void OnWorldLoad()
        {
            downedSplinterling = false;

            downedBrainOfCthulhu = false;
            downedEaterOfWorlds = false;
        }

        public override void OnWorldUnload()
        {
            downedSplinterling = false;

            downedBrainOfCthulhu = false;
            downedEaterOfWorlds = false;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedSplinterling = tag.GetBool("downedSplinterling");

            downedBrainOfCthulhu = tag.GetBool("downedBrainOfCthulhu");
            downedEaterOfWorlds = tag.GetBool("downedEaterOfWorlds");
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["downedSplinterling"] = downedSplinterling;

            tag["downedBrainOfCthulhu"] = downedBrainOfCthulhu;
            tag["downedEaterOfWorlds"] = downedEaterOfWorlds;
        }
    }

    public class DownedNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            //树精
            if (npc.type == NPCID.Splinterling)
                NPCCondition.downedSplinterling = true;

            //克苏鲁之脑
            if (npc.type == NPCID.BrainofCthulhu)
                NPCCondition.downedBrainOfCthulhu = true;
            //世界吞噬怪
            if (npc.type == NPCID.EaterofWorldsHead)
                NPCCondition.downedEaterOfWorlds = true;
        }
    }
}

