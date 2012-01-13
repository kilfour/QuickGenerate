using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class ActionConvention
    {
        public Type Type { get; set; }
        public Action<object> Action { get; set; }
    }

    public class FirstActionConvention
    {
        public int Amount { get; set; }
        public Type Type { get; set; }
        public Action<object> Action { get; set; }
    }

    public class LastActionConvention
    {
        public int Amount { get; set; }
        public Type Type { get; set; }
        public Action<object> Action { get; set; }
    }
}