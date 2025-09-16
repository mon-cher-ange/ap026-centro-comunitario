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
using Core.Repositories;
using Core.Interfaces;
using Core;
using Core.Services;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for CourseManagementView.xaml
    /// </summary>
    public partial class CourseManagementView : Window
    {
        private ICourseRepository _courseRepository;
        private ICourseService _courseService;
        private IStateRepository _stateRepository;
        private IStateService _stateService;
        private ITeacherRepository _teacherRepository;
        private ITeacherService _teacherService;
        private Course _selectedCourse;

        public CourseManagementView()
        {
            InitializeComponent();
            _courseRepository = new CourseRepository();
            _courseService = new CourseService(_courseRepository);
            _stateRepository = new StateRepository();
            _stateService = new StateService(_stateRepository);
            _teacherRepository = new TeacherRepository();
            _teacherService = new TeacherService(_teacherRepository);
            RefreshDataGrid();
            PopulateComboBoxes();
        }

        private void CourseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCourse = CourseDataGrid.SelectedItem as Course;

            if (_selectedCourse != null)
            {
                CourseNameTextBox.Text = _selectedCourse.Name;
                CourseDescriptionTextBox.Text = _selectedCourse.Description;
                CourseQuotaTextBox.Text = _selectedCourse.Quota.ToString();
                CourseStartDatePicker.SelectedDate = _selectedCourse.StartDate;
                CourseEndDatePicker.SelectedDate = _selectedCourse.EndDate;
                CourseStateComboBox.SelectedValue = _selectedCourse.StateId;
                CourseTeacherComboBox.SelectedValue = _selectedCourse.TeacherId;

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                CourseGroupBox.Header = "Details";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Course newCourse;

            if (TryGetCourseFromForm(out newCourse))
            {
                MessageBoxResult addCourseConfirmation = MessageBox.Show(
                    "Are you sure you want to save the course?",
                    "Save Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (addCourseConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _courseService.AddCourse(newCourse);
                        RefreshDataGrid();

                        MessageBox.Show(
                            "Course saved successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error saving course: {0}", ex.Message),
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
            if (_selectedCourse == null)
                return;

            Course updatedCourse;

            if (TryGetCourseFromForm(out updatedCourse))
            {
                updatedCourse.Id = _selectedCourse.Id;

                var updateCourseConfirmation = MessageBox.Show(
                    "Are you sure you want to update the course?",
                    "Update Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (updateCourseConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _courseService.UpdateCourse(updatedCourse);
                        RefreshDataGrid();
                        MessageBox.Show(
                            "Course updated successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error updating course: {0}", ex.Message),
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
            if (_selectedCourse == null)
            {
                MessageBox.Show(
                    "Please select a course to delete.",
                    "Selection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            MessageBoxResult deleteCourseConfirmation = MessageBox.Show(
                "Are you sure you want to delete the course?",
                "Delete Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (deleteCourseConfirmation == MessageBoxResult.Yes)
            {
                _courseService.DeleteCourse(_selectedCourse.Id);
                RefreshDataGrid();
                MessageBox.Show(
                    "Course deleted successfully!",
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
            CourseDataGrid.ItemsSource = null;
            CourseDataGrid.ItemsSource = _courseService.GetAllCourses();
            ClearAllFields();
        }

        private void PopulateComboBoxes()
        {
            CourseStateComboBox.ItemsSource = _stateService.GetAllStates();
            CourseStateComboBox.DisplayMemberPath = "Name";
            CourseStateComboBox.SelectedValuePath = "Id";
            CourseStateComboBox.SelectedIndex = 0;
            CourseTeacherComboBox.ItemsSource = _teacherService.GetAllTeachers();
            CourseTeacherComboBox.SelectedValuePath = "Id";
            CourseTeacherComboBox.SelectedIndex = 0;
        }


        private void ClearAllFields()
        {
            CourseNameTextBox.Text = string.Empty;
            CourseDescriptionTextBox.Text = string.Empty;
            CourseQuotaTextBox.Text = string.Empty;
            CourseStartDatePicker.SelectedDate = null;
            CourseEndDatePicker.SelectedDate = null;
            CourseGroupBox.Header = "New Course";

            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private bool TryGetCourseFromForm(out Course course)
        {
            course = null;

            string name = CourseNameTextBox.Text.Trim();
            string description = CourseDescriptionTextBox.Text.Trim();
            int quota;
            DateTime startDate, endDate;

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

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show(
                    "Course description is required.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (!int.TryParse(CourseQuotaTextBox.Text, out quota) || quota <= 0)
            {
                MessageBox.Show(
                    "Quota must be a positive integer.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (!DateTime.TryParse(CourseStartDatePicker.Text, out startDate))
            {
                MessageBox.Show(
                    "Start date is invalid.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (!DateTime.TryParse(CourseEndDatePicker.Text, out endDate))
            {
                MessageBox.Show(
                    "End date is invalid.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (endDate < startDate)
            {
                MessageBox.Show(
                    "End date cannot be before start date.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (CourseStateComboBox.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a state.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (CourseTeacherComboBox.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a teacher.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            course = new Course
            {
                Name = name,
                Description = description,
                Quota = quota,
                StartDate = DateTime.Parse(CourseStartDatePicker.Text),
                EndDate = DateTime.Parse(CourseEndDatePicker.Text),
                StateId = (int)CourseStateComboBox.SelectedValue,
                TeacherId = (int)CourseTeacherComboBox.SelectedValue
            };

            return true;
        }
    }
}
