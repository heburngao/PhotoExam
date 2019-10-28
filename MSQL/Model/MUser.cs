namespace MSQL.Model
{
    public class MUser
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string PassWord { get; set; }
        public virtual int Age { get; set; }
    }
}