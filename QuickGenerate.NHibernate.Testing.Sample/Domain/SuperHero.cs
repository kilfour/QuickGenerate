using System;
using Iesi.Collections.Generic;

namespace QuickGenerate.NHibernate.Testing.Sample.Domain
{
    public class SuperHero : IHaveAnId
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<SuperPower> SuperPowers { get; set; }

        public SuperHero()
        {
            SuperPowers = new HashedSet<SuperPower>();
        }
    }
}
