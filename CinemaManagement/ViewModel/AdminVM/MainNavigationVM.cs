using CinemaManagement.View;
using System.Windows.Input;


namespace CinemaManagement.ViewModel.AdminVM
{
    class MainNavigationVM : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; }

        public MainNavigationVM() => CurrentViewModel = new QuanLyPhimVM();





        //private object _currentView;

        //public object CurrentView
        //{
        //    get { return _currentView; }
        //    set { _currentView = value; OnPropertyChanged(); }
        //}

        //public ICommand QuanLyPhimCommand { get; set; }
        //public ICommand QuanLySanPhamCommand { get; set; }
        //public ICommand QuanLySuatChieuCommand { get; set; }
        //public ICommand QuanLyNhanVienCommand { get; set; }

        //private void QuanLyPhim(object obj) => CurrentView = new QuanLyPhimVM();
        //private void QuanLySanPham(object obj) => CurrentView = new QuanLySanPhamVM();
        //private void QuanLySuatChieu(object obj) => CurrentView = new QuanLySuatChieuVM();
        //private void QuanLyNhanVien(object obj) => CurrentView = new QuanLyNhanVienVM();

        //public MainNavigationVM()
        //{
        //    _currentView = new QuanLyPhimVM();


        //}





    }
}
