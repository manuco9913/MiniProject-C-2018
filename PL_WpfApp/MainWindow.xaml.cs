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
        IBL myBl; 
        public MainWindow()
        {
            InitializeComponent();
            myBl = BL.FactorySingletonBL.getInstance();
          
        }

        private void ShowPersons_Click(object sender, RoutedEventArgs e)
        {
             datagrid.ItemsSource = new ObservableCollection<Person>(myBl.GetAllPersons());
        }
    }
}
