using Xunit;

namespace QuickGenerate.Tests.Inspecting.Primitives
{
    public class PrimitivesTests
    {
        public class Something<T> { public T MyProp{ get; set; } }
        public class SomethingInt { public int MyProp{ get; set; } }

        [Fact]
        public void IntTest()
        {
            var thingOne = new SomethingInt {MyProp = 42};
            var thingTwo = new SomethingInt {MyProp = 42};

            var inspector = Inspect.This(thingOne, thingTwo);
           
            Assert.True(inspector.AreMemberWiseEqual());

            thingTwo.MyProp = 43;

            Assert.False(inspector.AreMemberWiseEqual());
        }

        
        public class SomethingInt16 { public int MyPropInt16{ get; set; } }
        public class SomethingInt32 { public int MyPropInt32{ get; set; } }
        public class SomethingInt64 { public int MyPropInt64{ get; set; } }
       
    }
}
