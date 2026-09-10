using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Utilities;
using Terraria.Localization;
using static Terraria.Item;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static LaziestNPC.LaziestNPC;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Common.ModBossess;
using LaziestNPC.Content.Items.SummonItems;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class BossNPC : ModNPC
    {
        private static int ShopNum = 1;

        private const string VanillaAll = "VanillaAll";
        private const string ModBags = "ModBags";
        private const string ModSums = "ModSums";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 5;
            //NPCID.Sets.ExtraFramesCount[Type] = 0;
            //NPCID.Sets.AttackFrameCount[Type] = 0; 
            NPCID.Sets.DangerDetectRange[Type] = 220; 

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                SpriteDirection = 1,
                Direction = -1,
                Velocity = 0.1f
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
            AIType = NPCID.Squirrel;
            //AnimationType = NPCID.Guide;
            // ↑这条【暂时】要删掉，因为这个【临时贴图】实际上不是NPC而是一个中立生物的
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.LaziestNPC.Bestiary.BossNPC")
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return numTownNPCs > 5;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                this.GetLocalizedValue("Name.BossNPC")
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
            switch (ShopNum)
            {
                case 1:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.VanillaAll");
                    break;
                case 2:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.ModBags");
                    break;
                default:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.ModSums");
                    break;
            }

            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.CycleShop");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                switch (ShopNum)
                {
                    case 1:
                        shopName = VanillaAll;
                        break;
                    case 2:
                        shopName = ModBags;
                        break;
                    default:
                        shopName = ModSums;
                        break;
                }
            }
            else
            {
                ShopNum++;
                if (ShopNum > 3)
                    ShopNum = 1;
            }
        }

        public override void AddShops()
        {
            var vanShop = new NPCShop(Type, VanillaAll);
            var modBag = new NPCShop(Type, ModBags);
            var modSum = new NPCShop(Type, ModSums);

            #region 【原版商店】

            #region 原版Boss宝藏袋
            //肉前
            vanShop.AddItem(ItemID.KingSlimeBossBag, (0, 10, 0, 0), Condition.DownedKingSlime)
                .AddItem(ItemID.EyeOfCthulhuBossBag, (0, 15, 0, 0), Condition.DownedEyeOfCthulhu)
                .AddItem(ItemID.BrainOfCthulhuBossBag, (0, 20, 0, 0), Condition.DownedEowOrBoc)
                .AddItem(ItemID.EaterOfWorldsBossBag, (0, 20, 0, 0), Condition.DownedEowOrBoc)
                .AddItem(ItemID.QueenBeeBossBag, (0, 25, 0, 0), Condition.DownedQueenBee)
                .AddItem(ItemID.SkeletronBossBag, (0, 30, 0, 0), Condition.DownedSkeletron)
                .AddItem(ItemID.DeerclopsBossBag, (0, 35, 0, 0), Condition.DownedDeerclops)
                .AddItem(ItemID.WallOfFleshBossBag, (0, 40, 0, 0), Condition.Hardmode);
            //肉后
            vanShop.AddItem(ItemID.QueenSlimeBossBag, (0, 50, 0, 0), Condition.DownedQueenSlime)
                .AddItem(ItemID.DestroyerBossBag, (0, 55, 0, 0), Condition.DownedDestroyer)
                .AddItem(ItemID.TwinsBossBag, (0, 55, 0, 0), Condition.DownedTwins)
                .AddItem(ItemID.SkeletronPrimeBossBag, (0, 55, 0, 0), Condition.DownedSkeletronPrime)
                .AddItem(ItemID.PlanteraBossBag, (0, 60, 0, 0), Condition.DownedPlantera)
                .AddItem(ItemID.FairyQueenBossBag, (0, 65, 0, 0), Condition.DownedEmpressOfLight)
                .AddItem(ItemID.GolemBossBag, (0, 70, 0, 0), Condition.DownedGolem)
                .AddItem(ItemID.FishronBossBag, (0, 75, 0, 0), Condition.DownedDukeFishron)
                .AddItem(ItemID.BossBagBetsy, (0, 80, 0, 0), Condition.DownedOldOnesArmyT3)
                .AddItem(ItemID.MoonLordBossBag, (1, 0, 0, 0), Condition.DownedMoonLord);
            #endregion

            #region 原版事件召唤物品
            vanShop.AddItem(ItemType<TheReturner>(), (0, 0, 50, 0))
                .AddItem(ItemID.BloodMoonStarter, (0, 1, 0, 0)) //血月
                .AddItem(ItemID.GoblinBattleStandard, (0, 1, 0, 0)) //哥布林入侵
                .AddItem(ItemID.PirateMap, (0, 10, 0, 0), Condition.Hardmode) //海盗入侵
                .AddItem(ItemID.SnowGlobe, (0, 10, 0, 0), Condition.Hardmode) //雪人军团
                .AddItem(ItemID.SolarTablet, (0, 15, 0, 0), Condition.DownedMechBossAny) //日食
                .AddItem(ItemID.PumpkinMoonMedallion, (0, 20, 0, 0), Condition.DownedPlantera) //南瓜月
                .AddItem(ItemID.NaughtyPresent, (0, 20, 0, 0), Condition.DownedPlantera); //霜月
            #endregion

            #region 原版Boss召唤物品
            //肉前
            vanShop.AddItem(ItemID.SlimeCrown, (0, 1, 0, 0)) //史莱姆王
                .AddItem(ItemID.SuspiciousLookingEye, (0, 1, 0, 0)) //克苏鲁之眼
                .AddItem(ItemID.BloodySpine, (0, 5, 0, 0)) //克苏鲁之脑
                .AddItem(ItemID.WormFood, (0, 5, 0, 0)) //世界吞噬怪
                .AddItem(ItemID.Abeemination, (0, 5, 0, 0)) //蜂王
                .AddItem(ItemID.DeerThing, (0, 5, 0, 0)) //独眼巨鹿
                .AddItem(ItemID.GuideVoodooDoll, (0, 10, 0, 0)) //血肉之墙
            //肉后
                .AddItem(ItemID.QueenSlimeCrystal, (0, 10, 0, 0), Condition.Hardmode) //史莱姆皇后
                .AddItem(ItemID.MechanicalWorm, (0, 15, 0, 0), Condition.Hardmode) //毁灭者
                .AddItem(ItemID.MechanicalEye, (0, 15, 0, 0), Condition.Hardmode) //双子魔眼
                .AddItem(ItemID.MechanicalSkull, (0, 15, 0, 0), Condition.Hardmode) //机械骷髅王
                .AddItem(ItemID.EmpressButterfly, (0, 20, 0, 0), Condition.DownedPlantera) //光之女皇
                .AddItem(ItemID.LihzahrdPowerCell, (0, 25, 0, 0), Condition.DownedPlantera) //石巨人
                .AddItem(ItemID.TruffleWorm, (0, 10, 0, 0), Condition.Hardmode) //猪龙鱼公爵
                .AddItem(ItemID.CelestialSigil, (0, 30, 0, 0), Condition.DownedCultist); //月亮领主

            #endregion

            #endregion

            #region 【模组Boss宝藏袋】
            //肉前
            modBag.AddModItem("CalamityMod/DesertScourgeBag", (0, 10, 0, 0), DownedDesertBug) //荒漠灾虫
            .AddModItem("CalamityMod/CrabulonBag", (0, 15, 0, 0), DownedCrabulon) //菌生蟹
            .AddModItem("CalamityMod/PerforatorBag", (0, 20, 0, 0), DownedPerforator) //血肉宿主
            .AddModItem("CalamityMod/HiveMindBag", (0, 20, 0, 0), DownedHiveMind) //腐巢意志
            .AddModItem("CalamityMod/SlimeGodBag", (0, 35, 0, 0), DownedSlimeGod); //史莱姆之神
            //肉后
            /*.AddModItem("CalamityMod/CryogenBag", (0, 50, 0, 0), DownedCryogen) //极地之灵
            .AddModItem("CalamityMod/AquaticScourgeBag", (0, 50, 0, 0), DownedAquaticBug) //渊海灾虫
            .AddModItem("CalamityMod/BrimstoneElementalBag", (0, 50, 0, 0), DownedBrimstone) //硫磺火元素
            .AddModItem("CalamityMod/AstrumAureusBag", (0, 55, 0, 0), DownedAstrum) //白金星舰
            .AddModItem("CalamityMod/CalamitasCloneBag", (0, 65, 0, 0), DownedCalamitas) //灾厄之影
            .AddModItem("CalamityMod/LeviathanBag", (0, 65, 0, 0), DownedLeviathan) //阿娜希塔和利维坦
            .AddModItem("CalamityMod/PlaguebringerGoliathBag", (0, 75, 0, 0), DownedPlague) //瘟疫使者歌莉娅
            .AddModItem("CalamityMod/RavagerBag", (0, 75, 0, 0), DownedRavager) //毁灭魔像
            .AddModItem("CalamityMod/AstrumDeusBag", (0, 90, 0, 0), DownedAstrum) //星神游龙
            //——————————————————————————末世星史莱姆
            //月后
            .AddModItem("CalamityMod/DragonfollyBag", (1, 50, 0, 0), DownedDragonfolly) //痴愚金龙
            .AddModItem("CalamityMod/ProvidenceBag", (2, 0, 0, 0), DownedProvidence) //亵渎天神
            .AddModItem("CalamityMod/StormWeaverBag", (2, 50, 0, 0), DownedStormWeaver) //风暴编织者
            .AddModItem("CalamityMod/CeaselessVoidBag", (2, 50, 0, 0), DownedVoid) //无尽虚空
            .AddModItem("CalamityMod/SignusBag", (2, 50, 0, 0), DownedSignus) //西格纳斯
            .AddModItem("CalamityMod/PolterghastBag", (3, 0, 0, 0), DownedPolterghast) //噬魂幽花
            .AddModItem("CalamityMod/OldDukeBag", (3, 50, 0, 0), DownedOldDuke) //硫海遗爵
            .AddModItem("CalamityMod/DevourerofGodsBag", (4, 0, 0, 0), DownedDOG) //神明吞噬者
            .AddModItem("CalamityMod/YharonBag", (5, 0, 0, 0), DownedYharon) //犽戎
            .AddModItem("CalamityMod/DraedonBag", (8, 0, 0, 0), DownedDraedon) //星流巨械
            .AddModItem("CalamityMod/CalamitasCoffer", (10, 0, 0, 0), DownedSCalamitas); //终灾*/

            #endregion

            #region 【模组召唤物品】

            #region 模组事件召唤物品
            modSum.AddModItem("CalamityMod/CausticTear", (0, 1, 50, 0), Condition.DownedEyeOfCthulhu)
            //.AddModItem("CalamityMod/TorrentialTear", (0, 1, 70, 0), Condition.DownedSkeletron)
            .AddModItem("CalamityMod/MartianDistressRemote", (0, 4, 0, 0), Condition.DownedGolem);

            #endregion

            #endregion


            vanShop.Register();
            modBag.Register();
            modSum.Register();
        }

        //正式添加新贴图后要删掉这个方法
        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0f)
            {
                if (!NPC.IsABestiaryIconDummy)
                {
                    if (NPC.direction == 1)
                    {
                        NPC.spriteDirection = -1;
                    }
                    if (NPC.direction == -1)
                    {
                        NPC.spriteDirection = 1;
                    }

                    if (NPC.velocity.X == 0f)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0.0;
                        return;
                    }
                }
                NPC.frameCounter += NPC.IsABestiaryIconDummy ? 0.6f : Math.Abs(NPC.velocity.X) * 0.25f;
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > 12.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y / frameHeight >= Main.npcFrameCount[NPC.type] - 1)
                {
                    NPC.frame.Y = frameHeight;
                }
            }
            else
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y = frameHeight * 2;
            }
        }

        /*public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 70;
            knockback = 3f;
        }

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            scale = 1f;
            horizontalHoldoutOffset = 20;
            if (!NPC.downedMoonlord)
            {
                item = TextureAssets.Item[ItemID.Shotgun].Value;
            }
            if (NPC.downedMoonlord)
            {
                item = TextureAssets.Item[ItemID.VortexBeater].Value;
            }
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!NPC.downedMoonlord)
            {
                attackDelay = 10;
                projType = 279;
            }
            if (NPC.downedMoonlord)
            {
                attackDelay = 4;
                projType = 638;
            }
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
        }*/
    }
}
