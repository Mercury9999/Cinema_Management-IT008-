﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using System.Windows;
using CinemaManagement.Ultis;
using System.Windows.Input;
using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using CinemaManagement.View;
using CinemaManagement.CustomControls;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyPhimVM : BaseViewModel
    {
        #region Icommand

        public ICommand ViewFilmCM { get; set; }
        public ICommand AddFilmCM { get; set; }
        public ICommand UpdateFilmCM { get; set; }
        public ICommand DeleteFilmCM { get; set; }

        public ICommand LoadDataFilmCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand GetQLPWindowCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand SaveNewFilmCM { get; set; }
        public ICommand SaveFilmCM { get; set; }
        public ICommand GetCurrentWindow { get; set; }
        #endregion
        #region Thuộc tính
        //Dùng lưu trữ dữ liệu của Phim

        private string _maPhim {  get; set; }
        public string MaPhim { get { return _maPhim; } set { _maPhim = value; OnPropertyChanged(); } }
        private string _tenPhim { get; set; }
        public string TenPhim { get { return _tenPhim; } set { _tenPhim = value; OnPropertyChanged(); } }
        private string _theLoai {  get; set; }
        public string TheLoai { get { return _theLoai; } set { _theLoai = value; OnPropertyChanged(); } }   
        private string _thoiLuong { get; set; }
        public string ThoiLuong { get { return _thoiLuong; } set { _thoiLuong = value; OnPropertyChanged(); } }
        private string _nuocSX {  get; set; }
        public string NuocSX { get { return _nuocSX; } set { _nuocSX = value; OnPropertyChanged(); } }  
        private string _ngayPH { get; set; }
        public string NgayPH { get { return _ngayPH; } set { _ngayPH = value; OnPropertyChanged(); } }
        private string _daoDien { get; set; }
        public string DaoDien { get { return _daoDien; } set { _daoDien = value; OnPropertyChanged(); } }
        private string _noiDung {  get; set; }
        public string NoiDung { get { return _noiDung; } set { _noiDung = value; OnPropertyChanged(); } }
        private string _gioiHanTuoi {  get; set; }
        public string GioiHanTuoi { get { return _gioiHanTuoi; } set { _gioiHanTuoi = value; OnPropertyChanged(); } }
        private byte[] _poster {  get; set; }
        public byte[] Poster { get { return _poster; } set { _poster = value; OnPropertyChanged(); } }
        private decimal _doanhThu { get; set; }
        public decimal DoanhThu { get { return _doanhThu; } set { _doanhThu = value; OnPropertyChanged(); } }

        //Biến khác
        private DataGrid dataGrid {  get; set; }
        private Window QuanLyPhimWindow { get; set; }
        private Window CurrentWindow { get; set; }
        private PhimDTO _phimselected {  get; set; }
        public PhimDTO PhimSelected { get { return _phimselected; } set { _phimselected = value; OnPropertyChanged(); } }
        private ObservableCollection<PhimDTO> _dsphim { get; set; }
        public ObservableCollection<PhimDTO> dsPhim { get { return _dsphim; } set { _dsphim = value; OnPropertyChanged(); } }
        private bool IsSaving { get; set; }
        private bool IsLoading { get; set; }
        private ObservableCollection<string> _dstheloai { get; set; }
        public ObservableCollection<string> dsTheLoai { get { return _dstheloai; } set { _dstheloai = value; OnPropertyChanged(); } }
        #endregion
        public QuanLyPhimVM()
        {
            GetQLPWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                QuanLyPhimWindow = p;
            });
            GetCurrentWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                CurrentWindow = p;
            });
            LoadDataFilmCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await PhimDAL.Instance.GetAllMovie());
                    dsPhim = new ObservableCollection<PhimDTO>(data);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống");
                    return;
                }
            });
            UploadImageCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string imagePath = ImageUtility.ImagePath();
                if(imagePath != null) Poster = ImageUtility.ConvertImageToByteArray(imagePath);
            });
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            AddFilmCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ClearData();
                ThemPhim w1 = new ThemPhim();
                w1.ShowDialog();
            });
            SaveNewFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveNewFilm(p);
                IsSaving = false;
            });
            DeleteFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Xoá sẽ mất hết dữ liệu", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    IsLoading = true;
                    (bool trangthai, string message) = await PhimDAL.Instance.DeleteMovie(PhimSelected.MaPhim);
                    if(trangthai)
                    {
                        for (int i = 0; i < dsPhim.Count; i++)
                        {
                            if (dsPhim[i].MaPhim == PhimSelected.MaPhim)
                            {
                                dsPhim.Remove(dsPhim[i]);
                                break;
                            }
                        }
                    }
                    CustomControls.MyMessageBox.Show(message);
                }
            });
            ViewFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (PhimSelected != null)
                {
                    IsLoading = true;
                    ClearData();
                    ThongTinPhim w1 = new ThongTinPhim();
                    GetDataFilm();
                    w1.ShowDialog();
                    IsLoading = false;
                }
            });
            UpdateFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;
                ClearData();
                SuaPhim w1 = new SuaPhim();
                GetDataFilm();
                w1.ShowDialog();
                IsLoading = false;
            });
            SaveFilmCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;
                await SaveUpdateFilm(p);
                IsSaving = false;
            });
            dsTheLoai = new ObservableCollection<string>
            { 
                "Kinh dị",
                "Hành động",
                "Anime"
            };
        }
        private void ClearData()
        {
            TenPhim = null;
            TheLoai = null;
            ThoiLuong = null;
            NuocSX = null;
            NgayPH = null;
            DaoDien = null;
            NoiDung = null;
            Poster = null;
            GioiHanTuoi = null;
        }
    }
}