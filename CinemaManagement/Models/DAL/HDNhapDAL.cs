using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class HDNhapDAL
    {
        private static HDNhapDAL _instance;
        public static HDNhapDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HDNhapDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<HDNhapHangDTO>> GetAllReceipt()
        {
            using(var context = new CinemaManagementEntities())
            {
                var dshdnhap = (from hdnhap in context.HDNhapHangs
                                select new HDNhapHangDTO
                                {
                                    SoHDNhap = hdnhap.SoHDNhap,
                                    MaNVNhap = hdnhap.MaNVNhap,
                                    NgayNhap = hdnhap.NgayNhap,
                                    ThanhTien = hdnhap.ThanhTien
                                }).ToListAsync();
                return await dshdnhap;
            }
        }
        public async Task<HDNhapHangDTO> GetReceiptDetail(int soHDNhap)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var hdnhap = await context.HDNhapHangs.FindAsync(soHDNhap);

                    HDNhapHangDTO CTNHAP = new HDNhapHangDTO
                    {
                        SoHDNhap = hdnhap.SoHDNhap,
                        MaNVNhap = hdnhap.MaNVNhap,
                        TenNVNhap = hdnhap.NhanVien.TenNV,
                        NgayNhap = hdnhap.NgayNhap,
                        ThanhTien = hdnhap.ThanhTien,
                        CTHDNhap = (from ct in hdnhap.CTHDNhaps
                                select new CTHDNhapDTO
                                {
                                    SoHDNhap = ct.SoHDNhap,
                                    MaSPNhap = ct.MaSPNhap,
                                    TenSPNhap = ct.SanPham.TenSP,
                                    DonGiaNhap = ct.DonGiaNhap, 
                                    SoLuong = ct.SoLuong,
                                }).ToList(),
                    };
                    return CTNHAP;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
