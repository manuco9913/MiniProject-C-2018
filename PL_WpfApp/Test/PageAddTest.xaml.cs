using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PL_WpfApp
{
    /// <summary>
    /// Interaction logic for PageAddTest.xaml
    /// </summary>
    public partial class PageAddTest : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.DrivingTest drivingTest;
        public PageAddTest()
        {
            InitializeComponent();
            this.dateDatePicker.DisplayDateEnd = DateTime.Now;
        }

        private void Click_AddTest(object sender, RoutedEventArgs e)
        {
            try
            {
                #region if text boxes are null or if the input format is wrong
                if (String.IsNullOrEmpty(this.iDTextBox.Text))
                    throw new Exception("You have to fill the ID field");
                else if (this.iDTextBox.Text.Length != 9 && !Regex.IsMatch(this.iDTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The ID needs to have 9 DIGITS");
                if (String.IsNullOrEmpty(this.tester_IDTextBox.Text))
                    throw new Exception("You have to fill the First Name field");
                else if (!Regex.IsMatch(this.tester_IDTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The first name can contain only letters");
                if (String.IsNullOrEmpty(this.trainee_IDTextBox.Text))
                    throw new Exception("You have to fill the Last Name field");
                else if (!Regex.IsMatch(this.trainee_IDTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The last name can contain only letters");
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
                    throw new Exception("You have to fill the experience field");
                else if (Regex.IsMatch(this.timeTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The experience field can only contain numbers");
                if (String.IsNullOrEmpty(this.commentTextBox.Text))
                    throw new Exception("You have to chose expertise");
                else if (!Regex.IsMatch(this.commentTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The experience field can only contain letters");
                #endregion

                drivingTest = new BE.DrivingTest();
                drivingTest.ID = this.iDTextBox.Text;
                drivingTest.Tester_ID = this.tester_IDTextBox.Text;
                drivingTest.Trainee_ID = this.trainee_IDTextBox.Text;
                drivingTest.Date = this.dateDatePicker.DisplayDate;
                drivingTest.Time = TimeSpan.Parse(this.timeTextBox.Text);
                drivingTest.StartingPoint.City = this.cityTextBox.Text;
                drivingTest.StartingPoint.StreetName = this.streetNameTextBox.Text;
                drivingTest.StartingPoint.Number = Convert.ToInt32(this.numberTextBox.Text);
                drivingTest.Comment = this.commentTextBox.Text;
                drivingTest.Success = (this.successCheckBox.IsChecked) == true;

                if (bl.DrivingTestExist(drivingTest))
                    throw new Exception("This test already exists");
                if (bl.GetTester(drivingTest.Tester_ID).Expertise != bl.GetTrainee(drivingTest.Trainee_ID).CarTrained)
                    throw new Exception("Cannot be tested on a different car");
                //פה נצטרך לבדוק את כל ההגבלות על טסט


                this.NavigationService.Navigate(new FirstPage());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
