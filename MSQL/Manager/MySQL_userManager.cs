using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MSQL.Model;
using NHibernate.Mapping;

namespace MSQL.Manager
{
    public class MySQL_userManager
    {
        public IList<MUser> GetAllUser()
        {
            using (var session = MySQLClientHelper.OpenSession())
            {
                    using (var cmd = session.CreateCommand())
                    {
                        var list = new List<MUser>();

                        cmd.Connection = session;
                        cmd.CommandText = "select * from users";
//                            cmd.ExecuteNonQuery();
//                            tran.Commit();
                        var reader = cmd.ExecuteReader();

//                        for (int i = 0 ; i < reader.GetSchemaTable().Rows.Count; i++ )
//                        {
//                            DataRow row = reader.GetSchemaTable().Rows[i];
//                            Console.WriteLine("x id: " + row[0] + " userName : " +
//                                              row[1] + " , " +row[2] +" , " + row[3]);
//                        }
                        while (reader.Read())
                        {
                            Console.WriteLine("id: " + reader.GetInt32("id") + " userName : " +
                                              reader.GetString("userName"));
                            var user = new MUser();
                            user.Id = reader.GetInt32("id");
                            user.UserName = reader.GetString("userName");
                            user.PassWord = reader.GetString("passWord");
                            user.Age = reader.GetInt32("age");
                            list.Add(user);
                        }

 
                        session.Close();
                        return list;
                    }
            }
        }
        public IList<MUser> GetUserByName(string name)
        {
            using (var session = MySQLClientHelper.OpenSession())
            {
                using (var cmd = session.CreateCommand())
                {
                    var list = new List<MUser>();

                    cmd.Connection = session;
                    cmd.CommandText = "select * from users";
//                            cmd.ExecuteNonQuery();
//                            tran.Commit();
                    var reader = cmd.ExecuteReader();

                     
                    while (reader.Read())
                    {
                        if (name == reader.GetString("userName"))
                        {

                            Console.WriteLine("id: " + reader.GetInt32("id") + " userName : " +
                                              reader.GetString("userName") + " passWord: " + reader.GetString("passWord"));
                            var user = new MUser();
                            user.Id = reader.GetInt32("id");
                            user.UserName = reader.GetString("userName");
                            user.PassWord = reader.GetString("passWord");
                            user.Age = reader.GetInt32("age");
                            list.Add(user);
                            break;
                            
                        }
                    }

 
                    session.Close();
                    return list;
                }
            }
        }
        public static void TestFn()
        {
             var mgr = new MySQL_userManager();
//             var list = mgr.GetAllUser();
            var list = mgr.GetUserByName("ghb");

        }
    }
}