using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class ConstructorInheritenceTests
    {
        [Fact]
        public void TheDefaultConstructor()
        {
            var y = new SomethingDerived();
            var thing =
                new DomainGenerator()
                    .With<SomethingToGenerate>(o => o.Construct(y))
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.GetValue());
        }

        public abstract class SomethingBase { }
        public class SomethingDerived : SomethingBase { }
        public class SomethingToGenerate
        {
            private readonly SomethingBase value;
            public SomethingBase GetValue()
            {
                return value;
            }
            public SomethingToGenerate(SomethingBase value)
            {
                this.value = value;
            }
        }
    }
}
