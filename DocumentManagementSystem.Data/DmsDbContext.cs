using DocumentManagementSystem.Core.Entities;
using DocumentManagementSystem.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Data
{
    public class DmsDbContext : DbContext
    {
        public DmsDbContext(DbContextOptions<DmsDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Programs> Programs { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<StudentInfo> StudenInfos { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<StudentFinalGrade> StudentFinalGrades { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Burda tablo özelliklerini belirtiyoruz.
            modelBuilder.ApplyConfiguration(new UserConfiguration()); //User Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration()); //User Type Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new ProgramConfiguration()); //Program Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new CourseConfiguration()); //Course Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new ProjectTypeConfiguration()); //ProjectType Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new StudenInfoConfiguration()); //StudentInfo Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new ReporstConfiguration()); //Reports Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new StudentFinalGradeConfiguration()); //Reports Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new SemesterConfiguration()); //Semester Tablosuna Ait Özellikler
            modelBuilder.ApplyConfiguration(new AnnouncementConfiguration()); //Announcement Tablosuna Ait Özellikler
        }
    }
}
