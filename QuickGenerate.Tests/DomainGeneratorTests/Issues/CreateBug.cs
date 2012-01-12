using Xunit;

namespace QuickGenerate.Tests.DomainGeneratorTests.Issues
{
    public class CreateBug
    {
        [Fact]
        public void DoesNotThrow()
        {
            var problem = Generate.One<Problem>();
        }

        [Fact]
        public void DoesNotThrowPolymorphic()
        {
            var problem = Generate.One<Problem>();
        }

        public class Problem
        {
            private readonly string a;

            public Problem(string a)
            {
                this.a = a;
            }
        }

        public class DerivedProblem
        {
            
        }
    }
}