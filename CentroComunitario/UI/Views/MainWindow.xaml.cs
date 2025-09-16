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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Views;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitSystemMenuItem(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CoursesMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TeachersMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StudentsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
