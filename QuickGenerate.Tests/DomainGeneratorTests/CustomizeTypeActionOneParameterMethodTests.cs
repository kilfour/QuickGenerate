using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeActionOneParameterMethodTests
    {
        private readonly IDomainGenerator domainGenerator;

        public CustomizeTypeActionOneParameterMethodTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Method<int>((e, i) => e.MyMethod(i)))
                    .With(42);
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(() => Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().MyProperty));
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; private set; }
            public void MyMethod(int anInt)
            {
                MyProperty = anInt;
            }
        }
    }
}
