using System;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class OneToManyRelation
    {
        public Func<int> Amount { get; set; }
        public Type One { get; set; }
        public Type Many { get; set; }
        public Action<object, object> Action { get; set; }
        public Func<object, object> ManyFunc;
    }
}