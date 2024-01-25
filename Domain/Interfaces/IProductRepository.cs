using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Product GetById(int id);
        List<Product> GetAll(int page, int pageSize);
        int Add(Product product);
        bool Update(Product product);
        bool Remove(int id);
    }
}