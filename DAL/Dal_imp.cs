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
                throw new Exception("Student with the same id not found...");
            temp_test.Requirements.RemoveAll();
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

        private Tester GetTester(string id)
        {
            return GetTesters().FirstOrDefault(tmp_tes => tmp_tes.ID == id);
        }

        public bool RemoveTrainee(Trainee trainee)
        {
            Trainee temp_trainee = GetTrainee(trainee.ID);
            if (temp_trainee == null)
                throw new Exception("Tester with the same id not found...");
            return GetTrainees().Remove(temp_trainee);
        }

        private Trainee GetTrainee(string id)
        {
            return GetTrainees().FirstOrDefault(tmp_trainee =>tmp_trainee.ID == id);
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
