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
        XElement testersRoot;
        XElement traineesRoot;
        XElement testsRoot;

        string testersPath = @"\XML Files";
        string traineesPath = @"\XML Files";
        string testsPath = @"\XML Files";

        private void CreateTestersFile()
        {
            testersRoot = new XElement("Testers");
            testersRoot.Save(testersPath);
        }
        private void SaveTestersList()
        {
            List<Tester> testersList = DS.DataSource.TestersList;

            testersRoot = new XElement("Testers", from tester in testersList
                                                  select new XElement("Tester",
                                                                new XElement("Id", tester.ID),
                                                                new XElement("Name",
                                                                        new XElement("First Name", tester.Name.FirstName),
                                                                        new XElement("Last Name", tester.Name.LastName)),
                                                                new XElement("Day Of Birth", tester.DayOfBirth.ToString()),
                                                                new XElement("Gender", tester.Gender.ToString()),
                                                                new XElement("Address",
                                                                        new XElement("Street Name", tester.Address.StreetName),
                                                                        new XElement("Number", tester.Address.Number.ToString()),
                                                                        new XElement("City", tester.Address.City)),
                                                                //todo: schedule?
                                                                new XElement("Expertise",tester.Expertise.ToString()),
                                                                new XElement("Experience",tester.Experience.ToString()),
                                                                new XElement("Max Test Weekly",tester.MaxTestWeekly.ToString()),
                                                                new XElement("Max Distance", tester.MaxDistance)
                                                                ));

            testersRoot.Save(testersPath);
        }
        private void LoadTestersFile()
        {
            testersRoot = XElement.Load(testersPath);
        }
        private void LoadTestersList()
        {
            List<Tester> testersList = new List<Tester>();

            testersList = (from tester in testersRoot.Elements()
                           select new Tester()
                           {
                               ID = tester.Element("Id").Value,
                               Name = {
                                        FirstName = tester.Element("Name").Element("First Name").Value,
                                        LastName = tester.Element("Name").Element("Last Name").Value
                                      },
                               DayOfBirth = Convert.ToDateTime(tester.Element("Day Of Birth").Value),
                               Gender = (Gender)Enum.Parse(typeof(Gender), tester.Element("Gender").Value),
                               Address = {
                                            StreetName = tester.Element("Address").Element("Street Name").Value,
                                            Number = Convert.ToInt32(tester.Element("Address").Element("Number").Value),
                                            City = tester.Element("Address").Element("City").Value
                                         },
                               Expertise = (CarType)Enum.Parse(typeof(CarType), tester.Element("Expertise").Value),
                               Experience = Convert.ToInt32(tester.Element("Experience").Value),
                               MaxTestWeekly = Convert.ToInt32(tester.Element("Max Test Weekly").Value),
                               MaxDistance = Convert.ToInt32(tester.Element("Max Distance").Value)
                               //todo: schedule
                           }).ToList();

            DS.DataSource.TestersList = testersList;
        }

        private void CreateTraineesFile()
        {
            traineesRoot = new XElement("Trainess");
            traineesRoot.Save(traineesPath);
        }
        private void SaveTraineesList()
        {
            List<Trainee> traineesList = DS.DataSource.TraineesList;

            traineesRoot = new XElement("Trainees", from trainee in traineesList
                                                  select new XElement("Trainee",
                                                                new XElement("Id", trainee.ID),
                                                                new XElement("Name",
                                                                        new XElement("First Name", trainee.Name.FirstName),
                                                                        new XElement("Last Name", trainee.Name.LastName)),
                                                                new XElement("Day Of Birth", trainee.DayOfBirth.ToString()),
                                                                new XElement("Gender",trainee.Gender.ToString()),
                                                                new XElement("Address",
                                                                        new XElement("Street Name", trainee.Address.StreetName),
                                                                        new XElement("Number",trainee.Address.Number.ToString()),
                                                                        new XElement("City",trainee.Address.City)),
                                                                new XElement("Car Trained",trainee.CarTrained.ToString()),
                                                                new XElement("Gear Type",trainee.GearType.ToString()),
                                                                new XElement("School Name", trainee.DrivingSchool.ToString()),
                                                                new XElement("Instructor",trainee.Instructor),
                                                                new XElement("Lessons Number",trainee.LessonsNb.ToString()),
                                                                new XElement("Success",trainee.Succsess.ToString())
                                                                ));

            traineesRoot.Save(traineesPath);
        }
        private void LoadTraineesFile()
        {
            traineesRoot = XElement.Load(traineesPath);
        }
        private void LoadTraineesList()
        {
            List<Trainee> traineesList = new List<Trainee>();

            traineesList = (from trainee in traineesRoot.Elements()
                            select new Trainee()
                            {
                                ID = trainee.Element("Id").Value,
                                Name =
                                {
                                    FirstName=trainee.Element("Name").Element("First Name").Value,
                                    LastName=trainee.Element("Name").Element("First Name").Value
                                },
                                DayOfBirth = Convert.ToDateTime(trainee.Element("Day Of Birth").Value),
                                Gender = (Gender)Enum.Parse(typeof(Gender), trainee.Element("Gender").Value),
                                Address = {
                                              StreetName = trainee.Element("Address").Element("Street Name").Value,
                                              Number = Convert.ToInt32(trainee.Element("Address").Element("Number").Value),
                                              City = trainee.Element("Address").Element("City").Value
                                          },
                                CarTrained = (CarType)Enum.Parse(typeof(CarType), trainee.Element("Car Trained").Value),
                                GearType = (GearType)Enum.Parse(typeof(GearType), trainee.Element("Gear Type").Value),
                                DrivingSchool = (SchoolName)Enum.Parse(typeof(SchoolName), trainee.Element("School Name").Value),
                                Instructor = trainee.Element("Instructor").Value,
                                LessonsNb = Convert.ToInt32(trainee.Element("Lessons Number").Value),
                                Succsess = Convert.ToBoolean(trainee.Element("Success").Value)
                            }).ToList();
            DS.DataSource.TraineesList = traineesList;
        }


        private void CreateTestsFile()
        {
            testsRoot = new XElement("Tests");
            testsRoot.Save(testersPath);
        }
        private void SaveTestsList()
        {
            List<DrivingTest> testsList = DS.DataSource.DrivingtestsList;

            testsRoot = new XElement("Tests", from test in testsList
                                              select new XElement("Trainee",
                                                            new XElement("Id",test.ID),
                                                            new XElement("Trainee Id",test.Trainee_ID),
                                                            new XElement("Tester Id",test.Tester_ID),
                                                            new XElement("Date",test.Date.ToString()),
                                                            new XElement("Time", test.Time.ToString()), //new XElement("Time", test.Date.TimeOfDay.ToString()),???????????????
                                                            new XElement("Starting Point",
                                                                    new XElement("Street Name", test.StartingPoint.StreetName),
                                                                    new XElement("Number",test.StartingPoint.Number.ToString()),
                                                                    new XElement("City", test.StartingPoint.City)),
                                                            new XElement("Success",test.Success.ToString()),
                                                            new XElement("Comment",test.Comment)
                                                            ));
            testsRoot.Save(testsPath);
        }
        private void LoadTestsFile()
        {
            testsRoot = XElement.Load(testersPath);
        }
        private void LoadTestsList()
        {
            List<DrivingTest> testsList = new List<DrivingTest>();

            testsList = (from test in testsRoot.Elements()
                         select new DrivingTest()
                         {
                             ID = test.Element("Id").Value,
                             Trainee_ID = test.Element("Trainee Id").Value,
                             Tester_ID = test.Element("Tester Id").Value,
                             Date = Convert.ToDateTime(test.Element("Date").Value),
                             Time = TimeSpan.Parse(test.Element("Time").Value),
                             StartingPoint = {
                                                StreetName = test.Element("Starting Point").Element("Street Name").Value,
                                                Number =Convert.ToInt32(test.Element("Starting Point").Element("Number").Value),
                                                City = test.Element("Starting Point").Element("City").Value
                                             },
                             Success = Convert.ToBoolean(test.Element("Success").Value),
                             Comment = test.Element("Comment").Value
                         }).ToList();

            DS.DataSource.DrivingtestsList = testsList;
        }

        private void LoadLists()
        {
            LoadTestersList();
            LoadTraineesList();
            LoadTestsList();
        }
        private void LoadFiles()
        {
            if (!File.Exists(testersPath))
                CreateTestersFile();
            else
                LoadTestersFile();

            if (!File.Exists(traineesPath))
                CreateTraineesFile();
            else
                LoadTraineesFile();

            if (!File.Exists(testsPath))
                CreateTestsFile();
            else
                LoadTestsFile();
        }

        //private void initTester()
        //{
        //    AddTester(new Tester
        //    {
        //        ID = "1111",
        //        Name = new Name { FirstName = "jojo", LastName = "chalass" },
        //        Address = new Address
        //        {
        //            City = "Jerusalem",
        //            Number = 21,
        //            StreetName = "havvad haleumi",
        //            //                  ZipCode = 91160
        //        },
        //        DayOfBirth = DateTime.Now.AddYears(-45),
        //        Gender = Gender.MALE,
        //        Experience = 10,
        //        Expertise = CarType.Truck_Heavy,
        //        MaxDistance = 2,
        //        // MaxTestWeekly = 1,
        //        Schedule = new Schedule
        //        {
        //            Data = new bool[5][]
        //            {
        //                new bool[6] { false, false, true, false, false, false},
        //                new bool[6] { false, false, false, false, false, false},
        //                new bool[6] { false, false, false, false, false, false},
        //                new bool[6] { false, false, true, false, false, false},
        //                new bool[6] { false, false, false, false, false, false}
        //            }
        //        }
        //    });
        //}

        public Dal_XML()
        {
            //initTester();
            LoadFiles();
        }
        
        public bool AddTester(Tester tester)
        {
            DS.DataSource.TestersList.Add(tester);
            SaveTestersList();
            return true;
        }
        public bool AddTrainee(Trainee trainee)
        {
            DS.DataSource.TraineesList.Add(trainee);
            SaveTraineesList();
            return true;
        }
        public bool AddDrivingTest(DrivingTest drivingTest)
        {
            DS.DataSource.DrivingtestsList.Add(drivingTest);
            SaveTestsList();
            return true;
        }

        //todo: i think we have to finish with all the functions by simply copyinng them and adding save/load List accordingly










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
