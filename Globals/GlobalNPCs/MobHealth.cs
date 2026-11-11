using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Globals.Config;

namespace LaziestNPC.Globals.GlobalNPCs
{
    public class MobHealth : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            //以倍率提高除了城镇NPC和小动物之外所有敌怪的血量
            if (!npc.CountsAsACritter && !npc.townNPC)
            {
                double Mult = ModContent.GetInstance<LNPCConfig>().MobHealth;
                npc.lifeMax = (int)(npc.lifeMax * Mult);
            }
        }
    }
}
