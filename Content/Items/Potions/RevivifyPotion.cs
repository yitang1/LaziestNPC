using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;

namespace LaziestNPC.Content.Items.Potions
{
	public class RevivifyPotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
            Item.ResearchUnlockCount = 30;
        }

		public override void SetDefaults()
		{
            Item.DefaultToFood(28, 36, BuffType<Revivify>(), 18000, true);
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(0, 1, 25, 0);
		}

		public override void AddRecipes()
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
                && Calamity.TryFind<ModItem>("StarblightSoot", out ModItem StarblightSoot)
                && Calamity.TryFind<ModItem>("EssenceofSunlight", out ModItem EssenceofSunlight)
                && Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{
                CreateRecipe(5)
					.AddIngredient(StarblightSoot, 20)
					.AddIngredient(ItemID.HolyWater, 5)
					.AddIngredient(ItemID.CrystalShard, 5)
					.AddIngredient(EssenceofSunlight, 3)
					.AddTile(TileID.AlchemyTable)
					.Register();

				CreateRecipe()
					.AddIngredient(ItemID.HolyWater)
					.AddIngredient(BloodOrb, 20)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
			else
			{
                CreateRecipe()
					.AddIngredient(ItemID.HolyWater)
					.AddIngredient(ItemID.SoulofLight, 4)
                    .AddIngredient(ItemID.CrystalShard)
                    .AddTile(TileID.AlchemyTable)
					.Register();
            }
        }
	}
}