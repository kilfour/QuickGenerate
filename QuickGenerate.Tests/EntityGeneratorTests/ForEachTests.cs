//using System.Collections.Generic;
//using Xunit;

//namespace QuickGenerate.Tests.EntityGeneratorTests
//{
//    public class ForEachTests
//    {
//        private readonly DomainGenerator domainGenerator;
//        private readonly Spy spy;

//        public ForEachTests()
//        {
//            spy = new Spy();

//            domainGenerator =
//                new DomainGenerator()
//                    .OneToMany<Category, Product>(1, (l, r) => { l.Products.Add(r);  r.MyCategory = l; })
//                    .ForEach<IThing>(thing => spy.Check(thing));
//        }

//        [Fact]
//        public void ShouldBeCalledForEachIThing()
//        {
//            var product = domainGenerator.One<Product>();
//            Assert.True(spy.Checked.Contains(product));
//            Assert.True(spy.Checked.Contains(product.MyCategory));

//            var category = domainGenerator.One<Category>();
//            Assert.True(spy.Checked.Contains(category));
//        }

//        public interface IThing { }

//        public class Product : IThing
//        {
//            public int Id { get; set; }
//            public Category MyCategory { get; set; }
//            public bool Deleted { get; set; }
//        }

//        public class Category : IThing
//        {
//            public string Id { get; set; }
//            public IList<Product> Products { get; set; }

//            public Category()
//            {
//                Products = new List<Product>();
//            }
//        }

//        public class Spy
//        {
//            public List<object> Checked = new List<object>();
//            public void Check(object toCheck)
//            {
//                Checked.Add(toCheck);
//            }
//        }
//    }
//}