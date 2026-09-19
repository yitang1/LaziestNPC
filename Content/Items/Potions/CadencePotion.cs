using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;

namespace LaziestNPC.Content.Items.Potions
{
	public class CadencePotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
            Item.ResearchUnlockCount = 30;
        }

		public override void SetDefaults()
		{
            Item.DefaultToFood(22, 38, BuffType<CadenceBuff>(), 18000, true);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 1, 25, 0);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LovePotion)
				.AddIngredient(ItemID.RegenerationPotion)
				.AddIngredient(ItemID.HeartreachPotion)
				.AddIngredient(ItemID.LifeforcePotion)
				.AddTile(TileID.AlchemyTable)
				.Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
                && Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{
                CreateRecipe()
				.AddIngredient(ItemID.BottledWater)
				.AddIngredient(BloodOrb, 40)
                .AddTile(TileID.AlchemyTable)
                .Register();
			}
		}
	}
}