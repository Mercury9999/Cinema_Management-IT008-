using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLySuatChieuVM : BaseViewModel
    {
        private int _masc {  get; set; }
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
    }
}
