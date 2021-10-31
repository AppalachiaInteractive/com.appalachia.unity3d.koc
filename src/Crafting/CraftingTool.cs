using System;
using Appalachia.CI.Constants;
using Appalachia.CI.Integration.Assets;
using Appalachia.KOC.Crafting.Base;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingTool : CraftingIconComponent<CraftingTool>
    {
        [UnityEditor.MenuItem(PKG.Menu.Appalachia.Components.Tool.Base, priority =  
            PKG.Menu.Appalachia.Components.Tool.Priority)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            AssetDatabaseManager.SetSelection(created);
        }
    }
}
