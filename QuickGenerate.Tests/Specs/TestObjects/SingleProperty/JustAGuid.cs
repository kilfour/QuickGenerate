using System;

namespace QuickGenerate.Tests.Doc.Domain.Primitives.SingleProperty
{
    public class JustAGuid : IHaveOnlyOnePrimitive
    {
        public Guid GuidProperty { get; set; }
    }
}