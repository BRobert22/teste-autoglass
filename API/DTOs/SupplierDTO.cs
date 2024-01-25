using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class SupplierDTO
    {
        public int ID { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String CNPJ { get; set; }
    }
    public class SupplierCreateDTO
    {
        [Required]
        public String Description { get; set; }
        [Required]
        public String CNPJ { get; set; }
    }
    
    public class SupplierUpdateDTO
    {
        [Required]
        public String Description { get; set; }
        [Required]
        public String CNPJ { get; set; }
    }
}