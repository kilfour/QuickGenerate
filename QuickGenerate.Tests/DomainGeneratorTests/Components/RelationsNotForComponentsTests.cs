using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Components
{
    public class RelationsNotForComponentsTests
    {
        [Fact]
        public void OneToOneFirst()
        {
            Assert.Throws<NoRelationAllowedOnComponentsException>(
                () => new DomainGenerator()
                          .OneToOne<SomeComponent, SomethingElse>((sc, se) => { })
                          .Component<SomethingToGenerate>()
                          .One<SomethingToGenerate>());
        }

        [Fact]
        public void OneToOneLast()
        {
            Assert.Throws<NoRelationAllowedOnComponentsException>(
                () => new DomainGenerator()
                          .Component<SomethingToGenerate>()
                          .OneToOne<SomeComponent, SomethingElse>((sc, se )=> { })
                          .One<SomethingToGenerate>());
        }

        [Fact]
        public void OneToManyFirst()
        {
            Assert.Throws<NoRelationAllowedOnComponentsException>(
                () => new DomainGenerator()
                          .OneToMany<SomeComponent, SomethingElse>(1, (sc, se) => { })
                          .Component<SomethingToGenerate>()
                          .One<SomethingToGenerate>());
        }

        [Fact]
        public void OneToManyLast()
        {
            Assert.Throws<NoRelationAllowedOnComponentsException>(
                () => new DomainGenerator()
                          .Component<SomethingToGenerate>()
                          .OneToMany<SomeComponent, SomethingElse>(1, (sc, se) => { })
                          .One<SomethingToGenerate>());
        }

        [Fact]
        public void ManyToOneFirst()
        {
            Assert.Throws<NoRelationAllowedOnComponentsException>(
                () => new DomainGenerator()
                          .ManyToOne<SomeComponent, SomethingElse>(1, (sc, se) => { })
                          .Component<SomethingToGenerate>()
                          .One<SomethingToGenerate>());
        }

        [Fact]
        public void ManyToOneLast()
        {
            Assert.Throws<NoRelationAllowedOnComponentsException>(
                () => new DomainGenerator()
                          .Component<SomethingToGenerate>()
                          .ManyToOne<SomeComponent, SomethingElse>(1, (sc, se) => { })
                          .One<SomethingToGenerate>());
        }

        public class SomethingToGenerate
        {
            public SomeComponent MyComponent { get; set; }
        }

        public class SomeComponent
        {
            public SomethingElse MySomethingElse { get; set; }    
        }

        public class SomethingElse { }
    }
}