using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Constructors
{
    public class ConstructorInheritenceTests
    {
        [Fact(Skip="Issue from Github")]
        public void TheDefaultConstructor()
        {
            var y = new Y();
            var thing =
                new DomainGenerator()
                    .With<X>(o => o.Construct(y))
                    .One<X>();

            Assert.NotNull(thing.GetValue());
        }

        public abstract class Z { }
        public class Y : Z { }
        public class X
        {
            private readonly Z value;
            public Z GetValue()
            {
                return value;
            }
            public X(Z value)
            {
                this.value = value;
            }
        }
    }
}
