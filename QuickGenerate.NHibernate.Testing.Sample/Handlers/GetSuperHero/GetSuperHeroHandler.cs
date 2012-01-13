using System;
using System.Linq;

namespace QuickGenerate.NHibernate.Testing.Sample.Handlers.GetSuperHero
{
    public class GetSuperHeroHandler
    {
        private readonly IGetSuperHeroQuery query;

        public GetSuperHeroHandler(IGetSuperHeroQuery query)
        {
            this.query = query;
        }

        public SuperHeroDto Handle(Guid superHeroId)
        {
            var hero = query.One(superHeroId);
            return
                new SuperHeroDto
                    {
                        Name = hero.Name,
                        SuperPowers = hero.SuperPowers.Select(sp => sp.Name).ToList()
                    };
        }
    }
}
