using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalRecipes
{
    public class VanillaTreasureBagTrophy : ModSystem
    {
        public override void AddRecipes()
        {
            if (ModLoader.HasMod("Fargowiltas"))
            {
                return;
            }

            /*
            Recipe.Create(ItemID.WhoopieCushion)
                .AddIngredient(ItemID.Leather, 5)
                .AddIngredient(ItemID.Cloud, 5)
                .AddIngredient(ItemID.StinkPotion)
                .AddTile(TileID.WorkBenches)
                .Register();*/

        }
    }
}
