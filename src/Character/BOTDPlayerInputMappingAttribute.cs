using System;
using System.Collections.Generic;
using Appalachia.Utility.Reflection.Extensions;

namespace Appalachia.KOC.Character
{
    public class BOTDPlayerInputMappingAttribute : Attribute
    {
        public int index;

        public BOTDPlayerInputMappingAttribute(BOTDPlayerInputMapping mapping)
        {
            index = (int) mapping;
        }

        public static IEnumerable<Type> GetTypes()
        {
            var types = ReflectionExtensions.GetAllTypes();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                
                if (type.IsDefined(typeof(BOTDPlayerInputMappingAttribute), false))
                {
                    yield return type;
                }
            }
        }
    }
}
