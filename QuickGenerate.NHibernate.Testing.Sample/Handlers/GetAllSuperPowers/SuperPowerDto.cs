using System.Collections.Generic;

namespace QuickGenerate.NHibernate.Testing.Sample.Handlers.GetAllSuperPowers
{
    public class SuperPowerDto
    {
        public string Name { get; set; }
        public List<string> SuperPowerEffects { get; set; } 
    }
}