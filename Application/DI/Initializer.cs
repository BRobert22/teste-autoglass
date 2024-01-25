using System;
using System.Data;
using System.Data.SqlClient;
using Domain.Interfaces;
using Domain.Services;
using Infla.Repositories;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Application.DI
{
    public class Initializer
    {
        public static void Configure (IServiceCollection services, string conection) 
        {
            services.AddScoped<ProductService>();
            services.AddScoped<SupplierService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IDbConnection>(provider => new SqlConnection(conection));
            
        }

    }
}