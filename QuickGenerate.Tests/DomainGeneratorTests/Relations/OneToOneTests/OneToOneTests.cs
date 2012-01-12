using QuickGenerate.Tests.DomainGeneratorTests.TheDomain;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToOneTests
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
        public void BidirectionalOneToOne_OtherWayRound_does_Not_Generate_LeftHand()
        {
            var productPrice = 
                new DomainGenerator()
                    .OneToOne<Product, BidirectionalProductPrice>(
                        (l, r) =>
                        {
                            l.MyBidirectionalProductPrice = r;
                            r.MyProduct = l;
                        }).One<BidirectionalProductPrice>();

            Assert.Null(productPrice.MyProduct);
        }
    }
}