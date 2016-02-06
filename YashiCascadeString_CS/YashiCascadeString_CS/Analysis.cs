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
                    returnvalue = (Dictionary<string, Object>)convert(instring.Substring(14, instring.Length - 15));
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

        private object convert(string datastr)
        {
            
            Console.WriteLine("convert=");
            string[] units = datastr.Split(indentstart.ToCharArray()[0]);
            int oldindent = 0;
            for (int i = 1; i < units.Length; i++)
            {
                string nowunit = units[i]; //当前数据条目
                int nowtype = 0; //0数组 1字典 2字符串
                int nowindent = 0; //缩进
                int cindent = 0; //缩进差异

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
                    //Console.WriteLine("字典 " + nowkey + " " + nowValue);
                }
                else if (colon.Length == 3) //数组声明
                {
                    nowtype = 0;
                    nowindent = int.Parse(colon[0]); //缩进
                    string v = colon[2];
                    nowkey = v.Substring(0, v.Length - 1);
                    //Console.WriteLine("数组 " + nowkey);
                }
                else if (colon.Length == 1) //字符串
                {
                    nowtype = 2;
                    string[] indentvalue = nowunit.Split(indentend.ToCharArray()[0]);
                    nowindent = int.Parse(indentvalue[0]);
                    nowValue = indentvalue[1];
                    //Console.WriteLine("字符串 " + nowValue);
                }
                else
                {
                    Console.WriteLine("YCSDF ERROR -7 (" + colon.Length + ")."); //数据条目拆分失败
                }

                cindent = nowindent - oldindent;
                oldindent = nowindent;

                Console.WriteLine("当前数据=" + nowunit + ", 当前缩进=" + nowindent + ", 缩进差异=" + cindent + ", 数据类型=" + nowtype + ", 键=" + nowkey + ", 值=" + nowValue);
                Dictionary<string, object> nowLineDic = new Dictionary<string, object>() { { "nowunit", nowunit }, { "nowindent", nowindent }, { "cindent", cindent }, { "nowtype", nowtype }, { "nowkey", nowkey }, { "nowValue", nowValue } };
                tmpObjs.Add(nowLineDic);
            }
            Console.WriteLine("数据缓存完毕。\n");
            return convertthis(0, null, rootdic);
        }

        //nowunit：当前数据，nowindent：当前缩进，cindent：缩进差异，nowtype：数据类型，nowkey：键，nowValue：值
        private object convertthis(int nowLine, ArrayList addArr, Dictionary<string, Object> addDic)
        {

            //string nowunit, int nowindent, int cindent, int nowtype, string nowkey, string nowValue
            //获得当前行数据
            if (nowLine >= tmpObjs.Count)
            {
                Console.WriteLine("解析结束。");
                return null;
            }
            Dictionary<string, Object> nowLineDic = (Dictionary<string, Object>)tmpObjs[nowLine];
            string nowunit = (string)nowLineDic["nowunit"];
            int nowindent = (int)nowLineDic["nowindent"];
            int cindent = (int)nowLineDic["cindent"];
            int nowtype = (int)nowLineDic["nowtype"];
            string nowkey = (string)nowLineDic["nowkey"];
            string nowValue = (string)nowLineDic["nowValue"];
            Console.WriteLine(nowLine + " 当前数据=" + nowunit + ", 当前缩进=" + nowindent + ", 缩进差异=" + cindent + ", 数据类型=" + nowtype + ", 键=" + nowkey + ", 值=" + nowValue);

            //判断层级
            //if (cindent >= 0)
            //{

            //判断数据类型
                if (nowtype == 0) //如果是数组:创建数组
                {
                    Console.WriteLine("创建子数组...");
                    ArrayList newArray = new ArrayList();
                    //遍历内部
                    newArray = (ArrayList)convertthis(nowLine + 1, newArray, null);
                    //添加到此层级
                    if (addDic != null) // && nowkey != null && nowkey.Length > 0
                    {
                        addDic.Add(nowkey, newArray); Console.WriteLine("创建子数组于字典中。");
                        return addDic;
                    }
                    else if (addArr != null) // && nowValue != null && nowValue.Length > 0
                    {
                        addArr.Add(newArray); Console.WriteLine("创建子数组于数组中。");
                        return addArr;
                    }
                }
                if (nowtype == 1) //如果是字典:创建字典
                {
                    Console.WriteLine("创建子字典...");
                    Dictionary<string, Object> newDic = new Dictionary<string, Object>();
                    //遍历内部
                    newDic = (Dictionary<string, Object>)convertthis(nowLine + 1, null, newDic);
                    //添加到此层级
                    if (addDic != null)
                    {
                        addDic.Add(nowkey, newDic); Console.WriteLine("创建子字典于字典中。");
                        return addDic;
                    }
                    else if (addArr != null)
                    {
                        addArr.Add(newDic); Console.WriteLine("创建子字典于数组中。");
                        return addArr;
                    }
                }
                else if (nowtype == 2) //如果是字符串:添加字符串
                {
                    //添加到此层级
                    Console.WriteLine("创建字符串...");
                    if (addDic != null)
                    {
                        Console.WriteLine("创建字符串到字典中（" + nowkey + "，" + nowValue + "）。");
                        addDic.Add(nowkey, nowValue);
                        return addDic;
                    }
                    else if (addArr != null)
                    {
                        Console.WriteLine("创建字符串到数组中（" + nowValue + "）。");
                        addArr.Add(nowValue);
                        return addArr;
                    }
                    convertthis(nowLine + 1, null, null);
                }

            //}
            //else
            //{
            //    convertthis(nowLine + 1, null, rootdic);
            //}
            return null;
        }


        private void convertthis0(string nowunit, int nowindent, int cindent, int nowtype, string nowkey, string nowValue)
        {
            /*
            int nowindentc = nowindent - nowObjs.Count; //计算要填充未知的量
            if (nowindentc > 0) //如果请求位数数组长度不够
            {
                //Console.WriteLine("填充未知级别 (" + nowindentc + ").");
                for (int nowindentci = 0; nowindentci < nowindentc; nowindentci++) //填充未知的量
                {
                    nowObjs.Add(new object());
                }
            }
            if (cindent < 0)
            {
                int nowindentAbs = Math.Abs(nowindent); //层级变化量绝对值:要向上级提交的次数
                for (int nowindentAbsi = 0; nowindentAbsi < nowindentAbs; nowindentAbsi++) //向上级提交
                {
                    //获得上级
                    int nowObjsn = nowindent - 1 - nowindentAbsi;
                    object nowObj = nowObjs[nowObjsn];
                    Type nowType = nowObj.GetType();
                    if (nowType == typeof(ArrayList))
                    {
                        ArrayList nowObjArr = (ArrayList)nowObj;
                    }
                    else if (nowType == typeof(Dictionary<string, Object>))
                    {
                        Dictionary<string, Object> nowObjDic = (Dictionary<string, Object>)nowObj;
                    }
                }
            }
            else if(cindent > 0)
            {

            }
            else
            {

            }
            */



            /*
            if (nowindent != 0) //如果层级有变化
            {
                int abs = Math.Abs(nowindent); //层级变化量绝对值
                abs--; //层级变化量绝对值-1
                if (nowindent < 0)
                {
                    nowindent = 0 - abs;
                    //return回上一级
                }
                else
                {
                    nowindent = abs;
                    //new出下一级并添加数据
                }
            }
            else
            {
                //当前级添加数据
            }
            */
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
