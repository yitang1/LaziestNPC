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
        public static Condition CanBuyFishJadeFirst = new Condition("CanBuyFishJadeFirst", () => WorldCondition.FishJadeBuyCount < 1);
        public static Condition CanBuyFishJadeSecond = new Condition("CanBuyFishJadeSecond", () => WorldCondition.FishJadeBuyCount < 2);
    }

    public class DownedSkeletron : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
        public bool CanShowItemDropInUI() => false;
        public string GetConditionDescription() => null;
    }

    public class DownedEowOrBoc : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss2;
        public bool CanShowItemDropInUI() => false;
        public string GetConditionDescription() => null;
    }
}
