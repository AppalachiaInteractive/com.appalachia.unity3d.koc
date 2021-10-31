using System;
using Appalachia.Core.Scriptables;

namespace Appalachia.KOC.Crafting.Base
{
    [Serializable]
    public abstract class CraftingComponent<T> : AutonamedIdentifiableAppalachiaObject<T>
        where T : CraftingComponent<T>
    {
    }
}
