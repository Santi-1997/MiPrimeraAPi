using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=DESKTOP-DP5P25Q;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me asegyri que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();

                                resultados.Add(producto);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return resultados;
        }

        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto] " +
                    "(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES " +
                    "(@descripcionesParameter, @costoParameter, @precioVentaParameter, @stockParameter, @idUsuarioParameter);";

                SqlParameter descripcionesParameter = new SqlParameter("descripcionesParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costoParameter", SqlDbType.Money) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVentaParameter", SqlDbType.Money) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stockParameter", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("idUsuarioParameter", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery(); // Se ejecuta la sentencia sql

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }

        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                                     "SET Descripciones = @descripcionesParameter," +
                                     "Costo = @costoParameter," +
                                     "PrecioVenta = @precioVentaParameter," +
                                     "Stock = @stockParameter " +
                                     "WHERE Id = @idParameter";

                SqlParameter descripcionesParameter = new SqlParameter("descripcionesParameter", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("costoParameter", SqlDbType.Money) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("precioVentaParameter", SqlDbType.Money) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("stockParameter", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idParameter = new SqlParameter("idParameter", SqlDbType.BigInt) { Value = producto.Id };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery(); // Se ejecuta la sentencia sql

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }

        public static bool EliminarProducto(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id = @id";
                SqlParameter idParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                idParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }

            return resultado;
        }
    }
}


