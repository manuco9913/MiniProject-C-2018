using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Tools;

namespace DAL
{
    internal class Dal_imp : Idal
    {
        public Dal_imp()
        {
            DS.DataSource.init();
        }
        public bool AddDrivingTest(DrivingTest drivingTest)
        {
            DS.DataSource.DrivingtestsList.Add(drivingTest.Clone());
            return true;
        }



        public bool AddTester(Tester tester)
        {
            foreach (Tester item in DS.DataSource.TestersList)
            {
                if(item.ID == tester.ID)
                {
                    throw new Exception("Tester already exists");
                 //   return false;
                }
            }
            DS.DataSource.TestersList.Add(tester.Clone());
            return true;
        }

        public bool AddTrainee(Trainee trainee)
        {
            foreach (Trainee item in DS.DataSource.TraineesList)
            {
                if (item.ID == trainee.ID)
                {
                    throw new Exception("Trainee already exists");
                    //   return false;
                }
            }
            DS.DataSource.TraineesList.Add(trainee.Clone());
            return true;
        }

        public List<DrivingTest> GetDrivingTests()
        {
            throw new NotImplementedException();
        }

        public List<Tester> GetTesters()
        {
            // return
            //(from item in DS.DataSource.TestersList
            // select item.Clone()
            //).ToList();
                      
            return DS.DataSource.TestersList.Select(item => item.Clone()).ToList();
        }
        // do not use Predicate<T> with Linq, instead use Func<T,bool>
        // public List<Trainee> GetTrainees(Predicate<Trainee> p =null) 
        public List<Trainee> GetTrainees(Func<Trainee,bool> p = null)
        {
            IEnumerable<Trainee> result = null;
           
            if(p !=null)
            {
                result = from t in DS.DataSource.TraineesList
                         where (p(t))
                        select t.Clone();
            }
            else
            {
            result = from t in DS.DataSource.TraineesList
                        select t.Clone();
            }
            return result.ToList();
        }
        //-------------------------------------Grouping With tzuriel--------------------------------
        /*
         public List<Trainee> GetGroupByCity()
        {
            var results = from t in DS.DataSource.TraineesList
                          group t by t.Address.City into g
                          select new { City = g.Key, Traineers = g.ToList() };
                          return (List<Trainee>)results;
        }
        */
        //-------------------------------------Grouping With tzuriel--------------------------------

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
