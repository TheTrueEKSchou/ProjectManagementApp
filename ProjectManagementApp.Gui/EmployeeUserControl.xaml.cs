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
using ProjectManagementApp.EF;

namespace ProjectManagementApp.Gui
{
	/// <summary>
	/// Interaction logic for EmployeeUserControl.xaml
	/// </summary>
	public partial class EmployeeUserControl : UserControl
	{
		protected Model model;
		private Employee selectedEmployee;

		public EmployeeUserControl()
		{
			InitializeComponent();
			model = new Model();
			DataGrid_Employees.ItemsSource = model.Employees.ToList();
		}

        public void CheckStuff(Employee employee)
        {
            bool birthDateBool = DateTime.TryParse(DatePicker_BirthDate.Text, out DateTime birthDate);
            bool startDateBool = DateTime.TryParse(DatePicker_StartDate.Text, out DateTime startDate);
            decimal salary = decimal.TryParse(TextBox_Salary.Text, out decimal amount) ? amount : 0.0m;
            if(birthDateBool == true && startDateBool == true)//if(a == b == c == true)?
            {
                if (!string.IsNullOrWhiteSpace(TextBox_FirstName.Text) && !string.IsNullOrWhiteSpace(TextBox_LastName.Text) && !string.IsNullOrWhiteSpace(TextBox_Ssn.Text))
                {
                    employee.FirstName = TextBox_FirstName.Text;
                    employee.LastName = TextBox_LastName.Text;
                    employee.BirthDate = birthDate;
                    employee.StartDate = startDate;
                    employee.Ssn = TextBox_Ssn.Text;
                    employee.Salary = salary;
                    ClearTextBoxes();
                }
            }
        }

		private void Button_SaveNewEmployee_Click(object sender, RoutedEventArgs e)
		{
			Employee employee = new Employee();
            CheckStuff(employee);
            model.Employees.Add(employee);
            model.SaveChanges();
            DataGrid_Employees.ItemsSource = model.Employees.ToList();
        }
		
		private void Button_UpdateEmployee_Click(object sender, RoutedEventArgs e)
		{
            if(selectedEmployee != null)
            {
                Employee employee = model.Employees.Find(selectedEmployee.Id);
                CheckStuff(employee);
                model.SaveChanges();
                DataGrid_Employees.ItemsSource = model.Employees.ToList();
            }
		}

		private void Button_UpdateContactInfo_Click(object sender, RoutedEventArgs e)
		{
            if (selectedEmployee != null)
            {
                Employee employee = model.Employees.Find(selectedEmployee.Id);
                if (employee.ContactInfo == null)
                {
                    ContactInfo contactInfo = new ContactInfo
                    {
                        Email = TextBox_Email.Text,
                        Phone = TextBox_Phone.Text
                    };
                    employee.ContactInfo = contactInfo;
                }
                else
                {
                    employee.ContactInfo.Email = TextBox_Email.Text;
                    employee.ContactInfo.Phone = TextBox_Phone.Text;
                }
                model.SaveChanges();
                DataGrid_Employees.ItemsSource = model.Employees.ToList();
                ClearTextBoxes();
            }
		}

		private void DataGrid_Employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			selectedEmployee = DataGrid_Employees.SelectedItem as Employee;
			if(selectedEmployee != null)
			{
				if (selectedEmployee.ContactInfo != null)
				{
					TextBox_Email.Text = selectedEmployee.ContactInfo.Email;
					TextBox_Phone.Text = selectedEmployee.ContactInfo.Phone;
				}
				else
				{
					TextBox_Email.Text = "";
					TextBox_Phone.Text = "";
				}
				TextBox_FirstName.Text = selectedEmployee.FirstName;
				TextBox_LastName.Text = selectedEmployee.LastName;
				DatePicker_BirthDate.Text = selectedEmployee.BirthDate.ToString();
				DatePicker_StartDate.Text = selectedEmployee.StartDate.ToString();
				TextBox_Ssn.Text = selectedEmployee.Ssn;
				TextBox_Salary.Text = selectedEmployee.Salary.ToString();
			}
		}

		private void ClearTextBoxes()
		{
			TextBox_FirstName.Text = "";
			TextBox_LastName.Text = "";
			DatePicker_BirthDate.Text = "";
			DatePicker_StartDate.Text = "";
			TextBox_Ssn.Text = "";
			TextBox_Salary.Text = "";
			TextBox_Email.Text = "";
			TextBox_Phone.Text = "";
			DataGrid_Employees.SelectedItem = null;
		}

		private void Grid_Employee_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Escape)
			{
				ClearTextBoxes();
            }
		}
	}
}