using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Relations.OneToManyTests
{
    public class ComplexOneToManyTests
    {
        private readonly DomainGenerator domainGenerator;

        public ComplexOneToManyTests()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToMany<SomeParent, SomeThingInCommon>(1, (one, many) => one.Common.Add(many))
                    .OneToMany<SomeParent, SomeChild>(
                        1,
                        one => new SomeChild(one.Common.PickOne()),
                        (one, many) => one.Children.Add(many));
        }

        [Fact]
        public void DoesNotThrow()
        {
            var something = domainGenerator.One<SomeParent>();
            Assert.Equal(1, something.Children.Count);
            Assert.Equal(1, something.Common.Count);
            Assert.Equal(something.Common.First(), something.Children.First().ThingInCommon);
        }


        public class SomeParent
        {
            public IList<SomeChild> Children { get; set; }
            public IList<SomeThingInCommon> Common { get; set; }

            public SomeParent()
            {
                Children = new List<SomeChild>();
                Common = new List<SomeThingInCommon>();
            }
        }

        public class SomeChild
        {
            public SomeChild(SomeThingInCommon thingInCommon)
            {
                ThingInCommon = thingInCommon;
            }

            public SomeThingInCommon ThingInCommon { get; private set; }
        }

        public class SomeThingInCommon { }
    }
}