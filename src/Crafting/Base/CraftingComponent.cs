using System;
using Appalachia.Core.Scriptables;

namespace Appalachia.KOC.Crafting.Base
{
    [Serializable]
    public abstract class CraftingComponent<T> : SelfNamingSavingAndIdentifyingScriptableObject<T>
        where T : CraftingComponent<T>
    {
    }
}
