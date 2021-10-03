using System;
using Appalachia.Core.Crafting.Base;
using Appalachia.Core.Crafting.Utility;
using UnityEditor;

namespace Appalachia.Core.Crafting
{
    [Serializable]
    public class CraftingTool : CraftingIconComponent<CraftingTool>
    {
        
        [MenuItem(CraftableConstants.TOOL_MENU, false, CraftableConstants.TOOL_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}