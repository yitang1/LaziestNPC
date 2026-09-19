using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;

namespace LaziestNPC.Content.Items.Potions
{
	public class YharimsStimulants : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
            Item.ResearchUnlockCount = 30;
        }

		public override void SetDefaults()
		{
            Item.DefaultToFood(40, 40, BuffType<YharimPower>(), 18000, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("AnyFood")
				.AddIngredient(ItemID.EndurancePotion)
				.AddIngredient(ItemID.IronskinPotion)
				.AddIngredient(ItemID.SwiftnessPotion)
				.AddIngredient(ItemID.TitanPotion)
				.AddTile(TileID.AlchemyTable)
				.Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity) 
				&& Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{
				CreateRecipe()
				.AddIngredient(ItemID.BottledWater)
				.AddIngredient(BloodOrb, 50)
				.AddTile(TileID.AlchemyTable)
				.Register();
			}
		}
	}
}