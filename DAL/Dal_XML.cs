using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

namespace DAL
{
    internal class Dal_XML
    {

        private void initTester()
        {
            AddTester(new Tester
            {
                ID = "1111",
                Name = new Name { FirstName = "jojo", LastName = "chalass" },
                Address = new Address
                {
                    City = "Jerusalem",
                    Number = 21,
                    StreetName = "havvad haleumi",
                    //                  ZipCode = 91160
                },
                DayOfBirth = DateTime.Now.AddYears(-45),
                Gender = Gender.MALE,
                Experience = 10,
                Expertise = CarType.Truck_Heavy,
                MaxDistance = 2,
                // MaxTestWeekly = 1,
                Schedule = new Schedule
                {
                    Data = new bool[5][]
                    {
                        new bool[6] { false, false, true, false, false, false},
                        new bool[6] { false, false, false, false, false, false},
                        new bool[6] { false, false, false, false, false, false},
                        new bool[6] { false, false, true, false, false, false},
                        new bool[6] { false, false, false, false, false, false}
                    }
                }
            });
        }

        public Dal_XML()
        {
            //initTester();
        }

        public bool AddDrivingTest(DrivingTest drivingTest)
        {
      //      DS.DataSourceXML.DrivingTests.Add(drivingTest.ToXML());
            DS.DataSourceXML.SaveDrivingtests();
            return true;
        }

        public bool AddTester(Tester tester)
        {
            string str = tester.ToXMLstring();
            XElement xml = XElement.Parse(str);
            DS.DataSourceXML.Testers.Add(xml);
            DS.DataSourceXML.SaveTesters();
            return true;
        }

        public bool AddTrainee(Trainee trainee)
        {
            string str = trainee.ToXMLstring();
            XElement xml = XElement.Parse(str);
            DS.DataSourceXML.Trainees.Add(xml);
            DS.DataSourceXML.SaveTrainees();
            return true;
        }

        public List<DrivingTest> GetDrivingTests()
        {
            var serializer = new XmlSerializer(typeof(DrivingTest));

            var elements = DS.DataSourceXML.Testers.Elements("DrivingTest");
            return elements.Select(element => (DrivingTest)serializer.Deserialize(element.CreateReader())).ToList();
        }

        public List<Tester> GetTesters()
        {
            var serializer = new XmlSerializer(typeof(Tester));

            var elements = DS.DataSourceXML.Testers.Elements("Tester");
            return elements.Select(element => (Tester)serializer.Deserialize(element.CreateReader())).ToList();
        }

        public List<Trainee> GetTrainees(Func<Trainee, bool> p)
        {
            var result = from t in DS.DataSourceXML.Trainees.Elements()
                         select new Trainee
                         {
                             ID = t.Element("ID").Value,
                             Name = new Name
                             {
                                 FirstName = t.Element("Name").Element("FirtsName").Value,
                                 LastName = t.Element("Name").Element("LastName").Value
                             }
                             //.....

                         };
            return (from tr in result
                    where p(tr)
                    select tr).ToList();
        }

        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTester(Tester tester)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDrivingTest(DrivingTest drivingTest)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTester(Tester tester)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }
    }
}
