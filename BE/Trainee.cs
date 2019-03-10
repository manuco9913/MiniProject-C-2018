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
        public SchoolName DrivingSchool { get; set; }
        public int LessonsNb { get; set; }
        public bool Succsess { get; set; }// Past or Failed test
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
