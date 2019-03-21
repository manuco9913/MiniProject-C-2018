using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.IO;
using System.Net;
using System.Xml;

// just because need to do push in GitHub

namespace BL
{
    internal class Dept_BL : IBL
    {

        private static DAL.Idal dal = DAL.FactorySingletonDal.getInstance(); // instance its like dal in the exam.
        

        //--------------Tester---------------
        public void AddTester(Tester tester)
        {
            if (TesterExist(tester))
                throw new Exception("This tester already exists...");
            if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)//checking tester age
                throw new Exception("Tester under 40 years");
            else
                dal.AddTester(tester);
        }
        public bool RemoveTester(Tester tester)
        {
            if (TesterExist(tester))
            {
                dal.RemoveTester(tester);
                return true;
            }
            else
            {
                throw new Exception("You can't remove a tester that doesn't exist");
                //   return false;
            }
        }
        public void UpdateTester(Tester tester)
        {
            if (TesterExist(tester))
                dal.UpdateTester(tester);
            else
                throw new Exception("This tester doesn't exist");
        }
        public bool TesterExist(Tester tester)
        {
            return dal.TesterExist(tester);
        }
        public List<Tester> GetTesters(Func<Tester, bool> p = null)
        {
            if (p == null)
                return dal.GetTesters();
            else
                return dal.GetTesters(p);
        }
        public Tester GetTester(string id)
        {
            return dal.GetTester(id);
        }

        //--------------Trainee---------------
        public void AddTrainee(Trainee trainee)
        {
            if (TraineeExist(trainee))
                throw new Exception("This trainee already exists");
            if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                throw new Exception("Trainee under 18 years");

            dal.AddTrainee(trainee);
        }
        public bool RemoveTrainee(Trainee trainee)
        {
            if (TraineeExist(trainee))
            {
                dal.RemoveTrainee(trainee);
                return true;
            }
            else
                throw new Exception("you can't remove a tester that doesn't exist");
            //bool b = true;
            //try
            //{
            //    b = instance.RemoveTrainee(trainee);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //return b;
        }
        public void UpdateTrainee(Trainee trainee)
        {
            dal.UpdateTrainee(trainee);
        }
        public bool TraineeExist(Trainee trainee)
        {
            return dal.TraineeExist(trainee);
        }
        public List<Trainee> GetTrainees(Func<Trainee, bool> p = null)
        {
            if (p == null)
                return dal.GetTrainees();
            else
                return dal.GetTrainees(p);
        }
        public Trainee GetTrainee(string id)
        {
            return dal.GetTrainee(id);
        }
        public int NumberTestsTraineeMade(Trainee trainee)
        {
            int count = 0;
            List<DrivingTest> res = GetDrivingTests(temp_trainee => temp_trainee.Trainee_ID == trainee.ID);
            if (res == null)
                throw new Exception("this trainee does not have a test");
            else
            {
                count = res.Count();
                return count;
            }
        }
        
        //--------------DrivingTest---------------
        public void AddDrivingTest(DrivingTest drivingTest)
        {
            if (GetTester(drivingTest.Tester_ID) == null)
                throw new Exception("This tester doesn't exist");
            if (GetTrainee(drivingTest.Trainee_ID) == null)
                throw new Exception("This trainee doesn't exist");
            if (!overMinLessonsTrainee(drivingTest.Trainee_ID)) // Done
                throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
            if (testedRecently(drivingTest)) // if he took a test in the last 7 days
                throw new Exception("The trainee cannot have 2 tests in the same week");
            if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID)) // Done
                throw new Exception("Tester and trainee have to use the same type of car");
            string suggestTesterHour = testerAvailableTesting_ADDTEST(drivingTest.Tester_ID, drivingTest.Date);
            if (suggestTesterHour != "Tester is available")//If the tester is available then you can set a test but if he is not available then you cant
                throw new Exception("The tester is not available during these hours\n" + suggestTesterHour);

            dal.AddDrivingTest(drivingTest);
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            if (DrivingTestExist(drivingTest))
            {
                dal.RemoveDrivingTest(drivingTest);
                return true;
            }
            else
                throw new Exception("Cannot remove a test that doesn't exist");
        }
        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            if (GetTester(drivingTest.Tester_ID) == null)
                throw new Exception("This tester doesn't exist");
            if (GetTrainee(drivingTest.Trainee_ID) == null)
                throw new Exception("This trainee doesn't exist");
            if (!overMinLessonsTrainee(drivingTest.Trainee_ID)) // Done
                throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
            if (testedRecently(drivingTest)) // if he took a test in the last 7 days
                throw new Exception("The trainee cannot have 2 tests in the same week");
            if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID)) // Done
                throw new Exception("Tester and trainee have to use the same type of car");
            string suggestTesterHour1 = testerAvailableTesting_UPDATETEST(drivingTest.Tester_ID, drivingTest.Date);
            if (suggestTesterHour1 != "Tester is available")//If the tester is available then you can set a test but if he is not available then you cant
                throw new Exception("The tester is not available during these hours\n" + suggestTesterHour1);

            dal.UpdateDrivingTest(drivingTest);
        }
        public bool DrivingTestExist(DrivingTest drivingTest)
        {
            return dal.DrivingTestExist(drivingTest);
        }
        public List<DrivingTest> GetDrivingTests(Func<DrivingTest, bool> p = null)
        {
            if (p == null)
                return dal.GetDrivingTests(p);//use GetDrivingTests in class Dal_imp
            else
                return dal.GetDrivingTests(p);
        }
        public DrivingTest GetDrivingTest(string id)
        {
            return dal.GetDrivingTest(id);
        }
        public List<DrivingTest> GetDrivingTestsByDate(Func<DrivingTest, bool> predicate = null)
        {
            List<DrivingTest> res = GetDrivingTests(predicate);
            if (res == null)
                throw new Exception("type in date for this test");
            else
            {
                var result = from t in res
                             where (predicate(t))
                             select t;
                if (result == null)
                {
                    throw new Exception("there are no tests in this date");
                }
                else
                {
                    return result.ToList();
                }
            }
        }

        
        //We need to implement this
        public List<Tester> printAllAvailableTestersAt(/*Some date or time, suggest: DateTime*/) { return null; }
        public IEnumerable<Person> GetAllPersons()
        {
            IEnumerable<Person> result1 = (from p in dal.GetTrainees()
                                           select p);
            IEnumerable<Person> result2 = (from p in dal.GetTesters()
                                           select p);
            return result1.Concat(result2);

        }

        //------------------------------------------------------------Test requirments--------------------------------------------------------------------
        enum DAYS { Sunday, Monday, Tuesday, Wednesday, Thursday };
        private string testerAvailableTesting_ADDTEST(string tester_ID, DateTime date)
        {

            Tester tester = GetTester(tester_ID);
            string AvailableTesterHours = "Your Tester is Available on:\n";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if ((tester.Schedule.Data[i][j] == true))
                    {
                        AvailableTesterHours += "--" + Enum.GetName(typeof(DAYS), i) + " " + (j + 9) + ":00\n";
                        if (((int)date.DayOfWeek == i) && (date.Hour == (j + 9)))
                        {
                            var res = GetDrivingTests(dt => (dt.Tester_ID == tester_ID) && (dt.Date == date));//creates a new list of "all" the tests who have that tester id and the same time
                            if (res.Count == 0)
                                return "Tester is available";//true
                            else
                                return AvailableTesterHours;//false
                        }
                    }
                }
            }
            //if there's no hour available return false
            if (AvailableTesterHours == "Your Tester is Available on:\n")
                return "Tester is not available at all";
            else
                return AvailableTesterHours;//false
        }
        private string testerAvailableTesting_UPDATETEST(string tester_ID, DateTime date)
        {

            Tester tester = GetTester(tester_ID);
            string AvailableTesterHours = "Your Tester is Available on:\n";

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if ((tester.Schedule.Data[i][j] == true))
                    {
                        AvailableTesterHours += "--" + Enum.GetName(typeof(DAYS), i) + " " + (j + 9) + ":00\n";
                        if (((int)date.DayOfWeek == i) && (date.Hour == (j + 9)))
                        {
                            var res = GetDrivingTests(dt => (dt.Tester_ID == tester_ID) && (dt.Date == date));//creates a new list of "all" the tests who have that tester id and the same time
                            if (res.Count == 1)
                                return "Tester is available";//true
                            else
                                return AvailableTesterHours;//false
                        }
                    }
                }
            }
            //if there's no hour available return false
            if (AvailableTesterHours == "Your Tester is Available on:\n")
                return "Tester is not available at all";
            else
                return AvailableTesterHours;//false
        }
        private bool testerAndTraineeUseSameCarType(string tester_ID, string trainee_ID)
        {
            return GetTester(tester_ID).Expertise == GetTrainee(trainee_ID).CarTrained;
        }
        private bool testedRecently(DrivingTest testToAdd)
        {
            List<DrivingTest> drivingTests = GetDrivingTests(temp_dt => temp_dt.Trainee_ID == testToAdd.Trainee_ID);
            if (drivingTests.Count == 0)
                return false;
            else
            {
                var result = (from test2 in drivingTests
                                  //where (drivingTest.Date.Subtract(test2.Date).TotalDays < 7)
                              where (Math.Abs((test2.Date - testToAdd.Date).Days) < BE.Configuration.MIN_GAP_TEST) && (test2.ID != testToAdd.ID)
                              select test2).ToList();
                if (result.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private bool overMinLessonsTrainee(string trainee_ID)
        {
            if (GetTrainee(trainee_ID).LessonsNb < Configuration.MIN_LESSONS)
                return false;
            return true;
        }
    }
}