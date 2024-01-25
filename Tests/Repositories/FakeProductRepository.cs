using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public FakeProductRepository()
        {
            _products = new List<Product>();
            // Adicione alguns dados fictícios para teste
            _products.Add(new Product
            {
                Id = 1, Description = "Product OK 1", Situation = 1, ManufactureDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(1),
                Supplier = new Supplier { ID = 1, Description = "Supplier A", CNPJ = "12345678901234" }
            });
            _products.Add(new Product
            {
                Id = 2, Description = "Product OK 2", Situation = 1, ManufactureDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(2),
                Supplier = new Supplier { ID = 2, Description = "Supplier B", CNPJ = "56789012345678" }
            });
            _products.Add(new Product
            {
                Id = 3, Description = "Product Deleted", Situation = 2, ManufactureDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(3),
                Supplier = new Supplier { ID = 3, Description = "Supplier C", CNPJ = "90123456789012" }
            });
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id && p.Situation != (int)ProductStatusEnum.Deleted);
        }

        public List<Product> GetAll(int page, int pageSize)
        {
            return _products.Where(p => p.Situation != (int)ProductStatusEnum.Deleted).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public int Add(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return product.Id;
        }

        public bool Update(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Description = product.Description;
                existingProduct.Situation = product.Situation;
                existingProduct.ManufactureDate = product.ManufactureDate;
                existingProduct.ExpirationDate = product.ExpirationDate;
                existingProduct.Supplier = product.Supplier;

                return true;
            }

            return false;
        }

        public bool Remove(int id)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return false;
            
            existingProduct.Situation = (byte)ProductStatusEnum.Deleted;
            return true;
        }
    }
}