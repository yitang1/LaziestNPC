using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.UI.Chat;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Localization;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.UI;
using LaziestNPC.Common.Rarities;

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
            Item.width = 62;
            Item.height = 76;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.rare = ModContent.RarityType<Rainbow>();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.TheReturner.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.TheReturner.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        //是否可以右键
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        //物品使用过后发生的逻辑，即执行使用后的具体效果
        public override bool? UseItem(Player player)
        {
            //右键
            ModContent.GetInstance<ItemControlUISystem>().ShowUI();
            return true;
        }
        //物品是否能使用
        public override bool CanUseItem(Player player)
        {
            return player.altFunctionUse == 2;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod != "Terraria" || line.Name != "ItemName")
                return true;

            var font = FontAssets.MouseText.Value;
            var sb = Main.spriteBatch;
            Vector2 startPos = new Vector2(line.X, line.Y);
            Vector2 scale = line.BaseScale;

            Color topColor = new Color(255, 206, 163);
            Color midColor = new Color(255, 150, 90);
            Color bottomColor = new Color(255, 89, 52);

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
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f));
            return false;
        }
    }
}
