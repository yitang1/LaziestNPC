using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.CalPlayer;

namespace LaziestNPC.WeakReferences.CalamityMod
{
    [JITWhenModsEnabled("CalamityMod")]
    public static class WRCalamityHelper
    {
        //灾厄Mod 半影药水 增强盗贼职业属性
        public static void ApplyCalamityPenumbraPotion(Player player)
        {
            CalamityPlayer calamityPlayer = player.Calamity();

            if (Main.eclipse || calamityPlayer.umbraphileSet)
            {
                calamityPlayer.stealthGenStandstill += 0.2f;
                calamityPlayer.stealthGenMoving += 0.2f;
            }
            else
            {
                calamityPlayer.stealthGenStandstill += 0.15f;
                calamityPlayer.stealthGenMoving += 0.15f;
            }
        }
    }
}
