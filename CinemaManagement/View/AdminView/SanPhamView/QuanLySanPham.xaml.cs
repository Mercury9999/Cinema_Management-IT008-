using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CinemaManagement.View
{
    /// <summary>
    /// Interaction logic for QuanLySanPham.xaml
    /// </summary>
    /// 

    public partial class QuanLySanPham : Page
    {
        public QuanLySanPham()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new MyItem()
            {
                Name = "Banh mi",
                Count = "1000",
                Price = "40000"
            };

            items.Add(newItem);
        }

        private void ComboBox_SuggestionChosen(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
