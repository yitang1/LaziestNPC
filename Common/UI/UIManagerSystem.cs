using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace LaziestNPC.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    internal class UIManagerSystem : ModSystem
    {
        private UserInterface _userInterface;
        internal ShopSelectorUI _ui;

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
            {
                _userInterface = new UserInterface();
                _ui = new ShopSelectorUI();
                _ui.Activate();
            }
        }

        public void ShowUI(string npcClass)
        {
            if (_userInterface != null && _ui != null)
            {
                _ui.SetCurrentNPC(npcClass);
                _userInterface.SetState(_ui);
            }
        }

        public void HideUI()
        {
            _userInterface?.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (_userInterface?.CurrentState != null)
                _userInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (index != -1 && _userInterface?.CurrentState != null)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer(
                    "LaziestNPC: ShopSelectorUI",
                    delegate
                    {
                        _userInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
