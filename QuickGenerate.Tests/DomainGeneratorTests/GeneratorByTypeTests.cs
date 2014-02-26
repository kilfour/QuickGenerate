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
			var is42 = false;
			var isNull = false;
			20.Times(() =>
			         	{
			         		var value = domainGenerator.One<SomethingElseToGenerate>().Value;
							if (value.HasValue)
								is42 = is42 || value.Value == 42;
							else
								isNull = true;
			         	});
			Assert.True(is42);
			Assert.True(isNull);
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