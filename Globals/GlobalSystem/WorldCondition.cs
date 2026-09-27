using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using LaziestNPC.Common.Helpers;
using static LaziestNPC.Common.ModConditions.AllModBossConditions;

namespace LaziestNPC.Globals.GlobalSystem
{
    public class WorldCondition : ModSystem
    {
        public override void OnWorldLoad()
        {
            FishJadeBuyCount = 0;
            BloodMoonHappened = false;
            SnowMoonHappened = false;
            SnowMoonDOG = false;
            PumpkinMoonDOG = false;
            EclipseDOG = false;
        }
        public override void OnWorldUnload()
        {
            FishJadeBuyCount = 0;
            BloodMoonHappened = false;
            SnowMoonHappened = false;
            SnowMoonDOG = false;
            PumpkinMoonDOG = false;
            EclipseDOG = false;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            FishJadeBuyCount = tag.GetInt("FishJadeBuyCount");
            BloodMoonHappened = tag.GetBool("BloodMoonHappened");
            SnowMoonHappened = tag.GetBool("SnowMoonHappened");
            SnowMoonDOG = tag.GetBool("SnowMoonDOG");
            PumpkinMoonDOG = tag.GetBool("PumpkinMoonDOG");
            EclipseDOG = tag.GetBool("EclipseDOG");
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["FishJadeBuyCount"] = FishJadeBuyCount;
            tag["BloodMoonHappened"] = BloodMoonHappened;
            tag["SnowMoonHappened"] = SnowMoonHappened;
            tag["SnowMoonDOG"] = SnowMoonDOG;
            tag["PumpkinMoonDOG"] = PumpkinMoonDOG;
            tag["EclipseDOG"] = EclipseDOG;
        }

        public override void PostUpdateWorld()
        {
            if (Main.bloodMoon)
                BloodMoonHappened = true;
            if (Main.snowMoon)
                SnowMoonHappened = true;
            if (Main.snowMoon && DownedDOG.IsMet())
                SnowMoonDOG = true;
            if (Main.pumpkinMoon && DownedDOG.IsMet())
                PumpkinMoonDOG = true;
            if (Main.eclipse && DownedDOG.IsMet())
                EclipseDOG = true;
        }

        public static int FishJadeBuyCount = 0;
        public static bool BloodMoonHappened = false;
        public static bool SnowMoonHappened = false;
        public static bool SnowMoonDOG = false;
        public static bool PumpkinMoonDOG = false;
        public static bool EclipseDOG = false;
    }
}
