//using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
//using Xunit;

//namespace QuickGenerate.Tests.DomainGeneratorTests
//{
//    public class CustomizeTypeOneToManyTests
//    {
//        private readonly DomainGenerator domainGenerator;

//        public CustomizeTypeOneToManyTests()
//        {
//            domainGenerator =
//                new DomainGenerator()
//                    .OneToMany<Category, Product>(
//                        2,
//                        (c, p) => 
//                        { 
//                            c.Products.Add(p);
//                            p.MyCategory = c;
//                        });
//        }

//        [Fact]
//        public void CategoryTest()
//        {
//            var category = domainGenerator.One<Category>();
//            Assert.Equal(2, category.Products.Count);

//            Assert.Equal(category, category.Products[0].MyCategory);
//            Assert.Equal(category, category.Products[1].MyCategory);
//        }

//        [Fact]
//        public void ProductTest()
//        {
//            var product = domainGenerator.One<Product>();
//            var category = product.MyCategory;

//            Assert.True(category.Products.Contains(product));
//            Assert.Equal(2, category.Products.Count);

//            Assert.Equal(category, category.Products[0].MyCategory);
//            Assert.Equal(category, category.Products[1].MyCategory);
//        }
//    }
//}