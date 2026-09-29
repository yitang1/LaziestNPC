using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Common.Rarities;

namespace LaziestNPC.Content.Items.Consumables
{
    public class SunlightNet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 57;
            Item.height = 80;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 1);
            Item.rare = ModContent.RarityType<Rainbow>();
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.SunlightNet.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.SunlightNet.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            //森林 ↓
            //itemLoot.Add(ItemDropRule.Common(ItemID.LadyBug)); //瓢虫
            itemLoot.Add(ItemDropRule.Common(ItemID.Bird)); //鸟
            itemLoot.Add(ItemDropRule.Common(ItemID.BlueJay)); //冠蓝鸦
            itemLoot.Add(ItemDropRule.Common(ItemID.Cardinal)); //红雀
            itemLoot.Add(ItemDropRule.Common(ItemID.Bunny)); //兔兔
            itemLoot.Add(ItemDropRule.Common(ItemID.Squirrel)); //松鼠
            itemLoot.Add(ItemDropRule.Common(ItemID.SquirrelRed)); //红松鼠
            itemLoot.Add(ItemDropRule.Common(ItemID.Grasshopper)); //蚱蜢
            itemLoot.Add(ItemDropRule.Common(ItemID.Goldfish)); //金鱼
            itemLoot.Add(ItemDropRule.Common(ItemID.Turtle)); //龟
            itemLoot.Add(ItemDropRule.Common(ItemID.Duck)); //鸭
            itemLoot.Add(ItemDropRule.Common(ItemID.MallardDuck)); //野鸭
            itemLoot.Add(ItemDropRule.Common(ItemID.WaterStrider)); //水黾
            itemLoot.Add(ItemDropRule.Common(ItemID.Stinkbug)); //臭虫
            //森林夜晚 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Firefly)); //萤火虫
            itemLoot.Add(ItemDropRule.Common(ItemID.Owl)); //猫头鹰
            itemLoot.Add(ItemDropRule.Common(ItemID.FairyCritterBlue)); //蓝仙灵
            itemLoot.Add(ItemDropRule.Common(ItemID.FairyCritterGreen)); //绿仙灵
            itemLoot.Add(ItemDropRule.Common(ItemID.FairyCritterPink)); //粉仙灵
            //丛林 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Grubby)); //蛆虫
            itemLoot.Add(ItemDropRule.Common(ItemID.Sluggy)); //鼻涕虫
            itemLoot.Add(ItemDropRule.Common(ItemID.Buggy)); //蚜虫
            //itemLoot.Add(ItemDropRule.Common(ItemID.Frog)); //青蛙
            itemLoot.Add(ItemDropRule.Common(ItemID.TurtleJungle)); //丛林龟
            itemLoot.Add(ItemDropRule.Common(ItemID.YellowCockatiel)); //黄玄凤鹦鹉
            itemLoot.Add(ItemDropRule.Common(ItemID.GrayCockatiel)); //灰玄凤鹦鹉
            itemLoot.Add(ItemDropRule.Common(ItemID.ScarletMacaw)); //绯红金刚鹦鹉
            itemLoot.Add(ItemDropRule.Common(ItemID.BlueMacaw)); //蓝金刚鹦鹉
            itemLoot.Add(ItemDropRule.Common(ItemID.Toucan)); //巨嘴鸟
            //雪原 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Penguin)); //企鹅
            //沙漠 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Scorpion)); //蝎子
            itemLoot.Add(ItemDropRule.Common(ItemID.BlackScorpion)); //黑蝎子
            itemLoot.Add(ItemDropRule.Common(ItemID.Grebe)); //䴙䴘
            itemLoot.Add(ItemDropRule.Common(ItemID.Pupfish)); //鳉鱼
            //地下和洞穴 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Snail)); //蜗牛
            itemLoot.Add(ItemDropRule.Common(ItemID.Mouse)); //老鼠
            //发光蘑菇 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.GlowingSnail)); //发光蜗牛
            //itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.TruffleWorm)); //松露虫
            //海洋 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Seagull)); //海鸥
            itemLoot.Add(ItemDropRule.Common(ItemID.Seahorse)); //海马
            //itemLoot.Add(ItemDropRule.Common(ItemID.Pufferfish)); //河豚
            //地狱 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Lavafly)); //熔岩萤火虫
            itemLoot.Add(ItemDropRule.Common(ItemID.MagmaSnail)); //岩浆蜗牛
            //墓地 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Rat)); //大鼠
            itemLoot.Add(ItemDropRule.Common(ItemID.Maggot)); //蝇蛆
            //微光 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.Shimmerfly)); //飞灵
            //神圣之地 ↓
            itemLoot.Add(ItemDropRule.ByCondition(new Conditions.IsHardmode(), ItemID.LightningBug)); //荧光虫
            //蝴蝶 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.MonarchButterfly)); //帝王蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.SulphurButterfly)); //黄粉蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.JuliaButterfly)); //珠袖蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.UlyssesButterfly)); //翠凤蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.ZebraSwallowtailButterfly)); //带凤蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.PurpleEmperorButterfly)); //紫蛱蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.RedAdmiralButterfly)); //红蛱蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.TreeNymphButterfly)); //帛斑蝶
            itemLoot.Add(ItemDropRule.Common(ItemID.HellButterfly)); //地狱蝴蝶
            //itemLoot.Add(ItemDropRule.ByCondition(new Conditions.DownedPlantera(), ItemID.EmpressButterfly)); //七彩草蛉
            //蜻蜓 ↓
            itemLoot.Add(ItemDropRule.Common(ItemID.RedDragonfly)); //红蜻蜓
            itemLoot.Add(ItemDropRule.Common(ItemID.BlueDragonfly)); //蓝蜻蜓
            itemLoot.Add(ItemDropRule.Common(ItemID.GreenDragonfly)); //绿蜻蜓
            itemLoot.Add(ItemDropRule.Common(ItemID.YellowDragonfly)); //黄蜻蜓
            itemLoot.Add(ItemDropRule.Common(ItemID.BlackDragonfly)); //黑蜻蜓
            itemLoot.Add(ItemDropRule.Common(ItemID.OrangeDragonfly)); //橙蜻蜓
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.6f, new Vector2(0f, 0f));
            return false;
        }
    }
}
