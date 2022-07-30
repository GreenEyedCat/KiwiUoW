using System;
using System.Collections.Generic;
using System.Text;

namespace KiwiUoW.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsSameOrSubclass(this Type thisType, Type type)
        {
            if(thisType.IsGenericType && type.IsGenericType)
            {
                if(thisType.GetGenericTypeDefinition() == type.GetGenericTypeDefinition())
                {
                    return true;
                }
                return thisType.GetGenericTypeDefinition().BaseType.IsSameOrSubclass(type);
            }
            return thisType == type || thisType.IsSubclassOf(type);
        }

        internal static bool IsSubclassOfGeneric(this Type thisType, Type type)
        {
            if(!type.IsGenericType)
            {
                throw new ArgumentException("Type is not generic", nameof(type));
            }
            if (thisType.IsGenericType)
            {
                return thisType.GetGenericTypeDefinition().IsSameOrSubclass(type.GetGenericTypeDefinition());
            }
            else
            {
                if(thisType.BaseType != null)
                {
                    return thisType.BaseType.IsSubclassOfGeneric(type);
                }
                return false;
            }
        }
    }
}
