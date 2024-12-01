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

namespace CinemaManagement.View.AdminView.SanPhamView
{
    /// <summary>
    /// Interaction logic for QuanLySanPhambeta.xaml
    /// </summary>
    public partial class QuanLySanPhambeta : Page
    {
        ObservableCollection<MyItem> items = new ObservableCollection<MyItem>();

        public class MyItem
        {
            public string ImagePath { get; set; }
            public string Name { get; set; }
            public string Count { get; set; }
            public string Price { get; set; }
        }

        public QuanLySanPhambeta()
        {
            InitializeComponent();
            listBox.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new MyItem()
            {
                ImagePath = "/CinemaManagement;component/Resource/Poster/1.jpg",
                Name = "Banh mi",
                Count = "1000",
                Price = "40000"
            };

            items.Add(newItem);
        }


    }
}
