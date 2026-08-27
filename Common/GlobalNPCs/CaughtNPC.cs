using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using LaziestNPC.Content.NPCs.TownNPCs;

namespace LaziestNPC.Common.GlobalNPCs
{
    public class CaughtNPCItem : ModItem
    {
        internal static Dictionary<int, int> CaughtTownies = new();

        public override string Name => _name;

        public string _name;
        public int AssociatedNpcId;
        public string NpcQuote;

        public CaughtNPCItem()
        {
            _name = base.Name;
            AssociatedNpcId = NPCID.None;
            NpcQuote = "";
        }

        public CaughtNPCItem(string internalName, int associatedNpcId, string npcQuote = "")
        {
            _name = internalName;
            AssociatedNpcId = associatedNpcId;
            NpcQuote = npcQuote;
        }

        public override bool IsLoadingEnabled(Mod mod) => AssociatedNpcId != NPCID.None;

        protected override bool CloneNewInstances => true;

        public override ModItem Clone(Item item)
        {
            CaughtNPCItem clone = base.Clone(item) as CaughtNPCItem;
            clone._name = _name;
            clone.AssociatedNpcId = AssociatedNpcId;
            clone.NpcQuote = NpcQuote;
            return clone;
        }

        public override bool IsCloneable => true;

        public override void Unload()
        {
            CaughtTownies.Clear();
        }

        public override string Texture => AssociatedNpcId < NPCID.Count ? $"Terraria/Images/NPC_{AssociatedNpcId}" : NPCLoader.GetNPC(AssociatedNpcId).Texture;

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, Main.npcFrameCount[AssociatedNpcId]));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToCapturedCritter(AssociatedNpcId);
            Item.rare = ItemRarityID.Blue;
        }

        public static void RegisterItems()
        {
            CaughtTownies = new Dictionary<int, int>();

            //添加NPC
            Add("BossNPC", ModContent.NPCType<BossNPC>());
            Add("PotionNPC", ModContent.NPCType<PotionNPC>());
        }

        public static void Add(string internalName, int id)
        {
            CaughtNPCItem item = new(internalName, id);
            ModContent.GetInstance<LaziestNPC>().AddContent(item);
            CaughtTownies.Add(id, item.Type);
        }
    }

    public class CatchGlobalNPC : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            if (CaughtNPCItem.CaughtTownies.ContainsKey(npc.type))
            {
                npc.catchItem = CaughtNPCItem.CaughtTownies[npc.type];
                Main.npcCatchable[npc.type] = true;
            }
        }
    }
}