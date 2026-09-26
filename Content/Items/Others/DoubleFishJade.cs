using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.Rarities;

namespace LaziestNPC.Content.Items.Others
{
    public class DoubleFishJade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 71;
            Item.height = 76;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ModContent.RarityType<Rainbow>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.DoubleFishJade.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.DoubleFishJade.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddCustomShimmerResult(ItemType<DoubleFishJade>(), 2)
            .AddIngredient(ItemType<DoubleFishJade>())
            .AddCondition(Condition.NearShimmer)
            .Register();
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod != "Terraria" || line.Name != "ItemName")
                return true;

            var font = FontAssets.MouseText.Value;
            var sb = Main.spriteBatch;
            Vector2 startPos = new Vector2(line.X, line.Y);
            Vector2 scale = line.BaseScale;

            Color topColor = new Color(119, 234, 4);
            Color midColor = new Color(80, 240, 180);
            Color bottomColor = new Color(11, 241, 223);

            string text = line.Text;
            int charCount = text.Length;
            if (charCount == 0)
                return false;

            float accumulatedWidth = 0f;

            for (int i = 0; i < charCount; i++)
            {
                char c = text[i];
                string s = c.ToString();
                float charWidth = font.MeasureString(s).X * scale.X;

                float t = charCount > 1 ? (float)i / (charCount - 1) : 0f;

                //两段插值：0~0.5, 0.5~1 
                Color charColor = t < 0.5f
                    ? Color.Lerp(topColor, midColor, t * 2f)
                    : Color.Lerp(midColor, bottomColor, (t - 0.5f) * 2f);

                ChatManager.DrawColorCodedStringWithShadow(sb, font, s,
                    startPos + new Vector2(accumulatedWidth, 0),
                    charColor, 0, Vector2.Zero, scale);

                accumulatedWidth += charWidth;
            }

            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.5f, new Vector2(0f, 0f));
            return false;
        }
    }
}
