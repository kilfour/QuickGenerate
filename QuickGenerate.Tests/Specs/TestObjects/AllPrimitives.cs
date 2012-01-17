using System;

namespace QuickGenerate.Tests.Specs.TestObjects
{
    public class AllPrimitives : IHaveOnlyPrimitives
    {
        public long LongProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public short ShortProperty { get; set; }
        public decimal DecimalProperty { get; set; }
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public bool BoolProperty { get; set; }
        public Guid GuidProperty { get; set; }
        public Char CharProperty { get; set; }
        public float FloatProperty { get; set; }
        public TimeSpan TimeSpanProperty { get; set; }
        public double DoubleProperty { get; set; }
    }

    public class IHaveAClass : ITestObject
    {
        public AllPrimitives AllPrimitives { get; set; }
    }
}
