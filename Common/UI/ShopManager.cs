using System.Collections.Generic;

namespace LaziestNPC.Common.UI
{
    public class ShopCategory
    {
        public string DisplayKey;
        public string ShopName;
    }

    public static class ShopManager
    {
        private static Dictionary<string, List<ShopCategory>> _categoryMap = new Dictionary<string, List<ShopCategory>>();
        private static Dictionary<string, string> _targetShopMap = new Dictionary<string, string>();

        // 注册某个NPC的分类列表
        public static void RegisterCategories(string npcClass, List<ShopCategory> categories)
        {
            if (!_categoryMap.ContainsKey(npcClass))
                _categoryMap[npcClass] = categories;
            else
                _categoryMap[npcClass] = categories; // 可覆盖
        }

        // 获取某个NPC的分类列表
        public static List<ShopCategory> GetCategories(string npcClass)
        {
            _categoryMap.TryGetValue(npcClass, out var list);
            return list;
        }

        // 设置某个NPC当前选中的商店
        public static void SetTargetShop(string npcClass, string shopName)
        {
            _targetShopMap[npcClass] = shopName;
        }

        // 获取某个NPC当前选中的商店
        public static string GetTargetShop(string npcClass)
        {
            _targetShopMap.TryGetValue(npcClass, out var shop);
            return shop ?? "Default";
        }
    }
}
