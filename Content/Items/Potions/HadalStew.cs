using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LaziestNPC.Content.Items.Potions
{
    public class HadalStew : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Type] = new Color[4] {
                new Color(185, 117, 70),
                new Color(214, 98, 44),
                new Color(235, 156, 117),
                new Color(89, 54, 46)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToHealingPotion(28, 18, 120);
            Item.healMana = 150;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 0, 75, 0);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string duration = (Main.LocalPlayer.pStone ? 50 * 0.75f : 50).ToString("N1");

            TooltipLine line = tooltips.FirstOrDefault(x => x.Mod == "Terraria" && x.Text.Contains("[PotionSick]"));
            if (line != null)
                line.Text = line.Text.Replace("[PotionSick]", duration);
        }

        public override bool CanUseItem(Player player)
        {
            return player.potionDelay <= 0;
        }

        public static int BuffDuration = 18000; // 5x60x60 5分钟
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(BuffDuration / 3600);

        public override void OnConsumeItem(Player player) => player.AddBuff(BuffID.WellFed3, BuffDuration);

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Obsidifish, 2)
                .AddIngredient(ItemID.Honeyfin, 2)
                .AddIngredient(ItemID.Bowl)
                .AddTile(TileID.CookingPots)
                .Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
                && Calamity.TryFind<ModItem>("AbyssGravel", out ModItem AbyssGravel)
                && Calamity.TryFind<ModItem>("Voidstone", out ModItem Voidstone)
                && Calamity.TryFind<ModItem>("CoastalDemonfish", out ModItem CoastalDemonfish))
            {
                CreateRecipe()
                    .AddIngredient(AbyssGravel.Type, 3)
                    .AddIngredient(CoastalDemonfish.Type, 2)
                    .AddIngredient(ItemID.Bowl)
                    .AddTile(TileID.CookingPots)
                    .Register();

                CreateRecipe()
                    .AddIngredient(Voidstone.Type, 3)
                    .AddIngredient(CoastalDemonfish.Type, 2)
                    .AddIngredient(ItemID.Bowl)
                    .AddTile(TileID.CookingPots)
                    .Register();
            }
        }
    }
}
