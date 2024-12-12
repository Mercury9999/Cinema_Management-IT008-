//------------------------------------------------------------------------------
// <auto-generated>
using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public class SuatChieuDTO
    { 
        public int MaSC { get; set; }
        public int MaPhim { get; set; }
        public int SoPhongChieu { get; set; }
        public System.DateTime BatDau { get; set; }
        public System.DateTime KetThuc { get; set; }
        public string GioChieuStr { get { return $"{BatDau:HH:mm} -> {KetThuc:HH:mm}"; } }
        public decimal GiaVe { get; set; }
        public string GiaVeStr
        {
            get
            {
                return MoneyFormat.FormatToVND(GiaVe);
            }
        }
        public PhimDTO Phim { get; set; }
        public IList<VeDTO> Ves { get; set; }
        public PhongChieuDTO PhongChieu { get; set; }
    }
}
