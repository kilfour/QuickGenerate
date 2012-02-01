using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToManyTests
{
    public class MultipleOneToManiesOfSameClassTests
    {
        [Fact]
        public void Generating_Parent()
        {
            var domainGenerator =
                new DomainGenerator()
                    .OneToMany<SomeParent, SomeChild>(1, (one, many) => one.Children.Add(many))
                    .OneToMany<SomeParent, SomeChild>(1, (one, many) => one.OtherChildren.Add(many));
            var something = domainGenerator.One<SomeParent>();
            Assert.Equal(1, something.Children.Count);
            Assert.Equal(1, something.OtherChildren.Count);
            Assert.Equal(3, domainGenerator.GeneratedObjects.Count());
        }


        public class SomeParent
        {
            public IList<SomeChild> Children { get; set; }
            public IList<SomeChild> OtherChildren { get; set; }

            public SomeParent()
            {
                Children = new List<SomeChild>();
                OtherChildren = new List<SomeChild>();
            }
        }

        public class SomeChild { }
    }
}