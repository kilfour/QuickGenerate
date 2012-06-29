using QuickDotNetCheck;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.BuildDomainFixtures
{
    public class AddSuperHero : Fixture, IUse<DataAccess>
    {
        private DataAccess data;

        public void Set(DataAccess state)
        {
            data = state;
        }

        public override bool CanAct()
        {
            return data.GetAll<SuperHero>().Count <= 3;
        }

        protected override void Act()
        {
            var hero = 
                new DomainGenerator()
                    .With<IHaveAnId>(opt => opt.Ignore(e => e.Id))
                    .One<SuperHero>();
            data.NHibernateSession.Save(hero);
            data.NHibernateSession.Flush();
        }
    }
}