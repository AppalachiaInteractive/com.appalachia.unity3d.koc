using System;
using System.Collections.Generic;
using System.Linq;
using Appalachia.CI.Constants;
using Appalachia.CI.Integration.Assets;
using Appalachia.KOC.Crafting.Base;
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
        [NonSerialized] private bool _categoriesInitialized;

        [NonSerialized]
        [ShowInInspector]
        private List<CraftingMaterialCategory> _categories;

        public void RefreshCategories()
        {
            if (_categories == null)
            {
                _categories = new List<CraftingMaterialCategory>();
            }

            var categories = AssetDatabaseManager.FindAssets<CraftingMaterialCategory>();

            _categories = categories.Where(c => c.materials.Contains(this)).ToList();
        }

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

        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Components.Material.Base,
            false,
            PKG.Menu.Appalachia.Components.Material.Priority
        )]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}
