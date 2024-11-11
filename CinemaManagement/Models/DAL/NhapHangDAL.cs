using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class NhapHangDAL
    {
        private static NhapHangDAL _instace;
        public static NhapHangDAL Instace
        {
            get
            {
                if( _instace == null )
                {
                    _instace = new NhapHangDAL();
                }
                return _instace;
            }
            private set => _instace = value;
        }
        public async Task<(bool TrangThai, string message)> CreateBooking(HDNhapHangDTO hdNhap, List<CTHDNhap> dsSPNhap)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    //Tạo hoá nhập mới
                    int newReceiptId;
                    newReceiptId = await CreateNewBillReceipt(context, hdNhap);

                    //Thêm chi tiết hoá đơn nhập
                    if (dsSPNhap.Any())
                    {
                        string ex = await AddNewProductReceiptInfo(context, dsSPNhap, newReceiptId);
                        if (ex != null)
                        {
                            return (false, ex);
                        }
                    }
                    await context.SaveChangesAsync();
                    return (true, "Nhập hàng thành công");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return (false, "Lỗi hệ thống");
            }
        }

        public async Task<string> AddNewProductReceiptInfo(CinemaManagementEntities context, List<CTHDNhap> dsSanPham, int newReceiptId)
        {
            try
            {
                List<CTHDNhap> dsCTHDNhap = new List<CTHDNhap>();
                for(int i = 0; i < dsSanPham.Count; i++)
                {
                    dsCTHDNhap.Add(new CTHDNhap
                    {
                        SoHDNhap = newReceiptId,
                        MaSPNhap = dsSanPham[i].MaSPNhap,
                        DonGiaNhap = dsSanPham[i].DonGiaNhap,
                        SoLuong = dsSanPham[i].SoLuong,
                        TongTien = dsSanPham[i].TongTien,
                    });
                    var SanPham = await context.SanPhams.FindAsync(dsSanPham[i].MaSPNhap);
                    SanPham.SoLuong += dsSanPham[i].SoLuong;
                }
                context.CTHDNhaps.AddRange(dsCTHDNhap);
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "Lỗi hệ thống";
            }
        }

        public async Task<int> CreateNewBillReceipt(CinemaManagementEntities context, HDNhapHangDTO hdNhap)
        {
            int maxBillReceiptId = await context.HDNhapHangs.MaxAsync(HDNhapHang => HDNhapHang.SoHDNhap);
            int newBillReceiptId = maxBillReceiptId + 1;
            HDNhapHang HDNhap = new HDNhapHang
            {
                SoHDNhap = newBillReceiptId,
                NgayNhap = DateTime.Now,
                MaNVNhap = hdNhap.MaNVNhap,
                ThanhTien = hdNhap.ThanhTien
            };
            context.HDNhapHangs.Add(HDNhap);
            return newBillReceiptId;
        }
    }
    
}
