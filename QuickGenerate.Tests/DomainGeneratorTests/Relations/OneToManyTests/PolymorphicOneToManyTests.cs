using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToManyTests
{
    public class PolymorphicOneToManyTests
    {
        private readonly IDomainGenerator domainGenerator;

        public PolymorphicOneToManyTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToMany<DerivedOne, Many>(
                        1,
                        (one, many) =>
                            {
                                one.Manies.Add(many);
                                many.One = one;
                            });
        }


        [Fact]
        public void GeneratingOne()
        {
            var one = domainGenerator.One<DerivedOne>();
            Assert.Equal(1, one.Manies.Count());
            Assert.Equal(one, one.Manies[0].One);
        }

        [Fact]
        public void GeneratingManyDoesNotGenerateOne()
        {
            var many = domainGenerator.One<Many>();
            Assert.Null(many.One);
        }

        public abstract class AbstractBase { }

        public class DerivedOne
        {
            public DerivedOne()
            {
                Manies = new List<Many>();
            }

            public List<Many> Manies { get; set; }
        }

        public class Many
        {
            public DerivedOne One { get; set; }
        }
    }
}
