using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Supplier GetById(int id);
        List<Supplier> GetAll(int page, int pageSize);
        int Add(Supplier supplier);
        bool Update(Supplier supplier);
        bool Remove(int id);
        bool IsCnpjUnique(String cnpj);
    }
}