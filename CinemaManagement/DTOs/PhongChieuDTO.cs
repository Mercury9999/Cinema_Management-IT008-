using System;
using System.Collections.Generic;

namespace CinemaManagement.DTOs
{
    using System;
    using System.Collections.Generic;
    
    public class PhongChieuDTO
    {
       
        public PhongChieuDTO()
        {
            this.Ghes = new List<GheDTO>();
        }
    
        public int SoPhong { get; set; }
        public int SLGhe { get; set; }
        public IList<GheDTO> Ghes { get; set; }
       
    }
}
