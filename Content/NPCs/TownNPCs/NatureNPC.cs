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
using Terraria.GameContent.ItemDropRules;
using LaziestNPC.Content.Items.Consumables;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class NatureNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 27;
            NPCID.Sets.ExtraFramesCount[Type] = 20;
            NPCID.Sets.AttackFrameCount[Type] = 0;
            NPCID.Sets.DangerDetectRange[Type] = 220;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
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
            AnimationType = NPCID.TownBunny;
            //AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.NatureNPC")
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }
                //任何在线的玩家的背包中存在【鲈鱼】物品时，NPC生成
                if (player.inventory.Any(item => item.type == ItemID.Bass))
                {
                    return true;
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                this.GetLocalizedValue("Name.NatureNPC")
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> dialogue = new WeightedRandom<string>();
            dialogue.Add(this.GetLocalizedValue("Chat.Normal1"));
            dialogue.Add(this.GetLocalizedValue("Chat.Normal2"));
            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.LaziestNPC.NPCs.NatureNPC.button1");
            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.NatureNPC.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "PlantsCritters";
            }
            else
            {
                shopName = "Fishery";
            }
        }

        public override void AddShops()
        {
            var plcr = new NPCShop(Type, "PlantsCritters");
            var fishery = new NPCShop(Type, "Fishery");

            #region 【动植物】

            #region [原版]
            plcr.AddItem(ItemID.Mushroom, (0, 0, 0, 10)) //蘑菇
            .AddItem(ItemID.GlowingMushroom, (0, 0, 0, 25)) //发光蘑菇
            .AddItem(ItemID.ViciousMushroom, (0, 0, 0, 25)) //猩红蘑菇
            .AddItem(ItemID.VileMushroom, (0, 0, 0, 25)) //腐化蘑菇
            //草种子 ↓
            .AddItem(ItemID.GrassSeeds, (0, 0, 0, 10)) //草种子
            .AddItem(ItemID.JungleGrassSeeds, (0, 0, 0, 25)) //丛林草种子
            .AddItem(ItemID.MushroomGrassSeeds, (0, 0, 0, 25)) //蘑菇草种子
            .AddItem(ItemID.CrimsonSeeds, (0, 0, 0, 50)) //猩红种子
            .AddItem(ItemID.CorruptSeeds, (0, 0, 0, 50)) //腐化种子
            .AddItem(ItemID.AshGrassSeeds, (0, 0, 0, 50)) //灰烬草种子
            .AddItem(ItemID.HallowedSeeds, (0, 0, 1, 0), Condition.Hardmode) //神圣种子
            //草药和草药种子 ↓
            .AddItem(ItemID.Acorn, (0, 0, 0, 10)) //橡实
            .AddItem(ItemID.Daybloom, (0, 0, 0, 50)) //太阳花
            .AddItem(ItemID.DaybloomSeeds, (0, 0, 0, 5)) //太阳花种子
            .AddItem(ItemID.Blinkroot, (0, 0, 0, 50)) //闪耀根
            .AddItem(ItemID.BlinkrootSeeds, (0, 0, 0, 5)) //闪耀根种子
            .AddItem(ItemID.Moonglow, (0, 0, 0, 50)) //月光草
            .AddItem(ItemID.MoonglowSeeds, (0, 0, 0, 5)) //月光草种子
            .AddItem(ItemID.Waterleaf, (0, 0, 0, 50)) //水叶
            .AddItem(ItemID.WaterleafSeeds, (0, 0, 0, 5)) //水叶种子
            .AddItem(ItemID.Shiverthorn, (0, 0, 0, 50)) //寒颤棘
            .AddItem(ItemID.ShiverthornSeeds, (0, 0, 0, 5)) //寒颤棘种子
            .AddItem(ItemID.Deathweed, (0, 0, 0, 50)) //死亡草
            .AddItem(ItemID.DeathweedSeeds, (0, 0, 0, 5)) //死亡草种子
            .AddItem(ItemID.Fireblossom, (0, 0, 0, 50)) //火焰花
            .AddItem(ItemID.FireblossomSeeds, (0, 0, 0, 5)) //火焰花种子
            //其他植物和种子 ↓
            .AddItem(ItemID.Pumpkin, (0, 0, 0, 50)) //南瓜
            .AddItem(ItemID.PumpkinSeed, (0, 0, 0, 5)) //南瓜种子
            //森林 ↓
            .AddItem(ItemID.LadyBug, (0, 0, 0, 0)) //瓢虫
            .AddItem(ItemID.Bird, (0, 0, 0, 0)) //鸟
            .AddItem(ItemID.BlueJay, (0, 0, 0, 0)) //蓝松鸦
            .AddItem(ItemID.Cardinal, (0, 0, 0, 0)) //红雀
            .AddItem(ItemID.Bunny, (0, 0, 0, 0)) //兔子
            .AddItem(ItemID.Squirrel, (0, 0, 0, 0)) //松鼠
            .AddItem(ItemID.SquirrelRed, (0, 0, 0, 0)) //红松鼠
            .AddItem(ItemID.Worm, (0, 0, 0, 0)) //蠕虫
            .AddItem(ItemID.TruffleWorm, (0, 0, 0, 0), Condition.Hardmode) //松露虫
            .AddItem(ItemID.Grasshopper, (0, 0, 0, 0)) //蚱蜢
            .AddItem(ItemID.Goldfish, (0, 0, 0, 0)) //金鱼
            .AddItem(ItemID.Turtle, (0, 0, 0, 0)) //乌龟
            .AddItem(ItemID.Duck, (0, 0, 0, 0)) //鸭子
            .AddItem(ItemID.MallardDuck, (0, 0, 0, 0)) //绿头鸭
            .AddItem(ItemID.WaterStrider, (0, 0, 0, 0)) //水黾
            .AddItem(ItemID.Stinkbug, (0, 0, 0, 0)) //臭虫
            //森林夜晚 ↓
            .AddItem(ItemID.Firefly, (0, 0, 0, 0)) //萤火虫
            .AddItem(ItemID.Owl, (0, 0, 0, 0)) //猫头鹰
            .AddItem(ItemID.FairyCritterBlue, (0, 0, 0, 0)) //蓝仙灵
            .AddItem(ItemID.FairyCritterGreen, (0, 0, 0, 0)) //绿仙灵
            .AddItem(ItemID.FairyCritterPink, (0, 0, 0, 0)) //粉仙灵
            //丛林 ↓
            .AddItem(ItemID.Grubby, (0, 0, 0, 0)) //丛林蛆
            .AddItem(ItemID.Sluggy, (0, 0, 0, 0)) //丛林鼻涕虫
            .AddItem(ItemID.Buggy, (0, 0, 0, 0)) //丛林甲虫
            .AddItem(ItemID.Frog, (0, 0, 0, 0)) //青蛙
            .AddItem(ItemID.TurtleJungle, (0, 0, 0, 0)) //丛林龟
            .AddItem(ItemID.YellowCockatiel, (0, 0, 0, 0)) //黄凤头鹦鹉
            .AddItem(ItemID.GrayCockatiel, (0, 0, 0, 0)) //灰凤头鹦鹉
            .AddItem(ItemID.ScarletMacaw, (0, 0, 0, 0)) //绯红金刚鹦鹉
            .AddItem(ItemID.BlueMacaw, (0, 0, 0, 0)) //蓝金刚鹦鹉
            .AddItem(ItemID.Toucan, (0, 0, 0, 0)) //巨嘴鸟
            //雪原 ↓
            .AddItem(ItemID.Penguin, (0, 0, 0, 0)) //企鹅
            //沙漠 ↓
            .AddItem(ItemID.Scorpion, (0, 0, 0, 0)) //蝎子
            .AddItem(ItemID.BlackScorpion, (0, 0, 0, 0)) //黑蝎子
            .AddItem(ItemID.Grebe, (0, 0, 0, 0)) //䴙䴘
            .AddItem(ItemID.Pupfish, (0, 0, 0, 0)) //鳉鱼
            //地下和洞穴 ↓
            .AddItem(ItemID.Snail, (0, 0, 0, 0)) //蜗牛
            .AddItem(ItemID.Mouse, (0, 0, 0, 0)) //老鼠
            //发光蘑菇 ↓
            .AddItem(ItemID.GlowingSnail, (0, 0, 0, 0)) //发光蜗牛
            //海洋 ↓
            .AddItem(ItemID.Seagull, (0, 0, 0, 0)) //海鸥
            .AddItem(ItemID.Seahorse, (0, 0, 0, 0)) //海马
            //地狱 ↓
            .AddItem(ItemID.Lavafly, (0, 0, 0, 0)) //熔岩萤火虫
            .AddItem(ItemID.MagmaSnail, (0, 0, 0, 0)) //岩浆蜗牛
            //墓地 ↓
            .AddItem(ItemID.Rat, (0, 0, 0, 0)) //老鼠
            .AddItem(ItemID.Maggot, (0, 0, 0, 0)) //蛆
            //微光 ↓
            .AddItem(ItemID.Shimmerfly, (0, 0, 0, 0)) //微光萤火虫
            //神圣之地 ↓
            .AddItem(ItemID.LightningBug, (0, 0, 0, 0), Condition.Hardmode) //闪电虫
            //蝴蝶 ↓
            .AddItem(ItemID.MonarchButterfly, (0, 0, 0, 0)) //帝王蝶
            .AddItem(ItemID.SulphurButterfly, (0, 0, 0, 0)) //硫磺蝶
            .AddItem(ItemID.JuliaButterfly, (0, 0, 0, 0)) //朱莉娅蝶
            .AddItem(ItemID.UlyssesButterfly, (0, 0, 0, 0)) //尤利西斯蝶
            .AddItem(ItemID.ZebraSwallowtailButterfly, (0, 0, 0, 0)) //斑马燕尾蝶
            .AddItem(ItemID.PurpleEmperorButterfly, (0, 0, 0, 0)) //紫帝王蝶
            .AddItem(ItemID.RedAdmiralButterfly, (0, 0, 0, 0)) //红海军上将蝶
            .AddItem(ItemID.TreeNymphButterfly, (0, 0, 0, 0)) //树仙蝶
            .AddItem(ItemID.HellButterfly, (0, 0, 0, 0)) //地狱蝶
            .AddItem(ItemID.EmpressButterfly, (0, 0, 0, 0), Condition.DownedPlantera) //皇后蝶
            //蜻蜓 ↓
            .AddItem(ItemID.RedDragonfly, (0, 0, 0, 0)) //红蜻蜓
            .AddItem(ItemID.BlueDragonfly, (0, 0, 0, 0)) //蓝蜻蜓
            .AddItem(ItemID.GreenDragonfly, (0, 0, 0, 0)) //绿蜻蜓
            .AddItem(ItemID.YellowDragonfly, (0, 0, 0, 0)) //黄蜻蜓
            .AddItem(ItemID.BlackDragonfly, (0, 0, 0, 0)) //黑蜻蜓
            .AddItem(ItemID.OrangeDragonfly, (0, 0, 0, 0)) //橙蜻蜓
            //染料 ↓
            .AddItem(ItemID.RedHusk, (0, 1, 0, 0)) //红壳
            .AddItem(ItemID.OrangeBloodroot, (0, 1, 0, 0)) //橙血根
            .AddItem(ItemID.YellowMarigold, (0, 1, 0, 0)) //黄万寿菊
            .AddItem(ItemID.LimeKelp, (0, 1, 0, 0)) //青柠海带
            .AddItem(ItemID.GreenMushroom, (0, 1, 0, 0)) //绿蘑菇
            .AddItem(ItemID.TealMushroom, (0, 1, 0, 0)) //青绿蘑菇
            .AddItem(ItemID.CyanHusk, (0, 1, 0, 0)) //青壳
            .AddItem(ItemID.SkyBlueFlower, (0, 1, 0, 0)) //天蓝花
            .AddItem(ItemID.BlueBerries, (0, 1, 0, 0)) //蓝浆果
            .AddItem(ItemID.PurpleMucos, (0, 1, 0, 0)) //紫粘液
            .AddItem(ItemID.VioletHusk, (0, 1, 0, 0)) //紫罗兰壳
            .AddItem(ItemID.PinkPricklyPear, (0, 1, 0, 0)) //粉仙人掌果
            .AddItem(ItemID.BlackInk, (0, 1, 0, 0)); //黑墨水
            #endregion

            #region [灾厄 Calamity Mod]
            plcr.AddModItem("CalamityMod/CharredLasher", (0, 1, 0, 0)); //焦黑鞭
            #endregion

            #region [灾劫 Catalyst Mod]
            // 暂无
            #endregion

            #endregion

            #region 【渔获物品】

            #region [原版]
            fishery.AddItem(ItemType<DreamSeaCallingBottle>(), (5, 0, 0, 0)) //唤海瓶
            .AddItem(ItemType<DreamSeaCrateBook>(), (5, 0, 0, 0)) //梦海宝匣大全
            //渔夫任务鱼饵 ↓
            .AddItem(ItemID.ApprenticeBait, (0, 0, 5, 0)) //学徒鱼饵
            .AddItem(ItemID.JourneymanBait, (0, 0, 10, 0)) //熟练鱼饵
            .AddItem(ItemID.MasterBait, (0, 0, 50, 0)) //大师鱼饵
            //水母鱼饵 ↓
            .AddItem(ItemID.BlueJellyfish, (0, 0, 75, 0)) //蓝水母
            .AddItem(ItemID.GreenJellyfish, (0, 1, 0, 0), Condition.Hardmode) //绿水母
            .AddItem(ItemID.PinkJellyfish, (0, 0, 75, 0)) //粉水母
            .AddItem(ItemID.ChumBucket, (0, 1, 0, 0), BloodMoonHappened) //鱼饵桶(血月)
            //渔夫商店补充 ↓
            .AddItem(ItemID.AnglerHat, (0, 5, 0, 0)) //渔夫帽
            .AddItem(ItemID.AnglerVest, (0, 5, 0, 0)) //渔夫背心
            .AddItem(ItemID.AnglerPants, (0, 5, 0, 0)) //渔夫裤
            .AddItem(ItemID.FishingBobber, (0, 1, 0, 0)) //钓鱼浮标
            .AddItem(ItemID.HighTestFishingLine, (0, 4, 0, 0)) //优质钓鱼线
            .AddItem(ItemID.AnglerEarring, (0, 4, 0, 0)) //渔夫耳环
            .AddItem(ItemID.TackleBox, (0, 4, 0, 0)) //钓具箱
            .AddItem(ItemID.FishermansGuide, (0, 4, 0, 0)) //渔民袖珍宝典
            .AddItem(ItemID.WeatherRadio, (0, 4, 0, 0)) //天气收音机
            .AddItem(ItemID.Sextant, (0, 4, 0, 0)) //六分仪

            .AddItem(ItemID.FuzzyCarrot, (0, 5, 0, 0)) //绒毛胡萝卜
            .AddItem(ItemID.FishMinecart, (0, 5, 0, 0)) //鲤鱼矿车
            .AddItem(ItemID.FishHook, (0, 5, 0, 0)) //鱼钩
            .AddItem(ItemID.GoldenBugNet, (0, 10, 0, 0)) //金虫网
            .AddItem(ItemID.GoldenFishingRod, (0, 15, 0, 0)) //金钓竿
            .AddItem(ItemID.BottomlessBucket, (0, 10, 0, 0)) //无底水桶
            .AddItem(ItemID.SuperAbsorbantSponge, (0, 10, 0, 0)) //超级吸收绵
            .AddItem(ItemID.BottomlessHoneyBucket, (0, 10, 0, 0)) //无底蜂蜜桶
            .AddItem(ItemID.HoneyAbsorbantSponge, (0, 10, 0, 0)) //蜂蜜吸收绵
            .AddItem(ItemID.BottomlessLavaBucket, (0, 10, 0, 0)) //无底熔岩桶
            .AddItem(ItemID.LavaAbsorbantSponge, (0, 10, 0, 0)) //熔岩吸收绵
            .AddItem(ItemID.HotlineFishingHook, (0, 10, 0, 0), Condition.Hardmode) //熔线钓钩
            .AddItem(ItemID.FinWings, (0, 15, 0, 0), Condition.Hardmode) //鳍翼
 
            //渔获-其他
            .AddItem(ItemID.GoldenCarp, (0, 1, 0, 0)) //金鲤鱼
            .AddItem(ItemID.OldShoe, (0, 0, 0, 2)) //旧鞋
            .AddItem(ItemID.FishingSeaweed, (0, 0, 0, 2)) //海草
            .AddItem(ItemID.TinCan, (0, 0, 0, 2)); //锡罐

            #endregion

            #region [灾厄 Calamity Mod]
            fishery.AddModItem("CalamityMod/GrandMarquisBait", (0, 1, 0, 0)) //大侯爵鱼饵
            .AddModItem("CalamityMod/BabyGhostBellItem", (0, 0, 10, 0)) //幼年幽灵铃
            .AddModItem("CalamityMod/SeaMinnowItem", (0, 0, 10, 0)) //海鲦鱼
            .AddModItem("CalamityMod/TwinklerItem", (0, 0, 20, 0), Condition.Hardmode) //闪烁鱼
            .AddModItem("CalamityMod/ArcturusAstroidean", (0, 0, 20, 0), Condition.Hardmode) //大角星海星
            .AddModItem("CalamityMod/EnchantedStarfish", (0, 1, 50, 0)) //附魔海星
            //鱼-沉沦之海 ↓
            .AddModItem("CalamityMod/PrismaticGuppy", (0, 0, 10, 0)) //棱镜孔雀鱼
            .AddModItem("CalamityMod/SunkenSailfish", (0, 0, 10, 0)) //沉没旗鱼
            .AddModItem("CalamityMod/GreenwaveLoach", (0, 5, 0, 0)) //绿波泥鳅
            //鱼-硫磺海 ↓
            .AddModItem("CalamityMod/PlantyMush", (0, 0, 15, 0)) //植物糊
            //鱼-硫火之崖 ↓
            .AddModItem("CalamityMod/Shadowfish", (0, 0, 10, 0)) //暗影鱼
            .AddModItem("CalamityMod/CoastalDemonfish", (0, 0, 10, 0)) //海岸恶魔鱼
            .AddModItem("CalamityMod/CharredLasher", (0, 10, 0, 0)) //焦黑鞭
            //鱼-星辉瘟疫 ↓
            .AddModItem("CalamityMod/TwinklingPollox", (0, 0, 10, 0), Condition.Hardmode) //闪烁波洛克斯
            .AddModItem("CalamityMod/ProcyonidPrawn", (0, 0, 10, 0), Condition.Hardmode) //南河三虾
            .AddModItem("CalamityMod/AldebaranAlewife", (0, 5, 0, 0), Condition.Hardmode) //毕宿五鲱鱼
            //鱼-摸彩袋 ↓
            .AddModItem("CalamityMod/StuffedFish", (0, 1, 50, 0)) //填充鱼
            .AddModItem("CalamityMod/GlimmeringGemfish", (0, 2, 0, 0)) //微光宝石鱼
            .AddModItem("CalamityMod/Gorecodile", (0, 3, 50, 0)) //血鳄鱼
            .AddModItem("CalamityMod/FishofEleum", (0, 1, 0, 0), Condition.Hardmode) //幻魂鱼
            .AddModItem("CalamityMod/FishofLight", (0, 1, 0, 0), Condition.Hardmode) //光鱼
            .AddModItem("CalamityMod/FishofNight", (0, 1, 0, 0), Condition.Hardmode) //夜鱼
            .AddModItem("CalamityMod/FishofFlight", (0, 1, 0, 0), Condition.Hardmode) //飞鱼
            .AddModItem("CalamityMod/SunbeamFish", (0, 1, 0, 0), Condition.Hardmode) //阳光鱼
            .AddModItem("CalamityMod/Havocfish", (0, 1, 0, 0), Condition.Hardmode); //浩劫鱼
            
            #endregion

            #region [灾劫 Catalyst Mod]
            // 暂无
            #endregion

            #endregion

            plcr.Register();
            fishery.Register();
        }
    }
}
