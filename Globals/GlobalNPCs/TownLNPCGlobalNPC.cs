using LaziestNPC.Common.Helpers;
using LaziestNPC.Globals.GlobalPlayers;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalNPCs
{
    //NPC关于Boss触发时的特殊台词，是否触发的状态，绑定于世界(世界中发生击杀，调用OnKill)
    public class TownLNPCGlobalNPC : GlobalNPC
    {
        //Boss等生物死亡时调用
        //全局性的，如果是Boss，多人联机下就给所有在线玩家发字符串记录，单人就自己本地。
        public override void OnKill(NPC npc)
        {
            string bossID = null;

            if (npc.type == NPCID.KingSlime) bossID = "KingSlime";
            else if (npc.type == NPCID.EyeofCthulhu) bossID = "EyeOfCthulhu";

            else if (npc.type == NPCID.MoonLordCore) bossID = "MoonLord";


            if (bossID != null)
            {
                //(再见了，player.active小弟)
                foreach (var player in Main.ActivePlayers)
                {
                    TownLNPCPlayer LTPlayer = player.LaziestTownLNPC();
                    //没有就添加这个Boss字符串
                    if (!LTPlayer.OnceBossChats.Contains(bossID))
                    {
                        LTPlayer.OnceBossChats.Add(bossID);
                    }
                }
            }
        }
    }
}
