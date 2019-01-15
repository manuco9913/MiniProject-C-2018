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
            if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)
            {
                throw new Exception("Tester under 40 years");
            }
            foreach (Tester item in DS.DataSource.TestersList)
            {
                if(item.ID == tester.ID)
                {
                    throw new Exception("Tester already exist");
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
                    throw new Exception("Trainee already exist");
                    //return false;
                }
            }
            DS.DataSource.TraineesList.Add(trainee.Clone());
            return true;
        }

        public List<DrivingTest> GetDrivingTests(Func<DrivingTest, bool> predicate = null)
        {
            IEnumerable<DrivingTest> result = null;

            if (predicate != null)
            {
                result = from t in DS.DataSource.DrivingtestsList
                         where (predicate(t))
                         select t.Clone();
            }
            else
            {
                result = from t in DS.DataSource.DrivingtestsList
                         select t.Clone();
            }
            return result.ToList();
        }

        public List<Tester> GetTesters()
        {
            var result = from t in DS.DataSource.TestersList
                         select t.Clone();
            return result.ToList();
        }
        public List<Tester> GetTesters(Func<Tester,bool> predicate = null)
        {
            IEnumerable<Tester> result = null;

            if (predicate != null)
            {
                result = from t in DS.DataSource.TestersList
                         where (predicate(t))
                         select t.Clone();
            }
            else
            {
                result = from t in DS.DataSource.TestersList
                         select t.Clone();
            }

            return result.ToList();
        }
        
        // do not use Predicate<T> with Linq, instead use Func<T,bool>
        // public List<Trainee> GetTrainees(Predicate<Trainee> p =null) 
        public List<Trainee> GetTrainees(Func<Trainee,bool> predicate = null)
        {
            IEnumerable<Trainee> result = null;
           
            if(predicate != null)
            {
                result = from t in DS.DataSource.TraineesList
                         where (predicate(t))
                        select t.Clone();
            }
            else
            {
            result = from t in DS.DataSource.TraineesList
                        select t.Clone();
            }
            return result.ToList();
        }


        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            DrivingTest temp_test = GetDrivingTest(drivingTest.ID);
            if (temp_test == null)
                throw new Exception("Driving Test with the same id not found...");
            temp_test.Requirements.Clear();
            return GetDrivingTests().Remove(temp_test);
        }

        private DrivingTest GetDrivingTest(string id)
        {
            return GetDrivingTests().FirstOrDefault(tmp_drivingtes => tmp_drivingtes.ID == id);
        }

        public bool RemoveTester(Tester tester)
        {
            Tester temp_tester = GetTester(tester.ID);
            if (temp_tester == null)
                throw new Exception("Tester with the same id not found...");
            return GetTesters().Remove(temp_tester);
           
        }

        private Tester GetTester(Tester tester)
        {
            return GetTesters().FirstOrDefault(tmp_tes => tmp_tes.ID == tester.ID);
        }

        public bool RemoveTrainee(Trainee trainee)
        {
            Trainee temp_trainee = GetTrainee(trainee.ID);
            if (temp_trainee == null)
                throw new Exception("Trainee with the same id not found...");
            return GetTrainees().Remove(temp_trainee);
        }

        public Trainee GetTrainee(Trainee trainee)
        {
            return GetTrainees().FirstOrDefault(tmp_trainee =>tmp_trainee.ID == trainee.ID);
        }
        public List<Trainee> GetTrainees()
        {
            var result = from t in DS.DataSource.TraineesList
                         select t.Clone();
            return result.ToList();
        } 
        

        public bool UpdateDrivingTest(DrivingTest drivingTest)
        {
            {

                int index = GetDrivingTests().FindIndex(s => s.ID == drivingTest.ID); // index = index of the tester we are looking for
                if (index == -1)
                {
                    throw new Exception("Driving Test with the same id not found...");
                }
                GetDrivingTests()[index] = drivingTest;
                return true;
            }
        }

        public bool UpdateTester(Tester tester)
        {
            {
                
                int index = GetTesters().FindIndex(s => s.ID == tester.ID); // index = index of the tester we are looking for
                if (index == -1)
                {
                    throw new Exception("Tester with the same id not found...");
                }
                GetTesters()[index] = tester;
                return true;
            }
        }
        public bool UpdateTrainee(Trainee trainee)
        {
            {
                int index = GetTrainees().FindIndex(s => s.ID == trainee.ID); // index = index of the trainee we are looking for
                if (index == -1)
                {
                    throw new Exception("Tester with the same id not found...");
                }
                GetTrainees()[index] = trainee;
                return true;
            }
        }

    }
}
