using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSession1.Utils
{
    public static class StringIdendex
    {
        public static string AddBitToString(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            bytes[bytes.Length - 1] |= 0x01;
            string result = Encoding.UTF8.GetString(bytes);
            return result;
        }
    }
}
