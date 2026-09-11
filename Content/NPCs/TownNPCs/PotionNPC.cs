using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using LaziestNPC.Globals.GlobalItems;

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

            #region 恢复类药水

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

            #region 实用类药水
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
                .AddItem(ItemID.LovePotion, (0, 1, 0, 0), Condition.Hardmode) //爱情药水
                .AddItem(ItemID.StinkPotion, (0, 0, 50, 0)) //臭味药水
                .AddItem(ItemID.BiomeSightPotion, (0, 0, 50, 0)) //生物群系视觉药水
                .AddItem(ItemID.RecallPotion, (0, 0, 50, 0)) //回忆药水
                .AddItem(ItemID.PotionOfReturn, (0, 0, 80, 0)) //返回药水
                .AddItem(ItemID.WormholePotion, (0, 0, 50, 0)) //虫洞药水
                .AddItem(ItemID.TeleportationPotion, (0, 0, 50, 0)) //传送药水
                .AddItem(ItemID.RedPotion, (0, 2, 0, 0)); //红药水

            #endregion

            #region 战斗类药水

            #endregion

            #endregion

            #region 【模组药水】

            #endregion


            vPotion.Register();
            mPotion.Register();
        }
    }
}
