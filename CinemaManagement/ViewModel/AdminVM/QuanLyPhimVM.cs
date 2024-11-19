using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;

namespace CinemaManagement.ViewModel.AdminVM
{
    public class QuanLyPhimVM : BaseViewModel
    {
        #region Icommand
        public ICommand GetDataGridCM { get; set; }

        public ICommand AddFilmCM { get; set; }
        public ICommand UpdateFilmCM { get; set; }
        public ICommand DeleteFilmCM { get; set; }

        public ICommand OpenAddFilmCM { get; set; }
        public ICommand OpenUpdateFilmCM { get; set; }
        public ICommand OpenDeleteFilmCM { get; set; }

        public ICommand LoadDataFilmCM { get; set; }

        #endregion
        #region Thuộc tính
        private PhimDTO _phim {  get; set; }
        public PhimDTO Phim { get { return _phim; } set { _phim = value; OnPropertyChanged(); } }
        private ObservableCollection<PhimDTO> _dsphim { get; set; }
        public ObservableCollection<PhimDTO> dsPhim { get { return _dsphim; } set { _dsphim = value; OnPropertyChanged(); } }
        private bool IsSaving { get; set; }
        private bool IsLoading { get; set; }
        #endregion
        public QuanLyPhimVM()
        {
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
                    MessageBox.Show("Lỗi hệ thống");
                    return;
                }
            });
        }
    }
}
