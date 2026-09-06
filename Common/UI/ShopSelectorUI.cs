using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ModLoader.UI;

namespace LaziestNPC.Common.UI
{
    //可拖动的UI面板
    internal class DraggablePanel : UIPanel
    {
        private Vector2 _offset;      //记录鼠标按下时相对于面板左上角的偏移
        private bool _dragging;       //是否正在拖动

        //鼠标左键按下时：记录拖动起始偏移并开启拖动状态
        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
            if (evt.Target == this)
            {
                _dragging = true;
                _offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            }
        }

        //鼠标左键松开时：结束拖动状态并重新计算布局
        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);
            if (evt.Target == this)
            {
                _dragging = false;
                Recalculate();
            }
        }

        //每帧更新：处理拖动逻辑、防止面板被拖出屏幕、让鼠标点击穿透到玩家操作
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
                Main.LocalPlayer.mouseInterface = true;

            if (_dragging)
            {
                Left.Set(Main.mouseX - _offset.X, 0f);
                Top.Set(Main.mouseY - _offset.Y, 0f);
                Recalculate();
            }

            //防止面板被拖出屏幕边界
            var parentSpace = Parent.GetDimensions().ToRectangle();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                Recalculate();
            }
        }
    }

    internal class ShopSelectorUI : UIState
    {
        private DraggablePanel _mainPanel;   //主面板
        private string _currentNpcClass;     //当前打开UI的NPC类名

        //初始化：创建一个空面板，具体内容由 SetCurrentNPC 动态填充
        public override void OnInitialize()
        {
            //初始化时只创建空面板，等 SetCurrentNPC 时填充内容
            _mainPanel = new DraggablePanel();
            _mainPanel.SetPadding(10);
            _mainPanel.Width.Set(200f, 0f);
            _mainPanel.Height.Set(250f, 0f);
            _mainPanel.HAlign = 0.5f;
            _mainPanel.VAlign = 0.5f;
            _mainPanel.Left.Set(50f, 0f); //向右偏移50像素
            _mainPanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
            Append(_mainPanel);
        }

        // 由外部调用，设置当前NPC并重建UI内容
        public void SetCurrentNPC(string npcClass)
        {
            _currentNpcClass = npcClass;
            BuildUI();
        }

        //构建UI内容：清空面板，然后根据当前NPC的分类列表动态生成按钮
        private void BuildUI()
        {
            //清空旧内容（保留主面板自身）
            _mainPanel.RemoveAllChildren();

            //标题
            string titleKey = $"Mods.LaziestNPC.NPCs.{_currentNpcClass}.ShopName.Title";
            UIText title = new UIText(Language.GetTextValue(titleKey), 0.95f);
            title.HAlign = 0.5f;
            title.Top.Set(0f, 0f);
            _mainPanel.Append(title);

            //获取分类列表
            var categories = ShopManager.GetCategories(_currentNpcClass);
            if (categories == null || categories.Count == 0)
            {
                UIText error = new UIText("No categories", 0.6f);
                error.HAlign = 0.5f;
                error.Top.Set(50f, 0f);
                _mainPanel.Append(error);
                return;
            }

            float yPos = 50f;
            //遍历当前NPC注册的所有分类条目(每个条目对应一个商店按钮)
            foreach (var cat in categories)
            {
                string displayText = Language.GetTextValue(cat.DisplayKey);
                UITextPanel<string> btn = new UITextPanel<string>(displayText, 0.85f);
                btn.Width.Set(250f, 0f);
                btn.Height.Set(30f, 0f);
                btn.HAlign = 0.5f;
                btn.Top.Set(yPos, 0f);
                //鼠标悬停效果，tmod官方ExampleMod推荐的
                btn.WithFadedMouseOver();
                //按钮点击事件：保存选中的商店到管理器，并关闭 UI 窗口
                btn.OnLeftClick += (evt, listener) =>
                {
                    ShopManager.SetTargetShop(_currentNpcClass, cat.ShopName);
                    ModContent.GetInstance<UIManagerSystem>().HideUI();
                };
                _mainPanel.Append(btn);
                yPos += 45f;
            }

            //关闭按钮
            string closeText = Language.GetTextValue("Mods.LaziestNPC.NPCs.BossNPC.ShopName.Close");
            UITextPanel<string> closeBtn = new UITextPanel<string>(closeText, 0.6f);
            closeBtn.Width.Set(80f, 0f);
            closeBtn.Height.Set(30f, 0f);
            closeBtn.HAlign = 1f;
            closeBtn.VAlign = 1f;
            closeBtn.Left.Set(-10f, 0f);
            closeBtn.BackgroundColor = new Color(80, 30, 30);
            closeBtn.WithFadedMouseOver();
            closeBtn.OnLeftClick += (evt, listener) => ModContent.GetInstance<UIManagerSystem>().HideUI();
            _mainPanel.Append(closeBtn);

            Recalculate();
        }

        //每帧更新：检测ESC键关闭UI，检测是否还在与NPC对话，若已退出对话则自动关闭UI
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.keyState.IsKeyDown(Keys.Escape) && !Main.oldKeyState.IsKeyDown(Keys.Escape))
                ModContent.GetInstance<UIManagerSystem>().HideUI();

            if (Main.LocalPlayer.talkNPC == -1)
            {
                ModContent.GetInstance<UIManagerSystem>().HideUI();
            }
        }
    }
}
