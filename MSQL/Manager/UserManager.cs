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

        public void DelUserById(int id)
        {
            using (var session = NHIbernateHelper.OpenSession()){
                using (var tran = session.BeginTransaction())
                {
                    var user = new MUser();
                    user.Id = id;
                    session.Delete(user);
                    tran.Commit();
                }    
            }
        }
        public void UpdateUser(MUser user)
        {
            using (var session = NHIbernateHelper.OpenSession()){
                using (var tran = session.BeginTransaction())
                {
                    session.Update(user);
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
            newuser.UserName = "高 奀";
            newuser.PassWord = "222222";
            newuser.Age = 3;
            mgr.SaveUser(newuser);

            var userX = list[0];
            userX.Age = 100;
            userX.PassWord = "0123456789";
            mgr.UpdateUser(userX);
//            mgr.DelUserById(4);

            Console.ReadKey();
        }
    }
}