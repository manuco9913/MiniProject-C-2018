using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for PageUpdateTesterAccount.xaml
    /// </summary>
    public partial class PageUpdateTesterAccount : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.Tester tester;

        public PageUpdateTesterAccount(BE.Tester t)
        {
            InitializeComponent();
            tester = t;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.expertiseComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));


            this.firstNameTextBox.Text = tester.Name.FirstName;
            this.lastNameTextBox.Text = tester.Name.LastName;
            this.iDTextBox.Text = tester.ID;
            this.cityTextBox.Text = tester.Address.City;
            this.dayOfBirthTextBlock.Text = tester.DayOfBirth.ToString();
            this.streetNameTextBox.Text = tester.Address.StreetName;
            this.numberTextBox.Text = tester.Address.Number.ToString();
            this.genderComboBox.SelectedIndex = (int)tester.Gender;
            this.experienceTextBox.Text = tester.Experience.ToString();
            this.expertiseComboBox.SelectedIndex = (int)tester.Expertise;
            this.maxDistanceTextBox.Text = tester.MaxDistance.ToString();
            this.maxTestWeeklyTextBox.Text = tester.MaxTestWeekly.ToString();
        }

        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {
           
                         BE.Tester tempTester = new BE.Tester();
            try
            {
                if (String.IsNullOrEmpty(this.iDTextBox.Text))
                    throw new Exception("You have to fill the ID field");
                if (String.IsNullOrEmpty(this.firstNameTextBox.Text))
                    throw new Exception("You have to fill the First Name field");
                if (String.IsNullOrEmpty(this.lastNameTextBox.Text))
                    throw new Exception("You have to fill the Last Name field");
                if (String.IsNullOrEmpty(this.cityTextBox.Text))
                    throw new Exception("You have to fill the City field");
                if (String.IsNullOrEmpty(this.streetNameTextBox.Text))
                    throw new Exception("You have to fill the Street Name field");
                if (String.IsNullOrEmpty(this.numberTextBox.Text))
                    throw new Exception("You have to fill the number field");
                if (String.IsNullOrEmpty(this.genderComboBox.Text))
                    throw new Exception("You have to chose Gender");
                if (String.IsNullOrEmpty(this.experienceTextBox.Text))
                    throw new Exception("You have to fill the experience field");
                if (String.IsNullOrEmpty(this.expertiseComboBox.Text))
                    throw new Exception("You have to chose expertise");
                if (String.IsNullOrEmpty(this.maxDistanceTextBox.Text))
                    throw new Exception("You have to fill the Max-Distance field");
                if (String.IsNullOrEmpty(this.maxTestWeeklyTextBox.Text))
                    throw new Exception("You have to fill the Max-Test-Weekly field");
                if (String.IsNullOrEmpty(this.dayOfBirthTextBlock.Text))
                    throw new Exception("You have to chose birth-day");

                tempTester = new BE.Tester();
                tempTester.Address = new BE.Address();
                tempTester.Name = new BE.Name();
                tempTester.ID = tester.ID;//the ID CANNOT change so it takes the id from before
                tempTester.Name.FirstName = this.firstNameTextBox.Text;
                tempTester.Name.LastName = this.lastNameTextBox.Text;
                tempTester.Address.City = this.cityTextBox.Text;
                tempTester.Address.StreetName = this.streetNameTextBox.Text;
                tempTester.Address.Number = Convert.ToInt32(this.numberTextBox.Text);
                tempTester.DayOfBirth = tester.DayOfBirth;//the DayOfBirth CANNOT change so we take the old one
                tempTester.Gender = (BE.Gender)this.genderComboBox.SelectedValue;
                tempTester.Expertise = (BE.CarType)this.expertiseComboBox.SelectedValue;

                if (!bl.TesterExist(tempTester))
                    throw new Exception("This tester doesn't exist...");
                if (DateTime.Now.Year - tester.DayOfBirth.Year < 18)
                    throw new Exception("tester under 18 years");

                bl.UpdateTester(tempTester);
                MessageBox.Show("Successfully updated tester!");
                this.NavigationService.Navigate(new FirstPage());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
