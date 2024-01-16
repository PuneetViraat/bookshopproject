using ProjectECommerce.DataAccess.Data;
using ProjectECommerce.DataAccess.Repoistory.IRepoistory;
using ProjectECommerce.DataAccess.Repoistory.IRepository;
using ProjectECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectECommerce.DataAccess.Repoistory
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
                
        }
    }
}
