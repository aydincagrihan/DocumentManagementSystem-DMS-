using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagementSystem.Data.Configurations
{
    /// <summary>
    /// Student Final Grade tablosuna ait alanların özellikllerinin belirtildiği sınıf. 
    /// </summary>
    class StudentFinalGradeConfiguration : IEntityTypeConfiguration<StudentFinalGrade>
    {
        public void Configure(EntityTypeBuilder<StudentFinalGrade> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.ReportsId).IsRequired();
            builder.ToTable("StudentFinalGrade");
        }
    }
}
