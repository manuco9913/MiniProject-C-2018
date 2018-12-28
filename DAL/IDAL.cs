using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL<T>
    {
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

        #region Get lists (Testers, Trainees, Tests)
        List<Tester> getTesters();
        List<Trainee> getTrainee();
        List<Test> getTest();
        #endregion

    }
}

