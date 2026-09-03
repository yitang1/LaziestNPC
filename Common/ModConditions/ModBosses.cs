using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace LaziestNPC.Common.ModBossess
{
    /// <summary>
    /// 通用Boss状态检查，用于检测任意模组的Boss是否被击败。<br/>
    /// 用法：在ModSystem.Load中注册Boss，<br/>
    /// 然后在NPC商店中使用ModBosses.GetCondition("唯一键")。
    /// </summary>
    public static class ModBosses
    {
        /// <summary>
        /// 封装单个Boss的完整信息
        /// </summary>
        private class BossInfo
        {   
            //模组实例(可能为null，表示模组未加载)
            public Mod Mod; 
            public string ModName;
            /*Boss类名是数组，因为存在特殊情况，比如蠕虫类Boss
            甚至还有的Boss同样存在多个部位共用整体血量的情况
            击败任意一个关节Boss都会死亡，因此也都算做击败Boss*/
            public string[] BossNames;  
            public bool Downed;
        }

        //存储所有注册的Boss信息的字典
        //键：唯一标识(格式 "模组名/Boss类名")
        //值：Boss信息(模组实例、NPC类型、击败状态)
        private static readonly Dictionary<string, BossInfo> AllBossInfos = new();

        /// <summary>
        /// 注册方法，添加一个Boss。
        /// </summary>
        /// <param name="bossKey">唯一标识，例如"CalamityMod/Crabulon"</param>
        /// <param name="modName">模组内部名称，例如"CalamityMod"</param>
        /// <param name="bossName">Boss类名，例如"Crabulon"</param>
        public static void Register(string bossKey, string modName, string bossName)
        {
            Register(bossKey, modName, new string[] { bossName });
        }

        /// <summary>
        /// 注册添加一个Boss，支持多部位(例如蠕虫类头身尾)。
        /// </summary>
        /// <param name="bossKey">唯一标识，例如"CalamityMod/DesertScourge"</param>
        /// <param name="modName">模组内部名称，例如"CalamityMod"</param>
        /// <param name="bossNames">Boss所有部位类名数组，例如 "DesertScourgeHead","DesertScourgeBody"</param>
        public static void Register(string bossKey, string modName, string[] bossNames)
        {
            //防止重复注册同一个Boss
            if (AllBossInfos.ContainsKey(bossKey))
                return;

            //尝试获取模组实例(不强制加载)
            ModLoader.TryGetMod(modName, out Mod mod);

            //存储Boss信息到他喵的字典里
            AllBossInfos[bossKey] = new BossInfo
            {
                Mod = mod,
                ModName = modName,
                BossNames = bossNames,
                Downed = false
            };
        }

        /// <summary>
        /// 获取一个Boss的击败状态：Condition。<br/>
        /// 如果Boss未注册或模组未加载，返回恒为false的条件。
        /// </summary>
        public static Condition GetCondition(string bossKey)
        {
            if (AllBossInfos.TryGetValue(bossKey, out BossInfo info))
            {
                return new Condition($"ModBosses.{bossKey}", () => info.Downed);
            }
            return new Condition($"ModBosses.{bossKey}_Missing", () => false);
        }

        /// <summary>
        /// 在GlobalNPC.OnKill中调用，<br/>
        /// 检测击杀的NPC是否匹配已注册的Boss。
        /// </summary>
        public static void IsKilled(NPC npc)
        {
            //遍历并检查所有注册的Boss
            foreach (var kvp in AllBossInfos)
            {
                BossInfo info = kvp.Value;

                //如果玩家对适配的某个模组未加载，跳过
                if (info.Mod == null)
                    continue;

                //遍历Boss的所有部位类名
                foreach (string bossName in info.BossNames)
                {
                    if (info.Mod.TryFind(bossName, out ModNPC modNpc) && npc.type == modNpc.Type)
                    {
                        info.Downed = true;
                        break; //匹配任意一个部位即标记击败，就特喵的跳出循环
                    }
                }
            }
        }

        /// <summary>
        /// 重置所有状态(在OnWorldLoad和OnWorldUnload中调用)
        /// </summary>
        public static void ResetAll()
        {
            foreach (var info in AllBossInfos.Values)
            {
                info.Downed = false;
            }
        }

        /// <summary>
        /// 保存所有状态(在ModSystem.SaveWorldData中调用)
        /// </summary>
        public static void SaveData(TagCompound tag)
        {
            foreach (var kvp in AllBossInfos)
            {
                tag.Add($"LaziestNPC_{kvp.Key}", kvp.Value.Downed);
            }
        }

        /// <summary>
        /// 加载所有状态(在ModSystem.LoadWorldData中调用)
        /// </summary>
        public static void LoadData(TagCompound tag)
        {
            foreach (var kvp in AllBossInfos)
            {
                string key = $"LaziestNPC_{kvp.Key}";
                //判断指定的键是否存在
                if (tag.ContainsKey(key))
                {
                    kvp.Value.Downed = tag.Get<bool>(key);
                }
            }
        }
    }
}
