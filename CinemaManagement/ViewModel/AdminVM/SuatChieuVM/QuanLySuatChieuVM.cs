using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLySuatChieuVM : BaseViewModel
    {
        private int _masc { get; set; }
        public int MaSC
        {
            get { return _masc; }
            set { _masc = value; OnPropertyChanged(); }
        }
        private int _maphim;
        public int MaPhim
        {
            get { return _maphim; }
        }
        public int SoPhongChieu { get; set; }
        public System.DateTime BatDau { get; set; }
        public System.DateTime KetThuc { get; set; }
        public decimal GiaVe { get; set; }
        private int _currentroom { get; set; }
        public int CurrentRoom
        {
            get { return _currentroom; }
            set { _currentroom = value; OnPropertyChanged(); }
        }
        DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        public ICommand DoiPhongChieuCM {  get; set; }
        private ObservableCollection<SuatChieuDTO> _dsSuatChieu;
        public ObservableCollection<SuatChieuDTO> dsSuatChieu
        {

            get { return _dsSuatChieu; }
            set { _dsSuatChieu = value; OnPropertyChanged(); }
        }

        public QuanLySuatChieuVM()
        {
            DoiPhongChieuCM = new RelayCommand<object>((p) => true, (p) =>
            {
                MessageBox.Show("Done");
            });
        }
    }
}
