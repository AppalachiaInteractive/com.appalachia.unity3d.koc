using System;
using Appalachia.KOC.Crafting.Base;
using Appalachia.KOC.Crafting.Utility;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingSkill : CraftingIconComponent<CraftingSkill>
    {
        
        [MenuItem(CraftableConstants.SKILL_MENU, false, CraftableConstants.SKILL_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}