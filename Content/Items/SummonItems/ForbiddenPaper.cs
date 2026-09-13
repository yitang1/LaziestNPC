using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using LaziestNPC.Common.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace LaziestNPC.Content.Items.SummonItems
{
    public class ForbiddenPaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 49;
            Item.height = 61;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player) => !NPC.AnyNPCs(NPCID.CultistBoss);

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 Pos = new Vector2(
                    player.Center.X + player.direction * Main.rand.Next(400, 800),
                    player.Center.Y - Main.rand.Next(350, 800)
                );

                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)Pos.X, (int)Pos.Y, NPCID.CultistBoss);
            }
            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.7f, new Vector2(0f, 0f));
            return false;
        }
    }
}
