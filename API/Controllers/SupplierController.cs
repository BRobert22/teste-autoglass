using System.Collections.Generic;
using System.Linq;
using API.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Results;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController: BaseController
    {
        private readonly ProductService _productService;
        private readonly SupplierService _supplierService;
        
        public SupplierController(IMapper mapper, SupplierService supplierService) : base(mapper)
        {
            _supplierService = supplierService;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GenericResult<Supplier> result = _supplierService.GetById(id);
            
            if (!result.Success)
                return BadRequest(result.Message);
            
            SupplierDTO supplier = _mapper.Map<SupplierDTO>(result.Data);
            
            return Ok(supplier);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SupplierCreateDTO supplierDTO)
        {
            Supplier supplier = _mapper.Map<Supplier>(supplierDTO);
            GenericResult<int> result  = _supplierService.Add(supplier);

            if (!result.Success)
                return BadRequest(result.Message);
            
            return Created($"api/supplier/{supplier.ID}", supplierDTO );
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SupplierUpdateDTO supplierDTO)
        {
            Supplier supplier = _mapper.Map<Supplier>(supplierDTO);
            supplier.ID = id;
            GenericResult<bool> result = _supplierService.Update(supplier);
            
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _supplierService.Delete(id);
            return NoContent();
        }
        
        [HttpGet]
        public IActionResult GetAll(int page, int pageSize)
        {
            GenericResult<List<Supplier>> result = _supplierService.GetAll(page, pageSize);
            
            if (result == null)
                return NotFound();
            
            List<SupplierDTO> data = _mapper.Map<List<SupplierDTO>>(result.Data);
            
            return Ok(data);
        }
    }
}