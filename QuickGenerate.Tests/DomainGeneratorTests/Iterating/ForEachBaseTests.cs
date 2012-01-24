using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Iterating
{
    public class ForEachBaseTests
    {
        [Fact]
        public void IsApplied()
        {
            var spy = new ForEachSpy();

            var domainGenerator =
                new DomainGenerator()
                    .OneToMany<SomethingElseToGenerate, SomethingToGenerate>(
                        1, (l, r) => { l.SomethingToGenerates.Add(r); r.MySomethingElseToGenerate = l; })
                    .ForEach<BaseThing>(spy.Check);

            var somethingElseToGenerate = domainGenerator.One<SomethingElseToGenerate>();
            Assert.True(spy.Checked.Contains(somethingElseToGenerate));
            Assert.True(spy.Checked.Contains(somethingElseToGenerate.SomethingToGenerates.First()));
        }

        public class BaseThing { }

        public class SomethingToGenerate : BaseThing
        {
            public SomethingElseToGenerate MySomethingElseToGenerate { get; set; }
        }

        public class SomethingElseToGenerate : BaseThing
        {
            public IList<SomethingToGenerate> SomethingToGenerates { get; set; }
            public SomethingElseToGenerate()
            {
                SomethingToGenerates = new List<SomethingToGenerate>();
            }
        }
    }
}