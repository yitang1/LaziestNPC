using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using LaziestNPC.Content.NPCs.TownNPCs;
using Terraria.GameContent.UI.Elements;
using CalamityMod.Items.Accessories.Wings;

namespace LaziestNPC.Common.UI
{
    /// <summary>
    /// 一个商店类别：商店内部名、本地化名称键、图标资源路径。<br/>
    /// 作用：注册信息、数据定义。
    /// </summary>
    public class ShopCategory
    {
        public readonly string ShopName;
        public readonly string NameKey;
        public readonly string IconPath;
        public readonly float TextOffsetY; //该类别文字的额外垂直偏移，正值向下

        public ShopCategory(string shopName, string nameKey, string iconPath, float textOffsetY = 0f)
        {
            ShopName = shopName;
            NameKey = nameKey;
            IconPath = iconPath;
            TextOffsetY = textOffsetY;
        }
    }

    /// <summary>
    /// NPC对话框右侧的商店类别竖条。跟随原版对话框定位，点击某一类后，<br/>
    /// 把索引写回NPC，由NPC自己的按钮1和OnChatButtonClicked读取。<br/>
    /// 作用：UI的生命周期管理类。即UI的创建、显示和隐藏。
    /// </summary>
    [Autoload(Side = ModSide.Client)]
    public class ShopExtraUISystem : ModSystem
    {
        private UserInterface userInterface;
        private ShopExtraUIState uiState;

        //在模组加载完后，开始创建UI，以后反复复用。
        public override void PostSetupContent()
        {
            if (Main.dedServ)
                return;

            userInterface = new UserInterface();
            uiState = new ShopExtraUIState();
            uiState.Activate();
        }

        //UI的显示条件，什么时候显示。
        public override void UpdateUI(GameTime gameTime)
        {
            if (userInterface == null)
                return;

            bool shouldShow = ShouldShowUI();

            if (shouldShow && userInterface.CurrentState == null)
                userInterface.SetState(uiState);
            else if (!shouldShow && userInterface.CurrentState != null)
                userInterface.SetState(null);

            if (userInterface.CurrentState != null)
                userInterface.Update(gameTime);
        }

        //一些条件，例如必须是城镇NPC。
        private bool ShouldShowUI()
        {
            if (string.IsNullOrEmpty(Main.npcChatText))
                return false;

            if (Main.LocalPlayer.talkNPC < 0)
                return false;

            NPC npc = Main.npc[Main.LocalPlayer.talkNPC];
            if (npc.ModNPC is not TownLNPC town)
                return false;

            IReadOnlyList<ShopCategory> list = town.ShopCategories;
            return list != null && list.Count > 0;
        }

        //把UI绘制到NPC对话框之上。
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name == "Vanilla: NPC / Sign Dialog");
            if (index == -1)
                return;

            layers.Insert(index + 1, new LegacyGameInterfaceLayer(
                "LaziestNPC: Shop Extra UI",
                () =>
                {
                    if (userInterface?.CurrentState != null)
                        userInterface.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI));
        }
    }

    /// <summary>
    /// UI面板的具体数据。<br/>
    /// 作用：负责面板的尺寸、位置、行列表的创建、刷新和修改。
    /// </summary>
    public class ShopExtraUIState : UIState
    {
        //可调参数：竖条UI布局
        private static float PanelWidth = 160f; //竖条宽度
        private static float RowHeight = 48f; //每一类项目的高度
        private static float PanelPadding = 10f; //竖条内部上下左右留白

        //可调参数：对话框定位
        //原版对话框外框公式：左边界 = (Main.screenWidth - 800) / 2，宽度 = 800。
        //顶部 = 130，高度 = 200。与 PortraitUISystem.cs 中的 dialogTop = 130 完全一致。
        private static float DialogWidth = 800f; //原版对话框外框宽度
        private static float DialogTop = 130f; //原版对话框顶部
        private static float DialogHeight = 200f; //原版对话框高度

        private static float UiXOffset = -150f; //竖条水平偏移，负值向左，正值向右
        private static float UiYOffset = -75f; //UI顶部到对话框底部的间距，负数向上，空隙变大；正数向下，空隙变小。

        private static int BaseLines = 3; //基准对话（3行）对应的非空行数
        private static float LineHeight = 30f; //每多一行，对话框底部下移的像素

        private readonly List<ShopCategoryRow> rows = new();
        private UIPanel panel;
        private TownLNPC currentTown;
        private int currentCategoryCount;

        //创建原版自带的UI，边框背景层等都不需要自己再画。
        public override void OnInitialize()
        {
            panel = new UIPanel();
            panel.Width.Set(PanelWidth, 0f);
            panel.SetPadding(0f);

            panel.BackgroundColor = new Color(55, 71, 150) * 0.7f; //背景色 (第四个参数0完全透明，255完全不透明)

            Append(panel);
        }

        //每帧执行，做四件事。
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //第一：确认当前对话的NPC仍然是城镇NPC，并且类别列表非空。
            Player player = Main.LocalPlayer;
            if (player.talkNPC < 0)
                return;

            NPC npc = Main.npc[player.talkNPC];
            if (npc.ModNPC is not TownLNPC town)
                return;

            //第二：检查当前NPC或类别数量是否发生改变，有改变就清空并重新生成。
            IReadOnlyList<ShopCategory> categories = town.ShopCategories;
            if (categories == null || categories.Count == 0)
                return;

            if (currentTown != town || currentCategoryCount != categories.Count)
            {
                currentTown = town;
                currentCategoryCount = categories.Count;
                RebuildRows(town, categories);
            }

            //第三：计算面板位置。借助原版对话框的位置，使其依附于原版对话框，并固定在右下。
            float totalHeight = categories.Count * RowHeight + PanelPadding * 2f;
            panel.Height.Set(totalHeight, 0f);

            //计算原版对话框外框的位置
            float dialogLeft = (Main.screenWidth - DialogWidth) / 2f;
            float dialogRight = dialogLeft + DialogWidth;

            //根据当前对话文本的实际渲染高度动态计算对话框底部
            int lines = GetChatLineCount();
            float dynamicDialogHeight = DialogHeight + (lines - BaseLines) * LineHeight;
            float dialogBottom = DialogTop + dynamicDialogHeight;

            //竖条位置 = 对话框右边缘/下边缘 + 偏移量
            float uiX = dialogRight - PanelWidth + UiXOffset;
            float uiY = dialogBottom + UiYOffset;
            if (uiY < DialogTop)
                uiY = DialogTop;

            panel.Left.Set(uiX, 0f);
            panel.Top.Set(uiY, 0f);
            panel.Recalculate();

            //第四：遍历所有行，标记选中状态，供NPC商店使用。
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i].SetSelected(i == town.CurrentShopCategoryIndex);
            }
        }

        //借助对话框内的文本行数，调整对话框和UI的空隙距离。
        private static int GetChatLineCount()
        {
            string text = Main.npcChatText;
            if (string.IsNullOrEmpty(text))
                return 1;

            //原版对话框文本区域的折行宽度，480是实测校准值，1到7行均与对话框实际高度一致
            string[] wrappedLines = Utils.WordwrapString(text, FontAssets.MouseText.Value, 480, 10, out _);

            int actualLines = 0;
            foreach (string line in wrappedLines)
            {
                if (!string.IsNullOrEmpty(line))
                    actualLines++;
            }

            return Math.Max(1, actualLines);
        }

        //当NPC更换，或类别数量发生变化，就按新的类别重新生成一遍行对象。没变化就不会重建行。
        private void RebuildRows(TownLNPC town, IReadOnlyList<ShopCategory> categories)
        {
            panel.RemoveAllChildren();
            rows.Clear();

            for (int i = 0; i < categories.Count; i++)
            {
                ShopCategoryRow row = new ShopCategoryRow(categories[i], i, town);
                row.Left.Set(PanelPadding, 0f);
                row.Top.Set(PanelPadding + i * RowHeight, 0f);
                row.Width.Set(PanelWidth - PanelPadding * 2f, 0f);
                row.Height.Set(RowHeight, 0f);
                panel.Append(row);
                rows.Add(row);
            }
        }
    }

    /// <summary>
    /// 作用：负责每一行的视觉表现(贴图、文字)和点击响应(光标触碰、字体颜色)。
    /// </summary>
    public class ShopCategoryRow : UIElement
    {
        //可调参数：图标与文字布局
        private static float IconSize = 34f; //图标尺寸
        private static float IconLeftPadding = 8f; //图标距离行左边缘的间距
        private static float IconTextGap = 16f; //图标和文字之间的间距
        private static float TextVerticalOffset = 4f; //文字垂直补偿，正值向下，负值向上

        //可调参数：悬停效果
        private static float HoverScale = 1.08f; //悬停时放大倍数
        private static float HoverLift = 2f; //悬停时上浮像素

        //可调参数：描边
        private static int IconOutlineOffset = 1; //图标描边厚度
        private static float TextOutlineOffset = 0.6f; //文字描边厚度
        private static Color OutlineColor = new Color(255, 200, 60) * 0.5f; //图标描边颜色
        private static Color HoverTextOutlineColor = new Color(87, 27, 0); //鼠标悬停时文字的描边颜色

        //可调参数：文字颜色
        private static Color HoverTextColor = new Color(255, 243, 115); //鼠标箭头触碰后的文字颜色
        private static Color NormalTextColor = new Color(251, 251, 224); //未触碰前的文字颜色

        private readonly ShopCategory category;
        private readonly int index;
        private readonly TownLNPC town;
        private readonly string displayName;
        private readonly Texture2D icon;
        private bool selected;

        public ShopCategoryRow(ShopCategory category, int index, TownLNPC town)
        {
            this.category = category;
            this.index = index;
            this.town = town;
            displayName = Language.GetTextValue(category.NameKey);

            try
            {
                icon = ModContent.Request<Texture2D>(
                    category.IconPath,
                    AssetRequestMode.ImmediateLoad).Value;
            }
            catch
            {
                icon = null;
            }
        }

        public void SetSelected(bool value)
        {
            selected = value;
        }

        //点击事件
        public override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);
            town.CurrentShopCategoryIndex = index;
            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        //绘制视觉效果
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            CalculatedStyle dim = GetDimensions();
            Rectangle rect = dim.ToRectangle();

            bool hover = IsMouseHovering;
            if (hover)
                Main.LocalPlayer.mouseInterface = true;

            float scale = hover ? HoverScale : 1f;
            float lift = hover ? HoverLift : 0f;

            // 图标位置：以原图标中心为基准缩放，再整体上浮
            Rectangle baseIconRect = new Rectangle(
                (int)(rect.X + IconLeftPadding),
                rect.Y + (rect.Height - (int)IconSize) / 2,
                (int)IconSize,
                (int)IconSize);

            Vector2 iconCenter = baseIconRect.Center.ToVector2();
            float iconSize = IconSize * scale;
            Rectangle iconRect = new Rectangle(
                (int)(iconCenter.X - iconSize / 2f),
                (int)(iconCenter.Y - iconSize / 2f - lift),
                (int)iconSize,
                (int)iconSize);

            if (icon != null)
            {
                if (hover)
                {
                    //图标描边
                    //4方向偏移画黄色副本，形成轮廓
                    spriteBatch.Draw(icon, new Rectangle(iconRect.X - IconOutlineOffset, iconRect.Y, iconRect.Width, iconRect.Height), OutlineColor);
                    spriteBatch.Draw(icon, new Rectangle(iconRect.X + IconOutlineOffset, iconRect.Y, iconRect.Width, iconRect.Height), OutlineColor);
                    spriteBatch.Draw(icon, new Rectangle(iconRect.X, iconRect.Y - IconOutlineOffset, iconRect.Width, iconRect.Height), OutlineColor);
                    spriteBatch.Draw(icon, new Rectangle(iconRect.X, iconRect.Y + IconOutlineOffset, iconRect.Width, iconRect.Height), OutlineColor);
                }

                spriteBatch.Draw(icon, iconRect, Color.White);
            }
            else
            {
                Color fallback = (index % 4) switch
                {
                    0 => new Color(120, 180, 120),
                    1 => new Color(100, 150, 220),
                    2 => new Color(220, 180, 100),
                    _ => new Color(200, 120, 160)
                };

                spriteBatch.Draw(TextureAssets.MagicPixel.Value, iconRect, fallback);
            }

            //文字
            //文字位置
            Vector2 baseSize = ChatManager.GetStringSize(
                FontAssets.MouseText.Value,
                displayName,
                Vector2.One);

            float textX = rect.X + IconSize + IconTextGap;
            float textY = rect.Y + (rect.Height - baseSize.Y * scale) / 2f - lift + TextVerticalOffset + category.TextOffsetY;
            Vector2 textPos = new Vector2(textX, textY);

            if (hover)
            {
                //悬停时：用橙色8方向副本作为描边，主文字不带黑边
                float textOffset = TextOutlineOffset * scale;
                Vector2 textScaleVec = new Vector2(scale, scale);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(-textOffset, 0f), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(textOffset, 0f), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(0f, -textOffset), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(0f, textOffset), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(-textOffset, -textOffset), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(textOffset, -textOffset), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(-textOffset, textOffset), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos + new Vector2(textOffset, textOffset), HoverTextOutlineColor, 0f, Vector2.Zero, textScaleVec);

                //主文字，不用 DrawBorderString，所以没有黑色描边
                ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, displayName,
                    textPos, HoverTextColor, 0f, Vector2.Zero, textScaleVec);
            }
            else
            {
                //未悬停：保持原版黑色描边
                Utils.DrawBorderString(spriteBatch, displayName, textPos, NormalTextColor, scale);
            }
        }
    }
}