namespace QuickGenerate.Tests.DomainGeneratorTests.TheDomain
{
    public class Product : IThing
    {
        public int Id { get; set; }
        public ProductPrice MyProductPrice { get; set; }
        public BidirectionalProductPrice MyBidirectionalProductPrice { get; set; }
        public Category MyCategory { get; set; }
        public bool Deleted { get; set; }
    }
}