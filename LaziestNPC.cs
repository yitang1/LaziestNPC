using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using LaziestNPC.Common.GlobalNPCs;

namespace LaziestNPC
{
	public class LaziestNPC : Mod
	{
        public override void Load()
        {
            CaughtNPCItem.RegisterItems();
            Terraria.ModLoader.Logging.IgnoreExceptionSource("System.Net");
        }

        // “创建物品、加载默认属性、设置商店价格”打包成一个方法
        public static Item CustomPrice(int type, int price)
        {
            var item = new Item();
            item.SetDefaults(type);
            item.shopCustomPrice = price;
            return item;
        }
    }
}
