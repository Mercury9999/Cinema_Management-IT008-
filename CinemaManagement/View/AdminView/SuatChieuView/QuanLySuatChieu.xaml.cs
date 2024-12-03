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
    /// Interaction logic for QuanLySuatChieu.xaml
    /// </summary>
    public partial class QuanLySuatChieu : Page
    {
        public class SuatChieu
        {
            public string Ten { get; set; }
            public string TheLoai { get; set; }
            public string ThoiLuong { get; set; }

        }

        public ObservableCollection<SuatChieu> testData;
        public QuanLySuatChieu()
        {
            InitializeComponent();
            testData = new ObservableCollection<SuatChieu>
            {
                new SuatChieu { Ten = "Chu meo con", TheLoai = "Hoat hinh", ThoiLuong = "120" },
                new SuatChieu { Ten = "Hoang ha Dao Chu", TheLoai = "Tien hiep", ThoiLuong = "130" }
            };
            DataContext = this;
            showtimeGrid.ItemsSource = testData;

        }

    }
}
