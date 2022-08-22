using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-DP5P25Q;Database=SistemaGestion;Trusted_Connection=True";

        public static List<ProductoVendido> GetProductosVendidos()
        {
            List<ProductoVendido> resultados = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me asegyri que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                resultados.Add(productoVendido);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return resultados;
        }
    }
}
