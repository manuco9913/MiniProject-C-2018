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

        #region Tester
        public bool AddTester(Tester tester)
        {
            DS.DataSource.TestersList.Add(tester);
            return true;
        }
        public bool UpdateTester(Tester tester)
        {
            //    int index = GetTesters().FindIndex(s => s.ID == tester.ID); // index = index of the tester we are looking for
            //    if (index == -1)
            //    {
            //        throw new Exception("Tester with the same id not found...");
            //    }
            //    GetTesters()[index] = tester;

            Tester result = (from item1 in DS.DataSource.TestersList
                             where item1.ID == tester.ID
                             select item1).FirstOrDefault();

            return true;

        }
        public bool RemoveTester(Tester tester)
        {
            Tester temp_tester = GetTester(tester.ID);
            if (temp_tester == null)
                throw new Exception("Tester with the same id not found...");
            return GetTesters().Remove(temp_tester);

        }
        public List<Tester> GetTesters(Func<Tester, bool> predicate = null)
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
        public List<Tester> GetTesters()
        {
            var result = from t in DS.DataSource.TestersList
                         select t.Clone();
            return result.ToList();
        }
        public Tester GetTester(string id)
        {
            return GetTesters().FirstOrDefault(tmp_tes => tmp_tes.ID == id);
        }
        #endregion

        #region Trainee
        public bool AddTrainee(Trainee trainee)
        {
            if(DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
            {
                throw new Exception("Trainee under 18");
            }
            foreach (Trainee item in DS.DataSource.TraineesList)
            {
                if (item.ID == trainee.ID)
                {
                    throw new Exception("Trainee already exist");
                }
            }
            DS.DataSource.TraineesList.Add(trainee.Clone());
            return true;
        }
        public bool UpdateTrainee(Trainee trainee)
        {
            if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
            {
                throw new Exception("Trainee under 18");
            }
            int index = GetTrainees().FindIndex(s => s.ID == trainee.ID); // index = index of the trainee we are looking for
            if (index == -1)
            {
                throw new Exception("Tester with the same id not found...");
            }
            GetTrainees()[index] = trainee;
            return true;

        }
        public bool RemoveTrainee(Trainee trainee)
        {
            Trainee temp_trainee = GetTrainee(trainee.ID);
            if (temp_trainee == null)
                throw new Exception("Trainee with the same id not found...");
            return GetTrainees().Remove(temp_trainee);
        }
        public List<Trainee> GetTrainees(Func<Trainee, bool> predicate = null)
        {
            IEnumerable<Trainee> result = null;

            if (predicate != null)
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
        public List<Trainee> GetTrainees()
        {
            var result = from t in DS.DataSource.TraineesList
                         select t.Clone();
            return result.ToList();
        }
        public Trainee GetTrainee(string id)
        {
            return GetTrainees().FirstOrDefault(tmp_trainee => tmp_trainee.ID == id);
        }
        #endregion

        #region DrivingTest
        public bool AddDrivingTest(DrivingTest drivingTest)
        {
            DS.DataSource.DrivingtestsList.Add(drivingTest.Clone());
            return true;
        }
        public bool UpdateDrivingTest(DrivingTest drivingTest)
        {
            int index = GetDrivingTests().FindIndex(s => s.ID == drivingTest.ID); // index = index of the tester we are looking for
            if (index == -1)
            {
                throw new Exception("Driving Test with the same id not found...");
            }
            GetDrivingTests()[index] = drivingTest;
            return true;
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            DrivingTest temp_test = GetDrivingTest(drivingTest);
            if (temp_test == null)
                throw new Exception("Driving Test with the same id not found...");
            temp_test.Requirements.Clear();
            return GetDrivingTests().Remove(temp_test);
        }
        public List<DrivingTest> GetDrivingTests(Func<DrivingTest, bool> predicate1 = null)
        {
            IEnumerable<DrivingTest> result = null;

            if (predicate1 != null)
            {
                result = from t in DS.DataSource.DrivingtestsList
                         where (predicate1(t))
                         select t.Clone();
            }
            else
            {
                result = from t in DS.DataSource.DrivingtestsList
                         select t.Clone();
            }
            return result.ToList();
        }
        private DrivingTest GetDrivingTest(DrivingTest dt)
        {
            return GetDrivingTests().FirstOrDefault(tmp_drivingtes => tmp_drivingtes.ID == dt.ID);
        }
        #endregion
    }
}
