using MySql.Data.MySqlClient;

namespace MSQL
{
    public class MySQLClientHelper
    {
        static private MySqlConnection session;

        public static void InitializeSessionFactory()
        {
            string conn = string.Format(
                "server={0};port={1};uid={2};password={3};database={4};pooling=false;charset={5};Allow User Variables={6}",
                "localhost", 3306, "root", "gongxifacai", "hellodb", "utf8", true);
            session = new MySqlConnection(conn);
        }

        private static MySqlConnection SessionFactory
        {
            get
            {
                if (null == session)
                {
                    InitializeSessionFactory();
                }

                return session;
            }
        }

        public static MySqlConnection OpenSession()
        {
              SessionFactory.Open();
              return SessionFactory;
        }
    }
}