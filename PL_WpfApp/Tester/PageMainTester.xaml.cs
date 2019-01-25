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
    /// Interaction logic for PageMainTester.xaml
    /// </summary>
    public partial class PageMainTester : Page
    {
        public PageMainTester()
        {
            InitializeComponent();
        }

        private void Update_TesterAccount(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageUpdateTesterAccount(null));
        }

        private void Update_Test(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageUpdateTest(null));
        }
    }
}
