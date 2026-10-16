using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using Terraria.UI.Chat;
using Terraria.Localization;
using LaziestNPC.Content.NPCs.TownNPCs;

namespace LaziestNPC.Common.UI
{
    public class PortraitUISystem : ModSystem
    {
        //1.跨帧保存
        //currentPortraitPath 存当前对话该显示哪张肖像图。
        //lastTalkNPC 记录上一帧正在对话的NPC索引，用来判断“对话是不是刚刚开始”。
        //这两个字段必须放在方法外面，否则每帧都会被重置，就起不到缓存作用了。
        private string currentPortraitPath = null;
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

            //(1)：判断对话是否开启
            //没有对话文本，或者没有和任何NPC对话，就清空缓存，不绘制。
            //清空缓存是为了下次对话时能重新随机一张肖像。
            if (Main.npcChatText == "" || player.talkNPC < 0)
            {
                currentPortraitPath = null;
                lastTalkNPC = -1;
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
                portraitAnimTimer = 0;
            }
            lastTalkNPC = player.talkNPC;

            //(3)：没有对应肖像就不绘制
            if (currentPortraitPath == null)
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
            //路径来自 currentPortraitPath，是对话开始时随机缓存的那一张。
            Texture2D portrait = ModContent.Request<Texture2D>(currentPortraitPath).Value;

            //按原图宽高比计算绘制高度。宽度固定为size，高度按比例换算。
            float ratio = (float)portrait.Height / portrait.Width;
            float drawWidth = size;
            float drawHeight = size * ratio;

            //水平方向和背景框对齐，垂直方向底部对齐背景框底部。
            //这样如果肖像比框高，超出部分会向上溢出，不会被裁掉。
            float drawX = portraitX;
            float drawY = portraitY + size - drawHeight;

            Main.spriteBatch.Draw(portrait,
                new Rectangle((int)drawX, (int)drawY, (int)drawWidth, (int)drawHeight),
                Color.White);

            //(9)：绘制NPC名字
            //名字显示在肖像框下方，水平居中。
            string npcName = Language.GetTextValue($"Mods.LaziestNPC.NPCs.{npc.ModNPC.Name}.PortraitName");
            Vector2 nameSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, npcName, Vector2.One);
            float nameX = portraitX + (size - nameSize.X) / 2f;
            float nameY = portraitY + size + 2f;
            Utils.DrawBorderString(Main.spriteBatch, npcName, new Vector2(nameX, nameY), Color.White);
        }

        //4.随机选择肖像
        //对话刚开始时调用一次，返回该NPC本次对话要显示的肖像路径。
        //每张图等概率。
        private string PickPortraitPath(NPC npc)
        {
            if (npc.ModNPC is BossNPC)
            {
                int num = Main.rand.Next(2);
                return "LaziestNPC/Assets/Portraits/BossNPC_" + num;
            }
            if (npc.ModNPC is NatureNPC)
            {
                int num = Main.rand.Next(3);
                return "LaziestNPC/Assets/Portraits/NatureNPC_" + num;
            }
            /*if (npc.ModNPC is NatureNPC)
            {
                int num = Main.rand.Next(3);
                return "LaziestNPC/Assets/Portraits/NatureNPC_" + num;
            }
            if (npc.ModNPC is NatureNPC)
            {
                int num = Main.rand.Next(3);
                return "LaziestNPC/Assets/Portraits/NatureNPC_" + num;
            }
            if (npc.ModNPC is NatureNPC)
            {
                int num = Main.rand.Next(3);
                return "LaziestNPC/Assets/Portraits/NatureNPC_" + num;
            }
            if (npc.ModNPC is NatureNPC)
            {
                int num = Main.rand.Next(3);
                return "LaziestNPC/Assets/Portraits/NatureNPC_" + num;
            }*/

            return null;
        }
    }
}
