using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Content.Items.Potions;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class PotionNPC : ModNPC
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
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.PotionNPC")
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return numTownNPCs > 4;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                this.GetLocalizedValue("Name.PotionNPC")
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
            button = Language.GetTextValue("Mods.LaziestNPC.NPCs.PotionNPC.button1");
            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.PotionNPC.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "VanillaPotions";
            }
            else
            {
                shopName = "ModPotions";
            }
        }

        public override void AddShops()
        {
            var vPotion = new NPCShop(Type, "VanillaPotions");
            var mPotion = new NPCShop(Type, "ModPotions");

            #region 【原版药水】

            #region [恢复类药水]

            #region 治疗药水
            //肉前
            vPotion.AddItem(ItemID.BottledWater, (0, 0, 0, 5)) //瓶装水 30
                .AddItem(ItemID.LesserHealingPotion, (0, 0, 1, 0)) //弱效治疗药水 50
                .AddItem(ItemID.StrangeBrew, (0, 0, 15, 0)) //诡药 70-120
                .AddItem(ItemID.BottledHoney, (0, 0, 5, 0)) //瓶装蜂蜜 80
                .AddItem(ItemID.Eggnog, (0, 0, 5, 0)) //蛋酒 80
                .AddItem(ItemID.RestorationPotion, (0, 0, 15, 0)) //恢复药水 90
                .AddItem(ItemID.HealingPotion, (0, 0, 5, 0)) //治疗药水 100
                .AddItem(ItemID.Honeyfin, (0, 0, 20, 0)) //蜂蜜鱼 120
            //肉后
                .AddItem(ItemID.GreaterHealingPotion, (0, 0, 50, 0), Condition.Hardmode) //强效治疗药水 150
                //.AddItem(ItemID.LifeFruitHealingPotion, (0, 1, 0, 0), Condition.Hardmode) //丛林果汁 180
                .AddItem(ItemID.SuperHealingPotion, (0, 1, 25, 0), Condition.DownedCultist); //超级治疗药水 200

            #endregion

            #region 魔力药水
            vPotion.AddItem(ItemID.LesserManaPotion, (0, 0, 0, 75)) //弱效魔力药水 50
                .AddItem(ItemID.ManaPotion, (0, 0, 5, 0)) //魔力药水 100
                .AddItem(ItemID.GreaterManaPotion, (0, 0, 50, 0), Condition.Hardmode) //强效魔力药水 200
                .AddItem(ItemID.SuperManaPotion, (0, 0, 75, 0), Condition.Hardmode); //超级魔力药水 300
                                                                                     //.AddItem(ItemID.SuperManaPotion, (0, 1, 25, 0), Condition.DownedPlantera); //超级魔力药水 400

            #endregion

            #endregion

            #region [实用类药水]
            vPotion.AddItem(ItemID.NightOwlPotion, (0, 0, 50, 0)) //夜视药水
                .AddItem(ItemID.ShinePotion, (0, 0, 50, 0)) //光芒药水
                .AddItem(ItemID.SpelunkerPotion, (0, 0, 50, 0)) //洞穴探险药水
                .AddItem(ItemID.TrapsightPotion, (0, 0, 50, 0)) //危险感知药水
                .AddItem(ItemID.HunterPotion, (0, 0, 50, 0)) //狩猎药水
                .AddItem(ItemID.ObsidianSkinPotion, (0, 0, 50, 0)) //黑曜石皮药水
                .AddItem(ItemID.MiningPotion, (0, 0, 50, 0)) //挖矿药水
                .AddItem(ItemID.BuilderPotion, (0, 0, 50, 0)) //建筑工药水
                .AddItem(ItemID.GravitationPotion, (0, 0, 50, 0)) //重力药水
                .AddItem(ItemID.FeatherfallPotion, (0, 0, 50, 0)) //羽落药水
                .AddItem(ItemID.WaterWalkingPotion, (0, 0, 50, 0)) //水上漂药水
                .AddItem(ItemID.GillsPotion, (0, 0, 50, 0)) //鱼鳃药水
                .AddItem(ItemID.FlipperPotion, (0, 0, 50, 0)) //脚蹼药水
                .AddItem(ItemID.FishingPotion, (0, 0, 50, 0)) //钓鱼药水
                .AddItem(ItemID.SonarPotion, (0, 0, 50, 0)) //声纳药水
                .AddItem(ItemID.CratePotion, (0, 0, 50, 0)) //宝匣药水
                .AddItem(ItemID.LuckPotionLesser, (0, 0, 50, 0)) //弱效幸运药水
                .AddItem(ItemID.LuckPotion, (0, 1, 0, 0)) //幸运药水
                .AddItem(ItemID.LuckPotionGreater, (0, 2, 0, 0)) //强效幸运药水
                .AddItem(ItemID.InvisibilityPotion, (0, 0, 50, 0)) //隐身药水
                .AddItem(ItemID.GenderChangePotion, (0, 1, 0, 0)) //变性药水
                .AddItem(ItemID.StinkPotion, (0, 0, 50, 0)) //臭味药水
                .AddItem(ItemID.BiomeSightPotion, (0, 0, 50, 0)) //生物群系视觉药水
                .AddItem(ItemID.RecallPotion, (0, 0, 50, 0)) //回忆药水
                .AddItem(ItemID.PotionOfReturn, (0, 0, 80, 0)) //返回药水
                .AddItem(ItemID.WormholePotion, (0, 0, 50, 0)) //虫洞药水
                .AddItem(ItemID.TeleportationPotion, (0, 0, 50, 0)) //传送药水
                .AddItem(ItemID.RedPotion, (0, 2, 0, 0)) //红药水
                .AddItem(ItemID.LovePotion, (0, 1, 0, 0), Condition.Hardmode); //爱情药水

            #endregion

            #region [战斗类药水]
            vPotion.AddItem(ItemID.RegenerationPotion, (0, 0, 50, 0)) //再生药水
                .AddItem(ItemID.SwiftnessPotion, (0, 0, 50, 0)) //敏捷药水
                .AddItem(ItemID.IronskinPotion, (0, 0, 50, 0)) //铁皮药水
                .AddItem(ItemID.AmmoReservationPotion, (0, 0, 50, 0)) //弹药储备药水
                .AddItem(ItemID.ArcheryPotion, (0, 0, 50, 0)) //箭术药水
                .AddItem(ItemID.ManaRegenerationPotion, (0, 0, 50, 0)) //魔力再生药水
                .AddItem(ItemID.MagicPowerPotion, (0, 0, 50, 0)) //魔能药水
                .AddItem(ItemID.SummoningPotion, (0, 0, 50, 0)) //召唤药水
                .AddItem(ItemID.HeartreachPotion, (0, 0, 50, 0)) //拾心药水
                .AddItem(ItemID.EndurancePotion, (0, 0, 50, 0)) //耐力药水
                .AddItem(ItemID.ThornsPotion, (0, 0, 50, 0)) //荆棘药水
                .AddItem(ItemID.RagePotion, (0, 0, 50, 0)) //怒气药水
                .AddItem(ItemID.WrathPotion, (0, 0, 50, 0)) //暴怒药水
                .AddItem(ItemID.InfernoPotion, (0, 0, 50, 0)) //狱火药水
                .AddItem(ItemID.TitanPotion, (0, 0, 50, 0)) //泰坦药水
                .AddItem(ItemID.WarmthPotion, (0, 0, 50, 0)) //保暖药水
                .AddItem(ItemID.BattlePotion, (0, 0, 50, 0)) //战斗药水
                .AddItem(ItemID.CalmingPotion, (0, 0, 50, 0)) //镇静药水
                .AddItem(ItemID.LifeforcePotion, (0, 1, 0, 0), Condition.DownedSkeletron) //生命力药水
                //特殊
                .AddItem(ItemID.FlaskofFire, (0, 0, 50, 0), QueenBeeAndEowOrBoc) //烈火药剂
                .AddItem(ItemID.FlaskofPoison, (0, 0, 50, 0), Condition.DownedQueenBee) //毒药剂
                .AddItem(ItemID.FlaskofIchor, (0, 1, 50, 0), Condition.Hardmode) //灵液药剂
                .AddItem(ItemID.FlaskofCursedFlames, (0, 1, 50, 0), Condition.Hardmode) //诅咒焰药剂
                .AddItem(ItemID.FlaskofVenom, (0, 1, 50, 0), Condition.DownedPlantera); //毒液药剂
            #endregion

            #endregion

            #region 【模组药水】

            #region [恢复类药水]

            #region 本模组独立
            mPotion.AddItem(ItemType<HadalStew>(), (0, 0, 75, 0)); //乱渊炖 120 150

            #endregion

            #region 灾厄 Calamity Mod
            mPotion.AddModItem("CalamityMod/Bloodfin", (0, 1, 50, 0), DownedProvidence) //血鳍 240
            .AddModItem("CalamityMod/SupremeHealingPotion", (0, 1, 50, 0), DownedProvidence) //至尊治疗药水 250
            .AddModItem("CalamityMod/OmegaHealingPotion", (0, 2, 0, 0), DownedDOG) //终极治疗药水 300

            .AddModItem("CalamityMod/SupremeManaPotion", (0, 1, 0, 0), Condition.DownedMoonLord); //至尊魔力药水 400

            #endregion

            #region 瑟银 Thorium Mod
            mPotion.AddModItem("ThoriumMod/SpringWater", (0, 0, 50, 0)) //泉水 125
            .AddModItem("ThoriumMod/Jelly", (0, 0, 25, 0), Condition.DownedSkeletron) //果酱 75
            .AddModItem("ThoriumMod/MidnightOil", (0, 0, 25, 0), Condition.DownedSkeletron) //午夜之油 75

            .AddModItem("ThoriumMod/LifeWater", (0, 0, 50, 0), Condition.Hardmode); //生命之水 125

            #endregion

            #endregion

            #region [战斗类/实用类药水]

            #region 本模组独立
            mPotion.AddItem(ItemType<TriumphPotion>(), (0, 0, 75, 0), KingSlimeOrDesertBug) //胜利药水
            .AddItem(ItemType<YharimsStimulants>(), (0, 1, 0, 0), Condition.DownedSkeletron) //魔君牌兴奋剂

            .AddItem(ItemType<CadencePotion>(), (0, 1, 25, 0), Condition.Hardmode) //韵律药水
            .AddItem(ItemType<RevivifyPotion>(), (0, 1, 25, 0), Condition.Hardmode) //新生药水
            .AddItem(ItemType<PenumbraPotion>(), (0, 1, 25, 0), Condition.DownedPlantera) //半影药水
            .AddItem(ItemType<ShatteringPotion>(), (0, 1, 25, 0), Condition.DownedGolem) //粉碎药水
            .AddItem(ItemType<TitanScalePotion>(), (0, 1, 25, 0), Condition.DownedGolem) //泰坦之鳞药水
            .AddItem(ItemType<ProfanedRagePotion>(), (0, 1, 50, 0), Condition.DownedCultist) //渎神之怒药水

            .AddItem(ItemType<DraconicElixir>(), (0, 2, 0, 0), Condition.DownedMoonLord); //龙魂秘药
            #endregion

            #region 灾厄 Calamity Mod
            //[实用类药水] 模组药水都放在一起
            mPotion.AddModItem("CalamityMod/ZenPotion", (0, 1, 0, 0)) //禅定药水
            .AddModItem("CalamityMod/ZergPotion", (0, 1, 0, 0)) //虫潮药水
            .AddModItem("CalamityMod/PotionofOmniscience", (0, 0, 75, 0)) //全知药水
            .AddModItem("CalamityMod/AnechoicCoating", (0, 0, 50, 0)) //吸音涂层
            //肉前
            .AddModItem("CalamityMod/BoundingPotion", (0, 0, 50, 0)) //弹跳药水
            .AddModItem("CalamityMod/CalciumPotion", (0, 0, 50, 0)) //钙质药水
            .AddModItem("CalamityMod/SulphurskinPotion", (0, 0, 50, 0)) //硫磺皮肤药水
            .AddModItem("CalamityMod/ShadowPotion", (0, 0, 75, 0)) //暗影药水
            //肉后
            .AddModItem("CalamityMod/PhotosynthesisPotion", (0, 1, 0, 0), Condition.Hardmode) //光合药水
            .AddModItem("CalamityMod/SoaringPotion", (0, 1, 0, 0), Condition.Hardmode) //腾飞药水
            .AddModItem("CalamityMod/GravityNormalizerPotion", (0, 1, 25, 0), DownedAstrum) //重力复原药水
            .AddModItem("CalamityMod/AstralInjection", (0, 1, 25, 0), DownedAstrum) //星幻注射剂
            //月后
            .AddModItem("CalamityMod/CeaselessHungerPotion", (0, 1, 50, 0), DownedAstrum) //无尽吞噬药水
            //武器灌注瓶
            .AddModItem("CalamityMod/FlaskOfCrumbling", (0, 1, 0, 0), Condition.Hardmode) //粉碎瓶
            .AddModItem("CalamityMod/FlaskOfBrimstone", (0, 1, 0, 0), DownedCalamitas) //硫火瓶
            .AddModItem("CalamityMod/FlaskOfHolyFlames", (0, 1, 50, 0), Condition.DownedMoonLord); //圣火瓶

            #endregion

            #region 瑟银 Thorium Mod
            //肉前
            mPotion.AddModItem("ThoriumMod/AquaPotion", (0, 0, 50, 0)) //潜水药水
            .AddModItem("ThoriumMod/ArtilleryPotion", (0, 0, 50, 0)) //集群火炮药水
            .AddModItem("ThoriumMod/AssassinPotion", (0, 0, 50, 0)) //刺客药水
            .AddModItem("ThoriumMod/BouncingFlamePotion", (0, 0, 50, 0)) //弹跳火焰药水
            .AddModItem("ThoriumMod/ConflagrationPotion", (0, 0, 50, 0)) //燃炎药水
            .AddModItem("ThoriumMod/EarwormPotion", (0, 0, 50, 0)) //耳虫药水
            .AddModItem("ThoriumMod/FrenzyPotion", (0, 0, 50, 0)) //狂怒药水
            .AddModItem("ThoriumMod/GlowingPotion", (0, 0, 50, 0)) //光辉药水
            .AddModItem("ThoriumMod/HydrationPotion", (0, 0, 50, 0)) //补水药水
            .AddModItem("ThoriumMod/BloodPotion", (0, 0, 50, 0), Condition.BloodMoon) //堕血药水
            .AddModItem("ThoriumMod/CreativityPotion", (0, 0, 75, 0), DownedGrandThunderBird) //创意药水
            .AddModItem("ThoriumMod/WarmongerPotion", (0, 0, 75, 0), Condition.DownedEowOrBoc) //好战药水
            //肉后
            .AddModItem("ThoriumMod/ArcanePotion", (0, 1, 0, 0), Condition.Hardmode) //奥术药水
            .AddModItem("ThoriumMod/HolyPotion", (0, 1, 0, 0), Condition.Hardmode) //圣洁药水
            .AddModItem("ThoriumMod/InspirationReachPotion", (0, 1, 0, 0), Condition.Hardmode) //灵感之触药水
            .AddModItem("ThoriumMod/KineticPotion", (0, 1, 0, 0), Condition.Hardmode) //动能药水
            //特殊
            .AddModItem("ThoriumMod/DeepFreezeCoatingItem", (0, 0, 50, 0), Condition.DownedQueenBee) //深寒涂层
            .AddModItem("ThoriumMod/SporeCoatingItem", (0, 0, 50, 0), Condition.DownedQueenBee) //孢子涂层
            .AddModItem("ThoriumMod/ToxicCoatingItem", (0, 0, 50, 0), Condition.DownedQueenBee) //剧毒涂层
            .AddModItem("ThoriumMod/ExplosiveCoatingItem", (0, 0, 50, 0), QueenBeeAndEowOrBoc) //爆炸涂层
            .AddModItem("ThoriumMod/GorgonCoatingItem", (0, 0, 75, 0), QueenBeeAndSkeletron) //石化涂层
            
            .AddModItem("ThoriumMod/BatRepellent", (0, 1, 0, 0), DownedPatchWerk) //蝙蝠趋避剂
            .AddModItem("ThoriumMod/FishRepellent", (0, 1, 0, 0), DownedPatchWerk) //鱼类趋避剂
            .AddModItem("ThoriumMod/InsectRepellent", (0, 1, 0, 0), DownedPatchWerk) //昆虫趋避剂
            .AddModItem("ThoriumMod/SkeletonRepellent", (0, 1, 0, 0), DownedPatchWerk) //骷髅趋避剂
            .AddModItem("ThoriumMod/ZombieRepellent", (0, 1, 0, 0), DownedPatchWerk); //僵尸趋避剂

            #endregion

            #endregion

            #endregion


            vPotion.Register();
            mPotion.Register();
        }
    }
}
