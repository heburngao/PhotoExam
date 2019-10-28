using FluentNHibernate.Mapping;

namespace MSQL.Model.Mapping
{
    public class UserMap : ClassMap<MUser>
    {
        public UserMap()
        {
            Id(p => p.Id).Column("id");
            Map(p => p.UserName).Column("userName");
            Map(p => p.PassWord).Column("passWord");
            Map(p => p.Age).Column("age");
            Table("users");
        }
    }
}