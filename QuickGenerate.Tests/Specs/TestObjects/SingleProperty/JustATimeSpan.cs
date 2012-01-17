using System;

namespace QuickGenerate.Tests.Doc.Domain.Primitives.SingleProperty
{
    public class JustATimeSpan : IHaveOnlyOnePrimitive
    {
        public TimeSpan TimeSpanProperty { get; set; }
    }
}