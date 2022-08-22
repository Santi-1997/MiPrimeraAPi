using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server=DESKTOP-DP5P25Q;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Venta> GetVentas()
        {
            List<Venta> resultados = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me asegyri que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                resultados.Add(venta);
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
