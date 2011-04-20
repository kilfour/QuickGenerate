using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.OneToManyTests
{
    public class OneToManyUsingConstructorTests
    {
        private readonly DomainGenerator domainGenerator;

        public OneToManyUsingConstructorTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .With(42)
                    .OneToMany<SomeParent, SomeChild>(
                        1,
                        one => new SomeChild(one),
                        (one, many) => one.Children.Add(many));
        }

        [Fact]
        public void RelationIsApplied()
        {
            var something = domainGenerator.One<SomeParent>();
            Assert.Equal(1, something.Children.Count);
            Assert.Equal(something, something.Children[0].Parent);
        }

        [Fact]
        public void ManyPartOfTheRelationHasGeneratorsApplied()
        {
            Assert.Equal(42, domainGenerator.One<SomeParent>().Children[0].Answer);
        }


        public class SomeParent
        {
            public IList<SomeChild> Children { get; set; }

            public SomeParent()
            {
                Children = new List<SomeChild>();
            }
        }

        public class SomeChild
        {
            public SomeChild(SomeParent parent)
            {
                Parent = parent;
            }

            public SomeParent Parent { get; private set; }
            public int Answer { get; set; }
        }
    }
}
