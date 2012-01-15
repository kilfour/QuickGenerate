using System;
using System.Collections.Generic;

namespace QuickGenerate.DomainGeneratorImplementation
{
    public class OneToManyRelation
    {
        public Func<int> Amount { get; set; }
        public Type One { get; set; }
        public Type Many { get; set; }
        public Action<object, object> AddChildElement { get; set; }
        public Func<object, object> CreateChildElement;
        public Func<object, IEnumerable<object>> GetChildren { get; set; }
    }

    public class OneToOneRelation
    {
        public Type Left { get; set; }
        public Type Right { get; set; }
        public Func<object, object> CreateRight;
        public Func<object, object> GetRight { get; set; }
    }

}