using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Globals.GlobalSystem;
using Terraria.GameContent.ItemDropRules;

namespace LaziestNPC.Globals.GlobalItems
{
    public static class ItemCondition
    {
        public static Condition CanBuyFishJade = new Condition("CanBuyFishJade", () => WorldCondition.FishJadeBuyCount < 3);
    }

    public class DownedSkeletron : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
        public bool CanShowItemDropInUI() => false;
        public string GetConditionDescription() => null;
    }
}
