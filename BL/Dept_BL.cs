using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

// just because need to do push in GitHub

namespace BL
{
    internal class Dept_BL : IBL
    {

        private static DAL.Idal instance = DAL.FactorySingletonDal.getInstance(); // instance its like dal in the exam.

        public void AddTester(Tester tester)
        {
            if (TesterExist(tester))
                throw new Exception("This tester already exists...");
            if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)//checking tester age
                throw new Exception("Tester under 40 years");
            else
                instance.AddTester(tester);
        }
        public bool RemoveTester(Tester tester)
        {
            if (TesterExist(tester))
            {
                instance.RemoveTester(tester);
                return true;
            }
            else
            {
                throw new Exception("You can't remove a tester that doesn't exist");
                return false;
            }
        }
        public void UpdateTester(Tester tester)
        {
            if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)
                throw new Exception("Tester under 40 years");
            if (TesterExist(tester))
                instance.UpdateTester(tester);
            else
                throw new Exception("This tester doesn't exist");
        }
        public bool TesterExist(Tester tester)
        {
            return instance.TesterExist(tester);
        }

        public void AddTrainee(Trainee trainee)
        {
            if (TraineeExist(trainee))
                throw new Exception("This trainee already exists");
            if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                throw new Exception("Trainee under 18 years");

            instance.AddTrainee(trainee);
        }
        public bool RemoveTrainee(Trainee trainee)
        {
            bool b = true;
            try
            {
                b = instance.RemoveTrainee(trainee);
            }
            catch (Exception e)
            {
                throw e;
            }
            return b;
        }
        public void UpdateTrainee(Trainee trainee)
        {
            if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                throw new Exception("Trainee under 18 years");

            instance.UpdateTrainee(trainee);
        }
        public bool TraineeExist(Trainee trainee)
        {
            return instance.TraineeExist(trainee);
        }


        public void AddDrivingTest(DrivingTest drivingTest)
        {
            if (!DrivingTestExist(drivingTest))
                throw new Exception("This driving test doesn't exist");
            if (!overMinLessonsTrainee(drivingTest.Trainee_ID)) // Done
                throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
            if (testedRecently(drivingTest.Trainee_ID)) // if he did test in the last 7 days
                throw new Exception("The trainee cannot take the test since he was tested recently");
            if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID)) // Done
                throw new Exception("Tester and trainee do not use the same type of car");
            if (testerMaxTestWeekly(drivingTest.Tester_ID))// if tester do more tests in week than the max he can do
                throw new Exception("Tester reached his maximum number of tests");
            if (!testerAvailableTesting(drivingTest.Tester_ID, drivingTest.Date))//If the tester is available then you can set a test but if it is not available then you cant
                throw new Exception("The tester is not available during these hours");

            instance.AddDrivingTest(drivingTest);
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            if (DrivingTestExist(drivingTest))
                return instance.RemoveDrivingTest(drivingTest);
            else
                throw new Exception("Cannot remove a test that doesn't exist");
        }
        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            if (!DrivingTestExist(drivingTest))
                throw new Exception("This driving test doesn't exist");
            if (!overMinLessonsTrainee(drivingTest.Trainee_ID))
                throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
            if (testedRecently(drivingTest.Trainee_ID))
                throw new Exception("The trainee cannot take the test since he was tested recently");
            if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID))
                throw new Exception("Tester and trainee do not use the same type of car");
            if (testerMaxTestWeekly(drivingTest.Tester_ID))
                throw new Exception("Tester reached his maximum number of tests");
            if (!testerAvailableTesting(drivingTest.Tester_ID, drivingTest.Date)) //drivingtest.Date - is it the hour that Trainee want to set?
                throw new Exception("The tester is not available during these hours");

            instance.UpdateDrivingTest(drivingTest);
        }
        public bool DrivingTestExist(DrivingTest drivingTest)
        {
            return instance.DrivingTestExist(drivingTest);
        }
         
        //We need to implement this
        public List<Tester> printAllAvailableTestersAt(/*Some date or time, suggest: DateTime*/) { return null; }
        public IEnumerable<Person> GetAllPersons()
        {
            IEnumerable<Person> result1 = (from p in instance.GetTrainees()
                                           select p);
            IEnumerable<Person> result2 = (from p in instance.GetTesters()
                                           select p);
            return result1.Concat(result2);

        }

        public List<Tester> GetTesters()
        {//try and catch????
            return instance.GetTesters();
        }
        public Tester GetTester(string id)
        {
            return instance.GetTester(id);
        }

        public List<Trainee> GetTrainees()
        { //try and catch????
            return instance.GetTrainees();
        }
        public Trainee GetTrainee(string id)
        {
            return instance.GetTrainee(id);
        }

        public List<DrivingTest> GetAllDrivingTests()
        {
            return instance.GetDrivingTests();
        }
        //||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //we can simply do this with predicate = null and we will have only one function
        public List<DrivingTest> GetAllDrivingTestsThat(Func<DrivingTest, bool> predicate)
        {
            return instance.GetDrivingTests(predicate);//use GetDrivingTests in class Dal_imp
        }




        //------------------------------------------------------------Test requirments--------------------------------------------------------------------
        private bool testerAvailableTesting(string tester_ID, DateTime date)
        {
            if (date.Hour > 14 || date.Hour < 9)
                return false;
            List<DrivingTest> res = GetAllDrivingTestsThat(temp_dt => temp_dt.Tester_ID == tester_ID);//creates a new list of "all" the testers who have that id (there will be only one
            IEnumerable<DrivingTest> result = null;
            if (res == null)//if there's no tester with that id it means that the tester we are trying to add to the test is available because he has no tests at all
                return true;
            else
            {
                result = from t in res
                         where (date.Subtract(t.Date).Hours >= 1) //Checks that the difference between the time the Trainee wants to set the test at, and the time the tester is working at. and makes sure it is at least an hour
                         select t;
                if (result == null) //result==null => found that this tester is not available in any hour
                {
                    return false;
                }
                return true; // if we found that the tester is avaiable for least 1 hour, we can set a test with this tester!!
            }

            //-------------------------------IMPORTANT--------------------------------
            /// The project instructions stipulate, among other things,
            /// that if the tester is not available, an option should be given to another 
            /// tester from the list of testers that is available
            //------------------------------------------------------------------------
        }
        private bool AreFallingInSameWeek(DateTime date1, DateTime date2) // check if 2 dates are in the same week
        {
            return date1.AddDays(-(int)date1.DayOfWeek) == date2.AddDays(-(int)date2.DayOfWeek);
        }
        private bool testerMaxTestWeekly(string tester_ID)
        {
            List<DrivingTest> res = GetAllDrivingTestsThat(temp_dt => temp_dt.Tester_ID == tester_ID);//creates a new list of "all" the testers who have that id (there will be only one
            IEnumerable<DrivingTest> result = null;
            if (res == null)
                return false;
            else
            {
                result = from t in res
                         where (AreFallingInSameWeek(t.Date, DateTime.Now))
                         select t;
                if (result == null)
                {
                    return false;
                }
                else
                {
                    if (result.Count() > GetTester(tester_ID).MaxTestWeekly)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        private bool testerAndTraineeUseSameCarType(string tester_ID, string trainee_ID)
        {
            return GetTester(tester_ID).Expertise == GetTrainee(trainee_ID).CarTrained;
        }
        private bool testedRecently(string trainee_ID)
        {
            // DrivingTest temp = new DrivingTest();
            //temp.Trainee_ID = trainee_ID;
            List<DrivingTest> res = GetAllDrivingTestsThat(temp_dt => temp_dt.Trainee_ID == trainee_ID);
            if (res == null)
                return false;
            else
            {
                IEnumerable<DrivingTest> result = null;
                result = from t in res
                         where (DateTime.Now.Subtract(t.Date).TotalDays > 7)
                         select t;
                if (result == null)
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
