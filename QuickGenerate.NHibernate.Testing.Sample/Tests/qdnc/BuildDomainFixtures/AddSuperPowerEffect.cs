using QuickDotNetCheck;
using QuickGenerate.NHibernate.Testing.Sample.Domain;
using QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.BuildDomainFixtures
{
    public class AddSuperPowerEffect : Fixture, IUse<DataAccess>
    {
        private DataAccess data;

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
            var power = data.PickOne<SuperPower>();
            var effect =
                new DomainGenerator()
                    .With<IHaveAnId>(opt => opt.Ignore(e => e.Id))
                    .One<SuperPowerEffect>();
            power.SuperPowerEffects.Add(effect);
            data.NHibernateSession.Flush();
        }
    }
}