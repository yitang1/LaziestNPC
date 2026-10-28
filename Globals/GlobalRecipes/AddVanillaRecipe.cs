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
            /*//气球河豚鱼
            Recipe.Create(ItemID.BalloonPufferfish)
                .AddIngredient(ItemID.ShinyRedBalloon)
                .AddIngredient(ItemID.Pufferfish)
                .AddTile(TileID.WorkBenches)
                .Register();*/
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
            //鞋带束头
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
            /*//海啸瓶
            Recipe.Create(ItemID.TsunamiInABottle)
                .AddIngredient(ItemID.CloudinaBottle)
                .AddIngredient(ItemID.WaterBlock, 10)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.Anvils)
                .Register();*/
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
            /*//牧羊符文
            Recipe.Create(ItemID.DeadCellsRamRune)
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ItemID.BoulderBlock, 10)
                .AddTile(TileID.Anvils)
                .Register();*/
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
                .AddIngredient(ItemID.PineTreeBlock, 10)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            #endregion

            #region 【信息类】
            //雷达
            Recipe.Create(ItemID.Radar)
                .AddRecipeGroup("SilverBarGroups", 4)
                .AddIngredient(ItemID.Glass, 2)
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
                .AddTile(TileID.MythrilAnvil)
                .Register();
            //月光护身符
            Recipe.Create(ItemID.MoonCharm)
                .AddRecipeGroup("PerEvilMaterial", 10)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.Moonglow)
                .AddTile(TileID.MythrilAnvil)
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
                .AddRecipeGroup("PlatinumBarGroups", 5)
                .AddIngredient(ItemID.Chain, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

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
                .AddTile(TileID.MythrilAnvil)
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

            #endregion

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
                .AddIngredient(ItemID.IceBlock, 10)
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
            //哀木纪念章
            Recipe.Create(ItemID.MourningWoodTrophy)
                .AddIngredient(ItemID.SpookyWood, 50)
                .AddIngredient(ItemID.Ectoplasm)
                .AddTile(TileID.Solidifier)
                .Register();
            //南瓜王纪念章
            Recipe.Create(ItemID.PumpkingTrophy)
                .AddIngredient(ItemID.SpookyWood, 50)
                .AddIngredient(ItemID.Pumpkin, 5)
                .AddIngredient(ItemID.Ectoplasm)
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
