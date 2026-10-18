using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Terraria.Localization;
using LaziestNPC.Content.NPCs.TownNPCs;
using LaziestNPC.Globals.Config;

namespace LaziestNPC.Common.UI
{
    public class PortraitUISystem : ModSystem
    {
        //1.跨帧保存
        //currentPortraitPath 存当前对话该显示哪张肖像图。
        //lastTalkNPC 记录上一帧正在对话的NPC索引，用来判断“对话是不是刚刚开始”。
        private string currentPortraitPath = null;
        //缓存贴图
        private Texture2D currentPortraitTexture = null;
        //缓存名字
        private string currentNpcName = null;
        private Vector2 currentNameSize;
        private int lastTalkNPC = -1;
        private int portraitAnimTimer = 0; //弹跳计时器

        //2.注册绘制层
        //找到原版对话窗口所在的图层，在它后面插入肖像层。
        //这样肖像会绘制在对话窗口旁边，且不会挡住对话内容。
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int dialogIndex = layers.FindIndex(layer => layer.Name == "Vanilla: NPC / Sign Dialog");
            if (dialogIndex == -1) return;

            layers.Insert(dialogIndex + 1, new LegacyGameInterfaceLayer(
                "LaziestNPC: NPC Portrait",
                () => { DrawPortrait(); return true; },
                InterfaceScaleType.UI));
        }

        //3.主绘制逻辑
        private void DrawPortrait()
        {
            Player player = Main.LocalPlayer;

            if (!LNPCConfig.Instance.ShowPortrait)
            {
                currentPortraitPath = null;
                currentPortraitTexture = null;
                lastTalkNPC = -1;
                currentNpcName = null;
                currentNameSize = Vector2.Zero;
                return;
            }

            //(1)：判断对话是否开启
            //没有对话文本，或者没有和任何NPC对话，就清空缓存，不绘制。
            //清空缓存是为了下次对话时能重新随机一张肖像。
            if (Main.npcChatText == "" || player.talkNPC < 0)
            {
                currentPortraitPath = null;
                //清空贴图缓存
                currentPortraitTexture = null; 
                lastTalkNPC = -1;
                //清空名字缓存
                currentNpcName = null;
                currentNameSize = Vector2.Zero;
                return;
            }

            NPC npc = Main.npc[player.talkNPC];

            //(2)：对话刚开始时，随机一次肖像图
            //player.talkNPC和lastTalkNPC不同，说明玩家刚开始和这个NPC对话。
            //此时随机一次，把结果存进currentPortraitPath。
            //之后每帧player.talkNP都不变，不会再触发随机，肖像稳定不闪。
            if (player.talkNPC != lastTalkNPC)
            {
                currentPortraitPath = PickPortraitPath(npc);

                //一次性请求贴图并缓存
                if (currentPortraitPath != null)
                    currentPortraitTexture = ModContent.Request<Texture2D>(
                        currentPortraitPath,
                        ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                else
                    currentPortraitTexture = null;

                //计算名字和尺寸，只做一次
                if (npc.ModNPC != null)
                    currentNpcName = Language.GetTextValue($"Mods.LaziestNPC.NPCs.{npc.ModNPC.Name}.PortraitName");
                else
                    currentNpcName = npc.HasGivenName ? npc.TypeName + " " + npc.GivenName : npc.TypeName;

                currentNameSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, currentNpcName, Vector2.One);

                portraitAnimTimer = 0;
            }
            lastTalkNPC = player.talkNPC;

            //(3)：没有对应肖像就不绘制
            if (currentPortraitTexture == null)
                return;

            //(4)：计算对话框位置
            //这个公式来自原版 DrawNPCChatButtons，会随屏幕宽度自动变化。
            //dialogLeft 是对话框内容区的左边界，dialogTop 是顶部基准。
            float dialogLeft = 180 + (Main.screenWidth - 800) / 2f;
            float dialogTop = 130;

            //(5)：计算肖像框位置
            //size 是肖像框边长，gap 是肖像框和对话框之间的间距。
            //offsetX / offsetY 是手动微调值，用来修正视觉上的偏移。

            float size = 112f;
            float gap = 8f;
            float offsetX = -28f; //负值往左，正值往右
            float offsetY = -25f; //负值往上，正值往下

            //(6) 计算弹跳偏移
            portraitAnimTimer++;
            float bounceOffset = 0f;
            if (portraitAnimTimer < 20)
            {
                float t = portraitAnimTimer / 20f;
                bounceOffset = -10f * MathF.Sin(t * MathF.PI);
            }
            else if (portraitAnimTimer < 40)
            {
                float t = (portraitAnimTimer - 20) / 20f;
                bounceOffset = -5f * MathF.Sin(t * MathF.PI);
            }

            float portraitX = dialogLeft - size - gap + offsetX;
            float portraitY = dialogTop + offsetY + bounceOffset;

            //(7)：绘制背景框
            Texture2D bg = ModContent.Request<Texture2D>("LaziestNPC/Assets/UI/PortraitBackground").Value;
            Main.spriteBatch.Draw(bg,
                new Rectangle((int)portraitX, (int)portraitY, (int)size, (int)size),
                Color.White);

            //(8)：绘制肖像图
            //直接使用对话开始时缓存好的贴图，不再每帧请求
            Texture2D portrait = currentPortraitTexture;

            float drawWidth;
            float drawHeight;
            float drawX;
            float drawY;

            if (currentPortraitPath.Contains("/Vanilla/"))
            {
                //原版肖像：200×200的图，角色占中间100×100。
                //画成 224×224，角色正好变成112×112，与背景框一致。
                drawWidth = size * 1.8f;
                drawHeight = size * 1.8f;
                drawX = portraitX + (size - drawWidth) / 2f;
                drawY = portraitY + (size - drawHeight) / 2f;
            }
            else
            {
                //Mod肖像：按原图宽高比计算绘制高度。宽度固定为size，高度按比例换算。
                float ratio = (float)portrait.Height / portrait.Width;
                drawWidth = size;
                drawHeight = size * ratio;
                //水平方向和背景框对齐，垂直方向底部对齐背景框底部。
                //这样如果肖像比框高，超出部分会向上溢出，不会被裁掉。
                drawX = portraitX;
                drawY = portraitY + size - drawHeight;
            }

            Main.spriteBatch.Draw(portrait,
                new Rectangle((int)drawX, (int)drawY, (int)drawWidth, (int)drawHeight),
                Color.White);

            //(9)：绘制NPC名字
            //名字显示在肖像框下方，水平居中。
            string npcName;
            if (npc.ModNPC != null)
            {
                //模组NPC
                npcName = Language.GetTextValue($"Mods.LaziestNPC.NPCs.{npc.ModNPC.Name}.PortraitName");
            }
            else
            {
                //原版NPC
                //TypeName会根据语言环境返回对应的职业名文本，即中文是“向导”，英文是"Guide"
                //GivenName是NPC的名字，但有些NPC貌似并没有，例如圣诞老人
                npcName = npc.HasGivenName ? npc.TypeName + " " + npc.GivenName : npc.TypeName;
            }
            Vector2 nameSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, npcName, Vector2.One);
            float nameX = portraitX + (size - currentNameSize.X) / 2f;
            float nameY = portraitY + size + 2f;
            Utils.DrawBorderString(Main.spriteBatch, currentNpcName, new Vector2(nameX, nameY), Color.White);
        }

        private static readonly Dictionary<int, string> VanillaPortraits = new()
        {
            //原版城镇NPC
            { NPCID.Guide, "Guide" }, //向导
            { NPCID.Merchant, "Merchant" }, //商人
            { NPCID.Nurse, "Nurse" }, //护士
            { NPCID.Demolitionist, "Demolitionist" }, //爆破专家
            { NPCID.DyeTrader, "DyeTrader" }, //染料商
            { NPCID.Angler, "Angler" }, //渔夫
            //{ NPCID.BestiaryGirl, "BestiaryGirl" }, //动物学家
            { NPCID.Dryad, "Dryad" }, //树妖
            { NPCID.Painter, "Painter" }, //油漆工
            { NPCID.Golfer, "Golfer" }, //高尔夫球手
            { NPCID.ArmsDealer, "ArmsDealer" }, //军火商
            { NPCID.DD2Bartender, "DD2Bartender" }, //酒馆老板
            { NPCID.Stylist, "Stylist" }, //发型师
            { NPCID.GoblinTinkerer, "GoblinTinkerer" }, //哥布林工匠
            { NPCID.WitchDoctor, "WitchDoctor" }, //巫医
            { NPCID.Clothier, "Clothier" }, //服装商
            { NPCID.Mechanic, "Mechanic" }, //机械师
            { NPCID.PartyGirl, "PartyGirl" }, //派对女孩
            { NPCID.Wizard, "Wizard" }, //巫师
            { NPCID.TaxCollector, "TaxCollector" }, //税收官
            { NPCID.Truffle, "Truffle" }, //松露人
            { NPCID.Pirate, "Pirate" }, //海盗
            { NPCID.Steampunker, "Steampunker" }, //蒸汽朋克人
            { NPCID.Cyborg, "Cyborg" }, //机器侠
            { NPCID.SantaClaus, "SantaClaus" }, //圣诞老人
            { NPCID.Princess, "Princess" }, //公主

            { NPCID.TravellingMerchant, "TravellingMerchant" }, //旅商
            { NPCID.OldMan, "OldMan" }, //老人
            { NPCID.SkeletonMerchant, "SkeletonMerchant" }, //骷髅商人

            //城镇史莱姆
            { NPCID.TownSlimeCopper, "TownSlimeCopper" }, //侍卫史莱姆
            { NPCID.TownSlimePurple, "TownSlimePurple" }, //笨拙史莱姆
            { NPCID.TownSlimeBlue, "TownSlimeBlue" }, //书呆子史莱姆
            { NPCID.TownSlimeRed, "TownSlimeRed" }, //暴躁史莱姆
            { NPCID.TownSlimeYellow, "TownSlimeYellow" }, //神秘史莱姆
            { NPCID.TownSlimeOld, "TownSlimeOld" }, //长者史莱姆
            { NPCID.TownSlimeGreen, "TownSlimeGreen" }, //酷酷史莱姆
            { NPCID.TownSlimeRainbow, "TownSlimeRainbow" }, //天后史莱姆

            /*//城镇宠物
            { NPCID.TownCat, "TownCat" }, //城镇猫咪
            { NPCID.TownDog, "TownDog" }, //城镇狗狗
            { NPCID.TownBunny, "TownBunny" }, //城镇兔兔*/

        };

        private static readonly Dictionary<Type, (string BaseName, int Num)> ModPortraits = new()
        {
            { typeof(BossNPC), ("BossNPC", 2) }, //试炼官
            { typeof(PotionNPC), ("PotionNPC", 2) }, //酿灵师
            { typeof(MaterialNPC), ("MaterialNPC", 2) }, //星海商人
            { typeof(BuildNPC), ("BuildNPC", 2) }, //机枢学者
            { typeof(NatureNPC), ("NatureNPC", 3) }, //采花匠
            { typeof(OreNPC), ("OreNPC", 3) }, //收藏家
        };

        //4.随机选择肖像
        //对话刚开始时调用一次，返回该NPC本次对话要显示的肖像路径。
        //每张图等概率。
        private string PickPortraitPath(NPC npc)
        {
            //本模组NPC
            if (npc.ModNPC != null)
            {
                if (ModPortraits.TryGetValue(npc.ModNPC.GetType(), out var info))
                {
                    int num = Main.rand.Next(info.Num);
                    return $"LaziestNPC/Assets/Portraits/LNPC/{info.BaseName}_{num}";
                }
            }

            //原版NPC
            if (npc.ModNPC == null)
            {
                //1.城镇宠物：按townNpcVariationIndex映射品种
                if (npc.type == NPCID.TownCat)
                {
                    string[] catBreeds = { "Siamese", "Black", "OrangeTabby", "RussianBlue", "Silver", "White" };
                    return $"LaziestNPC/Assets/Portraits/Vanilla/TownCat_{catBreeds[npc.townNpcVariationIndex]}";
                }
                if (npc.type == NPCID.TownDog)
                {
                    string[] dogBreeds = { "Labrador", "PitBull", "Beagle", "Corgi", "Dalmatian", "Husky" };
                    return $"LaziestNPC/Assets/Portraits/Vanilla/TownDog_{dogBreeds[npc.townNpcVariationIndex]}";
                }
                if (npc.type == NPCID.TownBunny)
                {
                    string[] bunnyBreeds = { "White", "Angora", "Dutch", "Flemish", "Lop", "Silver"};
                    return $"LaziestNPC/Assets/Portraits/Vanilla/TownBunny_{bunnyBreeds[npc.townNpcVariationIndex]}";
                }

                //2.动物学家(正常+兽人化 共四张不同的肖像图)
                if (npc.type == NPCID.BestiaryGirl)
                {
                    bool isWerefox = (Main.moonPhase == 0 || Main.bloodMoon) && !Main.dayTime;
                    string suffix = npc.IsShimmerVariant ? "_Shimmer" : "";

                    if (isWerefox)
                        return $"LaziestNPC/Assets/Portraits/Vanilla/BestiaryGirl_Werefox{suffix}";

                    return $"LaziestNPC/Assets/Portraits/Vanilla/BestiaryGirl{suffix}";
                }

                //3.城镇NPC + 城镇史莱姆
                if (VanillaPortraits.TryGetValue(npc.type, out string baseName))
                {
                    string suffix = npc.IsShimmerVariant ? "_Shimmer" : "";
                    return $"LaziestNPC/Assets/Portraits/Vanilla/{baseName}{suffix}";
                }
            }
            return null;
        }

    }
}
