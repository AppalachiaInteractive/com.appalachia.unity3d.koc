using System;
using Appalachia.Core.Crafting.Base;
using Appalachia.Core.Crafting.Utility;
using UnityEditor;

namespace Appalachia.Core.Crafting
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