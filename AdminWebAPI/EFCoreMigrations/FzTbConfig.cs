using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entitys;

namespace EFCoreMigrations;

public class FzTbConfig:IEntityTypeConfiguration<FzTbs>
{
    public void Configure(EntityTypeBuilder<FzTbs> builder)
    {
        builder.ToTable("FzTb");
    }
}