using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalRecipes
{
    public class GlobalRecipeGroups : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //通过食物增益来查找收集全部“食物”
            List<int> foodItems = new List<int>();
            foreach (var kv in ContentSamples.ItemsByType)
            {
                if (BuffID.Sets.IsWellFed[kv.Value.buffType])
                    foodItems.Add(kv.Value.type);
            }

            //注册任意食物组
            if (foodItems.Count > 0)
            {
                RecipeGroup AnyFood = new RecipeGroup(
                    () => Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroups.AnyFood"),
                    foodItems.ToArray()
                );
                RecipeGroup.RegisterGroup("AnyFood", AnyFood);
            }

        }
    }
}
