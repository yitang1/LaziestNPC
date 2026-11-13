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
            GoblinArmyHappened = false;
            EclipseHappened = false;
            SnowMoonHappened = false;
            PumpkinMoonHappened = false;

        }
        public override void OnWorldUnload()
        {
            FishJadeBuyCount = 0;
            BloodMoonHappened = false;
            GoblinArmyHappened = false;
            EclipseHappened = false;
            SnowMoonHappened = false;
            PumpkinMoonHappened = false;

        }

        public override void LoadWorldData(TagCompound tag)
        {
            FishJadeBuyCount = tag.GetInt("FishJadeBuyCount");
            BloodMoonHappened = tag.GetBool("BloodMoonHappened");
            GoblinArmyHappened = tag.GetBool("GoblinArmyHappened");
            EclipseHappened = tag.GetBool("EclipseHappened");
            SnowMoonHappened = tag.GetBool("SnowMoonHappened");
            PumpkinMoonHappened = tag.GetBool("PumpkinMoonHappened");

        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["FishJadeBuyCount"] = FishJadeBuyCount;
            tag["BloodMoonHappened"] = BloodMoonHappened;
            tag["GoblinArmyHappened"] = GoblinArmyHappened;
            tag["EclipseHappened"] = EclipseHappened;
            tag["SnowMoonHappened"] = SnowMoonHappened;
            tag["PumpkinMoonHappened"] = PumpkinMoonHappened;

        }

        public override void PostUpdateWorld()
        {
            //事件发生过一次，对应字段就会永久变为true
            if (Main.bloodMoon) BloodMoonHappened = true;
            if (Main.invasionType == InvasionID.GoblinArmy) GoblinArmyHappened = true;
            if (Main.eclipse) EclipseHappened = true;
            if (Main.snowMoon) SnowMoonHappened = true;
            if (Main.pumpkinMoon) PumpkinMoonHappened = true;

        }

        public static int FishJadeBuyCount = 0;
        public static bool BloodMoonHappened = false;
        public static bool GoblinArmyHappened = false;
        public static bool EclipseHappened = false;
        public static bool SnowMoonHappened = false;
        public static bool PumpkinMoonHappened = false;

    }
}
