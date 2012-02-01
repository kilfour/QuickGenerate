using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class ActionConvention
    {
        public Type Type { get; set; }
        public Action<object> Action { get; set; }
        public Action<int, object> IndexAction { get; set; }
    }
}