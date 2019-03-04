using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Schedule
    {
        public bool[][] Data { get; set; } = new bool[5][];

        //todo: a toString() that's easy also to translate from XML to Schedule class type
        public override string ToString()
        {
            int starttime = 9;
            bool oved = false;
            string result = null;
            string hayom = null;
            for (int i = 0; i < 5; i++)
            {
                oved = false;
                hayom = null;
                //result += ((Day)i).ToString() + "\n";
                for (int j = 0; j < 6; j++)
                {
                    if (Data[i][j] == true)
                    {
                        oved = true;
                        hayom += "\t" + (starttime + j) + ":00-";
                        hayom += (starttime + j + 1) + ":00\n";
                    }
                }
                if (oved == true)
                {
                    result += ((Day)i).ToString() + "\n";
                    result += hayom;
                }
            }
            return result;//.Substring(0, result.Length - 1);
        }
    }
}
