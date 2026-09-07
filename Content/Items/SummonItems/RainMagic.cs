using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace LaziestNPC.Content.Items.SummonItems
{
    public class RainMagic : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item4;
            Item.maxStack = 1;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.raining)
            {
                RainOff();
            }
            else if (!Main.raining && !Main.slimeRain)
            {
                RainOn();
            }
            return true;
        }

        private static void RainOff()
        {
            Main.NewText("雨停了！", new Color(0, 128, 255));
            Main.StopRain();
        }

        private static void RainOn()
        {
            Main.NewText("开始下雨！", new Color(0, 128, 255));
            int day = 86400;
            int hour = day / 24;
            Main.rainTime = hour * 12;
            Main.raining = true;
            Main.maxRaining = Main.cloudAlpha = 0.9f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BlizzardinaBottle)
                .AddIngredient(ItemID.Cloud, 30)
                .AddIngredient(ItemID.RainCloud, 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
