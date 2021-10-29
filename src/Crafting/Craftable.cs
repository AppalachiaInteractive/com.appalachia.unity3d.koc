using System;
using System.Collections.Generic;
using Appalachia.CI.Constants;
using Appalachia.KOC.Crafting.Base;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class Craftable : CraftingIconComponent<Craftable>
    {
        public List<CraftedItem> craftedItems;
        public List<CraftingRecipe> recipes;

        [ButtonGroup]
        public void NewCraftedItem()
        {
            if (craftedItems == null)
            {
                craftedItems = new List<CraftedItem>();
            }

            craftedItems.Add(CraftedItem.CreateNew());
        }

        [ButtonGroup]
        public void NewCraftingRecipe()
        {
            if (recipes == null)
            {
                recipes = new List<CraftingRecipe>();
            }

            recipes.Add(new CraftingRecipe());
        }
        
        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Components.Craftable.Base,
            false,
            PKG.Menu.Appalachia.Components.Craftable.Priority
        )]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}
