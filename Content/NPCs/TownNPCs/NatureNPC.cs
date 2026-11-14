using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.ItemDropRules;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Common.ModBossess;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;
using LaziestNPC.Content.Items.Consumables;
using LaziestNPC.Globals.Config;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.UI;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class NatureNPC : TownLNPC
    {
        protected override string BestiaryText => "Mods.LaziestNPC.Bestiary.NatureNPC";
        protected override string NPCNameText => "Name.NatureNPC";
        protected override int AttackProjType => ProjectileID.SeedlerNut;
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
            if (LNPCConfig.Instance.LNPCNatureNPC)
            {
                foreach (var player in Main.ActivePlayers)
                {
                    //任何在线的玩家的背包中存在【鲈鱼】物品时，NPC生成
                    if (player.inventory.Any(item => item.type == ItemID.Bass))
                        return true;
                }
            }
            return false;
        }

        public override string GetChat()
        {
            if (Main.rand.Next(100) < 70)
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                chat.Add(this.GetLocalizedValue("Chat.Normal1"));

                chat.Add(this.GetGenderValue("Chat.Normal2"));
                chat.Add(this.GetLocalizedValue("Chat.Normal3"));
                chat.Add(this.GetGenderValue("Chat.Normal4"));

                chat.Add(this.GetLocalizedValue("Chat.Normal5"));

                chat.Add(this.GetLocalizedValue("Chat.Normal6"), 0.1);
                chat.Add(this.GetLocalizedValue("Chat.Normal7"), 0.1);

                return chat.Get();

            }
            else
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                if (chat.elements.Count == 0) chat.Add(this.GetLocalizedValue("Chat.Normal1"));

                return chat.Get();
            }
        }

        private static readonly ShopCategory[] ShopCategoryList = new ShopCategory[]
        {
            new ShopCategory(
                "CritterShop",
                "Mods.LaziestNPC.NPCs.NatureNPC.ShopCategory.CritterShop",
                "LaziestNPC/Assets/ShopIcons/NatureNPC/CritterShop", 4.5f),

            new ShopCategory(
                "PlantShop",
                "Mods.LaziestNPC.NPCs.NatureNPC.ShopCategory.PlantShop",
                "LaziestNPC/Assets/ShopIcons/NatureNPC/PlantShop", 3f),

            new ShopCategory(
                "FishShop",
                "Mods.LaziestNPC.NPCs.NatureNPC.ShopCategory.FishShop",
                "LaziestNPC/Assets/ShopIcons/NatureNPC/FishShop", 1.5f),

            new ShopCategory(
                "CrateShop",
                "Mods.LaziestNPC.NPCs.NatureNPC.ShopCategory.CrateShop",
                "LaziestNPC/Assets/ShopIcons/NatureNPC/CrateShop", 2f),

            new ShopCategory(
                "AnglerReward",
                "Mods.LaziestNPC.NPCs.NatureNPC.ShopCategory.AnglerReward",
                "LaziestNPC/Assets/ShopIcons/NatureNPC/AnglerReward", 3f),
        };

        public override IReadOnlyList<ShopCategory> ShopCategories => ShopCategoryList;

        public override void SetChatButtons(ref string button, ref string button2)
        {
            ShopCategory category = CurrentShopCategory;
            //按钮1为当前类别的本地化名称
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
            var CritterShop = new NPCShop(Type, "CritterShop");
            var PlantShop = new NPCShop(Type, "PlantShop");
            var FishShop = new NPCShop(Type, "FishShop");
            var CrateShop = new NPCShop(Type, "CrateShop");
            var AnglerReward = new NPCShop(Type, "AnglerReward");

            AddCritterShop(CritterShop);
            AddPlantShop(PlantShop);
            AddFishShop(FishShop);
            AddCrateShop(CrateShop);
            AddAnglerRewardShop(AnglerReward);

            CritterShop.Register();
            PlantShop.Register();
            FishShop.Register();
            CrateShop.Register();
            AnglerReward.Register();
        }

        public void AddCritterShop(NPCShop CritterShop)
        {
            #region 【小动物】

            #region [原版]
            CritterShop.AddItem(ItemType<SunlightNet>(), (1, 0, 0, 0)) //阳光捕虫网
            .AddItem(ItemID.TruffleWorm, (0, 10, 0, 0), Condition.Hardmode) //松露虫
            .AddItem(ItemID.EmpressButterfly, (0, 25, 0, 0), Condition.DownedPlantera); //七彩草蛉
            //森林 ↓
            CritterShop.AddItem(ItemID.Worm, (0, 0, 5, 0)) //蠕虫
            .AddItem(ItemID.LadyBug, (0, 0, 5, 0)) //瓢虫
            .AddItem(ItemID.Bird, (0, 0, 5, 0)) //鸟
            .AddItem(ItemID.BlueJay, (0, 0, 5, 0)) //冠蓝鸦
            .AddItem(ItemID.Cardinal, (0, 0, 5, 0)) //红雀
            .AddItem(ItemID.Bunny, (0, 0, 5, 0)) //兔兔
            .AddItem(ItemID.Squirrel, (0, 0, 5, 0)) //松鼠
            .AddItem(ItemID.SquirrelRed, (0, 0, 5, 0)) //红松鼠
            .AddItem(ItemID.Grasshopper, (0, 0, 5, 0)) //蚱蜢
            .AddItem(ItemID.Goldfish, (0, 0, 5, 0)) //金鱼
            .AddItem(ItemID.Turtle, (0, 0, 5, 0)) //龟
            .AddItem(ItemID.Duck, (0, 0, 5, 0)) //鸭
            .AddItem(ItemID.MallardDuck, (0, 0, 5, 0)) //野鸭
            .AddItem(ItemID.WaterStrider, (0, 0, 5, 0)) //水黾
            .AddItem(ItemID.Stinkbug, (0, 0, 5, 0)); //臭虫
            //森林夜晚 ↓
            CritterShop.AddItem(ItemID.Firefly, (0, 0, 5, 0)) //萤火虫
            .AddItem(ItemID.Owl, (0, 0, 5, 0)) //猫头鹰
            .AddItem(ItemID.FairyCritterBlue, (0, 0, 5, 0)) //蓝仙灵
            .AddItem(ItemID.FairyCritterGreen, (0, 0, 5, 0)) //绿仙灵
            .AddItem(ItemID.FairyCritterPink, (0, 0, 5, 0)); //粉仙灵
            //丛林 ↓
            CritterShop.AddItem(ItemID.Grubby, (0, 0, 5, 0)) //蛆虫
            .AddItem(ItemID.Sluggy, (0, 0, 5, 0)) //鼻涕虫
            .AddItem(ItemID.Buggy, (0, 0, 5, 0)) //蚜虫
            .AddItem(ItemID.Frog, (0, 0, 5, 0)) //青蛙
            .AddItem(ItemID.TurtleJungle, (0, 0, 5, 0)) //丛林龟
            .AddItem(ItemID.YellowCockatiel, (0, 0, 5, 0)) //黄玄凤鹦鹉
            .AddItem(ItemID.GrayCockatiel, (0, 0, 5, 0)) //灰玄凤鹦鹉
            .AddItem(ItemID.ScarletMacaw, (0, 0, 5, 0)) //绯红金刚鹦鹉
            .AddItem(ItemID.BlueMacaw, (0, 0, 5, 0)) //蓝金刚鹦鹉
            .AddItem(ItemID.Toucan, (0, 0, 5, 0)); //巨嘴鸟
            //雪原 ↓
            CritterShop.AddItem(ItemID.Penguin, (0, 0, 5, 0)); //企鹅
            //沙漠 ↓
            CritterShop.AddItem(ItemID.Scorpion, (0, 0, 5, 0)) //蝎子
            .AddItem(ItemID.BlackScorpion, (0, 0, 5, 0)) //黑蝎子
            .AddItem(ItemID.Grebe, (0, 0, 5, 0)) //䴙䴘
            .AddItem(ItemID.Pupfish, (0, 0, 5, 0)); //鳉鱼
            //地下和洞穴 ↓
            CritterShop.AddItem(ItemID.Snail, (0, 0, 5, 0)) //蜗牛
            .AddItem(ItemID.Mouse, (0, 0, 5, 0)); //老鼠
            //发光蘑菇 ↓
            CritterShop.AddItem(ItemID.GlowingSnail, (0, 0, 5, 0)); //发光蜗牛
            //海洋 ↓
            CritterShop.AddItem(ItemID.Seagull, (0, 0, 5, 0)) //海鸥
            .AddItem(ItemID.Seahorse, (0, 0, 5, 0)); //海马
            //.AddItem(ItemID.Pufferfish, (0, 0, 5, 0)); //河豚
            //地狱 ↓
            CritterShop.AddItem(ItemID.Lavafly, (0, 0, 5, 0)) //熔岩萤火虫
            .AddItem(ItemID.MagmaSnail, (0, 0, 5, 0)); //岩浆蜗牛
            //墓地 ↓
            CritterShop.AddItem(ItemID.Rat, (0, 0, 5, 0)) //大鼠
            .AddItem(ItemID.Maggot, (0, 0, 5, 0)); //蝇蛆
            //微光 ↓
            CritterShop.AddItem(ItemID.Shimmerfly, (0, 0, 5, 0)); //飞灵
            //神圣之地 ↓
            CritterShop.AddItem(ItemID.LightningBug, (0, 0, 5, 0), Condition.Hardmode); //荧光虫
            //蝴蝶 ↓
            CritterShop.AddItem(ItemID.MonarchButterfly, (0, 0, 5, 0)) //帝王蝶
            .AddItem(ItemID.SulphurButterfly, (0, 0, 5, 0)) //黄粉蝶
            .AddItem(ItemID.JuliaButterfly, (0, 0, 5, 0)) //珠袖蝶
            .AddItem(ItemID.UlyssesButterfly, (0, 0, 5, 0)) //翠凤蝶
            .AddItem(ItemID.ZebraSwallowtailButterfly, (0, 0, 5, 0)) //带凤蝶
            .AddItem(ItemID.PurpleEmperorButterfly, (0, 0, 5, 0)) //紫蛱蝶
            .AddItem(ItemID.RedAdmiralButterfly, (0, 0, 5, 0)) //红蛱蝶
            .AddItem(ItemID.TreeNymphButterfly, (0, 0, 5, 0)) //帛斑蝶
            .AddItem(ItemID.HellButterfly, (0, 0, 5, 0)); //地狱蝴蝶
            //蜻蜓 ↓
            CritterShop.AddItem(ItemID.RedDragonfly, (0, 0, 5, 0)) //红蜻蜓
            .AddItem(ItemID.BlueDragonfly, (0, 0, 5, 0)) //蓝蜻蜓
            .AddItem(ItemID.GreenDragonfly, (0, 0, 5, 0)) //绿蜻蜓
            .AddItem(ItemID.YellowDragonfly, (0, 0, 5, 0)) //黄蜻蜓
            .AddItem(ItemID.BlackDragonfly, (0, 0, 5, 0)) //黑蜻蜓
            .AddItem(ItemID.OrangeDragonfly, (0, 0, 5, 0)); //橙蜻蜓

            #endregion

            #region [灾厄 Calamity Mod]
            CritterShop.AddModItem("CalamityMod/ShroombleItem", (0, 0, 5, 0)) //茸宝
            .AddModItem("CalamityMod/PiggyItem", (0, 0, 5, 0)) //小猪猪
            .AddModItem("CalamityMod/BabyGhostBellItem", (0, 0, 10, 0)) //小鬼铃水母
            .AddModItem("CalamityMod/BabyCannonballJellyfishItem", (0, 0, 10, 0)) //小炮弹水母
            .AddModItem("CalamityMod/SeaMinnowItem", (0, 0, 10, 0)) //海洋米诺鱼
            .AddModItem("CalamityMod/BabyFlakCrabItem", (0, 0, 50, 0), Condition.Hardmode) //小高口蟹
            .AddModItem("CalamityMod/TwinklerItem", (0, 0, 50, 0), Condition.Hardmode); //星幻萤火虫

            #endregion

            #region [瑟银 Thorium Mod]
            CritterShop.AddModItem("ThoriumMod/MarineKelp", (0, 0, 1, 0)) //海藻
            .AddModItem("ThoriumMod/OpalBunny", (0, 0, 5, 0)) //欧珀兔兔
            .AddModItem("ThoriumMod/AquamarineBunny", (0, 0, 5, 0)) //海蓝宝石兔兔
            .AddModItem("ThoriumMod/Crow", (0, 0, 5, 0)) //乌鸦
            .AddModItem("ThoriumMod/DumboOctopus", (0, 0, 5, 0)) //小飞象章鱼
            .AddModItem("ThoriumMod/PurpleDumboOctopus", (0, 0, 5, 0)) //紫小飞象章鱼
            .AddModItem("ThoriumMod/Lobster", (0, 0, 5, 0)) //龙虾
            .AddModItem("ThoriumMod/BlueLobster", (0, 0, 5, 0)); //蓝龙虾

            #endregion

            #endregion

        }

        public void AddPlantShop(NPCShop PlantShop)
        {
            #region 【植物类】

            #region [原版]
            PlantShop.AddItem(ItemID.HerbBag, (0, 0, 25, 0)) //草药袋
            .AddItem(ItemID.Mushroom, (0, 0, 1, 0)) //蘑菇
            .AddItem(ItemID.GlowingMushroom, (0, 0, 1, 50)) //发光蘑菇
            .AddItem(ItemID.ViciousMushroom, (0, 0, 3, 0)) //毒蘑菇
            .AddItem(ItemID.VileMushroom, (0, 0, 3, 0)); //魔菇
            //草种子 ↓
            PlantShop.AddItem(ItemID.GrassSeeds, (0, 0, 0, 10)) //草种子
            .AddItem(ItemID.JungleGrassSeeds, (0, 0, 0, 25)) //丛林草种子
            .AddItem(ItemID.MushroomGrassSeeds, (0, 0, 0, 25)) //蘑菇草种子
            .AddItem(ItemID.CrimsonSeeds, (0, 0, 0, 50)) //猩红种子
            .AddItem(ItemID.CorruptSeeds, (0, 0, 0, 50)) //腐化种子
            .AddItem(ItemID.AshGrassSeeds, (0, 0, 0, 50)) //灰烬草种子
            .AddItem(ItemID.HallowedSeeds, (0, 0, 1, 0), Condition.Hardmode) //神圣种子
            .AddItem(ItemID.Acorn, (0, 0, 0, 10)); //橡实

            //染料 ↓
            PlantShop.AddItem(ItemID.RedHusk, (0, 1, 50, 0)) //红外壳
            .AddItem(ItemID.OrangeBloodroot, (0, 1, 50, 0)) //橙血根草
            .AddItem(ItemID.YellowMarigold, (0, 1, 50, 0)) //黄万寿菊
            .AddItem(ItemID.LimeKelp, (0, 1, 50, 0)) //橙绿海藻
            .AddItem(ItemID.GreenMushroom, (0, 1, 50, 0)) //绿蘑菇
            .AddItem(ItemID.TealMushroom, (0, 1, 50, 0)) //青绿蘑菇
            .AddItem(ItemID.CyanHusk, (0, 1, 50, 0)) //青外壳
            .AddItem(ItemID.SkyBlueFlower, (0, 1, 50, 0)) //天蓝花朵
            .AddItem(ItemID.BlueBerries, (0, 1, 50, 0)) //蓝浆果
            .AddItem(ItemID.PurpleMucos, (0, 1, 50, 0)) //紫粘液
            .AddItem(ItemID.VioletHusk, (0, 1, 50, 0)) //蓝紫外壳
            .AddItem(ItemID.PinkPricklyPear, (0, 1, 50, 0)) //粉仙人掌果
            .AddItem(ItemID.BlackInk, (0, 1, 50, 0)); //黑墨水

            PlantShop.AddItem(ItemID.JungleRose, (0, 2, 0, 0)); //丛林玫瑰

            #endregion

            #region [瑟银 Thorium Mod]
            PlantShop.AddModItem("ThoriumMod/MarineKelp", (0, 0, 1, 0)); //海藻

            #endregion

            #endregion

        }

        public void AddFishShop(NPCShop FishShop)
        {
            #region 【渔获物品】

            #region [原版]
            FishShop.AddItem(ItemType<CallingSeaBottle>(), (1, 0, 0, 0)); //唤海瓶
            //鱼饵 ↓
            FishShop.AddItem(ItemID.CanOfWorms, (0, 1, 50, 0)) //蠕虫罐头
            .AddItem(ItemID.ApprenticeBait, (0, 0, 5, 0)) //学徒鱼饵
            .AddItem(ItemID.JourneymanBait, (0, 0, 10, 0)) //熟手诱饵
            .AddItem(ItemID.MasterBait, (0, 0, 50, 0)); //大师鱼饵
            //其他水生生物 ↓
            FishShop.AddItem(ItemID.BlueJellyfish, (0, 0, 75, 0)) //蓝水母
            .AddItem(ItemID.PinkJellyfish, (0, 0, 75, 0)) //粉水母
            .AddItem(ItemID.GreenJellyfish, (0, 1, 0, 0), Condition.Hardmode) //绿水母
            .AddItem(ItemID.ChumBucket, (0, 1, 0, 0), BloodMoonHappened); //鱼饵桶(血月)

            //鱼-森林 ↓
            FishShop.AddItem(ItemID.Bass, (0, 0, 25, 0)) //鲈鱼
            .AddItem(ItemID.Salmon, (0, 0, 25, 0)); //三文鱼
            //鱼-洞穴 ↓
            FishShop.AddItem(ItemID.ArmoredCavefish, (0, 0, 25, 0)) //装甲洞穴鱼
            .AddItem(ItemID.SpecularFish, (0, 0, 25, 0)) //镜面鱼
            .AddItem(ItemID.Stinkfish, (0, 0, 25, 0)) //臭味鱼
            .AddItem(ItemID.BombFish, (0, 0, 50, 0)); //炸弹鱼
            //鱼-沙漠 ↓
            FishShop.AddItem(ItemID.Oyster, (0, 0, 50, 0)) //牡蛎
            .AddItem(ItemID.Flounder, (0, 0, 25, 0)) //偏口鱼
            .AddItem(ItemID.RockLobster, (0, 0, 25, 0)); //岩石龙虾
            //鱼-雪原 ↓
            FishShop.AddItem(ItemID.AtlanticCod, (0, 0, 25, 0)) //大西洋鳕鱼
            .AddItem(ItemID.FrostMinnow, (0, 0, 25, 0)) //寒霜鲦鱼
            .AddItem(ItemID.FrostDaggerfish, (0, 0, 25, 0)); //寒霜飞鱼
            //鱼-太空 ↓
            FishShop.AddItem(ItemID.Damselfish, (0, 0, 25, 0)); //雀鲷
            //鱼-海洋 ↓
            FishShop.AddItem(ItemID.Trout, (0, 0, 25, 0)) //鳟鱼
            .AddItem(ItemID.Tuna, (0, 0, 25, 0)) //金枪鱼
            .AddItem(ItemID.RedSnapper, (0, 0, 25, 0)) //红鲷鱼
            .AddItem(ItemID.Shrimp, (0, 0, 25, 0)); //虾
            //鱼-丛林 ↓
            FishShop.AddItem(ItemID.NeonTetra, (0, 0, 50, 0)) //霓虹脂鲤
            .AddItem(ItemID.VariegatedLardfish, (0, 0, 50, 0)) //斑驳油鱼
            .AddItem(ItemID.DoubleCod, (0, 0, 50, 0)) //双鳍鳕鱼
            .AddItem(ItemID.Honeyfin, (0, 1, 50, 0)); //蜂蜜鱼
            //鱼-猩红之地 ↓
            FishShop.AddItem(ItemID.CrimsonTigerfish, (0, 0, 50, 0)) //猩红虎鱼
            .AddItem(ItemID.Hemopiranha, (0, 0, 50, 0)); //血腥食人鱼
            //鱼-腐化之地 ↓
            FishShop.AddItem(ItemID.Ebonkoi, (0, 0, 50, 0)); //黑檀锦鲤
            //鱼-熔岩 ↓
            FishShop.AddItem(ItemID.Obsidifish, (0, 0, 75, 0)) //黑曜石鱼
            .AddItem(ItemID.FlarefinKoi, (0, 0, 75, 0)); //闪鳍锦鲤
            //鱼-神圣之地(肉后) ↓
            FishShop.AddItem(ItemID.PrincessFish, (0, 1, 0, 0), Condition.Hardmode) //公主鱼
            .AddItem(ItemID.Prismite, (0, 1, 0, 0), Condition.Hardmode) //七彩矿鱼
            .AddItem(ItemID.ChaosFish, (0, 1, 0, 0), Condition.Hardmode); //混沌鱼
            //渔获-其他
            FishShop.AddItem(ItemID.GoldenCarp, (0, 50, 0, 0)) //金鲤鱼
            .AddItem(ItemID.OldShoe, (0, 0, 0, 1)) //旧鞋
            .AddItem(ItemID.FishingSeaweed, (0, 0, 0, 1)) //海草
            .AddItem(ItemID.TinCan, (0, 0, 0, 1)); //锡罐

            #endregion

            #region [灾厄 Calamity Mod]
            //鱼饵
            FishShop.AddModItem("CalamityMod/GrandMarquisBait", (0, 1, 0, 0)) //尊爵鱼饵
            .AddModItem("CalamityMod/ArcturusAstroidean", (0, 0, 20, 0), Condition.Hardmode) //大角海星
            .AddModItem("CalamityMod/EnchantedStarfish", (0, 1, 50, 0)); //附魔海星
            //鱼-沉沦之海 ↓
            FishShop.AddModItem("CalamityMod/PrismaticGuppy", (0, 0, 10, 0)) //棱镜孔雀鱼
            .AddModItem("CalamityMod/SunkenSailfish", (0, 0, 10, 0)) //沉沦帆鱼
            .AddModItem("CalamityMod/GreenwaveLoach", (0, 5, 0, 0)); //绿波泥鳅
            //鱼-硫火之崖 ↓
            FishShop.AddModItem("CalamityMod/Shadowfish", (0, 0, 10, 0)) //暗影鱼
            .AddModItem("CalamityMod/CoastalDemonfish", (0, 0, 10, 0)) //海岸恶魔鱼
            .AddModItem("CalamityMod/CharredLasher", (0, 10, 0, 0)); //焦黑鞭笞者
            //鱼-星辉瘟疫 ↓
            FishShop.AddModItem("CalamityMod/TwinklingPollox", (0, 0, 50, 0), Condition.Hardmode) //北河三烁光鱼
            .AddModItem("CalamityMod/ProcyonidPrawn", (0, 0, 50, 0), Condition.Hardmode) //南河三明虾
            .AddModItem("CalamityMod/AldebaranAlewife", (0, 0, 50, 0), Condition.Hardmode); //毕宿五灰西鲱
            //鱼-摸彩袋 ↓
            FishShop.AddModItem("CalamityMod/StuffedFish", (0, 1, 50, 0)); //填充草药鱼

            #endregion

            #region [瑟银 Thorium Mod]
            FishShop.AddModItem("ThoriumMod/MagmaGill", (0, 0, 10, 0)) //熔岩焰鱼
            .AddModItem("ThoriumMod/FlamingCrackGut", (0, 0, 10, 0)); //燃火裂肠鱼

            #endregion

            #region [旅人归途 Homeward Journey]
            FishShop.AddModItem("ContinentOfJourney/CrispyButterfly", (0, 0, 15, 0), Condition.DownedMechBossAny); //酥脆蝴蝶

            #endregion

            #endregion

        }

        public void AddCrateShop(NPCShop CrateShop)
        {
            #region 【宝匣商店】

            #region [原版]
            //肉前
            CrateShop.AddItem(ItemType<DreamSeaCrateBook>(), (1, 0, 0, 0)) //梦海宝匣大全
            .AddItem(ItemID.WoodenCrate, (0, 1, 0, 0)) //木匣
            .AddItem(ItemID.IronCrate, (0, 2, 0, 0)) //铁匣
            .AddItem(ItemID.GoldenCrate, (0, 3, 0, 0)) //金匣
            .AddItem(ItemID.OasisCrate, (0, 3, 0, 0)) //绿洲匣
            .AddItem(ItemID.FrozenCrate, (0, 3, 0, 0)) //冰冻匣
            .AddItem(ItemID.JungleFishingCrate, (0, 3, 0, 0)) //丛林匣
            .AddItem(ItemID.OceanCrate, (0, 3, 0, 0)) //海洋匣
            .AddItem(ItemID.FloatingIslandFishingCrate, (0, 3, 0, 0)) //天空匣
            .AddItem(ItemID.CrimsonFishingCrate, (0, 3, 0, 0)) //猩红匣
            .AddItem(ItemID.CorruptFishingCrate, (0, 3, 0, 0)) //腐化匣
            .AddItem(ItemID.HallowedFishingCrate, (0, 3, 0, 0)) //神圣匣
            .AddItem(ItemID.LavaCrate, (0, 3, 0, 0)) //黑曜石匣
            .AddItem(ItemID.DungeonFishingCrate, (0, 4, 0, 0), Condition.DownedSkeletron); //地牢匣
            //肉后
            CrateShop.AddItem(ItemID.WoodenCrateHard, (0, 5, 0, 0), Condition.Hardmode) //珍珠木匣
            .AddItem(ItemID.IronCrateHard, (0, 5, 0, 0), Condition.Hardmode) //秘银匣
            .AddItem(ItemID.GoldenCrateHard, (0, 5, 0, 0), Condition.Hardmode) //钛金匣
            .AddItem(ItemID.OasisCrateHard, (0, 5, 0, 0), Condition.Hardmode) //幻象匣
            .AddItem(ItemID.FrozenCrateHard, (0, 5, 0, 0), Condition.Hardmode) //针叶木匣
            .AddItem(ItemID.JungleFishingCrateHard, (0, 5, 0, 0), Condition.Hardmode) //荆棘匣
            .AddItem(ItemID.OceanCrateHard, (0, 5, 0, 0), Condition.Hardmode) //海边匣
            .AddItem(ItemID.FloatingIslandFishingCrateHard, (0, 5, 0, 0), Condition.Hardmode) //天蓝匣
            .AddItem(ItemID.CrimsonFishingCrateHard, (0, 5, 0, 0), Condition.Hardmode) //血匣
            .AddItem(ItemID.CorruptFishingCrateHard, (0, 5, 0, 0), Condition.Hardmode) //污损匣
            .AddItem(ItemID.HallowedFishingCrateHard, (0, 5, 0, 0), Condition.Hardmode) //天赐匣
            .AddItem(ItemID.LavaCrateHard, (0, 5, 0, 0), Condition.Hardmode) //狱石匣
            .AddItem(ItemID.DungeonFishingCrateHard, (0, 5, 0, 0), Condition.Hardmode); //围栏匣

            #endregion

            #region [灾厄 Calamity Mod]
            //肉前
            CrateShop.AddModItem("CalamityMod/SulphurousCrate", (0, 3, 0, 0)) //硫海匣
            .AddModItem("CalamityMod/MonolithCrate", (0, 3, 0, 0)) //星石匣
            .AddModItem("CalamityMod/SlagCrate", (0, 3, 0, 0)) //焦炭匣
            .AddModItem("CalamityMod/EutrophicCrate", (0, 3, 0, 0)); //富养匣
            //肉后
            CrateShop.AddModItem("CalamityMod/HydrothermalCrate", (0, 5, 0, 0), Condition.Hardmode) //渊泉匣
            .AddModItem("CalamityMod/AstralCrate", (0, 5, 0, 0), Condition.Hardmode) //星幻匣
            .AddModItem("CalamityMod/BrimstoneCrate", (0, 5, 0, 0), Condition.Hardmode) //硫磺火匣
            .AddModItem("CalamityMod/PrismCrate", (0, 5, 0, 0), Condition.Hardmode); //棱晶匣

            #endregion

            #region [瑟银 Thorium Mod]
            //肉前
            CrateShop.AddModItem("ThoriumMod/AquaticDepthsCrate", (0, 3, 0, 0), Condition.DownedEowOrBoc) //渊海匣
            .AddModItem("ThoriumMod/ScarletCrate", (0, 3, 0, 0)) //绯红匣
            .AddModItem("ThoriumMod/StrangeCrate", (0, 3, 0, 0)); //奇特匣
            //肉后
            CrateShop.AddModItem("ThoriumMod/AbyssalCrate", (0, 5, 0, 0), Condition.Hardmode) //深渊匣
            .AddModItem("ThoriumMod/SinisterCrate", (0, 5, 0, 0), Condition.Hardmode) //不详匣
            .AddModItem("ThoriumMod/WondrousCrate", (0, 5, 0, 0), Condition.Hardmode); //奇妙匣

            #endregion

            #region [旅人归途 Homeward Journey]
            //肉前
            CrateShop.AddModItem("ContinentOfJourney/MazeCrate", (0, 3, 0, 0), Condition.DownedEowOrBoc); //迷宫匣
            //肉后
            CrateShop.AddModItem("ContinentOfJourney/MistyCrate", (0, 5, 0, 0), Condition.Hardmode) //迷雾匣
            .AddModItem("ContinentOfJourney/DistortedCrate", (0, 7, 0, 0), Condition.DownedPlantera); //扭曲匣
            //暗影墙后
            CrateShop.AddModItem("ContinentOfJourney/QuakyCrate", (0, 10, 0, 0), DownedWallofShadow) //震颤匣
            .AddModItem("ContinentOfJourney/SolarCrate", (0, 10, 0, 0), DownedWallofShadow) //太阳匣
            .AddModItem("ContinentOfJourney/ForeverCrate", (0, 10, 0, 0), DownedWallofShadow) //永远匣
            .AddModItem("ContinentOfJourney/LivingCrate", (0, 10, 0, 0), DownedWallofShadow) //脉搏匣
            .AddModItem("ContinentOfJourney/CubistCrate", (0, 10, 0, 0), DownedWallofShadow); //立体匣
            CrateShop.AddModItem("ContinentOfJourney/ShinyCrate", (0, 15, 0, 0), DownedSunSlimeGod) //闪亮匣
            .AddModItem("ContinentOfJourney/CountdownCrate", (0, 15, 0, 0), DownedTheOverwatcher) //计时匣
            .AddModItem("ContinentOfJourney/MembraneCrate", (0, 15, 0, 0), DownedTheLifebringer) //薄膜匣
            .AddModItem("ContinentOfJourney/CubeCrate", (0, 15, 0, 0), DownedTheMaterealizer) //方块匣
            .AddModItem("ContinentOfJourney/ShadowCrate", (0, 20, 0, 0), DownedTheThree); //影子匣

            #endregion

            #endregion

        }

        public void AddAnglerRewardShop(NPCShop AnglerReward)
        {
            #region 【渔夫奖励】
            //主要奖励
            AnglerReward.AddItem(ItemID.AnglerHat, (0, 5, 0, 0)) //渔夫帽
            .AddItem(ItemID.AnglerVest, (0, 5, 0, 0)) //渔夫背心
            .AddItem(ItemID.AnglerPants, (0, 5, 0, 0)) //渔夫裤
            .AddItem(ItemID.FishingBobber, (0, 1, 0, 0)) //钓鱼浮标
            .AddItem(ItemID.HighTestFishingLine, (0, 5, 0, 0)) //优质钓鱼线
            .AddItem(ItemID.AnglerEarring, (0, 5, 0, 0)) //渔夫耳环
            .AddItem(ItemID.TackleBox, (0, 5, 0, 0)) //钓具箱
            .AddItem(ItemID.FishermansGuide, (0, 5, 0, 0)) //渔民袖珍宝典
            .AddItem(ItemID.WeatherRadio, (0, 5, 0, 0)) //天气收音机
            .AddItem(ItemID.Sextant, (0, 5, 0, 0)); //六分仪

            AnglerReward.AddItem(ItemID.FuzzyCarrot, (0, 5, 0, 0)) //绒毛胡萝卜
            .AddItem(ItemID.FishMinecart, (0, 5, 0, 0)) //鲤鱼矿车
            .AddItem(ItemID.FishHook, (0, 5, 0, 0)) //鱼钩
            .AddItem(ItemID.GoldenBugNet, (0, 50, 0, 0)) //金虫网
            .AddItem(ItemID.GoldenFishingRod, (0, 50, 0, 0)) //金钓竿
            .AddItem(ItemID.BottomlessBucket, (0, 25, 0, 0)) //无底水桶
            .AddItem(ItemID.SuperAbsorbantSponge, (0, 25, 0, 0)) //超级吸收绵
            .AddItem(ItemID.BottomlessHoneyBucket, (0, 25, 0, 0)) //无底蜂蜜桶
            .AddItem(ItemID.HoneyAbsorbantSponge, (0, 25, 0, 0)) //蜂蜜吸收绵
            .AddItem(ItemID.BottomlessLavaBucket, (0, 25, 0, 0)) //无底熔岩桶
            .AddItem(ItemID.LavaAbsorbantSponge, (0, 25, 0, 0)) //熔岩吸收绵
            .AddItem(ItemID.HotlineFishingHook, (0, 15, 0, 0), Condition.Hardmode) //熔线钓钩
            .AddItem(ItemID.FinWings, (0, 25, 0, 0), Condition.Hardmode); //鳍翼

            //装饰性奖励
            AnglerReward.AddItem(ItemID.SeashellHairpin, (0, 1, 0, 0)) //贝壳发夹
            .AddItem(ItemID.MermaidAdornment, (0, 1, 0, 0)) //美人鱼饰品
            .AddItem(ItemID.MermaidTail, (0, 1, 0, 0)) //美人鱼鱼尾裤
            .AddItem(ItemID.FishCostumeMask, (0, 1, 0, 0)) //鱼装面具
            .AddItem(ItemID.FishCostumeShirt, (0, 1, 0, 0)) //鱼装衣
            .AddItem(ItemID.FishCostumeFinskirt, (0, 1, 0, 0)) //鱼装鳍裙
            .AddItem(ItemID.BunnyfishTrophy, (0, 1, 0, 0)) //兔兔鱼纪念章
            .AddItem(ItemID.GoldfishTrophy, (0, 1, 0, 0)) //金鱼纪念章
            .AddItem(ItemID.SwordfishTrophy, (0, 1, 0, 0)) //剑鱼纪念章
            .AddItem(ItemID.SharkteethTrophy, (0, 1, 0, 0)) //鲨牙纪念章
            .AddItem(ItemID.TreasureMap, (0, 1, 0, 0)) //宝藏地图
            .AddItem(ItemID.SeaweedPlanter, (0, 1, 0, 0)) //海草花盆
            .AddItem(ItemID.CompassRose, (0, 1, 0, 0)) //罗盘针
            .AddItem(ItemID.ShipsWheel, (0, 1, 0, 0)) //舵轮
            .AddItem(ItemID.ShipInABottle, (0, 1, 0, 0)) //船舶瓶
            .AddItem(ItemID.LifePreserver, (0, 1, 0, 0)) //救生圈
            .AddItem(ItemID.WallAnchor, (0, 1, 0, 0)) //墙锚
            .AddItem(ItemID.PillaginMePixels, (0, 1, 0, 0)) //海贼像素画
            .AddItem(ItemID.NotSoLostInParadise, (0, 1, 0, 0)) //并未如此迷失在乐园
            .AddItem(ItemID.Crustography, (0, 1, 0, 0)) //甲壳摄影
            .AddItem(ItemID.WhatLurksBelow, (0, 1, 0, 0)) //下面潜伏着什么
            .AddItem(ItemID.Fangs, (0, 1, 0, 0)) //獠牙
            .AddItem(ItemID.CouchGag, (0, 1, 0, 0)) //沙发笑话
            .AddItem(ItemID.SilentFish, (0, 1, 0, 0)) //沉默的鱼
            .AddItem(ItemID.TheDuke, (0, 1, 0, 0)); //公爵

            #endregion

        }
    }
}
