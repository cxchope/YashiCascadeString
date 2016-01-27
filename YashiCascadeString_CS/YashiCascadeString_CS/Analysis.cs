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
        YashiCascadeString_CS --analysis YCSDF10000[]:@[2::rootArray][2]1[3:A]AA[3:B]BB[3:C]CC[4::arr2][4]1[4]2[4]3[4]4[3:D]DD[2]2[3::][3]1[3]2[3]3[3]4[3::][3]1[3]2[3]3[3]4@

YCSDF10000[]:@
--[2]rootArray(Array):
--[2]1
---[3]A:AA
---[3]B:BB
---[3]C:CC
----[4]arr2(Array):
----[4]1
----[4]2
----[4]3
----[4]4
---[3]D:DD
--[2]2
---[3](Array):
---[3]1
---[3]2
---[3]3
---[3]4
---[3](Array):
---[3]1
---[3]2
---[3]3
---[3]4@
        */

        private Dictionary<string, Object> returnvalue;
        public string instring;
        public string indentstart = "[";
        public string indentend = "]";
        public string indentdic = ":";

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
                convert(instring.Substring(14, instring.Length - 15));
            }
            return returnvalue;
        }

        private void convert(string datastr)
        {
            Dictionary<string, Object> rootdic = new Dictionary<string, Object>();
            Console.WriteLine("convert=");
            string[] units = datastr.Split(indentstart.ToCharArray()[0]);
            int oldindent = 0;
            for (int i = 1; i < units.Length; i++)
            {
                string nowunit = units[i];
                Console.WriteLine(nowunit);
                int nowmode = 0; //当前字段位置
                int nowtype = 0; //0数组 1字典 2字符串
                int nowindent = 0; //缩进

                string nowkey = ""; //key
                string nowValue = ""; //value
                string[] colon = nowunit.Split(indentdic.ToCharArray()[0]);

                
                if (colon.Length == 2) //字典
                {
                    nowtype = 1;
                    nowindent = int.Parse(colon[0]); //缩进
                    string keyvaluestr = colon[1]; //.Split(indentend.ToCharArray()[0])
                    string[] keyvalue = keyvaluestr.Split(indentend.ToCharArray()[0]);
                    nowkey = keyvalue[0];
                    nowValue = keyvalue[1];
                    Console.WriteLine("字典 " + nowkey + " " + nowValue);
                }
                else if(colon.Length == 3) //数组声明
                {
                    nowtype = 0;
                    nowindent = int.Parse(colon[0]); //缩进
                    string v = colon[2];
                    nowkey = v.Substring(0, v.Length - 1);
                    Console.WriteLine("数组 " + nowkey);
                }
                else if (colon.Length == 1) //字符串
                {
                    nowtype = 2;
                    string[] indentvalue = nowunit.Split(indentend.ToCharArray()[0]);
                    nowindent = int.Parse(indentvalue[0]);
                    nowValue = indentvalue[1];
                    Console.WriteLine("字符串 " + nowValue);
                }
                else
                {
                    Console.WriteLine("YCSDF ERROR -7 (" + colon.Length + ").");
                }
            }
        }

        public int check()
        {
            try
            {
                if (instring.Length <= 17)
                {
                    return -2; //长度不正确
                }
                else if (!instring.Substring(0, 5).Equals("YCSDF"))
                {
                    return -3; //格式描述不匹配
                }
                else if (int.Parse(instring.Substring(5, 5)) != 10000)
                {
                    return -4; //版本号不匹配
                }
                else if (!instring.Substring(13, 1).Equals("@"))
                {
                    return -5; //找不到起始符
                }
                else if (!instring.Substring(instring.Length - 1, 1).Equals("@"))
                {
                    return -6; //找不到结束符
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
