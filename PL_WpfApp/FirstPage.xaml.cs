﻿using System;
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

namespace PL_WpfApp
{
    //todo: IMPORTANT in UpdateTestPage, remove Success and Add a table of requirments with checkboxes and in WPF check if most of them are checked and change the field in trainee that he successed
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        BL.IBL bl = BL.FactorySingletonBL.getInstance();

        //todo: lists for all the entities and also icollectionview for everyone
        List<Trainee> traineesList = BL.FactorySingletonBL.getInstance().GetTrainees();
        ICollectionView groupedTrainees = new ListCollectionView(BL.FactorySingletonBL.getInstance().GetTrainees());

        public FirstPage()
        {//todo: binding the data grid that updates itself depending on the list 
            InitializeComponent();
            traineeDataGrid.ItemsSource = traineesList;
        }

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
                        List<Trainee> traineesList = BL.FactorySingletonBL.getInstance().GetTrainees();
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

        private void Click_AddTest(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageAddTest());
        }

        private void Click_UpdateTester(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = Interaction.InputBox("Type the tester you want to update", "Update tester", "Tester ID", -1, -1);
                if (id != null)
                {
                    BE.Tester tester = bl.GetTester(id);
                    if (bl.TesterExist(tester)) // if pressed "אישור"
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

        /*todo: define item source for every datagrid. erase all the lines and leave only te ones with x:Name...... and add to that line binding= the binding that its inside
        */
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        //todo: Grouping, the first time i select a comboboxItem it works, from the second time on it doesnt
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (e.AddedItems[0] as ComboBoxItem).Content as string;
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
    }

}
