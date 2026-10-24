using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalRecipes
{
    public class AddVanillaRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            #region 【配饰】
            //深度计
            Recipe.Create(ItemID.DepthMeter)
                .AddRecipeGroup("SilverBarGroups", 5)
                .AddRecipeGroup("PlatinumBarGroups", 2)
                .AddIngredient(ItemID.Ruby)
                .AddTile(TileID.Anvils)
                .Register();
            //罗盘
            Recipe.Create(ItemID.Compass)
                .AddRecipeGroup("SilverBarGroups", 5)
                .AddIngredient(ItemID.Ruby)
                .AddIngredient(ItemID.Diamond)
                .AddTile(TileID.Anvils)
                .Register();
            //杀怪计数器
            Recipe.Create(ItemID.TallyCounter)
                .AddRecipeGroup("SilverBarGroups", 5)
                .AddIngredient(ItemID.Bone, 10)
                .AddIngredient(ItemID.Diamond, 2)
                .AddTile(TileID.Anvils)
                .Register();
            //雏翼
            Recipe.Create(ItemID.CreativeWings)
                .AddIngredient(ItemID.Cloud, 5)
                .AddIngredient(ItemID.Feather, 10)
                .AddTile(TileID.Anvils)
                .Register();
            //疾风雪靴
            Recipe.Create(ItemID.FlurryBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Shiverthorn, 5)
                .AddIngredient(ItemID.IceBlock, 5)
                .AddTile(TileID.Loom)
                .Register();
            //远古凿子
            Recipe.Create(ItemID.AncientChisel)
                .AddIngredient(ItemID.MiningPotion, 5)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.SandBlock, 20)
                .AddTile(TileID.Anvils)
                .Register();
            //脚蹼
            Recipe.Create(ItemID.Flipper)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.FlipperPotion, 5)
                .AddIngredient(ItemID.Gel, 2)
                .AddTile(TileID.Anvils)
                .Register();
            //潜水头盔
            Recipe.Create(ItemID.DivingHelmet)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.GillsPotion, 5)
                .AddIngredient(ItemID.SharkFin, 1)
                .AddTile(TileID.Anvils)
                .Register();
            //闪亮红气球
            Recipe.Create(ItemID.ShinyRedBalloon)
                .AddIngredient(ItemID.WhiteString)
                .AddIngredient(ItemID.Cloud, 10)
                .AddIngredient(ItemID.Gel, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //沙丘行者靴
            Recipe.Create(ItemID.SandBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Waterleaf, 5)
                .AddIngredient(ItemID.SandBlock, 5)
                .AddTile(TileID.Loom)
                .Register();
            //航鱼靴
            Recipe.Create(ItemID.SailfishBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Seashell, 5)
                .AddIngredient(ItemID.Coral, 5)
                .AddTile(TileID.Loom)
                .Register();
            //整蛊坐垫
            Recipe.Create(ItemID.WhoopieCushion)
                .AddIngredient(ItemID.Leather, 5)
                .AddIngredient(ItemID.Cloud, 5)
                .AddIngredient(ItemID.StinkPotion)
                .AddTile(TileID.WorkBenches)
                .Register();

            #endregion

            #region 【近战武器】
            //拍拍手
            Recipe.Create(ItemID.SlapHand)
                .AddIngredient(ItemID.ZombieArm)
                .AddIngredient(ItemID.Bone, 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            //骨剑
            Recipe.Create(ItemID.BoneSword)
                .AddRecipeGroup("SilverBarGroups", 8)
                .AddRecipeGroup("PerEvilMaterial", 3)
                .AddTile(TileID.Anvils)
                .Register();
            //叶列茨球
            Recipe.Create(ItemID.Yelets)
                .AddIngredient(ItemID.JungleYoyo)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            //链刀
            Recipe.Create(ItemID.ChainKnife)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.Chain, 5)
                .AddIngredient(ItemID.Sapphire)
                .AddTile(TileID.Anvils)
                .Register();
            //蘑菇回旋镖
            Recipe.Create(ItemID.Shroomerang)
                .AddIngredient(ItemID.WoodenBoomerang)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.GlowingMushroom, 10)
                .AddTile(TileID.Anvils)
                .Register();
            //致胜炮
            Recipe.Create(ItemID.KOCannon)
                .AddIngredient(ItemID.TitanGlove)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.ChumBucket, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            //冰雪镰刀
            Recipe.Create(ItemID.IceSickle)
                .AddIngredient(ItemID.Sickle)
                .AddIngredient(ItemID.FrostCore)
                .AddIngredient(ItemID.IceBlock, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            /*//天顶剑
            Recipe.Create(ItemID.Zenith)
                .AddIngredient(ItemID.TerraBlade)
                .AddIngredient(ItemID.Meowmere)
                .AddIngredient(ItemID.StarWrath)
                .AddIngredient(ItemID.InfluxWaver)
                .AddIngredient(ItemID.TheHorsemansBlade)
                .AddIngredient(ItemID.Seedler)
                .AddIngredient(ItemID.Starfury)
                .AddIngredient(ItemID.BeeKeeper)
                .AddIngredient(ItemID.EnchantedSword)
                .AddIngredient(ItemID.CopperShortsword)
                .AddTile(TileID.MythrilAnvil)
                .Register(); */
            #endregion

            #region 【远程武器】
            //鱼叉枪
            Recipe.Create(ItemID.Harpoon)
                .AddIngredient(ItemID.ChainKnife)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.SpikyBall, 5)
                .AddTile(TileID.Anvils)
                .Register();

            #endregion

            #region 【魔法武器】
            //恶魔锄刀(恶魔之镰)
            Recipe.Create(ItemID.DemonScythe)
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.DemonTorch, 10)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddTile(TileID.Bookcases)
                .Register();

            #endregion

            #region 【召唤武器】
            //吸血鬼青蛙法杖
            Recipe.Create(ItemID.VampireFrogStaff)
                .AddIngredient(ItemID.Frog)
                .AddIngredient(ItemID.CrimtaneBar, 5)
                .AddIngredient(ItemID.ChumBucket, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //致命球法杖
            Recipe.Create(ItemID.DeadlySphereStaff)
                .AddIngredient(ItemID.SpikyBall, 20)
                .AddIngredient(ItemID.LunarTabletFragment, 10)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            #endregion

            #region 【工具类】
            //激光钻头
            Recipe.Create(ItemID.LaserDrill)
                .AddIngredient(ItemID.ChlorophyteDrill)
                .AddIngredient(ItemID.MartianConduitPlating, 20)
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            //掠夺鲨
            Recipe.Create(ItemID.ReaverShark)
                .AddRecipeGroup("PlatinumBarGroups", 10)
                .AddIngredient(ItemID.SharkFin, 5)
                .AddIngredient(ItemID.WhitePearl)
                .AddTile(TileID.Anvils)
                .Register();
            #endregion

            #region 【盔甲】
            //夜视头盔
            Recipe.Create(ItemID.NightVisionHelmet)
                .AddIngredient(ItemID.Granite, 10)
                .AddIngredient(ItemID.Glass, 5)
                .AddIngredient(ItemID.NightOwlPotion, 5)
                .AddTile(TileID.Anvils)
                .Register();

            #endregion

            #region 【时装】
            //星星发夹
            Recipe.Create(ItemID.StarHairpin)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.FallenStar)
                .AddTile(TileID.Loom)
                .Register();
            //猫咪衣
            Recipe.Create(ItemID.CatShirt)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.GoodieBag)
                .AddTile(TileID.Loom)
                .Register();
            //猫咪裤
            Recipe.Create(ItemID.CatPants)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.GoodieBag)
                .AddTile(TileID.Loom)
                .Register();
            #endregion

            #region 【消耗品】
            //生命水晶
            Recipe.Create(ItemID.LifeCrystal)
                .AddRecipeGroup("PlatinumBarGroups", 3)
                .AddIngredient(ItemID.LesserHealingPotion)
                .AddIngredient(ItemID.FallenStar)
                .AddTile(TileID.DemonAltar)
                .Register();
            //生命果
            Recipe.Create(ItemID.LifeFruit)
                .AddIngredient(ItemID.LifeCrystal)
                .AddIngredient(ItemID.HallowedBar)
                .AddIngredient(ItemID.Vine, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
            //Joja可乐
            Recipe.Create(ItemID.JojaCola)
                .AddIngredient(ItemID.AppleJuice)
                .AddIngredient(ItemID.MasterBait)
                .AddTile(TileID.Furnaces)
                .Register();

            #endregion

            #region 【召唤物品】
            //松露虫
            Recipe.Create(ItemID.TruffleWorm)
                .AddIngredient(ItemID.Worm)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.GlowingMushroom, 5)
                .AddTile(TileID.CrystalBall)
                .Register();
            //七彩草蛉
            Recipe.Create(ItemID.EmpressButterfly)
                .AddRecipeGroup("Butterflies")
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.CrystalBall)
                .Register();
            //丛林蜥蜴电池
            Recipe.Create(ItemID.LihzahrdPowerCell)
                .AddIngredient(ItemID.LunarTabletFragment, 5)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.CrystalBall)
                .Register();
            #endregion

            #region 【材料】
            //黑墨水
            Recipe.Create(ItemID.BlackInk)
                .AddIngredient(ItemID.DeathweedSeeds)
                .AddIngredient(ItemID.BlackLens)
                .AddTile(TileID.DyeVat)
                .Register();

            #endregion

            #region 【放置类物品】
            //地狱熔炉
            Recipe.Create(ItemID.Hellforge)
                .AddIngredient(ItemID.Furnace)
                .AddIngredient(ItemID.Hellstone, 20)
                .AddTile(TileID.Anvils)
                .Register();
            //活火块
            Recipe.Create(ItemID.LivingFireBlock, 10)
                .AddIngredient(ItemID.Hellstone)
                .AddIngredient(ItemID.Torch)
                .AddTile(TileID.AdamantiteForge)
                .Register();
            //附魔日晷
            Recipe.Create(ItemID.Sundial)
                .AddRecipeGroup("PlatinumWatchGroups")
                .AddIngredient(ItemID.SunplateBlock, 20)
                .AddTile(TileID.SkyMill)
                .Register();
            //哀木纪念章
            Recipe.Create(ItemID.MourningWoodTrophy)
                .AddIngredient(ItemID.SpookyWood, 50)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Solidifier)
                .Register();
            //南瓜王纪念章
            Recipe.Create(ItemID.PumpkingTrophy)
                .AddIngredient(ItemID.SpookyWood, 50)
                .AddIngredient(ItemID.Pumpkin, 5)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Solidifier)
                .Register();
            #endregion

            #region 【坐骑】
            #endregion

            #region 【杂项】
            //魔法海螺
            Recipe.Create(ItemID.MagicConch)
                .AddIngredient(ItemID.Seashell, 10)
                .AddIngredient(ItemID.Coral, 10)
                .AddIngredient(ItemID.SandBlock, 20)
                .AddTile(TileID.Anvils)
                .Register();
            //水枪
            Recipe.Create(ItemID.WaterGun)
                .AddRecipeGroup("Wood", 10)
                .AddIngredient(ItemID.Glass, 5)
                .AddIngredient(ItemID.Gel, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //史莱姆枪
            Recipe.Create(ItemID.SlimeGun)
                .AddIngredient(ItemID.WaterGun)
                .AddRecipeGroup("PlatinumBarGroups", 3)
                .AddIngredient(ItemID.Gel, 30)
                .AddTile(TileID.Solidifier)
                .Register();
            //胡萝卜
            Recipe.Create(ItemID.Carrot)
                .AddIngredient(ItemID.Daybloom)
                .AddIngredient(ItemID.Acorn)
                .AddIngredient(ItemID.Blinkroot)
                .Register();

            #endregion
        }
    }
}
