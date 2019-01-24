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
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();
        public FirstPage()
        {
            InitializeComponent();
        }

        private void Click_AddTrainee(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTrainee());
        }

        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new PageUpdateTraineeAccount());
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

        private void Remove_TesterAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("type the ID number of the Tester you want to delete", "Window delete Tester", "Tester ID:", -1, -1);
                if (id != null)
                {
                    if (bl.TesterExist(bl.GetTester(id))) // if press "אישור" so if ge goes to if.
                    {
                        bl.RemoveTester(bl.GetTester(id));

                        MessageBox.Show("Delete successful");
                    }
                    else
                        MessageBox.Show("The Tester doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void click_AddTester(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTester());
        }
    }
}
