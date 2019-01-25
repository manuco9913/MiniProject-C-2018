using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.IO;
using System.Net;
using System.Xml;

// just because need to do push in GitHub

namespace BL
{
    internal class Dept_BL : IBL
    {

        private static DAL.Idal instance = DAL.FactorySingletonDal.getInstance(); // instance its like dal in the exam.

        //--------------Tester---------------
        public void AddTester(Tester tester)
        {
            if (TesterExist(tester))
                throw new Exception("This tester already exists...");
            if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)//checking tester age
                throw new Exception("Tester under 40 years");
            else
                instance.AddTester(tester);
        }
        public bool RemoveTester(Tester tester)
        {
            if (TesterExist(tester))
            {
                instance.RemoveTester(tester);
                return true;
            }
            else
            {
                throw new Exception("You can't remove a tester that doesn't exist");
                //   return false;
            }
        }
        public void UpdateTester(Tester tester)
        {
            if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)
                throw new Exception("Tester under 40 years");
            if (TesterExist(tester))
                instance.UpdateTester(tester);
            else
                throw new Exception("This tester doesn't exist");
        }
        public bool TesterExist(Tester tester)
        {
            return instance.TesterExist(tester);
        }
        public List<Tester> GetTesters(Func<Tester, bool> p = null)
        {
            if (p == null)
                return instance.GetTesters();
            else
                return instance.GetTesters(p);
        }
        public Tester GetTester(string id)
        {
            return instance.GetTester(id);
        }
        
        //--------------Trainee---------------
        public void AddTrainee(Trainee trainee)
        {
            if (TraineeExist(trainee))
                throw new Exception("This trainee already exists");
            if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                throw new Exception("Trainee under 18 years");

            instance.AddTrainee(trainee);
        }
        public bool RemoveTrainee(Trainee trainee)
        {
            if (TraineeExist(trainee))
            {
                instance.RemoveTrainee(trainee);
                return true;
            }
            else
                throw new Exception("you can't remove a tester that doesn't exist");
            //bool b = true;
            //try
            //{
            //    b = instance.RemoveTrainee(trainee);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //return b;
        }
        public void UpdateTrainee(Trainee trainee)
        {
            if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                throw new Exception("Trainee under 18 years");

            instance.UpdateTrainee(trainee);
        }
        public bool TraineeExist(Trainee trainee)
        {
            return instance.TraineeExist(trainee);
        }
        public List<Trainee> GetTrainees(Func<Trainee, bool> p = null)
        {
            if (p == null)
                return instance.GetTrainees();
            else
                return instance.GetTrainees(p);
        }
        public Trainee GetTrainee(string id)
        {
            return instance.GetTrainee(id);
        }
        public int NumberTestsTraineeMade(Trainee trainee)
        {
            int count = 0;
            List<DrivingTest> res = GetDrivingTests(temp_trainee => temp_trainee.Trainee_ID == trainee.ID);
            if (res == null)
                throw new Exception("this trainee does not have a test");
            else
            {
                count = res.Count();
                return count;
            }
        }
        private bool CheckIfHasLicense(Trainee trainee)     //-----------check if he has a License
        {
            List<Trainee> res = GetTrainees(tra => tra.ID == trainee.ID && tra.CarTrained == trainee.CarTrained);

            if (res == null)
                return false;
            else
            {
                if (trainee.Succsess == true)
                    return true;
            }
            return false;
        }
        public bool succsessInTest(Trainee trainee)        //--------pass or field int test-------

        {

            DrivingTest dr = GetDrivingTest(trainee.ID);
            bool a = dr.requirments._gradeBlinkersUsed = false;
            bool b = dr.requirments._gradeDistanceKeepping = false;
            bool c = dr.requirments._gradeGearsUsage = false;
            bool d = dr.requirments._gradeGivingPriorityToPedestrians = false;
            bool e = dr.requirments._gradeMirrorLooking = false;
            bool f = dr.requirments._gradeObeyedToSigns = false;
            bool g = dr.requirments._gradeRegularParking = false;
            bool h = dr.requirments._gradeReverseParking = false;
            bool i = dr.requirments._gradeSpeedKeeping = false;
            if ((a && b && c && d && e && f && g && h && i) == true)
            {
                trainee.Succsess = true;
                return true;
            }
            else
                return false;
        }

        //--------------DrivingTest---------------
        public void AddDrivingTest(DrivingTest drivingTest)
        {
            if (!DrivingTestExist(drivingTest))
                throw new Exception("This driving test doesn't exist");
            if (!overMinLessonsTrainee(drivingTest.Trainee_ID)) // Done
                throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
            if (testedRecently(drivingTest.Trainee_ID)) // if he did test in the last 7 days
                throw new Exception("The trainee cannot take the test since he was tested recently");
            if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID)) // Done
                throw new Exception("Tester and trainee do not use the same type of car");
            if (testerMaxTestWeekly(drivingTest.Tester_ID))// if tester do more tests in week than the max he can do
                throw new Exception("Tester reached his maximum number of tests");
            if (!testerAvailableTesting(drivingTest.Tester_ID, drivingTest.Date))//If the tester is available then you can set a test but if it is not available then you cant
                throw new Exception("The tester is not available during these hours");

            instance.AddDrivingTest(drivingTest);
        }
        public bool RemoveDrivingTest(DrivingTest drivingTest)
        {
            if (DrivingTestExist(drivingTest))
                return instance.RemoveDrivingTest(drivingTest);
            else
                throw new Exception("Cannot remove a test that doesn't exist");
        }
        public void UpdateDrivingTest(DrivingTest drivingTest)
        {
            if (!DrivingTestExist(drivingTest))
                throw new Exception("This driving test doesn't exist");
            if (!overMinLessonsTrainee(drivingTest.Trainee_ID))
                throw new Exception("The trainee cannot take the test, because he has done less than the minimum number of lessons");
            if (testedRecently(drivingTest.Trainee_ID))
                throw new Exception("The trainee cannot take the test since he was tested recently");
            if (!testerAndTraineeUseSameCarType(drivingTest.Tester_ID, drivingTest.Trainee_ID))
                throw new Exception("Tester and trainee do not use the same type of car");
            if (testerMaxTestWeekly(drivingTest.Tester_ID))
                throw new Exception("Tester reached his maximum number of tests");
            if (!testerAvailableTesting(drivingTest.Tester_ID, drivingTest.Date)) //drivingtest.Date - is it the hour that Trainee want to set?
                throw new Exception("The tester is not available during these hours");

            instance.UpdateDrivingTest(drivingTest);
        }
        public bool DrivingTestExist(DrivingTest drivingTest)
        {
            return instance.DrivingTestExist(drivingTest);
        }
        public List<DrivingTest> GetDrivingTests(Func<DrivingTest, bool> p = null)
        {
            if (p == null)
                return instance.GetDrivingTests(p);//use GetDrivingTests in class Dal_imp
            else
                return instance.GetDrivingTests(p);
        }
        public DrivingTest GetDrivingTest(string id)
        {
            return instance.GetDrivingTest(id);
        }
        public List<DrivingTest> GetDrivingTestsByDate(Func<DrivingTest, bool> predicate = null)
        {
            List<DrivingTest> res = GetDrivingTests(predicate);
            if (res == null)
                throw new Exception("type in date for this test");
            else
            {
                var result = from t in res
                             where (predicate(t))
                             select t;
                if (result == null)
                {
                    throw new Exception("there are no tests in this date");
                }
                else
                {
                    return result.ToList();
                }
            }
        }



        //We need to implement this
        public List<Tester> printAllAvailableTestersAt(/*Some date or time, suggest: DateTime*/) { return null; }
        public IEnumerable<Person> GetAllPersons()
        {
            IEnumerable<Person> result1 = (from p in instance.GetTrainees()
                                           select p);
            IEnumerable<Person> result2 = (from p in instance.GetTesters()
                                           select p);
            return result1.Concat(result2);

        }

        public void distance_Calculate(Tester tester, Trainee trainee) // what this func need get?
        {

            string origin = "pisga 45 st. jerusalem"; //or "תקווה פתח 100 העם אחד "etc.
            string destination = "gilgal 78 st. ramat-gan";//or "גן רמת 10 בוטינסקי'ז "etc.
            string KEY = @"<Y8pDqGNymiQ8h8oxs4uaS9CMv5lZXcb6>";
            string url = @"https://www.mapquestapi.com/directions/v2/route" +
             @"?key=" + KEY +
             @"&from=" + origin +
             @"&to=" + destination +
             @"&outFormat=xml" +
             @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
             @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);
                //display the returned driving time
                XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
                string fTime = formattedTime[0].ChildNodes[0].InnerText;
                Console.WriteLine("Driving Time: " + fTime);
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                Console.WriteLine("an error occurred, one of the addresses is not found. try again.");
            }
            else //busy network or other error...
            {
                Console.WriteLine("We have'nt got an answer, maybe the net is busy...");
            }
        }
       




        //------------------------------------------------------------Test requirments--------------------------------------------------------------------
        private bool testerAvailableTesting(string tester_ID, DateTime date)
        {
            if (date.Hour > 14 || date.Hour < 9)
                return false;
            List<DrivingTest> res = GetDrivingTests(temp_dt => temp_dt.Tester_ID == tester_ID);//creates a new list of "all" the testers who have that id (there will be only one
            IEnumerable<DrivingTest> result = null;
            if (res == null)//if there's no tester with that id it means that the tester we are trying to add to the test is available because he has no tests at all
                return true;
            else
            {
                result = from t in res
                         where (date.Subtract(t.Date).Hours >= 1) //Checks that the difference between the time the Trainee wants to set the test at, and the time the tester is working at. and makes sure it is at least an hour
                         select t;
                if (result == null) //result==null => found that this tester is not available in any hour
                {
                    return false;
                }
                return true; // if we found that the tester is avaiable for least 1 hour, we can set a test with this tester!!
            }

            //-------------------------------IMPORTANT--------------------------------
            /// The project instructions stipulate, among other things,
            /// that if the tester is not available, an option should be given to another 
            /// tester from the list of testers that is available
            //------------------------------------------------------------------------
        }
        private bool AreFallingInSameWeek(DateTime date1, DateTime date2) // check if 2 dates are in the same week
        {
            return date1.AddDays(-(int)date1.DayOfWeek) == date2.AddDays(-(int)date2.DayOfWeek);
        }
        private bool testerMaxTestWeekly(string tester_ID)
        {
            List<DrivingTest> res = GetDrivingTests(temp_dt => temp_dt.Tester_ID == tester_ID);//creates a new list of "all" the testers who have that id (there will be only one
            IEnumerable<DrivingTest> result = null;
            if (res == null)
                return false;
            else
            {
                result = from t in res
                         where (AreFallingInSameWeek(t.Date, DateTime.Now))
                         select t;
                if (result == null)
                {
                    return false;
                }
                else
                {
                    if (result.Count() > GetTester(tester_ID).MaxTestWeekly)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        private bool testerAndTraineeUseSameCarType(string tester_ID, string trainee_ID)
        {
            return GetTester(tester_ID).Expertise == GetTrainee(trainee_ID).CarTrained;
        }
        private bool testedRecently(string trainee_ID)
        {
            // DrivingTest temp = new DrivingTest();
            //temp.Trainee_ID = trainee_ID;
            List<DrivingTest> res = GetDrivingTests(temp_dt => temp_dt.Trainee_ID == trainee_ID);
            if (res == null)
                return false;
            else
            {
                IEnumerable<DrivingTest> result = null;
                result = from t in res
                         where (DateTime.Now.Subtract(t.Date).TotalDays > 7)
                         select t;
                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private bool overMinLessonsTrainee(string trainee_ID)
        {
            if (GetTrainee(trainee_ID).LessonsNb < Configuration.MIN_LESSONS)
                return false;
            return true;
        }
    }
}