using LaziestNPC.Common.Helpers;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LaziestNPC.Globals.GlobalSystem
{
    public class LNPCSystem : ModSystem
    {
        public static int fishJadeBuyCount = 0;

        public override void OnWorldLoad() => fishJadeBuyCount = 0;
        public override void OnWorldUnload() => fishJadeBuyCount = 0;

        public override void SaveWorldData(TagCompound tag)
        {
            tag["fishJadeBuyCount"] = fishJadeBuyCount;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            fishJadeBuyCount = tag.GetInt("fishJadeBuyCount");
        }
    }
}
