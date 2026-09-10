using LaziestNPC.Common.ModBossess;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Common.ModConditions
{
    /// <summary>
    /// 集中定义所有需要适配的模组Boss的元数据，<br/>
    /// 并提供对应的击败条件静态属性。<br/>
    /// 所有与Boss相关的注册和条件访问均在此处统一管理。
    /// </summary>
    public static class AllModBossLists
    {
        //添加所有Boss类的信息，格式：(唯一键, 模组名, Boss类名)(Boss类名是判定玩家击杀的地方，有血条的东西)
        private static readonly (string Key, string ModName, string[] BossNames)[] BossLists = new (string, string, string[])[]
        {
            #region 灾厄
            ("DesertScourge", "CalamityMod", new string[] {"DesertScourgeHead", "DesertScourgeBody", "DesertScourgeTail"}), //荒漠灾虫
            ("Crabulon", "CalamityMod", new string[] {"Crabulon" }), //菌生蟹
            ("Perforator", "CalamityMod", new string[] {"PerforatorHive" }), //血肉宿主
            ("HiveMind", "CalamityMod", new string[] {"HiveMind" }), //腐巢意志
            ("SlimeGod", "CalamityMod", new string[] {"SlimeGodCore" }), //史莱姆之神
            #endregion

            #region Fargo魂石
            ("DeviBoss", "FargowiltasSouls", new string[] {"DeviBoss" }),
            #endregion

            #region 瑟银
            ("ThunderBird", "ThoriumMod", new string[] {"TheGrandThunderBird" })
            #endregion



        };

        /// <summary>
        /// 批量注册所有Boss到ModBosses系统。<br/>
        /// 此方法在ModSystem.Load()中调用一次。
        /// </summary>
        public static void RegisterAll()
        {
            //遍历每个元组，调用ModBosses.Register完成注册。
            foreach (var list in BossLists)
            {
                ModBosses.Register(list.Key, list.ModName, list.BossNames);
            }
        }
    }
}
