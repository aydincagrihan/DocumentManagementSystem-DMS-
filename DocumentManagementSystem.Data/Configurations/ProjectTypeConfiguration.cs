using DocumentManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Data.Configurations
{
	class ProjectTypeConfiguration : IEntityTypeConfiguration<ProjectType>
	{
		public void Configure(EntityTypeBuilder<ProjectType> builder)
		{
			builder.HasKey(x => x.Id);//Primary Key
			builder.Property(x => x.Id).UseMySqlIdentityColumn();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Code).IsRequired();
			builder.ToTable("ProjectType");//Veritabanındaki Tablo Adı
		}
	}
}
