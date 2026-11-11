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
        public static Condition HMAndBloodMoon = new Condition("HMAndBloodMoon", () => Main.hardMode && WorldCondition.BloodMoonHappened); //肉后 + 血月
        public static Condition EclipseHappened = new Condition("EclipseHappened", () => WorldCondition.EclipseHappened); //发生过日食
        public static Condition SnowMoonHappened = new Condition("SnowMoonHappened", () => WorldCondition.SnowMoonHappened); //发生过霜月
        public static Condition PumpkinMoonHappened = new Condition("PumpkinMoonHappened", () => WorldCondition.PumpkinMoonHappened); //发生过南瓜月
        
        public static Condition DownedBrainOfCthulhu = new Condition("DownedBrainOfCthulhu", () => NPCCondition.downedBrainOfCthulhu); //克苏鲁之脑
        public static Condition DownedEaterOfWorlds = new Condition("DownedEaterOfWorlds", () => NPCCondition.downedEaterOfWorlds); //世界吞噬怪
        public static Condition DownedDarkMage = new Condition("DownedDarkMage", () => NPCCondition.downedDarkMage); //黑暗魔法师
        public static Condition DownedOgre = new Condition("DownedOgre", () => NPCCondition.downedOgre); //食人魔
        public static Condition DownedBetsy = new Condition("DownedBetsy", () => NPCCondition.downedBetsy); //双足翼龙
        //public static Condition MourningWoodOrSplinter = new Condition("MourningWoodOrSplinter", () => Condition.DownedMourningWood.IsMet() || NPCCondition.downedSplinterling); //哀木 / 树精
        
        public static Condition DownedAnyPillar = new Condition("DownedPillar", () =>
        Condition.DownedSolarPillar.IsMet() || Condition.DownedVortexPillar.IsMet()
        || Condition.DownedNebulaPillar.IsMet() || Condition.DownedStardustPillar.IsMet()); //任意一个天界柱

        #endregion

        #region 灾厄 Calamity Mod
        //其他
        public static Condition KingSlimeOrDesertBug = new Condition("KingSlimeOrDesertBug", () => Condition.DownedKingSlime.IsMet() || DownedDesertBug.IsMet()); //史莱姆王或荒漠灾虫
        public static Condition AstrumBugAndCultist = new Condition("AstrumBugAndCultist", () => Condition.DownedCultist.IsMet() && DownedAstrumBug.IsMet()); //星神游龙 + 拜月教邪教徒
        public static Condition DOGAndSnowMoon = new Condition("DOGAndSnowMoon", () => WorldCondition.SnowMoonDOG); //神吞 + 霜月
        public static Condition DOGAndPumpkinMoon = new Condition("DOGAndPumpkinMoon", () => WorldCondition.PumpkinMoonDOG); //神吞 + 南瓜月
        public static Condition DOGAndEclipse = new Condition("DOGAndEclipse", () => WorldCondition.EclipseDOG); //神吞 + 日食
        //肉前
        public static Condition DownedDesertBug => ModBosses.GetCondition("DesertScourge"); //荒漠灾虫
        public static Condition DownedCrabulon => ModBosses.GetCondition("Crabulon"); //菌生蟹
        public static Condition DownedPerforator => ModBosses.GetCondition("Perforator"); //血肉宿主
        public static Condition DownedHiveMind => ModBosses.GetCondition("HiveMind"); //腐巢意志
        public static Condition PerOrHive => ModBosses.GetCondition("PerOrHive"); //血肉宿主 或 腐巢意志
        public static Condition DownedSlimeGod => ModBosses.GetCondition("SlimeGod"); //史莱姆之神
        //肉后
        public static Condition DownedCryogen => ModBosses.GetCondition("Cryogen"); //极地之灵
        public static Condition DownedAquaticBug => ModBosses.GetCondition("AquaticScourge"); //渊海灾虫
        public static Condition DownedBrimstone => ModBosses.GetCondition("Brimstone"); //硫磺火元素
        public static Condition DownedAstrum => ModBosses.GetCondition("AstrumAureus"); //白金星舰
        public static Condition DownedCalamitas => ModBosses.GetCondition("Calamitas"); //灾厄之影
        public static Condition DownedLeviathan => ModBosses.GetCondition("Leviathan"); //阿娜希塔和利维坦
        public static Condition DownedPlague => ModBosses.GetCondition("Plague"); //瘟疫使者歌莉娅
        public static Condition DownedRavager => ModBosses.GetCondition("Ravager"); //毁灭魔像
        public static Condition DownedAstrumBug => ModBosses.GetCondition("AstrumDeus"); //星神游龙
        //月后
        public static Condition DownedDragonfolly => ModBosses.GetCondition("Dragonfolly"); //痴愚金龙
        public static Condition DownedGuardians => ModBosses.GetCondition("Guardians"); //亵渎守卫
        public static Condition DownedProvidence => ModBosses.GetCondition("Providence"); //亵渎天神
        public static Condition DownedStormWeaver => ModBosses.GetCondition("StormWeaver"); //风暴编织者
        public static Condition DownedVoid => ModBosses.GetCondition("CeaselessVoid"); //无尽虚空
        public static Condition DownedSignus => ModBosses.GetCondition("Signus"); //西格纳斯
        public static Condition DownedPolterghast => ModBosses.GetCondition("Polterghast"); //噬魂幽花
        public static Condition DownedOldDuke => ModBosses.GetCondition("OldDuke"); //硫海遗爵
        public static Condition DownedDOG => ModBosses.GetCondition("DOG"); //神明吞噬者
        public static Condition DownedYharon => ModBosses.GetCondition("Yharon"); //犽戎
        public static Condition DownedDraedon => ModBosses.GetCondition("ExoMechs"); //星流巨械
        public static Condition DownedSCala => ModBosses.GetCondition("SCalamitas"); //终灾

        #endregion

        #region 灾劫 Catalyst Mod
        public static Condition DownedAstrageldon => ModBosses.GetCondition("Astrageldon"); //末世星史莱姆

        #endregion

        #region Fargo魂石 Fargo's Souls Mod
        public static Condition DownedTrojanSquirrel => ModBosses.GetCondition("TrojanSquirrel"); //特洛伊松鼠
        public static Condition DownedCursedCoffin => ModBosses.GetCondition("CursedCoffin"); //咒缚灵棺
        public static Condition DownedDevi => ModBosses.GetCondition("DeviBoss"); //戴薇安
        public static Condition DownedBanishedBaron => ModBosses.GetCondition("BanishedBaron"); //放逐遗爵
        public static Condition DownedLifelight => ModBosses.GetCondition("Lifelight"); //飘渺游光
        public static Condition DownedCosmosChampion => ModBosses.GetCondition("CosmosChampion"); //宇宙英灵
        public static Condition DownedAbom => ModBosses.GetCondition("AbomBoss"); //憎恶
        public static Condition DownedMutant => ModBosses.GetCondition("MutantBoss"); //突变体

        #endregion

        #region 瑟银 Thorium Mod
        //肉前
        public static Condition DownedGrandThunderBird => ModBosses.GetCondition("GrandThunderBird"); //惊雷王鹰
        public static Condition DownedQueenJellyfish => ModBosses.GetCondition("QueenJellyfish"); //水母皇后
        public static Condition DownedViscount => ModBosses.GetCondition("Viscount"); //蝙蝠子爵
        public static Condition DownedGraniteEnergyStorm => ModBosses.GetCondition("GraniteEnergyStorm"); //花岗岩流能风暴
        public static Condition DownedBuriedChampion => ModBosses.GetCondition("BuriedChampion"); //英灵遗骸
        public static Condition DownedStarScouter => ModBosses.GetCondition("StarScouter"); //星际监察者
        //肉后
        public static Condition DownedBoreanStrider => ModBosses.GetCondition("BoreanStrider"); //极地遁蛛
        public static Condition DownedFallenBeholder => ModBosses.GetCondition("FallenBeholder"); //堕落注视者
        public static Condition DownedLich => ModBosses.GetCondition("Lich"); //巫妖
        public static Condition DownedForgottenOne => ModBosses.GetCondition("ForgottenOne"); //遗忘之主
        public static Condition DownedThePrimordials => ModBosses.GetCondition("ThePrimordials"); //始生灾灵
        //迷你
        public static Condition DownedPatchWerk => ModBosses.GetCondition("PatchWerk"); //缝合人

        #endregion

        #region 旅人归途 Homeward Journey
        //特殊
        public static Condition DownedTheThree => new Condition("DownedTheThree", () => DownedTheOverwatcher.IsMet() && DownedTheLifebringer.IsMet() && DownedTheMaterealizer.IsMet()); //极视终表 + 万籁携生 + 化物天匠 (三至尊)
        //肉前
        public static Condition DownedGoblinChariot => ModBosses.GetCondition("GoblinChariot"); //哥布林战车
        public static Condition DownedBigDipper => ModBosses.GetCondition("BigDipper"); //大熊星座
        public static Condition DownedPuppetOpera => ModBosses.GetCondition("PuppetOpera"); //木偶歌剧
        public static Condition DownedMarquisMoonsquid => ModBosses.GetCondition("MarquisMoonsquid"); //月光鱿侯
        //肉后
        public static Condition DownedWeatherReport => ModBosses.GetCondition("WeatherReport"); //天气预报
        public static Condition DownedPriestessRod => ModBosses.GetCondition("PriestessRod"); //天竿鱼祭司
        public static Condition DownedDiver => ModBosses.GetCondition("Diver"); //深潜者
        public static Condition DownedTheMotherbrain => ModBosses.GetCondition("TheMotherbrain"); //母脑
        //月后
        public static Condition DownedWallofShadow => ModBosses.GetCondition("WallofShadow"); //暗影墙
        //暗影墙后
        public static Condition DownedSunSlimeGod => ModBosses.GetCondition("SunSlimeGod"); //史莱姆之神(旅人归途)
        public static Condition DownedTheOverwatcher => ModBosses.GetCondition("TheOverwatcher"); //极视终表
        public static Condition DownedTheLifebringer => ModBosses.GetCondition("TheLifebringer"); //万籁携生
        public static Condition DownedTheMaterealizer => ModBosses.GetCondition("TheMaterealizer"); //化物天匠
        public static Condition DownedScarabBelief => ModBosses.GetCondition("ScarabBelief"); //圣甲虫信仰
        public static Condition DownedFallingWhale => ModBosses.GetCondition("FallingWhale"); //世末·永落鲸
        public static Condition DownedTheSon => ModBosses.GetCondition("TheSon"); //圣子

        #endregion

        #region 救赎 Mod of Redemption

        #endregion



        #region 
        #endregion

    }
}
