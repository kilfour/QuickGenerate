using System;
using System.Linq;
using Xunit;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.Tools
{
    public class AssertQuery : IDisposable
    {
        private readonly LogSpy spy;
        private readonly int numberOfQueries;
        private bool showSql;

        public AssertQuery(int numberOfQueries)
        {
            spy = new LogSpy("NHibernate.SQL");
            this.numberOfQueries = numberOfQueries;
        }

        public void Dispose()
        {
            if(showSql)
                Assert.True(numberOfQueries == spy.Appender.GetEvents().Count(), spy.GetWholeLog());
            else
                Assert.Equal(numberOfQueries, spy.Appender.GetEvents().Count());
            spy.Dispose();
        }

        public AssertQuery ShowSql()
        {
            showSql = true;
            return this;
        }
    }
}