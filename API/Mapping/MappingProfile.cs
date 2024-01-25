using System.Collections.Generic;
using Domain.Entities;
using API.DTOs;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => new Supplier { ID = src.SupplierID }))
                .ForSourceMember(src => src.SupplierID, opt => opt.DoNotValidate());
            CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => new Supplier { ID = src.SupplierID }))
                .ForSourceMember(src => src.SupplierID, opt => opt.DoNotValidate());
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.SupplierDescription, opt => opt.MapFrom(src => src.Supplier.Description))
                .ForMember(dest => dest.SupplierCnpj, opt => opt.MapFrom(src => src.Supplier.CNPJ));;
            
            CreateMap<SupplierCreateDTO, Supplier>();
            CreateMap<SupplierUpdateDTO, Supplier>();
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<Supplier, SupplierDTO>();
            
        }
    }
}