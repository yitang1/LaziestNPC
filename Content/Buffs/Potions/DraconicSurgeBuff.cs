using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LaziestNPC.Globals.GlobalPlayers;
using LaziestNPC.Common.Helpers;

namespace LaziestNPC.Content.Buffs.Potions
{
    public class DraconicSurgeBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.LaziestNPC().draconicSurge = true;
		}
	}
}