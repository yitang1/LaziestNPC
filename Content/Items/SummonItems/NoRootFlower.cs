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
    public class NoRootFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 41;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player) => player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 Pos = new Vector2(
                    player.Center.X + player.direction * Main.rand.Next(400, 800),
                    player.Center.Y - Main.rand.Next(350, 800)
                );
                
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)Pos.X, (int)Pos.Y, NPCID.Plantera);
            }
            return true;
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar)
                .AddIngredient(ItemID.LifeFruit)
                .AddIngredient(ItemID.Moonglow, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.95f, new Vector2(0f, 0f));
            return false;
        }
    }
}
