using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Content.Buffs.Potions;
using LaziestNPC.Common.Helpers;
using LaziestNPC.WeakReferences.CalamityMod;

namespace LaziestNPC.Globals.GlobalMods.WeakReferences.CalamityMod
{
    public class WRCalamityPlayer : ModPlayer
    { 
        //灾厄Mod 半影药水 增强盗贼职业属性
        public override void PostUpdateMiscEffects()
        {
            if (!Player.LaziestNPC().penumbra)
                return;

            if (!ModLoader.HasMod("CalamityMod"))
                return;

            WRCalamityHelper.ApplyCalamityPenumbraPotion(Player);
        }
    }

    //灾厄Mod 真近战的射弹检测
    public static class TrueMeleeHelper
    {
        //缓存伤害类型，避免每次调用都TryFind
        private static DamageClass trueMelee;
        private static DamageClass trueMeleeNoSpeed;
        private static bool initialized;

        //初始化
        private static void Initialize()
        {
            if (initialized)
                return;

            initialized = true;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                calamity.TryFind("TrueMeleeDamageClass", out trueMelee);
                calamity.TryFind("TrueMeleeNoSpeedDamageClass", out trueMeleeNoSpeed);
            }
        }

        public static bool IsTrueMelee(Projectile proj)
        {
            if (proj == null || !proj.active)
                return false;

            //就算没加载灾厄，也默认初始化一次
            Initialize();

            //如果灾厄未加载，两个trueMelee字段都是null，直接返回false
            if (trueMelee != null && proj.CountsAsClass(trueMelee))
                return true;

            if (trueMeleeNoSpeed != null && proj.CountsAsClass(trueMeleeNoSpeed))
                return true;

            return false;
        }
    }
}
