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
                        var reader = cmd.ExecuteReader();
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
        
        public void SaveUser(MUser user)
        {
            using (var session = MySQLClientHelper.OpenSession())
            {
                using (var cmd = session.CreateCommand())
                {
                    var list = new List<MUser>();

                    cmd.Connection = session;
                    cmd.CommandText = string.Format("insert into users(id,userName,passWord,age) values ({0},'{1}','{2}',{3})", user.Id,user.UserName,user.PassWord,user.Age) ;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine(" save succeed");
                    }
                    else
                    {
                        Console.WriteLine(" save failed");
                    }

                    session.Close();
                }
            }
        }

        public void DelUser(int id)
        {
            using (var session = MySQLClientHelper.OpenSession())
            {
                using (var cmd = session.CreateCommand())
                {
                    var list = new List<MUser>();

                    cmd.Connection = session;
                    cmd.CommandText = string.Format("delete from users where id = {0}", id) ;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine(" delete succeed");
                    }
                    else
                    {
                        Console.WriteLine(" delete failed");
                    }

                    session.Close();
                }
            }
        }
        public void UpdateUser(MUser user)
        {
            using (var session = MySQLClientHelper.OpenSession())
            {
                using (var cmd = session.CreateCommand())
                {
                    var list = new List<MUser>();

                    cmd.Connection = session;
                    cmd.CommandText = string.Format("update users set userName = '{0}', age = {1}, passWord = '{2}' where id = {3}", user.UserName,user.Age,user.PassWord,user.Id) ;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Console.WriteLine(" update succeed");
                    }
                    else
                    {
                        Console.WriteLine(" update failed");
                    }

                    session.Close();
                }
            }
        }
        public static void TestFn()
        {
             var mgr = new MySQL_userManager();
             var list1 = mgr.GetAllUser();
            var list = mgr.GetUserByName("ga");
            mgr.UpdateUser(new MUser
            {
                Id = 3,
                UserName = "ga",
                PassWord = "1111",
                Age = 1,
            });
            
            mgr.DelUser(3);
            
            mgr.SaveUser(new MUser
            {
                Id = 3,
                UserName = "ga",
                PassWord = "1111",
                Age = 1,
            });

        }
    }
}