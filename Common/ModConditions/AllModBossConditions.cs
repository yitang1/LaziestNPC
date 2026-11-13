using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Common.ModBossess;
using LaziestNPC.Globals.GlobalNPCs;
using LaziestNPC.Globals.GlobalSystem;

namespace LaziestNPC.Common.ModConditions
{
    //所有敌怪的击败条件
    public static class AllModBossConditions
    {
        #region 原版
        public static readonly Condition BloodMoonHappened = new("BloodMoonHappened", () => WorldCondition.BloodMoonHappened); //发生过血月
        public static readonly Condition GoblinArmyHappened = new("GoblinArmyHappened", () => WorldCondition.GoblinArmyHappened); //发生过哥布林军队
        public static readonly Condition EclipseHappened = new Condition("EclipseHappened", () => WorldCondition.EclipseHappened); //发生过日食
        public static readonly Condition SnowMoonHappened = new Condition("SnowMoonHappened", () => WorldCondition.SnowMoonHappened); //发生过霜月
        public static readonly Condition PumpkinMoonHappened = new Condition("PumpkinMoonHappened", () => WorldCondition.PumpkinMoonHappened); //发生过南瓜月
        
        public static readonly Condition DownedBrainOfCthulhu = new Condition("DownedBrainOfCthulhu", () => NPCCondition.downedBrainOfCthulhu); //克苏鲁之脑
        public static readonly Condition DownedEaterOfWorlds = new Condition("DownedEaterOfWorlds", () => NPCCondition.downedEaterOfWorlds); //世界吞噬怪
        public static readonly Condition DownedDarkMage = new Condition("DownedDarkMage", () => NPCCondition.downedDarkMage); //黑暗魔法师
        public static readonly Condition DownedOgre = new Condition("DownedOgre", () => NPCCondition.downedOgre); //食人魔
        public static readonly Condition DownedBetsy = new Condition("DownedBetsy", () => NPCCondition.downedBetsy); //双足翼龙
        //public static Condition MourningWoodOrSplinter = new Condition("MourningWoodOrSplinter", () => Condition.DownedMourningWood.IsMet() || NPCCondition.downedSplinterling); //哀木 / 树精
        
        public static readonly Condition DownedAnyPillar = new Condition("DownedPillar", () =>
        Condition.DownedSolarPillar.IsMet() || Condition.DownedVortexPillar.IsMet()
        || Condition.DownedNebulaPillar.IsMet() || Condition.DownedStardustPillar.IsMet()); //任意一个天界柱

        #endregion

        #region 灾厄 Calamity Mod
        //其他
        public static readonly Condition KingSlimeOrDesertBug = new Condition("KingSlimeOrDesertBug", () => Condition.DownedKingSlime.IsMet() || DownedDesertBug.IsMet()); //史莱姆王或荒漠灾虫
        //肉前
        public static readonly Condition DownedDesertBug = ModBosses.GetCondition("DesertScourge"); //荒漠灾虫
        public static readonly Condition DownedCrabulon = ModBosses.GetCondition("Crabulon"); //菌生蟹
        public static readonly Condition DownedPerforator = ModBosses.GetCondition("Perforator"); //血肉宿主
        public static readonly Condition DownedHiveMind = ModBosses.GetCondition("HiveMind"); //腐巢意志
        public static readonly Condition PerOrHive = ModBosses.GetCondition("PerOrHive"); //血肉宿主 或 腐巢意志
        public static readonly Condition DownedSlimeGod = ModBosses.GetCondition("SlimeGod"); //史莱姆之神
        //肉后
        public static readonly Condition DownedCryogen = ModBosses.GetCondition("Cryogen"); //极地之灵
        public static readonly Condition DownedAquaticBug = ModBosses.GetCondition("AquaticScourge"); //渊海灾虫
        public static readonly Condition DownedBrimstone = ModBosses.GetCondition("Brimstone"); //硫磺火元素
        public static readonly Condition DownedAstrum = ModBosses.GetCondition("AstrumAureus"); //白金星舰
        public static readonly Condition DownedCalamitas = ModBosses.GetCondition("Calamitas"); //灾厄之影
        public static readonly Condition DownedLeviathan = ModBosses.GetCondition("Leviathan"); //阿娜希塔和利维坦
        public static readonly Condition DownedPlague = ModBosses.GetCondition("Plague"); //瘟疫使者歌莉娅
        public static readonly Condition DownedRavager = ModBosses.GetCondition("Ravager"); //毁灭魔像
        public static readonly Condition DownedAstrumBug = ModBosses.GetCondition("AstrumDeus"); //星神游龙
        //月后
        public static readonly Condition DownedDragonfolly = ModBosses.GetCondition("Dragonfolly"); //痴愚金龙
        public static readonly Condition DownedGuardians = ModBosses.GetCondition("Guardians"); //亵渎守卫
        public static readonly Condition DownedProvidence = ModBosses.GetCondition("Providence"); //亵渎天神
        public static readonly Condition DownedStormWeaver = ModBosses.GetCondition("StormWeaver"); //风暴编织者
        public static readonly Condition DownedVoid = ModBosses.GetCondition("CeaselessVoid"); //无尽虚空
        public static readonly Condition DownedSignus = ModBosses.GetCondition("Signus"); //西格纳斯
        public static readonly Condition DownedPolterghast = ModBosses.GetCondition("Polterghast"); //噬魂幽花
        public static readonly Condition DownedOldDuke = ModBosses.GetCondition("OldDuke"); //硫海遗爵
        public static readonly Condition DownedDOG = ModBosses.GetCondition("DOG"); //神明吞噬者
        public static readonly Condition DownedYharon = ModBosses.GetCondition("Yharon"); //犽戎
        public static readonly Condition DownedDraedon = ModBosses.GetCondition("ExoMechs"); //星流巨械
        public static readonly Condition DownedSCala = ModBosses.GetCondition("SCalamitas"); //终灾

        #endregion

        #region 灾劫 Catalyst Mod
        public static readonly Condition DownedAstrageldon = ModBosses.GetCondition("Astrageldon"); //末世星史莱姆

        #endregion

        #region Fargo魂石 Fargo's Souls Mod
        public static readonly Condition DownedTrojanSquirrel = ModBosses.GetCondition("TrojanSquirrel"); //特洛伊松鼠
        public static readonly Condition DownedCursedCoffin = ModBosses.GetCondition("CursedCoffin"); //咒缚灵棺
        public static readonly Condition DownedDevi = ModBosses.GetCondition("DeviBoss"); //戴薇安
        public static readonly Condition DownedBanishedBaron = ModBosses.GetCondition("BanishedBaron"); //放逐遗爵
        public static readonly Condition DownedLifelight = ModBosses.GetCondition("Lifelight"); //飘渺游光
        public static readonly Condition DownedCosmosChampion = ModBosses.GetCondition("CosmosChampion"); //宇宙英灵
        public static readonly Condition DownedAbom = ModBosses.GetCondition("AbomBoss"); //憎恶
        public static readonly Condition DownedMutant = ModBosses.GetCondition("MutantBoss"); //突变体

        #endregion

        #region 瑟银 Thorium Mod
        //肉前
        public static readonly Condition DownedGrandThunderBird = ModBosses.GetCondition("GrandThunderBird"); //惊雷王鹰
        public static readonly Condition DownedQueenJellyfish = ModBosses.GetCondition("QueenJellyfish"); //水母皇后
        public static readonly Condition DownedViscount = ModBosses.GetCondition("Viscount"); //蝙蝠子爵
        public static readonly Condition DownedGraniteEnergyStorm = ModBosses.GetCondition("GraniteEnergyStorm"); //花岗岩流能风暴
        public static readonly Condition DownedBuriedChampion = ModBosses.GetCondition("BuriedChampion"); //英灵遗骸
        public static readonly Condition DownedStarScouter = ModBosses.GetCondition("StarScouter"); //星际监察者
        //肉后
        public static readonly Condition DownedBoreanStrider = ModBosses.GetCondition("BoreanStrider"); //极地遁蛛
        public static readonly Condition DownedFallenBeholder = ModBosses.GetCondition("FallenBeholder"); //堕落注视者
        public static readonly Condition DownedLich = ModBosses.GetCondition("Lich"); //巫妖
        public static readonly Condition DownedForgottenOne = ModBosses.GetCondition("ForgottenOne"); //遗忘之主
        public static readonly Condition DownedThePrimordials = ModBosses.GetCondition("ThePrimordials"); //始生灾灵
        //迷你
        public static readonly Condition DownedPatchWerk = ModBosses.GetCondition("PatchWerk"); //缝合人

        #endregion

        #region 旅人归途 Homeward Journey
        //特殊
        public static readonly Condition DownedTheThree = new Condition("DownedTheThree", () => DownedTheOverwatcher.IsMet() && DownedTheLifebringer.IsMet() && DownedTheMaterealizer.IsMet()); //极视终表 + 万籁携生 + 化物天匠 (三至尊)
        //肉前
        public static readonly Condition DownedGoblinChariot = ModBosses.GetCondition("GoblinChariot"); //哥布林战车
        public static readonly Condition DownedBigDipper = ModBosses.GetCondition("BigDipper"); //大熊星座
        public static readonly Condition DownedPuppetOpera = ModBosses.GetCondition("PuppetOpera"); //木偶歌剧
        public static readonly Condition DownedMarquisMoonsquid = ModBosses.GetCondition("MarquisMoonsquid"); //月光鱿侯
        //肉后
        public static readonly Condition DownedWeatherReport = ModBosses.GetCondition("WeatherReport"); //天气预报
        public static readonly Condition DownedPriestessRod = ModBosses.GetCondition("PriestessRod"); //天竿鱼祭司
        public static readonly Condition DownedDiver = ModBosses.GetCondition("Diver"); //深潜者
        public static readonly Condition DownedTheMotherbrain = ModBosses.GetCondition("TheMotherbrain"); //母脑
        //月后
        public static readonly Condition DownedWallofShadow = ModBosses.GetCondition("WallofShadow"); //暗影墙
        //暗影墙后
        public static readonly Condition DownedSunSlimeGod = ModBosses.GetCondition("SunSlimeGod"); //史莱姆之神(旅人归途)
        public static readonly Condition DownedTheOverwatcher = ModBosses.GetCondition("TheOverwatcher"); //极视终表
        public static readonly Condition DownedTheLifebringer = ModBosses.GetCondition("TheLifebringer"); //万籁携生
        public static readonly Condition DownedTheMaterealizer = ModBosses.GetCondition("TheMaterealizer"); //化物天匠
        public static readonly Condition DownedScarabBelief = ModBosses.GetCondition("ScarabBelief"); //圣甲虫信仰
        public static readonly Condition DownedFallingWhale = ModBosses.GetCondition("FallingWhale"); //世末·永落鲸
        public static readonly Condition DownedTheSon = ModBosses.GetCondition("TheSon"); //圣子

        #endregion

        #region 救赎 Mod of Redemption

        #endregion



        #region 
        #endregion

    }
}
