using System;
using Appalachia.CI.Constants;
using Appalachia.KOC.Crafting.Base;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingSkill : CraftingIconComponent<CraftingSkill>
    {
        [UnityEditor.MenuItem(PKG.Menu.Appalachia.Components.Skill.Base, priority =  
            PKG.Menu.Appalachia.Components.Skill.Priority)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}
