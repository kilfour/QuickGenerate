using System.Linq;
using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class ForEachNullReferenceBug
    {
        private readonly IDomainGenerator domainGenerator;

        public ForEachNullReferenceBug()
        {
            domainGenerator =
                new DomainGenerator()
                    .With<SomethingElseToGenerate>(opt => opt.Use<SomethingDerivedToGenerate>().Use<SomethingElseDerivedToGenerate>())
                    .OneToOne<SomethingToGenerate, SomethingElseToGenerate>((l, r) => l.MyMethod(r))
                    .ForEach<SomethingToGenerate>(s => s.SomethingElse.DoSomething());
        }

        [Fact]
        public void Generating()
        {
            var something = domainGenerator.One<SomethingToGenerate>();
            Assert.Equal(something, something.SomethingElse.Something);
            Assert.Equal(2, ((DomainGenerator)domainGenerator).GeneratedObjects.Count());
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
