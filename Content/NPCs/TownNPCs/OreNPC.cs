using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Common.ModBossess;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;
using Terraria.GameContent;
using LaziestNPC.Globals.Config;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Globals.GlobalPlayers;
using LaziestNPC.Common.UI;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class OreNPC : TownLNPC
    {
        protected override string BestiaryText => "Mods.LaziestNPC.Bestiary.OreNPC";
        protected override string NPCNameText => "Name.OreNPC";
        protected override int AttackProjType => ProjectileID.Leaf;
        protected override int AttackProjDamage => 4;

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

        public override ITownNPCProfile TownNPCProfile()
        {
            return new Profiles.DefaultNPCProfile(
                Texture,
                NPCHeadLoader.GetHeadSlot(HeadTexture),
                Texture + "_Party"
            );
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (LNPCConfig.Instance.LNPCOreNPC)
            {
                foreach (var player in Main.ActivePlayers)
                {
                    //玩家的背包中存在【铁矿】或【铅矿】时，NPC生成
                    if (player.inventory.Any(item => item.type == ItemID.IronOre || item.type == ItemID.LeadOre))
                        return true;
                }
            }
            return false;
        }

        private static readonly (string Flag, string ChatKey)[] NPCBossChats =
        {
            //肉前
            ("WallOfFlesh_OreNPC", "Chat.WallOfFlesh00"), //血肉之墙
        };

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

            if (Main.rand.Next(100) < 70)
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                chat.Add(this.GetLocalizedValue("Chat.Normal1"));
                chat.Add(this.GetLocalizedValue("Chat.Normal2"));
                chat.Add(this.GetLocalizedValue("Chat.Normal3"));
                chat.Add(this.GetLocalizedValue("Chat.Normal4"));
                chat.Add(this.GetLocalizedValue("Chat.Normal5"));

                return chat.Get();

            }
            else
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                if (Main.hardMode) chat.Add(this.GetLocalizedValue("Chat.WallOfFlesh0")); //血肉之墙

                if (chat.elements.Count == 0) chat.Add(this.GetLocalizedValue("Chat.Normal1"));

                return chat.Get();
            }
        }

        private static readonly ShopCategory[] ShopCategoryList = new ShopCategory[]
        {
            new ShopCategory(
                "PreOre",
                "Mods.LaziestNPC.NPCs.OreNPC.ShopCategory.PreOre",
                "LaziestNPC/Assets/ShopIcons/OreNPC/PreOre", 2.5f),

            new ShopCategory(
                "HardOre",
                "Mods.LaziestNPC.NPCs.OreNPC.ShopCategory.HardOre",
                "LaziestNPC/Assets/ShopIcons/OreNPC/HardOre", 2.5f),

            new ShopCategory(
                "PostMoonOre",
                "Mods.LaziestNPC.NPCs.OreNPC.ShopCategory.PostMoonOre",
                "LaziestNPC/Assets/ShopIcons/OreNPC/PostMoonOre", 2.5f),

            new ShopCategory(
                "PreAcc",
                "Mods.LaziestNPC.NPCs.OreNPC.ShopCategory.PreAcc",
                "LaziestNPC/Assets/ShopIcons/OreNPC/PreAcc", 2.5f),

            new ShopCategory(
                "HardAcc",
                "Mods.LaziestNPC.NPCs.OreNPC.ShopCategory.HardAcc",
                "LaziestNPC/Assets/ShopIcons/OreNPC/HardAcc", 2.5f),

            new ShopCategory(
                "PostMoonAcc",
                "Mods.LaziestNPC.NPCs.OreNPC.ShopCategory.PostMoonAcc",
                "LaziestNPC/Assets/ShopIcons/OreNPC/PostMoonAcc", 2.5f),
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
            var PreOre = new NPCShop(Type, "PreOre");
            var HardOre = new NPCShop(Type, "HardOre");
            var PostMoonOre = new NPCShop(Type, "PostMoonOre");
            var PreAcc = new NPCShop(Type, "PreAcc");
            var HardAcc = new NPCShop(Type, "HardAcc");
            var PostMoonAcc = new NPCShop(Type, "PostMoonAcc");

            AddPreOreShop(PreOre);
            AddHardOreShop(HardOre);
            AddPostMoonOreShop(PostMoonOre);
            AddPreAccShop(PreAcc);
            AddHardAccShop(HardAcc);
            AddPostMoonAccShop(PostMoonAcc);

            PreOre.Register();
            HardOre.Register();
            PostMoonOre.Register();
            PreAcc.Register();
            HardAcc.Register();
            PostMoonAcc.Register();
        }

        public void AddPreOreShop(NPCShop PreOre)
        {
            #region 【矿物(肉前)】

            #region [原版]
            //肉前
            //宝石
            PreOre.AddItem(ItemID.Amethyst, (0, 0, 10, 0)) //紫水晶
            .AddItem(ItemID.Topaz, (0, 0, 15, 0)) //黄玉
            .AddItem(ItemID.Sapphire, (0, 0, 25, 0)) //蓝玉
            .AddItem(ItemID.Emerald, (0, 0, 25, 0)) //翡翠
            .AddItem(ItemID.Ruby, (0, 0, 50, 0)) //红玉
            .AddItem(ItemID.Amber, (0, 0, 50, 0)) //琥珀
            .AddItem(ItemID.Diamond, (0, 0, 75, 0)); //钻石

            PreOre.AddItem(ItemID.FossilOre, (0, 0, 25, 0)); //坚固化石
            //矿物
            PreOre.AddItem(ItemID.CopperOre, (0, 0, 25, 0)) //铜矿
            .AddItem(ItemID.TinOre, (0, 0, 25, 0)) //锡矿
            .AddItem(ItemID.IronOre, (0, 0, 50, 0)) //铁矿
            .AddItem(ItemID.LeadOre, (0, 0, 50, 0)) //铅矿
            .AddItem(ItemID.SilverOre, (0, 0, 75, 0)) //银矿
            .AddItem(ItemID.TungstenOre, (0, 0, 75, 0)) //钨矿
            .AddItem(ItemID.GoldOre, (0, 1, 0, 0)) //金矿
            .AddItem(ItemID.PlatinumOre, (0, 1, 0, 0)) //铂金矿
            .AddItem(ItemID.Obsidian, (0, 0, 25, 0)); //黑曜石

            PreOre.AddItem(ItemID.CrimtaneOre, (0, 1, 25, 0)) //猩红矿
            .AddItem(ItemID.DemoniteOre, (0, 1, 25, 0)) //魔矿
            .AddItem(ItemID.Meteorite, (0, 1, 50, 0), Condition.DownedEowOrBoc) //陨石
            .AddItem(ItemID.Hellstone, (0, 1, 50, 0), Condition.DownedEowOrBoc) //狱石
            .AddItem(ItemID.LifeCrystal, (0, 5, 0, 0), Condition.DownedEyeOfCthulhu); //生命水晶

            #endregion

            #region [灾厄 Calamity Mod]
            //肉前
            PreOre.AddModItem("CalamityMod/SeaPrism", (0, 0, 50, 0)) //海棱晶
            .AddModItem("CalamityMod/AerialiteOre", (0, 1, 0, 0), PerOrHive); //天蓝矿

            #endregion

            #region [瑟银 Thorium Mod]
            //肉前 宝石
            PreOre.AddModItem("ThoriumMod/Opal", (0, 0, 25, 0)) //欧珀
            .AddModItem("ThoriumMod/Aquamarine", (0, 0, 25, 0)); //海蓝宝石
            //矿物
            PreOre.AddModItem("ThoriumMod/SmoothCoal", (0, 0, 50, 0)) //光滑的煤
            .AddModItem("ThoriumMod/LifeQuartz", (0, 0, 50, 0)) //生命石英
            .AddModItem("ThoriumMod/ThoriumOre", (0, 0, 75, 0)) //瑟银矿
            .AddModItem("ThoriumMod/Aquaite", (0, 1, 0, 0), Condition.DownedEowOrBoc); //海洋矿

            #endregion

            #region [旅人归途 Homeward Journey]
            PreOre.AddModItem("ContinentOfJourney/HauntedOnyx", (0, 1, 0, 0)); //闹鬼玛瑙

            #endregion

            #endregion

        }

        public void AddHardOreShop(NPCShop HardOre)
        {
            #region 【矿物(肉后)】

            #region [原版]
            //肉后
            HardOre.AddItem(ItemID.CobaltOre, (0, 2, 0, 0), Condition.Hardmode) //钴矿
            .AddItem(ItemID.PalladiumOre, (0, 2, 0, 0), Condition.Hardmode) //钯金矿
            .AddItem(ItemID.MythrilOre, (0, 2, 50, 0), Condition.Hardmode) //秘银矿
            .AddItem(ItemID.OrichalcumOre, (0, 2, 50, 0), Condition.Hardmode) //山铜矿
            .AddItem(ItemID.AdamantiteOre, (0, 3, 0, 0), Condition.Hardmode) //精金矿
            .AddItem(ItemID.TitaniumOre, (0, 3, 0, 0), Condition.Hardmode) //钛金矿
            .AddItem(ItemID.LifeFruit, (0, 10, 0, 0), Condition.DownedMechBossAny) //生命果
            .AddItem(ItemID.ChlorophyteOre, (0, 3, 50, 0), Condition.DownedMechBossAll); //叶绿矿

            #endregion

            #region [灾厄 Calamity Mod]
            //肉后
            HardOre.AddModItem("CalamityMod/InfernalSuevite", (0, 2, 0, 0), Condition.Hardmode) //地狱角砾岩
            .AddModItem("CalamityMod/CryonicOre", (0, 2, 50, 0), DownedCryogen) //寒元矿
            .AddModItem("CalamityMod/PerennialOre", (0, 4, 0, 0), Condition.DownedPlantera) //永恒矿
            .AddModItem("CalamityMod/ScoriaOre", (0, 4, 50, 0), Condition.DownedGolem) //熔渣矿
            .AddModItem("CalamityMod/AstralOre", (0, 4, 0, 0), DownedAstrumBug); //炫星矿

            #endregion

            #region [灾劫 Catalyst Mod]
            //肉后
            HardOre.AddModItem("CatalystMod/MetanovaOre", (0, 8, 0, 0), DownedAstrageldon); //辉恒星矿

            #endregion

            #region [瑟银 Thorium Mod]
            //肉后
            HardOre.AddModItem("ThoriumMod/LodeStoneChunk", (0, 2, 50, 0), DownedFallenBeholder) //地脉矿
            .AddModItem("ThoriumMod/ValadiumChunk", (0, 2, 50, 0), DownedFallenBeholder) //虚金矿
            .AddModItem("ThoriumMod/IllumiteChunk", (0, 4, 0, 0), Condition.DownedPlantera); //荧光石

            #endregion

            #region [旅人归途 Homeward Journey]
            //宝石
            HardOre.AddModItem("ContinentOfJourney/Onyx", (0, 1, 0, 0), Condition.DownedPlantera); //玛瑙
            //矿石
            HardOre.AddModItem("ContinentOfJourney/DeepOre", (0, 3, 0, 0), Condition.DownedPlantera); //深藏矿

            #endregion

            #endregion

        }

        public void AddPostMoonOreShop(NPCShop PostMoonOre)
        {
            #region 【矿物(月后)】

            #region [灾厄 Calamity Mod]
            PostMoonOre.AddModItem("CalamityMod/ExodiumCluster", (0, 5, 0, 0), Condition.DownedMoonLord) //起源之簇
            .AddModItem("CalamityMod/UelibloomOre", (0, 6, 0, 0), DownedProvidence) //龙蒿矿
            .AddModItem("CalamityMod/AuricOre", (0, 9, 0, 0), DownedYharon); //圣金源矿

            #endregion

            #region [旅人归途 Homeward Journey]
            PostMoonOre.AddModItem("ContinentOfJourney/EternalOre", (0, 5, 0, 0), DownedWallofShadow) //永远矿
            .AddModItem("ContinentOfJourney/LivingOre", (0, 5, 0, 0), DownedWallofShadow) //脉搏矿
            .AddModItem("ContinentOfJourney/CubistOre", (0, 5, 0, 0), DownedWallofShadow) //立体矿
            .AddModItem("ContinentOfJourney/FinalOre", (0, 10, 0, 0), DownedTheThree); //最终矿

            #endregion

            #endregion

        }

        public void AddPreAccShop(NPCShop PreAcc)
        {
            #region 【配饰(肉前)】

            #region [原版]

            #region 机动性相关
            //靴子系列
            PreAcc.AddItem(ItemID.Aglet, (0, 2, 0, 0)) //鞋带束头
            .AddItem(ItemID.AnkletoftheWind, (0, 3, 0, 0)) //疾风脚镯
            .AddItem(ItemID.HermesBoots, (0, 2, 0, 0)) //赫尔墨斯靴
            .AddItem(ItemID.WaterWalkingBoots, (0, 2, 0, 0)) //水上漂靴
            .AddItem(ItemID.SailfishBoots, (0, 4, 0, 0)) //旗鱼靴
            .AddItem(ItemID.FlurryBoots, (0, 3, 0, 0)) //疾风雪靴
            .AddItem(ItemID.IceSkates, (0, 4, 0, 0)) //溜冰鞋
            .AddItem(ItemID.FlowerBoots, (0, 3, 0, 0)) //花靴
            .AddItem(ItemID.SandBoots, (0, 3, 0, 0)) //沙丘行者靴
            .AddItem(ItemID.FlyingCarpet, (0, 4, 0, 0)) //飞毯
            .AddItem(ItemID.FrogLeg, (0, 5, 0, 0)); //蛙腿
            //忍者大师装备系列
            PreAcc.AddItem(ItemID.ClimbingClaws, (0, 2, 0, 0)) //攀爬爪
            .AddItem(ItemID.ShoeSpikes, (0, 2, 0, 0)); //鞋钉
            //气瓶和气球
            PreAcc.AddItem(ItemID.ShinyRedBalloon, (0, 3, 0, 0)) //闪亮红气球
            .AddItem(ItemID.CloudinaBottle, (0, 3, 0, 0)) //云朵瓶
            .AddItem(ItemID.BlizzardinaBottle, (0, 4, 0, 0)) //暴雪瓶
            .AddItem(ItemID.SandstorminaBottle, (0, 5, 0, 0)) //沙暴瓶
            .AddItem(ItemID.TsunamiInABottle, (0, 4, 0, 0)) //海啸瓶
            .AddItem(ItemID.BalloonPufferfish, (0, 3, 0, 0)) //气球河豚
            .AddItem(ItemID.LuckyHorseshoe, (0, 2, 0, 0)); //幸运马掌
            //熔岩靴系列
            PreAcc.AddItem(ItemID.LavaCharm, (0, 5, 0, 0)) //熔岩护身符
            .AddItem(ItemID.ObsidianRose, (0, 5, 0, 0)) //黑曜石玫瑰
            .AddItem(ItemID.FlameWakerBoots, (0, 5, 0, 0)); //烈焰靴
            //海洋相关
            PreAcc.AddItem(ItemID.Flipper, (0, 3, 0, 0)) //脚蹼
            .AddItem(ItemID.DivingHelmet, (0, 5, 0, 0)) //潜水头盔
            .AddItem(ItemID.JellyfishNecklace, (0, 5, 0, 0)) //水母项链
            .AddItem(ItemID.FloatingTube, (0, 3, 0, 0)); //浮游圈

            #endregion

            #region 生命与魔力相关
            //生命恢复类
            PreAcc.AddItem(ItemID.BandofRegeneration, (0, 1, 0, 0)); //再生手环
            //魔力恢复类
            PreAcc.AddItem(ItemID.NaturesGift, (0, 3, 0, 0)) //自然恩赐
            .AddItem(ItemID.BandofStarpower, (0, 2, 0, 0)) //星力手环
            .AddItem(ItemID.CelestialMagnet, (0, 3, 0, 0)); //天界磁石

            #endregion

            #region 战斗相关
            //伤害类
            PreAcc.AddItem(ItemID.WhiteString, (0, 1, 0, 0)) //白绳
            .AddItem(ItemID.BlueCounterweight, (0, 1, 0, 0)); //蓝平衡锤
            //战士
            PreAcc.AddItem(ItemID.FeralClaws, (0, 3, 0, 0)) //猛爪手套
            .AddItem(ItemID.MagmaStone, (0, 4, 0, 0)); //岩浆石
            //射手
            PreAcc.AddItem(ItemID.SharkToothNecklace, (0, 2, 0, 0)); //鲨牙项链
            //召唤师
            PreAcc.AddItem(ItemID.SquireShield, (0, 4, 0, 0), DownedDarkMage) //侍卫护盾
            .AddItem(ItemID.ApprenticeScarf, (0, 4, 0, 0), DownedDarkMage); //学徒围巾
            //十字章护盾系列
            PreAcc.AddItem(ItemID.Bezoar, (0, 3, 0, 0)) //牛黄
            .AddItem(ItemID.AdhesiveBandage, (0, 3, 0, 0)) //粘性绷带
            .AddItem(ItemID.HandWarmer, (0, 5, 0, 0)); //暖手宝

            #endregion

            #region 建筑相关
            PreAcc.AddItem(ItemID.PortableStool, (0, 1, 0, 0)) //便携凳(梯凳)
            .AddItem(ItemID.AncientChisel, (0, 5, 0, 0)) //远古凿子
            .AddItem(ItemID.BrickLayer, (0, 5, 0, 0)) //砌砖刀
            .AddItem(ItemID.ExtendoGrip, (0, 5, 0, 0)) //加长握爪
            .AddItem(ItemID.PaintSprayer, (0, 5, 0, 0)) //喷漆器
            .AddItem(ItemID.PortableCementMixer, (0, 5, 0, 0)) //便携式水泥搅拌机
            .AddItem(ItemID.TreasureMagnet, (0, 5, 0, 0), Condition.DownedSkeletron); //宝藏磁石

            #endregion

            #region 其他类
            //其他类
            PreAcc.AddItem(ItemID.CordageGuide, (0, 1, 0, 0)) //植物纤维绳索宝典
            .AddItem(ItemID.Shackle, (0, 1, 0, 0)) //镣铐
            .AddItem(ItemID.PanicNecklace, (0, 2, 0, 0)); //恐慌项链

            #endregion

            #endregion

            /*
            #region [灾厄 Calamity Mod]
            accs.AddModItem("CalamityMod/WulfrumBattery", (0, 3, 0, 0)) //钨钢电池
            .AddModItem("CalamityMod/ScionsCurio", (0, 5, 0, 0)) //硫海珍嗣
            .AddModItem("CalamityMod/VoltaicJelly", (0, 3, 0, 0), DownedDesertBug); //伏打凝胶
            哼哼唧唧
            #endregion

            #region [旅人归途 Homeward Journey]
            //肉前
            PreAcc.AddModItem("ContinentOfJourney/GrindStone", (0, 2, 0, 0)) //磨刀石
            .AddModItem("ContinentOfJourney/Alpha", (0, 2, 0, 0)) //埃尔发
            .AddModItem("ContinentOfJourney/GrayRook", (0, 5, 0, 0)) //灰車
            .AddModItem("ContinentOfJourney/CherryOnFire", (0, 3, 0, 0), Condition.DownedEowOrBoc) //车厘熟了
            .AddModItem("ContinentOfJourney/ZeroPenny", (0, 5, 0, 0), DownedBigDipper) //零元硬币
            .AddModItem("ContinentOfJourney/TransactionCertificate", (0, 5, 0, 0), DownedBigDipper) //交易凭证
            .AddModItem("ContinentOfJourney/ShadowShackle", (0, 5, 0, 0), DownedBigDipper) //影脚镣
            .AddModItem("ContinentOfJourney/ShadeInABottle", (0, 5, 0, 0), DownedBigDipper) //瓶中影
            .AddModItem("ContinentOfJourney/PersonInHeart", (0, 5, 0, 0), DownedBigDipper); //心上人

            #endregion
            
            */

            #endregion

        }

        public void AddHardAccShop(NPCShop HardAcc)
        {
            #region 【配饰(肉后)】

            #region [原版]

            #region 机动性相关
            //忍者大师装备
            HardAcc.AddItem(ItemID.BlackBelt, (0, 10, 0, 0), Condition.DownedPlantera) //黑腰带
            .AddItem(ItemID.Tabi, (0, 10, 0, 0), Condition.DownedPlantera); //分趾厚底袜
            //其他类
            HardAcc.AddItem(ItemID.DiscountCard, (0, 6, 0, 0), Condition.DownedPirates) //优惠卡
            .AddItem(ItemID.GoldRing, (0, 6, 0, 0), Condition.DownedPirates) //金戒指
            .AddItem(ItemID.LuckyCoin, (0, 6, 0, 0), Condition.DownedPirates); //幸运币

            #endregion

            #region 战斗相关
            //伤害类
            HardAcc.AddItem(ItemID.YoYoGlove, (0, 5, 0, 0), Condition.Hardmode); //悠悠球手套
            //.AddItem(ItemID.MagicString, (0, 10, 0, 0), Condition.DownedMechBossAny); //魔法绳
            //射手
            HardAcc.AddItem(ItemID.MagicQuiver, (0, 5, 0, 0), Condition.Hardmode) //魔法箭袋
            .AddItem(ItemID.RifleScope, (0, 10, 0, 0), Condition.DownedPlantera); //步枪瞄准镜
            //召唤师
            HardAcc.AddItem(ItemID.PygmyNecklace, (0, 5, 0, 0), Condition.DownedQueenBee) //矮人项链
            .AddItem(ItemID.MonkBelt, (0, 5, 0, 0), DownedOgre) //武僧腰带
            .AddItem(ItemID.HuntressBuckler, (0, 5, 0, 0), DownedOgre) //女猎人圆盾
            .AddItem(ItemID.HerculesBeetle, (0, 10, 0, 0), Condition.DownedPlantera) //大力士甲虫
            .AddItem(ItemID.NecromanticScroll, (0, 10, 0, 0), PumpkinMoonHappened); //死灵卷轴
            //防御类
            HardAcc.AddItem(ItemID.FrozenTurtleShell, (0, 5, 0, 0), Condition.Hardmode) //冰冻海龟壳
            .AddItem(ItemID.PaladinsShield, (0, 10, 0, 0), Condition.DownedPlantera); //圣骑士护盾
            //宝箱怪掉落物
            HardAcc.AddItem(ItemID.TitanGlove, (0, 5, 0, 0), Condition.Hardmode) //泰坦手套
            .AddItem(ItemID.PhilosophersStone, (0, 5, 0, 0), Condition.Hardmode) //点金石
            .AddItem(ItemID.CrossNecklace, (0, 5, 0, 0), Condition.Hardmode) //十字项链
            .AddItem(ItemID.StarCloak, (0, 5, 0, 0), Condition.Hardmode) //星星斗篷
            .AddItem(ItemID.FleshKnuckles, (0, 5, 0, 0), Condition.Hardmode) //血肉指虎
            .AddItem(ItemID.PutridScent, (0, 5, 0, 0), Condition.Hardmode); //腐香囊
            //十字章护盾系列
            HardAcc.AddItem(ItemID.CobaltShield, (0, 4, 0, 0), Condition.DownedSkeletron) //钴护盾
            .AddItem(ItemID.ArmorPolish, (0, 5, 0, 0), Condition.Hardmode) //盔甲抛光剂
            .AddItem(ItemID.Vitamins, (0, 5, 0, 0), Condition.Hardmode) //维生素
            .AddItem(ItemID.TrifoldMap, (0, 5, 0, 0), Condition.Hardmode) //三折地图
            .AddItem(ItemID.FastClock, (0, 5, 0, 0), Condition.Hardmode) //快走时钟
            .AddItem(ItemID.Nazar, (0, 5, 0, 0), Condition.Hardmode) //邪眼
            .AddItem(ItemID.Megaphone, (0, 5, 0, 0), Condition.Hardmode) //扩音器
            .AddItem(ItemID.Blindfold, (0, 5, 0, 0), Condition.Hardmode) //蒙眼布
            .AddItem(ItemID.PocketMirror, (0, 5, 0, 0), Condition.Hardmode); //袖珍镜
            //天界壳系列
            HardAcc.AddItem(ItemID.MoonCharm, (0, 6, 0, 0), Condition.Hardmode) //月光护身符
            .AddItem(ItemID.NeptunesShell, (0, 8, 0, 0), EclipseHappened) //海神贝壳
            .AddItem(ItemID.MoonStone, (0, 8, 0, 0), EclipseHappened); //月亮石

            #endregion

            #endregion

            /*
            哼哼唧唧
            #region [旅人归途 Homeward Journey]
            //肉后
            accs.AddModItem("ContinentOfJourney/WindfallTicket", (0, 5, 0, 0), Condition.Hardmode) //横财彩票
            .AddModItem("ContinentOfJourney/Omega", (0, 5, 0, 0), Condition.Hardmode) //欧梅加
            .AddModItem("ContinentOfJourney/MapleMushroom", (0, 5, 0, 0), Condition.Hardmode) //枫糖浆蘑菇
            .AddModItem("ContinentOfJourney/LegacyTicket", (0, 5, 0, 0), Condition.Hardmode) //遗产彩票
            .AddModItem("ContinentOfJourney/Epsilon", (0, 10, 0, 0), Condition.DownedPlantera) //也普西隆
            .AddModItem("ContinentOfJourney/AbyssCore", (0, 10, 0, 0), Condition.DownedPlantera) //深渊核心
            .AddModItem("ContinentOfJourney/AffluenceTicket", (0, 10, 0, 0), Condition.DownedPlantera) //丰裕彩票
            .AddModItem("ContinentOfJourney/KippsApparatus", (0, 12, 0, 0), EclipseHappened, Condition.DownedPlantera) //启氏气体发生器
            .AddModItem("ContinentOfJourney/SwordmanBelt", (0, 12, 0, 0), PumpkinMoonHappened) //剑客腰带
            .AddModItem("ContinentOfJourney/BoxOfMystery", (0, 12, 0, 0), SnowMoonHappened) //神秘礼盒
            .AddModItem("ContinentOfJourney/WealthTicket", (0, 12, 0, 0), SnowMoonHappened) //财富彩票
            .AddModItem("ContinentOfJourney/JellyMushroom", (0, 12, 0, 0), SnowMoonHappened); //果冻蘑菇

            #endregion
            */
            
            #endregion

        }

        public void AddPostMoonAccShop(NPCShop PostMoonAcc)
        {
            #region 【配饰(月后)】

            /*
            哼哼唧唧
            #region [旅人归途 Homeward Journey]
            //暗影墙后
            accs.AddModItem("ContinentOfJourney/KingBill", (0, 30, 0, 0), DownedWallofShadow) //纸钞王
            .AddModItem("ContinentOfJourney/BatSignal", (0, 30, 0, 0), DownedWallofShadow) //蝙蝠标志
            .AddModItem("ContinentOfJourney/DangerousFlask", (0, 30, 0, 0), DownedWallofShadow) //危险的烧瓶
            .AddModItem("ContinentOfJourney/UnsuspiciousSkull", (0, 30, 0, 0), DownedWallofShadow) //不可疑的头骨
            .AddModItem("ContinentOfJourney/LazyShell", (0, 30, 0, 0), DownedWallofShadow) //惰怠之壳
            .AddModItem("ContinentOfJourney/TimeTurner", (0, 40, 0, 0), DownedTheOverwatcher) //时转仪

            .AddModItem("ContinentOfJourney/BuffBanner", (1, 0, 0, 0), DownedTheSon); //战旗

            #endregion
            */

            #endregion

        }
    }
}
