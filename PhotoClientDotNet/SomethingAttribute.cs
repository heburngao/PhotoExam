using System;
using PhotoServer;

namespace PhotoServer
{
    [AttributeUsage(AttributeTargets.Class |AttributeTargets.Method ,AllowMultiple = true, Inherited = true)]
    public class SomethingAttribute:System.Attribute
    {
        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        private string data;

        public string Data
        {
            get => data;
            set => data = value;
        }

        public SomethingAttribute()
        {
        }

        public SomethingAttribute(string name)
        {
            this.name = name;
        }
    }
}
//[Something("abcxxxx",Data = "2019-11-11")]
//[Something("abc",Data = "2019-11-12")]
class UseAttributeExample
{
    public UseAttributeExample()
    { 
        
    }
}

[Something]
class AA : UseAttributeExample
{
     //xxxx 
}

[Something]
class BB : UseAttributeExample
{
    
}