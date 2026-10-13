using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    public abstract class TownLNPC : ModNPC
    {
        //protected IList<string> OnceChats = new List<string>(); //战后首句文本

        protected virtual string BestiaryText => ""; //图鉴文本
        protected virtual string NPCNameText => ""; //NPC名字
        protected abstract int AttackProjType { get; } //射弹ID
        protected virtual int AttackProjDamage => 2; //射弹基础伤害

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 21;
            NPCID.Sets.ExtraFramesCount[Type] = 7;
            NPCID.Sets.AttackFrameCount[Type] = 2;
            NPCID.Sets.DangerDetectRange[Type] = 300;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 20; //控制单次动作的观感，动作持续时间
            NPCID.Sets.AttackAverageChance[NPC.type] = 20; //控制攻击倾向，多久才会触发下一次攻击
            NPCID.Sets.ShimmerTownTransform[Type] = false;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 20;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.defense = 100;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Dryad;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement(BestiaryText)
            });
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>()
            {
                this.GetLocalizedValue(NPCNameText)
            };
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = AttackProjDamage;
            knockback = 3f;
        }
        //控制攻击频率的下限，能保证“两次攻击之间至少隔多久”
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 180;
            randExtraCooldown = 60;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = AttackProjType;
            attackDelay = 10;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 8f;
        }

        /*public override void LoadData(TagCompound tag)
        {
            OnceChats = tag.GetList<string>("OnceChats");
        }
        //每个存档独立存储
        public override void SaveData(TagCompound tag)
        {
            tag["OnceChats"] = OnceChats;
        }*/

    }
}
