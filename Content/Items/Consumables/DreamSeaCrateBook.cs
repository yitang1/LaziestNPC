using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using LaziestNPC.Common.Rarities;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Globals.GlobalItems;

namespace LaziestNPC.Content.Items.Consumables
{
    public class DreamSeaCrateBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 71;
            Item.height = 76;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.rare = ModContent.RarityType<Rainbow>();
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.DreamSeaCrateBook.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.DreamSeaCrateBook.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            //宝匣-肉前
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenCrate)); //木匣
            itemLoot.Add(ItemDropRule.Common(ItemID.IronCrate)); //铁匣
            itemLoot.Add(ItemDropRule.Common(ItemID.GoldenCrate)); //金匣
            itemLoot.Add(ItemDropRule.Common(ItemID.OasisCrate)); //绿洲匣
            itemLoot.Add(ItemDropRule.Common(ItemID.FrozenCrate)); //冰冻匣
            itemLoot.Add(ItemDropRule.Common(ItemID.JungleFishingCrate)); //丛林匣
            itemLoot.Add(ItemDropRule.Common(ItemID.OceanCrate)); //海洋匣
            itemLoot.Add(ItemDropRule.Common(ItemID.FloatingIslandFishingCrate)); //天空匣
            itemLoot.Add(ItemDropRule.Common(ItemID.CrimsonFishingCrate)); //猩红匣
            itemLoot.Add(ItemDropRule.Common(ItemID.CorruptFishingCrate)); //腐化匣
            itemLoot.Add(ItemDropRule.Common(ItemID.HallowedFishingCrate)); //神圣匣
            itemLoot.Add(ItemDropRule.Common(ItemID.LavaCrate)); //黑曜石匣
            itemLoot.Add(ItemDropRule.ByCondition(new DownedSkeletron(), ItemID.DungeonFishingCrate)); //地牢匣

            //宝匣-肉后
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.WoodenCrateHard)); //珍珠木匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.IronCrateHard)); //秘银匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.GoldenCrateHard)); //钛金匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.OasisCrateHard)); //幻象匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FrozenCrateHard)); //针叶木匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.JungleFishingCrateHard)); //荆棘匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.OceanCrateHard)); //海边匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.FloatingIslandFishingCrateHard)); //天蓝匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.CrimsonFishingCrateHard)); //血匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.CorruptFishingCrateHard)); //污损匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.HallowedFishingCrateHard)); //天赐匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.LavaCrateHard)); //狱石匣
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.DungeonFishingCrateHard)); //围栏匣
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.6f, new Vector2(0f, 0f));
            return false;
        }
    }
}
