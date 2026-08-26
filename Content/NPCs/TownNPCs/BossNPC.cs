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
using static LaziestNPC.LaziestNPC;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class BossNPC : ModNPC
    {
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
                Velocity = 2f
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
            NPC.width = 26;
            NPC.height = 26;
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
            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.button1");
            button2 = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.button2");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "TreasureBags";
            }
            else
            {
                shopName = "SumShop";
            }
        }

        public override void AddShops()
        {
            var treasureBags = new NPCShop(Type, "TreasureBags");
            var sumShop = new NPCShop(Type, "SumShop");
            //好多，光是原版和灾厄Fargo两个模组，以及三者的Boss召唤物，我就看麻了，又要单独抽时间写了
            #region 原版Boss宝藏袋
            treasureBags.Add(CustomPrice(ItemID.KingSlimeBossBag, buyPrice(0, 5, 0, 0)), Condition.DownedKingSlime)
                .Add(CustomPrice(ItemID.EyeOfCthulhuBossBag, buyPrice(0, 10, 0, 0)), Condition.DownedEyeOfCthulhu)
                //.Add(CustomPrice(ItemID.BrainOfCthulhuBossBag, buyPrice(0, 15, 0, 0)), ModConditions.DownedBrainofCthulhu)
                //.Add(CustomPrice(ItemID.EaterOfWorldsBossBag, buyPrice(0, 15, 0, 0)), ModConditions.DownedEaterofWorlds)
                .Add(CustomPrice(ItemID.QueenBeeBossBag, buyPrice(0, 20, 0, 0)), Condition.DownedQueenBee)
                .Add(CustomPrice(ItemID.SkeletronBossBag, buyPrice(0, 25, 0, 0)), Condition.DownedSkeletron)
                .Add(CustomPrice(ItemID.DeerclopsBossBag, buyPrice(0, 30, 0, 0)), Condition.DownedDeerclops)
                .Add(CustomPrice(ItemID.WallOfFleshBossBag, buyPrice(0, 35, 0, 0)), Condition.Hardmode);

            #endregion

            #region 召唤物品

            #region 事件召唤物品
            sumShop
                //.Add(CustomPrice(ItemType<RainMagic>(), buyPrice(0, 1, 0, 0)))
                .Add(CustomPrice(ItemID.BloodMoonStarter, buyPrice(0, 2, 0, 0)))
                .Add(CustomPrice(ItemID.GoblinBattleStandard, buyPrice(0, 2, 0, 0)))
                //.Add(CustomPrice(ItemType<CausticTear>(), buyPrice(0, 1, 50, 0)), Condition.DownedEyeOfCthulhu)
                //.Add(CustomPrice(ItemType<TorrentialTear>(), buyPrice(0, 1, 70, 0)), Condition.DownedSkeletron)
                .Add(CustomPrice(ItemID.PirateMap, buyPrice(0, 3, 0, 0)), Condition.Hardmode)
                .Add(CustomPrice(ItemID.SnowGlobe, buyPrice(0, 3, 0, 0)), Condition.Hardmode)
                .Add(CustomPrice(ItemID.SolarTablet, buyPrice(0, 3, 50, 0)), Condition.DownedMechBossAny)
                .Add(CustomPrice(ItemID.PumpkinMoonMedallion, buyPrice(0, 4, 0, 0)), Condition.DownedPlantera)
                //.Add(CustomPrice(ItemType<MartianDistressRemote>(), buyPrice(0, 4, 0, 0)), Condition.DownedGolem)
                .Add(CustomPrice(ItemID.NaughtyPresent, buyPrice(0, 4, 0, 0)), Condition.DownedPlantera);

            #endregion

            #endregion
        
        }

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
