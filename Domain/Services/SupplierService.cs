using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Results;

namespace Domain.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public GenericResult<Supplier> GetById(int id)
        {
            try
            {
                Supplier supplier = _supplierRepository.GetById(id);

                if (supplier == null)
                    return new GenericResult<Supplier>(false, "Supplier not found", null);

                return new GenericResult<Supplier>(true, "Success", supplier);
            }
            catch (Exception ex)
            {
                return new GenericResult<Supplier>(false, $"Error retrieving supplier: {ex.Message}", null);
            }
        }

        public GenericResult<int> Add(Supplier supplier)
        {
            try
            {
                if (!_supplierRepository.IsCnpjUnique(supplier.CNPJ))
                    return new GenericResult<int>(false, "Supplier with the same CNPJ already exists", null);
                
                int supplierId = _supplierRepository.Add(supplier);

                return new GenericResult<int>(true, "Supplier added successfully", supplierId);
            }
            catch (Exception ex)
            {
                return new GenericResult<int>(false, $"Error adding supplier: {ex.Message}", null);
            }
        }

        public GenericResult<bool> Update(Supplier supplier)
        {
            try
            {
                bool success = _supplierRepository.Update(supplier);

                if (!success)
                    return new GenericResult<bool>(false, "Update failed", null);

                return new GenericResult<bool>(true, "Supplier updated successfully", true);
            }
            catch (Exception ex)
            {
                return new GenericResult<bool>(false, $"Error updating supplier: {ex.Message}", null);
            }
        }

        public GenericResult<bool> Delete(int id)
        {
            try
            {
                bool success = _supplierRepository.Remove(id);

                if (!success)
                    return new GenericResult<bool>(false, "Deletion failed", null);

                return new GenericResult<bool>(true, "Supplier deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new GenericResult<bool>(false, $"Error deleting supplier: {ex.Message}", null);
            }
        }

        public GenericResult<List<Supplier>> GetAll(int page, int pageSize)
        {
            try
            {
                List<Supplier> suppliers = _supplierRepository.GetAll(page, pageSize);

                return new GenericResult<List<Supplier>>(true, "Success", suppliers);
            }
            catch (Exception ex)
            {
                return new GenericResult<List<Supplier>>(false, $"Error retrieving suppliers: {ex.Message}", null);
            }
        }
    }
}