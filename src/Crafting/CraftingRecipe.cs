using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingRecipe
    {
        public CraftingRecipe()
        {
            elements = new List<CraftingRecipeElement>();
        }

        public List<CraftingRecipeElement> elements;

        [ButtonGroup]
        public void NewElement()
        {
            if (elements == null)
            {
                elements = new List<CraftingRecipeElement>();
            }

            elements.Add(new CraftingRecipeElement());
        }
    }
}
