using ChungKhoanASPDotNetCore.Applications.Interfaces;
using ChungKhoanASPDotNetCore.Data;
using ChungKhoanASPDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChungKhoanASPDotNetCore.Models.RequestModels;
using ChungKhoanASPDotNetCore.Entities;
using System;

namespace ChungKhoanASPDotNetCore.Applications.Services
{
    public class LenhDatService : ILenhDatService
    {
        ApplicationDbContext _context;
        public LenhDatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LenhDatViewModel> Get(int id)
        {
            return await _context.LenhDats.Where(x => x.Id == id).Select(x => new LenhDatViewModel()
            {
                Id = x.Id,
                GiaDat = x.GiaDat,
                LoaiGiaoDich = x.LoaiGiaoDich ? "Mua" : "Bán",
                MaCK = x.MaCK,
                LoaiLenh = x.LoaiLenh,
                SoLuong = x.SoLuong,
                NgayDat = x.NgayDat,
                TrangThai = x.TrangThai,
                LenhKhops = x.LenhKhops.Select(k => new LenhKhopViewModel()
                {
                    Id = k.Id,
                    NgayKhop = k.NgayKhop,
                    GiaKhop = k.GiaKhop,
                    SoLuongKhop = k.SoLuongKhop
                }).ToList(),
            }).FirstOrDefaultAsync();
        }

        public async Task<List<LenhDatViewModel>> GetAll()
        {
            return await _context.LenhDats.Select(x => new LenhDatViewModel()
            {
                Id = x.Id,
                GiaDat = x.GiaDat,
                LoaiGiaoDich = x.LoaiGiaoDich ? "Mua" : "Bán",
                MaCK = x.MaCK,
                LoaiLenh = x.LoaiLenh,
                SoLuong = x.SoLuong,
                NgayDat = x.NgayDat,
                TrangThai = x.TrangThai,
                LenhKhops = x.LenhKhops.Select(k => new LenhKhopViewModel()
                {
                    Id = k.Id,
                    NgayKhop = k.NgayKhop,
                    GiaKhop = k.GiaKhop,
                    SoLuongKhop = k.SoLuongKhop
                }).ToList(),
            }).ToListAsync();
        }

        public async Task<bool> Post(LenhDatRequest request)
        {
            var coPhieu = await _context.BangGiaTrucTuyens.FirstOrDefaultAsync(x=>x.MaCK.Equals(request.MaCK));
            if(coPhieu == null)
                return false;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var lenhDat = new LenhDat()
                    {
                        MaCK = request.MaCK,
                        LoaiLenh = request.LoaiLenh,
                        LoaiGiaoDich = request.LoaiGiaoDich,
                        NgayDat = System.DateTime.Now,
                        SoLuong = request.SoLuong,
                        TrangThai = "CK",
                        GiaDat = request.GiaDat,
                    };
                    await _context.LenhDats.AddAsync(lenhDat);
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("exec sp_khoplenh_lo @p0, @p1, @p2, @p3, @p4",
                        lenhDat.MaCK, lenhDat.LoaiGiaoDich, lenhDat.SoLuong, lenhDat.GiaDat, lenhDat.Id);
                    
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
