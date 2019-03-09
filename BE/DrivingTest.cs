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
        private DateTime _date = new DateTime();
        private List<string> _requirements = new List<string>();
        //  private ArrayList _requirements = new ArrayList();
        public string ID { get; set; }
        public string Trainee_ID { get; set; }
        public string Tester_ID { get; set; }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public Address StartingPoint { get; set; }
        public bool Success { get; set; }
        public string Comment { get; set; }
        public bool[] Requirements { get; set; }

      
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
