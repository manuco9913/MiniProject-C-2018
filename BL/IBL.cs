using System.Collections.Generic;
using BE;

namespace BL
{
    public interface IBL
    {
        bool AddDrivingTest(DrivingTest drivingTest);
        bool AddTester(Tester tester);
        bool AddTrainee(Trainee trainee);

        bool RemoveDrivingTest(DrivingTest drivingTest);
        bool RemoveTester(Tester tester);
        bool RemoveTrainee(Trainee trainee);

        bool UpdateDrivingTest(DrivingTest drivingTest);
        bool UpdateTester(Tester tester);
        bool UpdateTrainee(Trainee trainee);

        List<DrivingTest> GetAllDrivingTests();
        List<Tester> GetTesters();
        Tester GetTester(string id);//not sure we need this
        List<Trainee> GetTrainees();
        Trainee GetTrainee(string id);//not sure we need this
        IEnumerable<Person> GetAllPersons();


    }
}