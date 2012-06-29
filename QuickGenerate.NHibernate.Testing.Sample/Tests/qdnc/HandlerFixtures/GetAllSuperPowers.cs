using System.Linq;
using QuickDotNetCheck;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Handlers.GetAllSuperPowers;
using QuickGenerate.NHibernate.Testing.Sample.Tests.Tools;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.HandlerFixtures
{
    public class GetAllSuperPowers : Fixture, IUse<DataAccess>
    {
        private DataAccess data;
        private NHibernateSqlLogSpy spy;
        public void Set(DataAccess state)
        {
            data = state;
        }

        public override bool CanAct()
        {
            return data.Has<SuperPower>();
        }

        protected override void Act()
        {
            spy = new NHibernateSqlLogSpy();
            data.NHibernateSession.Clear();
            var handler = new GetAllSuperPowersHandler(new GetAllSuperPowersQuery(data.NHibernateSession));
            handler.Handle();
        }

        [Spec]
        public void ShouldUseOnlyOneQuery()
        {
            Ensure.Equal(1, spy.Appender.GetEvents().Count(), spy.GetWholeLog());
        }
    }
}