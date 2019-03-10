using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using System.Threading;
using System.Net;
using System.IO;
using System.Xml;

namespace PL_WpfApp
{
    /// <summary>
    /// Interaction logic for PageUpdateTest.xaml
    /// </summary>
    /// 

    public partial class PageUpdateTest : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.DrivingTest drivingTest;

        bool isCloseEnough = true;
        bool isLoading = false;

        public PageUpdateTest(BE.DrivingTest dr)
        {
            InitializeComponent();
            drivingTest = dr;
            this.iDTextBox.Text = drivingTest.ID;
            this.tester_IDTextBox.Text = drivingTest.Tester_ID;
            this.trainee_IDTextBox.Text = drivingTest.Trainee_ID;
            this.dateDatePicker.DisplayDateStart = DateTime.Now;
            this.dateDatePicker.DisplayDate = drivingTest.Date;
            this.dateDatePicker.Text = drivingTest.Date.ToString();
            this.timeTextBox.Text = drivingTest.Date.Hour.ToString();
            this.cityTextBox.Text = drivingTest.StartingPoint.City;
            this.streetNameTextBox.Text = drivingTest.StartingPoint.StreetName;
            this.numberTextBox.Text = drivingTest.StartingPoint.Number.ToString();
            this.commentTextBox.Text = drivingTest.Comment;
            one_blinkersUsedCheckBox.IsChecked = dr.Requirements[0];
            two_distanceKeepingCheckBox.IsChecked = dr.Requirements[1];
            three_gearsUsageCheckBox.IsChecked = dr.Requirements[2];
            four_priorityToCheckBox.IsChecked = dr.Requirements[3];
            five_mirrorLookingCheckBox.IsChecked = dr.Requirements[4];
            six_obeyedToSignsCheckBox.IsChecked = dr.Requirements[5];
            seven_regularParkingCheckBox.IsChecked = dr.Requirements[6];
            eight_reverseParkingCheckBox.IsChecked = dr.Requirements[7];
            nine_speedKeepingCheckBox.IsChecked = dr.Requirements[8];
        }

        private void Click_UpdateTest(object sender, RoutedEventArgs e)
        {
            try
            {
                #region if text boxes are null or if the input format is wrong
                if (String.IsNullOrEmpty(this.tester_IDTextBox.Text))
                    throw new Exception("You have to fill the tester id field");
                if (String.IsNullOrEmpty(this.trainee_IDTextBox.Text))
                    throw new Exception("You have to fill the trainee id field");
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
                if (dateDatePicker.SelectedDate == null)
                    throw new Exception("You have to select a date");
                #endregion

                drivingTest.Tester_ID = this.tester_IDTextBox.Text;
                drivingTest.Trainee_ID = this.trainee_IDTextBox.Text;
                TimeSpan tempTime = TimeSpan.FromHours(Convert.ToDouble(this.timeTextBox.Text));
                drivingTest.Date = Convert.ToDateTime(dateDatePicker.Text) + tempTime;
                drivingTest.StartingPoint.City = this.cityTextBox.Text;
                drivingTest.StartingPoint.StreetName = this.streetNameTextBox.Text;
                drivingTest.StartingPoint.Number = Convert.ToInt32(this.numberTextBox.Text);
                drivingTest.Comment = this.commentTextBox.Text;
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

                if (bl.GetTester(drivingTest.Tester_ID).Expertise != bl.GetTrainee(drivingTest.Trainee_ID).CarTrained)//if the tester and the trainee are not using the same car
                    throw new Exception("Trainee cannot be tested on a different car");
                if (isLoading)
                    throw new Exception("Checking distance...");
                else
                {
                    bl.UpdateDrivingTest(drivingTest);
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
                    MessageBox.Show("Test updated succesfully");
                    this.NavigationService.Navigate(new FirstPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        //Distance Function
        private void CityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Address address = new Address() { StreetName = streetNameTextBox.Text, City = cityTextBox.Text, Number = Convert.ToInt32(numberTextBox.Text) };
            var x = bl.GetTester(this.tester_IDTextBox.Text);
            Thread thread = new Thread(() => CheckDistance(x, address));
            thread.Start();
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