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
            plcr.AddItem(ItemType<SunlightNet>(), (0, 25, 0, 0)) //阳光捕虫网
            .AddItem(ItemID.HerbBag, (0, 0, 25, 0)) //草药袋
            .AddItem(ItemID.Mushroom, (0, 0, 1, 0)) //蘑菇
            .AddItem(ItemID.GlowingMushroom, (0, 0, 1, 50)) //发光蘑菇
            .AddItem(ItemID.ViciousMushroom, (0, 0, 3, 0)) //毒蘑菇
            .AddItem(ItemID.VileMushroom, (0, 0, 3, 0)) //魔菇
            //草种子 ↓
            .AddItem(ItemID.GrassSeeds, (0, 0, 0, 10)) //草种子
            .AddItem(ItemID.JungleGrassSeeds, (0, 0, 0, 25)) //丛林草种子
            .AddItem(ItemID.MushroomGrassSeeds, (0, 0, 0, 25)) //蘑菇草种子
            .AddItem(ItemID.CrimsonSeeds, (0, 0, 0, 50)) //猩红种子
            .AddItem(ItemID.CorruptSeeds, (0, 0, 0, 50)) //腐化种子
            .AddItem(ItemID.AshGrassSeeds, (0, 0, 0, 50)) //灰烬草种子
            .AddItem(ItemID.HallowedSeeds, (0, 0, 1, 0), Condition.Hardmode) //神圣种子
            .AddItem(ItemID.Acorn, (0, 0, 0, 10)) //橡实
            
            .AddItem(ItemID.Worm, (0, 0, 5, 0)) //蠕虫
            .AddItem(ItemID.LadyBug, (0, 0, 5, 0)) //瓢虫
            .AddItem(ItemID.Frog, (0, 0, 5, 0)) //青蛙
            .AddItem(ItemID.TruffleWorm, (0, 10, 0, 0), Condition.Hardmode) //松露虫
            .AddItem(ItemID.EmpressButterfly, (0, 25, 0, 0), Condition.DownedPlantera) //七彩草蛉
            //染料 ↓
            .AddItem(ItemID.RedHusk, (0, 1, 0, 0)) //红外壳
            .AddItem(ItemID.OrangeBloodroot, (0, 1, 0, 0)) //橙血根草
            .AddItem(ItemID.YellowMarigold, (0, 1, 0, 0)) //黄万寿菊
            .AddItem(ItemID.LimeKelp, (0, 1, 0, 0)) //橙绿海藻
            .AddItem(ItemID.GreenMushroom, (0, 1, 0, 0)) //绿蘑菇
            .AddItem(ItemID.TealMushroom, (0, 1, 0, 0)) //青绿蘑菇
            .AddItem(ItemID.CyanHusk, (0, 1, 0, 0)) //青外壳
            .AddItem(ItemID.SkyBlueFlower, (0, 1, 0, 0)) //天蓝花朵
            .AddItem(ItemID.BlueBerries, (0, 1, 0, 0)) //蓝浆果
            .AddItem(ItemID.PurpleMucos, (0, 1, 0, 0)) //紫粘液
            .AddItem(ItemID.VioletHusk, (0, 1, 0, 0)) //蓝紫外壳
            .AddItem(ItemID.PinkPricklyPear, (0, 1, 0, 0)) //粉仙人掌果
            .AddItem(ItemID.BlackInk, (0, 1, 0, 0)); //黑墨水

            #endregion

            #region [灾厄 Calamity Mod]
            plcr.AddModItem("CalamityMod/ShroombleItem", (0, 0, 5, 0)) //茸宝
            .AddModItem("CalamityMod/PiggyItem", (0, 0, 5, 0)) //小猪猪
            .AddModItem("CalamityMod/BabyGhostBellItem", (0, 0, 10, 0)) //小鬼铃水母
            .AddModItem("CalamityMod/BabyCannonballJellyfishItem", (0, 0, 10, 0)) //小炮弹水母
            .AddModItem("CalamityMod/SeaMinnowItem", (0, 0, 10, 0)) //海洋米诺鱼
            .AddModItem("CalamityMod/BabyFlakCrabItem", (0, 0, 50, 0), Condition.Hardmode) //小高口蟹
            .AddModItem("CalamityMod/TwinklerItem", (0, 0, 50, 0), Condition.Hardmode); //星幻萤火虫

            #endregion

            #region [瑟银 Thorium Mod]
            plcr.AddModItem("ThoriumMod/MarineKelp", (0, 0, 1, 0)) //海藻
            .AddModItem("ThoriumMod/OpalBunny", (0, 0, 5, 0)) //欧珀兔兔
            .AddModItem("ThoriumMod/AquamarineBunny", (0, 0, 5, 0)) //海蓝宝石兔兔
            .AddModItem("ThoriumMod/Crow", (0, 0, 5, 0)) //乌鸦
            .AddModItem("ThoriumMod/DumboOctopus", (0, 0, 5, 0)) //小飞象章鱼
            .AddModItem("ThoriumMod/PurpleDumboOctopus", (0, 0, 5, 0)) //紫小飞象章鱼
            .AddModItem("ThoriumMod/Lobster", (0, 0, 5, 0)) //龙虾
            .AddModItem("ThoriumMod/BlueLobster", (0, 0, 5, 0)); //蓝龙虾

            #endregion

            #endregion

            #region 【渔获物品】

            #region [原版]
            fishery.AddItem(ItemType<CallingSeaBottle>(), (0, 15, 0, 0)) //唤海瓶
            .AddItem(ItemType<DreamSeaCrateBook>(), (0, 25, 0, 0)) //梦海宝匣大全
            //鱼饵 ↓
            .AddItem(ItemID.CanOfWorms, (0, 1, 50, 0)) //蠕虫罐头
            .AddItem(ItemID.ApprenticeBait, (0, 0, 5, 0)) //学徒鱼饵
            .AddItem(ItemID.JourneymanBait, (0, 0, 10, 0)) //熟手诱饵
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
            //鱼饵
            fishery.AddModItem("CalamityMod/GrandMarquisBait", (0, 1, 0, 0)) //尊爵鱼饵
            .AddModItem("CalamityMod/ArcturusAstroidean", (0, 0, 20, 0), Condition.Hardmode) //大角海星
            .AddModItem("CalamityMod/EnchantedStarfish", (0, 1, 50, 0)) //附魔海星
            //鱼-沉沦之海 ↓
            .AddModItem("CalamityMod/PrismaticGuppy", (0, 0, 10, 0)) //棱镜孔雀鱼
            .AddModItem("CalamityMod/SunkenSailfish", (0, 0, 10, 0)) //沉沦帆鱼
            .AddModItem("CalamityMod/GreenwaveLoach", (0, 5, 0, 0)) //绿波泥鳅
            //鱼-硫火之崖 ↓
            .AddModItem("CalamityMod/Shadowfish", (0, 0, 10, 0)) //暗影鱼
            .AddModItem("CalamityMod/CoastalDemonfish", (0, 0, 10, 0)) //海岸恶魔鱼
            .AddModItem("CalamityMod/CharredLasher", (0, 10, 0, 0)) //焦黑鞭笞者
            //鱼-星辉瘟疫 ↓
            .AddModItem("CalamityMod/TwinklingPollox", (0, 0, 50, 0), Condition.Hardmode) //北河三烁光鱼
            .AddModItem("CalamityMod/ProcyonidPrawn", (0, 0, 50, 0), Condition.Hardmode) //南河三明虾
            .AddModItem("CalamityMod/AldebaranAlewife", (0, 0, 50, 0), Condition.Hardmode) //毕宿五灰西鲱
            //鱼-摸彩袋 ↓
            .AddModItem("CalamityMod/StuffedFish", (0, 1, 50, 0)); //填充草药鱼

            #endregion

            #region [瑟银 Thorium Mod]
            fishery.AddModItem("ThoriumMod/MagmaGill", (0, 0, 10, 0)) //熔岩焰鱼
            .AddModItem("ThoriumMod/FlamingCrackGut", (0, 0, 10, 0)); //燃火裂肠鱼


            #endregion

            #endregion

            plcr.Register();
            fishery.Register();
        }
    }
}
