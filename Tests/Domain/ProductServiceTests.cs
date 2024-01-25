using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using NUnit.Framework;
using Tests.Repositories;

namespace Tests.Domain
{
    [TestFixture]
    public class ProductServiceTests
    {
        private readonly IProductRepository fakeProductRepository;
        private readonly ProductService productService;
        private readonly int ProducstCount;

        public ProductServiceTests()
        {
            fakeProductRepository = new FakeProductRepository();
            productService = new ProductService(fakeProductRepository);
        }

        [Test]
        public void GetById_ReturnsCorrectProduct()
        {
            var result = productService.GetById(2);
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Product product = (Product)result.Data;
            Assert.AreEqual(2, product.Id);
        }
        
        [Test]
        public void GetAll_ReturnsCorrectNumberOfProducts()
        {
            var result = productService.GetAll(1, 10);
            
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result);
            List<Product> products = (List<Product>)result.Data;
            Assert.AreEqual(2, products.Count);
        }

        [Test]
        public void Add_ProductWithInvalidData_ReturnsError()
        {
            var newProduct = new Product
            {
                Description = "New Product Test", Situation = 1, ManufactureDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMonths(-1),
                Supplier = new Supplier { ID = 1}
            };
            
            var result = productService.Add(newProduct);

            Assert.IsFalse(result.Success);
        }
        
        [Test]
        public void Add_AddsProductToRepository()
        {
            var newProduct = new Product
            {
                Description = "New Product Test", Situation = 1, ManufactureDate = DateTime.Now.AddMonths(-1),
                ExpirationDate = DateTime.Now.AddMonths(1),
                Supplier = new Supplier { ID = 1}
            };
            var result = productService.Add(newProduct);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(4, result.Data); 
            Assert.AreEqual(4, fakeProductRepository.GetById((int)result.Data)?.Id);
        }

        [Test]
        public void Update_UpdatesProductInRepository()
        {
            var fakeRepository = new FakeProductRepository();
            var productService = new ProductService(fakeRepository);

            var existingProduct = fakeRepository.GetById(1);
            existingProduct.Description = "Updated Product Test";

            var result = productService.Update(existingProduct);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Updated Product Test", fakeRepository.GetById(1)?.Description);
        }

        [Test]
        public void Delete_SetsSituationToDeletedInRepository()
        {
            var result = productService.Delete(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(null, fakeProductRepository.GetById(1));
        }
    }
}