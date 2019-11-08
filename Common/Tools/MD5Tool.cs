using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Tools
{
    public class MD5Tool
    {
        public static string GetMD5(string str)
        {
            byte[] strBytes;
            strBytes = Encoding.UTF8.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output;
            output = md5.ComputeHash(strBytes);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}