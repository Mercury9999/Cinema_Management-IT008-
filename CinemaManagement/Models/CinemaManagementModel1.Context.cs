﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
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
    
        public DbSet<BanVe> BanVes { get; set; }
        public DbSet<CTHDNhap> CTHDNhaps { get; set; }
        public DbSet<CTHDSanPham> CTHDSanPhams { get; set; }
        public DbSet<Ghe> Ghes { get; set; }
        public DbSet<HDNhapHang> HDNhapHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<Phim> Phims { get; set; }
        public DbSet<PhongChieu> PhongChieux { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<SuatChieu> SuatChieux { get; set; }
        public DbSet<SuCo> SuCoes { get; set; }
        public DbSet<Ve> Ves { get; set; }
    }
}
