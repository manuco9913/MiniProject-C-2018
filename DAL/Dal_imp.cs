using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
namespace DAL
{
    public class Dal_imp : IDAL
    {
        public void addTest(Test test)//checking if all the testId that already exist, are different than the one we want to add
        {
            foreach (var test1 in getTestList())
            {
                if (test1._testId == test._testId)
                    return;
            }
            getTestList().Add(test);
        }

        public void addTester(Tester tester)//to implement like the one before
        {
            getTestersList().Add(tester);
        }

        public void addTrainee(Trainee trainee)//to implement like the one before
        {
            getTraineeList().Add(trainee);
        }

        public List<Tester> getTestersList()//not sure because it looks like it's calling himself recursively
        {
            return getTestersList();
        }

        public List<Test> getTestList()//not sure because it looks like it's calling himself recursively
        {
            return getTestList();
        }

        public List<Trainee> getTraineeList()//not sure because it looks like it's calling himself recursively
        {
            return getTraineeList();
        }

        public void removeTester(Tester tester)//to implement
        {
            getTestersList().Remove(tester);
        }

        public void removeTrainee(Trainee trainee)//to imolement
        {
            getTraineeList().Remove(trainee);
        }

        public void updateTest(Test test)
        {
            throw new NotImplementedException();
        }

        public void updateTester(Tester tester)
        {
            throw new NotImplementedException();
        }

        public void updateTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }
    }
}
