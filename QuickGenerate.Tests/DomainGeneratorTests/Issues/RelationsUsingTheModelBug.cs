using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class RelationsUsingTheModelBug
    {
        private readonly DomainGenerator domainGenerator;

        public RelationsUsingTheModelBug()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToMany<GrandFather, Father>(1, (f, c) => f.Add(c))
                    .OneToMany<Father, Child>(2, (f, c) => f.Add(c));
        }

        [Fact]
        public void GeneratingGrandFather()
        {
            var something = domainGenerator.One<GrandFather>();
            Assert.Equal(1, something.Down.Count); 
            Assert.Equal(2, something.Down[0].Down.Count);
            Assert.Equal(4, domainGenerator.GeneratedObjects.Count());
        }

        [Fact]
        public void GeneratingFather()
        {
            var something = domainGenerator.One<Father>();
            Assert.Equal(2, something.Down.Count);
            Assert.Equal(4, domainGenerator.GeneratedObjects.Count());
        }

        [Fact]
        public void GeneratingChild()
        {
            var something = domainGenerator.One<Child>();
            Assert.Equal(2, something.Up.Down.Count);
            Assert.Equal(4, domainGenerator.GeneratedObjects.Count());
        }

        public class GrandFather
        {
            public string MyString { get; set; }
            public List<Father> Down { get; set; }
            public GrandFather()
            {
                Down = new List<Father>();
            }

            public void Add(Father father)
            {
                Down.Add(father);
                father.Up = this;
            }
        }

        public class Father
        {
            public GrandFather Up { get; set; }
            public List<Child> Down { get; set; }
            public Father()
            {
                Down = new List<Child>();
            }

            public void Add(Child child)
            {
                Down.Add(child);
                child.Up = this;
                child.MyString = Up.MyString;
            }
        }

        public class Child
        {
            public Father Up { get; set; }
            public string MyString { get; set; }
        }
    }
}