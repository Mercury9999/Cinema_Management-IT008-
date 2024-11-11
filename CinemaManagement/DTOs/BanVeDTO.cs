﻿using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public class BanVeDTO
    {
        BanVeDTO() { }
        public int MaSC { get; set; }
        public int MaGhe { get; set; }
        public bool DaBan { get; set; } = false;

        public GheDTO Ghe { get; set; }
        public SuatChieuDTO SuatChieu { get; set; }
        public string SoGheStr
        {
            get
            {
                return Ghe.SoGheStr;
            }
        }
        public string DaBanStr
        {
            get
            {
                if (DaBan == false) return "Trống";
                else return "Hết chỗ";
            }
        }
    }
}