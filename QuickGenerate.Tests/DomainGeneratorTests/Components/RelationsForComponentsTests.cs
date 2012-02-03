using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Components
{
    public class RelationsForComponentsTests
    {
        [Fact]
        public void OneToOneFirst()
        {
            var thing =
                new DomainGenerator()
                    .OneToOne<SomeComponent, SomethingElse>((sc, se) => sc.MySomethingElse = se)
                    .Component<SomeComponent>()
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.MyComponent.MySomethingElse);
        }

        [Fact]
        public void OneToOneLast()
        {
            var thing =
                new DomainGenerator()
                    .Component<SomeComponent>()
                    .OneToOne<SomeComponent, SomethingElse>((sc, se) => sc.MySomethingElse = se)
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.MyComponent.MySomethingElse);
        }

        [Fact]
        public void OneToManyFirst()
        {
            var thing =
                new DomainGenerator()
                    .OneToMany<SomeComponent, SomethingElse>(1, (sc, se) => sc.MySomethingElse = se)
                    .Component<SomeComponent>()
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.MyComponent.MySomethingElse);
        }

        [Fact]
        public void OneToManyLast()
        {
            var thing =
                new DomainGenerator()
                    .Component<SomeComponent>()
                    .OneToMany<SomeComponent, SomethingElse>(1, (sc, se) => sc.MySomethingElse = se)
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.MyComponent.MySomethingElse);
        }

        [Fact]
        public void ManyToOneFirst()
        {
            var thing =
                new DomainGenerator()
                    .ManyToOne<SomeComponent, SomethingElse>(1, (sc, se) => sc.MySomethingElse = se)
                    .Component<SomeComponent>()
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.MyComponent.MySomethingElse);
        }

        [Fact]
        public void ManyToOneLast()
        {
            var thing =
                new DomainGenerator()
                    .Component<SomeComponent>()
                    .ManyToOne<SomeComponent, SomethingElse>(1, (sc, se) => sc.MySomethingElse = se)
                    .One<SomethingToGenerate>();

            Assert.NotNull(thing.MyComponent.MySomethingElse);
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