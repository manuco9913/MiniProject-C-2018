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
            bindGrid(boolgrid, tester.Schedule);

        }

        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {
            BE.Tester tempTester = new BE.Tester();
            try
            {
                #region if text boxes are null or if the input format is wrong
                if (String.IsNullOrEmpty(this.firstNameTextBox.Text))
                    throw new Exception("You have to fill the First Name field");
                else if (!Regex.IsMatch(this.firstNameTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The first name can contain only letters");
                if (String.IsNullOrEmpty(this.lastNameTextBox.Text))
                    throw new Exception("You have to fill the Last Name field");
                else if (!Regex.IsMatch(this.lastNameTextBox.Text, @"[\p{L} ]+$"))
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
                if (String.IsNullOrEmpty(this.genderComboBox.Text))
                    throw new Exception("You have to chose Gender");
                if (String.IsNullOrEmpty(this.experienceTextBox.Text))
                    throw new Exception("You have to fill the experience field");
                else if (!Regex.IsMatch(this.experienceTextBox.Text, @"^\d+$"))
                    throw new Exception("The experience field can only contain numbers");
                if (String.IsNullOrEmpty(this.expertiseComboBox.Text))
                    throw new Exception("You have to chose expertise");
                else if (!Regex.IsMatch(this.experienceTextBox.Text, @"^\d+$"))
                    throw new Exception("The experience field can only contain numbers");
                if (String.IsNullOrEmpty(this.maxDistanceTextBox.Text))
                    throw new Exception("You have to fill the Max-Distance field");
                if (String.IsNullOrEmpty(this.maxTestWeeklyTextBox.Text))
                    throw new Exception("You have to fill the Max-Test-Weekly field");
                else if (!Regex.IsMatch(this.maxTestWeeklyTextBox.Text, @"^\d+$"))
                    throw new Exception("The max test weekly can only contain numbers");
                #endregion

                tempTester = new BE.Tester();
                tempTester.Address = new BE.Address();
                tempTester.Name = new BE.Name();
                tempTester.Schedule = new BE.Schedule();
                tempTester.Schedule.Data = new bool[5][];
                for (int i = 0; i < tempTester.Schedule.Data.Length; i++)
                {
                    tempTester.Schedule.Data[i] = new bool[6];
                }

                tempTester.ID = tester.ID;//the ID CANNOT change so it takes the id from before
                tempTester.Name.FirstName = this.firstNameTextBox.Text;
                tempTester.Name.LastName = this.lastNameTextBox.Text;
                tempTester.Address.City = this.cityTextBox.Text;
                tempTester.Address.StreetName = this.streetNameTextBox.Text;
                tempTester.Address.Number = Convert.ToInt32(this.numberTextBox.Text);
                tempTester.DayOfBirth = tester.DayOfBirth;//the DayOfBirth CANNOT change so we take the old one
                tempTester.Gender = (BE.Gender)this.genderComboBox.SelectedValue;
                tempTester.Expertise = (BE.CarType)this.expertiseComboBox.SelectedValue;
                tempTester.MaxTestWeekly = Convert.ToInt32(maxTestWeeklyTextBox.Text);
                tempTester.MaxDistance = Convert.ToInt32(maxDistanceTextBox.Text);
                tempTester.Experience = Convert.ToInt32(experienceTextBox.Text);
                if (tempTester.Schedule != null)
                {
                    int i, j;
                    foreach (var item in this.boolgrid.Children)
                    {
                        CheckBox checkbox = item as CheckBox;
                        i = Grid.GetRow(checkbox);
                        j = Grid.GetColumn(checkbox);
                        tempTester.Schedule.Data[i][j] = (checkbox.IsChecked == true);
                    }
                }

                if (!bl.TesterExist(tempTester))
                    throw new Exception("This tester doesn't exist...");

                bl.UpdateTester(tempTester);
                MessageBox.Show("Successfully updated tester!");
                this.NavigationService.Navigate(new FirstPage());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void bindGrid(Grid thegrid, BE.Schedule schedule)
        {
            foreach (var item in thegrid.Children)
            {
                CheckBox checkbox = item as CheckBox;
                int i = Grid.GetRow(checkbox);
                int j = Grid.GetColumn(checkbox);
                checkbox.IsChecked = schedule.Data[i][j];
            }
            //refresh grid and window
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
