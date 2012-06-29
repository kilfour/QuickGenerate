using System.Collections.Generic;
using NHibernate;
using NHibernate.SqlCommand;
using QuickGenerate.NHibernate.Testing.Sample.Domain;

namespace QuickGenerate.NHibernate.Testing.Sample.Handlers.GetAllSuperPowers
{
    public class GetAllSuperPowersQuery
    {
        private readonly ISession session;

        public GetAllSuperPowersQuery(ISession session)
        {
            this.session = session;
        }

        public IList<SuperPower> List()
        {
            return
                session
                    .CreateCriteria<SuperPower>("sp")
                    .CreateAlias("sp.SuperPowerEffects", "spe", JoinType.LeftOuterJoin)
                    .List<SuperPower>();
        }
    }
}
