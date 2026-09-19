using System;
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
	public class TriumphPotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
            Item.ResearchUnlockCount = 30;
        }

		public override void SetDefaults()
		{
			Item.DefaultToFood(30, 34, BuffType<TriumphBuff>(), 18000, true);
			Item.rare = ItemRarityID.Green;
			Item.value = Item.buyPrice(0, 0, 75, 0);
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //根据是否按住左Shift，决定要追加的文本
            string newText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                newText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.TriumphPotion.ContentTexts");
            }
            else
            {
                newText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.TriumphPotion.CommonTips");
            }

            tooltips.Add(new TooltipLine(Mod, "TriumphPotion_newText", newText));
        }

        public override void AddRecipes()
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
				&& Calamity.TryFind<ModItem>("PearlShard", out ModItem PearlShard) 
				&& Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb))
			{
				CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(PearlShard.Type, 3)
					.AddTile(TileID.Bottles)
					.Register();

				CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(BloodOrb.Type, 30)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
			else
			{
                CreateRecipe(5)
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(ItemID.RoyalGel)
					.AddTile(TileID.Bottles)
					.Register();
            }
        }
	}
}