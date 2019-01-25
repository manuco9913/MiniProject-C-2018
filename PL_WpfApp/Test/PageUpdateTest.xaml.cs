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
            this.iDTextBox.Text = drivingTest.ID; // why does not working ???
            this.tester_IDTextBox.Text = drivingTest.Tester_ID;
            this.trainee_IDTextBox.Text = drivingTest.Trainee_ID;
            this.dateDatePicker.DisplayDateEnd = DateTime.Now;
            this.dateDatePicker.DisplayDate = drivingTest.Date;
            this.timeTextBox.Text = drivingTest.Time.ToString();



        }
        private void Click_UpdateTest(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FirstPage());
        }
    }
}