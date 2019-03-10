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
using BE;
using System.ComponentModel;
using System.Data;

namespace PL_WpfApp
{
    //todo: make project nicer: datagrids of the same size, add grouping, doubleclick on the line of an entity that navigates to pageUpdate-entity
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();

        List<Trainee> traineesList = BL.FactorySingletonBL.getInstance().GetTrainees();
        ICollectionView groupedTrainees = new ListCollectionView(BL.FactorySingletonBL.getInstance().GetTrainees());
        public ICollectionView traineeCollectionView { get; private set; }

        List<Tester> testersList = BL.FactorySingletonBL.getInstance().GetTesters();
        ICollectionView groupedTesters = new ListCollectionView(BL.FactorySingletonBL.getInstance().GetTesters());
        public ICollectionView testerCollectionView { get; private set; }

        List<DrivingTest> testsList = BL.FactorySingletonBL.getInstance().GetDrivingTests();
        ICollectionView groupedTests = new ListCollectionView(BL.FactorySingletonBL.getInstance().GetDrivingTests());
        public ICollectionView testCollectionView { get; private set; }


        public FirstPage()
        {
            InitializeComponent();
            traineeDataGrid.ItemsSource = traineesList;
            testerDataGrid.ItemsSource = testersList;
            drivingTestDataGrid.ItemsSource = testsList;
        }
        
        //todo: add checking if all the requirments are true, then bl.GetTrainee(Trainee_ID).success=true;
        //todo: updateTest doesnt work
        //todo: remove/add test has to show messagebox of deletion
        //todo: grouping in tester dont work
        //todo: addtest doesnt control the maxtestweekly with the tester
        
        private void Click_AddTrainee(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTrainee());
        }
        private void Click_UpdateTrainee(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("Type the trainee you want to update", "Update trainee", "Trainee ID", -1, -1);
                if (id != null)
                {
                    if (bl.GetTrainee(id) != null) // if pressed "אישור"
                    {
                        this.NavigationService.Navigate(new PageUpdateTraineeAccount(bl.GetTrainee(id)));
                        
                    }
                    else
                        MessageBox.Show("The Trainee doesn't exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                        List<Trainee> traineesList = bl.GetTrainees();
                        traineeDataGrid.ItemsSource = traineesList;
                        MessageBox.Show("Deleted successfully");
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

        private void click_AddTester(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTester());
        }
        private void Click_UpdateTester(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("Type the tester you want to update", "Update tester", "Tester ID", -1, -1);
                if (id != null)
                {
                    BE.Tester tester = bl.GetTester(id);
                    if (tester != null) // if pressed "אישור"
                    {
                        this.NavigationService.Navigate(new PageUpdateTesterAccount(tester));
                    }
                    else
                        MessageBox.Show("The Tester doesn't exist");
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
                        List<Tester> testersList = bl.GetTesters();
                        testerDataGrid.ItemsSource = testersList;
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
        
        private void Click_AddTest(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTest());
        }
        private void Click_UpdateTest(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("Type the test you want to update", "Update test", "Test ID", -1, -1);
                if (id != null)
                {
                    BE.DrivingTest dr = bl.GetDrivingTest(id);
                    if (dr != null) // if pressed "OK"
                    {
                        this.NavigationService.Navigate(new PageUpdateTest(dr));
                    }
                    else
                        MessageBox.Show("The Test doesn't exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Click_RemoveTest(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("type the ID number of the Test you want to delete", "Window delete Test", "Test ID:", -1, -1);
                if (id != null)
                {
                    if (bl.DrivingTestExist(bl.GetDrivingTest(id))) // if press "אישור" so if ge goes to if.
                    {
                        bl.RemoveDrivingTest(bl.GetDrivingTest(id));
                        List<DrivingTest> testsList = bl.GetDrivingTests();
                        drivingTestDataGrid.ItemsSource = testsList;
                        MessageBox.Show("Delete successful");
                    }
                    else
                        MessageBox.Show("The Test doesn't exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (e.AddedItems[0] as ComboBoxItem).Content as string;

            testersList = bl.GetTesters();
            testerCollectionView = CollectionViewSource.GetDefaultView(testersList);
            groupedTesters = new ListCollectionView(testersList);

            switch (item)
            {
                case "Car Expertise":
                    groupedTrainees.GroupDescriptions.Add(new PropertyGroupDescription("Expertise"));
                    break;
                case "Experience":
                    groupedTrainees.GroupDescriptions.Add(new PropertyGroupDescription("Experience"));
                    break;
                default:
                    break;
            }

            testerDataGrid.ItemsSource = groupedTesters;
        }
        private void TraineeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (e.AddedItems[0] as ComboBoxItem).Content as string;

            traineesList = bl.GetTrainees();
            traineeCollectionView = CollectionViewSource.GetDefaultView(traineesList);
            groupedTrainees = new ListCollectionView(traineesList);

            switch (item)
            {
                case "Driving School":
                    groupedTrainees.GroupDescriptions.Add(new PropertyGroupDescription("DrivingSchool"));
                    break;
                case "Instructor":
                    groupedTrainees.GroupDescriptions.Add(new PropertyGroupDescription("Instructor"));
                    break;
                case "Lessons Number":
                    groupedTrainees.GroupDescriptions.Add(new PropertyGroupDescription("LessonsNb"));
                    break;
                default:
                    break;
            }

            traineeDataGrid.ItemsSource = groupedTrainees;
        }
        private void TestComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (e.AddedItems[0] as ComboBoxItem).Content as string;

            testsList = bl.GetDrivingTests();
            testCollectionView = CollectionViewSource.GetDefaultView(testsList);
            groupedTests = new ListCollectionView(testsList);

            switch (item)
            {
                case "Trainee ID":
                    groupedTests.GroupDescriptions.Add(new PropertyGroupDescription("Trainee_ID"));
                    break;
                case "Tester ID":
                    groupedTests.GroupDescriptions.Add(new PropertyGroupDescription("Tester_ID"));
                    break;
                default:
                    break;
            }

            drivingTestDataGrid.ItemsSource = groupedTests;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e) { }
    }

}
