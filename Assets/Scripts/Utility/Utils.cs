using System;

namespace Utility
{
    public static class Utils
    {
        public static bool IsSubClassOf(object obj, Type type)
        {
            return obj.GetType().IsSubclassOf(type);
        }
    }
}