using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace LaziestNPC.Common.Rarities
{
	public class Rainbow : ModRarity
	{
		//彩虹色稀有度
		public override Color RarityColor => new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
	}
}