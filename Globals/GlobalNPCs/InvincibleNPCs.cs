using LaziestNPC.Globals.Config;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Globals.GlobalNPCs
{
    public class InvincibleNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(NPC npc)
        {
            if (LNPCConfig.Instance.LNPCInvincible)
            {
                if (npc.townNPC || npc.type == NPCID.BoundGoblin || npc.type == NPCID.BoundMechanic
                    || npc.type == NPCID.BoundWizard || npc.type == NPCID.BartenderUnconscious
                    || npc.type == NPCID.WebbedStylist || npc.type == NPCID.SleepingAngler)
                {
                    npc.lavaImmune = true;
                    npc.dontTakeDamage = true;
                    npc.dontTakeDamageFromHostiles = true;
                }
            }

            if (LNPCConfig.Instance.LNPCCritterInvincible)
            {
                if (npc.CountsAsACritter)
                {
                    npc.friendly = true;
                    npc.lavaImmune = true;
                    npc.dontTakeDamage = true;
                    npc.dontTakeDamageFromHostiles = true;
                }
            }
        }
    }
}
