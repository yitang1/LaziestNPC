

/*

        AllModBossLists.cs：提供Boss名单和类名，只被执行一次。

        ModBosses.cs：核心数据层，负责接收名单、监听到击杀后把状态改成true、存入世界存档、并提供查询当前状态的接口。

        AllModBossConditions.cs：包装层，把ModBosses(核心数据层)的查询接口包装成Terraria原生的Condition，方便商店直接调用。

        ModConditions.cs：生命周期调度器，在游戏加载时注册名单，在击杀时触发判定，在进出世界时保存和读取数据。

        AddShops(NPC的代码)：消费者，拿着Condition来过滤商品。

*/


/*
        这个系统的运行分为三个阶段：

        【阶段一：游戏加载（准备好花名册）】
        游戏刚启动时，ModConditions.cs 里的 Load() 方法被调用。

        它立刻执行 AllModBossLists.cs 里的 RegisterAll()。

        AllModBossLists.cs 把所有模组的 Boss 键名（比如 "DesertScourge"）和对应的 NPC 类名，打包塞进 ModBosses.cs 里的 AllBossInfos 字典中，初始击败状态全为 false。

        【阶段二：玩家实战击杀（记录击败状态）】
        玩家进入世界，打死了荒漠灾虫。

        ModConditions.cs 里的 ModBossesNPC : GlobalNPC 捕获到 OnKill 事件。

        它把死掉的 NPC 传给 ModBosses.cs 的 IsKilled() 方法。

        IsKilled() 遍历字典，发现死的这个 NPC 对应着 "DesertScourge"，于是把字典里这个 Boss 的 Downed 状态改为 true。

        玩家退出世界时，ModConditions.cs 调用 SaveWorldData()，ModBosses.cs 把这个 true 存进世界存档里。下次进游戏会通过 LoadWorldData() 重新读出来。

        【阶段三：打开 NPC 商店（实时判断是否解锁）】
        玩家右键点击 NPC，游戏开始构建商店列表（AddShops 方法）。

        AddShops 里写了：modBag.AddModItem("CalamityMod/DesertScourgeBag", (0,5,0,0), DownedDesertBug)。

        游戏看到商品绑定了 DownedDesertBug 这个条件，于是去问 AllModBossConditions.cs。

        AllModBossConditions.cs 里定义了：public static readonly Condition DownedDesertBug = ModBosses.GetCondition("DesertScourge");。

        商店底层触发这个条件的 .IsMet() 方法，去 ModBosses.cs 的字典里查 "DesertScourge" 的 Downed 当前到底是 true 还是 false。

        如果是 true，灾厄的宝藏袋显示在商店里；如果是 false，就不显示。

*/