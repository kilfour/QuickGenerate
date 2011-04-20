using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class ConstructionConvention
    {
        public Type Type { get; set; }
        public Func<object> Action { get; set; }
    }
}