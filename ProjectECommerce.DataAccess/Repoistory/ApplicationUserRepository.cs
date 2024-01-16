using ProjectECommerce.DataAccess.Data;
using ProjectECommerce.DataAccess.Repoistory.IRepository;
using ProjectECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectECommerce.DataAccess.Repoistory
{
    public class ApplicationUserRepository : Repository<AppliactionUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
