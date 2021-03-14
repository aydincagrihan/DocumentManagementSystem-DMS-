using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Data.Configurations
{
    /// <summary>
    /// User tablosuna ait alanların özellikllerinin belirtildiği sınıf. 
    /// </summary>
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Gender).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PictureUrl).IsRequired(false);            
            builder.Property(x => x.RegisterDate).IsRequired();
            builder.Property(x => x.UserTypeId).IsRequired();          
            builder.ToTable("User");
        }
    }
}
