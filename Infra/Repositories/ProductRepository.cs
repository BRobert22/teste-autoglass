using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Infla.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbConnection connection) : base(connection)
        {
        }

        public Product GetById(int id)
        {
            const string sql = @"SELECT p.*, s.*
                                    FROM Products p
                                    JOIN Suppliers s ON p.SupplierID = s.ID
                                    WHERE p.Id = @Id AND p.Situation <> @Situation";

            Product result = _connection.Query<Product, Supplier, Product>(
                sql,
                (product, supplier) =>
                {
                    product.Supplier = supplier;
                    return product;
                },
                new { Id = id, Situation = ProductStatusEnum.Deleted },
                splitOn: "ID"
            ).FirstOrDefault();

            return result;
        }


        public List<Product> GetAll(int page, int pageSize)
        {
            int offset = (page - 1) * pageSize;

            const string sql = @"SELECT p.*, s.*
                        FROM Products p
                        JOIN Suppliers s ON p.SupplierID = s.ID
                        WHERE p.Situation <> @Situation
                        ORDER BY p.Id
                        OFFSET @Offset rows fetch next @PageSize rows only";

            List<Product> result = _connection.Query<Product, Supplier, Product>(
                sql,
                (product, supplier) =>
                {
                    product.Supplier = supplier;
                    return product;
                },
                new { Situation = ProductStatusEnum.Deleted, PageSize = pageSize, Offset = offset },
                splitOn: "ID"
            ).ToList();

            return result;
        }

        public int Add(Product product)
        {
            const string sql =
                @"insert into Products (Description, Situation, ManufactureDate, ExpirationDate, SupplierID)
                                values ( @Description
                                        , @Situation
                                        , @ManufactureDate
                                        , @ExpirationDate
                                        , @SupplierID);
                    SELECT SCOPE_IDENTITY() AS ID;";

            var parameters = new
            {
                product.Description,
                product.Situation,
                product.ManufactureDate,
                product.ExpirationDate,
                SupplierID = product.Supplier.ID
            };

            return _connection.Execute(sql, parameters);
        }

        public bool Update(Product product)
        {
            const string sql = @"update Products
                                    set Description         = @Description
                                      , Situation           = @Situation
                                      , ManufactureDate   = @ManufactureDate
                                      , ExpirationDate      = @ExpirationDate
                                      , SupplierID          = @SupplierID
                                    where ID = @Id;";

            var parameters = new
            {
                product.Id,
                product.Description,
                product.Situation,
                product.ManufactureDate,
                product.ExpirationDate,
                SupplierID = product.Supplier.ID
            };

            int rowsAffected = _connection.Execute(sql, parameters);
            return rowsAffected > 0;
        }

        public bool Remove(int id)
        {
            const string sql = "update Products set Situation = @situation WHERE Id = @id";
            int rowsAffected = _connection.Execute(sql, new { situation = ProductStatusEnum.Deleted, id });
            return rowsAffected > 0;
        }
    }
}