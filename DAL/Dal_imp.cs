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
            return GetTesters().Remove(tester);
        }
        public void UpdateTester(Tester tester)
        {
            Tester result = (from item1 in DS.DataSource.TestersList
                             where item1.ID == tester.ID
                             select item1).FirstOrDefault();
            result.Address.City = tester.Address.City;
            result.Address.StreetName = tester.Address.StreetName;
            result.Address.Number = tester.Address.Number;
            result.Experience = tester.Experience;
            result.Expertise = tester.Expertise;
            result.MaxDistance = tester.MaxDistance;
        }
        public bool TesterExist(Tester tester)
        {
            if (GetTesters().Exists(tstr => tstr.ID == tester.ID))
                return true;
            return false;
        }
        public List<Tester> GetTesters(Func<Tester, bool> predicate = null)//to get either all the testers or the one that the predicate return true for
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
                result = from t in DS.DataSource.TestersList
                         select t;
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
            return GetTrainees().Remove(trainee);
        }
        public void UpdateTrainee(Trainee trainee)
        {
            var result = (from item in GetTrainees()
                          where item.ID == trainee.ID
                          select item).FirstOrDefault();
            result.Address.City = trainee.Address.City;
            result.Address.StreetName = trainee.Address.StreetName;
            result.Address.Number = trainee.Address.Number;
            result.DrivingSchool = trainee.DrivingSchool;
            result.GearType = trainee.GearType;
            result.Instructor = trainee.Instructor;
            result.LessonsNb = trainee.LessonsNb;
        }
        public bool TraineeExist(Trainee trainee)
        {
            if (GetTrainees().Exists(item => item.ID == trainee.ID))
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
                result = from t in DS.DataSource.TraineesList
                         select t;
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
            DS.DataSource.DrivingtestsList.Add(drivingTest);
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            DrivingTest temp_test = GetDrivingTest(drivingTest);
            temp_test.Requirements.Clear();
            return GetDrivingTests().Remove(temp_test);
        }
        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            //int index = GetDrivingTests().FindIndex(s => s.ID == drivingTest.ID); // index = index of the tester we are looking for
            //if (index == -1)
            //{
            //    throw new Exception("Driving Test with the same id not found...");
            //}
            //GetDrivingTests()[index] = drivingTest;
            //return true;
            var result = (from item in DS.DataSource.DrivingtestsList
                          where item.ID == drivingTest.ID
                          select item).FirstOrDefault();
            result.StartingPoint.City = drivingTest.StartingPoint.City;
            result.Success = drivingTest.Success;
            result.Tester_ID = drivingTest.Tester_ID;
            result.Time = drivingTest.Time;
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
        private DrivingTest GetDrivingTest(DrivingTest dt)
        {
            return GetDrivingTests().FirstOrDefault(tmp_drivingtes => tmp_drivingtes.ID == dt.ID);
        }
        #endregion
    }
}
