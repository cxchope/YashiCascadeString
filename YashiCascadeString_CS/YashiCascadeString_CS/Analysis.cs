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
        YashiCascadeString_CS --analysis YCSDFA0101AD,~#0,D,rootArray,~1#1,A,1,~2,2,~4,~5#5,A,1,2,3,4#4,A,1,2,3,4#2,D,A,AA,B,BB,C,CC,arr2,~3,D,DD#3,A,1,2,3,4#
        */

        private Dictionary<string, Object> returnvalue = new Dictionary<string, Object>();
        public string instring;
        public string indentObj = "#";
        public string indentUnit = ",";
        public string indentGoto = "~";
        public string indentArr = "A";
        public string indentDic = "D";

        private ArrayList tmpObjs = new ArrayList();

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
                    returnvalue = (Dictionary<string, Object>)convert(instring.Substring(15, instring.Length - 16));
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
            Console.WriteLine("datastr : "+ datastr);
            Dictionary<string, string[]> rootdic = new Dictionary<string, string[]>();
            string[] units = datastr.Split(indentObj.ToCharArray()[0]);
            for (int i = 0; i < units.Length; i++)
            {
                string obju = units[i];
                string[] objs = obju.Split(indentUnit.ToCharArray()[0]);
                string key = objs[0];
                rootdic.Add(key, objs);
            }
            return convert2(rootdic, "0");
        }

        private object convert2(Dictionary<string, string[]> rootdic, string uID)
        {
            string[] objs = rootdic[uID];
            string unitID = objs[0];
            string type = objs[1];
            if (type.Equals(indentDic))
            {
                string tmpKey = "";
                bool isKey = false;
                for (int i = 2; i < objs.Length; i++)
                {
                    isKey = !isKey;
                    if (isKey == true)
                    {
                        tmpKey = objs[i];
                    }
                    else
                    {
                        string tmpVal = objs[i];
                        //断点
                    }
                }
            }
            else if (type.Equals(indentArr))
            {
                for (int i = 2; i < objs.Length; i++)
                {
                    string nowobj = objs[i];
                    Console.WriteLine("nowobj : " + nowobj);
                }
            }

            
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
                else if (!instring.Substring(14, 1).Equals("#"))
                {
                    return -6; //找不到起始符
                }
                else if (!instring.Substring(instring.Length - 1, 1).Equals("#"))
                {
                    return -7; //找不到结束符
                }
                string ss = instring.Substring(10, 5);
                indentArr = ss.Substring(0, 1);
                Console.WriteLine("数组标识符 = " + indentArr);
                indentDic = ss.Substring(1, 1);
                Console.WriteLine("字典标识符 = " + indentDic);
                indentUnit = ss.Substring(2, 1);
                Console.WriteLine("对象分隔符 = " + indentUnit);
                indentGoto = ss.Substring(3, 1);
                Console.WriteLine("跳转符 = " + indentGoto);
                indentObj = ss.Substring(4, 1);
                Console.WriteLine("块分隔符 = " + indentObj);
            }
            catch
            {
                return -1; //意外错误
            }
            return 0;
        }

    }
}
