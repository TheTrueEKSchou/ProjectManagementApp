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
    /// Interaction logic for ProjectUserControl.xaml
    /// </summary>
    public partial class ProjectUserControl : UserControl
    {
        protected Model model;
        protected Project selectedProject;
        protected Team selectedTeam;

        public ProjectUserControl()
        {
            InitializeComponent();
            model = new Model();
            DataGrid_Projects.ItemsSource = model.Projects.ToList();
            DataGrid_Teams.ItemsSource = model.Teams.ToList();
        }

        private bool ValidateProjectInput(out DateTime startDate, out DateTime endDate, out decimal budgetLimit)
        {
            bool nameBool = Validate.IsEntityNameValid(TextBox_Name.Text);
            bool startDateBool = Validate.IsDateValid(DatePicker_StartDate.Text, out startDate);
            bool endDateBool = Validate.IsFutureDateValid(DatePicker_EndDate.Text, out endDate);
            bool budgetLimitBool = Validate.IsSalaryValid(TextBox_BudgetLimit.Text, out budgetLimit);
            if(nameBool && startDateBool && endDateBool && budgetLimitBool)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Button_SaveNewProject_Click(object sender, RoutedEventArgs e)
        {
            bool inputIsValid = ValidateProjectInput(out DateTime startDate, out DateTime endDate, out decimal budgetLimit);
            if (inputIsValid)
            {
                try
                {
                    Project project = new Project
                    {
                        Name = TextBox_Name.Text,
                        Description = TextBox_Description.Text,
                        StartDate = startDate,
                        EndDate = endDate,
                        BudgetLimit = budgetLimit
                    };
                    model.Projects.Add(project);
                    model.SaveChanges();
                    ClearTextBoxes();
                    DataGrid_Projects.ItemsSource = model.Projects.ToList();
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

        private void Button_UpdateProject_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProject != null)
            {
                bool inputIsValid = ValidateProjectInput(out DateTime startDate, out DateTime endDate, out decimal budgetLimit);
                if (inputIsValid)
                {
                    try
                    {
                        Project project = model.Projects.Find(selectedProject.Id);
                        project.Name = TextBox_Name.Text;
                        project.Description = TextBox_Description.Text;
                        project.StartDate = startDate;
                        project.EndDate = endDate;
                        project.BudgetLimit = budgetLimit;
                        model.SaveChanges();
                        ClearTextBoxes();
                        DataGrid_Projects.ItemsSource = model.Projects.ToList();
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
        }

        private void Button_RemoveProject_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProject != null)
            {
                Project project = model.Projects.Find(selectedProject.Id);
                model.Projects.Remove(project);
                model.SaveChanges();
                ClearTextBoxes();
                DataGrid_Projects.ItemsSource = model.Projects.ToList();
            }
        }

        private void Button_AddToProject_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProject != null && selectedTeam != null)
            {
                Team team = model.Teams.Find(selectedTeam.Id);
                if (team.ProjectId == null)
                {
                    if (selectedTeam.CalculatePay()+selectedProject.CalulculatePay() <= selectedProject.BudgetLimit)
                    {
                        team.ProjectId = selectedProject.Id;
                        model.SaveChanges();
                        ClearTextBoxes();
                        DataGrid_Teams.ItemsSource = model.Teams.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Kan ikke tilsætte team da projektets budget grænse vil blive overskræden.");
                    }
                }
            }
        }

        private void Button_RemoveFromProject_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTeam != null)
            {
                Team team = model.Teams.Find(selectedTeam.Id);
                if (team.ProjectId != null)
                {
                    team.ProjectId = null;
                    model.SaveChanges();
                    ClearTextBoxes();
                    DataGrid_Teams.ItemsSource = model.Teams.ToList();
                }
            }
        }

        private void DataGrid_Projects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProject = DataGrid_Projects.SelectedItem as Project;
            if(selectedProject != null)
            {
                TextBox_Name.Text = selectedProject.Name;
                TextBox_Description.Text = selectedProject.Description;
                DatePicker_StartDate.Text = selectedProject.StartDate.ToString();
                DatePicker_EndDate.Text = selectedProject.EndDate.ToString();
                TextBox_BudgetLimit.Text = selectedProject.BudgetLimit.ToString();
            }
        }

        private void DataGrid_Teams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTeam = DataGrid_Teams.SelectedItem as Team;
        }

        private void ClearTextBoxes()
        {
            TextBox_Name.Text = "";
            TextBox_Description.Text = "";
            DatePicker_StartDate.Text = "";
            DatePicker_EndDate.Text = "";
            TextBox_BudgetLimit.Text = "";
        }

        private void Grid_Project_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ClearTextBoxes();
                DataGrid_Projects.SelectedItem = null;
            }
        }
    }
}
