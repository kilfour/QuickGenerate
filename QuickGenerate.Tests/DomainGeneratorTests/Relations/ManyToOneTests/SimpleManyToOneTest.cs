using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.ManyToOneTests
{
    public class SimpleManyToOneTest
    {
        [Fact]
        public void Works()
        {
            var domainGenerator =
                new DomainGenerator()
                    .ManyToOne<Child, Parent>(2, (c, p) => p.Add(c));

            var child = domainGenerator.One<Child>();
            Assert.NotNull(child);
            Assert.NotNull(child.MyParent);
            Assert.Equal(2, child.MyParent.Children.Count);
            Assert.Contains(child, child.MyParent.Children);
        }

        public class Parent
        {
            public List<Child> Children { get; set; }

            public Parent()
            {
                Children = new List<Child>();
            }

            public void Add(Child child)
            {
                Children.Add(child);
                child.MyParent = this;
            }
        }

        public class Child
        {
            public Parent MyParent { get; set; }
        }
    }
}
