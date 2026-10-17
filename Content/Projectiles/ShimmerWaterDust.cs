using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Content.Projectiles
{
    public class ShimmerWaterDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.alpha = 170;
            dust.velocity *= 0.5f;
            dust.velocity.Y = dust.velocity.Y + 1f;
        }
    }
}
