using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ChungKhoanASPDotNetCore.Entities.Configuratuions
{
    public class LenhKhopConfiguration: IEntityTypeConfiguration<LenhKhop>
    {
        public void Configure(EntityTypeBuilder<LenhKhop> builder)
        {
            builder.ToTable("LenhKhop");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.LenhDat).WithMany(x => x.LenhKhops).HasForeignKey(x => x.IdLenhDat);
        }
    }
}
