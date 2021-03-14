using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagementSystem.Data.Configurations
{
    /// <summary>
    /// Semester tablosuna ait alanların özellikllerinin belirtildiği sınıf. 
    /// </summary>
    class SemesterConfiguration : IEntityTypeConfiguration<Semester>
    {
        public void Configure(EntityTypeBuilder<Semester> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SemesterType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Code).IsRequired();           
            builder.ToTable("Semester");
        }
    }
}
