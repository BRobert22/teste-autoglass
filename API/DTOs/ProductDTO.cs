using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public struct ProductDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Situation { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string SupplierDescription { get; set; }
        [Required]
        public string SupplierCnpj { get; set; }
    }
    
    public struct ProductCreateDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Situation { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public int SupplierID { get; set; }
    }
    
    public struct ProductUpdateDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Situation { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public int SupplierID { get; set; }
    }
}