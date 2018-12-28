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
        public string _gender { get; set; }
        public string _phoneNum { get; set; }
        public string _address { get; set; }
        public string _dateOfBirth { get; set; }
        public string _whichCarUsed { get; set; } // The type of vehicle he studied
        public string _typeOfGearbox { get; set; }
        public string _schoolName { get; set; }
        public string _myTesterName { get; set; }
        public string _countDrivingLessons { get; set; }
        // more Additional features as needed
        //-----------------------------------
        // doing ovverload for ToString


    }
}
