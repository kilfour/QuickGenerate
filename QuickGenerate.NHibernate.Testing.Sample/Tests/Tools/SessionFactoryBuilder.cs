using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.Tools
{
    public class SessionFactoryBuilder
    {
        public ISessionFactory BuildSessionFactory()
        {
            FileHelper.DeletePreviousDbFiles();
            var dbFile = FileHelper.GetDbFileName();
            var configuration = new Configuration();

            configuration.AddProperties(
                new Dictionary<string, string>
                    {
                        {Environment.ConnectionString, string.Format("Data Source={0};Version=3;New=True;", dbFile)}
                    });

            configuration.Configure("mysql.hibernate.cfg.xml");
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Create(false, true);
            return configuration.BuildSessionFactory();
        }
    }
}