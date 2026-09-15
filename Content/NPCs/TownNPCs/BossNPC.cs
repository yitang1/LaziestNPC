using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Utilities;
using Terraria.Localization;
using static Terraria.Item;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static LaziestNPC.LaziestNPC;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Common.ModBossess;
using LaziestNPC.Content.Items.SummonItems;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class BossNPC : ModNPC
    {
        private static int ShopNum = 1;

        private const string VanillaAll = "VanillaAll";
        private const string ModBags = "ModBags";
        private const string ModSums = "ModSums";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 5;
            //NPCID.Sets.ExtraFramesCount[Type] = 0;
            //NPCID.Sets.AttackFrameCount[Type] = 0; 
            NPCID.Sets.DangerDetectRange[Type] = 220; 

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                SpriteDirection = 1,
                Direction = -1,
                Velocity = 0.1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            /*NPC.Happiness
				.SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
				.SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
				.SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
				.SetNPCAffection(NPCID.Cyborg, AffectionLevel.Love)
				.SetNPCAffection(NPCID.Steampunker, AffectionLevel.Like);*/
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 20;
            NPC.height = 20;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AIType = NPCID.Squirrel;
            //AnimationType = NPCID.Guide;
            // ↑这条【暂时】要删掉，因为这个【临时贴图】实际上不是NPC而是一个中立生物的
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.BossNPC")
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return numTownNPCs > 5;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                this.GetLocalizedValue("Name.BossNPC")
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> dialogue = new WeightedRandom<string>();
            dialogue.Add(this.GetLocalizedValue("Chat.Normal1"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal2"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal3"));
            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            switch (ShopNum)
            {
                case 1:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.VanillaAll");
                    break;
                case 2:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.ModBags");
                    break;
                default:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.ModSums");
                    break;
            }

            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.CycleShop");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                switch (ShopNum)
                {
                    case 1:
                        shopName = VanillaAll;
                        break;
                    case 2:
                        shopName = ModBags;
                        break;
                    default:
                        shopName = ModSums;
                        break;
                }
            }
            else
            {
                ShopNum++;
                if (ShopNum > 3)
                    ShopNum = 1;
            }
        }

        public override void AddShops()
        {
            var vanShop = new NPCShop(Type, VanillaAll);
            var modBag = new NPCShop(Type, ModBags);
            var modSum = new NPCShop(Type, ModSums);

            #region 【原版相关内容】

            #region 原版Boss宝藏袋
            //肉前
            vanShop.AddItem(ItemType<TheReturner>(), (0, 1, 0, 0))
            .AddItem(ItemID.KingSlimeBossBag, (0, 5, 0, 0), Condition.DownedKingSlime) //史莱姆王
            .AddItem(ItemID.EyeOfCthulhuBossBag, (0, 10, 0, 0), Condition.DownedEyeOfCthulhu) //克苏鲁之眼
            .AddItem(ItemID.BrainOfCthulhuBossBag, (0, 15, 0, 0), Condition.DownedEowOrBoc) //克苏鲁之脑
            .AddItem(ItemID.EaterOfWorldsBossBag, (0, 15, 0, 0), Condition.DownedEowOrBoc) //世界吞噬怪
            .AddItem(ItemID.QueenBeeBossBag, (0, 20, 0, 0), Condition.DownedQueenBee) //蜂王
            .AddItem(ItemID.SkeletronBossBag, (0, 25, 0, 0), Condition.DownedSkeletron) //骷髅王
            .AddItem(ItemID.DeerclopsBossBag, (0, 30, 0, 0), Condition.DownedDeerclops) //独眼巨鹿
            .AddItem(ItemID.WallOfFleshBossBag, (0, 35, 0, 0), Condition.Hardmode); //肉山
            //肉后
            vanShop.AddItem(ItemID.QueenSlimeBossBag, (0, 40, 0, 0), Condition.DownedQueenSlime) //史莱姆皇后
            .AddItem(ItemID.DestroyerBossBag, (0, 45, 0, 0), Condition.DownedDestroyer) //毁灭者
            .AddItem(ItemID.TwinsBossBag, (0, 45, 0, 0), Condition.DownedTwins) //双子魔眼
            .AddItem(ItemID.SkeletronPrimeBossBag, (0, 45, 0, 0), Condition.DownedSkeletronPrime) //机械骷髅王
            .AddItem(ItemID.PlanteraBossBag, (0, 50, 0, 0), Condition.DownedPlantera) //世纪之花
            .AddItem(ItemID.FairyQueenBossBag, (0, 55, 0, 0), Condition.DownedEmpressOfLight) //光之女皇
            .AddItem(ItemID.GolemBossBag, (0, 60, 0, 0), Condition.DownedGolem) //石巨人
            .AddItem(ItemID.FishronBossBag, (0, 65, 0, 0), Condition.DownedDukeFishron) //猪鲨
            .AddItem(ItemID.BossBagBetsy, (0, 65, 0, 0), Condition.DownedOldOnesArmyT3) //双足翼龙
            .AddItem(ItemID.MoonLordBossBag, (0, 75, 0, 0), Condition.DownedMoonLord); //月总
            #endregion

            #region 原版事件召唤物品
            vanShop.AddItem(ItemID.BloodMoonStarter, (0, 1, 0, 0)) //血月
            .AddItem(ItemID.GoblinBattleStandard, (0, 1, 0, 0)) //哥布林入侵
            .AddItem(ItemID.PirateMap, (0, 5, 0, 0), Condition.Hardmode) //海盗入侵
            .AddItem(ItemID.SnowGlobe, (0, 5, 0, 0), Condition.Hardmode) //雪人军团
            .AddItem(ItemID.SolarTablet, (0, 10, 0, 0), Condition.DownedMechBossAny) //日食
            .AddItem(ItemID.PumpkinMoonMedallion, (0, 15, 0, 0), Condition.DownedPlantera) //南瓜月
            .AddItem(ItemID.NaughtyPresent, (0, 15, 0, 0), Condition.DownedPlantera); //霜月
            #endregion

            #region 原版Boss召唤物品
            //肉前
            vanShop.AddItem(ItemID.SlimeCrown, (0, 1, 0, 0)) //史莱姆王
            .AddItem(ItemID.SuspiciousLookingEye, (0, 1, 0, 0)) //克苏鲁之眼
            .AddItem(ItemID.BloodySpine, (0, 5, 0, 0)) //克苏鲁之脑
            .AddItem(ItemID.WormFood, (0, 5, 0, 0)) //世界吞噬怪
            .AddItem(ItemID.Abeemination, (0, 5, 0, 0)) //蜂王
            .AddItem(ItemType<GentleSkull>(), (0, 5, 0, 0)) //骷髅王
            .AddItem(ItemID.DeerThing, (0, 5, 0, 0)) //独眼巨鹿
            .AddItem(ItemID.GuideVoodooDoll, (0, 8, 0, 0)) //血肉之墙
            //肉后
            .AddItem(ItemID.QueenSlimeCrystal, (0, 10, 0, 0), Condition.Hardmode) //史莱姆皇后
            .AddItem(ItemID.MechanicalWorm, (0, 15, 0, 0), Condition.Hardmode) //毁灭者
            .AddItem(ItemID.MechanicalEye, (0, 15, 0, 0), Condition.Hardmode) //双子魔眼
            .AddItem(ItemID.MechanicalSkull, (0, 15, 0, 0), Condition.Hardmode) //机械骷髅王
            .AddItem(ItemType<NoRootFlower>(), (0, 20, 0, 0), Condition.DownedMechBossAll) //世纪之花
            .AddItem(ItemID.EmpressButterfly, (0, 25, 0, 0), Condition.DownedPlantera) //光之女皇
            .AddItem(ItemID.LihzahrdPowerCell, (0, 30, 0, 0), Condition.DownedPlantera) //石巨人
            .AddItem(ItemID.TruffleWorm, (0, 10, 0, 0), Condition.Hardmode) //猪龙鱼公爵
            .AddItem(ItemType<ForbiddenPaper>(), (0, 35, 0, 0), Condition.DownedGolem) //拜月教邪教徒
            .AddItem(ItemID.CelestialSigil, (0, 40, 0, 0), Condition.DownedCultist); //月亮领主

            #endregion

            #region 原版稀有敌怪召唤物品
            //肉前
            vanShop.AddModItem("Fargowiltas/WormSnack", (0, 1, 0, 0)) //巨型蠕虫/挖掘怪
            .AddModItem("Fargowiltas/PinkSlimeCrown", (0, 4, 0, 0)) //粉史莱姆
            .AddModItem("Fargowiltas/GnomeHat", (0, 1, 0, 0)) //侏儒
            .AddModItem("Fargowiltas/GoblinScrap", (0, 1, 0, 0)) //哥布林侦察兵
            .AddModItem("Fargowiltas/AttractiveOre", (0, 1, 0, 0)) //不死矿工
            .AddModItem("Fargowiltas/HeartChocolate", (0, 4, 0, 0)) //宁芙
            .AddModItem("Fargowiltas/HolyGrail", (0, 4, 0, 0)) //蒂姆
            .AddModItem("Fargowiltas/Eggplant", (0, 4, 0, 0)) //骷髅博士
            .AddModItem("Fargowiltas/GoldenSlimeCrown", (0, 4, 0, 0)) //金史莱姆
            .AddModItem("Fargowiltas/SuspiciousLookingLure", (0, 1, 0, 0)) //游荡眼球怪鱼/僵尸人鱼
            .AddModItem("Fargowiltas/SlimyLockBox", (0, 2, 0, 0), Condition.DownedSkeletron) //地牢史莱姆
            //肉后
            .AddModItem("Fargowiltas/SuspiciousLookingChest", (0, 4, 0, 0), Condition.Hardmode) //宝箱怪/冰雪宝箱怪
            .AddModItem("Fargowiltas/CrimsonChest", (0, 5, 0, 0), Condition.Hardmode) //猩红宝箱怪
            .AddModItem("Fargowiltas/CorruptChest", (0, 5, 0, 0), Condition.Hardmode) //腐化宝箱怪
            .AddModItem("Fargowiltas/HallowChest", (0, 5, 0, 0), Condition.Hardmode) //神圣宝箱怪
            .AddModItem("Fargowiltas/JungleChest", (0, 5, 0, 0), Condition.Hardmode) //丛林宝箱怪
            .AddModItem("Fargowiltas/AthenianIdol", (0, 4, 0, 0), Condition.Hardmode) //蛇发女妖
            .AddModItem("Fargowiltas/RuneOrb", (0, 4, 0, 0), Condition.Hardmode) //符文巫师
            .AddModItem("Fargowiltas/ClownLicense", (0, 4, 0, 0), Condition.Hardmode) //小丑
            .AddModItem("Fargowiltas/CloudSnack", (0, 5, 0, 0), Condition.Hardmode) //飞龙
            .AddModItem("Fargowiltas/DilutedRainbowMatter", (0, 4, 0, 0), Condition.Hardmode) //彩虹史莱姆
            .AddModItem("Fargowiltas/CoreoftheFrostCore", (0, 5, 0, 0), Condition.Hardmode) //冰雪巨人
            .AddModItem("Fargowiltas/ForbiddenForbiddenFragment", (0, 5, 0, 0), Condition.Hardmode) //沙尘精
            .AddModItem("Fargowiltas/ShadowflameIcon", (0, 4, 0, 0), Condition.Hardmode, Condition.DownedGoblinArmy) //哥布林术士
            .AddModItem("Fargowiltas/PirateFlag", (0, 4, 0, 0), Condition.Hardmode, Condition.DownedPirates) //海盗船长
            .AddModItem("Fargowiltas/HemoclawCrab", (0, 4, 0, 0), Condition.Hardmode) //血浆哥布林鲨鱼
            .AddModItem("Fargowiltas/BloodUrchin", (0, 4, 0, 0), Condition.Hardmode) //血鳗鱼
            .AddModItem("Fargowiltas/BloodSushiPlatter", (0, 5, 0, 0), Condition.Hardmode) //恐惧鹦鹉螺
            .AddModItem("Fargowiltas/MothLamp", (0, 4, 0, 0), Condition.Hardmode) //蛾
            .AddModItem("Fargowiltas/DemonicPlushie", (0, 4, 0, 0), Condition.DownedMechBossAny) //红魔鬼
            .AddModItem("Fargowiltas/Pincushion", (0, 4, 0, 0), Condition.DownedPlantera) //钉头
            .AddModItem("Fargowiltas/MothronEgg", (0, 5, 0, 0), Condition.DownedPlantera) //蛾怪
            .AddModItem("Fargowiltas/AmalgamatedSkull", (0, 5, 0, 0), Condition.DownedPlantera) //骷髅狙击手+骷髅特警+骷髅突击手
            .AddModItem("Fargowiltas/AmalgamatedSpirit", (0, 5, 0, 0), Condition.DownedPlantera) //褴褛邪教徒法师+死灵法师+魔教徒
            .AddModItem("Fargowiltas/LeesHeadband", (0, 5, 0, 0), Condition.DownedPlantera) //骷髅李
            .AddModItem("Fargowiltas/GrandCross", (0, 5, 0, 0), Condition.DownedPlantera); //圣骑士

            #endregion

            #endregion

            #region 【模组Boss宝藏袋】

            #region 灾厄 Calamity Mod
            //肉前
            modBag.AddModItem("CalamityMod/DesertScourgeBag", (0, 5, 0, 0), DownedDesertBug) //荒漠灾虫
            .AddModItem("CalamityMod/CrabulonBag", (0, 5, 0, 0), DownedCrabulon) //菌生蟹
            .AddModItem("CalamityMod/PerforatorBag", (0, 10, 0, 0), DownedPerforator) //血肉宿主
            .AddModItem("CalamityMod/HiveMindBag", (0, 10, 0, 0), DownedHiveMind) //腐巢意志
            .AddModItem("CalamityMod/SlimeGodBag", (0, 35, 0, 0), DownedSlimeGod) //史莱姆之神
            //肉后
            .AddModItem("CalamityMod/CryogenBag", (0, 40, 0, 0), DownedCryogen) //极地之灵
            .AddModItem("CalamityMod/AquaticScourgeBag", (0, 40, 0, 0), DownedAquaticBug) //渊海灾虫
            .AddModItem("CalamityMod/BrimstoneElementalBag", (0, 40, 0, 0), DownedBrimstone) //硫磺火元素
            .AddModItem("CalamityMod/CalamitasCloneBag", (0, 50, 0, 0), DownedCalamitas) //灾厄之影
            .AddModItem("CalamityMod/LeviathanBag", (0, 55, 0, 0), DownedLeviathan) //阿娜希塔和利维坦
            .AddModItem("CalamityMod/AstrumAureusBag", (0, 55, 0, 0), DownedAstrum) //白金星舰
            .AddModItem("CalamityMod/PlaguebringerGoliathBag", (0, 65, 0, 0), DownedPlague) //瘟疫使者歌莉娅
            .AddModItem("CalamityMod/RavagerBag", (0, 65, 0, 0), DownedRavager) //毁灭魔像
            .AddModItem("CalamityMod/AstrumDeusBag", (0, 70, 0, 0), DownedAstrumBug) //星神游龙
            //月后
            .AddModItem("CalamityMod/DragonfollyBag", (0, 75, 0, 0), DownedDragonfolly) //痴愚金龙
            .AddModItem("CalamityMod/ProvidenceBag", (0, 80, 0, 0), DownedProvidence) //亵渎天神
            .AddModItem("CalamityMod/StormWeaverBag", (0, 85, 0, 0), DownedStormWeaver) //风暴编织者
            .AddModItem("CalamityMod/CeaselessVoidBag", (0, 85, 0, 0), DownedVoid) //无尽虚空
            .AddModItem("CalamityMod/SignusBag", (0, 85, 0, 0), DownedSignus) //西格纳斯
            .AddModItem("CalamityMod/PolterghastBag", (0, 90, 0, 0), DownedPolterghast) //噬魂幽花
            .AddModItem("CalamityMod/OldDukeBag", (0, 95, 0, 0), DownedOldDuke) //硫海遗爵
            .AddModItem("CalamityMod/DevourerofGodsBag", (1, 0, 0, 0), DownedDOG) //神明吞噬者
            .AddModItem("CalamityMod/YharonBag", (2, 0, 0, 0), DownedYharon) //犽戎
            .AddModItem("CalamityMod/DraedonBag", (3, 0, 0, 0), DownedDraedon) //星流巨械
            .AddModItem("CalamityMod/CalamitasCoffer", (5, 0, 0, 0), DownedSCalamitas); //终灾

            #endregion

            #region 灾劫 Catalyst Mod
            modBag.AddModItem("CatalystMod/AstrageldonBag", (1, 0, 0, 0), DownedAstrageldon); //末世星史莱姆

            #endregion

            #region Fargo魂石 Fargo's Souls Mod
            modBag.AddModItem("FargowiltasSouls/TrojanSquirrelBag", (0, 5, 0, 0), DownedTrojanSquirrel) //特洛伊松鼠
            .AddModItem("FargowiltasSouls/CursedCoffinBag", (0, 5, 0, 0), DownedCursedCoffin) //咒缚灵棺
            .AddModItem("FargowiltasSouls/DeviBag", (0, 35, 0, 0), DownedDevi) //戴薇安
            .AddModItem("FargowiltasSouls/BanishedBaronBag", (0, 40, 0, 0), DownedBanishedBaron) //放逐遗爵
            .AddModItem("FargowiltasSouls/LifelightBag", (0, 45, 0, 0), DownedLifelight) //飘渺游光
            .AddModItem("FargowiltasSouls/CosmosBag", (0, 90, 0, 0), DownedCosmosChampion) //宇宙英灵
            .AddModItem("FargowiltasSouls/AbomBag", (5, 0, 0, 0), DownedAbom) //憎恶
            .AddModItem("FargowiltasSouls/MutantBag", (10, 0, 0, 0), DownedMutant); //突变体

            #endregion

            #region 瑟银 Thorium Mod
            //肉前
            modBag.AddModItem("ThoriumMod/TheGrandThunderBirdTreasureBag", (0, 5, 0, 0), DownedGrandThunderBird) //惊雷王鹰
            .AddModItem("ThoriumMod/QueenJellyfishTreasureBag", (0, 15, 0, 0), DownedQueenJellyfish) //水母皇后
            .AddModItem("ThoriumMod/ViscountTreasureBag", (0, 20, 0, 0), DownedViscount) //蝙蝠子爵
            .AddModItem("ThoriumMod/GraniteEnergyStormTreasureBag", (0, 25, 0, 0), DownedGraniteEnergyStorm) //花岗岩流能风暴
            .AddModItem("ThoriumMod/BuriedChampionTreasureBag", (0, 25, 0, 0), DownedBuriedChampion) //英灵遗骸
            .AddModItem("ThoriumMod/StarScouterTreasureBag", (0, 30, 0, 0), DownedStarScouter) //星际监察者
            //肉后
            .AddModItem("ThoriumMod/BoreanStriderTreasureBag", (0, 40, 0, 0), DownedBoreanStrider) //极地遁蛛
            .AddModItem("ThoriumMod/FallenBeholderTreasureBag", (0, 40, 0, 0), DownedFallenBeholder) //堕落注视者
            .AddModItem("ThoriumMod/LichTreasureBag", (0, 45, 0, 0), DownedLich) //巫妖
            .AddModItem("ThoriumMod/ForgottenOneTreasureBag", (0, 65, 0, 0), DownedForgottenOne) //遗忘之主
            .AddModItem("ThoriumMod/ThePrimordialsTreasureBag", (0, 80, 0, 0), DownedThePrimordials) //始生灾灵
            //迷你
            .AddModItem("ThoriumMod/DarkMageTreasureBag", (0, 15, 0, 0), Condition.DownedOldOnesArmyT1) //黑暗魔法师
            .AddModItem("ThoriumMod/FlyingDutchmanTreasureBag", (0, 40, 0, 0), Condition.DownedPirates) //荷兰飞盗船
            .AddModItem("ThoriumMod/OgreTreasureBag", (0, 45, 0, 0), Condition.DownedOldOnesArmyT2) //食人魔
            .AddModItem("ThoriumMod/MartianSaucerTreasureBag", (0, 65, 0, 0), Condition.DownedMartians); //火星飞碟

            #endregion

            #endregion

            #region 【模组召唤物品】

            #region [模组事件召唤物品]

            #region 灾厄
            modSum.AddModItem("CalamityMod/TorrentialTear", (0, 1, 50, 0), Condition.DownedSkeletron) //雨
            .AddModItem("CalamityMod/CausticTear", (0, 1, 50, 0), Condition.DownedEyeOfCthulhu) //酸雨
            .AddModItem("CalamityMod/AridArtifact", (0, 5, 0, 0), Condition.Hardmode) //沙尘暴
            .AddModItem("CalamityMod/MartianDistressRemote", (0, 20, 0, 0), Condition.DownedGolem); //火星暴乱

            #endregion

            #region
            #endregion

            #endregion

            #region [模组Boss召唤物品]

            #region 灾厄 Calamity Mod
            //肉前
            modSum.AddModItem("CalamityMod/DesertMedallion", (0, 1, 0, 0)) //荒漠灾虫
            .AddModItem("CalamityMod/DecapoditaSprout", (0, 1, 0, 0)) //菌生蟹
            .AddModItem("CalamityMod/BloodyWormFood", (0, 5, 0, 0), PerOrHive) //血肉宿主
            .AddModItem("CalamityMod/Teratoma", (0, 5, 0, 0), PerOrHive) //腐巢意志
            .AddModItem("CalamityMod/OverloadedSludge", (0, 8, 0, 0)) //史莱姆之神
            //肉后
            .AddModItem("CalamityMod/CryoKey", (0, 10, 0, 0), Condition.Hardmode) //极地之灵
            .AddModItem("CalamityMod/Seafood", (0, 10, 0, 0), Condition.Hardmode) //渊海灾虫
            .AddModItem("CalamityMod/CharredIdol", (0, 10, 0, 0), Condition.Hardmode) //硫磺火元素
            .AddModItem("CalamityMod/EyeofDesolation", (0, 20, 0, 0), Condition.Hardmode) //灾厄之影
            .AddModItem("CalamityMod/AstralChunk", (0, 25, 0, 0), Condition.Hardmode) //白金星舰
            .AddModItem("CalamityMod/Abombination", (0, 30, 0, 0), Condition.DownedGolem) //瘟疫使者歌莉娅
            .AddModItem("CalamityMod/DeathWhistle", (0, 30, 0, 0), Condition.DownedGolem) //毁灭魔像
            .AddModItem("CalamityMod/TitanHeart", (0, 35, 0, 0), Condition.Hardmode) //星神游龙
            //月后
            .AddModItem("CalamityMod/ExoticPheromones", (0, 30, 0, 0), Condition.DownedCultist) //痴愚金龙
            .AddModItem("CalamityMod/ProfanedShard", (0, 30, 0, 0), Condition.DownedMoonLord) //亵渎守卫
            .AddModItem("CalamityMod/ProfanedCore", (0, 35, 0, 0), DownedGuardians) //亵渎天神
            .AddModItem("CalamityMod/MarkofProvidence", (0, 40, 0, 0), DownedProvidence) //风暴编织者/无尽虚空/西格纳斯
            .AddModItem("CalamityMod/NecroplasmicBeacon", (0, 45, 0, 0), Condition.DownedMoonLord) //噬魂幽花
            .AddModItem("CalamityMod/BloodwormItem", (0, 50, 0, 0), Condition.DownedMoonLord) //硫海遗爵
            .AddModItem("CalamityMod/CosmicWorm", (0, 55, 0, 0), Condition.DownedMoonLord) //神明吞噬者
            .AddModItem("CalamityMod/YharonEgg", (0, 60, 0, 0), DownedDragonfolly) //犽戎
            .AddModItem("CalamityMod/CeremonialUrn", (0, 65, 0, 0), DownedSCalamitas); //终灾

            #endregion

            #region 灾劫 Catalyst Mod
            modSum.AddModItem("CatalystMod/AstralCommunicator", (0, 55, 0, 0), Condition.Hardmode); //末世星史莱姆

            #endregion

            #region Fargo魂石 Fargo's Souls Mod
            //肉前
            modSum.AddModItem("FargowiltasSouls/SquirrelCoatofArms", (0, 1, 0, 0)) //特洛伊松鼠
            .AddModItem("FargowiltasSouls/CoffinSummon", (0, 1, 0, 0)) //咒缚灵棺
            //肉后
            .AddModItem("FargowiltasSouls/DevisCurse", (0, 10, 0, 0), Condition.BloodMoon) //戴薇安
            .AddModItem("FargowiltasSouls/MechLure", (0, 10, 0, 0), Condition.Hardmode) //放逐遗爵
            .AddModItem("FargowiltasSouls/FragilePixieLamp", (0, 15, 0, 0), Condition.Hardmode) //飘渺游光
            //月后
            .AddModItem("FargowiltasSouls/SigilOfChampions", (0, 50, 0, 0), Condition.DownedMoonLord) //宇宙英灵
            .AddModItem("FargowiltasSouls/AbomsCurse", (0, 66, 0, 0), Condition.DownedMoonLord) //憎恶
            .AddModItem("FargowiltasSouls/MutantsCurse", (1, 0, 0, 0), DownedAbom); //突变体

            #endregion

            #region 瑟银 Thorium Mod
            //肉前
            modSum.AddModItem("ThoriumMod/GrandFlareGun", (0, 1, 0, 0)) //惊雷王鹰
            .AddModItem("ThoriumMod/JellyfishResonator", (0, 1, 0, 0)) //水母皇后
            .AddModItem("ThoriumMod/GraniteEnergyCore", (0, 5, 0, 0), Condition.DownedSkeletron) //花岗岩流能风暴
            .AddModItem("ThoriumMod/AncientBlade", (0, 5, 0, 0), Condition.DownedSkeletron) //英灵遗骸
            .AddModItem("ThoriumMod/StarCaller", (0, 7, 0, 0), Condition.DownedEowOrBoc) //星际监察者
            //肉后
            .AddModItem("ThoriumMod/StriderTear", (0, 10, 0, 0), Condition.Hardmode) //极地遁蛛
            .AddModItem("ThoriumMod/VoidLens", (0, 10, 0, 0), Condition.Hardmode) //堕落注视者
            .AddModItem("ThoriumMod/AbyssalShadow2", (0, 25, 0, 0), Condition.DownedPlantera) //遗忘之主
            .AddModItem("ThoriumMod/DoomSayersCoin", (0, 35, 0, 0), Condition.DownedCultist); //始生灾灵

            #endregion

            #endregion

            #endregion


            vanShop.Register();
            modBag.Register();
            modSum.Register();
        }

        //正式添加新贴图后删掉
        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0f)
            {
                if (!NPC.IsABestiaryIconDummy)
                {
                    if (NPC.direction == 1)
                    {
                        NPC.spriteDirection = -1;
                    }
                    if (NPC.direction == -1)
                    {
                        NPC.spriteDirection = 1;
                    }

                    if (NPC.velocity.X == 0f)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                        return;
                    }
                }
                NPC.frameCounter += NPC.IsABestiaryIconDummy ? 0.6f : Math.Abs(NPC.velocity.X) * 0.25f;
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > 12.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[NPC.type] - 1)
                {
                    NPC.frame.Y = frameHeight;
                }
            }
            else
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = frameHeight * 2;
            }
        }

        //正式添加新贴图后删掉
        public override void PostAI()
        {
            float maxSpeed = 0.7f;
            if (Math.Abs(NPC.velocity.X) > maxSpeed)
            {
                NPC.velocity.X = Math.Sign(NPC.velocity.X) * maxSpeed;
            }
        }

        /*public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 70;
            knockback = 3f;
        }

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            scale = 1f;
            horizontalHoldoutOffset = 20;
            if (!NPC.downedMoonlord)
            {
                item = TextureAssets.Item[ItemID.Shotgun].Value;
            }
            if (NPC.downedMoonlord)
            {
                item = TextureAssets.Item[ItemID.VortexBeater].Value;
            }
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!NPC.downedMoonlord)
            {
                attackDelay = 10;
                projType = 279;
            }
            if (NPC.downedMoonlord)
            {
                attackDelay = 4;
                projType = 638;
            }
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
        }*/
    }
}
