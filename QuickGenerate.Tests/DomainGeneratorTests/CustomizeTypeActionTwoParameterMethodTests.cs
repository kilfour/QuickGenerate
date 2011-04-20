using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeTypeActionTwoParameterMethodTests
    {
        private readonly DomainGenerator domainGenerator;

        public CustomizeTypeActionTwoParameterMethodTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<SomethingToGenerate>(g => g.Method<int, string>((e, i, s) => e.MyMethod(i, s)))
                    .With("TEST")
                    .With(42);
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                    {
                        Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().MyIntProperty);
                        Assert.Equal("TEST", domainGenerator.One<SomethingToGenerate>().MyStringProperty);
                    });
        }

        public class SomethingToGenerate
        {
            public int MyIntProperty { get; private set; }
            public string MyStringProperty { get; private set; }
            public void MyMethod(int anInt, string aString)
            {
                MyIntProperty = anInt;
                MyStringProperty = aString;
            }
        }
    }
}