using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Content.Projectiles
{
    public class ShimmerWater : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return true;
        }

        public override void AI()
        {
            if (Main.windPhysics)
            {
                Projectile.velocity.X = Projectile.velocity.X + Main.windSpeedCurrent * Main.windPhysicsStrength;
            }
            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.04f * (float)Projectile.direction;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 10f)
            {
                Projectile.velocity.Y += 0.1f;
                Projectile.velocity.X *= 0.998f;
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.dontTakeDamage)
                {
                    if (Projectile.Hitbox.Intersects(npc.Hitbox))
                    {
                        npc.AddBuff(BuffID.Shimmer, 100);
                        Projectile.Kill();
                        return;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, new Vector2?(new Vector2((float)((int)Projectile.position.X), (float)((int)Projectile.position.Y))), null);
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 13, 0f, 0f, 0, default(Color), 1f);
            }
            for (int j = 0; j < 30; j++)
            {
                int index = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<ShimmerWaterDust>(), 0f, -2f, 0, default(Color), 1.1f);
                Main.dust[index].alpha = 100;
                Dust dust = Main.dust[index];
                dust.velocity.X = dust.velocity.X * 1.5f;
                Main.dust[index].velocity *= 3f;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                int num = (int)(Projectile.position.X + (float)(Projectile.width / 2)) / 16;
                int k = (int)(Projectile.position.Y + (float)(Projectile.height / 2)) / 16;
                WorldGen.Convert(num, k, 0, 4, tiles: true, walls: true);
            }
        }
    }
}
