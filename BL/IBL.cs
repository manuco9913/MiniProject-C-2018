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
        bool check_TesterAge(Tester tester); 
        bool check_TraineeAge(Trainee trainee);
        bool check_PossibleAddTest(Trainee trainee ); //not possible to add test before pass 7 days from the last test , if there was one at all
        bool check_LessonsCount(Trainee trainee); // not possible add a test if no examiner is available for this date, and even if the examiner is available on the same day, he must ensure that he does not have another test at the same time. if the system not found tester she give to student currect option
        bool check_AddTest(Tester tester); // can add the tester just if he can, need check his calender
        bool check_AddTesterToTest(Tester tester);
        bool check_UpdateTest(Tester tester);//If the Tester did not fill all fields then the test could not be updated
        bool check_SameHourTest(Test test);// if the tester/Trainee have a test in the same hour -> return true
        bool check__finalGradeExists(Test test); //if the trainee already PASSED test -> return true , we need to ask if !check_FinalGrade...
        bool check_IfSameVehicle(Tester tester, Trainee trainee);// if whice_car_uses is same in the tester and trainee -> return true


        /*-------------------------------------FUNCTION----------------------------------------------------------------------------------*/

        List<Tester> testersAroundMe(string address);//funcs get address and returns all the Testers that live near it.
        List<Tester> available_HoursOfTesters(string date);//check if tester work in this hour/date and if he Available in this hour/date.
        List<Test> IfTest_returnTest(Predicate<Test> checkFunc);//function that checks if Tests Satisfy the condition
        int numOfTests(Trainee trainee);//function get trainee and return num of test that he took.
        bool passTheTest(Trainee trainee); // if trainee pass the test -> return true
        List<Test> allTestTime(List<Test> testsList);//tests list it mean getTestList from IDAL





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
