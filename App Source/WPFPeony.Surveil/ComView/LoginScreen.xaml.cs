using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFPeony.Surveil
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        public LoginScreen()
        {
            InitializeComponent();
            DataContextChanged += LoginScreen_DataContextChanged;
        }

        private void LoginScreen_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}