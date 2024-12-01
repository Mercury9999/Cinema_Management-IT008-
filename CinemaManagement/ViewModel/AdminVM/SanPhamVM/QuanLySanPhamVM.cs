using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLySanPhamVM : BaseViewModel
    {
        #region
        public ICommand ViewProductCM { get; set; }
        public ICommand AddProductCM { get; set; }
        public ICommand UpdateProductCM { get; set; }
        public ICommand DeleteProductCM { get; set; }

        public ICommand LoadDataProductCM { get; set; }
        public ICommand CloseCM { get; set; }
        public ICommand GetQLSPWindowCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand SaveNewProductCM { get; set; }
        public ICommand SaveUpdatedProductCM { get; set; }
        public ICommand GetCurrentWindowCM { get; set; }
        #endregion
        #region thuộc tính lưu dữ liệu
        private int _masp {  get; set; }
        public int MaSP { get { return _masp; } set { _masp = value; OnPropertyChanged(); } }
        private string _loaisp {  get; set; }
        public string LoaiSP { get { return _loaisp; } set { _loaisp = value; OnPropertyChanged(); } }
        private string _tensp { get; set; }
        public string TenSP { get { return _tensp; } set { _tensp = value; OnPropertyChanged(); } }
        private int _soluong { get; set; }
        public int SoLuong { get { return _soluong; } set { _soluong = value; OnPropertyChanged(); } }
        private decimal _giasp { get; set; }
        public decimal GiaSP { get { return _giasp; } set { _giasp = value; OnPropertyChanged(); } }
        private byte[] _hinhanhsp { get; set; }
        public byte[] HinhAnhSP { get { return _hinhanhsp; } set { _hinhanhsp = value; OnPropertyChanged(); } }
        #endregion
        #region biến khác
        private Page QLSanPhamPage { get; set; }
        private Window CurrentWindow { get; set; }
        private SanPhamDTO _SPSelected { get; set; }
        public SanPhamDTO SPSelected { get { return _SPSelected; } set { _SPSelected = value; OnPropertyChanged(); } }
        private ObservableCollection<SanPhamDTO> _dsSP { get; set; }
        public ObservableCollection<SanPhamDTO> dsSP { get { return _dsSP; } set { _dsSP = value; OnPropertyChanged(); } }
        public ObservableCollection<string> dsLoaiSP { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        #endregion
        public QuanLySanPhamVM()
        {
            GetQLSPWindowCM = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                QLSanPhamPage = p;
            });
            GetCurrentWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = p;
            });
            CloseCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            LoadDataProductCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                var data = await Task.Run(async () => await SanPhamDAL.Instance.GetAllProduct());
                dsSP = new ObservableCollection<SanPhamDTO>(data);
                IsLoading = false;
            });
        }
    }
}
