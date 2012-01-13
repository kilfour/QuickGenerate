using System;

namespace QuickGenerate.NHibernate.Testing.Sample.Domain
{
    public interface IHaveAnId
    {
        Guid Id { get; }
    }
}