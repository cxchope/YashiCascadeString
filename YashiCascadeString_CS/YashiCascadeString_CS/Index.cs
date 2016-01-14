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

        //YashiCascadeString_CS --analysis YCSDF10000[]:@[2::rootArray][2]1[3:A]AA[3:B]BB[3:C]CC[4::arr2][4]1[4]2[4]3[4]4[3:D]DD[2]2[3::][3]1[3]2[3]3[3]4[3::][3]1[3]2[3]3[3]4@

        public void start()
        {
            String name = "Yashi Cascade String Data Format";
            if (args.Length > 1)
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
                    g.indic = d.test1();
                    if (args[1] == "1")
                    {
                        g.formatoutput = true;
                    }
                    Console.WriteLine(g.start());
                }
                else if (args[0] == "--analysis") //String->Array
                {
                    Analysis g = new Analysis();
                    g.instring = val;
                    Dictionary<string,Object> r = g.start();
                    Console.WriteLine(r);
                }
            }
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
