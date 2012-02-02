using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class InheritenceTests
    {
        private readonly EntityGenerator<SomethingToGenerate> generator =
            new EntityGenerator<SomethingToGenerate>()
                .With(42)
                .With(
                    () =>
                    new[]
                        {
                            new SomethingToGenerate(),
                            new SomethingDerivedToGenerate(),
                            new SomethingElseDerivedToGenerate()
                        }.PickOne());

        [Fact]
        public void Inheritence()
        {
            var generatedSomething = false;
            var generatedSomethingDerived = false;
            var generatedSomethingElseDerived = false;
            100.Times(
                () =>
                {
                    var something = generator.One();
                    generatedSomething = generatedSomething || something.GetType() == typeof(SomethingToGenerate);
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
                    var something = generator.One();
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
