using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
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
            Item.useAnimation = 30;
            Item.useTime = 30;
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

        /*public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BlizzardinaBottle)
                .AddIngredient(ItemID.Cloud, 30)
                .AddIngredient(ItemID.RainCloud, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }*/

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f));
            return false;
        }
    }
}
