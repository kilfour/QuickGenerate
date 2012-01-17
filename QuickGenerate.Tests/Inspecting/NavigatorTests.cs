using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.Inspecting
{
    public class NavigatorTests
    {
        [Fact]
        public void ForAllPrimitives()
        {
            var thing = new Something {MyIntProp = 42, MyStringProp = "ne string"};
            var inspector = Inspect.This(thing);
            var list = new List<object>();
            inspector.ForAllPrimitives(list.Add);
            Assert.Equal(2, list.Count);
            Assert.Equal(42, list[0]);
            Assert.Equal("ne string", list[1]);
        }

        [Fact]
        public void ForAllInts()
        {
            var thing = new Something { MyIntProp = 42, MyStringProp = "ne string" };
            var inspector = Inspect.This(thing);
            var list = new List<object>();
            inspector.ForAll(pi => pi.PropertyType == typeof(int), list.Add);
            Assert.Equal(1, list.Count);
            Assert.Equal(42, list[0]);
        }

        [Fact]
        public void ForAllStrings()
        {
            var thing = new Something { MyIntProp = 42, MyStringProp = "ne string" };
            var inspector = Inspect.This(thing);
            var list = new List<object>();
            inspector.ForAll(pi => pi.PropertyType == typeof(string), list.Add);
            Assert.Equal(1, list.Count);
            Assert.Equal("ne string", list[0]);
        }

        [Fact]
        public void ForAll()
        {
            var somethingElse = new SomethingElse();
            var thing = new Something { MyIntProp = 42, MyStringProp = "ne string", MySomethingElse = somethingElse};
            var inspector = Inspect.This(thing);
            var list = new List<object>();
            inspector.ForAll(list.Add);
            Assert.Equal(3, list.Count);
            Assert.Contains(42, list);
            Assert.Contains("ne string", list);
            Assert.Contains(somethingElse, list);
        }
       
        public class Something
        {
            public int MyIntProp{ get; set; }
            public string MyStringProp { get; set; }
            public SomethingElse MySomethingElse { get; set; }
        }

        public class SomethingElse { }
    }
}
