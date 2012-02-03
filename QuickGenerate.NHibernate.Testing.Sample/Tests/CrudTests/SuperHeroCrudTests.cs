using QuickGenerate.DomainGeneratorImplementation;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Tests.Tools;
using Xunit;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.CrudTests
{
    public class SuperHeroCrudTests : CrudTest<SuperHero>
    {
        protected override IDomainGenerator GenerateIt()
        {
            return 
                base.GenerateIt()
                    .OneToMany<SuperHero, SuperPower>(1, (sh, sp) => sh.SuperPowers.Add(sp))
                    .ForEach<SuperHero>(e => NHibernateSession.Save(e));
        }

        [Fact]
        public void HasManySuperPowers()
        {
            HasMany(e => e.SuperPowers);
        }
    }
}
