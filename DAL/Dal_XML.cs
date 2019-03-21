using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DS;

namespace DAL
{
    internal class Dal_XML : Idal
    {
        XElement testersRoot;
        XElement traineesRoot;
        XElement testsRoot;

        string testersPath = @"testers.xml";
        string traineesPath = @"trainee.xml";
        string testsPath = @"tests.xml";

        private List<XElement> ScheduleElement(Tester tester)
        {
            List<XElement> DayList = new List<XElement>();
            for (int i = 0; i < 5; i++)
            {
                string dayString = "";
                for (int j = 0; j < 6; j++)
                    dayString += tester.Schedule.Data[i][j] + ",";
                DayList.Add(new XElement("day" + i.ToString(), dayString.Substring(0, dayString.Length - 1)));
            }
            return DayList;
        }
        private Schedule ElementToSchedule(XElement sched)
        {
            var array = sched.Elements();
            Schedule toReturn = new Schedule();
            foreach (var i in array)
            {
                var s = i.Value.Split(',');
                for (int j = 0; j < 6; j++)
                {
                    int x = int.Parse(i.Name.ToString().Substring(3));
                    bool b = bool.Parse(s[j]);
                    toReturn.Data[x][j] = b;
                }
            }
            return toReturn;
        }
        private XElement requirementsElement(DrivingTest test)
        {
            string reqsString = "";
            for (int i = 0; i < 9; i++)
            {
                reqsString += test.Requirements[i] + ",";
            }
            return new XElement("Requirements", reqsString);

        }
        private bool[] ElementToRequirements(XElement req)
        {
            bool[] toReturn = new bool[9];
            string[] str = req.Value.Split(',');
            var elems = str;
            for (int i = 0; i < 9; i++)
            {
                bool b = Convert.ToBoolean(elems[i]);
                toReturn[i] = b;
            }
            return toReturn;
        }

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
                                                                        new XElement("FirstName", tester.Name.FirstName),
                                                                        new XElement("LastName", tester.Name.LastName)),
                                                                new XElement("DayOfBirth", tester.DayOfBirth.ToString()),
                                                                new XElement("Gender", tester.Gender.ToString()),
                                                                new XElement("Address",
                                                                        new XElement("StreetName", tester.Address.StreetName),
                                                                        new XElement("Number", tester.Address.Number.ToString()),
                                                                        new XElement("City", tester.Address.City)),
                                                                new XElement("Schedule", ScheduleElement(tester)),
                                                                new XElement("Expertise",tester.Expertise.ToString()),
                                                                new XElement("Experience",tester.Experience.ToString()),
                                                                new XElement("MaxTestWeekly",tester.MaxTestWeekly.ToString()),
                                                                new XElement("MaxDistance", tester.MaxDistance)
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

                               Name = new Name() {
                                   FirstName = tester.Element("Name").Element("FirstName").Value,
                                   LastName = tester.Element("Name").Element("LastName").Value
                               },
                               DayOfBirth = Convert.ToDateTime(tester.Element("DayOfBirth").Value),
                               Gender = (Gender)Enum.Parse(typeof(Gender), tester.Element("Gender").Value),
                               Address = new Address()
                               {
                                   StreetName = tester.Element("Address").Element("StreetName").Value,
                                   Number = Convert.ToInt32(tester.Element("Address").Element("Number").Value),
                                   City = tester.Element("Address").Element("City").Value
                               },
                               Expertise = (CarType)Enum.Parse(typeof(CarType), tester.Element("Expertise").Value),
                               Experience = Convert.ToInt32(tester.Element("Experience").Value),
                               MaxTestWeekly = Convert.ToInt32(tester.Element("MaxTestWeekly").Value),
                               MaxDistance = Convert.ToInt32(tester.Element("MaxDistance").Value),
                               Schedule = ElementToSchedule(tester.Element("Schedule"))

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
                                                                        new XElement("FirstName", trainee.Name.FirstName),
                                                                        new XElement("LastName", trainee.Name.LastName)),
                                                                new XElement("DayOfBirth", trainee.DayOfBirth.ToString()),
                                                                new XElement("Gender",trainee.Gender.ToString()),
                                                                new XElement("Address",
                                                                        new XElement("StreetName", trainee.Address.StreetName),
                                                                        new XElement("Number",trainee.Address.Number.ToString()),
                                                                        new XElement("City",trainee.Address.City)),
                                                                new XElement("CarTrained",trainee.CarTrained.ToString()),
                                                                new XElement("GearType",trainee.GearType.ToString()),
                                                                new XElement("SchoolName", trainee.DrivingSchool.ToString()),
                                                                new XElement("LessonsNumber",trainee.LessonsNb.ToString()),
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
                                Name = new Name()
                                {
                                    FirstName=trainee.Element("Name").Element("FirstName").Value,
                                    LastName=trainee.Element("Name").Element("LastName").Value
                                },
                                DayOfBirth = Convert.ToDateTime(trainee.Element("DayOfBirth").Value),
                                Gender = (Gender)Enum.Parse(typeof(Gender), trainee.Element("Gender").Value),
                                Address = new Address() {
                                              StreetName = trainee.Element("Address").Element("StreetName").Value,
                                              Number = Convert.ToInt32(trainee.Element("Address").Element("Number").Value),
                                              City = trainee.Element("Address").Element("City").Value
                                          },
                                CarTrained = (CarType)Enum.Parse(typeof(CarType), trainee.Element("CarTrained").Value),
                                GearType = (GearType)Enum.Parse(typeof(GearType), trainee.Element("GearType").Value),
                                DrivingSchool = (SchoolName)Enum.Parse(typeof(SchoolName), trainee.Element("SchoolName").Value),
                                LessonsNb = Convert.ToInt32(trainee.Element("LessonsNumber").Value),
                                Succsess = Convert.ToBoolean(trainee.Element("Success").Value)
                            }).ToList();
            DS.DataSource.TraineesList = traineesList;
        }

        private void CreateTestsFile()
        {
            testsRoot = new XElement("Tests");
            testsRoot.Save(testsPath);
        }
        private void SaveTestsList()
        {
            List<DrivingTest> testsList = DS.DataSource.DrivingtestsList;

            testsRoot = new XElement("Tests", from test in testsList
                                              select new XElement("Trainee",
                                                            new XElement("Id",test.ID),
                                                            new XElement("TraineeId",test.Trainee_ID),
                                                            new XElement("TesterId",test.Tester_ID),
                                                            new XElement("Date",test.Date.ToString()),
                                                            new XElement("StartingPoint",
                                                                    new XElement("StreetName", test.StartingPoint.StreetName),
                                                                    new XElement("Number",test.StartingPoint.Number.ToString()),
                                                                    new XElement("City", test.StartingPoint.City)),
                                                            new XElement("Success",test.Success.ToString()),
                                                            new XElement("Comment",test.Comment),
                                                            new XElement("Requirements", requirementsElement(test))
                                                            ));
            testsRoot.Save(testsPath);
        }
        private void LoadTestsFile()
        {
            testsRoot = XElement.Load(testsPath);
        }
        private void LoadTestsList()
        {
            List<DrivingTest> testsList = new List<DrivingTest>();

            testsList = (from test in testsRoot.Elements()
                         select new DrivingTest()
                         {
                             ID = test.Element("Id").Value,
                             Trainee_ID = test.Element("TraineeId").Value,
                             Tester_ID = test.Element("TesterId").Value,
                             Date = Convert.ToDateTime(test.Element("Date").Value),
                             StartingPoint = new Address(){
                                                StreetName = test.Element("StartingPoint").Element("StreetName").Value,
                                                Number =Convert.ToInt32(test.Element("StartingPoint").Element("Number").Value),
                                                City = test.Element("StartingPoint").Element("City").Value
                                             },
                             Success = Convert.ToBoolean(test.Element("Success").Value),
                             Comment = test.Element("Comment").Value,
                             Requirements = ElementToRequirements(test.Element("Requirements"))
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
            LoadFiles();
            LoadLists();//todo: or not?

            //DS.DataSource.init();
            //SaveTestersList();
            //SaveTestsList();
            //SaveTraineesList();
        }

        #region Tester
        public void AddTester(Tester tester)
        {
            DS.DataSource.TestersList.Add(tester);
            SaveTestersList();
        }
        public bool RemoveTester(Tester tester)
        {
            bool b = DS.DataSource.TestersList.Remove(tester);
            if (b)
                SaveTestersList();
            return b;
            
        }
        public void UpdateTester(Tester tester)
        {
            int index = DataSource.TestersList.FindIndex(T => tester.ID == T.ID);
            DataSource.TestersList[index] = tester;
            SaveTestersList();
        }
        public bool TesterExist(Tester tester)
        {
            if (GetTesters().Exists(tstr => tstr.ID == tester.ID))
                return true;
            return false;
        }
        public List<Tester> GetTesters(Func<Tester, bool> predicate = null)//to get either all the testers or the ones that the predicate returns true for
        {
            IEnumerable<Tester> result = null;

            if (predicate != null)
            {
                result = from t in DS.DataSource.TestersList
                         where (predicate(t))
                         select t;
            }
            else
            {
                result = DS.DataSource.TestersList;
            }

            return result.ToList();
        }
        public Tester GetTester(string id)
        {
            return GetTesters().FirstOrDefault(tmp_tes => tmp_tes.ID == id);
        }
        #endregion

        #region Trainee
        public void AddTrainee(Trainee trainee)
        {
            DS.DataSource.TraineesList.Add(trainee);
            SaveTraineesList();
        }
        public bool RemoveTrainee(Trainee trainee)
        {
            bool b = DS.DataSource.TraineesList.Remove(trainee);
            if (b)
                SaveTraineesList();
            return b;
        }
        public void UpdateTrainee(Trainee trainee)
        {
            int index = DataSource.TraineesList.FindIndex(T => trainee.ID == T.ID);
            DataSource.TraineesList[index] = trainee;
            SaveTraineesList();
        }
        public bool TraineeExist(Trainee trainee)
        {
            if (GetTrainees().Exists(trat => trat.ID == trainee.ID))
                return true;
            return false;
        }
        public List<Trainee> GetTrainees(Func<Trainee, bool> predicate = null)
        {
            IEnumerable<Trainee> result = null;

            if (predicate != null)
            {
                result = from t in DS.DataSource.TraineesList
                         where (predicate(t))
                         select t;
            }
            else
            {
                result = DS.DataSource.TraineesList;
            }
            return result.ToList();
        }
        public List<Trainee> GetTrainees()
        {
            var result = from t in DS.DataSource.TraineesList
                         select t;
            return result.ToList();
        }
        public Trainee GetTrainee(string id)
        {
            return GetTrainees().FirstOrDefault(tmp_trainee => tmp_trainee.ID == id);
        }
        #endregion

        #region DrivingTest
        public void AddDrivingTest(DrivingTest drivingTest)
        {
            drivingTest.ID = (++Configuration.NUMBER_OF_TEST).ToString("00000000");

            DS.DataSource.DrivingtestsList.Add(drivingTest);
            SaveTestsList();
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            bool b = DS.DataSource.DrivingtestsList.Remove(drivingTest);
            if (b)
                SaveTestsList();
            return b;
        }
        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            int index = DataSource.DrivingtestsList.FindIndex(T => drivingTest.ID == T.ID);
            DataSource.DrivingtestsList[index] = drivingTest;
            SaveTestsList();
        }
        public bool DrivingTestExist(DrivingTest test)
        {
            if (GetDrivingTests().Exists(item => item.ID == test.ID))
                return true;
            return false;
        }
        public List<DrivingTest> GetDrivingTests(Func<DrivingTest, bool> predicate1 = null)
        {
            IEnumerable<DrivingTest> result = null;

            if (predicate1 != null)
            {
                result = from t in DS.DataSource.DrivingtestsList
                         where (predicate1(t))
                         select t;
            }
            else
            {
                result = from t in DS.DataSource.DrivingtestsList
                         select t;
            }
            return result.ToList();
        }
        public DrivingTest GetDrivingTest(string id)
        {
            foreach (DrivingTest test in DS.DataSource.DrivingtestsList)
            {
                if (int.Parse(test.ID) == int.Parse(id))
                {
                    return test;
                }
            }

            return null;
            //return GetDrivingTests().FirstOrDefault(tmp_drivingtest => int.Parse(tmp_drivingtest.ID) == int.Parse(id));
        }
        #endregion
    }
}
