using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalRecipes
{
    public class GlobalRecipeGroup : ModSystem
    {
        public override void AddRecipeGroups()
        {
            /*原版Terraria默认就有的配方组：(没有的就需要自己创建)
            "Wood", "IronBar", "PresurePlate", "Sand", "Fragment",
            "Birds", "Scorpions", "Squirrels", "Bugs", "Ducks",
            "Butterflies", "Fireflies", "Snails", "Dragonflies",
            "Turtles", "Fruit".
            直接调用示例：.AddRecipeGroup("Butterflies")*/

            /*【注0】在同一个程序库里，配方组可以直接调用，不需要using引用。
            【注1】配方里显示的贴图是配方组里排名第一个物品的贴图。
            【注2】配方里物品排列的顺序优先级：
            1.与目标合成物品同一系列和主题的靠前
            2.稀有和珍贵程度高的靠前
            3.需求数量少的按顺序靠后
            【注3】
            如果一个物品的合成配方项目的种类比较少，那么其合成材料数量需求就比较多；
            (例如1个武器需要1个下级武器 + A材料x10 合成)

			如果合成配方的项目比较多，那材料的需求数量就应当减少。
            (例如1个武器需要A材料x2 + B材料x2 + C材料x1 + D材料x1 合成)

            【注4】当一个饰品，可以在肉前大前期不需要打任何Boss和事件就能获得时，制作站通常是铁砧而不是工匠作坊，
			也就是说，即使是在大前期，铁砧和工匠作坊制作的物品也具有“时期前后”、“稀有和贵重程度”的区别。*/

            #region 【各类材料】

            //铁锭或铅锭
            RecipeGroup IronBarGroups = new RecipeGroup(() =>
            Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroup.IronBarGroups"), ItemID.IronBar, ItemID.LeadBar);
            RecipeGroup.RegisterGroup("IronBarGroups", IronBarGroups);

            //银锭或钨锭
            RecipeGroup SilverBarGroups = new RecipeGroup(() => 
            Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroup.SilverOrTungstenBar"), ItemID.SilverBar, ItemID.TungstenBar);
            RecipeGroup.RegisterGroup("SilverBarGroups", SilverBarGroups);
            
            //铂金锭或金锭
            RecipeGroup PlatinumBarGroups = new RecipeGroup(() =>
            Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroup.PlatinumBarGroups"), ItemID.PlatinumBar, ItemID.GoldBar);
            RecipeGroup.RegisterGroup("PlatinumBarGroups", PlatinumBarGroups);

            //椎骨或腐肉 (任意邪恶材料)
            RecipeGroup PerEvilMaterial = new RecipeGroup(() =>
            Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroup.PerEvilMaterials"), ItemID.Vertebrae, ItemID.RottenChunk);
            RecipeGroup.RegisterGroup("PerEvilMaterial", PerEvilMaterial);

            #endregion

            #region 【其他物品】
            //铂金表或金表
            RecipeGroup PlatinumWatchGroups = new RecipeGroup(() =>
            Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroup.PlatinumWatchGroups"), ItemID.PlatinumWatch, ItemID.GoldWatch);
            RecipeGroup.RegisterGroup("PlatinumWatchGroups", PlatinumWatchGroups);
            
            #endregion


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
                    () => Language.GetTextValue("Mods.LaziestNPC.Others.RecipeGroup.AnyFood"),
                    foodItems.ToArray()
                );
                RecipeGroup.RegisterGroup("AnyFood", AnyFood);
            }

        }
    }
}
