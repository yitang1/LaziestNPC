using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;
using System.Collections.Generic;
using Terraria.Localization;

namespace LaziestNPC.Content.Items.Potions
{
	public class ShatteringPotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 30;
		}

		public override void SetDefaults()
		{
            Item.DefaultToFood(26, 32, BuffType<ArmorShattering>(), 28800, true);
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(0, 1, 25, 0);
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string cText = "";
            string vText = "";

            if (ModLoader.HasMod("CalamityMod"))
            {
                cText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.ShatteringPotion.CalamityText");
            }
            else
            {
                vText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.ShatteringPotion.VanillaText");
            }

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[CalamityMod]", cText);
                line.Text = line.Text.Replace("[Vanilla]", vText);
            }
        }

        public override void AddRecipes()
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
				&& Calamity.TryFind<ModItem>("FlaskOfCrumbling", out ModItem FlaskOfCrumbling)
				&& Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{
                CreateRecipe()
					.AddIngredient(FlaskOfCrumbling, 2)
					.AddIngredient(ItemID.BeetleHusk)
					.AddTile(TileID.AlchemyTable)
					.Register();

				CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(BloodOrb, 30)
					.AddIngredient(ItemID.BeetleHusk)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
			else
			{
                CreateRecipe()
					.AddIngredient(ItemID.FlaskofIchor, 2)
					.AddIngredient(ItemID.BeetleHusk)
					.AddTile(TileID.AlchemyTable)
					.Register();
            }
        }
	}
}