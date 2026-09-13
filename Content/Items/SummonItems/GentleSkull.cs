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
    public class GentleSkull : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 39;
            Item.height = 49;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.rare = ItemRarityID.Orange;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player) => !Main.dayTime && !NPC.AnyNPCs(NPCID.SkeletronHead);

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 Pos = new Vector2(
                    player.Center.X + player.direction * Main.rand.Next(500, 800),
                    player.Center.Y - Main.rand.Next(500, 800)
                );

                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)Pos.X, (int)Pos.Y, NPCID.SkeletronHead);
            }
            return true;
        }

        /*public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID., 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }*/

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f));
            return false;
        }
    }
}
