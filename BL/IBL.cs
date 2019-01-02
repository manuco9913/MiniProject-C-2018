using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace BL
{
    public interface IBL
    {
        //--------------------------------------------checking functions---------------------------------------------------------------
        bool check_TesterAge(Tester tester); //not possible to add a tester under 40 years old
        bool check_TraineeAge(Trainee trainee);//not possible to add a trainee if under 18 years old
        bool check_PossibleAddTest(Trainee trainee ); //not possible to add a test if 7 days didn't pass since the last one (if there ose one at all)
        bool check_LessonsCount(Trainee trainee);//not possible to add a test if the trainee took less than 20 lessons
        bool check_AddTest(Test test);  // not possible to add a test if no examiner is available for this date and time, and if he is, we must ensure that he does not have another test at the same time. if the system does not find an available tester, it gives the possible options to the student
        bool check_AddTesterToTest(Tester tester);// not posssible to add a tester to the tes if he's not available
        bool check_UpdateTest(Tester tester);//not possible to update a tes if the Tester did not fill all the fields
        bool check_SameHourTest(Test test);// if the tester/Trainee have a test in the same hour -> return true
        bool check_finalGradeExists(Test test); //if the trainee already PASSED the test -> return true; we need to ask if !check_FinalGrade...
        bool check_IfSameVehicle(Tester tester, Trainee trainee);// checking if the trainee and the tester use the same type of vehicle; if whice_car_uses is the same in the tester and trainee -> return true


        /*-----------------------------------------------FUNCTIONS---------------------------------------------------------------------------*/

        List<Tester> testersAroundMe(string address);//the function returns all the Testers that live near the address specified.
        List<Tester> available_HoursOfTesters(string date);//check if the tester works in this hour/date and if he's Available in this hour/date.
        List<Test> checkTestConditions(Predicate<Test> checkFunc);//function that checks if the Tests Satisfies the predicate condition
        int numOfTests(Trainee trainee);//function that returns the number of tests that the trainee took.
        bool passTheTest(Trainee trainee); // if the trainee passed the test -> return true
        List<Test> getAllTestsTime(List<Test> testsList);//testsList it's the getTestList from IDAL





        #region Tester functions (add, remove, update)
        void addTester(Tester tester);
        void removeTester(Tester tester);
        void updateTester(Tester tester);
        //Tester getTester(?);
        #endregion

        #region Trainee functions (add, remove, update)
        void addTrainee(Trainee trainee);
        void removeTrainee(Trainee trainee);
        void updateTrainee(Trainee trainee);
        #endregion

        #region Test functions (add, update)
        void addTest(Test test);
        void updateTest(Test test);
        #endregion

    }
}
