using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using QuickGenerate.NHibernate.Testing.Sample.Domain;

namespace QuickGenerate.NHibernate.Testing.Sample.Handlers.GetSuperHero
{
    public interface IGetSuperHeroQuery
    {
        SuperHero One(Guid superHeroId);
    }

    public class GetSuperHeroQuery : IGetSuperHeroQuery
    {
        private readonly ISession session;

        public GetSuperHeroQuery(ISession session)
        {
            this.session = session;
        }

        public SuperHero One(Guid superHeroId)
        {
            return
                session
                    .CreateCriteria<SuperHero>()
                    .Add(Restrictions.Eq("Id", superHeroId))
                    .CreateAlias("SuperPowers", "sp", JoinType.LeftOuterJoin)
                    .UniqueResult<SuperHero>();
        }
    }
}
