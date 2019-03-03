using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //todo: check if all the add/remove/update work
        IBL myBl; 
        public MainWindow()
        {
            InitializeComponent();
            myBl = BL.FactorySingletonBL.getInstance();
            MainFrame.NavigationService.Navigate(new FirstPage());
        }



        private void ShowPersons_Click(object sender, RoutedEventArgs e)
        {
            // datagrid.ItemsSource = new ObservableCollection<Person>(myBl.GetAllPersons());
        }

        private void Go_BackPage(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
            else
            {
                //this.Button_back.Visibility =Visibility.Hidden;
                MessageBox.Show("Cannot go back from here", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
