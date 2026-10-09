using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Terraria.Localization;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.Rarities;

namespace LaziestNPC.Content.Items.Consumables
{
    public class CallingSeaBottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 79;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.rare = ModContent.RarityType<Rainbow>();
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.CallingSeaBottle.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.CallingSeaBottle.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            #region [原版]
            //鱼-森林 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Bass)); //鲈鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Salmon)); //三文鱼
            //鱼-洞穴 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.ArmoredCavefish)); //装甲洞穴鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.SpecularFish)); //镜面鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Stinkfish)); //臭味鱼
            //itemLoot.Add(ItemDropRule.Common(ItemID.BombFish)); //炸弹鱼
            //鱼-沙漠 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Oyster)); //牡蛎
            itemLoot.Add(ItemDropRule.Common(ItemID.Flounder)); //偏口鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.RockLobster)); //岩石龙虾
            //鱼-雪原 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.AtlanticCod)); //大西洋鳕鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.FrostMinnow)); //寒霜鲦鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.FrostDaggerfish)); //寒霜飞鱼
            //鱼-太空 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Damselfish)); //雀鲷
            //鱼-海洋 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Trout)); //鳟鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Tuna)); //金枪鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.RedSnapper)); //红鲷鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Shrimp)); //虾
            //鱼-丛林 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.NeonTetra)); //霓虹脂鲤
            itemLoot.Add(ItemDropRule.Common(ItemID.VariegatedLardfish)); //斑驳油鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.DoubleCod)); //双鳍鳕鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Honeyfin)); //蜂蜜鱼
            //鱼-猩红之地 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.CrimsonTigerfish)); //猩红虎鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Hemopiranha)); //血腥食人鱼
            //鱼-腐化之地 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Ebonkoi)); //黑檀锦鲤
            //鱼-熔岩 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Obsidifish)); //黑曜石鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.FlarefinKoi)); //闪鳍锦鲤
            //鱼-神圣之地(肉后) ↓
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.PrincessFish)); //公主鱼
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.Prismite)); //七彩矿鱼
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.ChaosFish)); //混沌鱼

            #endregion

            /*#region [灾厄 Calamity Mod]
            LNPCHelper.AddModItemLoot(itemLoot, "CalamityMod", "RoverDrive");
            LNPCHelper.AddModItemLoot(itemLoot, "CalamityMod", "AncientFossil");

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
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.6f, new Vector2(0f, 0f));
            return false;
        }
    }
}
