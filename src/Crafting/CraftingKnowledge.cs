using System;
using Appalachia.Core.Crafting.Base;
using Appalachia.Core.Crafting.Utility;
using UnityEditor;

namespace Appalachia.Core.Crafting
{
    [Serializable]
    public class CraftingKnowledge : CraftingIconComponent<CraftingKnowledge>
    {
        
        [MenuItem(CraftableConstants.KNOWLEDGE_MENU, false, CraftableConstants.KNOWLEDGE_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}