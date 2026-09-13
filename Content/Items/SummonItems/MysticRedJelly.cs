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
    public class MysticRedJelly : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 43;
            Item.height = 50;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player) => !NPC.AnyNPCs(NPCID.TownSlimeRed);

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 Pos = new Vector2(
                    player.Center.X + player.direction * Main.rand.Next(150, 250),
                    player.Center.Y
                );

                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)Pos.X, (int)Pos.Y, NPCID.TownSlimeRed);
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BloodMoonStarter)
                .AddIngredient(ItemID.Gel, 10)
                .AddIngredient(ItemID.FallenStar)
                .Register();
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            LNPCHelper.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.75f, new Vector2(0f, 0f));
            return false;
        }
    }
}
