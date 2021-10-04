using System;
using Appalachia.KOC.Crafting.Base;
using Appalachia.KOC.Crafting.Utility;
using UnityEditor;

namespace Appalachia.KOC.Crafting
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
