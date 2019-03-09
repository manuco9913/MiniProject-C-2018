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

        //todo: delete this function FILL-CLICK
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            
            tester_IDTextBox.Text = "123456789";
            trainee_IDTextBox.Text = "987654321";
            timeTextBox.Text = "12";
            dateDatePicker.DisplayDate = DateTime.Now;
            streetNameTextBox.Text = "av";
            cityTextBox.Text = "sdf";
            numberTextBox.Text = "1";
        }

        private void Click_AddTest(object sender, RoutedEventArgs e)
        {
            try
            {
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
                #endregion

                drivingTest = new BE.DrivingTest();
                drivingTest.StartingPoint = new BE.Address();
                drivingTest.Requirements = new bool[Configuration.NUMBER_OF_REQUIREMENTS];
                drivingTest.Tester_ID = this.tester_IDTextBox.Text;
                drivingTest.Trainee_ID = this.trainee_IDTextBox.Text;
                TimeSpan tempTime = TimeSpan.FromHours(Convert.ToDouble(this.timeTextBox.Text));
                drivingTest.Date = Convert.ToDateTime(dateDatePicker.Text) + tempTime;
                drivingTest.StartingPoint.City = this.cityTextBox.Text;
                drivingTest.StartingPoint.StreetName = this.streetNameTextBox.Text;
                drivingTest.StartingPoint.Number = Convert.ToInt32(this.numberTextBox.Text);

                if (bl.DrivingTestExist(drivingTest))
                    throw new Exception("This test already exists");
                if (bl.GetTester(drivingTest.Tester_ID).Expertise != bl.GetTrainee(drivingTest.Trainee_ID).CarTrained)
                    throw new Exception("Cannot be tested on a different car");

                if (isLoading)
                    throw new Exception("Checking distance...");
                else
                {
                    bl.AddDrivingTest(drivingTest);
                    this.NavigationService.Navigate(new FirstPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CityTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Address address = new Address() { StreetName = streetNameTextBox.Text, City = cityTextBox.Text, Number = Convert.ToInt32(numberTextBox.Text) };
            Thread thread = new Thread(() => IsTesterCloseEnough(bl.GetTester(this.tester_IDTextBox.Text), address));
            thread.Start();
        }

            public void CheckDistance(Tester tester, Address address)
        {
            isLoading = true;
            isCloseEnough = IsTesterCloseEnough(tester, address);
            isLoading = false;
        }
        //Distance Function
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
