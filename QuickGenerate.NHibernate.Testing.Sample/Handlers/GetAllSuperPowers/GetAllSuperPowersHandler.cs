using System.Linq;

namespace QuickGenerate.NHibernate.Testing.Sample.Handlers.GetAllSuperPowers
{
    public class GetAllSuperPowersHandler
    {
        private readonly GetAllSuperPowersQuery query;

        public GetAllSuperPowersHandler(GetAllSuperPowersQuery query)
        {
            this.query = query;
        }

        public SuperPowerDto[] Handle()
        {
            var powers = query.List();
            return
                powers
                    .Select(
                        p => new SuperPowerDto
                                 {
                                     Name = p.Name,
                                     SuperPowerEffects =
                                         p.SuperPowerEffects.Select(spe => spe.Name).ToList()
                                 })
                    .ToArray();
        }
    }
}
