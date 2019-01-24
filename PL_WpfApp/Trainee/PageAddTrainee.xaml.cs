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
                // we probably need to add phone numbers to trainee and tester
                ////................................

                if (bl.TraineeExist(trainee))
                    throw new Exception("This trainee already exists...");
                if (DateTime.Now.Year - trainee.DayOfBirth.Year < 18)
                    throw new Exception("Trainee under 18 years");

                bl.AddTrainee(trainee);
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
