using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Globals.GlobalPlayers;
using Terraria.GameContent.ItemDropRules;

namespace LaziestNPC.Globals.GlobalNPCs
{
    //NPC关于Boss触发时的特殊台词，是否触发的状态，绑定于世界(世界中发生击杀，调用OnKill)
    public class TownLNPCGlobalNPC : GlobalNPC
    {
        //Boss等生物死亡时调用
        //全局性的，如果是Boss，多人联机下就给所有在线玩家发字符串记录，单人就自己本地。
        public override void OnKill(NPC npc)
        {
            string[] bossID = npc.type switch
            {
                NPCID.KingSlime => ["KingSlime_BossNPC"], //史莱姆王
                NPCID.EyeofCthulhu => ["EyeOfCthulhu_BossNPC"], //克苏鲁之眼
                NPCID.BrainofCthulhu => ["BrainOfCthulhu_BossNPC"], //克苏鲁之脑
                NPCID.EaterofWorldsHead or NPCID.EaterofWorldsBody or
                NPCID.EaterofWorldsTail => ["EaterOfWorlds_BossNPC"], //世界吞噬怪
                NPCID.QueenBee => ["QueenBee_BossNPC"], //蜂王
                NPCID.SkeletronHead or NPCID.SkeletronHand => ["Skeletron_BossNPC"], //骷髅王
                NPCID.Deerclops => ["Deerclops_BossNPC"], //独眼巨鹿
                NPCID.WallofFlesh => ["WallOfFlesh_BossNPC"], //血肉之墙

                NPCID.QueenSlimeBoss => ["QueenSlime_BossNPC"], //史莱姆皇后
                NPCID.TheDestroyer => ["TheDestroyer_BossNPC"], //毁灭者
                NPCID.Retinazer or NPCID.Spazmatism => ["TheTwins_BossNPC"], //双子魔眼
                NPCID.SkeletronPrime or NPCID.PrimeCannon or NPCID.PrimeSaw or 
                NPCID.PrimeVice or NPCID.PrimeLaser => ["SkeletronPrime_BossNPC"], //机械骷髅王
                NPCID.Plantera => ["Plantera_BossNPC"], //世纪之花
                NPCID.HallowBoss => ["EmpressOfLight_BossNPC"], //光之女皇
                NPCID.Golem or NPCID.GolemFistLeft
                or NPCID.GolemFistRight or NPCID.GolemHead => ["Golem_BossNPC"], //石巨人
                NPCID.DukeFishron => ["DukeFishron_BossNPC"], //猪龙鱼公爵
                NPCID.MoonLordHead or NPCID.MoonLordCore => ["MoonLord_BossNPC"], //月亮领主

                _ => null
            };

            if (bossID != null)
                {
                    //(再见了，player.active小弟)
                    foreach (var player in Main.ActivePlayers)
                    {
                        TownLNPCPlayer LTPlayer = player.LaziestTownLNPC();
                        foreach (string id in bossID)
                        {
                            //没有就添加这个Boss字符串
                            if (!LTPlayer.OnceBossChats.Contains(id))
                                LTPlayer.OnceBossChats.Add(id);
                        }
                   }
                }
        }
    }
}
