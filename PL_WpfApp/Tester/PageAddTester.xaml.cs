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
    /// Interaction logic for PageAddTester.xaml
    /// </summary>
    public partial class PageAddTester : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        BE.Tester tester;

        public PageAddTester()
        {
            InitializeComponent();
            //----------------------- Aviad ADD this func for the comboBox ----------------------------
            this.dayOfBirthDatePicker.DisplayDateEnd = DateTime.Now;
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.expertiseComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            //-----------------------------------------------------------------------------------------
        }

        private void Click_AddTester(object sender, RoutedEventArgs e)
        {
            try
            {
                tester = new BE.Tester();
                tester.Address = new BE.Address();
                tester.Name = new BE.Name();
                tester.ID = this.iDTextBox.Text;
                tester.Name.FirstName = this.firstNameTextBox.Text;
                tester.Name.LastName = this.lastNameTextBox.Text;
                tester.Schedule = new BE.Schedule();
                tester.Address.City = this.cityTextBox.Text;
                tester.Address.StreetName = this.streetNameTextBox.Text;
                tester.Address.Number = Convert.ToInt32(this.numberTextBox.Text);
                tester.DayOfBirth = this.dayOfBirthDatePicker.DisplayDate;
                tester.Gender = (BE.Gender)this.genderComboBox.SelectedValue;
                tester.Experience =Convert.ToInt32(this.experienceTextBox.Text);
                tester.MaxDistance = Convert.ToInt32(this.maxDistanceTextBox.Text);
                //--------------------------------------------------------------------
                
                if (bl.TesterExist(tester))
                    throw new Exception("This tester already exists...");
                if (DateTime.Now.Year - tester.DayOfBirth.Year < 40)
                    throw new Exception("tester under 40 years");

                bl.AddTester(tester);
                MessageBox.Show("Successfully added tester!");
                this.NavigationService.Navigate(new PageMainTester());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
