using System.Collections.Generic;

namespace QuickGenerate.Tests.DomainGeneratorTests.TheDomain
{
    public class Category : IThing
    {
        public string Id { get; set; }
        public IList<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}