using System;
using System.Linq;

namespace QuickGenerate.Reflect
{
    public class TypePicker
    {
        private readonly Type[] types;

        public TypePicker(Type[] types)
        {
            this.types = types;
        }

        public Type[] Implementing<T>()
        {
            return Implementing(typeof(T));
        }

        public Type[] Implementing(Type type)
        {
            return
                types
                    .Where(t => ImplementingPredicate(type, t))
                    .Where(t => !t.IsInterface)
                    .ToArray();
        }

        private static bool ImplementingPredicate(Type @interface, Type implementation)
        {
            if(@interface.IsAssignableFrom(implementation))
                return true;

            if (@interface.IsGenericType)
            {
                return 
                    implementation
                    .GetInterfaces()
                    .Where(t => t.IsGenericType)
                    .Any(t => t.GetGenericTypeDefinition() == @interface);
            }

            return false;
        }
    }

    public static class TypeExtensions
    {
        public static Type[] Excluding<T>(this Type[] types)
        {
            return types.Where(t => t != typeof (T)).ToArray();
        }
    }
}