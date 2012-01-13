using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations
{
    public class WarnAgainstRecursionTests
    {
        [Fact]
        public void OneToOneShouldThrow()
        {
            var generator =
                new DomainGenerator()
                    .OneToOne<Something, SomethingElse>((s, se) => s.MySomethingElse = se);

            Assert.Throws<RecursiveRelationDefinedException>(() => generator.One<Something>());
        }

        [Fact]
        public void OneToManyShouldThrow()
        {
            var generator =
                new DomainGenerator()
                    .OneToMany<Something, SomethingElse>(1, (s, se) => s.MySomethingElse = se);

            Assert.Throws<RecursiveRelationDefinedException>(() => generator.One<Something>());
        }

        [Fact]
        public void ManyToOneShouldThrow()
        {
            var generator =
                new DomainGenerator()
                    .ManyToOne<Something, SomethingElse>(1, (s, se) => s.MySomethingElse = se);

            Assert.Throws<RecursiveRelationDefinedException>(() => generator.One<Something>());
        }

        public class Something
        {
            public SomethingElse MySomethingElse { get; set; }
        }

        public class SomethingElse
        {
            public Something MySomething { get; set; }

            public SomethingElse(Something something)
            {
                MySomething = something;
            }
        }

        
    }
}
