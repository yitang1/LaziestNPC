using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using LaziestNPC.Common.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria.Localization;

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
            Item.value = Item.buyPrice(0, 35, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string extraText;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.ForbiddenPaper.ContentTexts");
            else
                extraText = Language.GetTextValue("Mods.LaziestNPC.Items.ForbiddenPaper.CommonTips");

            foreach (TooltipLine line in tooltips)
            {
                line.Text = line.Text.Replace("[ExtraText]", extraText);
            }
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
