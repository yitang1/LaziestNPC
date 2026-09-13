using LaziestNPC.Common.ModBossess;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Common.ModConditions
{
    ///<summary>
    ///集中定义所有需要适配的模组Boss的元数据，<br/>
    ///并提供对应的击败条件静态属性。<br/>
    ///所有与Boss相关的注册和条件访问均在此处统一管理。
    ///</summary>
    public static class AllModBossLists
    {
        //添加所有Boss类的信息，格式：(唯一键(标识), 模组名, Boss类名)(Boss类名是判定玩家击杀的、有血条的实体)
        private static readonly (string Key, string ModName, string[] BossNames)[] BossLists = new (string, string, string[])[]
        {
            #region 灾厄 Calamity Mod
            ("DesertScourge", "CalamityMod", new string[] {"DesertScourgeHead", "DesertScourgeBody", "DesertScourgeTail"}), //荒漠灾虫
            ("Crabulon", "CalamityMod", new string[] {"Crabulon"}), //菌生蟹
            ("Perforator", "CalamityMod", new string[] {"PerforatorHive"}), //血肉宿主
            ("HiveMind", "CalamityMod", new string[] {"HiveMind"}), //腐巢意志
            ("PerOrHive", "CalamityMod", new string[] { "PerforatorHive", "HiveMind"}), //血肉宿主 或 腐巢意志
            ("SlimeGod", "CalamityMod", new string[] {"SlimeGodCore"}), //史莱姆之神
            
            ("Cryogen", "CalamityMod", new string[] { "Cryogen"}), //极地之灵
            ("AquaticScourge", "CalamityMod", new string[] { "AquaticScourgeHead", "AquaticScourgeBody", "AquaticScourgeBodyAlt", "AquaticScourgeTail"}), //渊海灾虫
            ("Brimstone", "CalamityMod", new string[] { "BrimstoneElemental"}), //硫磺火元素
            ("AstrumAureus", "CalamityMod", new string[] { "AstrumAureus"}), //白金星舰
            ("Calamitas", "CalamityMod", new string[] { "CalamitasClone"}), //灾厄之影
            ("Leviathan", "CalamityMod", new string[] { "Leviathan", "Anahita"}), //阿娜希塔和利维坦
            ("Plague", "CalamityMod", new string[] { "PlaguebringerGoliath"}), //瘟疫使者歌莉娅
            ("Ravager", "CalamityMod", new string[] { "RavagerBody"}), //毁灭魔像
            ("AstrumDeus", "CalamityMod", new string[] { "AstrumDeusHead", "AstrumDeusBody", "AstrumDeusTail"}), //星神游龙

            ("Dragonfolly", "CalamityMod", new string[] { "Dragonfolly"}), //痴愚金龙
            ("Guardians", "CalamityMod", new string[] { "ProfanedGuardianCommander"}), //亵渎守卫
            ("Providence", "CalamityMod", new string[] { "Providence"}), //亵渎天神
            ("StormWeaver", "CalamityMod", new string[] { "StormWeaverHead", "StormWeaverBody", "StormWeaverTail"}), //风暴编织者
            ("CeaselessVoid", "CalamityMod", new string[] { "CeaselessVoid", "DarkEnergy"}), //无尽虚空
            ("Signus", "CalamityMod", new string[] { "Signus"}), //西格纳斯
            ("Polterghast", "CalamityMod", new string[] { "Polterghast"}), //噬魂幽花
            ("OldDuke", "CalamityMod", new string[] { "OldDuke"}), //硫海遗爵
            ("DOG", "CalamityMod", new string[] { "DevourerofGodsHead", "DevourerofGodsBody", "DevourerofGodsTail"}), //神明吞噬者
            ("Yharon", "CalamityMod", new string[] { "Yharon"}), //犽戎
            ("ExoMechs", "CalamityMod", new string[] { "Artemis", "Apollo", "AresBody", "AresGaussNuke", "AresLaserCannon", "AresPlasmaFlamethrower", "AresTeslaCannon", "ThanatosHead", "ThanatosBody1", "ThanatosBody2", "ThanatosTail"}), //星流巨械
            ("SCalamitas", "CalamityMod", new string[] { "SupremeCalamitas"}), //终灾
            
            #endregion

            #region 灾劫 Catalyst Mod
            ("Astrageldon", "CatalystMod", new string[] { "Astrageldon"}), //末世星史莱姆

            #endregion

            #region Fargo魂石 Fargo's Souls Mod
            ("TrojanSquirrel", "FargowiltasSouls", new string[] {"TrojanSquirrel"}), //特洛伊松鼠
            ("CursedCoffin", "FargowiltasSouls", new string[] {"CursedCoffin"}), //咒缚灵棺
            ("DeviBoss", "FargowiltasSouls", new string[] {"DeviBoss"}), //戴薇安
            ("BanishedBaron", "FargowiltasSouls", new string[] {"BanishedBaron"}), //放逐遗爵
            ("Lifelight", "FargowiltasSouls", new string[] {"LifeChallenger"}), //飘渺游光
            ("CosmosChampion", "FargowiltasSouls", new string[] {"CosmosChampion"}), //宇宙英灵
            ("AbomBoss", "FargowiltasSouls", new string[] {"AbomBoss"}), //憎恶
            ("MutantBoss", "FargowiltasSouls", new string[] {"MutantBoss"}), //突变体

            #endregion

            #region 瑟银 Thorium Mod
            ("ThunderBird", "ThoriumMod", new string[] {"TheGrandThunderBird"}),
            #endregion

            #region 旅人归途 Homeward Journey
            ("Thunderird", "ContinentOfJourney", new string[] {"TheGranThunderBird"}),
            #endregion

            #region 救赎 Mod of Redemption
            ("Thunderird", "Redemption", new string[] {"TheGranThunderBird"})
            #endregion



            #region 
            #endregion

        };

        ///<summary>
        ///批量注册所有Boss到ModBosses系统。<br/>
        ///此方法在ModSystem.Load()中调用一次。
        ///</summary>
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
