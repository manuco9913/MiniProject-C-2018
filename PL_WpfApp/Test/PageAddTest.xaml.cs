using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace PL_WpfApp
{
    /// <summary>
    /// Interaction logic for PageAddTest.xaml
    /// </summary>
    public partial class PageAddTest : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.DrivingTest drivingTest;

        bool isCloseEnough = true;
        bool isLoading = false;

        public PageAddTest()
        {
            InitializeComponent();
            this.dateDatePicker.DisplayDateStart = DateTime.Now;
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {

            tester_IDTextBox.Text = "987654321";
            trainee_IDTextBox.Text = "123456789";
            timeTextBox.Text = "12";
            dateDatePicker.DisplayDate = DateTime.Now;
            streetNameTextBox.Text = "bayt vagan";
            cityTextBox.Text = "jerusalem";
            numberTextBox.Text = "10";
        }

        private void Click_AddTest(object sender, RoutedEventArgs e)
        {
            try
            {
                //to avoid the problem that if we have an error and we have to change a parameter, and only then, click the finish button again, the SUCCESS is already updated to be true so its like he passed
                Trainee newTrainee = bl.GetTrainee(this.trainee_IDTextBox.Text);
                if (newTrainee == null)
                    throw new Exception("This trainee doesn't exist");
                newTrainee.Succsess = false;

                #region if text boxes are null or if the input format is wrong
                if (String.IsNullOrEmpty(this.tester_IDTextBox.Text))
                    throw new Exception("You have to fill the tester id field");
                if (bl.GetTester(this.tester_IDTextBox.Text) == null)
                    throw new Exception("This tester doesn't exist");
                if (String.IsNullOrEmpty(this.trainee_IDTextBox.Text))
                    throw new Exception("You have to fill the trainee id field");
                if (bl.GetTrainee(this.trainee_IDTextBox.Text) == null)
                    throw new Exception("This trainee doesn't exist");
                if (String.IsNullOrEmpty(this.cityTextBox.Text))
                    throw new Exception("You have to fill the City field");
                else if (!Regex.IsMatch(this.cityTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The City name can contain only letters");
                if (String.IsNullOrEmpty(this.streetNameTextBox.Text))
                    throw new Exception("You have to fill the Street Name field");
                else if (!Regex.IsMatch(this.streetNameTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The street name can contain only letters");
                if (String.IsNullOrEmpty(this.numberTextBox.Text))
                    throw new Exception("You have to fill the number field");
                else if (!Regex.IsMatch(this.numberTextBox.Text, @"^\d+$"))
                    throw new Exception("The street number can only contain numbers");
                if (String.IsNullOrEmpty(this.timeTextBox.Text))
                    throw new Exception("You have to fill the time field");
                if (newTrainee.Succsess && bl.GetDrivingTests(test => bl.GetTrainee(test.Trainee_ID).Succsess == true &&
                        bl.GetTrainee(test.Trainee_ID).CarTrained == newTrainee.CarTrained) != null)//if the trainee is already tested on this type of car
                    throw new Exception("Trainee has already been tested on this car");
                #endregion

                drivingTest = new BE.DrivingTest();
                drivingTest.StartingPoint = new BE.Address();
                drivingTest.Requirements = new bool[9];
                drivingTest.Tester_ID = this.tester_IDTextBox.Text;
                drivingTest.Trainee_ID = this.trainee_IDTextBox.Text;
                TimeSpan tempTime = TimeSpan.FromHours(Convert.ToDouble(this.timeTextBox.Text));
                drivingTest.Date = Convert.ToDateTime(dateDatePicker.Text) + tempTime;
                drivingTest.StartingPoint.City = this.cityTextBox.Text;
                drivingTest.StartingPoint.StreetName = this.streetNameTextBox.Text;
                drivingTest.StartingPoint.Number = Convert.ToInt32(this.numberTextBox.Text);
                drivingTest.Requirements[0] = one_blinkersUsedCheckBox.IsChecked == true;
                drivingTest.Requirements[1] = two_distanceKeepingCheckBox.IsChecked == true;
                drivingTest.Requirements[2] = three_gearsUsageCheckBox.IsChecked == true;
                drivingTest.Requirements[3] = four_priorityToCheckBox.IsChecked == true;
                drivingTest.Requirements[4] = five_mirrorLookingCheckBox.IsChecked == true;
                drivingTest.Requirements[5] = six_obeyedToSignsCheckBox.IsChecked == true;
                drivingTest.Requirements[6] = seven_regularParkingCheckBox.IsChecked == true;
                drivingTest.Requirements[7] = eight_reverseParkingCheckBox.IsChecked == true;
                drivingTest.Requirements[8] = nine_speedKeepingCheckBox.IsChecked == true;

                int countRequirements = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (drivingTest.Requirements[i] == true)
                        countRequirements++;
                }
                if (bl.DrivingTestExist(drivingTest))
                    throw new Exception("This test already exists");
                if (bl.GetTester(drivingTest.Tester_ID).Expertise != bl.GetTrainee(drivingTest.Trainee_ID).CarTrained)
                    throw new Exception("Cannot be tested on a different car");
                if (testerMaxTestWeekly(drivingTest))// if tester does more tests in week than the max he can do
                    throw new Exception("Tester reached his maximum number of tests");

                if (isLoading)
                    throw new Exception("Checking distance...");
                else
                {
                    if (countRequirements > Configuration.MIN_NUMBER_OF_REQUIREMENTS)
                    {
                        bl.GetTrainee(drivingTest.Trainee_ID).Succsess = true;
                        drivingTest.Success = true;
                    }
                    else
                    {
                        bl.GetTrainee(drivingTest.Trainee_ID).Succsess = false;
                        drivingTest.Success = false;
                    }
                    if (!isCloseEnough) throw new Exception("The test is too far from the tester");
                    bl.AddDrivingTest(drivingTest);
                    MessageBox.Show("Test Added succesfully");
                    this.NavigationService.Navigate(new FirstPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool testerMaxTestWeekly(DrivingTest drivingTestToAdd)
        {
            List<DrivingTest> res = bl.GetDrivingTests(temp_dt => temp_dt.Tester_ID == drivingTestToAdd.Tester_ID);//creates a new list of "all" the testers who have that id
            if (res.Count != 0)
            {
                int testsInThisWeek = 0;
                foreach (DrivingTest test in res)
                {
                    if (AreFallingInSameWeek(test.Date, drivingTestToAdd.Date))
                        testsInThisWeek++;
                }
                if (testsInThisWeek >= bl.GetTester(drivingTest.Tester_ID).MaxTestWeekly)
                    return true;
            }
            else if (res.Count == 0 && bl.GetTester(drivingTest.Tester_ID).MaxTestWeekly == 0)
                return true;
            return false;

        }
        private bool AreFallingInSameWeek(DateTime date1, DateTime date2) // check if 2 dates are in the same week
        {
            //return date1.AddDays(-(int)date1.DayOfWeek) == date2.AddDays(-(int)date2.DayOfWeek);
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

            return d1 == d2;

        }


        //Distance Function
        private void CityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Address address = new Address() { StreetName = streetNameTextBox.Text, City = cityTextBox.Text, Number = Convert.ToInt32(numberTextBox.Text) };
                var x = bl.GetTester(this.tester_IDTextBox.Text);
                if (x == null)
                    MessageBox.Show("This tester doesn't exist");
                else
                {
                    Thread thread = new Thread(() => CheckDistance(x, address));
                    thread.Start();
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public void CheckDistance(Tester tester, Address address)
        {
            isLoading = true;
            isCloseEnough = IsTesterCloseEnough(tester, address);
            isLoading = false;
        }
        public bool IsTesterCloseEnough(Tester tester, Address address)
        {
            string origin = tester.Address.StreetName + " " + tester.Address.Number + " st." + tester.Address.City;  //"pisga 45 st. jerusalem"; //
            string destination = address.StreetName + " " + address.Number + " st." + address.City; //"gilgal 78 st. ramat-gan"; 
            if ((address.StreetName == "" && address.City == "" && address.Number.ToString() == "") || (tester.Address.StreetName == "" && tester.Address.City == "" && tester.Address.Number.ToString() == ""))
                return true;
            string KEY = @"Bem5PJyvuuUAhHz9K2qM88vC9QEHrMgx";
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
            //the response is given in an XML format//
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);
                if (distInMiles * 1.609344 > tester.MaxDistance && tester.MaxDistance > 3)
                    return false;
            }
            return true;
        }
    }
}
