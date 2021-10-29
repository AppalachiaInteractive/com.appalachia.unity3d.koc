using System;
using Appalachia.CI.Constants;
using Appalachia.KOC.Crafting.Base;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingKnowledge : CraftingIconComponent<CraftingKnowledge>
    {
        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Components.Knowledge.Base,
            false,
            PKG.Menu.Appalachia.Components.Knowledge.Priority
        )]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}
