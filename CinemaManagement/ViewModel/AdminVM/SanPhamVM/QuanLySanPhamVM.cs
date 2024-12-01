using CinemaManagement.CustomControls;
using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using CinemaManagement.Ultis;
using CinemaManagement.View;
using CinemaManagement.View.AdminView.SanPhamView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLySanPhamVM : BaseViewModel
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
        public ICommand SaveProductCM { get; set; }
        public ICommand GetCurrentWindowCM { get; set; }
        #endregion
        #region thuộc tính lưu dữ liệu
        private int? _masp {  get; set; }
        public int? MaSP { get { return _masp; } set { _masp = value; OnPropertyChanged(); } }
        private string _loaisp {  get; set; }
        public string LoaiSP { get { return _loaisp; } set { _loaisp = value; OnPropertyChanged(); } }
        private string _tensp { get; set; }
        public string TenSP { get { return _tensp; } set { _tensp = value; OnPropertyChanged(); } }
        private int _soluong { get; set; }
        public int SoLuong { get { return _soluong; } set { _soluong = value; OnPropertyChanged(); } }
        private decimal? _giasp { get; set; }
        public decimal? GiaSP { get { return _giasp; } set { _giasp = value; OnPropertyChanged(); } }
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
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await SanPhamDAL.Instance.GetAllProduct());
                    dsSP = new ObservableCollection<SanPhamDTO>(data);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    MyMessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            });
            UploadImageCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string imagePath = ImageUtility.ImagePath();
                if (imagePath != null) HinhAnhSP = ImageUtility.ConvertImageToByteArray(imagePath);
            });
            AddProductCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ClearData();
                ThemSanPham w1 = new ThemSanPham();
                w1.ShowDialog();
            });
            SaveNewProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveNewProduct(p);
                IsSaving = false;
            });
            DeleteProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Xoá sẽ mất hết dữ liệu", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(SPSelected.SoLuong > 0)
                {
                    MyMessageBox.Show("Sản phẩm còn hàng tồn trong kho, không thể xoá");
                }
                else if (result == MessageBoxResult.Yes)
                {
                    IsLoading = true;
                    (bool trangthai, string message) = await SanPhamDAL.Instance.DeleteProduct(SPSelected.MaSP);
                    if (trangthai)
                    {
                        for (int i = 0; i < dsSP.Count; i++)
                        {
                            if (dsSP[i].MaSP == SPSelected.MaSP)
                            {
                                dsSP.Remove(dsSP[i]);
                                break;
                            }
                        }
                    }
                    CustomControls.MyMessageBox.Show(message);
                }
            });
            ViewProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (SPSelected != null)
                {
                    IsLoading = true;
                    ClearData();
                    //ThongTinSanPham w1 = new ThongTinSanPham();
                    GetDataProduct();
                    //w1.ShowDialog();
                    IsLoading = false;
                }
            });
            UpdateProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                ClearData();
                SuaSanPham w1 = new SuaSanPham();
                GetDataProduct();
                w1.ShowDialog();
                IsLoading = false;
            });
            SaveProductCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveUpdatedProduct(p);
                IsSaving = false;
            });
            dsLoaiSP = new ObservableCollection<string>
            {
                "Thức ăn",
                "Nước uống",
                "Đồ chơi",
                "Khăn giấy"
            };
        }
        private void ClearData()
        {
            MaSP = null;
            TenSP = null;
            GiaSP = null;
            HinhAnhSP = null;
            SoLuong = 0;
        }
        private void GetDataProduct()
        {
            if(SPSelected != null)
            {
                MaSP = SPSelected.MaSP;
                TenSP = SPSelected.TenSP;
                GiaSP = SPSelected.GiaSP;
                HinhAnhSP = SPSelected.HinhAnhSP;
                SoLuong = SPSelected.SoLuong;
            }
        }
        private bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(TenSP)
                   && !(GiaSP is null) && !(HinhAnhSP is null) && !string.IsNullOrEmpty(LoaiSP);
        }
    }
}
