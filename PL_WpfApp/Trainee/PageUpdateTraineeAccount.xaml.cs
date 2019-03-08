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
    /// Interaction logic for PageUpdateTraineeAccount.xaml
    /// </summary>
    public partial class PageUpdateTraineeAccount : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.Trainee trainee;
        public PageUpdateTraineeAccount(BE.Trainee t)
        {
            InitializeComponent();
            trainee = t;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.carTrainedComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            this.gearTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearType));
            this.drivingSchoolComboBox.ItemsSource = Enum.GetValues(typeof(BE.SchoolName));


            this.iDTextBox.Text = trainee.ID;
            this.firstNameTextBox1.Text = trainee.Name.FirstName;
            this.lastNameTextBox.Text = trainee.Name.LastName;
            this.cityTextBox.Text = trainee.Address.City;
            this.streetNameTextBox.Text = trainee.Address.StreetName;
            this.numberTextBox.Text = trainee.Address.Number.ToString();
            this.dayOfBirthTextBlock.Text = trainee.DayOfBirth.ToString();
            this.genderComboBox.SelectedIndex = (int)trainee.Gender;
            this.lessonsNbTextBox.Text = trainee.LessonsNb.ToString();
            this.carTrainedComboBox.SelectedIndex = (int)trainee.CarTrained;
            this.gearTypeComboBox.SelectedIndex = (int)trainee.GearType;
            this.drivingSchoolComboBox.SelectedIndex = (int)trainee.DrivingSchool;
            this.InstructorNameTextBox.Text = trainee.Instructor;
        }

        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {
            BE.Trainee tempTrainee = new BE.Trainee();
            try
            {
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
                if (String.IsNullOrEmpty(this.InstructorNameTextBox.Text))
                    throw new Exception("You have to fill the Instructor First Name field");
                var result = (from tester in bl.GetTesters()
                              where (tester.FirstName + " " + tester.LastName) == this.InstructorNameTextBox.Text
                              select tester).FirstOrDefault();
                if (result == null)
                    throw new Exception("there is no tester with such a name");


                tempTrainee = new BE.Trainee();
                tempTrainee.Address = new BE.Address();
                tempTrainee.Name = new BE.Name();
                tempTrainee.ID = trainee.ID;//the ID CANNOT change so it takes the id from before
                tempTrainee.Name.FirstName = this.firstNameTextBox1.Text;
                tempTrainee.Name.LastName = this.lastNameTextBox.Text;
                tempTrainee.Address.City = this.cityTextBox.Text;
                tempTrainee.Address.StreetName = this.streetNameTextBox.Text;
                tempTrainee.Address.Number = Convert.ToInt32(this.numberTextBox.Text);
                tempTrainee.DayOfBirth = trainee.DayOfBirth;//the DayOfBirth CANNOT change so we take the old one
                tempTrainee.Gender = (BE.Gender)this.genderComboBox.SelectedValue;
                tempTrainee.LessonsNb = Convert.ToInt32(this.lessonsNbTextBox.Text);
                tempTrainee.CarTrained = (BE.CarType)this.carTrainedComboBox.SelectedValue;
                tempTrainee.GearType = (BE.GearType)this.gearTypeComboBox.SelectedValue;
                tempTrainee.DrivingSchool = (BE.SchoolName)this.drivingSchoolComboBox.SelectedValue;
                tempTrainee.Instructor = this.InstructorNameTextBox.Text;


                if (!bl.TraineeExist(tempTrainee))
                    throw new Exception("This trainee doesn't exist...");
                if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                    throw new Exception("Trainee under 18 years");

                bl.UpdateTrainee(tempTrainee);
                MessageBox.Show("Successfully updated trainee!");
                this.NavigationService.Navigate(new FirstPage());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
