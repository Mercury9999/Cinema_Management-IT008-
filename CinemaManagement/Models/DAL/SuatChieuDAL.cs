using CinemaManagement.DTOs;
using CinemaManagement.Ultis;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaManagement.Models.DAL
{
    public class SuatChieuDAL
    {
        private static SuatChieuDAL _instance;
        public  static SuatChieuDAL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SuatChieuDAL();
                }
                return _instance;
            }
            private set => _instance = value;   
        }
        public async Task<(bool, string)> AddShowTime(SuatChieuDTO suatchieu)
        {
            try
            {
                using(var context = new CinemaManagementEntities())
                {
                    var p = await context.Phims.FindAsync(suatchieu.MaPhim);
                    if(p == null)
                    {
                        return (false, "Phim không tồn tại");
                    }
                    var st = suatchieu.BatDau;
                    var ed = suatchieu.KetThuc;
                    TimeSpan thoigian = ed - st;
                    double tg = thoigian.TotalMinutes;
                    if(tg < suatchieu.Phim.ThoiLuong + 20)
                    {
                        return (false, "Thời gian suất chiếu tối thiểu phải dài hơn thời lượng phim 20 phút");
                        //Thời gian suất chiếu min = thời gian chiếu phim + 20 phút, lúc in vé chỉ in thời gian bắt đầu và thời lượng phim
                    }
                    var s = context.SuatChieux.Where(s => (IsTimeBetween(st, s.BatDau, s.KetThuc) || IsTimeBetween(ed, s.BatDau, s.KetThuc) == true)
                                                           && s.SoPhongChieu == suatchieu.SoPhongChieu).FirstOrDefault(); 
                    if(s != null)
                    {
                        return (false, $"Thời gian {ConvertDateTime.Clock(s.BatDau)} đến {ConvertDateTime.Clock(s.KetThuc)} đã có suất chiếu khác ");
                    }
                    int newShowTimeId = await context.SuatChieux.MaxAsync(s => s.MaSC) + 1;
                    SuatChieu sc = new SuatChieu()
                    {
                        MaSC = newShowTimeId,
                        MaPhim = suatchieu.MaPhim,
                        BatDau = suatchieu.BatDau,
                        KetThuc = suatchieu.KetThuc,
                        GiaVe = suatchieu.GiaVe,
                        SoPhongChieu = suatchieu.SoPhongChieu
                    };
                    context.SuatChieux.Add(sc);

                    //Tạo ds bán vé
                    var dsghe = await (from ghe in context.Ghes
                                 where ghe.SoPhong == suatchieu.SoPhongChieu
                                 select ghe.MaGhe
                                ).ToListAsync();
                    List<BanVe> banve = new List<BanVe>();
                    foreach( var maghe in dsghe  )
                    {
                        banve.Add(new BanVe
                        {
                            MaSC = newShowTimeId,
                            MaGhe = maghe,
                            DaBan = false
                        });
                    }
                    context.BanVes.AddRange( banve );
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, "Lỗi hệ thống");
            }
            return (true, "Thêm suất chiếu thành công");
        }

        //Này cho tính năng tìm kiếm
        //Nhận 1 phim và ngày, trả về danh sách suất chiếu của phim đó trong ngày đó
        public async Task<List<SuatChieuDTO>> GetShowTimeByMovie(int MaPhim, DateTime dt)
        {
            List<SuatChieuDTO> dsSC = new List<SuatChieuDTO>();
            try
            {
                using (var context = new CinemaManagementEntities())
                {
                    dsSC = await (from s in context.SuatChieux
                                    where s.MaPhim == MaPhim && s.BatDau.Date == dt.Date
                                    select new SuatChieuDTO
                                    {
                                        MaSC = s.MaSC,
                                        MaPhim = s.MaPhim,
                                        BatDau = s.BatDau,
                                        KetThuc = s.KetThuc,
                                        GiaVe = s.GiaVe,
                                        SoPhongChieu = s.SoPhongChieu
                                    }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return dsSC;
        }

        static bool IsTimeBetween(DateTime checkTime, DateTime startTime, DateTime endTime)
        {
            return checkTime >= startTime && checkTime <= endTime;
        }
    }

}
