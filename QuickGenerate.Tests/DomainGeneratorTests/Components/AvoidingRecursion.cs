using System.Collections.Generic;
using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Components
{
    public class AvoidingRecursion
    {
        [Fact]
        public void Works()
        {
            var domainGenerator =
                new DomainGenerator()
                    .Component<Something>();

            Assert.Throws<RecursiveRelationDefinedException>(() => domainGenerator.One<Something>());
        }

        public class Something
        {
            public Something MySomething { get; set; }
        }
    }
}
