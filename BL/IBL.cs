using System.Collections.Generic;
using BE;

namespace BL
{
    public interface IBL//מה יותר יפה? כל האדדים והרמובים וכו' ביחד? או כמו באיידל לפי ישות
    {
        void AddDrivingTest(DrivingTest drivingTest);
        void AddTester(Tester tester);
        void AddTrainee(Trainee trainee);

        bool TesterExist(Tester tester);
        bool TraineeExist(Trainee trainee);
        bool DrivingTestExist(DrivingTest drivingTest);

        bool RemoveDrivingTest(DrivingTest drivingTest);
        bool RemoveTester(Tester tester);
        bool RemoveTrainee(Trainee trainee);

        void UpdateDrivingTest(DrivingTest drivingTest);
        void UpdateTester(Tester tester);
        void UpdateTrainee(Trainee trainee);

        List<DrivingTest> GetAllDrivingTests();
        List<Tester> GetTesters();
        Tester GetTester(string id);//not sure we need this
        List<Trainee> GetTrainees();
        Trainee GetTrainee(string id);//not sure we need this
        IEnumerable<Person> GetAllPersons();


    }
}