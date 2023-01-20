using Endeksa.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TkgmDbContext _context;

        public UnitOfWork(TkgmDbContext context)
        {
            _context = context;
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
