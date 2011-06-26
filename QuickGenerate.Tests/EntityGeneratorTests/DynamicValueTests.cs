using Xunit;

namespace QuickGenerate.Tests.EntityGeneratorTests
{
    public class DynamicValueTests
    {
        private int autoincrement;
        private int GetNext()
        {
            return ++autoincrement;
        }

        [Fact]
        public void GeneratorIsApplied()
        {
            var generator =
                new EntityGenerator<SomethingToGenerate>()
                    .For(e => e.MyProperty, GetNext);

            var thing1 = generator.One();
            Assert.Equal(1, thing1.MyProperty);

            var thing2 = generator.One();
            Assert.Equal(2, thing2.MyProperty);

            var thing3 = generator.One();
            Assert.Equal(3, thing3.MyProperty);
        }

        public class SomethingToGenerate
        {
            public int MyProperty { get; set; }
        }
    }
}