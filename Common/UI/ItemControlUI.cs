using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace LaziestNPC.Common.UI
{
    internal class ItemControlUI : UIState
    {
        private DraggablePanel mainPanel;

        //背景主面板
        public override void OnInitialize()
        {
            mainPanel = new DraggablePanel();
            mainPanel.Width.Set(250f, 0f);
            mainPanel.Height.Set(270f, 0f);
            mainPanel.HAlign = 0.5f;
            mainPanel.VAlign = 0.5f;
            mainPanel.BackgroundColor = new Color(30, 30, 60, 230);
            mainPanel.BorderColor = new Color(80, 80, 160);
            mainPanel.SetPadding(10);
            Append(mainPanel);

            UIText title = new UIText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Title"), 0.5f, true);
            title.HAlign = 0.5f;
            title.Top.Set(8f, 0f);
            mainPanel.Append(title);

            float usableWidth = mainPanel.Width.Pixels - 20; //左右边距各减去10像素，所有按钮的宽度范围限制
            float spacing = 8f; //按钮之间的横向间距
            float vSpacing = 13f; //垂直间距
            float btnHeight = 30f; //所有按钮的统一高度
            float yPos = 50f; //按钮距离面板顶部的距离

            //第一行 早上/中午/夜晚
            float btnWidth1 = 70f;//三个按钮的统一宽度(长方形的长)
            float totalWidth = 3 * btnWidth1 + 2 * spacing;  //三个按钮 + 两个间距的总宽度
            float startX1 = (usableWidth - totalWidth) / 2f; //按钮左边距(自动居中)(确定了左边距和宽度，也就确定了按钮位置)

            //早上
            UITextPanel<string> dayBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Day"));
            dayBtn.Width.Set(btnWidth1, 0f); //固定按钮65像素宽 0f或1f是是否启用百分比
            dayBtn.Height.Set(btnHeight, 0f); //固定按钮30像素高
            dayBtn.Left.Set(startX1, 0f); //左边缘距离背景UI左边缘startX1像素
            dayBtn.Top.Set(yPos, 0f); //顶部距离背景UI顶部yPos像素
            dayBtn.WithFadedMouseOver(); //添加鼠标悬停时的高亮效果，tmod的
            dayBtn.OnLeftClick += (evt, listener) => { ServerDayTime(0); ModContent.GetInstance<ItemControlUISystem>().HideUI(); };
            mainPanel.Append(dayBtn);

            //中午
            UITextPanel<string> noonBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Noon"));
            noonBtn.Width.Set(btnWidth1, 0f);
            noonBtn.Height.Set(btnHeight, 0f);
            noonBtn.Left.Set(startX1 + btnWidth1 + spacing, 0f);
            noonBtn.Top.Set(yPos, 0f);
            noonBtn.WithFadedMouseOver();
            noonBtn.OnLeftClick += (evt, listener) => { ServerDayTime(1); ModContent.GetInstance<ItemControlUISystem>().HideUI(); };
            mainPanel.Append(noonBtn);

            //夜晚
            UITextPanel<string> nightBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Night"));
            nightBtn.Width.Set(btnWidth1, 0f);
            nightBtn.Height.Set(btnHeight, 0f);
            nightBtn.Left.Set(startX1 + 2 * (btnWidth1 + spacing), 0f);
            nightBtn.Top.Set(yPos, 0f);
            nightBtn.WithFadedMouseOver();
            nightBtn.OnLeftClick += (evt, listener) => { ServerDayTime(2); ModContent.GetInstance<ItemControlUISystem>().HideUI(); };
            mainPanel.Append(nightBtn);

            //第二行 下雨/停雨
            yPos += btnHeight + vSpacing;
            float btnWidth2 = 110f; //按钮统一宽度(长方形的长)
            float spacing2 = 8f; //间距
            float totalWidth2 = 2 * btnWidth2 + spacing2; //两个按钮的总宽度
            float startX2 = (usableWidth - totalWidth2) / 2f; //第二行第一个按钮的起始X坐标(左边距)

            UITextPanel<string> rainOnBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Rain"));
            rainOnBtn.Width.Set(btnWidth2, 0f);
            rainOnBtn.Height.Set(btnHeight, 0f);
            rainOnBtn.Left.Set(startX2, 0f);
            rainOnBtn.Top.Set(yPos, 0f);
            rainOnBtn.WithFadedMouseOver();
            rainOnBtn.OnLeftClick += (evt, listener) => { ServerRain(true); ModContent.GetInstance<ItemControlUISystem>().HideUI(); };
            mainPanel.Append(rainOnBtn);

            UITextPanel<string> rainOffBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.RainNo"));
            rainOffBtn.Width.Set(btnWidth2, 0f);
            rainOffBtn.Height.Set(btnHeight, 0f);
            rainOffBtn.Left.Set(startX2 + btnWidth2 + spacing2, 0f);
            rainOffBtn.Top.Set(yPos, 0f);
            rainOffBtn.WithFadedMouseOver();
            rainOffBtn.OnLeftClick += (evt, listener) => { ServerRain(false); ModContent.GetInstance<ItemControlUISystem>().HideUI(); };
            mainPanel.Append(rainOffBtn);

            //第三行 终止事件
            yPos += btnHeight + vSpacing;
            float btnWidth3 = usableWidth; //直接默认按钮的最大宽度

            UITextPanel<string> cancelBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.CancelEvents"));
            cancelBtn.Width.Set(btnWidth3, 0f);
            cancelBtn.Height.Set(btnHeight, 0f);
            cancelBtn.Left.Set(0f, 0f);
            cancelBtn.Top.Set(yPos, 0f);
            cancelBtn.WithFadedMouseOver();
            cancelBtn.OnLeftClick += (evt, listener) => { ServerCancelEvents(); ModContent.GetInstance<ItemControlUISystem>().HideUI(); };
            mainPanel.Append(cancelBtn);

            //关闭按钮
            UITextPanel<string> closeBtn = new UITextPanel<string>(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.Close"), 0.8f);
            closeBtn.Width.Set(85f, 0f);
            closeBtn.Height.Set(35f, 0f);
            closeBtn.HAlign = 1f;
            closeBtn.VAlign = 1f;
            //closeBtn.Left.Set(0f, 0f);//向左偏移
            closeBtn.Top.Set(-10f, 0f);
            closeBtn.WithFadedMouseOver();
            //closeBtn.BackgroundColor = new Color(80, 30, 30);
            closeBtn.OnLeftClick += (evt, listener) => ModContent.GetInstance<ItemControlUISystem>().HideUI();
            mainPanel.Append(closeBtn);

            Recalculate();
        }

        public static void ToggleRain(Player player, bool turnOn)
        {
            //沙漠群系里的沙尘暴
            if (player.ZoneDesert)
            {
                if (turnOn)
                {
                    if (!Sandstorm.Happening)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.SandstormOn"), new Color(255, 200, 61));
                        Sandstorm.Happening = true;
                        Sandstorm.TimeLeft = 36000;
                        Sandstorm.IntendedSeverity = 0.4f + Main.rand.NextFloat() * 0.3f;
                    }
                }
                else
                {
                    if (Sandstorm.Happening)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.SandstormOff"), new Color(255, 200, 61));
                        Sandstorm.Happening = false;
                        Sandstorm.TimeLeft = 0;
                        Sandstorm.IntendedSeverity = 0f;
                    }
                }
            }
            //其他群系的普通的下雨
            else
            {
                if (turnOn)
                {
                    if (!Main.raining)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.RainOn"), new Color(133, 215, 255));
                        Main.rainTime = 86400 / 24 * 12;
                        Main.raining = true;
                        Main.maxRaining = Main.cloudAlpha = 0.9f;
                    }
                }
                else
                {
                    if (Main.raining)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.RainOff"), new Color(133, 215, 255));
                        Main.StopRain();
                    }
                }
            }
        }

        public static void ToggleDayTime(Player player, int timeType)
        {
            switch (timeType)
            {
                //清晨
                case 0: 
                    if (!Main.dayTime || Main.time != 0)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.DayOn"), new Color(255, 161, 83));
                        Main.dayTime = true;
                        Main.time = 0;
                    }
                    break;
                //中午
                case 1: 
                    if (!Main.dayTime || Main.time != 27000)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.NoonOn"), new Color(255, 215, 41));
                        Main.dayTime = true;
                        Main.time = 27000;
                    }
                    break;
                //夜晚
                case 2:
                    if (Main.dayTime || Main.time != 0)
                    {
                        Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.NightOn"), new Color(186, 172, 255));
                        Main.dayTime = false;
                        Main.time = 0;
                    }
                    break;
            }
        }

        public static void CancelEvents()
        {
            bool anyCanceled = false;

            if (BirthdayParty.PartyIsUp)
            {
                BirthdayParty.CheckNight();
                anyCanceled = true;
            }
            //灯笼夜(纯正面的事件，玩家不喜欢并主动取消的可能性极低，暂时待定)
            /*if (LanternNight.NextNightIsLanternNight || LanternNight.GenuineLanterns || LanternNight.ManualLanterns)
            {
                LanternNight.NextNightIsLanternNight = false;
                LanternNight.GenuineLanterns = false;
                LanternNight.ManualLanterns = false;
                anyCanceled = true;
            }*/
            if (Main.IsItRaining || Main.IsItStorming)
            {
                Main.StopRain();
                Main.cloudAlpha = 0f;
                anyCanceled = true;
            }
            if (Sandstorm.Happening)
            {
                Sandstorm.Happening = false;
                Sandstorm.TimeLeft = 0;
                Sandstorm.IntendedSeverity = 0f;
                anyCanceled = true;
            }
            if (Main.WindyEnoughForKiteDrops)
            {
                Main.windSpeedTarget = 0;
                Main.windSpeedCurrent = 0;
                anyCanceled = true;
            }
            if (Main.bloodMoon)
            {
                Main.bloodMoon = false;
                anyCanceled = true;
            }
            //invasionType是原版四个入侵事件
            if (Main.invasionType != 0)
            {
                Main.invasionType = 0;
                anyCanceled = true;
            }
            if (Main.slimeRain)
            {
                Main.StopSlimeRain();
                Main.slimeWarningDelay = 1;
                Main.slimeWarningTime = 1;
                anyCanceled = true;
            }
            if (DD2Event.Ongoing)
            {
                DD2Event.StopInvasion();
                anyCanceled = true;
            }
            if (Main.eclipse)
            {
                Main.eclipse = false;
                anyCanceled = true;
            }
            if (Main.pumpkinMoon)
            {
                Main.pumpkinMoon = false;
                anyCanceled = true;
            }
            if (Main.snowMoon)
            {
                Main.snowMoon = false;
                anyCanceled = true;
            }
            if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
            {
                NPC.LunarApocalypseIsUp = false;
                NPC.ShieldStrengthTowerNebula = 0;
                NPC.ShieldStrengthTowerSolar = 0;
                NPC.ShieldStrengthTowerStardust = 0;
                NPC.ShieldStrengthTowerVortex = 0;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active &&
                        (Main.npc[i].type == NPCID.LunarTowerNebula || Main.npc[i].type == NPCID.LunarTowerSolar ||
                         Main.npc[i].type == NPCID.LunarTowerStardust || Main.npc[i].type == NPCID.LunarTowerVortex))
                    {
                        Main.npc[i].StrikeInstantKill();
                        Main.npc[i].active = false;
                    }
                }
                anyCanceled = true;
            }

            if (anyCanceled)
                Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.CancelEventsOn"), new Color(8, 252, 163));
            else
                Main.NewText(Language.GetTextValue("Mods.LaziestNPC.World.Items.TheReturner.EventsNone"));
        }

        //点击按钮后，检测，然后单机会直接执行ToggleRain，联机则把指令打包发给服务器。后面同理
        internal static void ServerRain(bool turnOn)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                ToggleRain(Main.LocalPlayer, turnOn);
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = ModContent.GetInstance<LaziestNPC>().GetPacket();
                packet.Write((byte)20);
                packet.Write((byte)1);
                packet.Write(turnOn);
                packet.Send();
            }
        }

        internal static void ServerDayTime(int timeType)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                ToggleDayTime(Main.LocalPlayer, timeType);
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = ModContent.GetInstance<LaziestNPC>().GetPacket();
                packet.Write((byte)20);
                packet.Write((byte)2);
                packet.Write(timeType);
                packet.Send();
            }
        }

        internal static void ServerCancelEvents()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                CancelEvents();
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = ModContent.GetInstance<LaziestNPC>().GetPacket();
                packet.Write((byte)20);
                packet.Write((byte)3);
                packet.Send();
            }
        }

        private bool oldMouseLeft = false; //记录上一帧鼠标左键是否按下

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.keyState.IsKeyDown(Keys.Escape) && !Main.oldKeyState.IsKeyDown(Keys.Escape))
                ModContent.GetInstance<ItemControlUISystem>().HideUI();

            //鼠标左键点击检测(当前帧按下，前一帧未按下)
            if (Main.mouseLeft && !oldMouseLeft)
            {
                Rectangle panelRect = mainPanel.GetDimensions().ToRectangle();
                
                if (!panelRect.Contains(Main.mouseX, Main.mouseY))
                {
                    ModContent.GetInstance<ItemControlUISystem>().HideUI();
                }
            }

            //更新状态，供下一帧使用
            oldMouseLeft = Main.mouseLeft;
        }
    }
}
