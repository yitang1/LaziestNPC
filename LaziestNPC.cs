using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using LaziestNPC.Globals.GlobalNPCs;
using LaziestNPC.Common.UI;
using System.IO;
using Terraria.ID;

namespace LaziestNPC
{
    public class LaziestNPC : Mod
	{
        public override void Load()
        {
            CaughtNPC.RegisterItems();

            string[] ignoredSources = new string[]
            {
                "System.Net.Security",
                "System.Net.Http",
            };
            foreach (string source in ignoredSources)
            {
                Terraria.ModLoader.Logging.IgnoreExceptionSource(source);
            }
        }

        //在联机时，客户端执行操作后，会向服务端发送一个数据包(通过ModPacket)，
        //服务端接收到后执行实际的状态变更，
        //然后服务端需将该变更广播给所有客户端，否则客户端之间会出现特喵的“不同步”
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte packetId = reader.ReadByte();
            if (packetId == 20)
            {
                //读取操作类型
                byte action = reader.ReadByte();
                if (Main.netMode == NetmodeID.Server)
                {
                    Player player = Main.player[whoAmI];
                    switch (action)
                    {   
                        //下雨/停雨
                        case 1: 
                            bool rainOn = reader.ReadBoolean();
                            ItemControlUI.ToggleRain(player, rainOn);
                            break;
                        //时间切换
                        case 2: 
                            int timeType = reader.ReadInt32();
                            ItemControlUI.ToggleDayTime(player, timeType);
                            break;
                        //取消事件
                        case 3: 
                            ItemControlUI.CancelEvents();
                            break;
                        //未知操作，忽略
                        default:
                            break;
                    }
                    //广播世界状态给所有客户端
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }

        // “创建物品、加载默认属性、设置商店价格”打包成一个方法(临时退休)
        /*public static Item CustomPrice(int type, int price)
        {
            var item = new Item();
            item.SetDefaults(type);
            item.shopCustomPrice = price;
            return item;
        }*/
    }
}
