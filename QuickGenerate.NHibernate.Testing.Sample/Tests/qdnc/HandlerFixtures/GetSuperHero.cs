using System.Linq;
using QuickDotNetCheck;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Handlers.GetSuperHero;
using QuickGenerate.NHibernate.Testing.Sample.Tests.Tools;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.HandlerFixtures
{
    public class GetSuperHero : Fixture, IUse<DataAccess>
    {
        private DataAccess data;
        private NHibernateSqlLogSpy spy;
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
            var id = hero.Id;
            spy = new NHibernateSqlLogSpy();
            data.NHibernateSession.Clear();
            var handler = new GetSuperHeroHandler(new GetSuperHeroQuery(data.NHibernateSession));
            handler.Handle(id);
        }

        [Spec]
        public void ShouldUseOnlyOneQuery()
        {
            Ensure.Equal(1, spy.Appender.GetEvents().Count(), spy.GetWholeLog());
        }
    }
}