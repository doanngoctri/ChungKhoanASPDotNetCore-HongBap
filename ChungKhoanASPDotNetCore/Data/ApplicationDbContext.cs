using Microsoft.EntityFrameworkCore;
using ChungKhoanASPDotNetCore.Entities;
using ChungKhoanASPDotNetCore.Entities.Configuratuions;
namespace ChungKhoanASPDotNetCore.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LenhDat> LenhDats { get; set; }
        public DbSet<LenhKhop> LenhKhops { get; set; }
        public DbSet<BangGiaTrucTuyen> BangGiaTrucTuyens { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BangGiaTrucTuyenConfiguration());
            builder.ApplyConfiguration(new LenhDatConfiguration());
            builder.ApplyConfiguration(new LenhKhopConfiguration());
            builder.ApplyConfiguration(new HistoryConfiguration());

            builder.Seed();
        }
    }
}
