using System;
using Iesi.Collections.Generic;

namespace QuickGenerate.NHibernate.Testing.Sample.Domain
{
    public class SuperPower : IHaveAnId
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ISet<SuperPowerEffect> SuperPowerEffects { get; set; }

        public SuperPower()
        {
            SuperPowerEffects = new HashedSet<SuperPowerEffect>();
        }
    }
}