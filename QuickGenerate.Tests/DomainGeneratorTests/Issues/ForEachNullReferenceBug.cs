using System.Collections.Generic;
using System.Linq;
using QuickGenerate.Reflect;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class ForEachNullReferenceBug
    {
        private readonly DomainGenerator domainGenerator;

        public ForEachNullReferenceBug()
        {
            domainGenerator =
                new DomainGenerator()
                    .OneToOne<SomethingToGenerate, SomethingElseToGenerate>((l, r) => l.MyMethod(r))
                    .ForEach<SomethingToGenerate>(s => s.SomethingElse.DoSomething());
        }

        [Fact(Skip = "Inheritence support removed")]
        public void GeneratingLeftHand()
        {
            var something = domainGenerator.One<SomethingToGenerate>();
            Assert.Equal(something, something.SomethingElse.Something);
            Assert.Equal(2, domainGenerator.GeneratedObjects.Count());
        }

        [Fact(Skip = "Inheritence support removed")]
        public void GeneratingRightHand()
        {
            var somethingElse = domainGenerator.One<SomethingElseToGenerate>();
            Assert.Equal(somethingElse, somethingElse.Something.SomethingElse);
            Assert.Equal(2, domainGenerator.GeneratedObjects.Count());
        }

        public class SomethingToGenerate
        {
            public SomethingElseToGenerate SomethingElse { get; private set; }
            public void MyMethod(SomethingElseToGenerate somethingElse)
            {
                SomethingElse = somethingElse;
                SomethingElse.Something = this;
            }
        }

        public abstract class SomethingElseToGenerate
        {
            public SomethingToGenerate Something { get; set; }
            public void DoSomething() { }
        }

        public class SomethingDerivedToGenerate : SomethingElseToGenerate { }
        public class SomethingElseDerivedToGenerate : SomethingElseToGenerate { }
    }
}
