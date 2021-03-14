using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagementSystem.Data.Configurations
{
    /// <summary>
    /// User Type tablosuna ait alanların özellikllerinin belirtildiği sınıf. 
    /// </summary>
    class StudenInfoConfiguration : IEntityTypeConfiguration<StudentInfo>
    {
        public void Configure(EntityTypeBuilder<StudentInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CourseId).IsRequired();
            builder.Property(x => x.ProjectTypeId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.ToTable("StudentInfo");
        }
    }
}
