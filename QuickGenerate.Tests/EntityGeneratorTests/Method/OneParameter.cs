using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests.Method
{
    public class OneParameter
    {
        private readonly EntityGenerator<SomethingToGenerate> generator =
            new EntityGenerator<SomethingToGenerate>()
                .Method<int>((e, i) => e.MyMethod(i))
                .With(42);

        [Fact]
        public void GeneratorIsApplied()
        {
            10.Times(() => Assert.Equal(42, generator.One().MyProperty));
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
