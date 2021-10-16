using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.CI.Integration.Assets;
using Appalachia.KOC.Crafting.Base;
using Appalachia.KOC.Crafting.Utility;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    /// <summary>
    ///     A raw material used in crafting.  For example, white pine wood.
    /// </summary>
    [Serializable]
    public class CraftingMaterial : CraftingIconComponent<CraftingMaterial>
    {
        [NonSerialized]
        [ShowInInspector]
        private List<CraftingMaterialCategory> _categories;

        [NonSerialized] private bool _categoriesInitialized;

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

            var categories = AssetDatabaseManager.FindAssets<CraftingMaterialCategory>();

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
