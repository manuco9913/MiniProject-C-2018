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

namespace PL_WpfApp
{
    /// <summary>
    /// Interaction logic for PageUpdateTest.xaml
    /// </summary>
    public partial class PageUpdateTest : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.DrivingTest drivingTest;

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
                
                if (bl.GetTester(drivingTest.Tester_ID).Expertise != bl.GetTrainee(drivingTest.Trainee_ID).CarTrained)//if the tester and the trainee are not using the same car
                    throw new Exception("Trainee cannot be tested on a different car");

                bl.UpdateDrivingTest(drivingTest);

                this.NavigationService.Navigate(new FirstPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}