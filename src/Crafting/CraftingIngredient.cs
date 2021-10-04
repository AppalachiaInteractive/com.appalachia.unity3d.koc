using System;
using Appalachia.Editing.Attributes;
using Sirenix.OdinInspector;

namespace Appalachia.KOC.Crafting
{
    [Serializable, InlineEditor]
    public class CraftingIngredient
    {
        [SmartLabel, EnumToggleButtons]
        public CraftingIngredientType ingredientType;

        private bool _showMaterial => ingredientType == CraftingIngredientType.MaterialCategory;

        [SmartLabel, ShowIf(nameof(_showMaterial))]
        public CraftingMaterialCategory material;

        [SmartLabel, ShowIf(nameof(_showMaterial))]
        public float materialAmount = 1.0f;
        
        [SmartLabel, HideIf(nameof(_showMaterial))]
        public CraftedItem item;
        
        [SmartLabel, HideIf(nameof(_showMaterial))]
        public int itemCount = 1;
        
        [ButtonGroup]
        public void NewMaterialCategory()
        {
            material = CraftingMaterialCategory.CreateNew();
        }
        
        [ButtonGroup]
        public void NewCraftedItem()
        {
            item = CraftedItem.CreateNew();
        }
    }
}