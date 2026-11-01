using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Common.Helpers;

namespace LaziestNPC.Globals.GlobalRecipes
{
    public class AddOtherModRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("AlchemistNPCLite", out Mod AlchemistNPCLite))
            {
                //万能药剂包
                Recipe.Create(AlchemistNPCLite.Find<ModItem>("UniversalCombination").Type)
                    .AddModMaterial(AlchemistNPCLite, "VanTankCombination")
                    .AddModMaterial(AlchemistNPCLite, "TankCombination")
                    .AddModMaterial(AlchemistNPCLite, "RangerCombination")
                    .AddModMaterial(AlchemistNPCLite, "MageCombination")
                    .AddModMaterial(AlchemistNPCLite, "SummonerCombination")
                    .AddTile(TileID.AlchemyTable)
                    .Register();
            }
        }
    }
}
