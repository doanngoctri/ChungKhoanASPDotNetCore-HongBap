using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ChungKhoanASPDotNetCore.Entities.Configuratuions
{
    public class BangGiaTrucTuyenConfiguration : IEntityTypeConfiguration<BangGiaTrucTuyen>
    {
        public void Configure(EntityTypeBuilder<BangGiaTrucTuyen> builder)
        {
            builder.ToTable("BangGiaTrucTuyen");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.MaCK).IsUnique();
        }
    }
}