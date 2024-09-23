using BusinessObject.Entities;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoryRepository(AppDbContext context)
        : BaseRepository<Category>(context), ICategoryRepository;
}
