using System;
using System.Collections.Generic;
using MSQL.Model;
using NHibernate;

namespace MSQL.Manager
{
    public class UserManager
    {
        public IList<MUser> GetAllUser()
        {
            using (var session = NHIbernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    IQueryOver<MUser, MUser> userlist = session.QueryOver<MUser>();
                    tran.Commit();
                    return userlist.List();
                }
            }
        }
        public IList<MUser> GetUserByName(string name)
        {
            using (var session = NHIbernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    IQueryOver<MUser, MUser> userlist = session.QueryOver<MUser>().Where(p=> p.UserName == name);
                    tran.Commit();
                    return userlist.List();
                }
            }
        }

        public void SaveUser(MUser user)
        {
            using (var session = NHIbernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Save(user);
                    tran.Commit();
                }
            }
        }
        public static void TestFn()
        {
            var mgr = new UserManager();
            var list = mgr.GetUserByName("ghb");
            foreach (var item in list)
            {
                Console.WriteLine(item.Id + " - " + item.UserName+" - " + item.PassWord +" - "+item.Age);
            }
            var newuser = new MUser();
            newuser.UserName = "高奀";
            newuser.PassWord = "222222";
            newuser.Age = 3;
            mgr.SaveUser(newuser);
            
            Console.ReadKey();
        }
    }
}