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

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : Window
    {
        public WelcomeView()
        {
            InitializeComponent();
        }

        private void LoginButton_CLick(object sender, RoutedEventArgs e)
        {
            this.Hide();

            LoginView loginView = new LoginView();
            loginView.Closed += new EventHandler(delegate(object s, EventArgs args)
            {
                this.Show();
            });

            loginView.Show();
        }

        private void AboutUsButton_Click(object sender, RoutedEventArgs e)
        {
            AboutUsView aboutUsView = new AboutUsView();
            aboutUsView.Owner = this;
            aboutUsView.Closed += new EventHandler(delegate(object s, EventArgs args)
            {
                this.Show();
            });

            aboutUsView.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult exitConfirmationResult = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (exitConfirmationResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
