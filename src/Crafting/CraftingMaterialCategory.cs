using System;
using System.Collections.Generic;
using Appalachia.CI.Constants;
using Appalachia.KOC.Crafting.Base;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Appalachia.KOC.Crafting
{
    /// <summary>
    ///     A flexible category of raw materials.  For example, wood.
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

        [UnityEditor.MenuItem(
            PKG.Menu.Appalachia.Components.MaterialCategory.Base,
            false,
            PKG.Menu.Appalachia.Components.MaterialCategory.Priority
        )]
        private static void MENU_CREATE()
        {
            var created = CreateNew();
            Selection.activeObject = created;
        }
    }
}
