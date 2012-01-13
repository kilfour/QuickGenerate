namespace QuickGenerate.NHibernate.Testing.Sample.Tests.Tools
{
    public static class AssertQueryBuilder
    {
        public static AssertQuery Queries(this int number)
        {
            return new AssertQuery(number);
        }
    }
}