using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Appalachia.KOC.Crafting
{
    [Serializable]
    public class CraftingRecipeElement
    {
        public CraftingRecipeElement()
        {
            options = new List<CraftingRecipeElementOption>();
        }
        
        public List<CraftingRecipeElementOption> options;
        
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