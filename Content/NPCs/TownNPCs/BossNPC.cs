using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Utilities;
using Terraria.Localization;
using static Terraria.Item;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;
using static LaziestNPC.LaziestNPC;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Common.ModBossess;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class BossNPC : ModNPC
    {
        private static int ShopNum = 1;  //1=肉前, 2=肉后, 3=月后

        private const string ShopPreHM = "PreHardMode";
        private const string ShopHM = "HardMode";
        private const string ShopPostML = "PostMoonLord";

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
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.PreHardMode");
                    break;
                case 2:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.HardMode");
                    break;
                default:
                    button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.PostMoonLord");
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
                        shopName = ShopPreHM;
                        break;
                    case 2:
                        shopName = ShopHM;
                        break;
                    default:
                        shopName = ShopPostML;
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
            var PreHMBags = new NPCShop(Type, ShopPreHM);
            var HardMBags = new NPCShop(Type, ShopHM);
            var PostMLBags = new NPCShop(Type, ShopPostML);
            //var sumShop = new NPCShop(Type, "SumShop");

            #region 原版Boss宝藏袋
            //肉前
            PreHMBags.AddItem(ItemID.KingSlimeBossBag, (0, 10, 0, 0), Condition.DownedKingSlime)
                .AddItem(ItemID.EyeOfCthulhuBossBag, (0, 15, 0, 0), Condition.DownedEyeOfCthulhu)
                .AddItem(ItemID.BrainOfCthulhuBossBag, (0, 20, 0, 0), Condition.DownedEowOrBoc)
                .AddItem(ItemID.EaterOfWorldsBossBag, (0, 20, 0, 0), Condition.DownedEowOrBoc)
                .AddItem(ItemID.QueenBeeBossBag, (0, 25, 0, 0), Condition.DownedQueenBee)
                .AddItem(ItemID.SkeletronBossBag, (0, 30, 0, 0), Condition.DownedSkeletron)
                .AddItem(ItemID.DeerclopsBossBag, (0, 35, 0, 0), Condition.DownedDeerclops)
                .AddItem(ItemID.WallOfFleshBossBag, (0, 40, 0, 0), Condition.Hardmode);
            //肉后
            HardMBags.AddItem(ItemID.QueenSlimeBossBag, (0, 50, 0, 0), Condition.DownedQueenSlime)
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

            #region 模组Boss宝藏袋
            //肉前
            PreHMBags.AddModItem("CalamityMod/DesertScourgeBag", (0, 10, 0, 0), DownedDesertScourge)
            .AddModItem("CalamityMod/CrabulonBag", (0, 15, 0, 0), DownedCrabulon);
            #endregion

            #region 召唤物品

            /*#region 事件召唤物品
            sumShop
                //.AddItem(ItemType<RainMagic>(), (0, 1, 0, 0))
                .AddItem(ItemID.BloodMoonStarter, (0, 2, 0, 0))
                .AddItem(ItemID.GoblinBattleStandard, (0, 2, 0, 0))
                //.AddItem(ItemType<CausticTear>(), (0, 1, 50, 0), Condition.DownedEyeOfCthulhu)
                //.AddItem(ItemType<TorrentialTear>(), (0, 1, 70, 0), Condition.DownedSkeletron)
                .AddItem(ItemID.PirateMap, (0, 3, 0, 0), Condition.Hardmode)
                .AddItem(ItemID.SnowGlobe, (0, 3, 0, 0), Condition.Hardmode)
                .AddItem(ItemID.SolarTablet, (0, 3, 50, 0), Condition.DownedMechBossAny)
                .AddItem(ItemID.PumpkinMoonMedallion, (0, 4, 0, 0), Condition.DownedPlantera)
                //.AddItem(ItemType<MartianDistressRemote>(), (0, 4, 0, 0), Condition.DownedGolem)
                .AddItem(ItemID.NaughtyPresent, (0, 4, 0, 0), Condition.DownedPlantera);

            #endregion*/

            #endregion

            PreHMBags.Register();
            HardMBags.Register();
            PostMLBags.Register();
            //sumShop.Register();
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
