using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        public string _numId { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public Gender _gender { get; set; }
        public string _phoneNum { get; set; }
        public string _address { get; set; }
        public string _dateOfBirth { get; set; }
        public TypeOfVehicle _whichCarUsed { get; set; } // The type of vehicle he studied
        public GearBoxType _typeOfGearbox { get; set; }
        public SchoolName _schoolName { get; set; }
        public string _myTesterName { get; set; }
        public int _lessonsCount { get; set; }
        /* more prop as needed
        {

        }
        */
        public override string ToString() //need change this ToString
        {
            return _lastName + _firstName;
        }


    }
}
