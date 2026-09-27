using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Utilities;
using LaziestNPC.Globals.GlobalItems;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;
using LaziestNPC.Common.ModBossess;
using LaziestNPC.Content.Items.SummonItems;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class MaterialNPC : ModNPC
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
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.MaterialNPC")
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
                this.GetLocalizedValue("Name.MaterialNPC")
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
            button = Language.GetTextValue("Mods.LaziestNPC.NPCs.MaterialNPC.button1");
            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.MaterialNPC.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "VanillaMaterials";
            }
            else
            {
                shopName = "ModMaterials";
            }
        }

        public override void AddShops()
        {
            var vMat = new NPCShop(Type, "VanillaMaterials");
            var modMat = new NPCShop(Type, "ModMaterials");

            #region 【原版材料】
            //肉前
            vMat.AddItem(ItemID.Bottle, (0, 0, 0, 10)) //玻璃瓶
            .AddItem(ItemID.Daybloom, (0, 0, 2, 0)) //太阳花
            .AddItem(ItemID.Moonglow, (0, 0, 2, 0)) //月光草
            .AddItem(ItemID.Blinkroot, (0, 0, 2, 0)) //闪耀根
            .AddItem(ItemID.Waterleaf, (0, 0, 2, 0)) //水叶草
            .AddItem(ItemID.Deathweed, (0, 0, 2, 0)) //死亡草
            .AddItem(ItemID.Shiverthorn, (0, 0, 2, 0)) //寒颤棘
            .AddItem(ItemID.Fireblossom, (0, 0, 2, 0)) //火焰花
            .AddItem(ItemID.HerbBag, (0, 0, 10, 0)) //草药袋
            .AddItem(ItemID.Mushroom, (0, 0, 1, 0)) //蘑菇
            .AddItem(ItemID.GlowingMushroom, (0, 0, 1, 50)) //发光蘑菇
            .AddItem(ItemID.ViciousMushroom, (0, 0, 3, 0)) //毒蘑菇
            .AddItem(ItemID.VileMushroom, (0, 0, 3, 0)) //魔菇
            .AddItem(ItemID.Coral, (0, 0, 3, 0)) //珊瑚
            .AddItem(ItemID.Seashell, (0, 0, 3, 0)) //贝壳
            .AddItem(ItemID.Starfish, (0, 0, 3, 0)) //海星
            .AddItem(ItemID.FallenStar, (0, 0, 25, 0)) //坠落之星
            .AddItem(ItemID.Bass, (0, 1, 50, 0)) //鲈鱼
            .AddItem(ItemID.Gel, (0, 0, 0, 10)) //凝胶
            .AddItem(ItemID.PinkGel, (0, 0, 8, 0)) //粉凝胶
            .AddItem(ItemID.Lens, (0, 0, 0, 75)) //晶状体
            .AddItem(ItemID.BlackLens, (0, 1, 0, 0)) //黑晶状体
            .AddItem(ItemID.Cobweb, (0, 0, 1, 50)) //蛛网
            .AddItem(ItemID.FlinxFur, (0, 0, 5, 0)) //小雪怪皮毛
            .AddItem(ItemID.JungleSpores, (0, 0, 5, 0)) //丛林孢子
            .AddItem(ItemID.Stinger, (0, 0, 5, 0)) //毒刺
            .AddItem(ItemID.Vine, (0, 0, 5, 0)) //藤蔓
            .AddItem(ItemID.Feather, (0, 0, 5, 0)) //羽毛
            .AddItem(ItemID.AntlionMandible, (0, 0, 2, 0)) //蚁狮上颚
            .AddItem(ItemID.RottenChunk, (0, 0, 2, 0)) //腐肉
            .AddItem(ItemID.Vertebrae, (0, 0, 2, 0)) //椎骨
            .AddItem(ItemID.WormTooth, (0, 0, 2, 0)) //蠕虫牙齿
            .AddItem(ItemID.SharkFin, (0, 0, 3, 0)) //鲨鱼鳍
            .AddItem(ItemID.TatteredCloth, (0, 0, 5, 0)) //破布
            .AddItem(ItemID.TissueSample, (0, 0, 8, 0), Condition.DownedEowOrBoc) //组织样本
            .AddItem(ItemID.ShadowScale, (0, 0, 8, 0), Condition.DownedEowOrBoc) //暗影鳞片
            .AddItem(ItemID.Bone, (0, 0, 8, 50), Condition.DownedSkeletron) //骨头

            .AddItem(ItemID.Present, (0, 1, 50, 0)) //礼物
            .AddItem(ItemID.GoodieBag, (0, 1, 50, 0)) //礼袋
            //肉后
            .AddItem(ItemID.SoulofLight, (0, 0, 10, 0), Condition.Hardmode) //光明之魂
            .AddItem(ItemID.SoulofNight, (0, 0, 10, 0), Condition.Hardmode) //暗影之魂
            .AddItem(ItemID.SoulofFlight, (0, 0, 10, 0), Condition.Hardmode) //飞翔之魂
            .AddItem(ItemID.CursedFlame, (0, 0, 10, 0), Condition.Hardmode) //诅咒焰
            .AddItem(ItemID.Ichor, (0, 0, 10, 0), Condition.Hardmode) //灵液
            .AddItem(ItemID.DarkShard, (0, 0, 25, 0), Condition.Hardmode) //暗影碎块
            .AddItem(ItemID.LightShard, (0, 0, 25, 0), Condition.Hardmode) //光明碎块
            .AddItem(ItemID.AncientCloth, (0, 0, 25, 0), Condition.Hardmode) //远古布匹
            .AddItem(ItemID.PixieDust, (0, 0, 10, 0), Condition.Hardmode) //妖精尘
            .AddItem(ItemID.UnicornHorn, (0, 0, 10, 0), Condition.Hardmode) //独角兽角
            .AddItem(ItemID.CrystalShard, (0, 0, 10, 0), Condition.Hardmode) //水晶碎块
            .AddItem(ItemID.SpiderFang, (0, 0, 25, 0), Condition.Hardmode) //蜘蛛牙
            .AddItem(ItemID.FrostCore, (0, 1, 0, 0), Condition.Hardmode) //寒霜核
            .AddItem(ItemID.AncientBattleArmorMaterial, (0, 1, 0, 0), Condition.Hardmode) //禁戒碎片
            .AddItem(ItemID.TurtleShell, (0, 1, 0, 0), Condition.Hardmode) //海龟壳
            .AddItem(ItemID.RodofDiscord, (8, 0, 0, 0), Condition.Hardmode) //混沌传送杖
            .AddItem(ItemID.SoulofMight, (0, 1, 50, 0), Condition.DownedDestroyer) //力量之魂
            .AddItem(ItemID.SoulofSight, (0, 1, 50, 0), Condition.DownedTwins) //视域之魂
            .AddItem(ItemID.SoulofFright, (0, 1, 50, 0), Condition.DownedSkeletronPrime) //恐惧之魂
            .AddItem(ItemID.HallowedBar, (0, 1, 50, 0), Condition.DownedMechBossAny) //神圣锭
            .AddItem(ItemID.ButterflyDust, (0, 1, 50, 0), Condition.DownedMechBossAny) //蝴蝶尘
            .AddItem(ItemID.Ectoplasm, (0, 2, 0, 0), Condition.DownedPlantera) //灵质
            .AddItem(ItemID.BrokenHeroSword, (0, 5, 0, 0), Condition.DownedPlantera) //断裂英雄剑
            .AddItem(ItemID.LunarTabletFragment, (0, 2, 0, 0), Condition.DownedPlantera); //日耀碑牌碎片
            if (ModLoader.HasMod("CalamityMod"))
            {
                vMat.AddItem(ItemID.FragmentSolar, (0, 2, 50, 0), Condition.DownedCultist) //日耀碎片
                .AddItem(ItemID.FragmentVortex, (0, 2, 50, 0), Condition.DownedCultist) //星旋碎片
                .AddItem(ItemID.FragmentNebula, (0, 2, 50, 0), Condition.DownedCultist) //星云碎片
                .AddItem(ItemID.FragmentStardust, (0, 2, 50, 0), Condition.DownedCultist); //星尘碎片
            }
            else
            {
                vMat.AddItem(ItemID.FragmentSolar, (0, 2, 50, 0), Condition.DownedSolarPillar) //日耀碎片
                .AddItem(ItemID.FragmentVortex, (0, 2, 50, 0), Condition.DownedVortexPillar) //星旋碎片
                .AddItem(ItemID.FragmentNebula, (0, 2, 50, 0), Condition.DownedNebulaPillar) //星云碎片
                .AddItem(ItemID.FragmentStardust, (0, 2, 50, 0), Condition.DownedStardustPillar); //星尘碎片
            }

            #endregion

            #region 【模组材料】

            #region [灾厄 Calamity Mod]
            //肉前
            modMat.AddModItem("CalamityMod/WulfrumMetalScrap", (0, 0, 5, 0)) //钨钢金属废料
            .AddModItem("CalamityMod/EnergyCore", (0, 0, 10, 0)) //能量核心
            .AddModItem("CalamityMod/DubiousPlating", (0, 0, 10, 0)) //可疑镀层
            .AddModItem("CalamityMod/MysteriousCircuitry", (0, 0, 10, 0)) //神秘电路
            .AddModItem("CalamityMod/StormlionMandible", (0, 0, 15, 0)) //风暴之颚
            .AddModItem("CalamityMod/BlightedGel", (0, 0, 15, 0)) //枯萎凝胶
            .AddModItem("CalamityMod/AncientBoneDust", (0, 0, 15, 0)) //上古骨灰
            .AddModItem("CalamityMod/BloodOrb", (0, 0, 50, 0)) //血珠
            .AddModItem("CalamityMod/SulphuricScale", (0, 0, 50, 0), Condition.DownedEyeOfCthulhu) //硫磺鳞片
            .AddModItem("CalamityMod/PearlShard", (0, 0, 25, 0), DownedDesertBug) //珍珠碎片
            .AddModItem("CalamityMod/PurifiedGel", (0, 0, 75, 0), DownedSlimeGod) //纯净凝胶
            //肉后
            .AddModItem("CalamityMod/MolluskHusk", (0, 1, 0, 0), Condition.Hardmode) //软体动物外壳
            .AddModItem("CalamityMod/EssenceofEleum", (0, 1, 0, 0), Condition.Hardmode) //冰川精华
            .AddModItem("CalamityMod/EssenceofSunlight", (0, 1, 0, 0), Condition.Hardmode) //日光精华
            .AddModItem("CalamityMod/EssenceofHavoc", (0, 1, 0, 0), Condition.Hardmode) //混乱精华
            .AddModItem("CalamityMod/StarblightSoot", (0, 1, 0, 0), Condition.Hardmode) //星尘
            .AddModItem("CalamityMod/CorrodedFossil", (0, 1, 50, 0), DownedAquaticBug) //酸腐化石
            .AddModItem("CalamityMod/LivingShard", (0, 2, 0, 0), Condition.DownedPlantera) //生命碎片
            .AddModItem("CalamityMod/SolarVeil", (0, 2, 0, 0), DownedCalamitas) //日影面纱
            .AddModItem("CalamityMod/DepthCells", (0, 2, 0, 0), DownedLeviathan) //深渊细胞
            .AddModItem("CalamityMod/Lumenyl", (0, 2, 0, 0), DownedLeviathan) //流明晶
            .AddModItem("CalamityMod/AureusCell", (0, 2, 0, 0), DownedAstrum) //星舰电池
            .AddModItem("CalamityMod/PlagueCellCanister", (0, 2, 25, 0), Condition.DownedGolem) //瘟疫细胞罐
            .AddModItem("CalamityMod/InfectedArmorPlating", (0, 2, 25, 0), DownedPlague) //瘟疫装甲镀层
            .AddModItem("CalamityMod/MeldBlob", (0, 2, 50, 0), Condition.DownedCultist) //冥思溶剂
            //月后
            .AddModItem("CalamityMod/UnholyEssence", (0, 2, 75, 0), Condition.DownedMoonLord) //浊火精华
            .AddModItem("CalamityMod/Necroplasm", (0, 2, 75, 0), Condition.DownedMoonLord) //灵质
            .AddModItem("CalamityMod/EffulgentFeather", (0, 3, 0, 0), DownedDragonfolly) //闪耀金羽
            .AddModItem("CalamityMod/DivineGeode", (0, 3, 50, 0), DownedProvidence) //神圣晶石
            .AddModItem("CalamityMod/Bloodstone", (0, 3, 50, 0), DownedProvidence) //血石
            .AddModItem("CalamityMod/ArmoredShell", (0, 3, 75, 0), DownedStormWeaver) //装甲外壳
            .AddModItem("CalamityMod/DarkPlasma", (0, 3, 75, 0), DownedVoid) //暗离子体
            .AddModItem("CalamityMod/TwistingNether", (0, 3, 75, 0), DownedSignus) //扭曲虚空
            .AddModItem("CalamityMod/RuinousSoul", (0, 4, 0, 0), DownedPolterghast) //毁灭之灵
            .AddModItem("CalamityMod/ReaperTooth", (0, 4, 0, 0), DownedPolterghast) //猎魂鲨牙
            .AddModItem("CalamityMod/CosmiliteBar", (0, 4, 50, 0), DownedDOG) //宇宙锭
            .AddModItem("CalamityMod/EndothermicEnergy", (0, 4, 50, 0), DOGAndSnowMoon) //恒温能量
            .AddModItem("CalamityMod/NightmareFuel", (0, 4, 50, 0), DOGAndPumpkinMoon) //梦魇魔能
            .AddModItem("CalamityMod/DarksunFragment", (0, 4, 50, 0), DOGAndEclipse) //日蚀之阴碎片
            .AddModItem("CalamityMod/YharonSoulFragment", (0, 5, 0, 0), DownedYharon) //龙魂碎片
            .AddModItem("CalamityMod/ExoPrism", (0, 5, 50, 0), DownedDraedon) //星流棱晶
            .AddModItem("CalamityMod/AshesofAnnihilation", (0, 6, 0, 0), DownedSCala); //湮灭余烬

            #endregion

            #region [灾劫 Catalyst Mod]
            modMat.AddModItem("CatalystMod/AstraJelly", (0, 1, 0, 0), Condition.Hardmode); //幻星凝露

            #endregion

            #region [瑟银 Thorium Mod]
            //肉前
            modMat.AddModItem("ThoriumMod/LivingLeaf", (0, 0, 5, 0)) //生命之叶
            .AddModItem("ThoriumMod/IcyShard", (0, 0, 5, 0)) //冰碎片
            .AddModItem("ThoriumMod/Petal", (0, 0, 5, 0)) //花瓣
            .AddModItem("ThoriumMod/Talon", (0, 0, 10, 0)) //鸟爪
            .AddModItem("ThoriumMod/DepthScale", (0, 0, 10, 0)) //深海之鳞
            .AddModItem("ThoriumMod/Blood", (0, 0, 25, 0)) //血
            .AddModItem("ThoriumMod/UnholyShards", (0, 0, 25, 0), BloodMoonHappened) //不洁碎晶
            .AddModItem("ThoriumMod/StrangeAlienTech", (0, 0, 50, 0), Condition.DownedEowOrBoc) //奇怪的外星人科技
            .AddModItem("ThoriumMod/GraniteEnergyCore", (0, 0, 50, 0), Condition.DownedSkeletron) //花岗岩能源核心
            .AddModItem("ThoriumMod/BronzeAlloyFragments", (0, 0, 50, 0), Condition.DownedSkeletron) //青铜合金碎片
            .AddModItem("ThoriumMod/SpiritDroplet", (0, 0, 75, 0), Condition.DownedSkeletron) //末灵
            //肉后
            .AddModItem("ThoriumMod/SoulofPlight", (0, 1, 0, 0), Condition.Hardmode) //困境之魂
            .AddModItem("ThoriumMod/CeruleanMorel", (0, 1, 0, 0), Condition.Hardmode) //蔚蓝真菌
            .AddModItem("ThoriumMod/PharaohsBreath", (0, 1, 0, 0), Condition.Hardmode) //法老之息
            .AddModItem("ThoriumMod/BioMatter", (0, 1, 0, 0), Condition.Hardmode) //生物物质
            .AddModItem("ThoriumMod/AbyssalChitin", (0, 1, 0, 0), Condition.Hardmode) //深渊角质鳞
            .AddModItem("ThoriumMod/BloodCell", (0, 1, 25, 0), HMAndBloodMoon) //血细胞
            .AddModItem("ThoriumMod/BrokenHeroFragment", (0, 2, 0, 0), Condition.DownedPlantera) //残缺英雄碎片
            .AddModItem("ThoriumMod/SolarPebble", (0, 2, 0, 0), Condition.DownedPlantera) //太阳石砾
            .AddModItem("ThoriumMod/DarkMatter", (0, 2, 0, 0), Condition.DownedPlantera) //暗物质
            .AddModItem("ThoriumMod/HolyKnightsAlloy", (0, 2, 0, 0), Condition.DownedPlantera) //圣骑士合金
            .AddModItem("ThoriumMod/Permafrost", (0, 2, 25, 0), SnowMoonHappened) //永冻土
            .AddModItem("ThoriumMod/WhiteDwarfFragment", (0, 2, 50, 0), DownedAnyPillar) //白矮星碎片
            .AddModItem("ThoriumMod/CelestialFragment", (0, 2, 50, 0), DownedAnyPillar) //天界碎片
            .AddModItem("ThoriumMod/ShootingStarFragment", (0, 2, 50, 0), DownedAnyPillar); //流影掠星碎片

            #endregion

            #endregion

            vMat.Register();
            modMat.Register();
        }
    }
}
