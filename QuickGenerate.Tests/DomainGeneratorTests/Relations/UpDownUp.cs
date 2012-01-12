using System.Collections.Generic;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations
{
    public class UpDownUp
    {
        [Fact]
        public void Works()
        {
            var domainGenerator =
                new DomainGenerator()
                    .ManyToOne<Child, Parent>(2, (c, p) => p.Add(c))
                    .OneToMany<Child, GrandChild>(2, (c, gc) => c.Add(gc));

            var child = domainGenerator.One<Child>();
            Assert.NotNull(child);
            Assert.NotNull(child.MyParent);
            Assert.Equal(2, child.MyParent.Children.Count);
            Assert.Contains(child, child.MyParent.Children);
            Assert.Equal(2, child.GrandChildren.Count);

            Assert.Equal(2, child.MyParent.Children[0].GrandChildren.Count);
            Assert.Equal(2, child.MyParent.Children[1].GrandChildren.Count);
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
            public List<GrandChild> GrandChildren { get; set; }

            public Child()
            {
                GrandChildren = new List<GrandChild>();
            }

            public void Add(GrandChild grandChild)
            {
                GrandChildren.Add(grandChild);
                grandChild.MyChild = this;
            }
        }

        public class GrandChild
        {
            public Child MyChild { get; set; }
        }
    }
}