using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalItems
{
    public static class ShopItemExpand
    {
        /// <summary>
        /// 添加一个物品到商店，使用元组格式的价格(铂金, 金币, 银币, 铜币)。
        /// </summary>
        public static NPCShop AddItem(this NPCShop shop, int itemId, (int plat, int gold, int silver, int copper) price, params Condition[] conditions)
        {
            int copperPrice = price.copper + price.silver * 100 + price.gold * 10000 + price.plat * 1000000;
            Item item = new Item(itemId);
            item.shopCustomPrice = copperPrice;
            shop.Add(item, conditions);
            return shop;
        }

        /// <summary>
        /// 尝试从其他模组添加一个物品到商店。如果该模组未加载或物品不存在，则什么都不做。
        /// </summary>
        /// <param name="shop">当前商店对象</param>
        /// <param name="modItemPath">格式为 "模组名/物品类名"，例如"CatalystMod/AstrageldonBag"</param>
        /// <param name="price">自定义价格（铜币），通常由 buyPrice 方法生成</param>
        /// <param name="conditions">可选的解锁条件，不传则无条件</param>
        /// <returns>返回 shop 自身，支持链式调用</returns>
        public static NPCShop AddModItem(this NPCShop shop, string modItemPath, (int plat, int gold, int silver, int copper) price, params Condition[] conditions)
        {
            int copperPrice = price.copper + price.silver * 100 + price.gold * 10000 + price.plat * 1000000;
            //尝试查找该物品，如果没有只会抛出静默异常，不会出现报错
            if (ModContent.TryFind<ModItem>(modItemPath, out ModItem modItem))
            {
                //创建一个新物品实例
                Item item = new Item(modItem.Type);
                //设置自定义价格，转化后的铜币信息传入shopCustomPrice
                item.shopCustomPrice = copperPrice;

                //添加到商店，传入条件
                shop.Add(item, conditions);
            }
            //如果物品不存在，不执行任何操作，直接返回shop对象(继续链式调用)
            return shop;
        }
    }
}
