using QuickDotNetCheck;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.BuildDomainFixtures
{
    public class AddSuperPower : Fixture, IUse<DataAccess>
    {
        private DataAccess data;

        public void Set(DataAccess state)
        {
            data = state;
        }
        
        public override bool CanAct()
        {
            return data.Has<SuperHero>();
        }

        protected override void Act()
        {
            var hero = data.PickOne<SuperHero>();
            var power =
                new DomainGenerator()
                    .With<IHaveAnId>(opt => opt.Ignore(e => e.Id))
                    .One<SuperPower>();
            hero.SuperPowers.Add(power);
            data.NHibernateSession.Flush();
        }
    }
}