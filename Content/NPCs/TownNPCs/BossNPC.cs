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
using LaziestNPC.Content.Items.Others;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;
using static LaziestNPC.Globals.GlobalItems.ItemCondition;
using static LaziestNPC.Common.Helpers.LNPCHelper;
using LaziestNPC.Globals.Config;
using LaziestNPC.Globals.GlobalPlayers;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Globals.GlobalNPCs;
using LaziestNPC.Content.Items.Consumables;
using LaziestNPC.Common.UI;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class BossNPC : TownLNPC
    {
        protected override string BestiaryText => "Mods.LaziestNPC.Bestiary.BossNPC";
        protected override string NPCNameText => "Name.BossNPC";
        protected override int AttackProjType => ProjectileID.DD2PhoenixBowShot;
        protected override int AttackProjDamage => 3;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            /*NPC.Happiness
				.SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
				.SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
				.SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
				.SetNPCAffection(NPCID.Cyborg, AffectionLevel.Love)
				.SetNPCAffection(NPCID.Steampunker, AffectionLevel.Like);*/
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return numTownNPCs > 6 && LNPCConfig.Instance.LNPCBossNPC;
        }

        private static readonly (string Flag, string ChatKey)[] NPCBossChats =
        {
            //肉前
            ("KingSlime_BossNPC", "Chat.KingSlime00"), //史莱姆王
            ("EyeOfCthulhu_BossNPC", "Chat.EyeOfCthulhu00"), //克苏鲁之眼
            ("BrainOfCthulhu_BossNPC", "Chat.BrainOfCthulhu00"), //克苏鲁之脑
            ("EaterOfWorlds_BossNPC", "Chat.EaterOfWorlds00"), //世界吞噬怪
            ("QueenBee_BossNPC", "Chat.QueenBee00"), //蜂王
            ("Skeletron_BossNPC", "Chat.Skeletron00"), //骷髅王
            ("Deerclops_BossNPC", "Chat.Deerclops00"), //独眼巨鹿
            ("WallOfFlesh_BossNPC", "Chat.WallOfFlesh00"), //血肉之墙
            //肉后
            ("QueenSlime_BossNPC", "Chat.QueenSlime00"), //史莱姆皇后
            ("TheDestroyer_BossNPC", "Chat.TheDestroyer00"), //毁灭者
            ("TheTwins_BossNPC", "Chat.TheTwins00"), //双子魔眼
            ("SkeletronPrime_BossNPC", "Chat.SkeletronPrime00"), //机械骷髅王
            ("Plantera_BossNPC", "Chat.Plantera00"), //世纪之花
            ("EmpressOfLight_BossNPC", "Chat.EmpressOfLight00"), //光之女皇
            ("Golem_BossNPC", "Chat.Golem00"), //石巨人
            ("DukeFishron_BossNPC", "Chat.DukeFishron00"), //猪龙鱼公爵
            ("Cultist_BossNPC", "Chat.Cultist00"), //拜月教邪教徒
            ("MoonLord_BossNPC", "Chat.MoonLord00"), //月亮领主
        };

        //【一位数表示关于Boss的日常台词"Chat.KingSlime0"】
        //【两位数表示刚击败Boss后的首句台词"Chat.KingSlime00"】
        public override string GetChat()
        {
            Player player = Main.LocalPlayer;
            TownLNPCPlayer LTPlayer = player.LaziestTownLNPC();

            //满足条件时，触发优先级最高的特殊台词
            foreach (var id in NPCBossChats)
            {
                if (LTPlayer.OnceBossChats.Contains(id.Flag))
                {
                    LTPlayer.OnceBossChats.Remove(id.Flag);
                    return this.GetLocalizedValue(id.ChatKey);
                }
            }

            //60%概率出现日常类台词，40%是其他类台词
            if (Main.rand.Next(100) < 60)
            {
                //权重值，默认不写就是1.0。这个和随机方法效果一样，是TR的
                //若三句台词，默认都是1.0，总权重是3.0，即每句话出现概率是1/3
                WeightedRandom<string> chat = new WeightedRandom<string>();

                //【日常对话，默认权重1.0】
                chat.Add(this.GetLocalizedValue("Chat.Normal1"));
                chat.Add(this.GetLocalizedValue("Chat.Normal2"));
                chat.Add(this.GetLocalizedValue("Chat.Normal3"));
                chat.Add(this.GetLocalizedValue("Chat.Normal4"));
                chat.Add(this.GetLocalizedValue("Chat.Normal5"));
                chat.Add(this.GetLocalizedValue("Chat.Normal6"));

                chat.Add(this.GetLocalizedValue("Chat.Normal7"), 0.1);

                return chat.Get();
            }
            else
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                //【日常对话，Boss或事件的专属对话，权重1.0】
                //史莱姆王
                if (NPC.downedSlimeKing)
                {
                    chat.Add(this.GetLocalizedValue("Chat.KingSlime0"));
                    chat.Add(this.GetLocalizedValue("Chat.KingSlime1"));
                }
                //克苏鲁之眼
                if (NPC.downedBoss1)
                {
                    chat.Add(this.GetLocalizedValue("Chat.EyeOfCthulhu0"));
                    chat.Add(this.GetLocalizedValue("Chat.EyeOfCthulhu1"));

                }

                if (NPCCondition.downedBrainOfCthulhu) chat.Add(this.GetLocalizedValue("Chat.BrainOfCthulhu0")); //克苏鲁之脑
                if (NPCCondition.downedEaterOfWorlds) chat.Add(this.GetLocalizedValue("Chat.EaterOfWorlds0")); //世界吞噬怪
                if (NPC.downedQueenBee) chat.Add(this.GetLocalizedValue("Chat.QueenBee0")); //蜂王
                if (NPC.downedBoss3) chat.Add(this.GetLocalizedValue("Chat.Skeletron0")); //骷髅王
                if (NPC.downedDeerclops) chat.Add(this.GetLocalizedValue("Chat.Deerclops0")); //独眼巨鹿
                if (Main.hardMode) chat.Add(this.GetLocalizedValue("Chat.WallOfFlesh0")); //血肉之墙

                if (NPC.downedQueenSlime) chat.Add(this.GetLocalizedValue("Chat.QueenSlime0")); //史莱姆皇后
                if (NPC.downedMechBoss1) chat.Add(this.GetLocalizedValue("Chat.TheDestroyer0")); //毁灭者
                if (NPC.downedMechBoss2) chat.Add(this.GetLocalizedValue("Chat.TheTwins0")); //双子魔眼
                if (NPC.downedMechBoss3) chat.Add(this.GetLocalizedValue("Chat.SkeletronPrime0")); //机械骷髅王
                if (NPC.downedPlantBoss) chat.Add(this.GetLocalizedValue("Chat.Plantera0")); //世纪之花
                if (NPC.downedEmpressOfLight) chat.Add(this.GetLocalizedValue("Chat.EmpressOfLight0")); //光之女皇
                if (NPC.downedGolemBoss) chat.Add(this.GetLocalizedValue("Chat.Golem0")); //石巨人
                if (NPC.downedFishron) chat.Add(this.GetLocalizedValue("Chat.DukeFishron0")); //猪龙鱼公爵
                if (NPC.downedAncientCultist) chat.Add(this.GetLocalizedValue("Chat.Cultist0")); //拜月教邪教徒
                
                if (NPC.downedMoonlord)
                {
                    chat.Add(this.GetLocalizedValue("Chat.Moonlord0"), 0.1); //月亮领主(特殊)
                    chat.Add(this.GetLocalizedValue("Chat.Moonlord1")); //月亮领主

                }
                
                if (chat.elements.Count == 0) chat.Add(this.GetLocalizedValue("Chat.NoBoss"));

                return chat.Get();
            }
        }

        private static readonly ShopCategory[] ShopCategoryList = new ShopCategory[]
        {
            new ShopCategory(
                "PreBag",
                "Mods.LaziestNPC.NPCs.BossNPC.ShopCategory.PreBag",
                "LaziestNPC/Assets/ShopIcons/BossNPC/PreBag", 2.5f),

            new ShopCategory(
                "HardBag",
                "Mods.LaziestNPC.NPCs.BossNPC.ShopCategory.HardBag",
                "LaziestNPC/Assets/ShopIcons/BossNPC/HardBag", 2.5f),

            new ShopCategory(
                "PostMoonBag",
                "Mods.LaziestNPC.NPCs.BossNPC.ShopCategory.PostMoonBag",
                "LaziestNPC/Assets/ShopIcons/BossNPC/PostMoonBag", 2.5f),

            new ShopCategory(
                "PreSum",
                "Mods.LaziestNPC.NPCs.BossNPC.ShopCategory.PreSum",
                "LaziestNPC/Assets/ShopIcons/BossNPC/PreSum", 2.5f),

            new ShopCategory(
                "HardSum",
                "Mods.LaziestNPC.NPCs.BossNPC.ShopCategory.HardSum",
                "LaziestNPC/Assets/ShopIcons/BossNPC/HardSum", 2.5f),

            new ShopCategory(
                "PostMoonSum",
                "Mods.LaziestNPC.NPCs.BossNPC.ShopCategory.PostMoonSum",
                "LaziestNPC/Assets/ShopIcons/BossNPC/PostMoonSum", 2.5f),
        };

        public override IReadOnlyList<ShopCategory> ShopCategories => ShopCategoryList;

        public override void SetChatButtons(ref string button, ref string button2)
        {
            ShopCategory category = CurrentShopCategory;
            button = category != null ? Language.GetTextValue(category.NameKey) : "";
            button2 = null;
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (!firstButton)
                return;

            ShopCategory category = CurrentShopCategory;
            if (category != null)
                shopName = category.ShopName;
        }

        public override void AddShops()
        {
            var PreBag = new NPCShop(Type, "PreBag");
            var HardBag = new NPCShop(Type, "HardBag");
            var PostMoonBag = new NPCShop(Type, "PostMoonBag");
            var PreSum = new NPCShop(Type, "PreSum");
            var HardSum = new NPCShop(Type, "HardSum");
            var PostMoonSum = new NPCShop(Type, "PostMoonSum");

            AddPreBagShop(PreBag);
            AddHardBagShop(HardBag);
            AddPostMoonBagShop(PostMoonBag);
            AddPreSumShop(PreSum);
            AddHardSumShop(HardSum);
            AddPostMoonSumShop(PostMoonSum);

            PreBag.Register();
            HardBag.Register();
            PostMoonBag.Register();
            PreSum.Register();
            HardSum.Register();
            PostMoonSum.Register();
        }

        public void AddPreBagShop(NPCShop PreBag)
        {
            #region 【宝藏袋(肉前)】

            #region [原版]
            //肉前
            PreBag.AddItem(ItemType<TheReturner>(), (0, 0, 0, 1)) //归乡者吊坠
            .AddItem(ItemType<DoubleFishJade>(), (0, 0, 0, 1), CanBuyFishJadeFirst) //双鱼玉佩
            .AddItem(ItemType<DoubleFishJade>(), (0, 0, 0, 1), CanBuyFishJadeSecond) //双鱼玉佩
            .AddItem(ItemType<PlatinumGoblet>(), (1, 0, 0, 0)) //白金高脚杯
            .AddItem(ItemID.KingSlimeBossBag, (0, 5, 0, 0), Condition.DownedKingSlime) //史莱姆王
            .AddItem(ItemID.EyeOfCthulhuBossBag, (0, 10, 0, 0), Condition.DownedEyeOfCthulhu) //克苏鲁之眼
            .AddItem(ItemID.BrainOfCthulhuBossBag, (0, 15, 0, 0), DownedBrainOfCthulhu) //克苏鲁之脑
            .AddItem(ItemID.EaterOfWorldsBossBag, (0, 15, 0, 0), DownedEaterOfWorlds) //世界吞噬怪
            .AddItem(ItemID.QueenBeeBossBag, (0, 20, 0, 0), Condition.DownedQueenBee) //蜂王
            .AddItem(ItemID.SkeletronBossBag, (0, 25, 0, 0), Condition.DownedSkeletron) //骷髅王
            .AddItem(ItemID.DeerclopsBossBag, (0, 30, 0, 0), Condition.DownedDeerclops) //独眼巨鹿
            .AddItem(ItemID.WallOfFleshBossBag, (0, 35, 0, 0), Condition.Hardmode); //肉山

            #endregion

            #region [灾厄 Calamity Mod]
            //肉前
            PreBag.AddModItem("CalamityMod/DesertScourgeBag", (0, 5, 0, 0), DownedDesertBug) //荒漠灾虫
            .AddModItem("CalamityMod/CrabulonBag", (0, 5, 0, 0), DownedCrabulon) //菌生蟹
            .AddModItem("CalamityMod/PerforatorBag", (0, 10, 0, 0), DownedPerforator) //血肉宿主
            .AddModItem("CalamityMod/HiveMindBag", (0, 10, 0, 0), DownedHiveMind) //腐巢意志
            .AddModItem("CalamityMod/SlimeGodBag", (0, 35, 0, 0), DownedSlimeGod); //史莱姆之神

            #endregion

            #region [Fargo魂石 Fargo's Souls Mod]
            //肉前
            PreBag.AddModItem("FargowiltasSouls/TrojanSquirrelBag", (0, 5, 0, 0), DownedTrojanSquirrel) //特洛伊松鼠
            .AddModItem("FargowiltasSouls/CursedCoffinBag", (0, 5, 0, 0), DownedCursedCoffin) //咒缚灵棺
            .AddModItem("FargowiltasSouls/DeviBag", (0, 35, 0, 0), DownedDevi); //戴薇安

            #endregion

            #region [瑟银 Thorium Mod]
            //肉前
            PreBag.AddModItem("ThoriumMod/TheGrandThunderBirdTreasureBag", (0, 5, 0, 0), DownedGrandThunderBird) //惊雷王鹰
            .AddModItem("ThoriumMod/QueenJellyfishTreasureBag", (0, 15, 0, 0), DownedQueenJellyfish) //水母皇后
            .AddModItem("ThoriumMod/ViscountTreasureBag", (0, 20, 0, 0), DownedViscount) //蝙蝠子爵
            .AddModItem("ThoriumMod/GraniteEnergyStormTreasureBag", (0, 25, 0, 0), DownedGraniteEnergyStorm) //花岗岩流能风暴
            .AddModItem("ThoriumMod/BuriedChampionTreasureBag", (0, 25, 0, 0), DownedBuriedChampion) //英灵遗骸
            .AddModItem("ThoriumMod/StarScouterTreasureBag", (0, 30, 0, 0), DownedStarScouter); //星际监察者

            #endregion

            #region [旅人归途 Homeward Journey]
            //肉前
            PreBag.AddModItem("ContinentOfJourney/GoblinChariotTreasureBag", (0, 5, 0, 0), DownedGoblinChariot) //哥布林战车
            .AddModItem("ContinentOfJourney/BigDipperTreasureBag", (0, 10, 0, 0), DownedBigDipper) //大熊星座
            .AddModItem("ContinentOfJourney/PuppetOperaTreasureBag", (0, 25, 0, 0), DownedPuppetOpera) //木偶歌剧
            .AddModItem("ContinentOfJourney/MarquisMoonsquidTreasureBag", (0, 25, 0, 0), DownedMarquisMoonsquid); //月光鱿侯

            #endregion

            #endregion

        }

        public void AddHardBagShop(NPCShop HardBag)
        {
            #region 【宝藏袋(肉后)】

            #region [原版]
            //肉后
            HardBag.AddItem(ItemID.QueenSlimeBossBag, (0, 40, 0, 0), Condition.DownedQueenSlime) //史莱姆皇后
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

            #region [灾厄 Calamity Mod]
            //肉后
            HardBag.AddModItem("CalamityMod/CryogenBag", (0, 40, 0, 0), DownedCryogen) //极地之灵
            .AddModItem("CalamityMod/AquaticScourgeBag", (0, 40, 0, 0), DownedAquaticBug) //渊海灾虫
            .AddModItem("CalamityMod/BrimstoneElementalBag", (0, 40, 0, 0), DownedBrimstone) //硫磺火元素
            .AddModItem("CalamityMod/CalamitasCloneBag", (0, 50, 0, 0), DownedCalamitas) //灾厄之影
            .AddModItem("CalamityMod/LeviathanBag", (0, 55, 0, 0), DownedLeviathan) //阿娜希塔和利维坦
            .AddModItem("CalamityMod/AstrumAureusBag", (0, 55, 0, 0), DownedAstrum) //白金星舰
            .AddModItem("CalamityMod/PlaguebringerGoliathBag", (0, 65, 0, 0), DownedPlague) //瘟疫使者歌莉娅
            .AddModItem("CalamityMod/RavagerBag", (0, 65, 0, 0), DownedRavager) //毁灭魔像
            .AddModItem("CalamityMod/AstrumDeusBag", (0, 70, 0, 0), DownedAstrumBug); //星神游龙

            #endregion

            #region [灾劫 Catalyst Mod]
            HardBag.AddModItem("CatalystMod/AstrageldonBag", (1, 0, 0, 0), DownedAstrageldon); //末世星史莱姆

            #endregion

            #region [Fargo魂石 Fargo's Souls Mod]
            //肉后
            HardBag.AddModItem("FargowiltasSouls/BanishedBaronBag", (0, 40, 0, 0), DownedBanishedBaron) //放逐遗爵
            .AddModItem("FargowiltasSouls/LifelightBag", (0, 45, 0, 0), DownedLifelight); //飘渺游光

            #endregion

            #region [瑟银 Thorium Mod]
            //肉后            
            HardBag.AddModItem("ThoriumMod/BoreanStriderTreasureBag", (0, 40, 0, 0), DownedBoreanStrider) //极地遁蛛
            .AddModItem("ThoriumMod/FallenBeholderTreasureBag", (0, 40, 0, 0), DownedFallenBeholder) //堕落注视者
            .AddModItem("ThoriumMod/LichTreasureBag", (0, 45, 0, 0), DownedLich) //巫妖
            .AddModItem("ThoriumMod/ForgottenOneTreasureBag", (0, 65, 0, 0), DownedForgottenOne) //遗忘之主
            .AddModItem("ThoriumMod/ThePrimordialsTreasureBag", (0, 80, 0, 0), DownedThePrimordials); //始生灾灵
            //迷你
            HardBag.AddModItem("ThoriumMod/DarkMageTreasureBag", (0, 15, 0, 0), Condition.DownedOldOnesArmyT1) //黑暗魔法师
            .AddModItem("ThoriumMod/FlyingDutchmanTreasureBag", (0, 40, 0, 0), Condition.DownedPirates) //荷兰飞盗船
            .AddModItem("ThoriumMod/OgreTreasureBag", (0, 45, 0, 0), Condition.DownedOldOnesArmyT2) //食人魔
            .AddModItem("ThoriumMod/MartianSaucerTreasureBag", (0, 65, 0, 0), Condition.DownedMartians); //火星飞碟

            #endregion

            #region [旅人归途 Homeward Journey]
            //肉后
            HardBag.AddModItem("ContinentOfJourney/WeatherReportTreasureBag", (0, 40, 0, 0), DownedWeatherReport) //天气预报
            .AddModItem("ContinentOfJourney/PriestessRodTreasureBag", (0, 45, 0, 0), DownedPriestessRod) //天竿鱼祭司
            .AddModItem("ContinentOfJourney/DiverTreasureBag", (0, 50, 0, 0), DownedDiver) //深潜者
            .AddModItem("ContinentOfJourney/TheMotherbrainTreasureBag", (0, 55, 0, 0), DownedTheMotherbrain); //母脑

            #endregion

            #endregion

        }

        public void AddPostMoonBagShop(NPCShop PostMoonBag)
        {
            #region 【宝藏袋(月后)】

            #region [灾厄 Calamity Mod]
            //月后
            PostMoonBag.AddModItem("CalamityMod/DragonfollyBag", (0, 75, 0, 0), DownedDragonfolly) //痴愚金龙
            .AddModItem("CalamityMod/ProvidenceBag", (0, 80, 0, 0), DownedProvidence) //亵渎天神
            .AddModItem("CalamityMod/StormWeaverBag", (0, 85, 0, 0), DownedStormWeaver) //风暴编织者
            .AddModItem("CalamityMod/CeaselessVoidBag", (0, 85, 0, 0), DownedVoid) //无尽虚空
            .AddModItem("CalamityMod/SignusBag", (0, 85, 0, 0), DownedSignus); //西格纳斯
            PostMoonBag.AddModItem("CalamityMod/PolterghastBag", (0, 90, 0, 0), DownedPolterghast) //噬魂幽花
            .AddModItem("CalamityMod/OldDukeBag", (0, 95, 0, 0), DownedOldDuke) //硫海遗爵
            .AddModItem("CalamityMod/DevourerofGodsBag", (1, 0, 0, 0), DownedDOG) //神明吞噬者
            .AddModItem("CalamityMod/YharonBag", (2, 0, 0, 0), DownedYharon) //犽戎
            .AddModItem("CalamityMod/DraedonBag", (3, 0, 0, 0), DownedDraedon) //星流巨械
            .AddModItem("CalamityMod/CalamitasCoffer", (5, 0, 0, 0), DownedSCala); //终灾

            #endregion

            #region [Fargo魂石 Fargo's Souls Mod]
            //月后
            PostMoonBag.AddModItem("FargowiltasSouls/CosmosBag", (0, 90, 0, 0), DownedCosmosChampion) //宇宙英灵
            .AddModItem("FargowiltasSouls/AbomBag", (5, 0, 0, 0), DownedAbom) //憎恶
            .AddModItem("FargowiltasSouls/MutantBag", (10, 0, 0, 0), DownedMutant); //突变体

            #endregion

            #region [旅人归途 Homeward Journey]
            //月后
            PostMoonBag.AddModItem("ContinentOfJourney/WallofShadowTreasureBag", (0, 75, 0, 0), DownedWallofShadow); //暗影墙
            //暗影墙后
            PostMoonBag.AddModItem("ContinentOfJourney/SlimeGodTreasureBag", (0, 80, 0, 0), DownedSunSlimeGod) //史莱姆之神
            .AddModItem("ContinentOfJourney/TheOverwatcherTreasureBag", (1, 0, 0, 0), DownedTheOverwatcher) //极视终表
            .AddModItem("ContinentOfJourney/TheLifebringerTreasureBag", (1, 0, 0, 0), DownedTheLifebringer) //万籁携生
            .AddModItem("ContinentOfJourney/TheMaterealizerTreasureBag", (1, 0, 0, 0), DownedTheMaterealizer) //化物天匠
            .AddItem(ItemType<HJTanks>(), (5, 0, 0, 0), DownedTheThree) //古老世界之罐 
            .AddModItem("ContinentOfJourney/ScarabBeliefTreasureBag", (1, 0, 0, 0), DownedScarabBelief) //圣甲虫信仰
            .AddModItem("ContinentOfJourney/EverlastingFallingWhaleTreasureBag", (4, 0, 0, 0), DownedFallingWhale) //世末·永落鲸
            .AddModItem("ContinentOfJourney/TheSonTreasureBag", (5, 0, 0, 0), DownedTheSon); //圣子

            #endregion

            #endregion

        }

        public void AddPreSumShop(NPCShop PreSum)
        {
            #region 【召唤物品(肉前)】

            #region [原版]
            //肉前 事件
            PreSum.AddItem(ItemID.BloodMoonStarter, (0, 1, 0, 0)) //血月
            .AddItem(ItemID.GoblinBattleStandard, (0, 1, 0, 0)); //哥布林入侵
            //肉前 Boss
            PreSum.AddItem(ItemID.SlimeCrown, (0, 1, 0, 0)) //史莱姆王
            .AddItem(ItemID.SuspiciousLookingEye, (0, 1, 0, 0)) //克苏鲁之眼
            .AddItem(ItemID.BloodySpine, (0, 5, 0, 0)) //克苏鲁之脑
            .AddItem(ItemID.WormFood, (0, 5, 0, 0)) //世界吞噬怪
            .AddItem(ItemID.Abeemination, (0, 5, 0, 0)) //蜂王
            .AddItem(ItemType<GentleSkull>(), (0, 5, 0, 0)) //骷髅王
            .AddItem(ItemID.DeerThing, (0, 5, 0, 0)) //独眼巨鹿
            .AddItem(ItemID.GuideVoodooDoll, (0, 8, 0, 0)); //血肉之墙

            #endregion

            #region [炼金术士NPC AlchemistNPC Lite]
            PreSum.AddModItem("AlchemistNPCLite/SingleUseCellphone", (0, 1, 0, 0)) //一次性手机
            .AddModItem("AlchemistNPCLite/HorrifyingSkull", (0, 1, 0, 0)); //恐怖骷髅头

            #endregion

            #region [灾厄 Calamity Mod]
            //肉前 事件
            PreSum.AddModItem("CalamityMod/TorrentialTear", (0, 1, 50, 0), Condition.DownedSkeletron) //雨
            .AddModItem("CalamityMod/CausticTear", (0, 1, 50, 0), Condition.DownedEyeOfCthulhu); //酸雨

            //肉前 Boss
            PreSum.AddModItem("CalamityMod/DesertMedallion", (0, 1, 0, 0)) //荒漠灾虫
            .AddModItem("CalamityMod/DecapoditaSprout", (0, 1, 0, 0)) //菌生蟹
            .AddModItem("CalamityMod/BloodyWormFood", (0, 5, 0, 0), PerOrHive) //血肉宿主
            .AddModItem("CalamityMod/Teratoma", (0, 5, 0, 0), PerOrHive) //腐巢意志
            .AddModItem("CalamityMod/OverloadedSludge", (0, 8, 0, 0)); //史莱姆之神

            #endregion

            #region [Fargo突变 Fargo's Mutant Mod]
            //肉前 稀有敌怪
            PreSum.AddModItem("Fargowiltas/WormSnack", (0, 1, 0, 0)) //巨型蠕虫/挖掘怪
            .AddModItem("Fargowiltas/PinkSlimeCrown", (0, 5, 0, 0)) //粉史莱姆
            .AddModItem("Fargowiltas/GnomeHat", (0, 1, 0, 0)) //侏儒
            .AddModItem("Fargowiltas/GoblinScrap", (0, 2, 0, 0)) //哥布林侦察兵
            .AddModItem("Fargowiltas/AttractiveOre", (0, 3, 0, 0)) //不死矿工
            .AddModItem("Fargowiltas/HeartChocolate", (0, 5, 0, 0)) //宁芙
            .AddModItem("Fargowiltas/HolyGrail", (0, 5, 0, 0)) //蒂姆
            .AddModItem("Fargowiltas/Eggplant", (0, 5, 0, 0)) //骷髅博士
            .AddModItem("Fargowiltas/GoldenSlimeCrown", (0, 10, 0, 0)) //金史莱姆
            .AddModItem("Fargowiltas/SuspiciousLookingLure", (0, 5, 0, 0), BloodMoonHappened) //游荡眼球怪鱼/僵尸人鱼
            .AddModItem("Fargowiltas/SlimyLockBox", (0, 5, 0, 0), Condition.DownedSkeletron); //地牢史莱姆

            #endregion

            #region [Fargo魂石 Fargo's Souls Mod]
            //肉前            
            PreSum.AddModItem("FargowiltasSouls/SquirrelCoatofArms", (0, 1, 0, 0)) //特洛伊松鼠
            .AddModItem("FargowiltasSouls/CoffinSummon", (0, 1, 0, 0)) //咒缚灵棺
            .AddModItem("FargowiltasSouls/DevisCurse", (0, 10, 0, 0)); //戴薇安

            #endregion

            #region [瑟银 Thorium Mod]
            //肉前
            PreSum.AddModItem("ThoriumMod/GrandFlareGun", (0, 1, 0, 0)) //惊雷王鹰
            .AddModItem("ThoriumMod/JellyfishResonator", (0, 1, 0, 0)) //水母皇后
            .AddModItem("ThoriumMod/GraniteEnergyCore", (0, 5, 0, 0), Condition.DownedSkeletron) //花岗岩流能风暴
            .AddModItem("ThoriumMod/AncientBlade", (0, 5, 0, 0), Condition.DownedSkeletron) //英灵遗骸
            .AddModItem("ThoriumMod/StarCaller", (0, 7, 0, 0), Condition.DownedEowOrBoc); //星际监察者

            #endregion

            #region [旅人归途 Homeward Journey]
            //肉前 Boss
            PreSum.AddModItem("ContinentOfJourney/PurpleFlareGun", (0, 1, 0, 0)) //紫信号枪 (哥布林战车)
            .AddModItem("ContinentOfJourney/MaliciousPacket", (0, 5, 0, 0), Condition.DownedEowOrBoc); //恶意捆绑包 (大熊星座)
            //肉前 事件
            //星座奖牌            
            PreSum.AddModItem("ContinentOfJourney/CapricornMedal", (0, 5, 0, 0), DownedBigDipper) //摩羯座奖牌
            .AddModItem("ContinentOfJourney/GeminiMedal", (0, 5, 0, 0), DownedBigDipper) //双子座奖牌
            .AddModItem("ContinentOfJourney/LibraMedal", (0, 5, 0, 0), DownedBigDipper) //天秤座奖牌
            .AddModItem("ContinentOfJourney/ScorpioMedal", (0, 5, 0, 0), DownedBigDipper) //天蝎座奖牌
            .AddModItem("ContinentOfJourney/TaurusMedal", (0, 5, 0, 0), DownedBigDipper) //金牛座奖牌
            .AddModItem("ContinentOfJourney/VirgoMedal", (0, 5, 0, 0), DownedBigDipper); //室女座奖牌
            //天气水晶
            PreSum.AddModItem("ContinentOfJourney/Crystal_Clear", (0, 1, 0, 0)) //天气水晶：晴朗
            .AddModItem("ContinentOfJourney/Crystal_LowPressure", (0, 1, 0, 0)) //天气水晶：低压
            .AddModItem("ContinentOfJourney/Crystal_HighPressure", (0, 1, 0, 0)) //天气水晶：高压
            .AddModItem("ContinentOfJourney/Crystal_Cold", (0, 1, 0, 0)) //天气水晶：低温
            .AddModItem("ContinentOfJourney/Crystal_Hot", (0, 1, 0, 0)) //天气水晶：高温
            .AddModItem("ContinentOfJourney/Crystal_Harvest", (0, 1, 0, 0)) //天气水晶：丰收
            .AddModItem("ContinentOfJourney/Crystal_Typhoon", (0, 1, 0, 0)) //天气水晶：台风
            .AddModItem("ContinentOfJourney/Crystal_Nuclear", (0, 1, 0, 0)); //天气水晶：辐射

            #endregion

            #endregion

        }

        public void AddHardSumShop(NPCShop HardSum)
        {
            #region 【召唤物品(肉后)】

            #region [原版]
            //肉后 事件
            HardSum.AddItem(ItemID.PirateMap, (0, 5, 0, 0), Condition.Hardmode) //海盗入侵
            .AddItem(ItemID.SnowGlobe, (0, 5, 0, 0), Condition.Hardmode) //雪人军团
            .AddItem(ItemID.SolarTablet, (0, 10, 0, 0), Condition.DownedMechBossAny) //日食
            .AddItem(ItemID.PumpkinMoonMedallion, (0, 15, 0, 0), Condition.DownedPlantera) //南瓜月
            .AddItem(ItemID.NaughtyPresent, (0, 15, 0, 0), Condition.DownedPlantera); //霜月
            //肉后 Boss
            HardSum.AddItem(ItemID.QueenSlimeCrystal, (0, 10, 0, 0), Condition.Hardmode) //史莱姆皇后
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

            #region [灾厄 Calamity Mod]
            //肉后 事件
            HardSum.AddModItem("CalamityMod/AridArtifact", (0, 5, 0, 0), Condition.Hardmode) //沙尘暴
            .AddModItem("CalamityMod/MartianDistressRemote", (0, 20, 0, 0), Condition.DownedGolem); //火星暴乱
            //肉后 Boss
            HardSum.AddModItem("CalamityMod/CryoKey", (0, 10, 0, 0), Condition.Hardmode) //极地之灵
            .AddModItem("CalamityMod/Seafood", (0, 10, 0, 0), Condition.Hardmode) //渊海灾虫
            .AddModItem("CalamityMod/CharredIdol", (0, 10, 0, 0), Condition.Hardmode) //硫磺火元素
            .AddModItem("CalamityMod/EyeofDesolation", (0, 20, 0, 0), Condition.Hardmode) //灾厄之影
            .AddModItem("CalamityMod/AstralChunk", (0, 25, 0, 0), Condition.Hardmode) //白金星舰
            .AddModItem("CalamityMod/Abombination", (0, 30, 0, 0), Condition.DownedGolem) //瘟疫使者歌莉娅
            .AddModItem("CalamityMod/DeathWhistle", (0, 30, 0, 0), Condition.DownedGolem) //毁灭魔像
            .AddModItem("CalamityMod/TitanHeart", (0, 35, 0, 0), Condition.Hardmode); //星神游龙

            #endregion

            #region [灾劫 Catalyst Mod]
            HardSum.AddModItem("CatalystMod/AstralCommunicator", (0, 55, 0, 0), Condition.Hardmode); //末世星史莱姆

            #endregion

            #region [Fargo突变 Fargo's Mutant Mod]
            //肉后 稀有敌怪
            HardSum.AddModItem("Fargowiltas/SuspiciousLookingChest", (0, 5, 0, 0), Condition.Hardmode) //宝箱怪/冰雪宝箱怪
            .AddModItem("Fargowiltas/CrimsonChest", (0, 6, 0, 0), Condition.Hardmode) //猩红宝箱怪
            .AddModItem("Fargowiltas/CorruptChest", (0, 6, 0, 0), Condition.Hardmode) //腐化宝箱怪
            .AddModItem("Fargowiltas/HallowChest", (0, 6, 0, 0), Condition.Hardmode) //神圣宝箱怪
            .AddModItem("Fargowiltas/JungleChest", (0, 6, 0, 0), Condition.Hardmode); //丛林宝箱怪
            HardSum.AddModItem("Fargowiltas/AthenianIdol", (0, 5, 0, 0), Condition.Hardmode) //蛇发女妖
            .AddModItem("Fargowiltas/RuneOrb", (0, 5, 0, 0), Condition.Hardmode) //符文巫师
            .AddModItem("Fargowiltas/ClownLicense", (0, 5, 0, 0), Condition.Hardmode) //小丑
            .AddModItem("Fargowiltas/CloudSnack", (0, 6, 0, 0), Condition.Hardmode) //飞龙
            .AddModItem("Fargowiltas/DilutedRainbowMatter", (0, 5, 0, 0), Condition.Hardmode) //彩虹史莱姆
            .AddModItem("Fargowiltas/CoreoftheFrostCore", (0, 6, 0, 0), Condition.Hardmode) //冰雪巨人
            .AddModItem("Fargowiltas/ForbiddenForbiddenFragment", (0, 6, 0, 0), Condition.Hardmode); //沙尘精
            HardSum.AddModItem("Fargowiltas/ShadowflameIcon", (0, 6, 0, 0), GoblinArmyHappened, Condition.Hardmode) //哥布林术士
            .AddModItem("Fargowiltas/PirateFlag", (0, 6, 0, 0), Condition.Hardmode, Condition.DownedPirates) //海盗船长
            .AddModItem("Fargowiltas/HemoclawCrab", (0, 6, 0, 0), Condition.Hardmode) //血浆哥布林鲨鱼
            .AddModItem("Fargowiltas/BloodUrchin", (0, 6, 0, 0), Condition.Hardmode) //血鳗鱼
            .AddModItem("Fargowiltas/BloodSushiPlatter", (0, 10, 0, 0), Condition.Hardmode) //恐惧鹦鹉螺
            .AddModItem("Fargowiltas/MothLamp", (0, 6, 0, 0), Condition.Hardmode); //蛾
            HardSum.AddModItem("Fargowiltas/DemonicPlushie", (0, 8, 0, 0), Condition.DownedMechBossAny) //红魔鬼
            .AddModItem("Fargowiltas/Pincushion", (0, 10, 0, 0), Condition.DownedPlantera) //钉头
            .AddModItem("Fargowiltas/MothronEgg", (0, 10, 0, 0), Condition.DownedPlantera) //蛾怪
            .AddModItem("Fargowiltas/AmalgamatedSkull", (0, 10, 0, 0), Condition.DownedPlantera) //骷髅狙击手+骷髅特警+骷髅突击手
            .AddModItem("Fargowiltas/AmalgamatedSpirit", (0, 10, 0, 0), Condition.DownedPlantera) //褴褛邪教徒法师+死灵法师+魔教徒
            .AddModItem("Fargowiltas/LeesHeadband", (0, 10, 0, 0), Condition.DownedPlantera) //骷髅李
            .AddModItem("Fargowiltas/GrandCross", (0, 10, 0, 0), Condition.DownedPlantera); //圣骑士

            #endregion

            #region [Fargo魂石 Fargo's Souls Mod]
            //肉后
            HardSum.AddModItem("FargowiltasSouls/MechLure", (0, 10, 0, 0), Condition.Hardmode) //放逐遗爵
            .AddModItem("FargowiltasSouls/FragilePixieLamp", (0, 15, 0, 0), Condition.Hardmode); //飘渺游光

            #endregion

            #region [瑟银 Thorium Mod]
            //肉后
            HardSum.AddModItem("ThoriumMod/StriderTear", (0, 10, 0, 0), Condition.Hardmode) //极地遁蛛
            .AddModItem("ThoriumMod/VoidLens", (0, 10, 0, 0), Condition.Hardmode) //堕落注视者
            .AddModItem("ThoriumMod/AbyssalShadow2", (0, 25, 0, 0), Condition.DownedPlantera) //遗忘之主
            .AddModItem("ThoriumMod/DoomSayersCoin", (0, 35, 0, 0), Condition.DownedCultist); //始生灾灵

            #endregion

            #region [旅人归途 Homeward Journey]
            //肉后 事件
            HardSum.AddModItem("ContinentOfJourney/Crystal_Coin", (0, 10, 0, 0), Condition.Hardmode) //天气水晶：硬币雨
            .AddModItem("ContinentOfJourney/Crystal_Knife", (0, 15, 0, 0), Condition.DownedSkeletronPrime) //天气水晶：刀子雨
            .AddModItem("ContinentOfJourney/Crystal_Donut", (0, 15, 0, 0), Condition.DownedMechBossAny) //天气水晶：甜甜圈季风
            .AddModItem("ContinentOfJourney/Crystal_Mist", (0, 20, 0, 0), PumpkinMoonHappened, SnowMoonHappened) //天气水晶：迷雾
            .AddModItem("ContinentOfJourney/Crystal_Oxygen", (0, 25, 0, 0), DownedDiver); //天气水晶：纯氧
            //肉后 稀有敌怪
            HardSum.AddModItem("ContinentOfJourney/DivineKey", (0, 10, 0, 0), Condition.Hardmode) //圣洁钥匙 (沙漠宝箱怪)
            .AddModItem("ContinentOfJourney/PolarKey", (0, 10, 0, 0), Condition.Hardmode) //冰川钥匙 (极地宝箱怪)            
            .AddModItem("ContinentOfJourney/LihzahrdKey", (0, 10, 0, 0), Condition.Hardmode); //蜥蜴钥匙 (神庙宝箱怪)
            //肉后 Boss
            HardSum.AddModItem("ContinentOfJourney/CyanFlareGun", (0, 10, 0, 0), Condition.Hardmode) //青色信号枪 (天气预报)
            .AddModItem("ContinentOfJourney/CannedSoulofFlight", (0, 15, 0, 0), Condition.DownedMechBossAny) //罐装飞翔之魂 (天竿鱼祭司)
            .AddModItem("ContinentOfJourney/UnstableGlobe", (0, 20, 0, 0), DownedDiver) //不稳定空间裂缝 (深潜者)
            .AddModItem("ContinentOfJourney/MetalSpine", (0, 25, 0, 0), Condition.DownedPlantera); //机械脊椎 (母脑)

            #endregion

            #endregion

        }

        public void AddPostMoonSumShop(NPCShop PostMoonSum)
        {
            #region 【召唤物品(月后)】

            #region [灾厄 Calamity Mod]
            //月后
            PostMoonSum.AddModItem("CalamityMod/ExoticPheromones", (0, 30, 0, 0), Condition.DownedCultist) //痴愚金龙
            .AddModItem("CalamityMod/ProfanedShard", (0, 30, 0, 0), Condition.DownedMoonLord) //亵渎守卫
            .AddModItem("CalamityMod/ProfanedCore", (0, 35, 0, 0), DownedGuardians) //亵渎天神
            .AddModItem("CalamityMod/MarkofProvidence", (0, 40, 0, 0), DownedProvidence) //风暴编织者/无尽虚空/西格纳斯
            .AddModItem("CalamityMod/NecroplasmicBeacon", (0, 45, 0, 0), Condition.DownedMoonLord) //噬魂幽花
            .AddModItem("CalamityMod/BloodwormItem", (0, 50, 0, 0), Condition.DownedMoonLord) //硫海遗爵
            .AddModItem("CalamityMod/CosmicWorm", (0, 55, 0, 0), Condition.DownedMoonLord) //神明吞噬者
            .AddModItem("CalamityMod/YharonEgg", (0, 60, 0, 0), DownedDragonfolly) //犽戎
            .AddModItem("CalamityMod/CeremonialUrn", (0, 65, 0, 0), DownedSCala); //终灾

            #endregion

            #region [Fargo魂石 Fargo's Souls Mod]
            //月后
            PostMoonSum.AddModItem("FargowiltasSouls/SigilOfChampions", (0, 50, 0, 0), Condition.DownedMoonLord) //宇宙英灵
            .AddModItem("FargowiltasSouls/AbomsCurse", (0, 66, 0, 0), Condition.DownedMoonLord) //憎恶
            .AddModItem("FargowiltasSouls/MutantsCurse", (1, 0, 0, 0), DownedAbom); //突变体

            #endregion

            #region [旅人归途 Homeward Journey]
            //月后 事件
            PostMoonSum.AddModItem("ContinentOfJourney/Crystal_Timenado", (0, 30, 0, 0), DownedWallofShadow) //天气水晶：时光隧道
            .AddModItem("ContinentOfJourney/Crystal_Threebody", (0, 40, 0, 0), DownedSunSlimeGod); //天气水晶：三体
            //月后 Boss
            PostMoonSum.AddModItem("ContinentOfJourney/UltimateTorch", (0, 30, 0, 0), Condition.DownedMoonLord); //究极火把 (暗影墙)
            //暗影墙后
            PostMoonSum.AddModItem("ContinentOfJourney/SunlightCrown", (0, 40, 0, 0), DownedWallofShadow) //日冕 (史莱姆之神)
            .AddModItem("ContinentOfJourney/HallowedAltar", (0, 50, 0, 0), DownedWallofShadow); //神圣祭坛

            #endregion

            #endregion

        }
    }
}
