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
                    tran.Commit();
                     
                    IQueryOver<MUser, MUser> userlist = session.QueryOver<MUser>();
                    return userlist.List();
                }
            }
        }

        public static void TestFn()
        {
            var mgr = new UserManager();
            var list = mgr.GetAllUser();
            foreach (var item in list)
            {
                Console.WriteLine(item.UserName);
            }
            Console.ReadKey();
        }
    }
}