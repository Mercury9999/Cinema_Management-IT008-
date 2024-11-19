using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLyKhachHangVM : BaseViewModel
    {
        #region Thuộc tính

        private KhachHangDTO _khachhang { get; set; }
        public KhachHangDTO KhachHang
        {
            get { return _khachhang; }
            set { KhachHang = value; OnPropertyChanged(); }
        }
        private string _khEmail;
        public string KhEmail
        {
            get { return _khEmail; }
            set { _khEmail = value; OnPropertyChanged(); }
        }
        private string _khSDT;
        public string KhSDT
        {
            get { return _khSDT; }
            set { _khSDT = value; OnPropertyChanged(); }
        }

        private ObservableCollection<KhachHangDTO> _dskh;
        public ObservableCollection<KhachHangDTO> dsKH
        {

            get { return _dskh; }
            set { _dskh = value; OnPropertyChanged(); }
        }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }
        #endregion

        #region ICommand
        public ICommand GetListViewCM { get; set; }

        public ICommand AddCustomerCM { get; set; }
        public ICommand UpdateCustomerCM { get; set; }
        public ICommand DeleteCustomerCM { get; set; }

        public ICommand OpenAddCustomerCM { get; set; }
        public ICommand OpenUpdateCustomerCM { get; set; }
        public ICommand OpenDeleteCustomerCM { get; set; }

        public ICommand LoadDataCustomerCM { get; set; }

        #endregion
        public QuanLyKhachHangVM()
        {

            LoadDataCustomerCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                /*
                try
                {
                    IsLoading = true;
                    var data = await Task.Run(async () => await KhachHangDAL.Instance.GetAllcustomer());
                    dsKH = new ObservableCollection<KhachHangDTO>(data);
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                */
            });
            OpenAddCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
            });
        }
      
    }
}
