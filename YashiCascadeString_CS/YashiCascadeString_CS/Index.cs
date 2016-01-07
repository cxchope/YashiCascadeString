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
                    string r = g.start();
                    Console.WriteLine(r);
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
                returnValue += nowI;
            }
            return returnValue;
        }

        
    }
}
