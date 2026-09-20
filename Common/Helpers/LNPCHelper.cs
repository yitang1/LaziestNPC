using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using LaziestNPC.Globals.GlobalPlayers;
using Terraria.GameContent.Events;
using Terraria.ID;

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

        /* 拓展方法，在另一个地方对某一个类(LNPCPlayer)创建一个方法(LaziestNPC)，
		但对那个类的代码没有做任何涉及和改动。在其他地方可以直接去调用实例( player.LaziestNPC() )，
		看起来就像是在调用那个类的实例方法一样，但实际上不是那个类的。
        也就是所谓的“方法套方法”，套了一层皮，呃唔。*/
        public static LNPCPlayer LaziestNPC(this Player player)
        {
            return player.GetModPlayer<LNPCPlayer>();
        }

        //是否处于任意Boss战或事件期间
        public static bool AnyBossOrEvent()
        {
            if (NPC.AnyDanger())
                return true;

            /*[JITWhenModsEnabled("CalamityMod")]
            if (ModLoader.HasMod("CalamityMod") && AcidRainEvent.AcidRainEventIsOngoing)
                return true;*/
            return false;
        }
    }
}
