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
using CalamityMod.Items.Potions.Alcohol;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class OreNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 28;
            NPCID.Sets.ExtraFramesCount[Type] = 18;
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
            AnimationType = NPCID.TownCat;
            //AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.OreNPC")
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
                //玩家的背包中存在【铁矿】或【铅矿】时，NPC生成
                if (player.inventory.Any(item => item.type == ItemID.IronOre || item.type == ItemID.LeadOre))
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
                this.GetLocalizedValue("Name.OreNPC")
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
            button = Language.GetTextValue("Mods.LaziestNPC.NPCs.OreNPC.button1");
            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.OreNPC.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Ores";
            }
            else
            {
                shopName = "Accessories";
            }
        }

        public override void AddShops()
        {
            var ores = new NPCShop(Type, "Ores");
            var accs = new NPCShop(Type, "Accessories");

            #region 【矿物商店】

            #region [原版]
            ores.AddItem(ItemID.Amethyst, (0, 0, 10, 0)) //紫水晶
            .AddItem(ItemID.Topaz, (0, 0, 15, 0)) //黄玉
            .AddItem(ItemID.Sapphire, (0, 0, 25, 0)) //蓝玉
            .AddItem(ItemID.Emerald, (0, 0, 25, 0)) //翡翠
            .AddItem(ItemID.Ruby, (0, 0, 50, 0)) //红玉
            .AddItem(ItemID.Amber, (0, 0, 50, 0)) //琥珀
            .AddItem(ItemID.Diamond, (0, 0, 75, 0)) //钻石

            .AddItem(ItemID.FossilOre, (0, 0, 25, 0)) //坚固化石

            .AddItem(ItemID.CopperOre, (0, 0, 25, 0)) //铜矿
            .AddItem(ItemID.TinOre, (0, 0, 25, 0)) //锡矿
            .AddItem(ItemID.IronOre, (0, 0, 50, 0)) //铁矿
            .AddItem(ItemID.LeadOre, (0, 0, 50, 0)) //铅矿
            .AddItem(ItemID.SilverOre, (0, 0, 75, 0)) //银矿
            .AddItem(ItemID.TungstenOre, (0, 0, 75, 0)) //钨矿
            .AddItem(ItemID.GoldOre, (0, 1, 0, 0)) //金矿
            .AddItem(ItemID.PlatinumOre, (0, 1, 0, 0)) //铂金矿
            .AddItem(ItemID.Obsidian, (0, 0, 25, 0)) //黑曜石

            .AddItem(ItemID.DemoniteOre, (0, 1, 0, 0)) //魔矿
            .AddItem(ItemID.CrimtaneOre, (0, 1, 0, 0)) //猩红矿
            .AddItem(ItemID.Meteorite, (0, 1, 50, 0), Condition.DownedEowOrBoc) //陨石
            .AddItem(ItemID.Hellstone, (0, 1, 50, 0), Condition.DownedEowOrBoc) //狱石
            .AddItem(ItemID.LifeCrystal, (0, 5, 0, 0), Condition.DownedEyeOfCthulhu) //生命水晶

            .AddItem(ItemID.CobaltOre, (0, 2, 0, 0), Condition.Hardmode) //钴矿
            .AddItem(ItemID.PalladiumOre, (0, 2, 0, 0), Condition.Hardmode) //钯金矿
            .AddItem(ItemID.MythrilOre, (0, 2, 50, 0), Condition.Hardmode) //秘银矿
            .AddItem(ItemID.OrichalcumOre, (0, 2, 50, 0), Condition.Hardmode) //山铜矿
            .AddItem(ItemID.AdamantiteOre, (0, 3, 0, 0), Condition.Hardmode) //精金矿
            .AddItem(ItemID.TitaniumOre, (0, 3, 0, 0), Condition.Hardmode) //钛金矿
            .AddItem(ItemID.LifeFruit, (0, 10, 0, 0), Condition.DownedMechBossAny) //生命果
            .AddItem(ItemID.ChlorophyteOre, (0, 3, 50, 0), Condition.DownedMechBossAll); //叶绿矿
            
            #endregion

            #region [灾厄 Calamity Mod]
            ores.AddModItem("CalamityMod/SeaPrism", (0, 0, 50, 0)) //海棱晶
            .AddModItem("CalamityMod/AerialiteOre", (0, 1, 0, 0), PerOrHive) //天蓝矿

            .AddModItem("CalamityMod/InfernalSuevite", (0, 2, 0, 0), Condition.Hardmode) //地狱角砾岩
            .AddModItem("CalamityMod/CryonicOre", (0, 2, 50, 0), DownedCryogen) //寒元矿
            .AddModItem("CalamityMod/PerennialOre", (0, 3, 0, 0), Condition.DownedPlantera) //永恒矿
            .AddModItem("CalamityMod/ScoriaOre", (0, 3, 50, 0), Condition.DownedGolem) //熔渣矿
            .AddModItem("CalamityMod/AstralOre", (0, 4, 0, 0), AstrumBugAndCultist) //炫星矿

            .AddModItem("CalamityMod/ExodiumCluster", (0, 5, 0, 0), Condition.DownedMoonLord) //起源之簇
            .AddModItem("CalamityMod/UelibloomOre", (0, 6, 0, 0), DownedProvidence) //龙蒿矿
            .AddModItem("CalamityMod/AuricOre", (0, 9, 0, 0), DownedYharon); //圣金源矿
            
            #endregion

            #region [灾劫 Catalyst Mod]
            ores.AddModItem("CatalystMod/MetanovaOre", (0, 8, 0, 0), DownedAstrageldon); //辉恒星矿

            #endregion

            #region [瑟银 Thorium Mod]
            ores.AddModItem("ThoriumMod/Opal", (0, 0, 25, 0)) //欧珀
            .AddModItem("ThoriumMod/Aquamarine", (0, 0, 25, 0)) //海蓝宝石

            .AddModItem("ThoriumMod/SmoothCoal", (0, 0, 50, 0)) //光滑的煤
            .AddModItem("ThoriumMod/LifeQuartz", (0, 0, 50, 0)) //生命石英
            .AddModItem("ThoriumMod/ThoriumOre", (0, 0, 75, 0)) //瑟银矿
            .AddModItem("ThoriumMod/Aquaite", (0, 0, 75, 0)) //海洋矿

            .AddModItem("ThoriumMod/LodeStoneChunk", (0, 2, 50, 0), DownedFallenBeholder) //地脉矿
            .AddModItem("ThoriumMod/ValadiumChunk", (0, 2, 50, 0), DownedFallenBeholder) //虚金矿
            .AddModItem("ThoriumMod/IllumiteChunk", (0, 3, 0, 0), Condition.DownedPlantera); //荧光石

            #endregion

            #endregion

            #region 【配饰商店】

            #region [原版]

            #region 机动性相关
            //靴子系列↓
            accs.AddItem(ItemID.Aglet, (0, 2, 0, 0)) //鞋带束头
            .AddItem(ItemID.AnkletoftheWind, (0, 3, 0, 0)) //疾风脚镯
            .AddItem(ItemID.HermesBoots, (0, 2, 0, 0)) //赫尔墨斯靴
            .AddItem(ItemID.WaterWalkingBoots, (0, 2, 0, 0)) //水上漂靴
            .AddItem(ItemID.SailfishBoots, (0, 4, 0, 0)) //旗鱼靴
            .AddItem(ItemID.FlurryBoots, (0, 3, 0, 0)) //疾风雪靴
            .AddItem(ItemID.IceSkates, (0, 4, 0, 0)) //溜冰鞋
            .AddItem(ItemID.FlowerBoots, (0, 3, 0, 0)) //花靴
            .AddItem(ItemID.SandBoots, (0, 3, 0, 0)) //沙丘行者靴
            .AddItem(ItemID.FlyingCarpet, (0, 4, 0, 0)) //飞毯
            .AddItem(ItemID.FrogLeg, (0, 5, 0, 0)) //蛙腿
            //.AddItem(ItemID.TerrasparkBoots, (0, 30, 0, 0), Condition.DownedMoonLord) //泰拉闪耀靴
            //忍者大师装备 ↓
            .AddItem(ItemID.ClimbingClaws, (0, 2, 0, 0)) //攀爬爪
            .AddItem(ItemID.ShoeSpikes, (0, 2, 0, 0)) //鞋钉
            .AddItem(ItemID.BlackBelt, (0, 10, 0, 0), Condition.DownedPlantera) //黑腰带
            .AddItem(ItemID.Tabi, (0, 10, 0, 0), Condition.DownedPlantera) //分趾厚底袜
            //气瓶和气球 ↓
            .AddItem(ItemID.ShinyRedBalloon, (0, 3, 0, 0)) //闪亮红气球
            .AddItem(ItemID.CloudinaBottle, (0, 3, 0, 0)) //云朵瓶
            .AddItem(ItemID.BlizzardinaBottle, (0, 4, 0, 0)) //暴雪瓶
            .AddItem(ItemID.SandstorminaBottle, (0, 5, 0, 0)) //沙暴瓶
            .AddItem(ItemID.TsunamiInABottle, (0, 4, 0, 0)) //海啸瓶
            .AddItem(ItemID.BalloonPufferfish, (0, 3, 0, 0)) //气球河豚
            .AddItem(ItemID.LuckyHorseshoe, (0, 2, 0, 0)) //幸运马掌
            //熔岩靴系列 ↓
            //.AddItem(ItemID.ObsidianSkull, (0, 1, 0, 0)) //黑曜石骷髅头
            .AddItem(ItemID.LavaCharm, (0, 5, 0, 0)) //熔岩护身符
            .AddItem(ItemID.ObsidianRose, (0, 5, 0, 0)) //黑曜石玫瑰
            .AddItem(ItemID.FlameWakerBoots, (0, 5, 0, 0)) //烈焰靴
            //海洋相关 ↓
            .AddItem(ItemID.Flipper, (0, 3, 0, 0)) //脚蹼
            .AddItem(ItemID.DivingHelmet, (0, 5, 0, 0)) //潜水头盔
            .AddItem(ItemID.JellyfishNecklace, (0, 5, 0, 0)) //水母项链
            .AddItem(ItemID.FloatingTube, (0, 3, 0, 0)) //浮游圈
            //其他类 ↓
            .AddItem(ItemID.DiscountCard, (0, 6, 0, 0), Condition.DownedPirates) //优惠卡
            .AddItem(ItemID.GoldRing, (0, 6, 0, 0), Condition.DownedPirates) //金戒指
            .AddItem(ItemID.LuckyCoin, (0, 6, 0, 0), Condition.DownedPirates) //幸运币
            #endregion

            #region 生命与魔力相关
            //生命恢复类 ↓
            .AddItem(ItemID.BandofRegeneration, (0, 1, 0, 0)) //再生手环
            //.AddItem(ItemID.ShinyStone, (0, 15, 0, 0), Condition.DownedGolem) //闪亮石
            //魔力恢复类 ↓
            .AddItem(ItemID.NaturesGift, (0, 3, 0, 0)) //自然恩赐
            .AddItem(ItemID.BandofStarpower, (0, 2, 0, 0)) //星力手环
            .AddItem(ItemID.CelestialMagnet, (0, 3, 0, 0)) //天界磁石
            #endregion

            #region 战斗相关
            //伤害类 ↓
            .AddItem(ItemID.WhiteString, (0, 1, 0, 0)) //白绳
            .AddItem(ItemID.BlueCounterweight, (0, 1, 0, 0)) //蓝平衡锤
            .AddItem(ItemID.YoYoGlove, (0, 5, 0, 0), Condition.Hardmode) //悠悠球手套

            .AddItem(ItemID.FeralClaws, (0, 3, 0, 0)) //猛爪手套
            .AddItem(ItemID.MagmaStone, (0, 4, 0, 0)) //岩浆石

            .AddItem(ItemID.SharkToothNecklace, (0, 2, 0, 0)) //鲨牙项链
            .AddItem(ItemID.MagicQuiver, (0, 5, 0, 0), Condition.Hardmode) //魔法箭袋
            .AddItem(ItemID.RifleScope, (0, 10, 0, 0), Condition.DownedPlantera) //步枪瞄准镜

            .AddItem(ItemID.PygmyNecklace, (0, 5, 0, 0), Condition.DownedQueenBee) //矮人项链
            .AddItem(ItemID.HerculesBeetle, (0, 10, 0, 0), Condition.DownedPlantera) //大力士甲虫
            .AddItem(ItemID.NecromanticScroll, (0, 10, 0, 0), PumpkinMoonHappened) //死灵卷轴
            .AddItem(ItemID.SquireShield, (0, 4, 0, 0), Condition.DownedEowOrBoc) //侍卫护盾
            .AddItem(ItemID.ApprenticeScarf, (0, 4, 0, 0), Condition.DownedEowOrBoc) //学徒围巾
            .AddItem(ItemID.MonkBelt, (0, 5, 0, 0), Condition.DownedMechBossAny) //武僧腰带
            .AddItem(ItemID.HuntressBuckler, (0, 5, 0, 0), Condition.DownedMechBossAny) //女猎人圆盾
            //防御类 ↓
            .AddItem(ItemID.FrozenTurtleShell, (0, 5, 0, 0), Condition.Hardmode) //冰冻海龟壳
            .AddItem(ItemID.PaladinsShield, (0, 10, 0, 0), Condition.DownedPlantera) //圣骑士护盾
            //宝箱怪掉落物 ↓
            .AddItem(ItemID.TitanGlove, (0, 5, 0, 0), Condition.Hardmode) //泰坦手套
            .AddItem(ItemID.PhilosophersStone, (0, 5, 0, 0), Condition.Hardmode) //点金石
            .AddItem(ItemID.CrossNecklace, (0, 5, 0, 0), Condition.Hardmode) //十字项链
            .AddItem(ItemID.StarCloak, (0, 5, 0, 0), Condition.Hardmode) //星星斗篷
            .AddItem(ItemID.FleshKnuckles, (0, 5, 0, 0), Condition.Hardmode) //血肉指虎
            .AddItem(ItemID.PutridScent, (0, 5, 0, 0), Condition.Hardmode) //腐香囊
            //十字章护盾系列 ↓
            .AddItem(ItemID.CobaltShield, (0, 4, 0, 0), Condition.DownedSkeletron) //钴护盾
            .AddItem(ItemID.Bezoar, (0, 3, 0, 0)) //牛黄
            .AddItem(ItemID.AdhesiveBandage, (0, 3, 0, 0)) //粘性绷带
            .AddItem(ItemID.ArmorPolish, (0, 5, 0, 0), Condition.Hardmode) //盔甲抛光剂
            .AddItem(ItemID.Vitamins, (0, 5, 0, 0), Condition.Hardmode) //维生素
            .AddItem(ItemID.TrifoldMap, (0, 5, 0, 0), Condition.Hardmode) //三折地图
            .AddItem(ItemID.FastClock, (0, 5, 0, 0), Condition.Hardmode) //快走时钟
            .AddItem(ItemID.Nazar, (0, 5, 0, 0), Condition.Hardmode) //邪眼
            .AddItem(ItemID.Megaphone, (0, 5, 0, 0), Condition.Hardmode) //扩音器
            .AddItem(ItemID.Blindfold, (0, 5, 0, 0), Condition.Hardmode) //蒙眼布
            .AddItem(ItemID.PocketMirror, (0, 5, 0, 0), Condition.Hardmode) //袖珍镜
            .AddItem(ItemID.HandWarmer, (0, 5, 0, 0)) //暖手宝
            //.AddItem(ItemID.AnkhShield, (0, 30, 0, 0), Condition.DownedMoonLord) //十字章护盾
            //天界壳系列 ↓
            .AddItem(ItemID.MoonCharm, (0, 6, 0, 0), Condition.Hardmode) //月光护身符
            .AddItem(ItemID.NeptunesShell, (0, 8, 0, 0), EclipseHappened) //海神贝壳
            .AddItem(ItemID.MoonStone, (0, 8, 0, 0), EclipseHappened) //月亮石
                                                                      //.AddItem(ItemID.SunStone, (0, 20, 0, 0), Condition.DownedGolem) //太阳石

            #endregion

            #region 建筑相关
            .AddItem(ItemID.PortableStool, (0, 1, 0, 0)) //便携凳(梯凳)
            .AddItem(ItemID.AncientChisel, (0, 5, 0, 0)) //远古凿子
            .AddItem(ItemID.BrickLayer, (0, 5, 0, 0)) //砌砖刀
            .AddItem(ItemID.ExtendoGrip, (0, 5, 0, 0)) //加长握爪
            .AddItem(ItemID.PaintSprayer, (0, 5, 0, 0)) //喷漆器
            .AddItem(ItemID.PortableCementMixer, (0, 5, 0, 0)) //便携式水泥搅拌机
            .AddItem(ItemID.TreasureMagnet, (0, 5, 0, 0), Condition.DownedSkeletron) //宝藏磁石

            #endregion

            //其他类 ↓
            .AddItem(ItemID.CordageGuide, (0, 1, 0, 0)) //植物纤维绳索宝典
            .AddItem(ItemID.Shackle, (0, 1, 0, 0)) //镣铐
            .AddItem(ItemID.PanicNecklace, (0, 2, 0, 0)); //恐慌项链
            #endregion

            #endregion

            ores.Register();
            accs.Register();
        }
    }
}
