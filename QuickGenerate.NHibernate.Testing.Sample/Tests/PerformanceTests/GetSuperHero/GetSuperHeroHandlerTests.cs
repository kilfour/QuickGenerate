using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Handlers.GetSuperHero;
using QuickGenerate.NHibernate.Testing.Sample.Tests.Tools;
using Xunit;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.PerformanceTests.GetSuperHero
{
    public class GetSuperHeroHandlerTests : DatabaseTest
    {
        [Fact]
        public void DoesNotCauseLazyLoading()
        {
            var superhero =
                new DomainGenerator()
                    .With<IHaveAnId>(opt => opt.Ignore(e => e.Id))
                    .OneToMany<SuperHero, SuperPower>(5, (sh, sp) => sh.SuperPowers.Add(sp))
                    .ForEach<IHaveAnId>(SaveToSession)
                    .One<SuperHero>();

            FlushAndClear();

            var id = superhero.Id;
            
            var handler = new GetSuperHeroHandler(new GetSuperHeroQuery(NHibernateSession));

            using(1.Queries())
            {
                handler.Handle(id);
            }
        }
    }
}
