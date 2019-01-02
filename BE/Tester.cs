using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester
    {
        public string _numId { get; set; }
        public string _lastName { get; set; }
        public string _firstName { get; set; }
        public string _dateOfBirth { get; set; }
        public Gender _gender { get; set; }
        public string _phoneNum { get; set; }
        public string _address { get; set; }
        public int _experienceYears { get; set; }
        public int _maximumTestsPerWeek { get; set; }
        public TypeOfVehicle _whichCarUses { get; set; }
        
        public string _maxDistanceFromHome { get; set; }
        //public string this[int x, int y]
        //{
        //    get { return this[x, y]; }
        //    set { this[x, y] = value; }
        //}
        public override string ToString()
        {
            return _lastName + _firstName;
        }

    }
}
