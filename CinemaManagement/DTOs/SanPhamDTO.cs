using CinemaManagement.Models;
using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    
    public class SanPhamDTO
    {
        public SanPhamDTO()
        {
            
        }
        public SanPhamDTO(int maSP, string loaiSP, string tenSP, int soLuong, decimal giaSP, byte[] hinhAnhSP)
        {
            MaSP = maSP;
            LoaiSP = loaiSP;
            TenSP = tenSP;
            SoLuong = soLuong;
            GiaSP = giaSP;
            HinhAnhSP = hinhAnhSP;
        }

        public int MaSP { get; set; }
        public string LoaiSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaSP { get; set; }
        public byte[] HinhAnhSP { get; set; }
        public Nullable<bool> IsDeleted { get; set; } = false;
        public string GiaSPStr
        {
            get
            {
                return MoneyFormat.FormatToVND(GiaSP);
            }
        }
        public string MaSPStr
        {
            get
            {
                return $"SP{MaSP:D4}";
            }
        }

    }
}