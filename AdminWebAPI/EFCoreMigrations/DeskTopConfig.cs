using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entitys;

namespace EFCoreMigrations;

public class DeskTopConfig:IEntityTypeConfiguration<DeskTops>
{
    public void Configure(EntityTypeBuilder<DeskTops> builder)
    {
        builder.ToTable("DeskTop");
    }
}