using QuickGenerate.Primitives;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class GeneratorByTypeTests
    {
        [Fact]
        public void GeneratorIsApplied()
        {
            var domainGenerator =
                new DomainGenerator()
                    .ForPrimitive(new IntGenerator(42, 42));

            Assert.Equal(42, domainGenerator.One<SomethingToGenerate>().Value);
        }

		[Fact]
		public void HandlesNullables()
		{
			var domainGenerator =
				new DomainGenerator()
					.ForPrimitive(new IntGenerator(42, 42));

			Assert.Equal(42, domainGenerator.One<SomethingElseToGenerate>().Value.GetValueOrDefault());
		}

        public class SomethingToGenerate
        {
            public int Value { get; set; }
        }

		public class SomethingElseToGenerate
		{
			public int? Value { get; set; }
		}
    }
}