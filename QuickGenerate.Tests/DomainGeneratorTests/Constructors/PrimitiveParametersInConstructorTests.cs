using System;
using System.Reflection;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class PrimitiveParametersInConstructorTests
    {
        [Fact]
        public void Ints()
        {
            var generated = Generator.For<IntParams>().With(42).With<Int64>(43).One();
            Assert.Equal(42, generated.GetFieldValue("one"));
            Assert.Equal((Int64)43, generated.GetFieldValue("two"));
            
        }

        public class IntParams
        {
            private int one;
            private Int64 two;
            public IntParams(int one, Int64 two)
            {
                this.one = one;
                this.two = two;
            }
        }
    }

    public static class Ext
    {
        public static object GetFieldValue(this object obj, string name)
        {
            var info = obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            return info.GetValue(obj);
        }
    }
}
