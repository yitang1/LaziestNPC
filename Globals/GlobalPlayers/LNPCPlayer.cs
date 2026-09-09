using LaziestNPC.Content.Items.SummonItems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using LaziestNPC.Globals.GlobalOthers;

namespace LaziestNPC.Globals.GlobalPlayers
{
    public class LNPCPlayer : ModPlayer
    {
        public bool EnablePre = true;
        public bool EnableHard = true;

        //确保状态的数据保存和持久化
        public override void SaveData(TagCompound tag)
        {
            tag["EnablePre"] = EnablePre;
            tag["EnableHard"] = EnableHard;
        }

        public override void LoadData(TagCompound tag)
        {
            EnablePre = tag.GetBool("EnablePre");
            EnableHard = tag.GetBool("EnableHard");
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (TryReborn())
            {
                //复活成功，阻止死亡
                playSound = false;
                genDust = false;
                return false; //返回false阻止玩家死亡
            }
            return true; //复活失败，正常死亡
        }

        //TheReturner的复活逻辑封装
        private bool TryReborn()
        {
            //检查背包中是否收藏了TheReturner
            bool hasFavorited = false;
            foreach (Item item in Player.inventory)
                if (item.type == ModContent.ItemType<TheReturner>() && item.favorited) { hasFavorited = true; break; }
            if (!hasFavorited) return false;

            //判断可用次数
            bool canRevive = true;
            if (!Main.hardMode && EnablePre)
            {
                canRevive = true;
                EnablePre = false;
            }
            else if (Main.hardMode && EnableHard)
            {
                canRevive = true;
                EnableHard = false;
            }
            if (!canRevive) return false;

            //50%概率
            if (Main.rand.NextFloat() >= 0.5f) return false;

            //执行复活
            Player.statLife += Player.statLifeMax2;
            Player.HealEffect(Player.statLifeMax2, true);
            Player.immune = true;
            Player.immuneTime = 120;
            Player.dead = false;

            //雷击特效
            LightningFlashDust.SpawnBolt(Player.Center);
            SoundEngine.PlaySound(SoundID.Thunder, Player.Center);
            Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Reborn"), new Color(255, 70, 123));

            return true;
        }
    }
}
