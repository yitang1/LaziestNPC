using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.ModBossess;
using LaziestNPC.Globals.GlobalItems;
using Terraria.GameContent.ItemDropRules;

namespace LaziestNPC.Content.Items.Consumables
{
    public class HJTanks : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 40;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.rare = ItemRarityID.Red;
            Item.consumable = true;
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            //Mod：Homeward Journey (旅人归途)
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod HomewardJourney))
                return;

            string[] TankItems =
            {
                "TankOfThePastForest", //古老森林之罐
                "TankOfThePastCave", //古老地底之罐
                "TankOfThePastCrimson", //古老血地之罐
                "TankOfThePastCorruption", //古老腐地之罐
                "TankOfThePastHallow", //古老圣地之罐
                "TankOfThePastDesert", //古老沙漠之罐
                "TankOfThePastSnowland", //古老雪地之罐
                "TankOfThePastJungle", //古老丛林之罐
                "TankOfThePastSky", //古老天空之罐
                "TankOfThePastUnderworld" //古老下界之罐
            };

            List<int> AllItems = new List<int>();
            foreach (string name in TankItems)
            {
                if (HomewardJourney.TryFind(name, out ModItem item))
                    AllItems.Add(item.Type);
            }

            if (AllItems.Count > 0)
                itemLoot.Add(ItemDropRule.OneFromOptions(1, AllItems.ToArray()));
        }
    }
}
