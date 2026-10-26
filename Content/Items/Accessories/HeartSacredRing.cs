using System;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LaziestNPC.Common.Helpers;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria.Localization;

namespace LaziestNPC.Content.Items.Accessories
{
    public class HeartSacredRing : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(54, 44);
            Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 50, 0));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.LaziestNPC().HeartSacredRing = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BandofRegeneration)
                .AddIngredient(ItemID.PanicNecklace)
                .AddIngredient(ItemID.AegisCrystal, 2)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.HeartSacredRing.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.HeartSacredRing.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f));
            return false;
        }
    }
}
