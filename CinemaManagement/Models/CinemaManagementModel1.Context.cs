﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CinemaManagementEntities : DbContext
    {
        public CinemaManagementEntities()
            : base("name=CinemaManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BanVe> BanVes { get; set; }
        public virtual DbSet<CTHDSanPham> CTHDSanPhams { get; set; }
        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<HDNhapHang> HDNhapHangs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<PhongChieu> PhongChieux { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SuatChieu> SuatChieux { get; set; }
        public virtual DbSet<SuCo> SuCoes { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }
    }
}
