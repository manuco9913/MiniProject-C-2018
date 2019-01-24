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
using Microsoft.VisualBasic;

namespace PL_WpfApp
{
    /// <summary>
    /// Interaction logic for PageMainTrainee.xaml
    /// </summary>
    public partial class PageMainTrainee : Page
    {
        public PageMainTrainee()
        {
            InitializeComponent();
        }

        private void Update_TraineeAccount(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageUpdateTraineeAccount());
        }

        private void Add_Test(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTest());
        }

        private void Update_Test(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageUpdateTest());
        }

        private void Remove_TraineeAccount(object sender, RoutedEventArgs e)
        {
            string id = Interaction.InputBox("Message", "Title", "Defult Value", -1, -1);

        }
    }
}
