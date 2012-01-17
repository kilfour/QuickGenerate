using Xunit;

namespace QuickGenerate.Tests.Inspecting
{
    public class OneToOneTests
    {
        [Fact]
        public void CompositeTests()
        {
            var generator =
                new DomainGenerator()
                    .With(42)
                    .Component<SomethingElse>();
            var thingOne = generator.One<Something>();
            var thingTwo = generator.One<Something>();
            var inspector = 
                Inspect
                    .This(thingOne, thingTwo)
                    .Inspect<Something, SomethingElse>(s => s.MySomethingElse);
            Assert.True(inspector.AreMemberWiseEqual());
            thingTwo.MySomethingElse.MyProp = 43;
            Assert.False(inspector.AreMemberWiseEqual());
        }

        public class Something
        {
            public SomethingElse MySomethingElse { get; set; }
            public int MyProp { get; set; }
        }

        public class SomethingElse { public int MyProp { get; set; } }
    }
}