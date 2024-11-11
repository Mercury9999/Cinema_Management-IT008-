using CinemaManagement.Models;
using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public partial class CTHDSanPham
    {
        public int SoHD { get; set; }
        public int MaSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien
        {
            get
            {
                return DonGia * SoLuong;
            }
        }
        public string ThanhTienStr
        {
           get
           {
                return MoneyFormat.FormatToVND(ThanhTien);
           }
        }
        public string SoHDStr
        {
            get
            {
                return $"HD{SoHD:D6}"; 
            }
        }
    }
}
