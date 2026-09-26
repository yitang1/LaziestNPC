using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Globals.GlobalPlayers;

namespace LaziestNPC.Globals.GlobalNPCs
{
    public class LNPCGlobalNPC : GlobalNPC
    {
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (player.whoAmI != Main.myPlayer)
                return;
            //粉碎药水
            if (player.LaziestNPC().armorShattering)
            {
                InflictArmorCrunch(npc);
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (!projectile.friendly || projectile.owner != Main.myPlayer)
                return;

            Player player = Main.player[projectile.owner];
            //粉碎药水
            if (player.LaziestNPC().armorShattering)
            {
                InflictArmorCrunch(npc);
            }
        }

        private void InflictArmorCrunch(NPC npc)
        {
            int duration;
            int roll = Main.rand.Next(8); //生成0到7的整数
            // 2/8=25%概率，6秒
            if (roll < 2)
                duration = 6 * 60;
            // 3/8=37.5%概率，4秒
            else if (roll < 5)
                duration = 4 * 60;
            // 3/8=37.5%概率，2秒
            else
                duration = 2 * 60;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (calamityMod.TryFind<ModBuff>("ArmorCrunch", out ModBuff ArmorCrunch))
                {

                    npc.AddBuff(ArmorCrunch.Type, duration);
                }
            }
        }
    }
}
