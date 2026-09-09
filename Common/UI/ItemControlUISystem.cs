using System.Collections.Generic;
using Humanizer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace LaziestNPC.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    //此类是负责管理UI的显示和绘制
    internal class ItemControlUISystem : ModSystem
    {
        private UserInterface userInterface;
        internal ItemControlUI ui;

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
            {
                userInterface = new UserInterface();
                ui = new ItemControlUI();
                ui.Activate();
            }
        }

        public void ShowUI()
        {
            if (userInterface != null && ui != null)
            {
                userInterface.SetState(ui);
            }
        }

        public void HideUI()
        {
            userInterface?.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (userInterface?.CurrentState != null)
                userInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (index != -1 && userInterface?.CurrentState != null)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer(
                    "LaziestNPC: ItemControlUI",
                    delegate {
                        userInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }

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
}
