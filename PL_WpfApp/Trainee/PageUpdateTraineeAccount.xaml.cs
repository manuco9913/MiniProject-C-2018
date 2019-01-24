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
        BE.Trainee trainee = new BE.Trainee();

        public PageUpdateTraineeAccount()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.carTrainedComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            this.gearTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearType));
            this.drivingSchoolComboBox.ItemsSource = Enum.GetValues(typeof(BE.SchoolName));


            trainee.Address = new BE.Address();
            trainee.Name = new BE.Name();

        }

        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
