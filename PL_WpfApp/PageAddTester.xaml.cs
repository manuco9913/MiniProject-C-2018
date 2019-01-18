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
        BE.Tester tester;
        BL.IBL bl;

        public PageAddTester()
        {
            bl = BL.FactorySingletonBL.getInstance();
            InitializeComponent();
            tester = new BE.Tester();
            tester.Address = new BE.Address();
            tester.Name = new BE.Name();
            tester.Schedule = new BE.Schedule();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));

            //this.grid1.DataContext = tester;
            //this.firstNameTextBox.Text = tester.Name.FirstName;
        }

        private void Update_TesterInformation(object sender, RoutedEventArgs e)
        {
            bl.AddTester(tester);
            this.NavigationService.Navigate(new PageMainTester());
        }
    }
}
