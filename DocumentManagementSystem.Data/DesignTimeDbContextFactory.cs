using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagementSystem.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DmsDbContext>
    {
        public DmsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DmsDbContext>();
            var connectionString = "server=94.73.148.157;port=3306;database=emu-dms_site_db;uid=emu-d_ms;password=AHvb74C1";
            builder.UseMySql(connectionString);
            return new DmsDbContext(builder.Options);
        }
    }
}
