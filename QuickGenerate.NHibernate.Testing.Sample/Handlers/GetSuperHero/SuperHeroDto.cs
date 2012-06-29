using System.Collections.Generic;

namespace QuickGenerate.NHibernate.Testing.Sample.Handlers.GetSuperHero
{
    public class SuperHeroDto
    {
        public string Name { get; set; }
        public List<string> SuperPowers { get; set; }
        public List<string> SuperPowerEffects { get; set; } 
    }
}