using System;
using Appalachia.KOC.Crafting.Base;
using Appalachia.KOC.Crafting.Utility;
using UnityEditor;
using UnityEngine;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftedItem : CraftingIconComponent<CraftedItem>
    {
        public GameObject product;
        
        [MenuItem(CraftableConstants.ITEM_MENU, false, CraftableConstants.ITEM_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}