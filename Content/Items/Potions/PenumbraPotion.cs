using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;
using Microsoft.Xna.Framework.Input;

namespace LaziestNPC.Content.Items.Potions
{
	public class PenumbraPotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 30;
		}

		public override void SetDefaults()
		{
            Item.DefaultToFood(26, 30, BuffType<PenumbraBuff>(), 28800, true);
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.buyPrice(0, 1, 25, 0);
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string cText = "";
            string vText = "";

            if (ModLoader.HasMod("CalamityMod"))
            {
                cText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.PenumbraPotion.CalamityText");
            }
            else
            {
                vText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.PenumbraPotion.VanillaText");
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
			&& Calamity.TryFind<ModItem>("SolarVeil", out ModItem SolarVeil)
			&& Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{

                CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(SolarVeil, 3)
					.AddIngredient(ItemID.LunarTabletFragment)
					.AddTile(TileID.AlchemyTable)
					.Register();

				CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(BloodOrb, 30)
					.AddIngredient(SolarVeil)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
			else
			{
                CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(ItemID.Ectoplasm, 2)
					.AddIngredient(ItemID.LunarTabletFragment)
					.AddTile(TileID.AlchemyTable)
					.Register();
            }
        }
	}
}