using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        static public List<Tester> testerInitList()
        {
            return new List<Tester>
            {
            new Tester { _firstName = "Manu", _lastName = "Cohenca", _numId="12345678", _address = "havaad haleumi 21", _dateOfBirth = "13/06/1999", _experienceYears = 4, _gender="m", _maxDistanceFromHome="10 km", _maximumTestsPerWeek=5, _phoneNum="053-1234567", _whichCarUses="B" },
            new Tester { _firstName = "Aviad", _lastName = "Feig", _numId="23456789", _address = "havaad haleumi 22", _dateOfBirth = "13/06/1840", _experienceYears = 1, _gender="m", _maxDistanceFromHome="2 km", _maximumTestsPerWeek=1, _phoneNum="053-2345678", _whichCarUses="C" },
            new Tester { _firstName = "Giovanna", _lastName = "Rossi", _numId="87654321", _address = "ben yehuda 2", _dateOfBirth = "13/06/1399", _experienceYears = 5, _gender="f", _maxDistanceFromHome="45 km", _maximumTestsPerWeek=4, _phoneNum="051-2345678", _whichCarUses="A" }
            };
        }

        static public List<Trainee> traineeInitList()
        {
            return new List<Trainee>
            {
            new Trainee { _firstName = "As", _lastName = "Er", _numId="02345678", _address = "havaad 21", _dateOfBirth = "13/07/1999", _gender="m", _phoneNum="053-1234567", _whichCarUsed="B",_lessonsCount=5, _myTesterName="Manu", _schoolName="driveLessons", _typeOfGearbox="manual" },
            new Trainee { _firstName = "Angus", _lastName = "Brother", _numId="03456789", _address = "via alciati 22", _dateOfBirth = "13/07/1840", _gender="m", _phoneNum="053-2345678", _whichCarUsed="C",_lessonsCount=4, _myTesterName="Aviad", _schoolName="driverToBe", _typeOfGearbox="automatic" },
            new Trainee { _firstName = "Rosa", _lastName = "Rossi", _numId="07654321", _address = "ben yehuda 2", _dateOfBirth = "13/07/1399",  _gender="f", _phoneNum="051-2345678", _whichCarUsed="A" ,_lessonsCount=3, _myTesterName="Manu", _schoolName="driveLessons", _typeOfGearbox="manual"}
            };
        }

        static public List<Test> testInitList()
        {
            return new List<Test>
            {
                new Test {_testId="782" , _testerId="12345678", _traineeId="02345678" , _addressToStart="king george 4", _date="28/01/2019", _dateAndTime= "28/01/2019 13:00"},
                new Test {_testId="23" , _testerId="23456789", _traineeId="03456789" , _addressToStart="king george 4", _date="28/01/2019", _dateAndTime= "28/01/2019 13:00"},
                new Test {_testId="456" , _testerId="23456789", _traineeId="07654321" , _addressToStart="king george 4", _date="28/01/2019", _dateAndTime= "28/01/2019 13:00"}//here the tester doesnt match to the trainee
            };
        }
    }
}