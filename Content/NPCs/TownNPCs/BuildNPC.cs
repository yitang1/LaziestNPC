using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using LaziestNPC.Globals.GlobalItems;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class BuildNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 28;
            NPCID.Sets.ExtraFramesCount[Type] = 11;
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
            AnimationType = NPCID.TownDog;
            //AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.BuildNPC")
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return numTownNPCs > 3;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                this.GetLocalizedValue("Name.BuildNPC")
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
            button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BuildNPC.button1");
            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.BuildNPC.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "NaturalBlocks";
            }
            else
            {
                shopName = "BuildingBlocks";
            }
        }

        public override void AddShops()
        {
            var naturalB = new NPCShop(Type, "NaturalBlocks");
            var buildingB = new NPCShop(Type, "BuildingBlocks");

            //【尽量先只添加需要作为[合成配方]的物块，而不是把游戏中所有物块全添加进来，无他，唯累人尔】
            //【能够被其他物品合成出来的，也暂时不添加】

            #region 【天然物块】

            #region [原版物块]
            //土块和石块类
            naturalB.AddItem(ItemID.DirtBlock, (0, 0, 0, 1)) //土块
            .AddItem(ItemID.StoneBlock, (0, 0, 0, 1)) //石块
            .AddItem(ItemID.CrimstoneBlock, (0, 0, 0, 2)) //猩红石
            .AddItem(ItemID.EbonstoneBlock, (0, 0, 0, 2)) //黑檀石
            .AddItem(ItemID.PearlstoneBlock, (0, 0, 0, 10), Condition.Hardmode) //珍珠石
            .AddItem(ItemID.Granite, (0, 0, 0, 5)) //花岗岩
            .AddItem(ItemID.Marble, (0, 0, 0, 5)) //大理石
            //沙块类
            .AddItem(ItemID.SandBlock, (0, 0, 0, 1)) //沙块
            .AddItem(ItemID.CrimsandBlock, (0, 0, 0, 2)) //猩红沙
            .AddItem(ItemID.EbonsandBlock, (0, 0, 0, 2)) //黑檀沙
            .AddItem(ItemID.PearlsandBlock, (0, 0, 0, 10), Condition.Hardmode) //珍珠沙
            .AddItem(ItemID.Sandstone, (0, 0, 0, 1)) //沙岩块
            .AddItem(ItemID.CrimsonSandstone, (0, 0, 0, 2)) //猩红沙岩块
            .AddItem(ItemID.CorruptSandstone, (0, 0, 0, 2)) //黑檀沙岩块
            .AddItem(ItemID.HallowSandstone, (0, 0, 0, 10), Condition.Hardmode) //珍珠沙岩块
            .AddItem(ItemID.HardenedSand, (0, 0, 0, 1)) //硬化沙
            .AddItem(ItemID.CrimsonHardenedSand, (0, 0, 0, 2)) //猩红硬化沙
            .AddItem(ItemID.CorruptHardenedSand, (0, 0, 0, 2)) //腐化硬化沙
            .AddItem(ItemID.HallowHardenedSand, (0, 0, 0, 10), Condition.Hardmode) //神圣硬化沙
            .AddItem(ItemID.DesertFossil, (0, 0, 1, 0)) //沙漠化石
            .AddItem(ItemID.AshBlock, (0, 0, 0, 2)) //灰烬块
            //泥块类
            .AddItem(ItemID.ClayBlock, (0, 0, 0, 1)) //黏土块
            .AddItem(ItemID.MudBlock, (0, 0, 0, 1)) //泥块
            .AddItem(ItemID.SlushBlock, (0, 0, 0, 1)) //雪泥块
            .AddItem(ItemID.SiltBlock, (0, 0, 0, 1)) //淤泥块(泥沙块)
            //雪块类
            .AddItem(ItemID.SnowBlock, (0, 0, 0, 1)) //雪块
            .AddItem(ItemID.IceBlock, (0, 0, 0, 1)) //冰块
            .AddItem(ItemID.RedIceBlock, (0, 0, 0, 1)) //红冰块
            .AddItem(ItemID.PurpleIceBlock, (0, 0, 0, 1)) //紫冰块
            .AddItem(ItemID.PinkIceBlock, (0, 0, 0, 10), Condition.Hardmode) //粉冰块
            //栽培类
            .AddItem(ItemID.Hay, (0, 0, 0, 1)) //干草
            .AddItem(ItemID.Cactus, (0, 0, 0, 2)) //仙人掌
            .AddItem(ItemID.Pumpkin, (0, 0, 0, 5)) //南瓜
            .AddItem(ItemID.BambooBlock, (0, 0, 0, 5)) //竹块
            //空岛类
            .AddItem(ItemID.Cloud, (0, 0, 1, 0)) //云块
            .AddItem(ItemID.RainCloud, (0, 0, 1, 0)) //雨云块
            .AddItem(ItemID.SunplateBlock, (0, 0, 1, 0)) //日盘块
            //蜂巢类
            .AddItem(ItemID.Hive, (0, 0, 1, 0)) //蜂巢块
            .AddItem(ItemID.HoneyBlock, (0, 0, 3, 0)) //蜂蜜块
            .AddItem(ItemID.CrispyHoneyBlock, (0, 0, 3, 0)); //松脆蜂蜜块
            
            #endregion

            #region [模组物块]

            #region 灾厄 Calamity Mod
            naturalB.AddModItem("CalamityMod/EutrophicSand", (0, 0, 0, 10)) //富营养沙
            .AddModItem("CalamityMod/Navystone", (0, 0, 0, 10)) //沉沦渊石
            .AddModItem("CalamityMod/SulphurousSand", (0, 0, 0, 10)) //硫磺沙
            .AddModItem("CalamityMod/BrimstoneSlag", (0, 0, 0, 20)) //硫磺火石砖
            .AddModItem("CalamityMod/PlantyMush", (0, 0, 5, 0)) //植物混融块
            .AddModItem("CalamityMod/AbyssGravel", (0, 0, 0, 20), Condition.DownedEowOrBoc) //深渊砾石
            .AddModItem("CalamityMod/Voidstone", (0, 0, 1, 0), Condition.Hardmode) //虚空石
            //星辉瘟疫
            .AddModItem("CalamityMod/AstralDirt", (0, 0, 1, 0), Condition.Hardmode) //星幻土块
            .AddModItem("CalamityMod/AstralStone", (0, 0, 1, 0), Condition.Hardmode) //星幻石块
            .AddModItem("CalamityMod/AstralSnow", (0, 0, 1, 0), Condition.Hardmode) //星幻雪块
            .AddModItem("CalamityMod/AstralIce", (0, 0, 1, 0), Condition.Hardmode) //星幻冰雪块
            .AddModItem("CalamityMod/AstralSand", (0, 0, 1, 0), Condition.Hardmode) //星幻沙块
            .AddModItem("CalamityMod/AstralSandstone", (0, 0, 1, 0), Condition.Hardmode) //星幻沙岩块
            .AddModItem("CalamityMod/HardenedAstralSand", (0, 0, 1, 0), Condition.Hardmode) //硬化星幻沙块
            .AddModItem("CalamityMod/AstralClay", (0, 0, 1, 0), Condition.Hardmode) //星幻粘土
            .AddModItem("CalamityMod/NovaeSlag", (0, 0, 50, 0), Condition.Hardmode) //新星熔渣
            .AddModItem("CalamityMod/CelestialRemains", (0, 1, 0, 0), Condition.Hardmode); //星界残骸

            #endregion

            #region 瑟银 Thorium Mod
            naturalB.AddModItem("ThoriumMod/BrackishClump", (0, 0, 5, 0)) //咸泥块
            .AddModItem("ThoriumMod/MarineBlock", (0, 0, 0, 20), Condition.DownedEowOrBoc) //海洋块
            .AddModItem("ThoriumMod/LeakyMarineBlock", (0, 0, 0, 20), Condition.DownedEowOrBoc) //渗漏海洋块
            .AddModItem("ThoriumMod/MossyMarineBlock", (0, 0, 0, 20), Condition.DownedEowOrBoc) //多苔海洋块
            .AddModItem("ThoriumMod/LeakyMossyMarineBlock", (0, 0, 0, 20), Condition.DownedEowOrBoc); //渗漏多苔海洋块
            
            #endregion

            #endregion

            #endregion

            #region 【建筑物块】

            #region [原版物块]
            //照明物块
            buildingB.AddItem(ItemID.WhiteTorch, (0, 0, 0, 20)) //白火把
            .AddItem(ItemID.UltrabrightTorch, (0, 0, 0, 50)) //超亮火把
            .AddItem(ItemID.GlassLantern, (0, 0, 0, 10)) //玻璃灯笼
            .AddItem(ItemID.GlassLamp, (0, 0, 0, 10)) //玻璃灯
            .AddItem(ItemID.LampPost, (0, 0, 0, 10)) //路灯
            //木材类
            .AddItem(ItemID.Wood, (0, 0, 0, 5)) //木材
            .AddItem(ItemID.BorealWood, (0, 0, 0, 10)) //针叶木
            .AddItem(ItemID.RichMahogany, (0, 0, 0, 10)) //红木
            .AddItem(ItemID.Shadewood, (0, 0, 0, 10)) //暗影木
            .AddItem(ItemID.Ebonwood, (0, 0, 0, 10)) //黑檀木(乌木)
            .AddItem(ItemID.Pearlwood, (0, 0, 0, 20), Condition.Hardmode) //珍珠木
            .AddItem(ItemID.PalmWood, (0, 0, 0, 10)) //棕榈木
            .AddItem(ItemID.DynastyWood, (0, 0, 0, 20)) //王朝木
            .AddItem(ItemID.AshWood, (0, 0, 0, 10)) //灰烬木
            .AddItem(ItemID.SpookyWood, (0, 0, 1, 0), MourningWoodOrSplinter) //阴森木
            .AddItem(ItemID.CandyCaneBlock, (0, 0, 0, 10)) //糖棒块
            .AddItem(ItemID.GreenCandyCaneBlock, (0, 0, 0, 10)) //绿糖棒块
            .AddItem(ItemID.Glass, (0, 0, 0, 5)) //玻璃
            .AddItem(ItemID.GrayBrick, (0, 0, 0, 5)) //灰砖
            .AddItem(ItemID.SandstoneBrick, (0, 0, 0, 5)) //沙岩砖
            .AddItem(ItemID.ObsidianBrick, (0, 0, 0, 10)) //黑曜石砖
            //地牢砖(TODO：暂时设计成任何时期都能获得)
            .AddItem(ItemID.BlueBrick, (0, 0, 0, 20)) //蓝砖
            .AddItem(ItemID.GreenBrick, (0, 0, 0, 20)) //绿砖
            .AddItem(ItemID.PinkBrick, (0, 0, 0, 20)) //粉砖
            .AddItem(ItemID.RainbowBrick, (0, 0, 0, 50), Condition.Hardmode) //彩虹砖
            .AddItem(ItemID.LivingFireBlock, (0, 0, 1, 0), Condition.Hardmode) //活火块
            .AddItem(ItemID.LihzahrdBrick, (0, 0, 5, 0), Condition.DownedGolem) //丛林蜥蜴砖
            .AddItem(ItemID.MartianConduitPlating, (0, 0, 5, 0), Condition.DownedMartians) //火星管道镀层

            .AddItem(ItemID.Gravestone, (0, 0, 5, 0)) //墓碑
            //制作站
            .AddItem(ItemID.WorkBench, (0, 0, 5, 0)) //工作台
            .AddItem(ItemID.IronAnvil, (0, 0, 5, 0)) //铁砧
            .AddItem(ItemID.MythrilAnvil, (0, 5, 0, 0), Condition.Hardmode) //秘银砧
            .AddItem(ItemID.Furnace, (0, 0, 5, 0)) //熔炉
            .AddItem(ItemID.Hellforge, (0, 2, 0, 0), Condition.DownedEowOrBoc) //地狱熔炉
            .AddItem(ItemID.TitaniumForge, (0, 5, 0, 0), Condition.Hardmode) //钛金熔炉
            .AddItem(ItemID.Book, (0, 0, 1, 0)) //书
            .AddItem(ItemID.HeavyWorkBench, (0, 0, 5, 0)) //重型工作台(重型装配台)
            .AddItem(ItemID.LivingLoom, (0, 0, 10, 0)) //生命织布机
            .AddItem(ItemID.GlassKiln, (0, 0, 5, 0)) //玻璃窑
            .AddItem(ItemID.SkyMill, (0, 1, 0, 0)) //天磨
            .AddItem(ItemID.IceMachine, (0, 0, 5, 0)) //冰雪机
            .AddItem(ItemID.HoneyDispenser, (0, 1, 0, 0)) //蜂蜜分配器
            .AddItem(ItemID.Sawmill, (0, 0, 10, 0)) //锯木机
            .AddItem(ItemID.Loom, (0, 0, 5, 0)) //织布机
            .AddItem(ItemID.Keg, (0, 0, 5, 0)) //酒桶
            .AddItem(ItemID.AlchemyTable, (0, 1, 0, 0), Condition.DownedSkeletron) //炼药桌
            .AddItem(ItemID.BoneWelder, (0, 1, 0, 0), Condition.DownedSkeletron) //骨头焊机
            .AddItem(ItemID.MeatGrinder, (0, 2, 0, 0), Condition.Hardmode) //绞肉机
            .AddItem(ItemID.FleshCloningVaat, (0, 2, 0, 0), Condition.DownedMechBossAny) //血肉克隆台
            .AddItem(ItemID.LesionStation, (0, 2, 0, 0), Condition.DownedMechBossAny) //病变站(腐变室)
            .AddItem(ItemID.LihzahrdFurnace, (0, 3, 0, 0), Condition.DownedPlantera) //丛林蜥蜴熔炉
            .AddItem(ItemID.LunarCraftingStation, (0, 10, 0, 0), Condition.DownedCultist) //远古操纵机
            //关于建筑的饰品
            .AddItem(ItemID.PortableStool, (0, 1, 0, 0)) //便携凳(梯凳)
            .AddItem(ItemID.AncientChisel, (0, 5, 0, 0)) //远古凿子
            .AddItem(ItemID.BrickLayer, (0, 5, 0, 0)) //砌砖刀
            .AddItem(ItemID.ExtendoGrip, (0, 5, 0, 0)) //加长握爪
            .AddItem(ItemID.PaintSprayer, (0, 5, 0, 0)) //喷漆器
            .AddItem(ItemID.PortableCementMixer, (0, 5, 0, 0)) //便携式水泥搅拌机
            .AddItem(ItemID.TreasureMagnet, (0, 5, 0, 0), Condition.DownedSkeletron); //宝藏磁石

            #endregion

            #region [模组物块]

            #region 灾厄 Calamity Mod
            buildingB.AddModItem("CalamityMod/Acidwood", (0, 0, 0, 10)) //酸蚀木
            .AddModItem("CalamityMod/ScorchedBone", (0, 0, 0, 10), Condition.DownedEowOrBoc) //焦灼脊骨
            .AddModItem("CalamityMod/AstralMonolith", (0, 0, 0, 50), Condition.Hardmode); //星幻木材

            #endregion

            #region Fargo突变 Fargo's Mutant Mod
            buildingB.AddModItem("Fargowiltas/Semistation", (0, 2, 0, 0)) //多功能增益站
            .AddModItem("Fargowiltas/Omnistation", (0, 5, 0, 0), Condition.Hardmode) //万能增益站
            .AddModItem("Fargowiltas/Omnistation2", (0, 5, 0, 0), Condition.Hardmode) //万能增益站(配方不同而贴图不同)
            .AddModItem("Fargowiltas/GoldenDippingVat", (0, 10, 0, 0), Condition.Hardmode) //金染缸
            .AddModItem("Fargowiltas/MultitaskCenter", (0, 20, 0, 0)) //多功能合成桌
            .AddModItem("Fargowiltas/ElementalAssembler", (0, 20, 0, 0), QueenBeeAndSkeletron); //元素装配台

            #endregion

            #region 瑟银 Thorium Mod
            buildingB.AddModItem("ThoriumMod/Deadwood", (0, 0, 0, 10)) //枯木
            .AddModItem("ThoriumMod/YewWood", (0, 0, 0, 10)) //紫衫木
            .AddModItem("ThoriumMod/EvergreenBlock", (0, 0, 0, 10)); //常青木
            #endregion

            #endregion

            #endregion

            naturalB.Register();
            buildingB.Register();
        }
    }
}
