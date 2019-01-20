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

        public bool AddTester(Tester tester)
        {
            try
            {
                if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)//checking tester age
                {
                    throw new Exception("Tester under 40 years");
                }
                else
                instance.AddTester(tester);
            }
            catch (Exception exception)
            {
                throw exception;//if the DAL sent an exception, throw it to the UI/PL
            }
            return true;
        }
        public bool RemoveTester(Tester tester)
        {
            bool b = true;
            try
            {
                b = instance.RemoveTester(tester);
            }
            catch (Exception e)//if this tester doesn't exist, the DAL  will throw an exception
            {
                throw e;
            }
            return b;  
        }
        public bool UpdateTester(Tester tester)
        {
            bool check = true;
            try
            {
                if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)
                {
                    throw new Exception("Tester under 40 years");
                }
                check = instance.UpdateTester(tester);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return check;
        }

        public bool AddTrainee(Trainee trainee)
        {
            try
            {
                if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                {
                    throw new Exception("Trainee under 18 years");
                }
                instance.AddTrainee(trainee);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return true;
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
        public bool UpdateTrainee(Trainee trainee)
        {
            bool check = true;
            try
            {
                if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                {
                    throw new Exception("Trainee under 18 years");
                }
                check = instance.UpdateTrainee(trainee);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return check;
        }

        public bool AddDrivingTest(DrivingTest drivingTest)
        {
            try
            {
                if (!overMinLessonsTrainee(drivingTest.Trainee_ID)) // Done
                    throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
                if (testedRecently(drivingTest.Trainee_ID)) // if he did test in the last 7 days
                    throw new Exception("The trainee cannot take the test since he was tested recently");
                if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID)) // Done
                    throw new Exception("Tester and trainee do not use the same type of car");
                if (testerMaxTestWeekly(drivingTest.Tester_ID))// if tester do more tests in week than the max he can do
                    throw new Exception("Tester reached his maximum number of tests");
                if (!testerAvailableTesting(drivingTest.Tester_ID,drivingTest.Date))//If the tester is available then you can set a test but if it is not available then you cant
                    throw new Exception("The tester is not available during these hours");

                    
                

                instance.AddDrivingTest(drivingTest);
            }
            catch (Exception e)
            {
                throw e;

            }
            return true;
        }

        private bool testerAvailableTesting(string tester_ID,DateTime date)
        {
            if (date.Hour > 14 || date.Hour < 9)
                return false;
                List<DrivingTest> res = GetAllDrivingTestsThat(temp_dt => temp_dt.Tester_ID == tester_ID);
            IEnumerable<DrivingTest> result = null;
            if (res == null)
                return true;
            else
            {
                result = from t in res
                         where (date.Subtract(t.Date).Hours >= 1) //Checks that the difference between the time Trainee wants to set the test and the time the tester is working and makes sure it is at least an hour
                         select t;
                if (result == null) //if we found that the is tester not available in any hour so we said that the tester is not available
                {
                    return false;
                }
                return true; // if we found the tester is avaiable for least 1 hour so we can set a test with this tester
            }

            //-------------------------------IMPORTANT--------------------------------
            /// The project instructions stipulate, among other things,
            /// that if the tester is not available, an option should be given to another 
            /// tester from the list of testers that is available
            //------------------------------------------------------------------------
        }
        private bool AreFallingInSameWeek(DateTime date1, DateTime date2) // check if 2 dates in the same week
        {
            return date1.AddDays(-(int)date1.DayOfWeek) == date2.AddDays(-(int)date2.DayOfWeek);
        }
        private bool testerMaxTestWeekly(string tester_ID)
        {
            List<DrivingTest> res = GetAllDrivingTestsThat(temp_dt => temp_dt.Tester_ID == tester_ID);
            IEnumerable<DrivingTest> result = null;
            if (res == null)
                return false;
            else
            {
                result = from t in res
                         where (AreFallingInSameWeek(t.Date,DateTime.Now) )
                         select t;
                if (result == null)
                {
                    return false;
                }
                else
                {
                    if (result.Count() > Configuration.MAX_TEST_WEEKLY)
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

        public bool RemoveDrivingTest(DrivingTest drivingTest) { return true; }
        public bool UpdateDrivingTest(DrivingTest drivingTest)
        {
            bool check = true;
            try
            {
                if (!overMinLessonsTrainee(drivingTest.Trainee_ID))
                    throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
                if (!testedRecently(drivingTest.Trainee_ID))
                    throw new Exception("The trainee cannot take the test since he was tested recently");
                if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID))
                    throw new Exception("Tester and trainee do not use the same type of car");
                if (!testerMaxTestWeekly(drivingTest.Tester_ID))
                    throw new Exception("Tester reached his maximum number of tests");
                if (!testerAvailableTesting(drivingTest.Tester_ID,drivingTest.Date)) //drivingtest.Date - is it the hour that Trainee want to set?
                    throw new Exception("The tester is not available during these hours");
                check = instance.UpdateDrivingTest(drivingTest);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return check;
        }

        //We need to implement this
        public List<Tester> printAllAvailableTestersAt(/*Some date or time, suggest: DateTime*/) { return null; }
        //We need to implement this
        public List<DrivingTest> GetAllDrivingTestsThat(Func<DrivingTest,bool> predicate)
        {
            return instance.GetDrivingTests(predicate);//use GetDrivingTests in class Dal_imp
        }

        public List<Tester> GetTesters()
        {
            try
            {
                return instance.GetTesters();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public Tester GetTester(string id) { return new Tester(); }//not sure we need this

        public List<Trainee> GetTrainees()
        {
            try
            {
                return instance.GetTrainees();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public Trainee GetTrainee(string id) { return new Trainee(); }//not sure we need this

        public List<DrivingTest> GetAllDrivingTests() { return null; }

        public IEnumerable<Person> GetAllPersons()
        {
            IEnumerable<Person> result1 = (from p in instance.GetTrainees(null)
                                          select p);
            IEnumerable<Person> result2 =(from p in instance.GetTesters()
                                 select p);
            return result1.Concat(result2);
            
        }
        // private bool SelectMaleTrainee(Trainee t)
        // {
        //       return (t.Gender==Gender.MALE);
        // }
    }
}
