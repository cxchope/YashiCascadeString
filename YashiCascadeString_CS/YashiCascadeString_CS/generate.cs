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
        public object indata;
        public string indentObj = "#";
        public string indentUnit = ",";
        public string indentGoto = "~";
        public string indentArr = "A";
        public string indentDic = "D";
        public bool debug = false;
        private int iID = 0;

        public string start()
        {
            if (debug == true) Console.WriteLine("\n开始创建...");
            iID = 0;
            convert(indata);
            returnvalue = pre() + returnvalue;
            if (debug == true) Console.WriteLine("创建完成: " + returnvalue);
            return returnvalue;
        }
        
        private string convert(object uObj)
        {
            int uID = iID;
            string uStr = uID.ToString();
            if (uObj is ArrayList)
            {
                uStr = uStr + indentUnit + indentArr;
                if (debug == true) Console.WriteLine("输出数组 " + uStr + " ...");
                ArrayList objArray = (ArrayList)uObj;
                for (int i = 0; i < objArray.Count; i++)
                {
                    object tObj = objArray[i];
                    if (tObj is string)
                    {
                        if (debug == true) Console.WriteLine("将字符串变量 " + (string)tObj + " 添加到数组...");
                        uStr = uStr + indentUnit + (string)tObj;
                    }
                    else
                    {
                        if (debug == true) Console.WriteLine("将对象块 " + iID + " 添加到数组...");
                        iID++;
                        uStr = uStr + indentUnit + indentGoto + iID;
                        convert(tObj);
                    }
                }
            }
            else if (uObj is Dictionary<string, Object>)
            {
                uStr = uStr + indentUnit + indentDic;
                if (debug == true) Console.WriteLine("输出字典 " + uStr + " ...");
                Dictionary<string, Object> objDictionary = (Dictionary<string, Object>)uObj;
                string[] objKey = objDictionary.Keys.ToArray();
                for (int i = 0; i < objKey.Length; i++)
                {
                    string nowKey = objKey[i];
                    object tObj = objDictionary[nowKey];
                    if (tObj is string)
                    {
                        if (debug == true) Console.WriteLine("将Key " + (string)tObj + " 添加到字典...");
                        uStr = uStr + indentUnit + nowKey + indentUnit + (string)tObj;
                    }
                    else
                    {
                        if (debug == true) Console.WriteLine("将对象块 " + iID + " 添加到数组...");
                        iID++;
                        uStr = uStr + indentUnit + nowKey + indentUnit + indentGoto + iID;
                        convert(tObj);
                    }
                }
            }
            else
            {
                Console.WriteLine("错误：输入数据类型错误");
            }
            uStr = uStr + indentObj;
            returnvalue = uStr + returnvalue;
            return null;
        }

        //创建头
        private string pre()
        {
            //5位格式描述代码，1位变体代码，2位HEX版本代码，2位HEX模式代码，1位数组标记，1位字典标记，1位缩进起始标记，1位缩进结束标记，1位区分标记
            return "YCSDFA0101" + indentArr + indentDic + indentUnit + indentGoto + indentObj;
        }
    }
}
