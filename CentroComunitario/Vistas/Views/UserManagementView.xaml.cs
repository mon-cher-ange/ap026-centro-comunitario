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
using ClasesBase.Services;

namespace Vistas.Views
{
    /// <summary>
    /// Interaction logic for UserManagementView.xaml
    /// </summary>
    public partial class UserManagementView : Window
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private User _selectedUser;

        public UserManagementView()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
            _userService = new UserService(_userRepository);
            RefreshDataGrid();
            PopulateRoleComboBoxes();
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUser = UserDataGrid.SelectedValue as User;

            if (_selectedUser != null)
            {
                UserUsernameTextBox.Text = _selectedUser.Username;
                UserSecretPasswordBox.Password = _selectedUser.Password;
                UserFullnameTextBox.Text = _selectedUser.FullName;
                UserRoleComboBox.SelectedValue = _selectedUser.RoleId;

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                UserGroupBox.Header = "Detalles";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            User newUser;

            if (TryToGetUserFromForm(out newUser))
            {
                MessageBoxResult addUserConfirmation = MessageBox.Show(
                    "Are you sure you want to save the user?",
                    "Save Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (addUserConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _userService.AddUser(newUser);
                        RefreshDataGrid();

                        MessageBox.Show(
                            "User saved successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error saving user: {0}", ex.Message),
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
            if (_selectedUser== null)
                return;

            User updatedUser;

            if (TryToGetUserFromForm(out updatedUser))
            {
                updatedUser.Id = _selectedUser.Id;

                var updateUserConfirmation = MessageBox.Show(
                    "Are you sure you want to update the user?",
                    "Update Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (updateUserConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        _userService.UpdateUser(updatedUser);
                        RefreshDataGrid();
                        MessageBox.Show(
                            "Userupdated successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show(
                            string.Format("Error updating user: {0}", ex.Message),
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
            if (_selectedUser == null)
            {
                MessageBox.Show(
                    "Please select a user to delete.",
                    "Selection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            MessageBoxResult deleteUserConfirmation = MessageBox.Show(
                "Are you sure you want to delete the user?",
                "Delete Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (deleteUserConfirmation == MessageBoxResult.Yes)
            {
                _userService.DeleteUser(_selectedUser.Id);
                RefreshDataGrid();
                MessageBox.Show(
                    "User deleted successfully!",
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
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _userService.GetAllUsers();
            ClearAllFields();
        }

        private void PopulateRoleComboBoxes()
        {
            List<Role> roles = new List<Role>()
            {
                new Role
                {
                    Id = 1,
                    Description = "Administrador"
                },
                new Role
                {
                    Id = 2,
                    Description = "Teacher"
                },
                new Role
                {
                    Id = 3,
                    Description = "Recepción"
                }
            };

            UserRoleComboBox.ItemsSource = roles;
            UserRoleComboBox.DisplayMemberPath = "Description";
            UserRoleComboBox.SelectedValuePath = "Id";
            UserRoleComboBox.SelectedIndex = 0;
        }

        private void ClearAllFields()
        {
            UserUsernameTextBox.Text = string.Empty;
            UserSecretPasswordBox.Password = string.Empty;
            UserFullnameTextBox.Text = string.Empty;
            UserRoleComboBox.SelectedIndex = 0;
            UserGroupBox.Header = "Nuevo Usuario";

            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private bool TryToGetUserFromForm(out User user)
        {
            user = null;

            string username = UserUsernameTextBox.Text.Trim();
            string password = UserSecretPasswordBox.Password;
            string fullName = UserFullnameTextBox.Text.Trim();
            int roleId = (int)UserRoleComboBox.SelectedValue;
            
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "El nombre de usuario es obligatorio",
                    "Error de validación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "La contraseña es obligatoria",
                    "Error de validación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show(
                    "El nombre completo es obligatorio",
                    "Error de validación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            if (roleId < 0)
            {
                MessageBox.Show(
                    "El rol de usuario es obligatorio",
                    "Error de validación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return false;
            }

            user = new User
            {
                Username = username,
                Password = password,
                FullName = fullName,
                RoleId = roleId
            };

            return true;
        }
    }
}
