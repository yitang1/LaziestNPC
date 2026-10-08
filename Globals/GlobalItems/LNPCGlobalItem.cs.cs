using System;
using System.Collections.Generic;
using LaziestNPC.Common.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalItems
{
    public class LNPCGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //物品是不是本Mod的
            if (item.ModItem != null && item.ModItem.Mod.Name == "LaziestNPC")
            {
                Color defaultColor = new Color(251, 251, 224);
                bool LColorStarted = false;

                foreach (TooltipLine line in tooltips)
                {
                    //检测工具提示部分
                    if (!line.Name.StartsWith("Tooltip"))
                        continue;

                    //工具提示里遇到[L]标记，去掉标记，为之后的内容开始进行颜色替换
                    if (line.Text.Contains("[L]"))
                    {
                        line.Text = line.Text.Replace("[L]", "");
                        LColorStarted = true;
                    }

                    if (LColorStarted)
                    {
                        line.OverrideColor = defaultColor;
                    }
                }
            }
        }
    }
}
