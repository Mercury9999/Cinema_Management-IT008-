using CinemaManagement.ViewModel.AdminVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CinemaManagement
{
    /// <summary>
    /// Interaction logic for MainNavigation.xaml
    /// </summary>
    public partial class MainNavigation : Window
    {
        public MainNavigation()
        {
            InitializeComponent();
        }

        private void Closebutton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
