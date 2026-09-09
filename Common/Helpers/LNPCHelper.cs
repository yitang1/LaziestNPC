using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LaziestNPC.Common.Helpers
{
    public static class LNPCHelper
    {
        //物品栏中的贴图缩放
        public static void DrawInventoryCustomScale(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale, float wantedScale = 1f, Vector2 drawOffset = default)
        {
            wantedScale = Math.Max(scale, wantedScale * Main.inventoryScale);
            position += drawOffset * wantedScale;
            spriteBatch.Draw(texture, position, new Rectangle?(frame), drawColor, 0f, origin, wantedScale, 0, 0f);
        }
    }
}
