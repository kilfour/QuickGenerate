using QuickDotNetCheck;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.BuildDomainFixtures;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.HandlerFixtures;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools;
using QuickGenerate.Reflect;
using Xunit;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc
{
    public class HandlerSuite : Suite
    {
        public HandlerSuite() : base(5)
        {
            Using(() => new DataAccess());
        }

        [Fact]
        public void VerifyAll()
        {
            Do(20, opt => opt.Register(
                FromAssembly
                    .Containing<HandlerSuite>()
                    .Implementing<Fixture>()
                    .InSameNamespaceAs<AddSuperHero>()));

            Do(10, opt => opt.Register(
                FromAssembly
                    .Containing<HandlerSuite>()
                    .Implementing<Fixture>()
                    .InSameNamespaceAs<GetSuperHero>()));
            
            Run();
        }
    }
}