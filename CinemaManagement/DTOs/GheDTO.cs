using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    public class GheDTO
    {
        public GheDTO() { }
        public int MaGhe { get; set; }
        public int SoGhe { get; set; }
        public int SoPhong { get; set; }
        public string SoGheStr
        {
            get
            {
                return SoGhe.ToString();
            }
        }
    }
}
