using DocumentManagementSystem.Core.Repositories;
using DocumentManagementSystem.Core.UnitOfWork;
using DocumentManagementSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DmsDbContext _context;
        public UnitOfWork(DmsDbContext dmsDbContext)
        {
            _context = dmsDbContext;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
