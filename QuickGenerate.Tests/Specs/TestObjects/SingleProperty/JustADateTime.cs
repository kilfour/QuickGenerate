using System;

namespace QuickGenerate.Tests.Doc.Domain.Primitives.SingleProperty
{
    public class JustADateTime : IHaveOnlyOnePrimitive
    {
        public DateTime DateTimeProperty { get; set; }
    }
}