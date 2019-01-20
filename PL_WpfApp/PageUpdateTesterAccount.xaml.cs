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
    /// Interaction logic for PageUpdateTesterAccount.xaml
    /// </summary>
    public partial class PageUpdateTesterAccount : Page
    {
        BE.Tester tester;
        BL.IBL bl;

        public PageUpdateTesterAccount()
        {
            InitializeComponent();

            tester = new BE.Tester();
            this.grid1.DataContext = tester;
            bl = BL.FactorySingletonBL.getInstance();

            tester.Address = new BE.Address();
            tester.Name = new BE.Name();
            tester.Schedule = new BE.Schedule();
            this.expertiseComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));

            this.firstNameTextBox.Text = tester.Name.FirstName;
        }

        private void Click_GoBackToMainTester(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateTester(tester);
                tester = new BE.Tester();
                this.grid1.DataContext = tester;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            this.NavigationService.Navigate(new PageMainTester());
        }
    }
}
