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
        BL.IBL bl = BL.FactorySingletonBL.getInstance();

        public PageMainTrainee()
        {
            InitializeComponent();
        }

        private void Update_TraineeAccount(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageUpdateTraineeAccount(null));
        }

        private void Add_Test(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTest());
        }

        private void Update_Test(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageUpdateTest(null));//change from null to driving test
        }

        private void Remove_TraineeAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("type the ID number of the Trainee you want to delete", "Window delete trainee", "Trainee ID:", -1, -1);
                if (id != null)
                {
                    if (bl.TraineeExist(bl.GetTrainee(id))) // if press "אישור" so if ge goes to if.
                    {
                        bl.RemoveTrainee(bl.GetTrainee(id));
                        MessageBox.Show("Delete successful");
                    }
                    else
                        MessageBox.Show("The Trainee doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
