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

            .AddItem(ItemID.CopperBar, (0, 0, 25, 0)) //铜锭
            .AddItem(ItemID.TinBar, (0, 0, 25, 0)) //锡锭
            .AddItem(ItemID.IronBar, (0, 0, 50, 0)) //铁锭
            .AddItem(ItemID.LeadBar, (0, 0, 50, 0)) //铅锭
            .AddItem(ItemID.SilverBar, (0, 0, 75, 0)) //银锭
            .AddItem(ItemID.TungstenBar, (0, 0, 75, 0)) //钨锭
            .AddItem(ItemID.GoldBar, (0, 1, 0, 0)) //金锭
            .AddItem(ItemID.PlatinumBar, (0, 1, 0, 0)) //铂金锭
            .AddItem(ItemID.Obsidian, (0, 0, 25, 0)) //黑曜石

            .AddItem(ItemID.DemoniteBar, (0, 1, 50, 0)) //魔矿锭
            .AddItem(ItemID.CrimtaneBar, (0, 1, 50, 0)) //猩红矿锭
            .AddItem(ItemID.MeteoriteBar, (0, 2, 0, 0), Condition.DownedEowOrBoc) //陨石锭
            .AddItem(ItemID.HellstoneBar, (0, 2, 0, 0), Condition.DownedEowOrBoc) //狱石锭
            .AddItem(ItemID.LifeCrystal, (0, 5, 0, 0), Condition.DownedEyeOfCthulhu) //生命水晶

            .AddItem(ItemID.CobaltBar, (0, 2, 50, 0), Condition.Hardmode) //钴锭
            .AddItem(ItemID.PalladiumBar, (0, 2, 50, 0), Condition.Hardmode) //钯金锭
            .AddItem(ItemID.MythrilBar, (0, 3, 0, 0), Condition.Hardmode) //秘银锭
            .AddItem(ItemID.OrichalcumBar, (0, 3, 0, 0), Condition.Hardmode) //山铜锭
            .AddItem(ItemID.AdamantiteBar, (0, 4, 0, 0), Condition.Hardmode) //精金锭
            .AddItem(ItemID.TitaniumBar, (0, 4, 0, 0), Condition.Hardmode) //钛金锭
            .AddItem(ItemID.LifeFruit, (0, 10, 0, 0), Condition.DownedMechBossAny) //生命果
            .AddItem(ItemID.ChlorophyteBar, (0, 5, 0, 0), Condition.DownedMechBossAll); //叶绿锭
            
            #endregion

            //原版的矿物直接用锭的形式出售，但模组的矿由于不确定合成锭的另一个材料或者矿本身的时期
            //为防止模组里可能出现的各种奇奇怪怪的解锁条件，暂时先以原矿形式出售。既然如此，原版是否也要改为原矿？
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
            #endregion

            #region [灾厄 Calamity Mod]
            #endregion

            #endregion

            ores.Register();
            accs.Register();
        }
    }
}
