using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private int _soPhong;


        public int SoPhong
        {
            get { return _soPhong; }
            set { _soPhong = value; OnPropertyChanged(); }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        public System.DateTime BatDau { get; set; }
        public System.DateTime KetThuc { get; set; }
        public decimal GiaVe { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        public ICommand LoadDataShowTimeCM { get; set; }
        private ObservableCollection<SuatChieuDTO> _dsSuatChieu;
        public ObservableCollection<SuatChieuDTO> dsSuatChieu
        {

            get { return _dsSuatChieu; }
            set { _dsSuatChieu = value; OnPropertyChanged(); }
        }

        public QuanLySuatChieuVM()
        {
            LoadDataShowTimeCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;

                    var data = await SuatChieuDAL.Instance.GetShowTimeByRoom(SoPhong, SelectedDate);
                    dsSuatChieu = new ObservableCollection<SuatChieuDTO>(data);

                    IsLoading = false;
                }
                catch (Exception ex)
                {

                    CustomControls.MyMessageBox.Show("Lỗi hệ thống");

                    Console.WriteLine($"Error: {ex.Message}");
                }
            });
        }
    }
}
