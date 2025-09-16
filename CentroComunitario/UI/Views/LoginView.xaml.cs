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
using Core.Interfaces;
using Core.Services;
using Core.Repositories;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private IAuthService _authService;

        public LoginView()
        {
            InitializeComponent();
            _authService = new AuthService(new UserRepository());
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            bool success = _authService.Login(
                UsernameTextBox.Text,
                LoginPasswordBox.Password
            );

            if (success)
            {
                this.Hide();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Closed += new EventHandler(delegate(object s, EventArgs args)
                {
                    this.Show(); 
                });

                mainWindow.Show();
            }
            else
            {
                MessageBox.Show(
                    "Invalid username or password. Please try again.",
                    "Login Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
