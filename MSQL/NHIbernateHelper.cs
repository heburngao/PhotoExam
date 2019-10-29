 
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MySql.Data.MySqlClient;
using NHibernate;

namespace MSQL
{
    public class NHIbernateHelper
    {
            static ISessionFactory sessionFactory;
        public  static  void InitializeSessionFactory()
        {
            var cnf = MySQLConfiguration.Standard.ConnectionString(db => 
                db.Server("localhost").Database("hellodb").Username("root").Password("gongxifacai")
            );
            sessionFactory = Fluently.Configure().Database(cnf).Mappings(p=>p.FluentMappings.AddFromAssemblyOf<NHIbernateHelper>()).BuildSessionFactory();
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    InitializeSessionFactory();
                return sessionFactory;
            }
        }
         
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}