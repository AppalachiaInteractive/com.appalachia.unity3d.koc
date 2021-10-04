using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingRecipeElement
    {
        public List<CraftingRecipeElementOption> options;

        public CraftingRecipeElement()
        {
            options = new List<CraftingRecipeElementOption>();
        }

        [ButtonGroup]
        public void NewOptions()
        {
            if (options == null)
            {
                options = new List<CraftingRecipeElementOption>();
            }

            options.Add(new CraftingRecipeElementOption());
        }
    }
}
