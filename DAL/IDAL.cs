using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface Idal
    {
        void AddTester(Tester tester);
        bool RemoveTester(Tester tester);
        void UpdateTester(Tester tester);
        bool TesterExist(Tester tester);

        void AddTrainee(Trainee trainee);
        bool RemoveTrainee(Trainee trainee);
        void UpdateTrainee(Trainee trainee);
        bool TraineeExist(Trainee trainee);


        void AddDrivingTest(DrivingTest drivingTest);
        bool RemoveDrivingTest(DrivingTest drivingTest);
        void UpdateDrivingTest(DrivingTest drivingTest);
        bool DrivingTestExist(DrivingTest test);

        List<Tester> GetTesters(Func<Tester, bool> p = null);
        Tester GetTester(string id);

        List<Trainee> GetTrainees(Func<Trainee, bool> p = null);
        Trainee GetTrainee(string id);

        List<DrivingTest> GetDrivingTests(Func<DrivingTest, bool> p = null);
        DrivingTest GetDrivingTest(string id);
    }
}
