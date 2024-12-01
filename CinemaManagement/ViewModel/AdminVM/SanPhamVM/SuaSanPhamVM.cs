using CinemaManagement.DTOs;
using CinemaManagement.Models;
using CinemaManagement.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.ViewModel.AdminVM
{
    public partial class QuanLySanPhamVM
    {
        private async Task SaveUpdatedProduct(Window w1)
        {
            if (CheckNonEmpty())
            {
                try
                {
                    SanPhamDTO sanPham = new SanPhamDTO()
                    {
                        TenSP = TenSP,
                        GiaSP = GiaSP ?? 0m,
                        LoaiSP = LoaiSP,
                        HinhAnhSP = HinhAnhSP,
                        SoLuong = 0
                    };
                    (bool trangthai, string messages) = await SanPhamDAL.Instance.UpdateProduct(SPSelected);
                    if (trangthai)
                    {
                        MessageBox.Show(messages);
                        IsLoading = true;
                        var SanPhamUpdated = dsSP.FirstOrDefault(s => s.MaSP == sanPham.MaSP);
                        if (SanPhamUpdated != null)
                        {
                            SanPhamUpdated = sanPham;
                            return;
                        }
                        IsLoading = false;
                        CurrentWindow.Close();
                    }
                    else
                    {
                        CustomControls.MyMessageBox.Show("Lỗi hệ thống" + messages);
                    }
                }
                catch (Exception ex)
                {
                    CustomControls.MyMessageBox.Show("Lỗi hệ thống" + ex.Message);
                }
            }
            else
            {
                CustomControls.MyMessageBox.Show("Vui lòng nhập đủ thông tin sản phẩm");
            }
        }
    }
}
