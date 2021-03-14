using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagementSystem.Data.Configurations
{
    /// <summary>
    /// User Type tablosuna ait alanların özellikllerinin belirtildiği sınıf. 
    /// </summary>
    class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Code).IsRequired();           
            builder.ToTable("UserType");
        }
    }
}
