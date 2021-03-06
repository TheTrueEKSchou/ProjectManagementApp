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
using ProjectManagementApp.EF;

namespace ProjectManagementApp.Gui
{
    /// <summary>
    /// Interaction logic for EmployeeUserControl.xaml
    /// </summary>
    public partial class EmployeeUserControl : UserControl
    {
        protected Model model;
        protected Employee selectedEmployee;

        public EmployeeUserControl()
        {
            InitializeComponent();
            model = new Model();
            DataGrid_Employees.ItemsSource = model.Employees.ToList();
        }

        /// <summary>
        /// Validates the users input and converts the input into the correct datatypes.
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="startDate"></param>
        /// <param name="ssn"></param>
        /// <param name="salary"></param>
        /// <returns>true if the input is valid and false if not.</returns>
        private bool ValidateEmployeeInput(out DateTime birthDate, out DateTime startDate, out string ssn, out Decimal salary)
        {
            bool firstNameBool = Validate.IsPersonNameValid(TextBox_FirstName.Text);
            bool lastNameBool = Validate.IsPersonNameValid(TextBox_LastName.Text);
            bool birthDateBool = Validate.IsPastDateValid(DatePicker_BirthDate.Text, out birthDate);
            bool startDateBool = Validate.IsDateValid(DatePicker_StartDate.Text, out startDate);
            bool ssnBool = Validate.IsSsnValid(TextBox_Ssn.Text, out ssn);
            bool salaryBool = Validate.IsSalaryValid(TextBox_Salary.Text, out salary);
            if(firstNameBool && lastNameBool && birthDateBool && startDateBool && ssnBool && salaryBool)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateContactInput(out string phone)
        {
            bool emailBool = Validate.IsEmailValid(TextBox_Email.Text);
            bool phoneBool = Validate.IsPhoneValid(TextBox_Phone.Text, out phone);
            if(emailBool && phoneBool)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Button_SaveNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool inputIsValid = ValidateEmployeeInput(out DateTime birthDate, out DateTime startDate, out string ssn, out decimal salary);
            if (inputIsValid)
            {
                try
                {
                    Employee employee = new Employee
                    {
                        FirstName = TextBox_FirstName.Text,
                        LastName = TextBox_LastName.Text,
                        BirthDate = birthDate,
                        StartDate = startDate,
                        Ssn = ssn,
                        Salary = salary
                    };
                    model.Employees.Add(employee);
                    model.SaveChanges();
                    ClearTextBoxes();
                    DataGrid_Employees.ItemsSource = model.Employees.ToList();
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
        
        private void Button_UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee != null)
            {
                bool inputIsValid = ValidateEmployeeInput(out DateTime birthDate, out DateTime startDate, out string ssn, out decimal salary);
                if (inputIsValid)
                {
                    try
                    {
                        Employee employee = model.Employees.Find(selectedEmployee.Id);
                        employee.FirstName = TextBox_FirstName.Text;
                        employee.LastName = TextBox_LastName.Text;
                        employee.BirthDate = birthDate;
                        employee.StartDate = startDate;
                        employee.Ssn = ssn;
                        employee.Salary = salary;
                        model.SaveChanges();
                        ClearTextBoxes();
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
            else
            {
                MessageBox.Show("Du har ikke valgt en ansat.");
            }
        }

        private void Button_RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEmployee != null)
            {
                Employee employee = model.Employees.Find(selectedEmployee.Id);
                model.Employees.Remove(employee);
                model.SaveChanges();
                ClearTextBoxes();
                DataGrid_Employees.ItemsSource = model.Employees.ToList();
            }
            else
            {
                MessageBox.Show("Du har ikke valgt en ansat.");
            }
        }

        private void Button_UpdateContactInfo_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                bool InputIsValid = ValidateContactInput(out string phone);
                if (InputIsValid)
                {
                    Employee employee = model.Employees.Find(selectedEmployee.Id);
                    if (employee.ContactInfo == null)
                    {
                        ContactInfo contactInfo = new ContactInfo();
                        try
                        {
                            contactInfo.Email = TextBox_Email.Text;
                            contactInfo.Phone = phone;
                            employee.ContactInfo = contactInfo;
                            model.SaveChanges();
                            DataGrid_Employees.ItemsSource = model.Employees.ToList();
                            ClearTextBoxes();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Noget gik galt: "+ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            employee.ContactInfo.Email = TextBox_Email.Text;
                            employee.ContactInfo.Phone = phone;
                            model.SaveChanges();
                            DataGrid_Employees.ItemsSource = model.Employees.ToList();
                            ClearTextBoxes();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Noget gik galt: "+ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ikke alle input felter er udfyldt korrekt.");
                }
            }
            else
            {
                MessageBox.Show("Du har ikke valgt en ansat.");
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
            else if (e.Key == Key.LeftCtrl)
            {
                DataGrid_Employees.ItemsSource = model.Employees.ToList();
            }
        }
    }
}