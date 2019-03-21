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
    /// Interaction logic for PageAddTester.xaml
    /// </summary>
    public partial class PageAddTester : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.Tester tester;

        public PageAddTester()
        {
            InitializeComponent();
            this.dayOfBirthDatePicker.DisplayDateEnd = DateTime.Now;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender)); 
            this.expertiseComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
        }

        private void Click_AddTester(object sender, RoutedEventArgs e)
        {
            try
            {
                #region if text boxes are null or if the input format is wrong
                if (String.IsNullOrEmpty(this.iDTextBox.Text))
                    throw new Exception("You have to fill the ID field");
                else if (this.iDTextBox.Text.Length != 9 && !Regex.IsMatch(this.iDTextBox.Text, @"[\p{L} ]+$"))
                    throw new Exception("The ID needs to have 9 digits");
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
                if (String.IsNullOrEmpty(this.dayOfBirthDatePicker.Text))
                    throw new Exception("You have to chose birthday");
                #endregion

                tester = new BE.Tester();
                tester.Address = new BE.Address();
                tester.Name = new BE.Name();
                tester.ID = this.iDTextBox.Text;
                tester.Name.FirstName = this.firstNameTextBox.Text;
                tester.Name.LastName = this.lastNameTextBox.Text;
                tester.Schedule = new BE.Schedule();
                tester.Schedule.Data = new bool[5][];
                for (int i = 0; i < tester.Schedule.Data.Length; i++)
                {
                    tester.Schedule.Data[i] = new bool[6];
                }
                tester.Address.City = this.cityTextBox.Text;
                tester.Address.StreetName = this.streetNameTextBox.Text;
                tester.Address.Number = Convert.ToInt32(this.numberTextBox.Text);
                tester.DayOfBirth = this.dayOfBirthDatePicker.DisplayDate;
                tester.Gender = (BE.Gender)this.genderComboBox.SelectedValue;
                tester.Experience = Convert.ToInt32(this.experienceTextBox.Text);
                tester.Expertise = (BE.CarType)this.expertiseComboBox.SelectedValue;
                tester.MaxDistance = Convert.ToInt32(this.maxDistanceTextBox.Text);
                tester.MaxTestWeekly = Convert.ToInt32(this.maxTestWeeklyTextBox.Text);

                if (tester.Schedule != null)
                {
                    int i, j;
                    foreach (var item in this.boolgrid.Children)
                    {
                        CheckBox checkbox = item as CheckBox;
                        i = Grid.GetRow(checkbox);
                        j = Grid.GetColumn(checkbox);
                        tester.Schedule.Data[i][j] = (checkbox.IsChecked == true);
                    }
                }
                if (bl.TesterExist(tester))
                    throw new Exception("This tester already exists...");
                if (DateTime.Now.Year - tester.DayOfBirth.Year < BE.Configuration.MIN_TESTER_AGE)
                    throw new Exception("tester too young");

                bl.AddTester(tester);
                MessageBox.Show("Successfully added tester!");
                this.NavigationService.Navigate(new FirstPage());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void Click_Fill(object sender, RoutedEventArgs e)
        {
            iDTextBox.Text = "987654321";
            firstNameTextBox.Text = "jojo";
            lastNameTextBox.Text = "chalass";
            cityTextBox.Text = "jerusalem";
            streetNameTextBox.Text = "hapalmach";
            numberTextBox.Text = "15";
            genderComboBox.SelectedIndex = 0;
            maxTestWeeklyTextBox.Text = "10";
            maxDistanceTextBox.Text = "10";
            experienceTextBox.Text = "5";
            expertiseComboBox.SelectedIndex = 0;
        }
    }
}
