using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;
using System.Windows.Annotations;

namespace CinemaManagement.DTOs
{

    public class CTHDNhapDTO
    {
        public CTHDNhapDTO() { }
        public int SoHDNhap { get; set; }
        public int MaSPNhap { get; set; }
        public decimal DonGiaNhap { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien
        {
            get
            {
                return DonGiaNhap * SoLuong;
            }
        }
        public string DonGiaNhapStr { get { return MoneyFormat.FormatToVND(DonGiaNhap); } }
        public string TongTienStr { get { return MoneyFormat.FormatToVND(TongTien); } }
        public string SoHDNhapStr
        {
            get
            {
                return $"Nhap{SoHDNhap:D6}"; 
            }
        }
    }
}
