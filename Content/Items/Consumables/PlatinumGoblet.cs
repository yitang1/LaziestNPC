using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.Rarities;
using LaziestNPC.Globals.GlobalItems;
using LaziestNPC.Common.ModBossess;
using LaziestNPC.Globals.GlobalNPCs;

namespace LaziestNPC.Content.Items.Consumables
{
    public class PlatinumGoblet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 59;
            Item.height = 59;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.rare = ModContent.RarityType<Rainbow>();
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.PlatinumGoblet.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.PlatinumGoblet.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            #region [原版]
            //肉前
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedSlimeKing), ItemID.KingSlimeTrophy)); //史莱姆王纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedBoss1), ItemID.EyeofCthulhuTrophy)); //克苏鲁之眼纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPCCondition.downedBrainOfCthulhu), ItemID.BrainofCthulhuTrophy)); //克苏鲁之脑纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPCCondition.downedEaterOfWorlds), ItemID.EaterofWorldsTrophy)); //世界吞噬怪纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedQueenBee), ItemID.QueenBeeTrophy)); //蜂王纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedBoss3), ItemID.SkeletronTrophy)); //骷髅王纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedDeerclops), ItemID.DeerclopsTrophy)); //独眼巨鹿纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => Main.hardMode), ItemID.WallofFleshTrophy)); //血肉墙纪念章

            //肉后
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedQueenSlime), ItemID.QueenSlimeTrophy)); //史莱姆皇后纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedMechBoss2), ItemID.RetinazerTrophy)); //激光眼纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedMechBoss2), ItemID.SpazmatismTrophy)); //魔焰眼纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedMechBoss1), ItemID.DestroyerTrophy)); //毁灭者纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedMechBoss3), ItemID.SkeletronPrimeTrophy)); //机械骷髅王纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedPlantBoss), ItemID.PlanteraTrophy)); //世纪之花纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedGolemBoss), ItemID.GolemTrophy)); //石巨人纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedEmpressOfLight), ItemID.FairyQueenTrophy)); //光之女皇纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedFishron), ItemID.DukeFishronTrophy)); //猪龙鱼公爵纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedAncientCultist), ItemID.AncientCultistTrophy)); //拜月教邪教徒纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedMoonlord), ItemID.MoonLordTrophy)); //月亮领主纪念章

            //事件
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPCCondition.downedDarkMage), ItemID.BossTrophyDarkmage)); //黑暗魔法师纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPCCondition.downedOgre), ItemID.BossTrophyOgre)); //食人魔纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPCCondition.downedBetsy), ItemID.BossTrophyBetsy)); //双足翼龙纪念章
            
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedHalloweenTree), ItemID.MourningWoodTrophy)); //哀木纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedHalloweenKing), ItemID.PumpkingTrophy)); //南瓜王纪念章

            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedChristmasTree), ItemID.EverscreamTrophy)); //常绿尖叫怪纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedChristmasSantank), ItemID.SantaNK1Trophy)); //圣诞坦克纪念章
            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedChristmasIceQueen), ItemID.IceQueenTrophy)); //冰雪女王纪念章

            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedPirates), ItemID.FlyingDutchmanTrophy)); //荷兰飞盗船纪念章

            itemLoot.Add(ItemDropRule.ByCondition(new DropCondition(() => NPC.downedMartians), ItemID.MartianSaucerTrophy)); //火星飞碟纪念章

            #endregion

            /*#region [灾厄 Calamity Mod]
            LNPCHelper.AddModItemLoot(itemLoot, "CalamityMod", "DesertScourgeTrophy", new DropCondition(() => ModBosses.IsDowned("DesertScourge")));
            #endregion*/

        }

        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            string plain = "";
            int i = 0;
            while (i < line.Text.Length)
            {
                if (i + 2 < line.Text.Length && line.Text[i] == '[' && line.Text[i + 1] == 'c' && line.Text[i + 2] == '/')
                {
                    int colon = line.Text.IndexOf(':', i);
                    if (colon < 0)
                    {
                        plain += line.Text[i];
                        i++;
                    }
                    else
                    {
                        i = colon + 1;
                        while (i < line.Text.Length && line.Text[i] != ']')
                        {
                            plain += line.Text[i];
                            i++;
                        }
                        if (i < line.Text.Length)
                            i++;
                    }
                }
                else
                {
                    plain += line.Text[i];
                    i++;
                }
            }

            if (plain.Contains("超凡之物"))
            {
                int index = plain.IndexOf("超凡之物");
                string beforeText = plain.Substring(0, index);
                Vector2 beforeSize = FontAssets.MouseText.Value.MeasureString(beforeText) * line.BaseScale;
                Vector2 targetSize = FontAssets.MouseText.Value.MeasureString("超凡之物") * line.BaseScale;

                float startX = line.X + beforeSize.X;
                float y = line.Y;
                float width = targetSize.X;
                float height = targetSize.Y;

                Main.spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    new Rectangle((int)startX, (int)(y + height * 0.3f), (int)width, 1),
                    new Color(128, 128, 128));
            }
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.75f, new Vector2(0f, 0f));
            return false;
        }
    }
}
