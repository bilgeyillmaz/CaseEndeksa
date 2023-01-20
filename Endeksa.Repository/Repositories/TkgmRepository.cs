using Endeksa.Core.Models;
using Endeksa.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Repository.Repositories
{
    public class TkgmRepository : GenericRepository<Root>, ITkgmRepository
    {
        public TkgmRepository(TkgmDbContext context) : base(context)
        {
        }
    }
}
