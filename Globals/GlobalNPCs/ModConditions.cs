//using System;
//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace LaziestNPC.Globals.GlobalNPCs
//{
//    public class ModConditions : ModSystem
//    {
//        internal static bool DownedEvilBoss = false;

//        //对应的 Condition，用于商店
//        public static readonly Condition DownedEvilBossCondition = new(
//            "EvilBoss",
//            () => DownedEvilBoss
//        );

//        //世界加载时重置状态
//        public override void OnWorldLoad() => DownedEvilBoss = false;
//        public override void OnWorldUnload() => DownedEvilBoss = false;

//        //保存到世界文件
//        public override void SaveWorldData(Terraria.ModLoader.IO.TagCompound tag)
//            => tag.Add("downedEvilBoss", DownedEvilBoss);

//        public override void LoadWorldData(Terraria.ModLoader.IO.TagCompound tag)
//            => DownedEvilBoss = tag.Get<bool>("downedEvilBoss");

//        public class EvilBossKillTracker : GlobalNPC
//        {
//            public override void OnKill(NPC npc)
//            {
//                if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.BrainofCthulhu)
//                    DownedEvilBoss = true;
//            }
//        }
//    }
//}
