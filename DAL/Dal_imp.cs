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
        public void AddTester(Tester tester)
        {
            DS.DataSource.TestersList.Add(tester);
        }
        public bool RemoveTester(Tester tester)
        {
            return DS.DataSource.TestersList.Remove(tester);
        }
        public void UpdateTester(Tester tester)
        {
            Tester result = (from item1 in DS.DataSource.TestersList
                             where item1.ID == tester.ID
                             select item1).FirstOrDefault();
            result = tester;
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
        }
        public bool RemoveTrainee(Trainee trainee)
        {
            //BE.Trainee tr = new Trainee();
            //tr=trainee.Clone();
            return DS.DataSource.TraineesList.Remove(trainee);
        }
        public void UpdateTrainee(Trainee trainee)
        {
            var result = (from item in GetTrainees()
                          where item.ID == trainee.ID
                          select item).FirstOrDefault();
            result = trainee;
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
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            DrivingTest temp_test = GetDrivingTest(drivingTest.ID);
            return DS.DataSource.DrivingtestsList.Remove(drivingTest);
            /* return GetDrivingTests().Remove(temp_test); */
        }
        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            var result = (from item in DS.DataSource.DrivingtestsList
                          where item.ID == drivingTest.ID
                          select item).FirstOrDefault();
            result = drivingTest;
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
