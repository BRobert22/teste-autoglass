using System.Collections.Generic;
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
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(IMapper mapper, ProductService productService) : base(mapper)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GenericResult<Product> result = _productService.GetById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            ProductDTO data = _mapper.Map<ProductDTO>(result.Data);
            
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreateDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            GenericResult<int> result = _productService.Add(product);

            if (!result.Success)
                return BadRequest(result.Message);

            return Created($"api/product/{product.Id}", productDTO);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateDTO productDTO)
        {
            Product product = _mapper.Map<Product>(productDTO);
            product.Id = id;
            GenericResult<bool> result = _productService.Update(product);
            
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();
            
            GenericResult<bool> result = _productService.Delete(id);
            
            if (!result.Success)
                return NotFound(result.Message);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll(int page, int pageSize)
        {
            GenericResult<List<Product>> result = _productService.GetAll(page, pageSize);

            if (result.Success == false)
                return NotFound(result.Message);
            
            List<ProductDTO> data = _mapper.Map<List<ProductDTO>>(result.Data);

            return Ok(data);
        }
    }
}