using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    public interface IBL
    {
        bool check_TesterAge(Tester tester); 
        bool check_TraineeAge(Trainee trainee);
        bool check_PossibleAddTest(Trainee trainee ); //not possible to add test before pass 7 days from the last test , if there was one at all
        bool check_LessonsCount(Trainee trainee); // not possible add a test if no examiner is available for this date, and even if the examiner is available on the same day, he must ensure that he does not have another test at the same time. if the system not found tester she give to student currect option
        bool check_AddTest(Tester tester); // can add the tester just if he can, need check his calender
        bool check_AddTesterToTest(Tester tester);
        bool update_Test(Tester tester);//If the Tester did not fill all fields then the test could not be updated






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
