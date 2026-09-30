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
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static LNPCConfig Instance;

        [Header("LNPCs")]

        [DefaultValue(true)]
        [ReloadRequired]
        public bool LNPCInvincible;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool LNPCCritterInvincible;
    }
}
