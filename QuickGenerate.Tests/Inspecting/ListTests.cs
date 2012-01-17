using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.Inspecting
{
    public class OneToManyTests
    {
        [Fact]
        public void AreMemberWiseEqual()
        {
            var generator =
                new DomainGenerator()
                    .With(42)
                    .OneToMany<Something, SomethingElse>(3, (s, se) => s.MySomethingElse.Add(se));
            var thingOne = generator.One<Something>();
            var thingTwo = generator.One<Something>();
            var inspector =
                Inspect
                    .This(thingOne, thingTwo)
                    .Inspect<Something, SomethingElse>(s => s.MySomethingElse);
            Assert.True(inspector.AreMemberWiseEqual());
            thingTwo.MySomethingElse[1].MyProp = 43;
            Assert.False(inspector.AreMemberWiseEqual());
        }

        public class Something
        {
            public List<SomethingElse> MySomethingElse { get; set; }
            public Something()
            {
                MySomethingElse = new List<SomethingElse>();
            }
        }

        public class SomethingElse { public int MyProp { get; set; } }
    }
}