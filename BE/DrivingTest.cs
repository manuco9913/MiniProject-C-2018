using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace BE
{
    /// <summary>
    /// טסט
    /// </summary>
    public class DrivingTest
    {
        //
        public TestProp requirments { get; set; } // what do with this ?
        
        //
        private DateTime _date;
        // private List<string> _requirements = new List<string>();
        //  private ArrayList _requirements = new ArrayList();
        /// <summary>
        /// get set the Trainee ID
        /// </summary>
        public string ID { get; set; }
        public String Trainee_ID { get; set; }
        /// <summary>
        /// get set the Tester ID
        /// </summary>
        public String Tester_ID { get; set; }

        public DateTime Date { get => _date.Date; set => _date = value.Date; }
        public TimeSpan Time { get => Date.TimeOfDay; set => _date.AddMilliseconds(value.TotalMilliseconds); }
        public Address StartingPoint { get; set; }
       // public List<String> Requirements { get => _requirements; set => _requirements = value; }
        public bool Success { get; set; }
        public String Comment { get; set; }

      
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
