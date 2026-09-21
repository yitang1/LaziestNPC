using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using LaziestNPC.Globals.GlobalOthers;
using LaziestNPC.Content.Items.SummonItems;
using LaziestNPC.Common.Helpers;
using LaziestNPC.Content.Buffs.Potions;
using LaziestNPC.Globals.GlobalMods.WeakReferences.CalamityMod;

namespace LaziestNPC.Globals.GlobalPlayers
{
    public class LNPCPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            triumph = false;
            yPower = false;
            cadence = false;
            revivify = false;
            penumbra = false;
            armorShattering = false;
            tScale = false;
            profanedRage = false;
            draconicSurge = false;
        }

        public override void UpdateDead()
        {
            triumph = false;
            yPower = false;
            cadence = false;
            revivify = false;
            penumbra = false;
            armorShattering = false;
            tScale = false;
            titanBoost = 0;
            profanedRage = false;
            draconicSurge = false;
        }

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

        public override void UpdateLifeRegen()
        {
            if (cadence)
            {
                /* 原版TR生命再生的公式里最后要除以2。每2秒恢复N点生命值，
                也就是说如果lifeRegen += 5
                那么游戏里实际显示和应用的生命再生速度为增加2.5点。*/
                Player.lifeRegen += 5;
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (revivify)
            {
                /* HealEffect让治疗溢出完全保留，先回血再扣血，溢出就会从溢出开始计算。
                Heal方法会多了一步强制等于最大生命值的钳制。*/
                int revivifyNum = (int)(info.Damage / 15.0);
                Player.statLife += revivifyNum;
                Player.HealEffect(revivifyNum, true);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (triumph)
            {
                #region 实现思路
                /* 【1. 计算敌人损失的血量比例】
                npc.life / (float)npc.lifeMax —— 敌人剩余的血量比例
                healthLost —— 敌人损失的血量比例
                (最大1，代表敌人损失了所有血量，即死亡)
                举例：100血，剩30，损失了70，即healthLost = 1 - 30/100 = 0.7 */

                /* 【2. 根据敌人损失的血量比例 计算 减伤率】
                当敌怪损失血量比例为1，最大减伤率就是0.15(15%)
                举例，reduction = 0.7 * 0.15 = 0.105，即减伤率为10.5% */

                /* 【3. 计算玩家最终受到的伤害】
                SourceDamage：在所有防御和伤害减免计算之前 的伤害
                1f - reduction为 剩余伤害倍率 ，乘以SourceDamage为受到的伤害值
                举例：1 - 0.105 = 0.895，即原来的伤害只剩下89.5% */

                //总结：敌人越残血，减伤效果越强，最大15%(其实应该是无限逼近于15%)
                #endregion
                float healthLost = 1f - npc.life / (float)npc.lifeMax;
                float reduction = healthLost * 0.15f;
                modifiers.SourceDamage *= 1f - reduction;
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //PostUpdateMiscEffects()这个方法在游戏里每秒调用60次，因此600的变量就是10秒
            Player.LaziestNPC().titanBoost = 600;
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //灾厄Mod的真近战武器
            if (TrueMeleeHelper.IsTrueMelee(proj))
            {
                titanBoost = 600;
            }
            //原版的特例(这几个就算剑刃本身也不触发效果，不知为何，暂且认为挥舞的伤害全是剑气)
            //暂时不知道有没有漏网之鱼😡
            if (proj.type == ProjectileID.NightsEdge
                || proj.type == ProjectileID.TrueNightsEdge
                || proj.type == ProjectileID.Excalibur
                || proj.type == ProjectileID.TrueExcalibur
                || proj.type == ProjectileID.TheHorsemansBlade)
            {
                titanBoost = 600;
            }
        }

        public override void PostUpdateEquips()
        {
            if (cadence)
            {
                #region 公式实现
                /*同样是提升最大生命值的25%，但是有两个公式可以实现：
				线性提升：Player.statLifeMax2 += (int)(Player.statLifeMax * 0.25);
				非线性提升：Player.statLifeMax2 += Player.statLifeMax / 5 / 20 * 25;

				这里的公式最终采用了和原版Terraria的生命力药水相似的非线性公式去计算，而不是固定地去乘以25%。
				目的是为了平衡，线性公式的情况下，无论玩家的最大生命值是多少，增加的量始终是statLifeMax的25%，
                这就导致在区间内，要么增加缓慢，要么突兀地大幅增加。
				
                而非线性公式是：每满100点最大生命值，增加25点，不足100的部分不计。
                这样在同一个百位区间内，增加量固定，比例随生命值上升而下降；
                跨过整百时增加量跳一档，比例回到25%。
                与原版生命力药水的非线性逻辑一致，避免线性百分比在高生命值时无限膨胀。*/
                #endregion
                Player.statLifeMax2 += Player.statLifeMax / 100 * 25;
            }
        }

        public override void PostUpdateMiscEffects()
        {
            if (yPower)
            {
                Player.GetDamage(DamageClass.Generic) += 0.05f;
                Player.GetCritChance(DamageClass.Generic) += 2f;
                Player.endurance += 0.04f;
                Player.statDefense += 10;
                Player.pickSpeed -= 0.1f;
                Player.GetKnockback(DamageClass.Summon) += 1f;
                Player.GetAttackSpeed(DamageClass.Melee) += 0.075f;
                Player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.075f;
                Player.moveSpeed += 0.075f;
            }

            if (cadence)
            {
                //生命拾心的药水效果
                Player.lifeMagnet = true;
            }

            if (armorShattering)
            {
                Player.GetDamage(DamageClass.Generic) += 0.08f;
                Player.GetCritChance(DamageClass.Generic) += 8f;

                if (!ModLoader.HasMod("CalamityMod"))
                {
                    Player.GetArmorPenetration(DamageClass.Generic) += 50f;
                }
            }

            if (tScale)
            {
                Player.endurance += 0.05f;
                Player.statDefense += 5;
                Player.kbBuff = true;
                if (titanBoost > 0)
                {
                    Player.statDefense += 25;
                    Player.endurance += 0.1f;
                }
            }
            else
            {
                titanBoost = 0;
            }

            if (titanBoost > 0)
            {
                titanBoost--;
            }

            if (profanedRage)
            {
                Player.GetCritChance(DamageClass.Generic) += 12f;
            }

            double flightTimeMult = 1f + (draconicSurge ? 0.2f : 0f);

            if (Player.wingTimeMax > 0)
            {
                Player.wingTimeMax = (int)(Player.wingTimeMax * flightTimeMult);
            }
            if (draconicSurge)
            {
                //Player.accRunSpeed += 0.1f;
                //Player.runAcceleration += 0.1f;
                Player.statDefense += 16;
            }
            //每次进游戏只会检查一次
            if (calamityRebornBuffType == -1)
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity)
                    && calamity.TryFind<ModBuff>("SilvaRevival", out ModBuff SilvaRevival))
                {
                    calamityRebornBuffType = SilvaRevival.Type;
                }
                else
                {
                    //标记已查找但不可用，不再重试
                    calamityRebornBuffType = -2; 
                }
            }
            if (calamityRebornCooldown > 0)
            {
                calamityRebornCooldown--;
            }
            //玩家触发灾厄里【始源林海套装】的无敌效果，并获得【始源林海无敌】增益时 ↓
            if (Player.LaziestNPC().draconicSurge && calamityRebornCooldown <= 0
                && calamityRebornBuffType > 0 && Player.HasBuff(calamityRebornBuffType))
            {
                Player.Heal(Player.statLifeMax2 / 2);
                // 5分钟冷却时间
                calamityRebornCooldown = 5 * 60 * 60;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (LNPCHelper.TryRebornWithTheReturner(Player))
            {
                //复活成功，阻止死亡
                playSound = false;
                genDust = false;
                return false; //返回false阻止玩家死亡
            }

            if (LNPCHelper.TryRebornWithLunarArmor(Player))
            {
                playSound = false;
                genDust = false;
                return false;
            }

            return true; //复活失败，正常死亡
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (penumbra && !ModLoader.HasMod("CalamityMod"))
            {
                bool inEventOrBoss = LNPCHelper.AnyBossOrEvent();
                int dodgeChance = inEventOrBoss ? 5 : 1;

                int roll = Main.rand.Next(100);
                if (roll < dodgeChance)
                {
                    Player.NinjaDodge();
                    return true;
                }
            }
            return false;
        }

        public bool EnablePre = true;
        public bool EnableHard = true;

        public bool triumph = false;
        public bool yPower = false;
        public bool cadence = false;
        public bool revivify = false;
        public bool penumbra = false;
        public bool armorShattering = false;
        public bool tScale = false;
        public int titanBoost = 0;
        public bool profanedRage = false;

        public bool draconicSurge = false;
        //-1表示未开始查找，-2表示查找失败(灾厄未加载或找不到)，大于0表示找到了
        private static int calamityRebornBuffType = -1;
        //每个玩家独立的冷却倒计时
        public int calamityRebornCooldown = 0;
    }
}
