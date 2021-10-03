using System;
using System.Collections.Generic;
using Appalachia.Core.Crafting.Base;
using Appalachia.Core.Crafting.Utility;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Appalachia.Core.Crafting
{
    /// <summary>
    /// A flexible category of raw materials.  For example, wood.
    /// </summary>
    [Serializable]
    public class CraftingMaterialCategory : CraftingIconComponent<CraftingMaterialCategory>
    {
        public List<CraftingMaterial> materials;
        
        [ButtonGroup]
        public void NewMaterial()
        {
            if (materials == null)
            {
                materials = new List<CraftingMaterial>();
            }
            
            materials.Add(CraftingMaterial.CreateNew());
        }
        
        [MenuItem(CraftableConstants.MATERIAL_CATEGORY_MENU, false, CraftableConstants.MATERIAL_CATEGORY_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}