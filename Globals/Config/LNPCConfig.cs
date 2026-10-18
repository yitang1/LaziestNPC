using System;
using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace LaziestNPC.Globals.Config
{
    public class LNPCConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static LNPCConfig Instance;

        [Header("LNPCs")]

        [DefaultValue(true)]
        public bool LNPCBossNPC;

        [DefaultValue(true)]
        public bool LNPCPotionNPC;

        [DefaultValue(true)]
        public bool LNPCMaterialNPC;

        [DefaultValue(true)]
        public bool LNPCBuildNPC;

        [DefaultValue(true)]
        public bool LNPCNatureNPC;

        [DefaultValue(true)]
        public bool LNPCOreNPC;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ShowPortrait;

        [Header("OtherNPCs")]

        [DefaultValue(true)]
        [ReloadRequired]
        public bool LNPCInvincible;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool LNPCCritterInvincible;

    }
}
