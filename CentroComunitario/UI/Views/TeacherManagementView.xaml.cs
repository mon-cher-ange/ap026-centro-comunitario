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
using Core;
using Core.Repositories;
using Core.Interfaces;
using Core.Services;
using System.Text.RegularExpressions;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for TeacherManagementView.xaml
    /// </summary>
    public partial class TeacherManagementView : Window
    {
        private ITeacherRepository _teacherRepository;
        private ITeacherService _teacherService;
        private Teacher _selectedTeacher;

        public TeacherManagementView()
        {
            InitializeComponent();
            _teacherRepository = new TeacherRepository();
            _teacherService = new TeacherService(_teacherRepository);
            RefreshDataGrid();
        }

        private void TeacherDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedTeacher = TeacherDataGrid.SelectedItem as Teacher;

            if (_selectedTeacher != null)
            {
                TeacherDNITextBox.Text = _selectedTeacher.DNI;
                TeacherNameTextBox.Text = _selectedTeacher.Name;
                TeacherLastNameTextBox.Text = _selectedTeacher.LastName;
                TeacherEmailTextBox.Text = _selectedTeacher.Email;

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                TeacherGroupBox.Header = "Details";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Teacher newTeacher;

            if (TryGetTeacherFromForm(out newTeacher))
            {
                MessageBoxResult addTeacherConfirmation = MessageBox.Show(
                    "Are you sure you want to save the teacher?",
                    "Save Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (addTeacherConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _teacherService.AddTeacher(newTeacher);
                        RefreshDataGrid();

                        MessageBox.Show(
                            "Teacher saved successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error saving teacher: {0}", ex.Message),
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
            if (_selectedTeacher == null)
                return;

            Teacher updatedTeacher;

            if (TryGetTeacherFromForm(out updatedTeacher))
            {
                updatedTeacher.Id = _selectedTeacher.Id;

                var updateTeacherConfirmation = MessageBox.Show(
                    "Are you sure you want to update the teacher?",
                    "Update Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (updateTeacherConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _teacherService.UpdateTeacher(updatedTeacher);
                        RefreshDataGrid();
                        MessageBox.Show(
                            "Teacher updated successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error updating teacher: {0}", ex.Message),
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
            if (_selectedTeacher == null)
            {
                MessageBox.Show(
                    "Please select a teacher to delete.",
                    "Selection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            MessageBoxResult deleteTeacherConfirmation = MessageBox.Show(
                "Are you sure you want to delete the teacher?",
                "Delete Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (deleteTeacherConfirmation == MessageBoxResult.Yes)
            {
                _teacherService.DeleteTeacher(_selectedTeacher.Id);
                RefreshDataGrid();
                MessageBox.Show(
                    "Teacher deleted successfully!",
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
            TeacherDataGrid.ItemsSource = null;
            TeacherDataGrid.ItemsSource = _teacherService.GetAllTeachers();
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            TeacherDNITextBox.Text = string.Empty;
            TeacherNameTextBox.Text = string.Empty;
            TeacherLastNameTextBox.Text = string.Empty;
            TeacherEmailTextBox.Text = string.Empty;
            TeacherGroupBox.Header = "New Teacher";

            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private bool TryGetTeacherFromForm(out Teacher teacher)
        {
            teacher = null;

            string dni = TeacherDNITextBox.Text.Trim();
            string name = TeacherNameTextBox.Text.Trim();
            string lastName = TeacherLastNameTextBox.Text.Trim();
            string email = TeacherEmailTextBox.Text.Trim();

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

            teacher = new Teacher
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
