using System.Collections.Generic;
using QuickGenerate.NHibernate.Testing.Sample.Tests.Tools;

namespace QuickGenerate.NHibernate.Testing.Sample.Tests.qdnc.Tools
{
    public class DataAccess : DatabaseTest
    {
        public IList<T> GetAll<T>() where T : class
        {
            return
                NHibernateSession
                    .CreateCriteria<T>()
                    .List<T>();
        }

        public bool Has<T>() where T : class
        {
            return GetAll<T>().Count > 0;
        }

        public T PickOne<T>() where T : class
        {
            return GetAll<T>().PickOne();
        }            
    }
}
