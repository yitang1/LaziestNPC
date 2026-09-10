using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalOthers
{
    public class LightningFlashDust : ModDust
    {
        public override string Texture => null;

        public override void OnSpawn(Dust dust)
        {
            int desiredVanillaDustTexture = 229;
            int frameX = desiredVanillaDustTexture * 10 % 1000;
            int frameY = desiredVanillaDustTexture * 10 / 1000 * 30 + Main.rand.Next(3) * 10;
            dust.frame = new Rectangle(frameX, frameY, 8, 8);

            dust.noGravity = true;
            dust.velocity = Vector2.Zero;
            dust.scale = 2.0f + Main.rand.NextFloat(1.0f);
            dust.rotation = Main.rand.NextFloat(6.28f);
            dust.alpha = 100;
        }

        public override bool Update(Dust dust)
        {
            //逐渐缩小并淡出
            dust.scale *= 0.985f;
            dust.alpha += 1;

            //位置微飘(让闪电有“闪烁”感)
            dust.position += new Vector2(
                Main.rand.NextFloat(-1f, 1f),
                Main.rand.NextFloat(-1f, 1f)
            );

            if (dust.scale < 0.1f || dust.alpha > 250)
                dust.active = false;

            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            //白色带淡蓝，随alpha淡出
            return new Color(200, 220, 255, 255 - dust.alpha);
        }

        public static void SpawnBolt(Vector2 position)
        {
            //落点爆炸：大量闪电Dust向四周扩散
            for (int i = 0; i < 80; i++)
            {
                Vector2 velocity = new Vector2(
                    Main.rand.NextFloat(-50f, 50f),
                    Main.rand.NextFloat(-50f, 50f)
                );
                Dust dust = Dust.NewDustDirect(
                    position - new Vector2(4, 4),
                    8, 8,
                    ModContent.DustType<LightningFlashDust>(),
                    velocity.X, velocity.Y,
                    100,
                    default,
                    1.5f + Main.rand.NextFloat(0.5f)
                );
                dust.noGravity = true;
            }

            //垂直闪电柱：从上方劈下
            Vector2 start = position - new Vector2(0, 600f);
            int segments = 25;
            for (int i = 0; i <= segments; i++)
            {
                float progress = i / (float)segments;
                Vector2 currentPos = Vector2.Lerp(start, position, progress);

                //随机偏移让闪电弯曲
                if (i > 0 && i < segments)
                {
                    currentPos.X += Main.rand.NextFloat(-20f, 20f);
                    currentPos.Y += Main.rand.NextFloat(-10f, 10f);
                }

                //主柱：密集 Dust
                for (int j = 0; j < 10; j++)
                {
                    Vector2 offset = new Vector2(
                        Main.rand.NextFloat(-10f, 10f),
                        Main.rand.NextFloat(-3f, 3f)
                    );
                    Dust dust = Dust.NewDustDirect(
                        currentPos + offset - new Vector2(2, 2),
                        4, 4,
                        ModContent.DustType<LightningFlashDust>(),
                        0, 0,
                        100,
                        default,
                        1.2f + Main.rand.NextFloat(0.5f)
                    );
                    dust.noGravity = true;
                }

                //分支闪电
                if (Main.rand.NextBool(2) && i > 5 && i < segments - 5)
                {
                    Vector2 branchPos = currentPos + new Vector2(
                        Main.rand.NextFloat(-80f, 80f),
                        Main.rand.NextFloat(10f, 80f)
                    );
                    for (int k = 0; k < 8; k++)
                    {
                        Dust dust = Dust.NewDustDirect(
                            branchPos - new Vector2(2, 2),
                            4, 4,
                            ModContent.DustType<LightningFlashDust>(),
                            Main.rand.NextFloat(-1f, 1f),
                            Main.rand.NextFloat(-1f, 1f),
                            100,
                            default,
                            0.8f + Main.rand.NextFloat(0.3f)
                        );
                        dust.noGravity = true;
                    }
                }
            }
        }
    }
}