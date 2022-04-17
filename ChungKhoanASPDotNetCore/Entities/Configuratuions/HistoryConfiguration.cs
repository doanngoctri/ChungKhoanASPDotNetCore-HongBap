using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ChungKhoanASPDotNetCore.Entities.Configuratuions
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("_History");
            builder.HasKey(x => x.FileName);
        }
    }
}
