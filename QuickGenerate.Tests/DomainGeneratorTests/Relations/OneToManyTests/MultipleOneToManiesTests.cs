using System.Collections.Generic;
using System.Linq;
using QuickGenerate.DomainGeneratorImplementation;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToManyTests
{
    public class MultipleOneToManiesTests
    {
        private readonly IDomainGenerator domainGenerator;

        public MultipleOneToManiesTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToMany<SomeParent, SomeChild>(1, (one, many) => one.Children.Add(many))
                    .OneToMany<SomeParent, SomeOtherChild>(1, (one, many) => one.OtherChildren.Add(many));
        }

        [Fact]
        public void Generating_Parent()
        {
            var something = domainGenerator.One<SomeParent>();
            Assert.Equal(1, something.Children.Count);
            Assert.Equal(1, something.OtherChildren.Count);
            Assert.Equal(3, ((DomainGenerator)domainGenerator).GeneratedObjects.Count());
        }

        [Fact]
        public void Generating_Child_Does_Not_Apply_Relation()
        {
            var something = domainGenerator.One<SomeChild>();
            Assert.NotNull(something);
            Assert.Equal(1, ((DomainGenerator)domainGenerator).GeneratedObjects.Count());
        }


        public class SomeParent
        {
            public IList<SomeChild> Children { get; set; }
            public IList<SomeOtherChild> OtherChildren { get; set; }

            public SomeParent()
            {
                Children = new List<SomeChild>();
                OtherChildren = new List<SomeOtherChild>();
            }
        }

        public class SomeChild { }

        public class SomeOtherChild { }
    }
}