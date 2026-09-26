using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Globals.GlobalSystem;

namespace LaziestNPC.Globals.GlobalItems
{
    public static class ItemCondition
    {
        public static Condition CanBuyFishJade = new Condition("CanBuyFishJade", () => LNPCSystem.fishJadeBuyCount < 3);
    }
}
