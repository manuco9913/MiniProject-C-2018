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
using BE;

namespace PL_WpfApp
{
    /// <summary>
    /// Interaction logic for PageAddTrainee.xaml
    /// </summary>
    public partial class PageAddTrainee : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.Trainee trainee;

        public PageAddTrainee()
        {
            InitializeComponent();
            this.dayOfBirthDatePicker.DisplayDateEnd = DateTime.Now;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.carTrainedComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            this.gearTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearType));
            this.drivingSchoolComboBox.ItemsSource = Enum.GetValues(typeof(BE.SchoolName));

        }

        private void Click_AddTrainee(object sender, RoutedEventArgs e)
        {
            try
            {
                //if we want to add in one exception one string to throw with all the fields there are to fields
                if (String.IsNullOrEmpty(this.iDTextBox.Text))
                    throw new Exception("You have to fill the ID field");
                if (String.IsNullOrEmpty(this.firstNameTextBox1.Text))
                    throw new Exception("You have to fill the First Name field");
                if (String.IsNullOrEmpty(this.lastNameTextBox.Text))
                    throw new Exception("You have to fill the Last Name field");
                if (String.IsNullOrEmpty(this.cityTextBox.Text))
                    throw new Exception("You have to fill the City field");
                if (String.IsNullOrEmpty(this.streetNameTextBox.Text))
                    throw new Exception("You have to fill the Street Name field");
                if (String.IsNullOrEmpty(this.numberTextBox.Text))
                    throw new Exception("You have to fill the Number field");
                if (String.IsNullOrEmpty(this.genderComboBox.Text))
                    throw new Exception("You have to choose a Gender");
                if (String.IsNullOrEmpty(this.drivingSchoolComboBox.Text))
                    throw new Exception("You have to choose a Driving School");
                if (String.IsNullOrEmpty(this.lessonsNbTextBox.Text))
                    throw new Exception("You have to fill the Lessons Number field");
                if (String.IsNullOrEmpty(this.carTrainedComboBox.Text))
                    throw new Exception("You have to choose a Car Trained");
                if (String.IsNullOrEmpty(this.gearTypeComboBox.Text))
                    throw new Exception("You have to choose a Gear Type ");
                if (String.IsNullOrEmpty(this.dayOfBirthDatePicker.Text))
                    throw new Exception("You have to choose a Day Of Birth");
                if (String.IsNullOrEmpty(this.InstructorNameTextBox1.Text))
                    throw new Exception("You have to fill the Instructor First Name field");


                trainee = new BE.Trainee();
                trainee.Address = new BE.Address();
                trainee.Name = new BE.Name();
                trainee.ID = this.iDTextBox.Text;
                trainee.Name.FirstName = this.firstNameTextBox1.Text;
                trainee.Name.LastName = this.lastNameTextBox.Text;
                trainee.Address.City = this.cityTextBox.Text;
                trainee.Address.StreetName = this.streetNameTextBox.Text;
                trainee.Address.Number = Convert.ToInt32(this.numberTextBox.Text);
                trainee.DayOfBirth = this.dayOfBirthDatePicker.DisplayDate;
                trainee.Gender = (BE.Gender)this.genderComboBox.SelectedValue;
                trainee.LessonsNb = Convert.ToInt32(this.lessonsNbTextBox.Text);
                trainee.CarTrained = (BE.CarType)this.carTrainedComboBox.SelectedValue;
                trainee.GearType = (BE.GearType)this.gearTypeComboBox.SelectedValue;
                trainee.DrivingSchool = (BE.SchoolName)this.drivingSchoolComboBox.SelectedValue;
                //todo: we need to show in WPF the list of testers so the trainee can choose an instructor and then read it and convert it to Name class type
                string[] temp = InstructorNameTextBox1.Text.Split(' ');
                trainee.Instructor.FirstName = temp[0];
                trainee.Instructor.LastName = temp[1];

                if (bl.TraineeExist(trainee))
                    throw new Exception("This trainee already exists...");
                if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                    throw new Exception("Trainee under 18 years");

                bl.AddTrainee(trainee);
                MessageBox.Show("Successfully added trainee!");
                this.NavigationService.Navigate(new FirstPage());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void IDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
