using CinemaManagement.DTOs;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLyPhimVM : BaseViewModel
    {
        public bool CheckNonEmpty()
        {
            return !string.IsNullOrEmpty(TenPhim) 
            && !string.IsNullOrEmpty(ThoiLuong) && !string.IsNullOrEmpty(NuocSX) 
            && !string.IsNullOrEmpty(DaoDien) && !string.IsNullOrEmpty(GioiHanTuoi)
            && !string.IsNullOrEmpty(NoiDung) && Poster != null;
        }
        public async Task SaveNewFilm(Window w1)
        {
            if (CheckNonEmpty())
            {
                PhimDTO Phim = new PhimDTO
                {
                    TenPhim = TenPhim,
                    ThoiLuong = Convert.ToInt32(ThoiLuong),
                    TheLoai = "Chưa binding được",
                    NuocSX = NuocSX,
                    DaoDien = DaoDien,
                    NoiDung = NoiDung,
                    GioiHanTuoi = Convert.ToByte(GioiHanTuoi),
                    Poster = Poster
                };
                (bool trangthai, string messages) = await PhimDAL.Instance.AddMovie(Phim);
                if (trangthai)
                {
                    MessageBox.Show(messages);
                    dsPhim.Add(Phim);
                    return;
                }
                else
                {
                    MessageBox.Show(messages);
                    MessageBox.Show("Lỗi hệ thống");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
        }
    }
}
