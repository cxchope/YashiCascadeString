using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace YashiCascadeString_CS
{
    class Generate
    {
        private string returnvalue;
        public Dictionary<string, Object> indic;
        public string indentstart = "[";
        public string indentend = "]";
        public string indentdic = ":";
        public bool formatoutput = false;

        int indent = 0; //缩进级别

        public string start()
        {
            returnvalue = pre(); //创建头
            string[] keys = indic.Keys.ToArray();
            indent = 0; //缩进级别
            convert(indic, "main");
            return returnvalue + "@";
        }
        
        private void convert(Object cObj, string dkey)
        {
            Type nowType = cObj.GetType();
            if (nowType == typeof(ArrayList))
            {
                indent++;
                ArrayList nowArr = (ArrayList)cObj;
                for (int j = 0; j < nowArr.Count; j++)
                {
                    if (j == 0)
                    {
                        if (formatoutput)
                        {
                            string ns = dkey;
                            returnvalue += format(ns+"(Array):", null);
                        }
                        else
                        {
                            returnvalue = returnvalue + indentstart + indent + indentdic + indentdic + dkey + indentend;
                        }
                    }
                    Object childObj = nowArr[j];
                    convert(childObj, null);
                }
                indent--;
            }
            else if (nowType == typeof(Dictionary<string, Object>))
            {
                indent++;
                Dictionary<string, Object> nowDic = (Dictionary<string, Object>)cObj;
                string[] nowDicKeys = nowDic.Keys.ToArray();
                for (int j = 0; j < nowDicKeys.Length; j++)
                {
                    string childKey = nowDicKeys[j];
                    Object childObj = nowDic[childKey];
                    convert(childObj, childKey);
                }
                indent--;
            }
            else if (nowType == typeof(string))
            {
                string nowStr = (string)cObj;
                if (formatoutput)
                {
                    returnvalue += format(nowStr, dkey);
                }
                else
                {
                    if (dkey == null)
                    {
                        returnvalue = returnvalue + indentstart + indent + indentend + nowStr;
                    }
                    else
                    {
                        returnvalue = returnvalue + indentstart + indent + indentdic + dkey + indentend + nowStr;
                    }
                }
            }
            else
            {
                Console.WriteLine("输入数据类型不正确。不支持 " + nowType.Name + " | " + (int)cObj);
                //Console.WriteLine("Convert Error: Unkown Type.\nPlease using string/Dictionary/ArrayList type.");
            }
        }

        //创建头
        private string pre()
        {
            //5位格式代码，5位版本代码，1位缩进起始标记，1位缩进结束标记，1位区分标记
            return "YCSDF10000" + indentstart + indentend + indentdic + "@";
        }

        private string format(string nowStr, string dkey)
        {
            //return returnvalue + indentstart + indent + indentend + nowStr;
            string returnvalue = "\n";
            for (int i = 0; i < indent; i++)
            {
                returnvalue += "-";
            }
            if (dkey == null)
            {
                returnvalue = returnvalue + indentstart + indent + indentend + nowStr;
            }
            else
            {
                returnvalue = returnvalue + indentstart + indent + indentend + dkey + indentdic + nowStr;
            }
            return returnvalue;
        }
    }
}
