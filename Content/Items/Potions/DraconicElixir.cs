using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using LaziestNPC.Content.Buffs.Potions;
using LaziestNPC.Content.Items.Materials;

namespace LaziestNPC.Content.Items.Potions
{
	public class DraconicElixir : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
		{
            /* ticksperframe:每张图停留的时间帧tick，frameCount:贴图里动画帧的数量
            (8, 10):这10张图里每张图都停留8tick，泰拉里60tick为现实1秒，
			因此，此物品的贴图会每1.33秒播放一次完整的动画 */
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(8, 10));
            Item.ResearchUnlockCount = 30;
		}

		public override void SetDefaults()
		{
            Item.DefaultToFood(50, 44, BuffType<DraconicSurgeBuff>(), 28800, true);
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity)
				&& calamity.TryFind<ModRarity>("BurnishedAuric", out ModRarity BurnishedAuric))
            {
                Item.rare = BurnishedAuric.Type;
            }
            else
            {
                Item.rare = ItemRarityID.Red;
            }
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string cText = "";
            string vText = "";

            if (ModLoader.HasMod("CalamityMod"))
            {
                cText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.DraconicElixir.CalamityText");
            }
            else
            {
                vText = Language.GetTextValue("Mods.LaziestNPC.Items.Potions.DraconicElixir.VanillaText");
            }

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[CalamityMod]", cText);
                line.Text = line.Text.Replace("[Vanilla]", vText);
            }

            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "ItemName")
                {
                    line.OverrideColor = new Color(254, 233, 128);
                    break;
                }
            }
        }

		public override void AddRecipes()
		{
            if (ModLoader.TryGetMod("CalamityMod", out Mod Calamity)
                && Calamity.TryFind<ModItem>("BloodOrb", out ModItem BloodOrb)
                && Calamity.TryFind<ModItem>("YharonSoulFragment", out ModItem YharonSoulFragment))
			{
                CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(YharonSoulFragment)
					.AddIngredient(ItemID.Daybloom)
					.AddIngredient(ItemID.Moonglow)
					.AddIngredient(ItemID.Fireblossom)
					.AddTile(TileID.AlchemyTable)
					.Register();

				CreateRecipe()
					.AddIngredient(ItemID.BottledWater)
					.AddIngredient(BloodOrb, 50)
					.AddIngredient(YharonSoulFragment)
					.AddTile(TileID.AlchemyTable)
					.Register();
			}
			else
			{
                CreateRecipe(4)
                    .AddIngredient(ItemID.BottledWater)
                    .AddIngredient(ItemID.DD2BetsyPetItem)
                    .AddIngredient(ItemID.LunarOre, 4)
                    .AddIngredient(ItemType<GalacticaSingularity>(), 4)
                    .AddTile(TileID.AlchemyTable)
                    .Register();
            }
        }
	}
}