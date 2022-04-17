using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ChungKhoanASPDotNetCore.Entities.Configuratuions
{
    public class LenhDatConfiguration : IEntityTypeConfiguration<LenhDat>
    {
        public void Configure(EntityTypeBuilder<LenhDat> builder)
        {
            builder.ToTable("LenhDat");
            builder.HasKey(x => x.Id);
        }
    }
}
