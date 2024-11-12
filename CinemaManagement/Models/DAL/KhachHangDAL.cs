﻿using CinemaManagement.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class KhachHangDAL
    {
        private static KhachHangDAL _instance;
        public static KhachHangDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KhachHangDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }

        //Dùng cho chức năng thêm xoá sửa
        public async Task<List<KhachHangDTO>> GetAllcustomer()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsKhachHang = (from kh in context.KhachHangs
                                       select new KhachHangDTO
                                       {
                                           MaKH = kh.MaKH,
                                           TenKH = kh.TenKH,
                                           SDT_KH = kh.SDT_KH,
                                           email_KH = kh.email_KH,
                                           NgaySinh = kh.NgaySinh,
                                           GioiTinh = kh.GioiTinh,
                                           NgayDK = kh.NgayDK,
                                           HDTichLuy = kh.HDTichLuy,
                                       }).ToListAsync();
                    return await dsKhachHang;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
        public async Task<(bool, string)> Deletecustomer(int maKhXoa)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var kh = await context.KhachHangs.FindAsync(maKhXoa);
                    if (kh == null)
                    {
                        return (false, "Khách hàng không tồn tại");
                    }
                    context.KhachHangs.Remove(kh);
                    return (true, "Đã xoá khách hàng");
                }
            }
            catch (DbUpdateException ex)
            {
                return (false, "Lỗi CSDL");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, "Lỗi hệ thống");
            }
        }
        public async Task<(bool, string)> Updatecustomer(KhachHangDTO khcapnhat)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var kh = await context.KhachHangs.FindAsync(khcapnhat.MaKH);
                    if (kh == null)
                    {
                        return (false, "Mã khách hàng không tồn tại");
                    }
                    bool checkSDT = await context.KhachHangs.AnyAsync(s => s.SDT_KH == khcapnhat.SDT_KH && s.MaKH != khcapnhat.MaKH);
                    bool checkEmail = await context.KhachHangs.AnyAsync(s => s.email_KH == khcapnhat.email_KH && s.MaKH != khcapnhat.MaKH);
                    if (checkSDT)
                    {
                        return (false, "Số điện thoại đã được đăng ký");
                    }
                    if (checkEmail)
                    {
                        return (false, "Email đã được đăng ký");
                    }
                    kh.TenKH = khcapnhat.TenKH;
                    kh.SDT_KH = khcapnhat.SDT_KH;
                    kh.email_KH = khcapnhat.email_KH;
                    kh.NgaySinh = khcapnhat.NgaySinh;
                    kh.GioiTinh = khcapnhat.GioiTinh;
                    kh.NgayDK = khcapnhat.NgayDK;
                    kh.HDTichLuy = khcapnhat.HDTichLuy;

                    await context.SaveChangesAsync();

                }
            }
            catch (DbUpdateException e)
            {
                return (false, "Lỗi CSDL");
            }
            catch (Exception ex)
            {
                return (false, ex.ToString());
            }
            return (true, "Đã cập nhật");
        }
        public async Task<(bool, string)> Addcustomer(KhachHangDTO KhachHang)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    int maxcustomerId = await context.KhachHangs.MaxAsync(s => s.MaKH);
                    int newcustomerId = maxcustomerId + 1;

                    var kh = new KhachHang();

                    //Kiểm tra SĐT, email của KH mới
                    bool checkSDT = await context.KhachHangs.AnyAsync(s => s.SDT_KH == KhachHang.SDT_KH && s.MaKH != KhachHang.MaKH);
                    bool checkEmail = await context.KhachHangs.AnyAsync(s => s.email_KH == KhachHang.email_KH && s.MaKH != KhachHang.MaKH);
                    if (checkSDT)
                    {
                        return (false, "Số điện thoại đã được đăng ký");
                    }
                    if (checkEmail)
                    {
                        return (false, "Email đã được đăng ký");
                    }

                    kh.MaKH = newcustomerId;
                    kh.TenKH = KhachHang.TenKH;
                    kh.SDT_KH = KhachHang.SDT_KH;
                    kh.email_KH = KhachHang.email_KH;
                    kh.NgaySinh = KhachHang.NgaySinh;
                    kh.GioiTinh = KhachHang.GioiTinh;
                    kh.NgayDK = DateTime.Now;
                    kh.HDTichLuy = 0;

                    context.KhachHangs.Add(kh);
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                return (false, "Lỗi CSDL");
            }
            catch (Exception ex)
            {
                return (false, ex.ToString());
            }
            return (true, "Đăng ký khách hàng thành công");
        }

        //Dùng cho thống kê
        public async Task<List<KhachHangDTO>> GetAllcustomerByBenefit()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dsKhachHang = (from kh in context.KhachHangs
                                       orderby kh.HDTichLuy descending
                                       select new KhachHangDTO
                                       {
                                           MaKH = kh.MaKH,
                                           TenKH = kh.TenKH,
                                           SDT_KH = kh.SDT_KH,
                                           email_KH = kh.email_KH,
                                           NgaySinh = kh.NgaySinh,
                                           GioiTinh = kh.GioiTinh,
                                           NgayDK = kh.NgayDK,
                                           HDTichLuy = kh.HDTichLuy,
                                       }).ToListAsync();
                    return await dsKhachHang;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}
