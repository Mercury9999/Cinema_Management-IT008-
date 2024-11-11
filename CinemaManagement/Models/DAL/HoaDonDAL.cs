using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class HoaDonDAL
    {
        private static HoaDonDAL _instance;
        public static HoaDonDAL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new HoaDonDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<HoaDonDTO>> GetAllBill()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dshoadon = (from hoadon in context.HoaDons
                                    select new HoaDonDTO
                                    {
                                        SoHD = hoadon.SoHD,
                                        MaKH = hoadon.MaKH,
                                        TenKH = hoadon.KhachHang.TenKH,
                                        MaNV = hoadon.MaNV,
                                        NgayHD = hoadon.NgayHD,
                                        ChietKhau = hoadon.ChietKhau,
                                        GiamGia = hoadon.GiamGia,
                                        GiaTriHD = hoadon.GiaTriHD,
                                        ThanhTien = hoadon.ThanhTien
                                    }).ToListAsync();
                    return await dshoadon;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
