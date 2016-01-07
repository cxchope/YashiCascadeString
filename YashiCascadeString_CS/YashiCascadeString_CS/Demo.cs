using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace YashiCascadeString_CS
{
    class Demo
    {
        public Dictionary<string, Object> test1()
        {
            Dictionary<string, Object> returnvalue = new Dictionary<string, Object>();
            ArrayList arr2 = new ArrayList();
            arr2.Add("1");
            arr2.Add("2");
            arr2.Add("3");
            arr2.Add("4");
            Dictionary<string, Object> dic2 = new Dictionary<string, Object>();
            dic2.Add("A", "AA");
            dic2.Add("B", "BB");
            dic2.Add("C", "CC");
            dic2.Add("arr2", arr2);
            dic2.Add("D", "DD");
            ArrayList arr1 = new ArrayList();
            arr1.Add("1");
            arr1.Add(dic2);
            arr1.Add("2");
            arr1.Add(arr2);
            arr1.Add(arr2);
            returnvalue.Add("rootArray", arr1);
            return returnvalue;
        }
    }
}
