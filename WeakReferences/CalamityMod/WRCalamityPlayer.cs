using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Rogue;
using LaziestNPC.Content.Buffs.Potions;
using LaziestNPC.Common.Helpers;
using LaziestNPC.WeakReferences.CalamityMod;

namespace LaziestNPC.Globals.GlobalMods.WeakReferences.CalamityMod
{
    public class WRCalamityPlayer : ModPlayer
    { 
        public override void PostUpdateMiscEffects()
        {
            if (!Player.LaziestNPC().penumbra)
                return;

            if (!ModLoader.HasMod("CalamityMod"))
                return;

            WRCalamityHelper.ApplyCalamityPenumbraPotion(Player);
        }
    }
}
