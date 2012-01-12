namespace QuickGenerate.NHibernate.Testing.Sample.Domain
{
    public class SuperPower : IHaveAnId
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}