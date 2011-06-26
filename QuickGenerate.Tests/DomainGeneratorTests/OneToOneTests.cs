using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class OneToOneTests
    {
        [Fact]
        public void UnidirectionalOneToOne()
        {
            var generator = new DomainGenerator()
                .OneToOne<Product, ProductPrice>((l, r) => l.MyProductPrice = r);

            var notZero = false;
            5.Times(() => notZero = notZero || generator.One<Product>().MyProductPrice.Value != 0);
            Assert.True(notZero);
        }

        [Fact]
        public void BidirectionalOneToOne()
        {
            var product =
                new DomainGenerator()
                    .OneToOne<Product, BidirectionalProductPrice>(
                        (l, r) =>
                            {
                                l.MyBidirectionalProductPrice = r;
                                r.MyProduct = l;
                            })
                    .One<Product>();

            Assert.NotNull(product.MyBidirectionalProductPrice);

            Assert.Equal(product, product.MyBidirectionalProductPrice.MyProduct);
        }

        [Fact]
        public void BidirectionalOneToOne_OtherWayRound()
        {
            var productPrice = 
                new DomainGenerator()
                    .OneToOne<Product, BidirectionalProductPrice>(
                        (l, r) =>
                        {
                            l.MyBidirectionalProductPrice = r;
                            r.MyProduct = l;
                        }).One<BidirectionalProductPrice>();

            Assert.NotNull(productPrice.MyProduct);

            Assert.Equal(productPrice, productPrice.MyProduct.MyBidirectionalProductPrice);
        }
    }
}