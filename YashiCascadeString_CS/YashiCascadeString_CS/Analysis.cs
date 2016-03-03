using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace YashiCascadeString_CS
{
    class Analysis
    {
        /*
        YashiCascadeString_CS --analysis YCSDFA0101AD^~#0^D^rootArray^~1#1^A^1^~2^2^~4^~5#5^A^1^2^3^4#4^A^1^2^3^4#2^D^A^AA^B^BB^C^CC^arr2^~3^D^DD#3^A^1^2^3^4#
        */

        private Dictionary<string, Object> returnvalue = new Dictionary<string, Object>();
        public string instring;
        public string indentstart = "[";
        public string indentend = "]";
        public string indentdic = ":";

        private ArrayList tmpObjs = new ArrayList();
        private Dictionary<string, Object> rootdic = new Dictionary<string, Object>();

        public Dictionary<string, Object> start()
        {
            //Console.WriteLine("String=" + instring);
            //str.Substring(start-1, length)
            int bchk = check();
            if (bchk < 0)
            {
                Console.WriteLine("YCSDF ERROR " + bchk + ".");
            }
            else
            {
                try
                {
                    returnvalue = (Dictionary<string, Object>)convert(instring.Substring(13, instring.Length - 14));
                }
                catch(Exception e)
                {
                    Console.WriteLine("YCSDF ERROR :");
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
            if (returnvalue == null)
            {
                Console.WriteLine("YCSDF ERROR : returnvalue == null");
            }
            return returnvalue;
        }

        //step1
        private object convert(string datastr)
        {

            return null;
        }

        public int check()
        {
            try
            {
                if (instring.Length <= 14)
                {
                    return -2; //长度不正确
                }
                else if (!instring.Substring(0, 6).Equals("YCSDFA"))
                {
                    return -3; //格式描述不匹配
                }
                else if (int.Parse(instring.Substring(6, 2)) != 01)
                {
                    return -4; //版本号不匹配
                }
                else if (int.Parse(instring.Substring(8, 2)) != 01)
                {
                    return -5; //模式不支持
                }
                else if (!instring.Substring(13, 1).Equals("#"))
                {
                    return -6; //找不到起始符
                }
                else if (!instring.Substring(instring.Length - 1, 1).Equals("#"))
                {
                    return -7; //找不到结束符
                }
                string ss = instring.Substring(10, 3);
                indentstart = ss.Substring(0, 1);
                indentend = ss.Substring(1, 1);
                indentdic = ss.Substring(2, 1);
            }
            catch
            {
                return -1; //意外错误
            }
            return 0;
        }

    }
}
