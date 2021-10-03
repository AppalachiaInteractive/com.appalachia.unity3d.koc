using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.Core.Crafting.Base;
using Appalachia.Core.Crafting.Utility;
using Appalachia.Core.Editing;
using Appalachia.Core.Editing.AssetDB;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Appalachia.Core.Crafting
{
    /// <summary>
    /// A raw material used in crafting.  For example, white pine wood.
    /// </summary>
    [Serializable]
    public class CraftingMaterial : CraftingIconComponent<CraftingMaterial> 
    {
        [NonSerialized] private bool _categoriesInitialized;
        [NonSerialized, ShowInInspector] private List<CraftingMaterialCategory> _categories;

        [OnInspectorGUI]
        private void SetupCategories()
        {
            if (_categoriesInitialized)
            {
                return;
            }

            _categoriesInitialized = true;
            
            RefreshCategories();
        }

        public void RefreshCategories()
        {
            if (_categories == null)
            {
                _categories = new List<CraftingMaterialCategory>();
            }

            var categories = AssetDatabaseHelper.FindAssets<CraftingMaterialCategory>();

            _categories = categories.Where(c => c.materials.Contains(this)).ToList();
        }

        [MenuItem(CraftableConstants.MATERIAL_MENU, false, CraftableConstants.MATERIAL_PRIORITY)]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}