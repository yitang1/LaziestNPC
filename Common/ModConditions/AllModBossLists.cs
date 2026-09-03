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
        //所有Boss元数据条目，格式：(唯一键, 模组名, Boss类名)
        private static readonly (string Key, string ModName, string[] BossNames)[] BossLists = new (string, string, string[])[]
        {
            #region 灾厄
            ("CalamityMod/DesertScourge", "CalamityMod", new string[] {"DesertScourgeHead", "DesertScourgeBody", "DesertScourgeTail"}),
            ("CalamityMod/Crabulon", "CalamityMod", new string[] {"Crabulon" }),
            #endregion

            #region Fargo魂石
            ("FargowiltasSouls/DeviBoss", "FargowiltasSouls", new string[] {"DeviBoss" }),
            #endregion

            #region 瑟银
            ("ThoriumMod/TheGrandThunderBird", "ThoriumMod", new string[] {"TheGrandThunderBird" }),
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
