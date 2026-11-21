using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LaziestNPC.Globals.GlobalPlayers
{
    //NPC关于事件触发时的特殊台词，是否触发的状态，绑定于玩家
    public class TownLNPCPlayer : ModPlayer
    {
        //战后词条记录
        public IList<string> OnceBossChats = new List<string>();
        //NPC商店索引记录
        public Dictionary<string, int> ShopCategoryIndex = new();

        public override void SaveData(TagCompound tag)
        {
            tag["OnceBossChats"] = OnceBossChats;

            List<string> keys = new();
            List<int> values = new();
            foreach (KeyValuePair<string, int> kv in ShopCategoryIndex)
            {
                keys.Add(kv.Key);
                values.Add(kv.Value);
            }

            tag["ShopCategoryIndexKeys"] = keys;
            tag["ShopCategoryIndexValues"] = values;
        }

        public override void LoadData(TagCompound tag)
        {
            OnceBossChats = tag.GetList<string>("OnceBossChats") ?? new List<string>();

            ShopCategoryIndex.Clear();

            if (tag.TryGet("ShopCategoryIndexKeys", out List<string> keys)
                && tag.TryGet("ShopCategoryIndexValues", out List<int> values))
            {
                for (int i = 0; i < keys.Count && i < values.Count; i++)
                    ShopCategoryIndex[keys[i]] = values[i];
            }
        }

        //记录上一帧的事件状态(每个玩家客户端独立计算)
        private bool lastBloodMoon = false;

        public override void PostUpdate()
        {
            if (Main.gameMenu) return;

            //边沿检测：只在事件发生的第一帧触发
            //事件发生时，!lastBloodMoon为false，就不会重复添加字符串
            if (Main.bloodMoon && !lastBloodMoon)
            {
                if (!OnceBossChats.Contains("BloodMoon"))
                    OnceBossChats.Add("BloodMoon");
            }
            //重置，更新上一帧的状态，事件结束时变回false
            lastBloodMoon = Main.bloodMoon;
        }
    }
}
