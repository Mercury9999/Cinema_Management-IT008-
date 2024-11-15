﻿using CinemaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Models.DAL
{
    public class SanPhamDAL
    {
        private static SanPhamDAL _instance;
        public static SanPhamDAL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SanPhamDAL();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public async Task<List<SanPhamDTO>> GetAllProduct()
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var dssanpham = (from sp in context.SanPhams
                                      select new SanPhamDTO
                                      {
                                          MaSP = sp.MaSP,
                                          TenSP = sp.TenSP,
                                          LoaiSP = sp.LoaiSP,
                                          SoLuong = sp.SoLuong,
                                          GiaSP = sp.GiaSP,
                                          HinhAnhSP = sp.HinhAnhSP
                                      }).ToListAsync();
                    return await dssanpham;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
        public async Task<(bool, string)> DeleteProduct(int maspXoa)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var sp = await context.SanPhams.FindAsync(maspXoa);
                    if (sp == null || sp.IsDeleted == true)
                    {
                        return (false, "sản phẩm không tồn tại");
                    }
                    sp.IsDeleted = true;
                    sp.SoLuong = 0;
                    await context.SaveChangesAsync();
                    return (true, "Đã xoá sản phẩm");
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
        public async Task<(bool, string)> UpdateProduct(SanPhamDTO spcapnhat)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    var sp = await context.SanPhams.FindAsync(spcapnhat.MaSP);
                    if (sp == null)
                    {
                        return (false, "Mã sản phẩm không tồn tại");
                    }

                    sp.TenSP = spcapnhat.TenSP;
                    sp.LoaiSP = spcapnhat.LoaiSP;
                    sp.SoLuong = spcapnhat.SoLuong;
                    sp.GiaSP = spcapnhat.GiaSP;
                    sp.HinhAnhSP = spcapnhat.HinhAnhSP;

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
        public async Task<(bool, string)> AddProduct(SanPhamDTO sanpham)
        {
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    int maxProductId = await context.SanPhams.MaxAsync(s => s.MaSP);
                    int newProductId = maxProductId + 1;

                    var sp = new SanPham()
                    {
                        MaSP = newProductId,
                        TenSP = sanpham.TenSP,
                        LoaiSP = sanpham.LoaiSP,
                        SoLuong = sanpham.SoLuong,
                        GiaSP = sanpham.GiaSP,
                        HinhAnhSP = sanpham.HinhAnhSP,
                    };

                    context.SanPhams.Add(sp);
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
            return (true, "Thêm sản phẩm thành công");
        }
    }
}
