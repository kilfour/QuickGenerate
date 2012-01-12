using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests
{
    public class ForEachTests
    {
        private readonly DomainGenerator domainGenerator;
        private readonly Spy spy;

        public ForEachTests()
        {
            spy = new Spy();

            domainGenerator =
                new DomainGenerator()
                    .OneToMany<SomethingElseToGenerate, SomethingToGenerate>(
                        1, (l, r) => { l.SomethingToGenerates.Add(r); r.MySomethingElseToGenerate = l; })
                    .ForEach<IThing>(thing => spy.Check(thing));
        }

        [Fact]
        public void ShouldBeCalledForEachIThing()
        {
            var somethingElseToGenerate = domainGenerator.One<SomethingElseToGenerate>();
            Assert.True(spy.Checked.Contains(somethingElseToGenerate));
            Assert.True(spy.Checked.Contains(somethingElseToGenerate.SomethingToGenerates.First()));
        }

        public interface IThing { }

        public class SomethingToGenerate : IThing
        {
            public SomethingElseToGenerate MySomethingElseToGenerate { get; set; }
        }

        public class SomethingElseToGenerate : IThing
        {
            public IList<SomethingToGenerate> SomethingToGenerates { get; set; }
            public SomethingElseToGenerate()
            {
                SomethingToGenerates = new List<SomethingToGenerate>();
            }
        }

        public class Spy
        {
            public List<object> Checked = new List<object>();
            public void Check(object toCheck)
            {
                Checked.Add(toCheck);
            }
        }
    }
}