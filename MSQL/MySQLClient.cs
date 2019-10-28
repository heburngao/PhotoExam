using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MSQL
{
    public class MySQLClient
    {
        public static void TestFn()
        {



            string host = "localhost";
            int port = 3306;
            string user = "root";
            string passwd = "gongxifacai";
            string db = "hellodb";
            string charset = "utf8";
            bool isAllowUserParam = true;



//            SelectData(host, port, user, passwd, db, charset, isAllowUserParam);
            UpdateData(host, port, user, passwd, db, charset, isAllowUserParam);

//                Reader(conn);
//                ReaderOne(conn);
//                InsertRow(conn);



        }


        private static void SelectData(string host, int port, string user, string passwd, string db, string charset,
             bool isAllowUserParam)
         {
             string conn = string.Format(
                 "server={0};port={1};uid={2};password={3};database={4};pooling=false;charset={5};Allow User Variables={6}",
                 host, port, user, passwd, db, charset, isAllowUserParam);

             DataSet set = new DataSet();
             DataTable table = new DataTable();
             DataRowCollection rows; //= new DataRowCollection();
             DataRow row; //= new DataRow();
             try
             {
                 MySqlConnection sqlConnect = new MySqlConnection(conn);
                 sqlConnect.Open();

                 string queryStr = string.Format("SELECT id,userName,passWord,age  FROM  {0}", "users");
                 MySqlCommand cmd = new MySqlCommand(queryStr, sqlConnect);
                 MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                 adapter.Fill(set, "V_users"); //将查询的结果存到虚拟数据库ds中的虚拟表tabuser中
                 table = set.Tables["V_users"]; //将数据表tabuser的数据复制到DataTable对象（取数据）
                 rows = table.Rows; //用DataRowCollection对象获取这个数据表的所有数据行
                 for (int i = 0; i < rows.Count; i++)
                 {
                     row = rows[i];
                     Console.WriteLine(i + ", id:" + row[0] + " userName:" + row[1] + " passWord:" + row[2] + " age:" +
                                       row[3]);
                 }

                 sqlConnect.Close();
                 sqlConnect = null;
             }
             catch (MySqlException ex)
             {
                 switch (ex.Number)
                 {
                     case 0:
                         Console.WriteLine("Cannot connect to server.  Contact administrator");
                         break;
                     case 1045:
                         Console.WriteLine("Invalid username/password, please try again");
                         break;
                 }
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
             }
         }

        private static void UpdateData(string host, int port, string user, string passwd, string db, string charset,
            bool isAllowUserParam)
        {
            string conn = string.Format(
                "server={0};port={1};uid={2};password={3};database={4};pooling=false;charset={5};Allow User Variables={6}",
                host, port, user, passwd, db, charset, isAllowUserParam);

            DataSet set = new DataSet();
            DataTable table = new DataTable();
            DataRowCollection rows; //= new DataRowCollection();
            DataRow row; //= new DataRow();
            try
            {
                MySqlConnection sqlConnect = new MySqlConnection(conn);
                sqlConnect.Open();

                string queryStr = string.Format("SELECT id,userName,passWord,age  FROM  {0}", "users");
                MySqlCommand cmd = new MySqlCommand(queryStr, sqlConnect);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);
                adapter.Fill(set, "V_users"); //将查询的结果存到虚拟数据库ds中的虚拟表tabuser中
                table = set.Tables["V_users"]; //将数据表tabuser的数据复制到DataTable对象（取数据）
                rows = table.Rows; //用DataRowCollection对象获取这个数据表的所有数据行
                for (int i = 0; i < rows.Count; i++)
                {
                    row = rows[i];
                    if (row["id"].ToString() == 2 + "")
                    {
                        row["passWord"] = "abc";
                    }

                    Console.WriteLine(i + ", update : id:" + row["id"] + " userName:" + row["userName"] + " passWord:" +
                                      row["passWord"] + " age:" + row["age"]);
                }

                adapter.Update(set, "V_users");

                sqlConnect.Close();
                sqlConnect = null;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string Cnf(string host, int port, string user, string passwd, string db, string charset = "utf8",
            bool isAllowUserParam = true)
        {
//            string host = "localhost";ooo
//            int port = 3306;
//            string user = "root";
//            string passwd = "gongxifacai";
//            string db = "hellodb";
//            string charset = "utf8";
//            bool isAllowUserParam = true;
            string conn;
            conn = string.Format(
                "server={0};port={1};uid={2};password={3};database={4};pooling=false;charset={5};Allow User Variables={6}",
                host, port, user, passwd, db, charset, isAllowUserParam);
            return conn;
        }

        private static object QueryData(string sql, string conn)
        {
            var connection = new MySqlConnection(conn);
            MySqlCommand cmd;
            cmd = new MySqlCommand(sql, connection);
            object result = cmd.ExecuteScalar();
            return result;
        }

        private static DataSet Query(string sql, string conn)
        {
            var dt = new DataSet();

            var connection = new MySqlConnection(conn);
//            var adapter = new MySqlDataAdapter(sql , connection);
            var adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(sql, connection);
            adapter.Fill(dt);
            adapter.Dispose();
            return dt;
        }

        public static DataSet SelectRows(DataSet dataset, string connection, string query)
        {
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, conn);
            adapter.Fill(dataset);
            return dataset;
        }

        public static void Reader(string conn)
        {
            var connection = new MySqlConnection(conn);
            MySqlCommand myCommand = connection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM users";
            myCommand.Connection.Open();
            MySqlDataReader reader = myCommand.ExecuteReader(); //执行ExecuteReader()返回一个MySqlDataReader对象
            while (reader.Read()) //初始索引是-1，执行读取下一行数据，返回值是bool
            {
                //"id"是数据库对应的列名，推荐这种方式
                Console.WriteLine(reader.GetInt32("id") + reader.GetString("userName") + reader.GetString("passWord") +
                                  reader.GetInt32("age"));
            }

            myCommand.Connection.Close();
        }

        public static void ReaderOne(string conn)
        {
            var connection = new MySqlConnection(conn);
            MySqlCommand myCommand = connection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM users";
            myCommand.Connection.Open();
            ////执行查询，并返回查询结果集中第一行的第一列。所有其他的列和行将被忽略。select语句无记录返回时，ExecuteScalar()返回NULL值
            object reader = myCommand.ExecuteScalar();
            if (reader != null)
            {
                Console.WriteLine(reader.ToString());
            }

            myCommand.Connection.Close();
        }

        public static void InsertRow(string conn)
        {

            var connection = new MySqlConnection(conn);
            connection.Open(); //必须打开通道之后才能开始事务
            var tran = connection.BeginTransaction(); //事务必须在try外面赋值不然catch里的transaction会报错:未赋值
            try
            {

                string myInsertQuery =
                    string.Format("INSERT INTO users(id,userName,passWord,age) values ({0},'{1}','{2}',{3})", 7,
                        "heburn",
                        "gongxifacai", 22);

                MySqlCommand myCommand = new MySqlCommand(myInsertQuery, connection);
                //            myCommand.Connection = connection;

                Console.WriteLine(" InserRow ");
                //执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，下面代码都不执行
                var result = myCommand.ExecuteNonQuery();
                Console.WriteLine(" InserRow result : " + result);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("OOXX" + ex.Message);
                tran.Rollback();
                connection.Close();
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    tran.Commit(); //事务要么回滚要么提交，即Rollback()与Commit()只能执行一个
                    connection.Close();
                }
            }
        }
    }
}