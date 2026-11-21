using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using LaziestNPC.Common.UI;
using LaziestNPC.Globals.GlobalPlayers;
using LaziestNPC.Common.Helpers;

namespace LaziestNPC.Content.NPCs.TownNPCs
{
    public abstract class TownLNPC : ModNPC
    {
        private TownLNPCPlayer LTPlayer => Main.LocalPlayer.LaziestTownLNPC();
        //当前选中的UI行索引(是序号)
        public int CurrentShopCategoryIndex
        {
            get
            {
                if (Main.dedServ)
                    return 0;

                //ShopCategoryIndex字典里有对应NPC的数字，就让CurrentShopCategoryIndex返回此数字
                return LTPlayer.ShopCategoryIndex.TryGetValue(Name, out int index) ? index : 0;
            }
            set
            {
                if (!Main.dedServ)
                {
                    //点击哪一行，ShopCategoryIndex被赋值为一个数字
                    LTPlayer.ShopCategoryIndex[Name] = value;
                }
            }
        }

        //这是一个有序的虚列表，存在里面的元素，第0个、第1个、第2个等等都是固定的。
        //在每个NPC类里用ShopCategoryList“重写”
        public virtual IReadOnlyList<ShopCategory> ShopCategories => Array.Empty<ShopCategory>();

        //子类：定义自己的ShopCategory商店类别
        public ShopCategory CurrentShopCategory
        {
            get
            {
                //NPC的所有商店类别，放到一个临时列表里
                IReadOnlyList<ShopCategory> list = ShopCategories;
                //没类别就null
                if (list == null || list.Count == 0)
                    return null;

                int index = CurrentShopCategoryIndex; //取出玩家记住的序号
                //把越界的序号拉回合法范围
                if (index < 0)
                    index = 0;
                if (index >= list.Count)
                    index = list.Count - 1;

                return list[index]; //根据索引返回元组对象new ShopCategory(xxxx...
            }
        }

        protected virtual string BestiaryText => ""; //图鉴文本
        protected virtual string NPCNameText => ""; //NPC名字
        protected abstract int AttackProjType { get; } //射弹ID
        protected virtual int AttackProjDamage => 3; //射弹基础伤害

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
    }
}
