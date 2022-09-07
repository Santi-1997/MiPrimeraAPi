using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server=DESKTOP-DP5P25Q;Database=SistemaGestion;Trusted_Connection=True";

        public static List<VentaConProducto> GetVentas()
        {
            List<VentaConProducto> resultados = new List<VentaConProducto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta AS V " +
                    "INNER JOIN ProductoVendido AS PV ON V.Id = PV.IdVenta " +
                    "INNER JOIN Producto AS P ON P.Id = PV.IdProducto", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                VentaConProducto venta = new VentaConProducto();

                                venta.Venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Venta.Comentarios = dataReader["Comentarios"].ToString();
                                venta.DescripcionProducto = dataReader["Descripciones"].ToString();

                                resultados.Add(venta);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return resultados;
        }
        public static bool EliminarVenta(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE V FROM Venta AS V " +
                    "WHERE @id = V.Id ";
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

    public class VentaConProducto
    {
        public Venta Venta { get; set; }
        public string DescripcionProducto { get; set; }

        public VentaConProducto()
        {
            Venta = new Venta();
        }
    }
}


