using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class FirstSteps
    {
        private readonly DomainGenerator domainGenerator;

        public FirstSteps()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToOne<Product, ProductPrice>((l, r) => l.MyProductPrice = r)
                    .OneToOne<Product, BidirectionalProductPrice>(
                        (l, r) =>
                            {
                                l.MyBidirectionalProductPrice = r;
                                r.MyProduct = l;
                            });
        }

        [Fact]
        public void SimpleEntity()
        {
            var notZero = false;
            5.Times(() => notZero = notZero || domainGenerator.One<ProductPrice>().Value != 0);
            Assert.True(notZero);
        }

        [Fact]
        public void UnidirectionalOneToOne()
        {
            var product = domainGenerator.One<Product>();

            Assert.NotNull(product.MyProductPrice);

            var notZero = false;
            5.Times(() => notZero = notZero || domainGenerator.One<Product>().MyProductPrice.Value != 0);
            Assert.True(notZero);
        }

        [Fact]
        public void BidirectionalOneToOne()
        {
            var product = domainGenerator.One<Product>();

            Assert.NotNull(product.MyBidirectionalProductPrice);

            Assert.Equal(product, product.MyBidirectionalProductPrice.MyProduct);
        }

        [Fact]
        public void BidirectionalOneToOne_OtherWayRound()
        {
            var productPrice = domainGenerator.One<BidirectionalProductPrice>();

            Assert.NotNull(productPrice.MyProduct);

            Assert.Equal(productPrice, productPrice.MyProduct.MyBidirectionalProductPrice);
        }
    }
}
