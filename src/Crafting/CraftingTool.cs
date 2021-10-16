using System;
using Appalachia.CI.Integration.Assets;
using Appalachia.KOC.Crafting.Base;
using Appalachia.KOC.Crafting.Utility;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingTool : CraftingIconComponent<CraftingTool>
    {
        [MenuItem(CraftableConstants.TOOL_MENU, false, CraftableConstants.TOOL_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();            
            AssetDatabaseManager.SetSelection(created);
        }
    }
}
