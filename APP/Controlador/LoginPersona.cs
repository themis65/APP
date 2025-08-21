using APP.Modelo;
using APP.Vista;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP.Controlador
{
    internal class LoginPersona
    {
        private readonly Conexion _conexion = new Conexion();
        private Boolean errormysql = false;

        private string HashSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public bool Logout(int idUsuario) 
        {
            string updateQuery = "UPDATE usuario SET sesion_activa = 0 WHERE id_usuario = @id";
            try
            {
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idUsuario);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Error al cerrar la sesión del usuario.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MySQL no disponible: " + ex.Message);
                
            }
            return false;

        }


        public bool Login(string username, string password, out string nombre, out string apellido, out int id_usuario, out string rol)
        {
            nombre = "";
            apellido = "";
            id_usuario = 0;
            rol = "";
            string passwordHash = HashSHA256(password);

            try
            {
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    // Consulta para obtener los datos del usuario
                    string query = @"
                SELECT u.id_usuario, u.sesion_activa, r.nombre_rol, d.nombres, d.apellidos
                FROM usuario u
                INNER JOIN roles r ON r.id_rol = u.id_rol
                INNER JOIN (
                    SELECT id_usuario, nombres, apellidos FROM paciente
                    UNION ALL
                    SELECT id_usuario, nombres, apellidos FROM medico
                    UNION ALL
                    SELECT id_usuario, nombres, apellidos FROM auditor
                ) AS d ON d.id_usuario = u.id_usuario
                WHERE u.nombre_usuario = @user AND u.contrasena = @pass";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", passwordHash);

                        int sesionActiva;

                        // Leer datos del usuario
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de inicio de sesión",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }

                            id_usuario = reader.GetInt32("id_usuario");
                            sesionActiva = reader.GetInt32("sesion_activa");
                            rol = reader.GetString("nombre_rol");
                            nombre = reader.GetString("nombres");
                            apellido = reader.GetString("apellidos");
                        }

                        // Verificar si ya hay sesión activa
                        if (sesionActiva == 1)
                        {
                            MessageBox.Show("El usuario ya está conectado.", "Sesión activa",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        // Actualizar sesion_activa a 1
                        string updateQuery = "UPDATE usuario SET sesion_activa = 1 WHERE id_usuario = @id";
                        using (var updateCmd = new MySqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@id", id_usuario);
                            int filasAfectadas = updateCmd.ExecuteNonQuery();
                            if (filasAfectadas == 0)
                            {
                                MessageBox.Show("Error al actualizar la sesión del usuario.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MySQL no disponible: " + ex.Message);
                return false;
            }
            if (errormysql)
            {
                try
                {

                    var database = _conexion.ObtenerConexionMongo();
                    var collection = database.GetCollection<BsonDocument>("usuario");

                    var pipeline = new[]
                    {
            new BsonDocument("$match", new BsonDocument
            {
                { "nombre_usuario", username },
                { "contrasena", passwordHash }
            }),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "roles" },
                { "localField", "rol_id" },
                { "foreignField", "_id" },
                { "as", "rol" }
            }),
            new BsonDocument("$unwind", "$rol"),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "paciente" },
                { "localField", "_id" },
                { "foreignField", "usuario_id" },
                { "as", "paciente" }
            }),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "medico" },
                { "localField", "_id" },
                { "foreignField", "usuario_id" },
                { "as", "medico" }
            }),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "auditor" },
                { "localField", "_id" },
                { "foreignField", "usuario_id" },
                { "as", "auditor" }
            }),
            new BsonDocument("$addFields", new BsonDocument
            {
                { "datos", new BsonDocument("$concatArrays", new BsonArray { "$paciente", "$medico", "$auditor" }) }
            }),
            new BsonDocument("$unwind", "$datos"),
            new BsonDocument("$project", new BsonDocument
            {
                { "id_usuario", "$_id" },
                { "nombre_rol", "$rol.nombre_rol" },
                { "nombres", "$datos.nombres" },
                { "apellidos", "$datos.apellidos" }
            })
        };

                    var result = collection.Aggregate<BsonDocument>(pipeline).FirstOrDefault();

                    if (result != null)
                    {
                        id_usuario = result["id_usuario"].AsInt32;
                        rol = result["nombre_rol"].AsString;
                        nombre = result["nombres"].AsString;
                        apellido = result["apellidos"].AsString;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con MongoDB: " + ex.Message);
                    MessageBox.Show("Error al conectar con la base de datos. Por favor, intente más tarde.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            return false;
        }
    }
}
