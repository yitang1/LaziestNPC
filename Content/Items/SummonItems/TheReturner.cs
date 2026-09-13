using System;
using Terraria;
using Terraria.ID;
using Terraria.UI.Chat;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.UI;

namespace LaziestNPC.Content.Items.SummonItems
{
    public class TheReturner : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 52;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.maxStack = 1;
            Item.consumable = false;
        }

        //是否可以右键
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            //右键
            if (player.altFunctionUse == 2)
            {
                ModContent.GetInstance<ItemControlUISystem>().ShowUI();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod != "Terraria" || line.Name != "ItemName")
                return true;

            var font = FontAssets.MouseText.Value;
            var sb = Main.spriteBatch;
            Vector2 startPos = new Vector2(line.X, line.Y);
            Vector2 scale = line.BaseScale;

            Color topColor = new Color(255,206,163);
            Color bottomColor = new Color(255,89,52);

            string text = line.Text;
            float accumulatedWidth = 0f;
            float totalWidth = font.MeasureString(text).X * scale.X;

            foreach (char c in text)
            {
                string s = c.ToString();
                float charWidth = font.MeasureString(s).X * scale.X;
                float t = totalWidth > 0 ? (accumulatedWidth + charWidth / 2f) / totalWidth : 0f;
                Color charColor = Color.Lerp(topColor, bottomColor, t);

                ChatManager.DrawColorCodedStringWithShadow(sb, font, s,
                    startPos + new Vector2(accumulatedWidth, 0),
                    charColor, 0, Vector2.Zero, scale);

                accumulatedWidth += charWidth;
            }

            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f));
            return false;
        }
    }
}
