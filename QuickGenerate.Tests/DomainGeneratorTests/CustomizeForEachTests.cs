using System.Collections.Generic;
using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class CustomizeForEachTests
    {
        private readonly DomainGenerator domainGenerator;
        private readonly Spy spy;

        public CustomizeForEachTests()
        {
            spy = new Spy();

            domainGenerator =
                new DomainGenerator()
                    .OneToMany<Category, Product>(1, (l, r) => { l.Products.Add(r);  r.MyCategory = l; })
                    .ForEach<IThing>(thing => spy.Check(thing));
        }

        [Fact]
        public void ShouldBeCalledForEachIThing()
        {
            var product = domainGenerator.One<Product>();
            Assert.True(spy.Checked.Contains(product));
            Assert.True(spy.Checked.Contains(product.MyCategory));

            var category = domainGenerator.One<Category>();
            Assert.True(spy.Checked.Contains(category));
        }

        public class Spy
        {
            public List<object> Checked = new List<object>();
            public void Check(object toCheck)
            {
                Checked.Add(toCheck);
            }
        }
    }
}