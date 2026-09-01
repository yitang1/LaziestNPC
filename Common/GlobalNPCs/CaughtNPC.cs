using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using LaziestNPC.Content.NPCs.TownNPCs;

namespace LaziestNPC.Common.GlobalNPCs
{
    public class CaughtNPC : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod) => TheNpcId != NPCID.None;
        
        internal static Dictionary<int, int> CaughtTownNpcs = new();

        private string _name;
        public int TheNpcId;

        /*_name在类中进行工作，例如被Add添加不同的NPC名字
        来自多层父类中的Name去读取_name这个私有字段，
        tmod通过Name知道每一个Name都是独立的、不冲突的实例，也就是游戏中的每一个NPC物品形式，
        “用同一个类生成不同实例”的场景，都可以套用这个思路*/
        public override string Name => _name;

        //空壳构造函数，作保险用，让tmod时时刻刻知道还有这么个可供缓冲的、安全的空壳
        public CaughtNPC()
        {
            _name = base.Name;
            TheNpcId = NPCID.None;
        }

        //进行实际工作的构造函数。被Add调用后，就是一个完整的NPC物品的信息
        public CaughtNPC(string theNpcName, int theNpcId)
        {
            _name = theNpcName;
            TheNpcId = theNpcId;
        }

        protected override bool CloneNewInstances => true;

        /*因为物品有_name和TheNpcId这样的“专属标签”，
        所以在拆分堆叠时，必须用Clone方法复制一份完整的标签*/
        public override ModItem Clone(Item item)
        {
            CaughtNPC clone = base.Clone(item) as CaughtNPC;
            clone._name = _name;
            clone.TheNpcId = TheNpcId;
            return clone;
        }

        public override bool IsCloneable => true;

        //静态字典的内容，在游戏退出或重载时要主动清理掉
        public override void Unload()
        {
            CaughtTownNpcs.Clear();
        }

        public override string Texture => TheNpcId < NPCID.Count ? $"Terraria/Images/NPC_{TheNpcId}" : NPCLoader.GetNPC(TheNpcId).Texture;

        public override void SetStaticDefaults()
        {
            //为该物品类型注册一个动画
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, Main.npcFrameCount[TheNpcId]));
            //告诉游戏该物品在背包中也应显示动画
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            //tmod内置方法，自动设置该物品为“捕捉生物”类型，有那些可堆叠、可消耗的属性
            Item.DefaultToCapturedCritter(TheNpcId);
            Item.rare = ItemRarityID.Blue;
        }

        public static void Add(string theNpcName, int id)
        {
            CaughtNPC item = new(theNpcName, id);
            //为游戏中添加一个具有自己的Name和ID的新物品
            ModContent.GetInstance<LaziestNPC>().AddContent(item);
            CaughtTownNpcs.Add(id, item.Type);
        }

        public static void RegisterItems()
        {
            CaughtTownNpcs = new Dictionary<int, int>();

            //添加NPC
            Add("BossNPC", ModContent.NPCType<BossNPC>());
            Add("PotionNPC", ModContent.NPCType<PotionNPC>());
            Add("BuildNPC", ModContent.NPCType<BuildNPC>());
            Add("NatureNPC", ModContent.NPCType<NatureNPC>());
            Add("OreNPC", ModContent.NPCType<OreNPC>());
            Add("MaterialNPC", ModContent.NPCType<MaterialNPC>());
            Add("TrinketNPC", ModContent.NPCType<TrinketNPC>());
        }
    }

    public class CatchGlobalNPC : GlobalNPC
    {
        /*public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<LaziestNPCConfig>().EnableCatching && TheNpcId != NPCID.None;
        }*/

        public override void SetDefaults(NPC npc)
        {
            if (CaughtNPC.CaughtTownNpcs.ContainsKey(npc.type))
            {
                //捕捉NPC后获得对应的物品，字典里的ID和捕捉的物品ID对应
                npc.catchItem = CaughtNPC.CaughtTownNpcs[npc.type];
                //全局生效，NPC在被释放时强制保持友善状态
                Main.npcCatchable[npc.type] = true;
            }
        }
    }
}