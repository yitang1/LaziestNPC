using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Content.Projectiles;

namespace LaziestNPC.Content.Items.Consumables
{
    public class ThrownShimmerWater : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.consumable = true;
            Item.damage = 0;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.useStyle = 1;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<ShimmerWater>();
            Item.shootSpeed = 9f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Daybloom)
                .AddIngredient(ItemID.Moonglow)
                .AddIngredient(ItemID.Blinkroot)
                .AddCondition(Condition.NearShimmer)
                .Register();
        }
    }

}
