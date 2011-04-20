using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickGenerate.Writing
{
    public class PrimitivesWriter
    {
        private readonly Dictionary<Type, IWriter> primitiveWriters =
            new Dictionary<Type, IWriter>();

        public PrimitivesWriter()
        {
            primitiveWriters.Add(typeof(int?), new IntWriter());
            primitiveWriters.Add(typeof(string), new StringWriter());
            primitiveWriters.Add(typeof(Guid?), new GuidWriter());
        }

        public void Write(IStream stream, Type type, object somethingToWrite)
        {
            primitiveWriters.First(kv => kv.Key.IsAssignableFrom(type)).Value.Write(stream, somethingToWrite);
        }

        public bool IsMatch(Type type)
        {
            return primitiveWriters.Keys.Any(t => t.IsAssignableFrom(type));
        }
    }
}
