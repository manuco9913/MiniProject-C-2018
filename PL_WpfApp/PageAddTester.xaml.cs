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
        BL.IBL instance;

        public PageAddTester()
        {
            InitializeComponent();
            tester = new BE.Tester();
            //this.grid1.DataContext = tester;
            //instance = new BL.FactorySingletonBL.getInstance();
        }

        private void Update_TesterInformation(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageMainTester());
        }
    }
}
