using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Inheritance
{
    public class CustomizeTypeInheritenceTests
    {
        private readonly DomainGenerator domainGenerator =
            new DomainGenerator()
                .With(42)
                .With<SomethingToGenerate>(opt => opt.StartingValue(
                    () =>
                    new[]
                        {
                            new SomethingToGenerate(),
                            new SomethingDerivedToGenerate(),
                            new SomethingElseDerivedToGenerate()
                        }.PickOne()));

        [Fact]
        public void Inheritence()
        {   
            var generatedSomething = false;
            var generatedSomethingDerived = false;
            var generatedSomethingElseDerived = false;
            100.Times(
                () =>
                {
                    var something = domainGenerator.One<SomethingToGenerate>();
                    generatedSomething = generatedSomething || something.GetType() == typeof (SomethingToGenerate);
                    generatedSomethingDerived = generatedSomethingDerived || something.GetType() == typeof(SomethingDerivedToGenerate);
                    generatedSomethingElseDerived = generatedSomethingElseDerived || something.GetType() == typeof(SomethingElseDerivedToGenerate);
                });
            Assert.True(generatedSomething);
            Assert.True(generatedSomethingDerived);
            Assert.True(generatedSomethingElseDerived);
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(
                () =>
                {
                    var something = domainGenerator.One<SomethingToGenerate>();
                    Assert.Equal(42, something.MyProperty);
                });
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }

        public class SomethingDerivedToGenerate : SomethingToGenerate { }
        public class SomethingElseDerivedToGenerate : SomethingToGenerate { }
    }
}
