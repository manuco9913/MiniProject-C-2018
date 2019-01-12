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
        public PageAddTrainee()
        {
            InitializeComponent();

        }

        private void Update_TraineeInformation(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageMainTrainee());
        }


    }
}
