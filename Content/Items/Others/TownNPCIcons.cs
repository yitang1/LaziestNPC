using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LaziestNPC.Content.Items.Others
{
    public class BossNPCIcon : ModItem
    {
        public override string Texture => "LaziestNPC/Content/NPCs/TownNPCs/BossNPC_Head";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.White;
        }
    }

    public class PotionNPCIcon : ModItem
    {
        public override string Texture => "LaziestNPC/Content/NPCs/TownNPCs/PotionNPC_Head";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.White;
        }
    }

    public class MaterialNPCIcon : ModItem
    {
        public override string Texture => "LaziestNPC/Content/NPCs/TownNPCs/MaterialNPC_Head";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.White;
        }
    }

    public class BuildNPCIcon : ModItem
    {
        public override string Texture => "LaziestNPC/Content/NPCs/TownNPCs/BuildNPC_Head";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.White;
        }
    }

    public class NatureNPCIcon : ModItem
    {
        public override string Texture => "LaziestNPC/Content/NPCs/TownNPCs/NatureNPC_Head";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.White;
        }
    }

    public class OreNPCIcon : ModItem
    {
        public override string Texture => "LaziestNPC/Content/NPCs/TownNPCs/OreNPC_Head";
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.White;
        }
    }
}
