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


            trainee.Address = new BE.Address();
            trainee.Name = new BE.Name();
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
        }

        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {
            BE.Trainee tempTrainee = new BE.Trainee();
            try
            { 
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


                if (!bl.TraineeExist(tempTrainee))
                    throw new Exception("This trainee doesn't exist...");
                if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                    throw new Exception("Trainee under 18 years");

                bl.AddTrainee(tempTrainee);
                MessageBox.Show("Successfully added trainee!");
                this.NavigationService.Navigate(new PageMainTrainee());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
