using System;
using NHibernate;
using QuickGenerate.NHibernate.Testing.Sample.Domain;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.Tools
{
    public class DatabaseTest : IDisposable
    {
        private static ISessionFactory sessionFactory;

        public ISession NHibernateSession { get; set; }
        private readonly ITransaction transaction;

        protected DatabaseTest()
        {
            if (sessionFactory == null)
            {
                sessionFactory = new SessionFactoryBuilder().BuildSessionFactory();
            }

            NHibernateSession = sessionFactory.OpenSession();
            transaction = NHibernateSession.BeginTransaction();
        }

        public void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
            NHibernateSession.Dispose();
        }

        protected void FlushAndClear()
        {
            NHibernateSession.Flush();
            NHibernateSession.Clear();
        }

        protected void SaveToSession(IHaveAnId entity)
        {
            NHibernateSession.Save(entity);
        }
    }
}
