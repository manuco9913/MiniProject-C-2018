using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


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
                if (!overMinLessonsTrainee(drivingTest.Trainee_ID))
                    throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
                //if (!testedRecently(drivingTest.Trainee_ID))
                //    throw new Exception("The trainee cannot take the test since he was tested recently");
                

                instance.AddDrivingTest(drivingTest);
            }
            catch (Exception e)
            {
                throw e;

            }
            return true;
        }

        //private bool testedRecently(string trainee_ID)
        //{
        //    if(instance.GetDrivingTests(item => i)
        //    { }
        //}

        private bool overMinLessonsTrainee(string trainee_ID)
        {
            if (GetTrainee(trainee_ID).LessonsNb < Configuration.MIN_LESSONS)
                return false;
            return true;
        }

        public bool RemoveDrivingTest(DrivingTest drivingTest) { return true; }
        public bool UpdateDrivingTest(DrivingTest drivingTest) { return true; }

    

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

        public List<DrivingTest> GetDrivingTests() { return null; }

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
