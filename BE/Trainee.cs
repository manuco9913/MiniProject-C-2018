using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace BE
{
    public class Trainee: Person
    {
        public CarType CarTrained { get; set; }
        public GearType GearType { get; set; }
        public String DrivingSchool { get; set; }
        public Name Instructor { get; set; }
        public int LessonsNb { get; set; }  //new balance of lessons number

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
