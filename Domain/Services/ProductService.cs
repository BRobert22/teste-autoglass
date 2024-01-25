using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Results;

namespace Domain.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public GenericResult<Product> GetById(int id)
        {
            try
            {
                Product product = _productRepository.GetById(id);

                if (product == null)
                    return new GenericResult<Product>(false, "Product not found", null);

                return new GenericResult<Product>(true, "Success", product);
            }
            catch (Exception ex)
            {
                return new GenericResult<Product>(false, $"Error retrieving product: {ex.Message}", null);
            }
        }

        public GenericResult<int> Add(Product product)
        {
            try
            {
                product.Validate();
                
                if (!product.IsValid)
                {
                    List<string> validationErrors = product.Notifications
                        .Select(notification => notification.Message)
                        .ToList();
                    
                    string errors = string.Join(", ", validationErrors);

                    return new GenericResult<int>(false, $"Validation failed - {{{errors}}}", null);
                }

                int productId = _productRepository.Add(product);

                return new GenericResult<int>(true, "Product added successfully", productId);
            }
            catch (Exception ex)
            {
                return new GenericResult<int>(false, $"Error adding product: {ex.Message}", null);
            }
        }

        public GenericResult<bool> Update(Product product)
        {
            try
            {
                product.Validate();
                
                if (!product.IsValid)
                {
                    List<string> validationErrors = product.Notifications
                        .Select(notification => notification.Message)
                        .ToList();
                    
                    string errors = string.Join(", ", validationErrors);

                    return new GenericResult<bool>(false, $"Validation failed - {{{errors}}}", null);
                }
                
                bool success = _productRepository.Update(product);

                if (!success)
                    return new GenericResult<bool>(false, "Update failed", null);

                return new GenericResult<bool>(true, "Product updated successfully", true);
            }
            catch (Exception ex)
            {
                return new GenericResult<bool>(false, $"Error updating product: {ex.Message}", null);
            }
        }

        public GenericResult<bool> Delete(int id)
        {
            try
            {
                bool success = _productRepository.Remove(id);

                if (!success)
                    return new GenericResult<bool>(false, "Deletion failed", null);

                return new GenericResult<bool>(true, "Product deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new GenericResult<bool>(false, $"Error deleting product: {ex.Message}", null);
            }
        }

        public GenericResult<List<Product>> GetAll(int page, int pageSize)
        {
            try
            {
                List<Product> products = _productRepository.GetAll(page, pageSize);

                return new GenericResult<List<Product>>(true, "Success", products);
            }
            catch (Exception ex)
            {
                return new GenericResult<List<Product>>(false, $"Error retrieving products: {ex.Message}", null);
            }
        }
    }
}