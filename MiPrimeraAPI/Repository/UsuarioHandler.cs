using MiPrimeraAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraAPI.Repository
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = "Server=DESKTOP-DP5P25Q;Database=SistemaGestion;Trusted_Connection=True";

        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me asegyri que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                resultados.Add(usuario);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            return resultados;
        }

        public static bool EliminarUsuario(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Usuario WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
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
        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                    "SET Nombre = @nombre,"+
                         "Apellido = @apellido," +
                          "Contraseña = @contraseña," +
                          "NombreUsuario = @nombreUsuario," +
                          "Mail = @mail " +
                    "WHERE Id = @id ";

                SqlParameter nombreParameter = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter idParameter = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };
                SqlParameter apellidoParameter = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter mailParameter = new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail };
                SqlParameter contraseñaParameter = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };


                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(mailParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);


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

        public static bool CrearUsuario(Usuario usuario)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Usuario] " +
                    "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES " +
                    "(@nombreParameter, @apellidoParameter, @nombreUsuarioParameter, @contraseñaParameter, @mailParameter);";

                SqlParameter nombreParameter = new SqlParameter("nombreParameter", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mailParameter", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

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
    }
}