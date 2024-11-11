using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public class SuCoDTO
    {
        public SuCoDTO()
        {
            TinhTrang = false;
        }
        public int MaSuCo { get; set; }
        public int MaNVBao { get; set; }
        public string DiaDiem { get; set; }
        public string CTSuCo { get; set; }
        public bool TinhTrang { get; set; }
        public decimal PhiSuaChua { get; set; }
        public string PhiSuaChuaStr
        {
            get
            {
                return MoneyFormat.FormatToVND(PhiSuaChua);
            }
        }
    }
}
