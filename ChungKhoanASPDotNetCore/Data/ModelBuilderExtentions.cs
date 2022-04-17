using ChungKhoanASPDotNetCore.Entities;
using Microsoft.EntityFrameworkCore;
namespace ChungKhoanASPDotNetCore.Data
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangGiaTrucTuyen>().HasData(
                new BangGiaTrucTuyen()
                {
                    MaCK = "AAA",
                    Id = 1,
                },
                new BangGiaTrucTuyen()
                {
                    MaCK = "BBB",
                    Id = 2,
                },
                new BangGiaTrucTuyen()
                {
                    MaCK = "CCC",
                    Id = 3,
                }
            );
        }
    }
}
