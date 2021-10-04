using System;
using System.Collections.Generic;
using System.Linq;

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
            return from a in AppDomain.CurrentDomain.GetAssemblies()
                   from t in a.GetTypes()
                   where t.IsDefined(typeof(BOTDPlayerInputMappingAttribute), false)
                   select t;
        }
    }
}