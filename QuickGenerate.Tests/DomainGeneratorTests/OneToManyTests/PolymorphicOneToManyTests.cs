using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.OneToManyTests
{
    public class PolymorphicOneToManyTests
    {
        private readonly DomainGenerator domainGenerator;

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
        public void GeneratingMany()
        {
            var many = domainGenerator.One<Many>();
            Assert.Equal(many, many.One.Manies[0]);
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
