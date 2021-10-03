using System;
using System.Collections.Generic;
using Appalachia.Core.Crafting.Base;
using Appalachia.Core.Crafting.Utility;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Appalachia.Core.Crafting
{
    [Serializable]
    public class Craftable : CraftingIconComponent<Craftable> 
    {
        public List<CraftingRecipe> recipes;
        
        public List<CraftedItem> craftedItems;

        [ButtonGroup]
        public void NewCraftingRecipe()
        {
            if (recipes == null)
            {
                recipes = new List<CraftingRecipe>();
            }
            
            recipes.Add(new CraftingRecipe());
        }
        
        [ButtonGroup]
        public void NewCraftedItem()
        {
            if (craftedItems == null)
            {
                craftedItems = new List<CraftedItem>();
            }

            craftedItems.Add(CraftedItem.CreateNew());
        }

        [MenuItem(CraftableConstants.CRAFTABLE_MENU, false, CraftableConstants.CRAFTABLE_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}