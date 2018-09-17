using ProjectManagementApp.EF;
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

namespace ProjectManagementApp.Gui
{
    /// <summary>
    /// Interaction logic for TeamUserControl.xaml
    /// </summary>
    public partial class TeamUserControl : UserControl
    {
        protected Team selectedTeam;
        protected Employee selectedEmployee;
        protected Model model;
        public TeamUserControl()
        {
            InitializeComponent();
            model = new Model();
            DataGrid_Teams.ItemsSource = model.Teams.ToList();
            DataGrid_Employees.ItemsSource = model.Employees.ToList();
        }

        public void CheckStuff(Team team)
        {
            bool startDateBool = DateTime.TryParse(DatePicker_StartDate.Text, out DateTime startDate);
            bool endDateBool = DateTime.TryParse(DatePicker_EndDate.Text, out DateTime endDate);
            if (startDateBool == true && endDateBool == true && !string.IsNullOrWhiteSpace(TextBox_Name.Text))
            {
                team.Name = TextBox_Name.Text;
                team.Description = TextBox_Description.Text;
                team.StartDate = startDate;
                team.ExpectedEnd = endDate;
                ClearTextBoxes();
            }
        }

        private void DataGrid_Teams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTeam = DataGrid_Teams.SelectedItem as Team;
            if (selectedTeam != null)
            {
                TextBox_Name.Text = selectedTeam.Name;
                TextBox_Description.Text = selectedTeam.Description;
                DatePicker_StartDate.Text = selectedTeam.StartDate.ToString();
                DatePicker_EndDate.Text = selectedTeam.ExpectedEnd.ToString();
            }
        }

        private void Buttn_SaveNew_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team();
            CheckStuff(team);
            model.Teams.Add(team);
            model.SaveChanges();
            DataGrid_Teams.ItemsSource = model.Teams.ToList();
        }

        private void Buttn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTeam != null)
            {
                Team team = model.Teams.Find(selectedTeam.Id);
                CheckStuff(team);
                DataGrid_Teams.SelectedItem = null;
                model.SaveChanges();
                DataGrid_Teams.ItemsSource = model.Teams.ToList();
            }
        }

        private void ClearTextBoxes()
        {
            TextBox_Name.Text = "";
            TextBox_Description.Text = "";
            DatePicker_StartDate.Text = "";
            DatePicker_EndDate.Text = "";
            DataGrid_Employees.SelectedItem = null;
        }

        private void Grid_Team_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ClearTextBoxes();
                DataGrid_Teams.SelectedItem = null;
            }
        }

        private void DataGrid_Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedEmployee = DataGrid_Employees.SelectedItem as Employee;
        }

        private void Button_AddToTeam_Click(object sender, RoutedEventArgs e)
        {
            if(selectedTeam != null && selectedEmployee != null)
            {
                Employee employee = model.Employees.Find(selectedEmployee.Id);
                if (employee.TeamId == null)
                {
                    employee.TeamId = selectedTeam.Id;
                    model.SaveChanges();
                    ClearTextBoxes();
                    DataGrid_Employees.ItemsSource = model.Employees.ToList();
                }
            }
        }

        private void Button_RemoveFromTeam_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                Employee employee = model.Employees.Find(selectedEmployee.Id);
                if (employee.TeamId != null)
                {
                    employee.TeamId = null;
                    model.SaveChanges();
                    ClearTextBoxes();
                    DataGrid_Employees.ItemsSource = model.Employees.ToList();
                }
            }
        }
    }
}