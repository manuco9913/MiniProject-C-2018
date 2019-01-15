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
                if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)
                {
                    throw new Exception("Tester under 40 years");
                }
                instance.AddTester(tester);
            }
            catch (Exception exception)
            {
                throw exception;
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
            catch (Exception e)
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
                    throw new Exception("tester under 40 years");
                }
                check =instance.UpdateTester(tester);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return check ;
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

        public bool AddDrivingTest(DrivingTest drivingTest) { return true; }
        public bool RemoveDrivingTest(DrivingTest drivingTest) { return true; }
        public bool UpdateDrivingTest(DrivingTest drivingTest) { return true; }

        public List<Tester> GetTesters()
        {
            return instance.GetTesters();
        }
        public List<Trainee> GetTrainees() //Male only
        {
            try
            {
                return instance.GetTrainees((Trainee t) => t.Gender == Gender.MALE);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public List<DrivingTest> GetDrivingTests() { return null; }

        public IEnumerable<Person> GetAllPersons()
        {
            IEnumerable<Person> result1 = (from p in instance.GetTrainees(null)
                                          select p);
            IEnumerable<Person> result2 =(from p in instance.GetTesters()
                                 select p);
            return result1.Concat(result2);

            //List<Person> list = new List<Person>();

            //list.AddRange(GetTrainees());
            //list.AddRange(GetTesters());

            //return list;
        }
        // private bool SelectMaleTrainee(Trainee t)
        // {
        //       return (t.Gender==Gender.MALE);
        // }
    }
}
