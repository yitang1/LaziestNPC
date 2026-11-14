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
using LaziestNPC.Globals.Config;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.UI;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class PotionNPC : TownLNPC
    {
        protected override string BestiaryText => "Mods.LaziestNPC.Bestiary.PotionNPC";
        protected override string NPCNameText => "Name.PotionNPC";
        protected override int AttackProjType => ProjectileID.LostSoulFriendly;
        protected override int AttackProjDamage => 2;

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
            return numTownNPCs > 5 && LNPCConfig.Instance.LNPCPotionNPC;
        }

        public override string GetChat()
        {
            if (Main.rand.Next(100) < 70)
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                chat.Add(this.GetLocalizedValue("Chat.Normal1"));

                chat.Add(this.GetGenderValue("Chat.Normal2"));
                chat.Add(this.GetGenderValue("Chat.Normal3"));

                chat.Add(this.GetLocalizedValue("Chat.Normal4"));
                chat.Add(this.GetLocalizedValue("Chat.Normal5"));

                if (!Main.dayTime)
                {
                    chat.Add(this.GetLocalizedValue("Chat.Night0"));
                    chat.Add(this.GetLocalizedValue("Chat.Night1"));
                }

                chat.Add(this.GetLocalizedValue("Chat.Normal6"), 0.2);
                chat.Add(this.GetLocalizedValue("Chat.Normal7"), 0.1);
                chat.Add(this.GetLocalizedValue("Chat.Normal8"), 0.1);


                return chat.Get();

            }
            else
            {
                WeightedRandom<string> chat = new WeightedRandom<string>();

                //肉后时期
                if (Main.hardMode) chat.Add(this.GetLocalizedValue("Chat.HardMode0"));


                if (chat.elements.Count == 0) chat.Add(this.GetLocalizedValue("Chat.Normal5"));

                return chat.Get();
            }
        }

        private static readonly ShopCategory[] ShopCategoryList = new ShopCategory[]
        {
            new ShopCategory(
                "RestorePotion",
                "Mods.LaziestNPC.NPCs.PotionNPC.ShopCategory.RestorePotion",
                "LaziestNPC/Assets/ShopIcons/PotionNPC/RestorePotion", 2.5f),

            new ShopCategory(
                "BuffPotion",
                "Mods.LaziestNPC.NPCs.PotionNPC.ShopCategory.BuffPotion",
                "LaziestNPC/Assets/ShopIcons/PotionNPC/BuffPotion", 2.5f),

            new ShopCategory(
                "OtherPotion",
                "Mods.LaziestNPC.NPCs.PotionNPC.ShopCategory.OtherPotion",
                "LaziestNPC/Assets/ShopIcons/PotionNPC/OtherPotion", 2.5f),

            new ShopCategory(
                "FoodShop",
                "Mods.LaziestNPC.NPCs.PotionNPC.ShopCategory.FoodShop",
                "LaziestNPC/Assets/ShopIcons/PotionNPC/FoodShop", 2.5f),
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
            var RestorePotion = new NPCShop(Type, "RestorePotion");
            var BuffPotion = new NPCShop(Type, "BuffPotion");
            var OtherPotion = new NPCShop(Type, "OtherPotion");
            var FoodShop = new NPCShop(Type, "FoodShop");

            AddRestorePotionShop(RestorePotion);
            AddBuffPotionShop(BuffPotion);
            AddOtherPotionShop(OtherPotion);
            AddFoodShop(FoodShop);

            RestorePotion.Register();
            BuffPotion.Register();
            OtherPotion.Register();
            FoodShop.Register();
        }

        public void AddRestorePotionShop(NPCShop RestorePotion)
        {
            #region 【恢复药水】

            #region [原版]
            //治疗药水
            //肉前
            RestorePotion.AddItem(ItemID.BottledWater, (0, 0, 0, 5)) //瓶装水 30
            .AddItem(ItemID.LesserHealingPotion, (0, 0, 1, 0)) //弱效治疗药水 50
            .AddItem(ItemID.StrangeBrew, (0, 0, 15, 0)) //诡药 70-120
            .AddItem(ItemID.BottledHoney, (0, 0, 5, 0)) //瓶装蜂蜜 80
            .AddItem(ItemID.Eggnog, (0, 0, 5, 0)) //蛋酒 80
            .AddItem(ItemID.RestorationPotion, (0, 0, 15, 0)) //恢复药水 90
            .AddItem(ItemID.HealingPotion, (0, 0, 5, 0)) //治疗药水 100
            .AddItem(ItemID.Honeyfin, (0, 0, 45, 0)); //蜂蜜鱼 120
            //肉后
            RestorePotion.AddItem(ItemID.GreaterHealingPotion, (0, 0, 50, 0), Condition.Hardmode) //强效治疗药水 150
            //.AddItem(ItemID.LifeFruitHealingPotion, (0, 1, 0, 0), Condition.Hardmode) //丛林果汁 180
            .AddItem(ItemID.SuperHealingPotion, (0, 1, 25, 0), Condition.DownedCultist); //超级治疗药水 200

            //魔力药水
            RestorePotion.AddItem(ItemID.LesserManaPotion, (0, 0, 0, 75)) //弱效魔力药水 50
            .AddItem(ItemID.ManaPotion, (0, 0, 5, 0)) //魔力药水 100
            .AddItem(ItemID.GreaterManaPotion, (0, 0, 50, 0), Condition.Hardmode) //强效魔力药水 200
            .AddItem(ItemID.SuperManaPotion, (0, 0, 75, 0), Condition.Hardmode); //超级魔力药水 300

            #endregion

            #region [本模组独立]
            RestorePotion.AddItem(ItemType<HadalStew>(), (0, 0, 75, 0)); //乱渊炖 120 150

            #endregion

            #region [灾厄 Calamity Mod]
            //生命药水
            RestorePotion.AddModItem("CalamityMod/Bloodfin", (0, 1, 50, 0), DownedProvidence) //血鳍 240
            .AddModItem("CalamityMod/SupremeHealingPotion", (0, 1, 50, 0), DownedProvidence) //至尊治疗药水 250
            .AddModItem("CalamityMod/OmegaHealingPotion", (0, 2, 50, 0), DownedDOG); //终极治疗药水 300
            //魔力药水
            RestorePotion.AddModItem("CalamityMod/SupremeManaPotion", (0, 3, 0, 0), Condition.DownedMoonLord); //至尊魔力药水 400

            #endregion

            #region [瑟银 Thorium Mod]
            RestorePotion.AddModItem("ThoriumMod/SpringWater", (0, 0, 50, 0)) //泉水 125
            .AddModItem("ThoriumMod/Jelly", (0, 0, 25, 0), Condition.DownedSkeletron) //果酱 75
            .AddModItem("ThoriumMod/MidnightOil", (0, 0, 25, 0), Condition.DownedSkeletron); //午夜之油 75

            RestorePotion.AddModItem("ThoriumMod/LifeWater", (0, 0, 50, 0), Condition.Hardmode); //生命之水 125

            #endregion

            #region [旅人归途 Homeward Journey]
            //生命
            RestorePotion.AddModItem("ContinentOfJourney/HornofHarvest", (0, 2, 75, 0), DownedTheLifebringer) //丰收号角 320
            .AddModItem("ContinentOfJourney/UltraHealingPotion", (0, 2, 50, 0), DownedWallofShadow) //究极治疗药水 300
            .AddModItem("ContinentOfJourney/TheBody", (0, 3, 0, 0), DownedTheSon); //彼之身 400
            //魔力
            RestorePotion.AddModItem("ContinentOfJourney/UltraManaPotion", (0, 2, 50, 0), DownedWallofShadow) //究极魔力药水 320
            .AddModItem("ContinentOfJourney/TheBlood", (0, 3, 0, 0), DownedTheSon); //彼之血 400

            #endregion

            #endregion

        }

        public void AddBuffPotionShop(NPCShop BuffPotion)
        {
            #region 【增益药水】

            #region [原版]
            //实用类
            BuffPotion.AddItem(ItemID.NightOwlPotion, (0, 0, 50, 0)) //夜视药水
            .AddItem(ItemID.ShinePotion, (0, 0, 50, 0)) //光芒药水
            .AddItem(ItemID.SpelunkerPotion, (0, 0, 50, 0)) //洞穴探险药水
            .AddItem(ItemID.TrapsightPotion, (0, 0, 50, 0)) //危险感知药水
            .AddItem(ItemID.HunterPotion, (0, 0, 50, 0)) //狩猎药水
            .AddItem(ItemID.ObsidianSkinPotion, (0, 0, 50, 0)) //黑曜石皮药水
            .AddItem(ItemID.MiningPotion, (0, 0, 50, 0)) //挖矿药水
            .AddItem(ItemID.BuilderPotion, (0, 0, 50, 0)) //建筑工药水
            .AddItem(ItemID.GravitationPotion, (0, 0, 50, 0)) //重力药水
            .AddItem(ItemID.FeatherfallPotion, (0, 0, 50, 0)); //羽落药水
            BuffPotion.AddItem(ItemID.WaterWalkingPotion, (0, 0, 50, 0)) //水上漂药水
            .AddItem(ItemID.GillsPotion, (0, 0, 50, 0)) //鱼鳃药水
            .AddItem(ItemID.FlipperPotion, (0, 0, 50, 0)) //脚蹼药水
            .AddItem(ItemID.FishingPotion, (0, 0, 50, 0)) //钓鱼药水
            .AddItem(ItemID.SonarPotion, (0, 0, 50, 0)) //声纳药水
            .AddItem(ItemID.CratePotion, (0, 0, 50, 0)) //宝匣药水
            .AddItem(ItemID.LuckPotionLesser, (0, 0, 50, 0)) //弱效幸运药水
            .AddItem(ItemID.LuckPotion, (0, 1, 0, 0)) //幸运药水
            .AddItem(ItemID.LuckPotionGreater, (0, 2, 0, 0)) //强效幸运药水
            .AddItem(ItemID.InvisibilityPotion, (0, 0, 50, 0)); //隐身药水
            //战斗类
            BuffPotion.AddItem(ItemID.RegenerationPotion, (0, 0, 50, 0)) //再生药水
            .AddItem(ItemID.SwiftnessPotion, (0, 0, 50, 0)) //敏捷药水
            .AddItem(ItemID.IronskinPotion, (0, 0, 50, 0)) //铁皮药水
            .AddItem(ItemID.AmmoReservationPotion, (0, 0, 50, 0)) //弹药储备药水
            .AddItem(ItemID.ArcheryPotion, (0, 0, 50, 0)) //箭术药水
            .AddItem(ItemID.ManaRegenerationPotion, (0, 0, 50, 0)) //魔力再生药水
            .AddItem(ItemID.MagicPowerPotion, (0, 0, 50, 0)) //魔能药水
            .AddItem(ItemID.SummoningPotion, (0, 0, 50, 0)) //召唤药水
            .AddItem(ItemID.HeartreachPotion, (0, 0, 50, 0)) //拾心药水
            .AddItem(ItemID.EndurancePotion, (0, 0, 50, 0)); //耐力药水
            BuffPotion.AddItem(ItemID.ThornsPotion, (0, 0, 50, 0)) //荆棘药水
            .AddItem(ItemID.RagePotion, (0, 0, 50, 0)) //怒气药水
            .AddItem(ItemID.WrathPotion, (0, 0, 50, 0)) //暴怒药水
            .AddItem(ItemID.InfernoPotion, (0, 0, 50, 0)) //狱火药水
            .AddItem(ItemID.TitanPotion, (0, 0, 50, 0)) //泰坦药水
            .AddItem(ItemID.WarmthPotion, (0, 0, 50, 0)) //保暖药水
            .AddItem(ItemID.BattlePotion, (0, 0, 50, 0)) //战斗药水
            .AddItem(ItemID.CalmingPotion, (0, 0, 50, 0)); //镇静药水
            if (ModLoader.HasMod("CalamityMod"))
                BuffPotion.AddItem(ItemID.LifeforcePotion, (0, 1, 0, 0)); //生命力药水
            else
                BuffPotion.AddItem(ItemID.LifeforcePotion, (0, 1, 0, 0), Condition.DownedSkeletron); //生命力药水

            #endregion

            #region [本模组独立]
            //肉前
            BuffPotion.AddItem(ItemType<TriumphPotion>(), (0, 0, 75, 0), KingSlimeOrDesertBug) //胜利药水
            .AddItem(ItemType<YharimsStimulants>(), (0, 1, 0, 0), Condition.DownedSkeletron); //魔君牌兴奋剂
            //肉后
            BuffPotion.AddItem(ItemType<CadencePotion>(), (0, 1, 25, 0), Condition.Hardmode) //韵律药水
            .AddItem(ItemType<RevivifyPotion>(), (0, 1, 25, 0), Condition.Hardmode) //新生药水
            .AddItem(ItemType<PenumbraPotion>(), (0, 1, 25, 0), Condition.DownedPlantera) //半影药水
            .AddItem(ItemType<ShatteringPotion>(), (0, 1, 25, 0), Condition.DownedGolem) //粉碎药水
            .AddItem(ItemType<TitanScalePotion>(), (0, 1, 25, 0), Condition.DownedGolem) //泰坦之鳞药水
            .AddItem(ItemType<ProfanedRagePotion>(), (0, 1, 50, 0), Condition.DownedCultist); //渎神之怒药水
            //月后
            BuffPotion.AddItem(ItemType<DraconicElixir>(), (0, 2, 0, 0), Condition.DownedMoonLord); //龙魂秘药

            #endregion

            #region [LuiAFK懒人 LuiAFK Reborn]
            //【无限百科全书】的配方物品 ↓
            BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedExplorer", (0, 10, 0, 0)); //探索者卷轴

            if (ModLoader.HasMod("UnofficialLuiAFKDLC"))
                BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedCombat", (0, 30, 0, 0), Condition.DownedPlantera); //战斗卷轴
            else
                BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedCombat", (0, 30, 0, 0), Condition.Hardmode); //战斗卷轴

            BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedFishing", (0, 10, 0, 0)) //垂钓卷轴
            .AddModItem("miningcracks_take_on_luiafk/UnlimitedTravel", (0, 10, 0, 0)) //旅行卷轴
            .AddModItem("miningcracks_take_on_luiafk/UnlimitedFlight", (0, 10, 0, 0)) //飞行卷轴
            .AddModItem("miningcracks_take_on_luiafk/UnlimitedInvisibility", (0, 5, 0, 0)) //隐身之力
            .AddModItem("miningcracks_take_on_luiafk/UnlimitedTeleportation", (0, 5, 0, 0)); //传送之力
            BuffPotion.AddModItem("miningcracks_take_on_luiafk/UltimateBattler", (0, 15, 0, 0), Condition.DownedEowOrBoc) //极限战争
            .AddModItem("miningcracks_take_on_luiafk/UltimatePeaceful", (0, 15, 0, 0), Condition.Hardmode) //极限和平
            .AddModItem("miningcracks_take_on_luiafk/UnlimitedLucky", (0, 25, 0, 0)); //幸运卷轴

            //【战斗卷轴】的配方物品 ↓
            //UnofficialLuiAFKDLC把药剂瓶卷轴(纳米瓶是花后)放到了战士增益里，因此让战士增益和其上级战斗卷轴的时期变成了花后
            if (ModLoader.HasMod("UnofficialLuiAFKDLC"))
            {
                BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedBasics", (0, 5, 0, 0), Condition.NotDownedPlantera) //基础增益卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedDamage", (0, 10, 0, 0), Condition.NotDownedPlantera) //伤害卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedDefense", (0, 10, 0, 0), Condition.DownedSkeletron, Condition.NotDownedPlantera) //防御卷轴
                //.AddModItem("miningcracks_take_on_luiafk/UnlimitedMelee", (0, 5, 0, 0), Condition.DownedPlantera) //战士卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedRange", (0, 5, 0, 0), Condition.NotDownedPlantera) //射手卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedMagic", (0, 5, 0, 0), Condition.NotDownedPlantera) //魔法卷轴
                //.AddModItem("miningcracks_take_on_luiafk/UnlimitedStation", (0, 10, 0, 0), Condition.NotDownedPlantera) //增益站卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedArena", (0, 10, 0, 0), Condition.NotDownedPlantera); //战斗场地卷轴
            }
            else
            {
                BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedBasics", (0, 5, 0, 0), Condition.PreHardmode) //基础增益卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedDamage", (0, 10, 0, 0), Condition.PreHardmode) //伤害卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedDefense", (0, 10, 0, 0), Condition.DownedSkeletron, Condition.PreHardmode) //防御卷轴
                //.AddModItem("miningcracks_take_on_luiafk/UnlimitedMelee", (0, 5, 0, 0), Condition.Hardmode) //战士卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedRange", (0, 5, 0, 0), Condition.PreHardmode) //射手卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedMagic", (0, 5, 0, 0), Condition.PreHardmode) //魔法卷轴
                //.AddModItem("miningcracks_take_on_luiafk/UnlimitedStation", (0, 10, 0, 0), Condition.Hardmode) //增益站卷轴
                .AddModItem("miningcracks_take_on_luiafk/UnlimitedArena", (0, 10, 0, 0), Condition.PreHardmode); //战斗场地卷轴
            }

            //【极限战争】的配方物品 ↓
            BuffPotion.AddModItem("miningcracks_take_on_luiafk/UnlimitedBattler", (0, 5, 0, 0), Condition.NotDownedEowOrBoc) //战争卷轴
            //【极限和平】的配方物品 ↓
            .AddModItem("miningcracks_take_on_luiafk/UnlimitedPeaceful", (0, 5, 0, 0), Condition.PreHardmode); //和平卷轴

            #endregion

            #region [LuiAFK懒人DLC LuiAFK Reborn DLC]
            //原版补充
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/ScrollOfFlasks", (0, 25, 0, 0), Condition.DownedPlantera); //药剂卷轴
            //炼金术士NPC Mod
            //【炼金秘药】
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/ScrollOfMinerAlchemist", (0, 10, 0, 0)) //流光挖掘卷轴
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedGreaterDangersense", (0, 5, 0, 0)); //强感真视
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/UnlimitedNaturesBlessing", (0, 5, 0, 0), Condition.DownedEyeOfCthulhu) //自然祝福
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedDiscord", (0, 5, 0, 0), Condition.Hardmode) //混沌神药
            .AddModItem("UnofficialLuiAFKDLC/ScrollOfWarriorAlchemist", (0, 40, 0, 0), Condition.DownedPlantera); //刚毅阴影卷轴

            //【刚毅阴影卷轴】配方物品
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/UnlimitedFortitude", (0, 5, 0, 0), Condition.NotDownedPlantera) //坚韧之盾
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedBlurring", (0, 5, 0, 0), Condition.DownedMechBossAll, Condition.NotDownedPlantera) //模糊斗篷
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedInvincibility", (0, 5, 0, 0), Condition.Hardmode, Condition.NotDownedPlantera) //庇护灵药
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedTitanSkin", (0, 5, 0, 0), Condition.Hardmode, Condition.NotDownedPlantera); //泰坦韧鳞
            //.AddModItem("UnofficialLuiAFKDLC/UnlimitedNinja", (0, 0, 0, 0)) //忍者风姿

            //灾厄Mod
            //【灾厄卷轴】的配方材料 ↓
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/UnlimitedMushy", (0, 5, 0, 0)) //菌生蟹之感染
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedChi", (0, 5, 0, 0)) //福业之拥
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedShadow", (0, 5, 0, 0)); //暗影面纱
            
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/UnlimitedBaguettePower", (0, 5, 0, 0), BloodMoonHappened) //法式面包
            .AddModItem("UnofficialLuiAFKDLC/ScrollOfDepths", (0, 10, 0, 0), Condition.DownedEyeOfCthulhu) //深渊卷轴
            .AddModItem("UnofficialLuiAFKDLC/ScrollOfEffigies", (0, 10, 0, 0), Condition.DownedEowOrBoc, DownedDesertBug); //雕像卷轴

            BuffPotion.AddModItem("UnofficialLuiAFKDLC/ScrollOfInfiniteAscent", (0, 10, 0, 0), Condition.Hardmode) //升天卷轴
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedPhotosynthesis", (0, 5, 0, 0), Condition.Hardmode) //光浴之呼
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedChaosCandle", (0, 15, 0, 0), Condition.Hardmode) //混沌之魂
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedTranquilityCandle", (0, 15, 0, 0), Condition.Hardmode) //安宁之魂
            .AddModItem("UnofficialLuiAFKDLC/FermentedBook", (0, 50, 0, 0), DownedSlimeGod, Condition.Hardmode) //酒酿之书
            .AddModItem("UnofficialLuiAFKDLC/ScrollOfAstral", (0, 10, 0, 0), DownedAstrum); //星幻卷轴

            BuffPotion.AddModItem("UnofficialLuiAFKDLC/ScrollOfCandles", (0, 25, 0, 0), Condition.DownedMoonLord) //嗜酒秘典卷轴(翻译需要TODD)
            .AddModItem("UnofficialLuiAFKDLC/ScrollOfCalamityFlasks", (0, 25, 0, 0), DownedCalamitas, Condition.DownedMoonLord) //神圣碎焰卷轴
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedBloodfin", (0, 5, 0, 0), DownedProvidence); //血鳍灵药

            //【混沌之魂】和【安宁之魂】的配方材料 ↓
            BuffPotion.AddModItem("UnofficialLuiAFKDLC/UnlimitedZerg", (0, 5, 0, 0), DownedSlimeGod, Condition.PreHardmode) //虫群本能
            .AddModItem("UnofficialLuiAFKDLC/UnlimitedZen", (0, 5, 0, 0), DownedSlimeGod, Condition.PreHardmode); //安宁谐波

            #endregion

            #region [炼金术士NPC AlchemistNPC Lite]
            //单体药水
            BuffPotion.AddModItem("AlchemistNPCLite/Dopamine", (0, 0, 50, 0)) //多巴胺
            .AddModItem("AlchemistNPCLite/SunshinePotion", (0, 1, 0, 0)) //阳光药剂
            .AddModItem("AlchemistNPCLite/ExcavationPotion", (0, 1, 0, 0)) //挖掘药剂
            .AddModItem("AlchemistNPCLite/BewitchingPotion", (0, 1, 0, 0)) //迷人药剂
            .AddModItem("AlchemistNPCLite/GreaterDangersensePotion", (0, 1, 0, 0)) //强力危险感药剂
            .AddModItem("AlchemistNPCLite/FortitudePotion", (0, 1, 0, 0)); //刚毅药剂
            BuffPotion.AddModItem("AlchemistNPCLite/NatureBlessingPotion", (0, 1, 0, 0), Condition.DownedEyeOfCthulhu) //自然赐福药剂
            .AddModItem("AlchemistNPCLite/InvincibilityPotion", (0, 1, 25, 0), Condition.Hardmode) //无敌药剂
            .AddModItem("AlchemistNPCLite/TitanSkinPotion", (0, 1, 25, 0), Condition.Hardmode) //泰坦皮肤药剂
            .AddModItem("AlchemistNPCLite/BlurringPotion", (0, 1, 50, 0), Condition.DownedMechBossAll) //模糊药剂
            .AddModItem("AlchemistNPCLite/NinjaPotion", (0, 2, 0, 0), Condition.DownedPlantera) //忍者药剂
            .AddModItem("AlchemistNPCLite/DiscordPotion", (0, 5, 0, 0), Condition.Hardmode) //混乱药剂
            .AddModItem("AlchemistNPCLite/PerfectDiscordPotion", (0, 10, 0, 0), Condition.DownedPlantera) //真·混乱药剂
            .AddModItem("AlchemistNPCLite/TrapsPotion", (0, 1, 0, 0)); //陷阱药剂
            //药剂包
            BuffPotion.AddModItem("AlchemistNPCLite/UniversalCombination", (0, 5, 0, 0), Condition.Hardmode); //万能药剂包

            #endregion

            #region [灾厄 Calamity Mod]
            //实用类
            BuffPotion.AddModItem("CalamityMod/ZergPotion", (0, 1, 0, 0), DownedSlimeGod) //虫潮药水
            .AddModItem("CalamityMod/ZenPotion", (0, 1, 0, 0), DownedSlimeGod) //禅定药水
            .AddModItem("CalamityMod/PotionofOmniscience", (0, 0, 75, 0)) //全知药水
            .AddModItem("CalamityMod/AnechoicCoating", (0, 0, 50, 0)); //吸音涂层
            //战斗类
            //肉前
            BuffPotion.AddModItem("CalamityMod/BoundingPotion", (0, 0, 50, 0)) //弹跳药水
            .AddModItem("CalamityMod/CalciumPotion", (0, 0, 50, 0)) //钙质药水
            .AddModItem("CalamityMod/SulphurskinPotion", (0, 0, 50, 0)) //硫磺皮肤药水
            .AddModItem("CalamityMod/ShadowPotion", (0, 0, 75, 0)); //暗影药水
            //肉后
            BuffPotion.AddModItem("CalamityMod/PhotosynthesisPotion", (0, 1, 0, 0), Condition.Hardmode) //光合药水
            .AddModItem("CalamityMod/SoaringPotion", (0, 1, 0, 0), Condition.Hardmode) //腾飞药水
            .AddModItem("CalamityMod/GravityNormalizerPotion", (0, 1, 25, 0), DownedAstrum) //重力复原药水
            .AddModItem("CalamityMod/AstralInjection", (0, 1, 25, 0), DownedAstrum); //星幻注射剂
            //月后
            BuffPotion.AddModItem("CalamityMod/CeaselessHungerPotion", (0, 1, 50, 0), DownedVoid); //无尽吞噬药水

            #endregion

            #region [瑟银 Thorium Mod]
            //肉前
            BuffPotion.AddModItem("ThoriumMod/AquaPotion", (0, 0, 50, 0)) //潜水药水
            .AddModItem("ThoriumMod/ArtilleryPotion", (0, 0, 50, 0)) //集群火炮药水
            .AddModItem("ThoriumMod/AssassinPotion", (0, 0, 50, 0)) //刺客药水
            .AddModItem("ThoriumMod/BouncingFlamePotion", (0, 0, 50, 0)) //弹跳火焰药水
            .AddModItem("ThoriumMod/ConflagrationPotion", (0, 0, 50, 0)) //燃炎药水
            .AddModItem("ThoriumMod/EarwormPotion", (0, 0, 50, 0)) //耳虫药水
            .AddModItem("ThoriumMod/FrenzyPotion", (0, 0, 50, 0)) //狂怒药水
            .AddModItem("ThoriumMod/GlowingPotion", (0, 0, 50, 0)) //光辉药水
            .AddModItem("ThoriumMod/HydrationPotion", (0, 0, 50, 0)); //补水药水
            BuffPotion.AddModItem("ThoriumMod/BloodPotion", (0, 0, 50, 0), BloodMoonHappened) //堕血药水
            .AddModItem("ThoriumMod/CreativityPotion", (0, 0, 75, 0), DownedGrandThunderBird) //创意药水
            .AddModItem("ThoriumMod/WarmongerPotion", (0, 0, 75, 0), Condition.DownedEowOrBoc); //好战药水
            //肉后
            BuffPotion.AddModItem("ThoriumMod/ArcanePotion", (0, 1, 0, 0), Condition.Hardmode) //奥术药水
            .AddModItem("ThoriumMod/HolyPotion", (0, 1, 0, 0), Condition.Hardmode) //圣洁药水
            .AddModItem("ThoriumMod/InspirationReachPotion", (0, 1, 0, 0), Condition.Hardmode) //灵感之触药水
            .AddModItem("ThoriumMod/KineticPotion", (0, 1, 0, 0), Condition.Hardmode); //动能药水

            #endregion

            #region [旅人归途 Homeward Journey]
            BuffPotion.AddModItem("ContinentOfJourney/YangPotion", (0, 2, 50, 0), DownedWallofShadow) //阳药水
            .AddModItem("ContinentOfJourney/YinPotion", (0, 2, 50, 0), DownedWallofShadow) //阴药水
            .AddModItem("ContinentOfJourney/FlightPotion", (0, 3, 0, 0), DownedTheMaterealizer) //飞升药水
            .AddModItem("ContinentOfJourney/HastePotion", (0, 3, 0, 0), DownedTheMaterealizer) //急迫药水
            .AddModItem("ContinentOfJourney/ReanimationPotion", (0, 3, 0, 0), DownedTheMaterealizer); //复生药水

            #endregion

            #endregion

        }

        public void AddOtherPotionShop(NPCShop OtherPotion)
        {
            #region 【其他药水】

            #region [原版]
            //其他类
            OtherPotion.AddItem(ItemID.GenderChangePotion, (0, 1, 0, 0)) //变性药水
            .AddItem(ItemID.StinkPotion, (0, 0, 50, 0)) //臭味药水
            .AddItem(ItemID.BiomeSightPotion, (0, 0, 50, 0)) //生物群系视觉药水
            .AddItem(ItemID.RecallPotion, (0, 0, 50, 0)) //回忆药水
            .AddItem(ItemID.PotionOfReturn, (0, 0, 80, 0)) //返回药水
            .AddItem(ItemID.WormholePotion, (0, 0, 50, 0)) //虫洞药水
            .AddItem(ItemID.TeleportationPotion, (0, 0, 50, 0)) //传送药水
            .AddItem(ItemID.RedPotion, (0, 2, 0, 0)) //红药水
            .AddItem(ItemID.LovePotion, (0, 1, 0, 0), Condition.Hardmode); //爱情药水
            //药剂瓶
            OtherPotion.AddItem(ItemID.FlaskofFire, (0, 0, 50, 0), Condition.DownedEowOrBoc, Condition.DownedQueenBee) //烈火药剂
            .AddItem(ItemID.FlaskofPoison, (0, 0, 50, 0), Condition.DownedQueenBee) //毒药剂
            .AddItem(ItemID.FlaskofIchor, (0, 1, 50, 0), Condition.Hardmode, Condition.DownedQueenBee) //灵液药剂
            .AddItem(ItemID.FlaskofCursedFlames, (0, 1, 50, 0), Condition.Hardmode, Condition.DownedQueenBee) //诅咒焰药剂
            .AddItem(ItemID.FlaskofVenom, (0, 1, 50, 0), Condition.DownedPlantera, Condition.DownedQueenBee); //毒液药剂

            #endregion

            #region [炼金术士NPC AlchemistNPC Lite]
            //特殊
            OtherPotion.AddModItem("AlchemistNPCLite/GlobalTeleporter", (0, 10, 0, 0), Condition.Hardmode) //世界传送装置
            .AddModItem("AlchemistNPCLite/GlobalTeleporterUp", (10, 0, 0, 0), Condition.DownedMoonLord); //改良世界传送装置
            //传送药剂
            OtherPotion.AddModItem("AlchemistNPCLite/JungleTeleporterPotion", (0, 1, 0, 0)) //丛林传送药剂
            .AddModItem("AlchemistNPCLite/DungeonTeleportationPotion", (0, 1, 0, 0)) //地牢传送药剂
            .AddModItem("AlchemistNPCLite/BeachTeleporterPotion", (0, 1, 0, 0)) //海滩传送药剂
            .AddModItem("AlchemistNPCLite/OceanTeleporterPotion", (0, 1, 0, 0)) //海洋传送药剂
            .AddModItem("AlchemistNPCLite/UnderworldTeleportationPotion", (0, 1, 0, 0)) //地狱传送药剂            
            .AddModItem("AlchemistNPCLite/TempleTeleportationPotion", (0, 1, 0, 0)); //神庙传送药剂
            //其他
            OtherPotion.AddModItem("AlchemistNPCLite/UltimaCake", (0, 3, 0, 0), Condition.Hardmode); //终极蛋糕

            #endregion

            #region [灾厄 Calamity Mod]
            //武器灌注瓶
            OtherPotion.AddModItem("CalamityMod/FlaskOfCrumbling", (0, 1, 0, 0), Condition.Hardmode, Condition.DownedQueenBee) //粉碎瓶
            .AddModItem("CalamityMod/FlaskOfBrimstone", (0, 1, 0, 0), DownedCalamitas, Condition.DownedQueenBee) //硫火瓶
            .AddModItem("CalamityMod/FlaskOfHolyFlames", (0, 1, 50, 0), Condition.DownedMoonLord); //圣火瓶

            #endregion

            #region [瑟银 Thorium Mod]
            //涂层
            OtherPotion.AddModItem("ThoriumMod/DeepFreezeCoatingItem", (0, 0, 50, 0), Condition.DownedQueenBee) //深寒涂层
            .AddModItem("ThoriumMod/SporeCoatingItem", (0, 0, 50, 0), Condition.DownedQueenBee) //孢子涂层
            .AddModItem("ThoriumMod/ToxicCoatingItem", (0, 0, 50, 0), Condition.DownedQueenBee) //剧毒涂层
            .AddModItem("ThoriumMod/ExplosiveCoatingItem", (0, 0, 50, 0), Condition.DownedEowOrBoc, Condition.DownedQueenBee) //爆炸涂层
            .AddModItem("ThoriumMod/GorgonCoatingItem", (0, 0, 75, 0), Condition.DownedSkeletron, Condition.DownedQueenBee); //石化涂层
            //趋避剂
            OtherPotion.AddModItem("ThoriumMod/BatRepellent", (0, 1, 0, 0), DownedPatchWerk) //蝙蝠趋避剂
            .AddModItem("ThoriumMod/FishRepellent", (0, 1, 0, 0), DownedPatchWerk) //鱼类趋避剂
            .AddModItem("ThoriumMod/InsectRepellent", (0, 1, 0, 0), DownedPatchWerk) //昆虫趋避剂
            .AddModItem("ThoriumMod/SkeletonRepellent", (0, 1, 0, 0), DownedPatchWerk) //骷髅趋避剂
            .AddModItem("ThoriumMod/ZombieRepellent", (0, 1, 0, 0), DownedPatchWerk); //僵尸趋避剂

            #endregion

            #region [旅人归途 Homeward Journey]
            //药剂
            OtherPotion.AddModItem("ContinentOfJourney/DivineFireFlask", (0, 2, 50, 0), DownedWallofShadow, Condition.DownedQueenBee) //圣洁瓶
            .AddModItem("ContinentOfJourney/ForceBreakFlask", (0, 2, 50, 0), DownedWallofShadow, Condition.DownedQueenBee) //断键瓶
            .AddModItem("ContinentOfJourney/PlagueFlask", (0, 2, 50, 0), DownedWallofShadow, Condition.DownedQueenBee) //瘟疫瓶
            .AddModItem("ContinentOfJourney/SteelFlask", (0, 2, 50, 0), DownedWallofShadow, Condition.DownedQueenBee); //钢瓶
            //其他
            OtherPotion.AddModItem("ContinentOfJourney/Iodine", (0, 1, 50, 0), Condition.DownedPlantera); //碘酒

            #endregion


            #endregion

        }

        public void AddFoodShop(NPCShop FoodShop)
        {
            #region 【食物商店】

            #region [原版]
            #endregion

            #region [旅人归途 Homeward Journey]
            FoodShop.AddModItem("ContinentOfJourney/FluorescentBerry", (0, 2, 0, 0)) //荧光果
            .AddModItem("ContinentOfJourney/FluorescentBerryCan", (0, 2, 50, 0)) //荧光果罐头
            .AddModItem("ContinentOfJourney/ForbiddenFruit", (0, 2, 0, 0), DownedWallofShadow) //分辨善恶果
            .AddModItem("ContinentOfJourney/ForbiddenFruitCan", (0, 2, 50, 0), DownedWallofShadow); //禁果罐头

            #endregion

            #endregion

        }
    }
}
