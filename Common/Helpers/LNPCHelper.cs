using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ModLoader;
using LaziestNPC.Globals.GlobalPlayers;
using Terraria.GameContent.Events;
using Terraria.Audio;
using LaziestNPC.Content.Items.SummonItems;
using LaziestNPC.Globals.GlobalOthers;

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

        //物品【TheReturner】的复活逻辑封装
        public static bool TryRebornWithTheReturner(Player player)
        {
            LNPCPlayer LPlayer = player.LaziestNPC();

            //【第一层判定】检查背包中是否收藏了TheReturner
            bool hasFavorited = false;
            foreach (Item item in player.inventory)
                if (item.type == ModContent.ItemType<TheReturner>() && item.favorited) { hasFavorited = true; break; }
            if (!hasFavorited) return false;

            //【第二层判定】判断可用次数
            if (!Main.hardMode && LPlayer.EnablePre)
            {
                LPlayer.EnablePre = false;
            }
            else if (Main.hardMode && LPlayer.EnableHard)
            {
                LPlayer.EnableHard = false;
            }
            else
            {
                return false;
            }

            //【第三层判定】50%概率
            if (Main.rand.NextFloat() >= 0.5f) return false;

            //执行复活
            player.statLife += player.statLifeMax2;
            player.HealEffect(player.statLifeMax2, true);
            player.immune = true;
            player.immuneTime = 120;
            player.dead = false;

            //雷击特效
            LightningFlashDust.SpawnBolt(player.Center);
            SoundEngine.PlaySound(SoundID.Thunder, player.Center);
            Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Reborn"), new Color(255, 70, 123));

            return true;
        }

        //药水【龙魂秘药】在原版的功能
        public static bool TryRebornWithLunarArmor(Player player)
        {
            if (ModLoader.HasMod("CalamityMod"))
                return false;

            LNPCPlayer LPlayer = player.LaziestNPC();

            if (!LPlayer.draconicSurge) return false;
            if (LPlayer.calamityRebornCooldown > 0) return false;
            if (!IsWearingLunarArmorSet(player)) return false;

            player.Heal(player.statLifeMax2 / 2);
            player.immune = true;
            player.immuneTime = 300;
            player.dead = false;

            LPlayer.calamityRebornCooldown = 5 * 60 * 60;
            return true;
        }

        //药水【龙魂秘药】在原版功能实现的判定
        public static bool IsWearingLunarArmorSet(Player player)
        {
            int head = player.armor[0].type;
            int body = player.armor[1].type;
            int legs = player.armor[2].type;

            if (head == ItemID.SolarFlareHelmet
                && body == ItemID.SolarFlareBreastplate
                && legs == ItemID.SolarFlareLeggings)
                return true;

            if (head == ItemID.VortexHelmet
                && body == ItemID.VortexBreastplate
                && legs == ItemID.VortexLeggings)
                return true;

            if (head == ItemID.NebulaHelmet
                && body == ItemID.NebulaBreastplate
                && legs == ItemID.NebulaLeggings)
                return true;

            if (head == ItemID.StardustHelmet
                && body == ItemID.StardustBreastplate
                && legs == ItemID.StardustLeggings)
                return true;

            return false;
        }
    }
}
