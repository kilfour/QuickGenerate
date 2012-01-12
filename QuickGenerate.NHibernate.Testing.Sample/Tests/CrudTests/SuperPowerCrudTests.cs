using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Tests.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.CrudTests
{
    public class SuperPowerCrudTests : CrudTest<SuperPower>
    {
        protected override DomainGenerator GenerateIt()
        {
            return
                base.GenerateIt()
                    .ManyToOne<SuperPower, SuperHero>(1, (sp, sh) => sh.SuperPowers.Add(sp))
                    .ForEach<SuperHero>(e => NHibernateSession.Save(e));
        }

        protected override bool DeleteEntity(SuperPower entity)
        {
            return false;// deleted when the SuperHero dies
        }
    }
}