using BusinessObject.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAll();
        Task<ProductEntity> GetById(int id);
    }
}
