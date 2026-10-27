using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using LaziestNPC.Globals.GlobalSystem;

namespace LaziestNPC.Globals.GlobalItems
{
    public static class ItemCondition
    {
        public static Condition CanBuyFishJadeFirst = new Condition("CanBuyFishJadeFirst", () => WorldCondition.FishJadeBuyCount < 1);
        public static Condition CanBuyFishJadeSecond = new Condition("CanBuyFishJadeSecond", () => WorldCondition.FishJadeBuyCount < 2);
    }

    //通用摸彩袋类掉落条件
    public class DropCondition : IItemDropRuleCondition
    {
        private readonly Func<bool> check;

        public DropCondition(Func<bool> condition)
        {
            check = condition;
        }

        public bool CanDrop(DropAttemptInfo info) => check();
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }

}
