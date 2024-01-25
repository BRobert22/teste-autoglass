using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Infla.Repositories
{
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        public SupplierRepository(IDbConnection connection) : base(connection)
        {
        }

        public Supplier GetById(int id)
        {
            const string sql = "SELECT * FROM Suppliers WHERE ID = @Id";
            return _connection.Query<Supplier>(sql, new { Id = id }).FirstOrDefault();
        }

        public List<Supplier> GetAll(int page, int pageSize)
        {
            int offset = (page - 1) * pageSize;

            const string sql = @"SELECT *
                        FROM Suppliers
                        ORDER BY ID
                        OFFSET @Offset rows fetch next @PageSize rows only";

            List<Supplier> result = _connection.Query<Supplier>(
                sql,
                new { PageSize = pageSize, Offset = offset }
            ).ToList();

            return result;
        }


        public int Add(Supplier supplier)
        {
            const string sql =
                @"insert into Suppliers (Description, Cnpj)
                        values (@Description, @CNPJ);
                    SELECT SCOPE_IDENTITY() AS ID;";

            return _connection.QuerySingle<int>(sql, supplier);
            return _connection.QuerySingle<int>(sql, supplier);
        }

        public bool Update(Supplier supplier)
        {
            const string sql = @"update Suppliers
                                    set Description        = @Description
                                      , Cnpj             = @CNPJ
                                    where ID = @Id;";

            int rowsAffected = _connection.Execute(sql, supplier);
            return rowsAffected > 0;
        }

        public bool Remove(int id)
        {
            const string sql = "Delete from Suppliers WHERE ID = @id";
            int rowsAffected = _connection.Execute(sql, new { id });
            return rowsAffected > 0;
        }

        public bool IsCnpjUnique(string cnpj)
        {
            const string sql = "SELECT COUNT(*) FROM Suppliers WHERE Cnpj = @Cnpj";
            int count = _connection.QueryFirstOrDefault<int>(sql, new { Cnpj = cnpj });
            return count == 0;
        }
    }
}