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

            #region 【机动性】
            //雏翼
            Recipe.Create(ItemID.CreativeWings)
                .AddIngredient(ItemID.Feather, 10)
                .AddIngredient(ItemID.Cloud, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //赫尔墨斯靴
            Recipe.Create(ItemID.HermesBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Loom)
                .Register();
            //疾风雪靴
            Recipe.Create(ItemID.FlurryBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Shiverthorn, 5)
                .AddIngredient(ItemID.SnowBlock, 10)
                .AddTile(TileID.Loom)
                .Register();
            //脚蹼
            Recipe.Create(ItemID.Flipper)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.FlipperPotion)
                .AddIngredient(ItemID.Gel, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //潜水头盔
            Recipe.Create(ItemID.DivingHelmet)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.GillsPotion)
                .AddIngredient(ItemID.SharkFin)
                .AddTile(TileID.Anvils)
                .Register();
            //闪亮红气球
            Recipe.Create(ItemID.ShinyRedBalloon)
                .AddIngredient(ItemID.WhiteString)
                .AddIngredient(ItemID.Cloud, 10)
                .AddIngredient(ItemID.RedHusk)
                .AddTile(TileID.WorkBenches)
                .Register();
            //沙丘行者靴
            Recipe.Create(ItemID.SandBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Waterleaf, 5)
                .AddIngredient(ItemID.SandBlock, 10)
                .AddTile(TileID.Loom)
                .Register();
            //航鱼靴(旗鱼靴)
            Recipe.Create(ItemID.SailfishBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Seashell, 5)
                .AddIngredient(ItemID.CoralstoneBlock, 10)
                .AddTile(TileID.Loom)
                .Register();
            //水上漂靴
            Recipe.Create(ItemID.WaterWalkingBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.WaterWalkingPotion)
                .AddIngredient(ItemID.Coral, 10)
                .AddTile(TileID.Loom)
                .Register();
            /*//水上漂靴
            Recipe.Create(ItemID.WaterWalkingBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.WaterWalkingPotion)
                .AddIngredient(ItemID.WaterBlock, 10)
                .AddTile(TileID.Loom)
                .Register();*/
            //鞋带束头(金属带扣)
            Recipe.Create(ItemID.Aglet)
                .AddRecipeGroup("CopperBarGroups", 2)
                .AddTile(TileID.Anvils)
                .Register();
            //幸运马掌
            Recipe.Create(ItemID.LuckyHorseshoe)
                .AddRecipeGroup("PlatinumBarGroups", 4)
                .AddIngredient(ItemID.Cloud, 2)
                .AddTile(TileID.Anvils)
                .Register();
            //蛙腿
            Recipe.Create(ItemID.FrogLeg)
                .AddIngredient(ItemID.Frog, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //疾风脚镯
            Recipe.Create(ItemID.AnkletoftheWind)
                .AddIngredient(ItemID.JungleSpores, 10)
                .AddIngredient(ItemID.Vine, 5)
                .AddIngredient(ItemID.PinkGel)
                .AddTile(TileID.Anvils)
                .Register();
            //溜冰鞋
            Recipe.Create(ItemID.IceSkates)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Shiverthorn, 5)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddTile(TileID.Loom)
                .Register();
            //云朵瓶
            Recipe.Create(ItemID.CloudinaBottle)
                .AddIngredient(ItemID.Bottle)
                .AddIngredient(ItemID.Cloud, 10)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //暴雪瓶
            Recipe.Create(ItemID.BlizzardinaBottle)
                .AddIngredient(ItemID.CloudinaBottle)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //沙暴瓶
            Recipe.Create(ItemID.SandstorminaBottle)
                .AddIngredient(ItemID.CloudinaBottle)
                .AddIngredient(ItemID.SandBlock, 10)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //海啸瓶
            Recipe.Create(ItemID.TsunamiInABottle)
                .AddIngredient(ItemID.CloudinaBottle)
                .AddIngredient(ItemID.CoralstoneBlock, 10)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //攀爬爪
            Recipe.Create(ItemID.ClimbingClaws)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.Gel, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //鞋钉
            Recipe.Create(ItemID.ShoeSpikes)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.Gel, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //飞毯
            Recipe.Create(ItemID.FlyingCarpet)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.Amber)
                .AddTile(TileID.Loom)
                .Register();
            //浮游圈
            Recipe.Create(ItemID.FloatingTube)
                .AddIngredient(ItemID.Leather, 5)
                .AddIngredient(ItemID.Cloud, 5)
                .AddIngredient(ItemID.WaterWalkingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            //梯凳
            Recipe.Create(ItemID.PortableStool)
                .AddRecipeGroup("Wood", 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //分趾厚底袜
            Recipe.Create(ItemID.Tabi)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.BlackInk)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Loom)
                .Register();
            //黑腰带
            Recipe.Create(ItemID.BlackBelt)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.BlackInk)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.Loom)
                .Register();
            //蛾怪之翼
            Recipe.Create(ItemID.MothronWings)
                .AddIngredient(ItemID.SoulofFlight, 20)
                .AddIngredient(ItemID.LunarTabletFragment, 5)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();
            //喜庆之翼
            Recipe.Create(ItemID.FestiveWings)
                .AddIngredient(ItemID.SoulofFlight, 20)
                .AddIngredient(ItemID.PineTreeBlock, 15)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            /*//气球河豚鱼
            Recipe.Create(ItemID.BalloonPufferfish)
                .AddIngredient(ItemID.ShinyRedBalloon)
                .AddIngredient(ItemID.Pufferfish)
                .AddTile(TileID.WorkBenches)
                .Register();
            //牧羊符文
            Recipe.Create(ItemID.DeadCellsRamRune)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.BoulderBlock, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();*/

            #endregion

            #region 【信息类】
            //雷达
            Recipe.Create(ItemID.Radar)
                .AddRecipeGroup("SilverBarGroups", 2)
                .AddIngredient(ItemID.Glass)
                .AddIngredient(ItemID.Emerald)
                .AddTile(TileID.Anvils)
                .Register();
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
            //生命体分析机
            Recipe.Create(ItemID.LifeformAnalyzer)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Diamond, 5)
                .AddIngredient(ItemID.Ruby)
                .AddTile(TileID.Anvils)
                .Register();
            //金属探测器
            Recipe.Create(ItemID.MetalDetector)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.Diamond)
                .AddIngredient(ItemID.Emerald)
                .AddIngredient(ItemID.Ruby)
                .AddTile(TileID.Anvils)
                .Register();
            //秒表
            Recipe.Create(ItemID.Stopwatch)
                .AddRecipeGroup("SilverWatchGroups", 5)
                .AddRecipeGroup("IronBarGroups", 2)
                .AddIngredient(ItemID.Ruby)
                .AddTile(TileID.Anvils)
                .Register();
            //每秒伤害计数器
            Recipe.Create(ItemID.DPSMeter)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddRecipeGroup("SilverBarGroups", 2)
                .AddIngredient(ItemID.Ruby, 5)
                .AddTile(TileID.Anvils)
                .Register();

            #endregion

            #region 【战斗类】
            //大自然的恩赐
            Recipe.Create(ItemID.NaturesGift)
                .AddIngredient(ItemID.JungleRose)
                .AddIngredient(ItemID.SkyBlueFlower)
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //月亮石
            Recipe.Create(ItemID.MoonStone)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.Moonglow)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //月光护身符
            Recipe.Create(ItemID.MoonCharm)
                .AddRecipeGroup("PerEvilMaterial", 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.Moonglow)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //再生手环
            Recipe.Create(ItemID.BandofRegeneration)
                .AddIngredient(ItemID.LifeCrystal)
                .AddIngredient(ItemID.Chain, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //星力手环
            Recipe.Create(ItemID.BandofStarpower)
                .AddIngredient(ItemID.ManaCrystal)
                .AddIngredient(ItemID.Chain, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //恐慌项链
            Recipe.Create(ItemID.PanicNecklace)
                .AddIngredient(ItemID.LifeCrystal)
                .AddIngredient(ItemID.SwiftnessPotion)
                .AddIngredient(ItemID.Chain, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //十字项链
            Recipe.Create(ItemID.CrossNecklace)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.Chain, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //腐香囊
            Recipe.Create(ItemID.PutridScent)
                .AddIngredient(ItemID.RottenChunk, 10)
                .AddIngredient(ItemID.Leather, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //血肉指虎
            Recipe.Create(ItemID.FleshKnuckles)
                .AddIngredient(ItemID.Vertebrae, 10)
                .AddIngredient(ItemID.CrimtaneBar, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //天界磁石
            Recipe.Create(ItemID.CelestialMagnet)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddIngredient(ItemID.Cloud, 10)
                .AddIngredient(ItemID.ManaCrystal)
                .AddTile(TileID.Anvils)
                .Register();
            //镣铐
            Recipe.Create(ItemID.Shackle)
                .AddRecipeGroup("IronBarGroups")
                .AddTile(TileID.Anvils)
                .Register();
            //点金石
            Recipe.Create(ItemID.PhilosophersStone)
                .AddIngredient(ItemID.PalladiumBar, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.GreaterHealingPotion)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //星星斗篷
            Recipe.Create(ItemID.StarCloak)
                .AddIngredient(ItemID.Silk, 15)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.ManaCrystal)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //钴护盾
            Recipe.Create(ItemID.CobaltShield)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.TitanPotion)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //猛爪手套
            Recipe.Create(ItemID.FeralClaws)
                .AddIngredient(ItemID.Leather, 5)
                .AddIngredient(ItemID.Vine, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //泰坦手套
            Recipe.Create(ItemID.TitanGlove)
                .AddIngredient(ItemID.CobaltBar, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.TitanPotion)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //冰冻海龟壳
            Recipe.Create(ItemID.FrozenTurtleShell)
                .AddRecipeGroup("TitaniumBarGroups", 5)
                .AddIngredient(ItemID.IceBlock, 15)
                .AddIngredient(ItemID.IronskinPotion)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //圣骑士护盾
            Recipe.Create(ItemID.PaladinsShield)
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddRecipeGroup("TitaniumBarGroups", 10)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //魔法箭袋
            Recipe.Create(ItemID.MagicQuiver)
                .AddIngredient(ItemID.WoodenArrow, 20)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //岩浆石
            Recipe.Create(ItemID.MagmaStone)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.InfernoPotion)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //鲨牙项链
            Recipe.Create(ItemID.SharkToothNecklace)
                .AddIngredient(ItemID.Chain, 5)
                .AddIngredient(ItemID.SharkFin, 2)
                .AddTile(TileID.Anvils)
                .Register();
            //粘性绷带
            Recipe.Create(ItemID.AdhesiveBandage)
                .AddIngredient(ItemID.Cobweb, 5)
                .AddIngredient(ItemID.Gel, 5)
                .AddIngredient(ItemID.LesserHealingPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            //牛黄
            Recipe.Create(ItemID.Bezoar)
                .AddIngredient(ItemID.Stinger, 5)
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddIngredient(ItemID.MudBlock, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
            //盔甲抛光剂
            Recipe.Create(ItemID.ArmorPolish)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //维生素
            Recipe.Create(ItemID.Vitamins)
                .AddIngredient(ItemID.Ichor, 5)
                .AddIngredient(ItemID.CursedFlame, 5)
                .AddIngredient(ItemID.GreaterHealingPotion)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //快走时钟
            Recipe.Create(ItemID.FastClock)
                .AddRecipeGroup("PlatinumWatchGroups")
                .AddIngredient(ItemID.SoulofLight)
                .AddIngredient(ItemID.SoulofNight)
                .AddIngredient(ItemID.SwiftnessPotion)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //三折地图
            Recipe.Create(ItemID.TrifoldMap)
                .AddIngredient(ItemID.Compass)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //扩音器
            Recipe.Create(ItemID.Megaphone)
                .AddIngredient(ItemID.IronBar, 5)
                .AddIngredient(ItemID.Wire, 5)
                .AddIngredient(ItemID.Glass, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //邪眼
            Recipe.Create(ItemID.Nazar)
                .AddRecipeGroup("CrimtaneBarGroups", 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.BlackLens)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //蒙眼布
            Recipe.Create(ItemID.Blindfold)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.HardenedSand, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //袖珍镜
            Recipe.Create(ItemID.PocketMirror)
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ItemID.Marble, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //暖手宝
            Recipe.Create(ItemID.HandWarmer)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.WarmthPotion)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddTile(TileID.Loom)
                .Register();
            //黑平衡锤
            Recipe.Create(ItemID.BlackCounterweight)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddRecipeGroup("GemGroups", 5)
                .AddTile(TileID.Anvils)
                .Register();
            //黄平衡锤
            Recipe.Create(ItemID.YellowCounterweight)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddRecipeGroup("GemGroups", 5)
                .AddTile(TileID.Anvils)
                .Register();
            //蓝平衡锤
            Recipe.Create(ItemID.BlueCounterweight)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddRecipeGroup("GemGroups", 5)
                .AddTile(TileID.Anvils)
                .Register();
            //红平衡锤
            Recipe.Create(ItemID.RedCounterweight)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddRecipeGroup("GemGroups", 5)
                .AddTile(TileID.Anvils)
                .Register();
            //紫平衡锤
            Recipe.Create(ItemID.PurpleCounterweight)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddRecipeGroup("GemGroups", 5)
                .AddTile(TileID.Anvils)
                .Register();
            //绿平衡锤
            Recipe.Create(ItemID.GreenCounterweight)
                .AddRecipeGroup("IronBarGroups", 5)
                .AddRecipeGroup("GemGroups", 5)
                .AddTile(TileID.Anvils)
                .Register();
            //悠悠球手套
            Recipe.Create(ItemID.YoYoGlove)
                .AddIngredient(ItemID.TitanGlove)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.BlackInk)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            /*//淬毒倒刺
            Recipe.Create(ItemID.PoisonBarb)
                .AddIngredient(ItemID.Stinger, 3)
                .AddIngredient(ItemID.Rope, 2)
                .AddTile(TileID.Anvils)
                .Register();
            //邪恶臂环
            Recipe.Create(ItemID.WickedArmlet)
                .AddRecipeGroup("CrimtaneBarGroups", 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //碎裂之石
            Recipe.Create(ItemID.SnappingStone)
                .AddIngredient(ItemID.Granite, 10)
                .AddIngredient(ItemID.Sapphire, 5)
                .AddIngredient(ItemID.Diamond)
                .AddTile(TileID.Anvils)
                .Register();
            //混沌弹筒
            Recipe.Create(ItemID.AmmoCycler4)
                .AddRecipeGroup("TitaniumBarGroups", 10)
                .AddIngredient(ItemID.EmptyBullet, 20)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //蛇形手环
            Recipe.Create(ItemID.SnakeBand)
                .AddIngredient(ItemID.VineRope)
                .AddIngredient(ItemID.FossilOre, 10)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //重型投石索
            Recipe.Create(ItemID.HeavySling)
                .AddIngredient(ItemID.AcornSlingshot)
                .AddIngredient(ItemID.BoulderBlock, 5)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //银护腕
            Recipe.Create(ItemID.SilverBracer)
                .AddIngredient(ItemID.SilverBar, 10)
                .AddIngredient(ItemID.Bone, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //魔法绳
            Recipe.Create(ItemID.MagicString)
                .AddIngredient(ItemID.WhiteString)
                .AddIngredient(ItemID.HallowedBar)
                .AddIngredient(ItemID.FallenStar)
                .AddTile(TileID.CrystalBall)
                .Register();*/

            #endregion

            #region 【非战斗类(实用类)】
            //远古凿子
            Recipe.Create(ItemID.AncientChisel)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.MiningPotion, 5)
                .AddIngredient(ItemID.SandBlock, 10)
                .AddTile(TileID.Anvils)
                .Register();
            //水母项链
            Recipe.Create(ItemID.JellyfishNecklace)
                .AddRecipeGroup("PlatinumBarGroups", 2)
                .AddIngredient(ItemID.Glowstick, 5)
                .AddIngredient(ItemID.PinkPearl)
                .AddTile(TileID.Anvils)
                .Register();
            //海神贝壳
            Recipe.Create(ItemID.NeptunesShell)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.PinkPearl)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //熔岩护身符
            Recipe.Create(ItemID.LavaCharm)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.ObsidianSkinPotion)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //黑曜石玫瑰
            Recipe.Create(ItemID.ObsidianRose)
                .AddIngredient(ItemID.JungleRose)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddTile(TileID.Anvils)
                .Register();
            //步枪瞄准镜
            Recipe.Create(ItemID.RifleScope)
                .AddRecipeGroup("TitaniumBarGroups", 5)
                .AddIngredient(ItemID.Sapphire)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            //防熔岩钓钩
            Recipe.Create(ItemID.LavaFishingHook)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Obsidian, 5)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddTile(TileID.Anvils)
                .Register();

            #endregion

            #region 【其他类】
            //整蛊坐垫
            Recipe.Create(ItemID.WhoopieCushion)
                .AddIngredient(ItemID.Leather, 5)
                .AddIngredient(ItemID.Cloud, 5)
                .AddIngredient(ItemID.StinkPotion)
                .AddTile(TileID.WorkBenches)
                .Register();
            //花靴
            Recipe.Create(ItemID.FlowerBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.NaturesGift)
                .AddIngredient(ItemID.JungleGrassSeeds, 5)
                .AddTile(TileID.Loom)
                .Register();
            //烈焰靴
            Recipe.Create(ItemID.FlameWakerBoots)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Fireblossom, 5)
                .AddIngredient(ItemID.Obsidian, 10)
                .AddTile(TileID.Hellforge)
                .Register();
            //植物纤维绳索宝典
            Recipe.Create(ItemID.CordageGuide)
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.Daybloom, 5)
                .AddIngredient(ItemID.Vine, 5)
                .AddTile(TileID.WorkBenches)
                .Register();

            #endregion

            #endregion

            #region 【武器】

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
                .AddRecipeGroup("SilverBarGroups", 5)
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
                .AddIngredient(ItemID.IceBlock, 15)
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

            #region 【其他武器】
            /*//橡实弹弓
            Recipe.Create(ItemID.AcornSlingshot)
                .AddRecipeGroup("Wood", 5)
                .AddIngredient(ItemID.Acorn)
                .AddTile(TileID.WorkBenches)
                .Register();*/

            #endregion

            #endregion

            #region 【工具】
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
                .AddRecipeGroup("PlatinumBarGroups", 5)
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
            //白珍珠
            Recipe.Create(ItemID.WhitePearl)
                .AddIngredient(ItemID.Oyster, 5)
                .Register();
            //黑珍珠
            Recipe.Create(ItemID.BlackPearl)
                .AddIngredient(ItemID.Oyster, 10)
                .Register();
            //粉珍珠
            Recipe.Create(ItemID.PinkPearl)
                .AddIngredient(ItemID.Oyster, 20)
                .Register();
            //皮革
            Recipe.Create(ItemID.Leather)
                .AddRecipeGroup("PerEvilMaterial", 5)
                .AddTile(TileID.WorkBenches)
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
                .AddIngredient(ItemID.SunplateBlock, 10)
                .AddTile(TileID.SkyMill)
                .Register();
            //藤蔓绳
            Recipe.Create(ItemID.VineRope, 3)
                .AddIngredient(ItemID.Vine)
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
