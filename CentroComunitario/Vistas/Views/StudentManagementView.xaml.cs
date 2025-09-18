using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClasesBase.Repositories;
using ClasesBase.Interfaces;
using ClasesBase;
using System.Text.RegularExpressions;
using ClasesBase.Services;

namespace Vistas.Views
{
    /// <summary>
    /// Interaction logic for StudentManagementView.xaml
    /// </summary>
    public partial class StudentManagementView : Window
    {
        private IStudentRepository _studentRepository;
        private IStudentService _studentService;
        private Student _selectedStudent;

        public StudentManagementView()
        {
            InitializeComponent();
            _studentRepository = new StudentRepository();
            _studentService = new StudentService(_studentRepository);
            RefreshDataGrid();
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStudent = StudentDataGrid.SelectedItem as Student;

            if (_selectedStudent != null)
            {
                StudentDNITextBox.Text = _selectedStudent.DNI;
                StudentNameTextBox.Text = _selectedStudent.Name;
                StudentLastNameTextBox.Text = _selectedStudent.LastName;
                StudentEmailTextBox.Text = _selectedStudent.Email;

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                StudentGroupBox.Header = "Detalles";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent;

            if (TryGetStudentFromForm(out newStudent))
            {
                MessageBoxResult addStudentConfirmation = MessageBox.Show(
                    "Are you sure you want to save the student?",
                    "Save Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );
                
                if (addStudentConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _studentService.AddStudent(newStudent);
                        RefreshDataGrid();

                        MessageBox.Show(
                            "Student saved successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error saving student: {0}", ex.Message),
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                    }
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent == null)
                return;

            Student updatedStudent;

            if (TryGetStudentFromForm(out updatedStudent))
            {
                updatedStudent.Id = _selectedStudent.Id;

                var updateStudentConfirmation = MessageBox.Show(
                    "Are you sure you want to update the student?",
                    "Update Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (updateStudentConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _studentService.UpdateStudent(updatedStudent);
                        RefreshDataGrid();
                        MessageBox.Show(
                            "Student updated successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error updating student: {0}", ex.Message),
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                        );
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent == null)
            {
                MessageBox.Show(
                    "Please select a student to delete.",
                    "Selection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            MessageBoxResult deleteStudentConfirmation = MessageBox.Show(
                "Are you sure you want to delete the student?",
                "Delete Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (deleteStudentConfirmation == MessageBoxResult.Yes)
            {
                _studentService.DeleteStudent(_selectedStudent.Id);
                RefreshDataGrid();
                MessageBox.Show(
                    "Student deleted successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshDataGrid()
        {
            StudentDataGrid.ItemsSource = null;
            StudentDataGrid.ItemsSource = _studentService.GetAllStudents();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            StudentDNITextBox.Text = string.Empty;
            StudentNameTextBox.Text = string.Empty;
            StudentLastNameTextBox.Text = string.Empty;
            StudentEmailTextBox.Text = string.Empty;
            StudentGroupBox.Header = "New Student";

            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private bool TryGetStudentFromForm(out Student student)
        {
            student = null;

            string dni = StudentDNITextBox.Text.Trim();
            string name = StudentNameTextBox.Text.Trim();
            string lastName = StudentLastNameTextBox.Text.Trim();
            string email = StudentEmailTextBox.Text.Trim();

            int dniParsed;
            if (string.IsNullOrWhiteSpace(dni) || !int.TryParse(dni, out dniParsed) || dniParsed < 0)
            {
                MessageBox.Show(
                    "DNI must be a positive number.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Name is required.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show(
                    "Last Name is required.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show(
                    "A valid Email is required.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            student = new Student
            {
                DNI = dni,
                Name = name,
                LastName = lastName,
                Email = email
            };

            return true;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
    }
}
