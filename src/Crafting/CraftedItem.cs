using System;
using Appalachia.CI.Constants;
using Appalachia.KOC.Crafting.Base;
using UnityEditor;
using UnityEngine;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftedItem : CraftingIconComponent<CraftedItem>
    {
        public GameObject product;
        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Components.Item.Base,
            false,
            PKG.Menu.Appalachia.Components.Item.Priority
        )]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}
