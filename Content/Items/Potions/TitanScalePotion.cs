using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;

namespace LaziestNPC.Content.Items.Potions
{
	public class TitanScalePotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
            Item.ResearchUnlockCount = 30;
        }

		public override void SetDefaults()
		{
            Item.DefaultToFood(24, 34, BuffType<TitanScale>(), 28800, true);
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(0, 1, 25, 0); 
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.TitanPotion)
				.AddIngredient(ItemID.BeetleHusk)
				.AddTile(TileID.AlchemyTable)
				.Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
                && Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{
                CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(BloodOrb, 10)
					.AddIngredient(ItemID.BeetleHusk)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
		}
	}
}