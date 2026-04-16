using CibertecDemo.Models;
using Microsoft.Data.SqlClient;

namespace CibertecDemo.Data
{
    public class ProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration config) 
        {
            _connectionString = config.GetConnectionString("Conn");
        
        }

        public async Task AgregarProductoAsync(ProductoModel prod)
        {
            var sql = "INSERT INTO Producto(Nombre, Precio, Cantidad, Estado) values(@Nombre, @Precio, @Cantidad, @Estado)";
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nombre", prod.Nombre);
                cmd.Parameters.AddWithValue("@Precio", prod.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", prod.Cantidad);
                cmd.Parameters.AddWithValue("@Estado", prod.Estado);
            
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<ProductoModel>> ObtenerProductosAsync() 
        {
            var list = new List<ProductoModel>();
            var query = "dbo.sp_FiltrarProductosPaginados";
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) {
                        list.Add(new ProductoModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Precio = reader.GetDecimal(2),
                            Cantidad = reader.GetInt32(3),
                            Estado = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return list;
        }
    }
}
