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

        private bool ValidateTeamInput(out DateTime startDate, out DateTime endDate)
        {
            bool nameBool = Validate.IsEntityNameValid(TextBox_Name.Text);
            bool startDateBool = Validate.IsDateValid(DatePicker_StartDate.Text, out startDate);
            bool endDateBool = Validate.IsDateValid(DatePicker_EndDate.Text, out endDate);
            if(nameBool && startDateBool && endDateBool)
            {
                return true;
            }
            else
            {
                return false;
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

        private void Button_SaveNew_Click(object sender, RoutedEventArgs e)
        {
            bool inputIsValid = ValidateTeamInput(out DateTime startDate, out DateTime endDate);
            if (inputIsValid)
            {
                try
                {
                    Team team = new Team
                    {
                        Name = TextBox_Name.Text,
                        Description = TextBox_Description.Text,
                        StartDate = startDate,
                        ExpectedEnd = endDate
                    };
                    model.Teams.Add(team);
                    model.SaveChanges();
                    ClearTextBoxes();
                    DataGrid_Teams.ItemsSource = model.Teams.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Noget gik galt: "+ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ikke alle input felter er udfyldt korrekt.");
            }
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTeam != null)
            {
                bool inputIsValid = ValidateTeamInput(out DateTime startDate, out DateTime endDate);
                if (inputIsValid)
                {
                    try
                    {
                        Team team = model.Teams.Find(selectedTeam.Id);
                        team.Name = TextBox_Name.Text;
                        team.Description = TextBox_Description.Text;
                        team.StartDate = startDate;
                        team.ExpectedEnd = endDate;
                        model.SaveChanges();
                        ClearTextBoxes();
                        DataGrid_Teams.ItemsSource = model.Teams.ToList();
                        DataGrid_Employees.ItemsSource = model.Employees.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Noget gik galt: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Ikke alle input felter er udfyldt korrekt.");
                }
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTeam != null)
            {
                Team team = model.Teams.Find(selectedTeam.Id);
                List<Employee> employees = model.Employees.ToList();
                foreach(Employee employee in employees)
                {
                    if(employee.TeamId == team.Id)
                    {
                        employee.TeamId = null;
                    }
                }
                model.Teams.Remove(team);
                model.SaveChanges();
                ClearTextBoxes();
                DataGrid_Teams.ItemsSource = model.Teams.ToList();
                DataGrid_Employees.ItemsSource = model.Employees.ToList();
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