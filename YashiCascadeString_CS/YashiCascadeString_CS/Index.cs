using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace YashiCascadeString_CS
{
    class Index
    {
        public string[] args = null;
        private string mode = "";
        private string val = "";
        private int testi = 0;

        //YashiCascadeString_CS --analysis YCSDF10000[]:@[2::rootArray][2]1[3:A]AA[3:B]BB[3:C]CC[4::arr2][4]1[4]2[4]3[4]4[3:D]DD[2]2[3::][3]1[3]2[3]3[3]4[3::][3]1[3]2[3]3[3]4@

        public void start()
        {
            String name = "Yashi Cascade String Data Format";
            if (args.Length > 0)
            {
                Console.Clear();
                mode = args[0];
                val = MergeParameter();
                Console.Title = name + " " + mode + " " + val;
                Console.WriteLine(Console.Title);
                if (args[0] == "--generate") //Array->String
                {
                    Generate g = new Generate();
                    Demo d = new Demo();
                    g.indata = d.test1();
                    object returnValue = g.start();
                    Console.WriteLine(returnValue);
                }
                else if (args[0] == "--analysis") //String->Array
                {
                    Analysis a = new Analysis();
                    a.instring = val;
                    a.debug = true;
                    object returnValue = a.start();

                    Generate g = new Generate();
                    g.indata = returnValue;
                    g.debug = true;
                    string returnValue2 = (string)g.start();
                }
                else if (args[0] == "--test")
                {
                    Demo d = new Demo();
                    object obj = d.test1();
                    for (int i = 0; i < 100; i++)
                    {
                        obj = test(obj);
                    }
                }
                else
                {
                    Console.WriteLine(this.GetType().Assembly.Location);
                }
            }
        }

        private object test(object obj)
        {
            Generate g = new Generate();
            g.indata = obj;
            string returnValue2 = (string)g.start();
            Console.WriteLine(returnValue2);

            Analysis a = new Analysis();
            a.instring = returnValue2;
            return a.start();
        }

        private string MergeParameter()
        {
            string returnValue = "";
            for (int i = 1; i < args.Length; i++)
            {
                string nowI = args[i];
                if (i == 1)
                {
                    returnValue += nowI;
                }
                else
                {
                    returnValue = returnValue + " " + nowI;
                }
            }
            return returnValue;
        }

        
    }
}
