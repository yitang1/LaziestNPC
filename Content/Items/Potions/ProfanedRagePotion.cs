using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;
using LaziestNPC.Content.Items.Materials;

namespace LaziestNPC.Content.Items.Potions
{
	public class ProfanedRagePotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 30;
		}

		public override void SetDefaults()
		{
            Item.DefaultToFood(34, 42, BuffType<ProfanedRageBuff>(), 18000, true);
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(0, 1, 50, 0);
		}

		public override void AddRecipes()
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
                && Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb)
                && Calamity.TryFind<ModItem>("UnholyEssence", out ModItem UnholyEssence))
			{
                CreateRecipe()
					.AddIngredient(ItemID.RagePotion)
					.AddIngredient(UnholyEssence)
					.AddIngredient(ItemType<GalacticaSingularity>())
					.AddTile(TileID.AlchemyTable)
					.Register();

				CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(UnholyEssence)
					.AddIngredient(BloodOrb, 40)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
			else
			{
                CreateRecipe()
					.AddIngredient(ItemID.RagePotion)
					.AddIngredient(ItemType<GalacticaSingularity>())
					.AddTile(TileID.AlchemyTable)
					.Register();
            }
        }
	}
}