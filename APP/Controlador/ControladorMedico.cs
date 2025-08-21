using APP.Controlador;
using APP.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP.Controlador
{
    internal class ControladorMedico
    {
        private readonly Conexion _conexion;
        public ControladorMedico(Conexion conexion)
        {
            _conexion = conexion;
        }

        public bool insertarPaciente(string cedula, int idUsuario, string nombres, string apellidos, DateTime fNacimiento, string celular, string descripcion)
        {
            try
            {
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Verificar si el paciente ya existe
                        string queryVerificar = @"SELECT COUNT(*) FROM paciente WHERE id_cedula = @id_cedula";
                        using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conn, transaction))
                        {
                            cmdVerificar.Parameters.AddWithValue("@id_cedula", cedula);
                            int count = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                            if (count > 0)
                            {
                                DialogResult resultado = MessageBox.Show(
                                    "Paciente ya existe, ¿desea actualizar la información?",
                                    "Paciente existente",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
                                );

                                if (resultado == DialogResult.Yes)
                                {
                                    string actualizarQuery = @"UPDATE paciente 
                                    SET id_usuario = @id_usuario, 
                                        nombres = @nombres, 
                                        apellidos = @apellidos, 
                                        fecha_nacimiento = @fecha_nacimiento, 
                                        telefono = @celular 
                                    WHERE id_cedula = @id_cedula;";
                                    using (MySqlCommand cmdActualizar = new MySqlCommand(actualizarQuery, conn, transaction))
                                    {
                                        cmdActualizar.Parameters.AddWithValue("@id_cedula", cedula);
                                        cmdActualizar.Parameters.AddWithValue("@id_usuario", idUsuario);
                                        cmdActualizar.Parameters.AddWithValue("@nombres", nombres);
                                        cmdActualizar.Parameters.AddWithValue("@apellidos", apellidos);
                                        cmdActualizar.Parameters.AddWithValue("@fecha_nacimiento", fNacimiento.ToString("yyyy-MM-dd"));
                                        cmdActualizar.Parameters.AddWithValue("@celular", celular);
                                        cmdActualizar.ExecuteNonQuery();
                                    }

                                    string queryActualizarHistorial = @"UPDATE historial_medico 
                                    SET descripcion_general = @descripcion, 
                                        fecha_ultima_actualizacion = NOW() 
                                    WHERE id_paciente = @id_cedula;";
                                    using (MySqlCommand cmdHistorial = new MySqlCommand(queryActualizarHistorial, conn, transaction))
                                    {
                                        cmdHistorial.Parameters.AddWithValue("@id_cedula", cedula);
                                        cmdHistorial.Parameters.AddWithValue("@descripcion", descripcion);
                                        cmdHistorial.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Paciente actualizado correctamente.");
                                    return true;
                                }
                                string eliminarUsuarioQuery = @"DELETE FROM usuario WHERE id_usuario = @id_usuario;";
                                using (MySqlCommand cmdEliminarUsuario = new MySqlCommand(eliminarUsuarioQuery, conn, transaction))
                                {
                                    cmdEliminarUsuario.Parameters.AddWithValue("@id_usuario", idUsuario);
                                    cmdEliminarUsuario.ExecuteNonQuery();
                                }
                                transaction.Commit();
                                MessageBox.Show("Operación cancelada, usuario borrado.");
                                return false;
                            }

                            // ===== Insertar nuevo paciente y su historial =====
                            string query = @"INSERT INTO paciente (id_cedula, id_usuario, nombres, apellidos, fecha_nacimiento, telefono) 
                                     VALUES (@id_cedula, @id_usuario, @nombres, @apellidos, @fecha_nacimiento, @celular);";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id_cedula", cedula);
                                cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                                cmd.Parameters.AddWithValue("@nombres", nombres);
                                cmd.Parameters.AddWithValue("@apellidos", apellidos);
                                cmd.Parameters.AddWithValue("@fecha_nacimiento", fNacimiento.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@celular", celular);
                                cmd.ExecuteNonQuery();
                            }

                            string queryInsertarHistorial = @"INSERT INTO historial_medico (id_paciente, descripcion_general, fecha_ultima_actualizacion) 
                                                      VALUES (@id_cedula, @descripcion, NOW());";
                            using (MySqlCommand cmdHistorial = new MySqlCommand(queryInsertarHistorial, conn, transaction))
                            {
                                cmdHistorial.Parameters.AddWithValue("@id_cedula", cedula);
                                cmdHistorial.Parameters.AddWithValue("@descripcion", descripcion);
                                cmdHistorial.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar paciente: " + ex.Message);
                return false;
            }
        }


        public string[] insertarUsuarioPaciente(string nombre, string apellido, string contrasena)
        {
            string usuario = nombre + apellido;
            string contrasenaHash = HashSHA256(contrasena);
            string[] datosUsuario = new string[2];
            try
            {
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    int num = 0;
                    while (true)
                    {
                        // Verificar si el usuario ya existe
                        string query = @"SELECT COUNT(*) FROM usuario WHERE nombre_usuario = @nombre_usuario";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nombre_usuario", usuario);
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count == 0)
                            {
                                break;
                            }
                            num += num;
                            usuario = usuario + num.ToString();
                        }
                    }
                    string queryInsertarUsuario = @"INSERT INTO usuario (nombre_usuario, contrasena, id_rol) 
                                                    VALUES (@nombre_usuario, @contrasena, 1);";
                    using (MySqlCommand cmd = new MySqlCommand(queryInsertarUsuario, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre_usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasenaHash);
                        int comando = cmd.ExecuteNonQuery();
                        if (comando > 0)
                        {
                            int idUsuario = (int)cmd.LastInsertedId;
                            datosUsuario[0] = idUsuario.ToString();
                            datosUsuario[1] = usuario;
                            return datosUsuario;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el usuario: " + ex.Message);
            }
            return new string[] { "0", "Error al insertar usuario" };
        }


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


        public String[] ObtenerHistoriaClinica(int idCita)
        {
            bool mysqlError = false;
            String[] historiaClinica = new String[4];
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    h.id_historial, 
                    p.id_cedula, 
                    CONCAT(p.apellidos, ' ', p.nombres) AS Paciente,
                    tc.nombre_tipo AS Servicio
                FROM historial_medico h
                INNER JOIN paciente p ON h.id_paciente = p.id_cedula
                INNER JOIN cita c ON p.id_cedula = c.id_paciente
                INNER JOIN tipo_cita tc ON c.id_tipo_cita = tc.id_tipo_cita
                WHERE c.id_cita = @id_cita";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_cita", idCita);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                historiaClinica[0] = reader["id_historial"].ToString();
                                historiaClinica[1] = reader["id_cedula"].ToString();
                                historiaClinica[2] = reader["Paciente"].ToString();
                                historiaClinica[3] = reader["Servicio"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mysqlError = true;
                Console.WriteLine("Error al obtener la historia clínica desde MySQL: " + ex.Message);
            }
            if (mysqlError)
            {
                return ObtenerHistoriaClinicaMongo(idCita);
            }
            return historiaClinica;
        }


        public String[] ObtenerHistoriaClinicaMongo(int idCita)
        {
            String[] historiaClinica = new String[4];
            try
            {
                var database = _conexion.ObtenerConexionMongo();
                var colCita = database.GetCollection<BsonDocument>("cita");

                var pipeline = new[]
                {
            // Buscar la cita específica
            new BsonDocument("$match", new BsonDocument("_id", idCita)),

            // Relacionar con el paciente
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "paciente" },
                { "localField", "paciente_id" },
                { "foreignField", "_id" },
                { "as", "paciente" }
            }),

            // Relacionar con tipo_cita
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "tipo_cita" },
                { "localField", "tipo_cita_id" },
                { "foreignField", "_id" },
                { "as", "tipoCita" }
            }),

            // Relacionar con historial médico
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "historial_medico" },
                { "localField", "paciente_id" },
                { "foreignField", "paciente_id" },
                { "as", "historial" }
            }),

            // Seleccionar y transformar los datos
            new BsonDocument("$project", new BsonDocument
            {
                { "id_historial", new BsonDocument("$arrayElemAt", new BsonArray { "$historial._id", 0 }) },
                { "id_cedula", "$paciente_id" },
                { "Paciente", new BsonDocument("$concat", new BsonArray {
                    new BsonDocument("$arrayElemAt", new BsonArray { "$paciente.apellidos", 0 }),
                    " ",
                    new BsonDocument("$arrayElemAt", new BsonArray { "$paciente.nombres", 0 })
                }) },
                { "Servicio", new BsonDocument("$arrayElemAt", new BsonArray { "$tipoCita.nombre_tipo", 0 }) }
            })
        };

                var result = colCita.Aggregate<BsonDocument>(pipeline).FirstOrDefault();

                if (result != null)
                {
                    historiaClinica[0] = result.GetValue("id_historial", "").ToString();
                    historiaClinica[1] = result.GetValue("id_cedula", "").ToString();
                    historiaClinica[2] = result.GetValue("Paciente", "").ToString();
                    historiaClinica[3] = result.GetValue("Servicio", "").ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la historia clínica desde MongoDB: " + ex.Message);
            }

            return historiaClinica;
        }

        public int InsertarDiagnostico(int idCita, string diagnostico, int idMedico)
        {
            try
            {
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    string query = @"
                INSERT INTO diagnostico (id_cita, descripcion, fecha_registro, id_modificado)
                VALUES (@id_cita, @descripcion, NOW(), @id_medico);";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_cita", idCita);
                        cmd.Parameters.AddWithValue("@descripcion", diagnostico);
                        cmd.Parameters.AddWithValue("@id_medico", idMedico);

                        cmd.ExecuteNonQuery(); // Ejecuta el INSERT
                        int idDiagnostico = (int)cmd.LastInsertedId;
                        InsertarDiagnosticoMongo(idDiagnostico, idCita, diagnostico, idMedico);
                        return idDiagnostico;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el diagnóstico: " + ex.Message);
                return InsertarDiagnosticoMongo(0, idCita, diagnostico, idMedico);
            }

        }

        public int InsertarDiagnosticoMongo(int idDiagnostico, int idCita, string diagnostico, int idMedico)
        {
            try
            {
                int nextId;
                nextId = idDiagnostico;
                var database = _conexion.ObtenerConexionMongo();
                var collection = database.GetCollection<BsonDocument>("diagnostico");
                if (idDiagnostico <= 0)
                {
                    var sort = Builders<BsonDocument>.Sort.Descending("_id");
                    var ultimoDoc = collection.Find(new BsonDocument()).Sort(sort).Limit(1).FirstOrDefault();

                    nextId = 1; // valor por defecto si la colección está vacía
                    if (ultimoDoc != null && ultimoDoc.Contains("_id"))
                    {
                        nextId = ultimoDoc["_id"].AsInt32 + 1;
                    }

                }
                // Crear documento
                var nuevoDiagnostico = new BsonDocument
        {
            { "_id", nextId },
            { "cita_id", idCita },
            { "descripcion", diagnostico },
            { "fecha_registro", DateTime.Now },
            { "modificado_por", idMedico }
        };

                collection.InsertOne(nuevoDiagnostico);
                return nextId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error MongoDB: " + ex.Message);
                return 0;
            }

        }

        public bool ActualizarCita(int idCita, int idDiagnostico, int idUsuario)
        {
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    string query = @"
                UPDATE cita 
                SET id_estado = 2, id_modificado = @id_medico, fecha_modificado = NOW() 
                WHERE id_cita = @id_cita";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_cita", idCita);
                        cmd.Parameters.AddWithValue("@id_medico", idUsuario);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (ActualizarCitaMongo(idCita, idUsuario))
                        {
                            return true;
                        }
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No se pudo actualizar en MySQL.");
                            return false;
                        }
                        MessageBox.Show("Cita actualizada correctamente en MySQL.");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la cita: " + ex.Message);
                MessageBox.Show("Error al actualizar la cita en MySQL, intentando con MongoDB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bool conf = ActualizarCitaMongo(idCita, idUsuario);
                return conf;

            }

        }


        public bool ActualizarCitaMongo(int idCita, int idUsuario)
        {
            try
            {
                var database = _conexion.ObtenerConexionMongo();
                var collection = database.GetCollection<BsonDocument>("cita");


                var filtro = Builders<BsonDocument>.Filter.Eq("_id", idCita);

                // Campos a actualizar
                var update = Builders<BsonDocument>.Update
                    .Set("estado_id", 2)                     // Estado actualizado
                    .Set("modificado_por", idUsuario)        // Usuario que modifica
                    .Set("fecha_modificado", DateTime.Now);  // Fecha de modificación

                // Ejecutar la actualización
                var result = collection.UpdateOne(filtro, update);

                return result.ModifiedCount > 0; // true si se modificó al menos un documento
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la cita en MongoDB: " + ex.Message);
                return false;
            }
        }


        public bool ActualizarHistorial(int idCita, int idDiagnostico, int idHistorialMedico)
        {
            try
            {
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    string query = @"
                    INSERT INTO historial_cita_medica (id_cita, id_diagnostico, id_historial_medico, fecha_consulta)
                    VALUES (@id_cita, @id_diagnostico, @id_historial_medico, NOW());";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_cita", idCita);
                        cmd.Parameters.AddWithValue("@id_diagnostico", idDiagnostico);
                        cmd.Parameters.AddWithValue("@id_historial_medico", idHistorialMedico);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            string updateHistoria = @"
                            UPDATE historial_medico 
                            SET fecha_ultima_actualizacion = NOW() 
                            WHERE id_historial = @id_historial";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateHistoria, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@id_historial", idHistorialMedico);
                                int rows = updateCmd.ExecuteNonQuery();
                                if (ActualizarHistorialMongo(idCita, idDiagnostico, idHistorialMedico))
                                {
                                    return true;
                                }
                                if (rows == 0)
                                {
                                    MessageBox.Show("No se pudo actualizar el historial médico en MySQL.");
                                    return false;
                                }
                                MessageBox.Show("Historial médico actualizado correctamente.");
                                return rows > 0;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el historial médico: " + ex.Message);
                bool conf = ActualizarHistorialMongo(idCita, idDiagnostico, idHistorialMedico);
                return conf;
            }
            return false;
        }


        public bool ActualizarHistorialMongo(int idCita, int idDiagnostico, int idHistorialMedico)
        {
            try
            {
                var database = _conexion.ObtenerConexionMongo();

                // 1️⃣ Insertar en historial_cita_medica
                var collectionHistorialCita = database.GetCollection<BsonDocument>("historial_cita_medica");

                var nuevoHistorialCita = new BsonDocument
        {
            { "cita_id", idCita },
            { "diagnostico_id", idDiagnostico },
            { "historial_medico_id", idHistorialMedico },
            { "fecha_consulta", DateTime.Now }
        };

                collectionHistorialCita.InsertOne(nuevoHistorialCita);

                // 2️⃣ Actualizar historial_medico
                var collectionHistorialMedico = database.GetCollection<BsonDocument>("historial_medico");

                var filtro = Builders<BsonDocument>.Filter.Eq("_id", idHistorialMedico);
                var update = Builders<BsonDocument>.Update
                    .Set("fecha_ultima_actualizacion", DateTime.Now);

                var result = collectionHistorialMedico.UpdateOne(filtro, update);

                return result.ModifiedCount > 0; // true si se actualizó el historial
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar historial en MongoDB: " + ex.Message);
                return false;
            }
        }


        public DataTable ObtenerAgendaMedico(int idUsuario)
        {
            bool mysqlError = false;
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    c.id_cita,
                    c.fecha_cita AS Fecha,
                    c.hora_cita AS Hora,
                    p.nombres AS Nombres,
                    p.apellidos AS Apellidos,
                    tc.nombre_tipo AS Servicio,
                    ec.estado AS Estado_cita
                FROM cita c
                INNER JOIN medico m ON c.id_medico = m.id_cedula
                INNER JOIN paciente p ON c.id_paciente = p.id_cedula
                INNER JOIN tipo_cita tc ON c.id_tipo_cita = tc.id_tipo_cita
                INNER JOIN estado_cita ec ON c.id_estado = ec.id_estado
                WHERE m.id_usuario = @id_usuario AND c.id_estado = 1
                ORDER BY c.fecha_cita, c.hora_cita;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mysqlError = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);
            }
            if (mysqlError)
            {
                try
                {
                    // ================= MongoDB optimizado =================
                    var database = _conexion.ObtenerConexionMongo();

                    // 1️⃣ Buscar el médico a partir del usuario
                    var colMedicos = database.GetCollection<BsonDocument>("medico");
                    var medico = colMedicos.Find(Builders<BsonDocument>.Filter.Eq("usuario_id", idUsuario)).FirstOrDefault();
                    if (medico == null) return new DataTable();

                    string idCedulaMedico = medico["_id"].AsString;

                    // 2️⃣ Pipeline para traer citas con datos de paciente, tipo_cita y estado_cita
                    var colCitas = database.GetCollection<BsonDocument>("cita");
                    var pipeline = new[]
                    {
            new BsonDocument("$match", new BsonDocument
                  {
                    { "medico_id", idCedulaMedico },
                    { "estado_id", 1 } // <-- ID del estado pendiente
                }),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "paciente" },
                { "localField", "paciente_id" },
                { "foreignField", "_id" },
                { "as", "paciente" }
            }),
            new BsonDocument("$unwind", "$paciente"),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "tipo_cita" },
                { "localField", "tipo_cita_id" },
                { "foreignField", "_id" },
                { "as", "tipo_cita" }
            }),
            new BsonDocument("$unwind", "$tipo_cita"),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "estado_cita" },
                { "localField", "estado_id" },
                { "foreignField", "_id" },
                { "as", "estado_cita" }
            }),
            new BsonDocument("$unwind", "$estado_cita"),
            new BsonDocument("$project", new BsonDocument
            {
                { "id_cita", "$_id" },
                { "fecha_cita", "$fecha_cita" },
                { "hora_cita", "$hora_cita" },
                { "nombre_paciente", "$paciente.nombres" },
                { "apellido_paciente", "$paciente.apellidos" },
                { "tipo_cita", "$tipo_cita.nombre_tipo" },
                { "estado_cita", "$estado_cita.estado" }
            }),
            new BsonDocument("$sort", new BsonDocument
            {
                { "fecha_cita", 1 },
                { "hora_cita", 1 }
            })
        };

                    var results = colCitas.Aggregate<BsonDocument>(pipeline).ToList();

                    // 3️⃣ Llenar DataTable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_cita", typeof(int));
                    dt.Columns.Add("Fecha", typeof(string));
                    dt.Columns.Add("Hora", typeof(string));
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("Apellido", typeof(string));
                    dt.Columns.Add("Servicio", typeof(string));
                    dt.Columns.Add("Estado_cita", typeof(string));

                    foreach (var item in results)
                    {
                        string fechaStr = item["fecha_cita"].BsonType == BsonType.DateTime
                            ? item["fecha_cita"].ToUniversalTime().ToString("yyyy-MM-dd")
                            : item["fecha_cita"].AsString;

                        dt.Rows.Add(
                            item["id_cita"].ToInt32(),
                            fechaStr,
                            item["hora_cita"].AsString,
                            item["nombre_paciente"].AsString,
                            item["apellido_paciente"].AsString,
                            item["tipo_cita"].AsString,
                            item["estado_cita"].AsString
                        );
                    }

                    return dt;
                }
                catch (Exception exMongo)
                {
                    MessageBox.Show("Error al conectar con la base de datos, intente mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error al conectar con MongoDB: " + exMongo.Message);
                    return new DataTable();
                }
            }
            return new DataTable();
        }


        public DataTable ObtenerHistorialMedico(int idUsuario)
        {
            bool mysqlError = false;
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    string query = @"
                    SELECT 
                    c.id_cita,
                    c.fecha_cita AS Fecha,
                    c.hora_cita AS Hora,
                    p.apellidos AS Apellidos,
                    p.nombres AS Nombres,
                    tc.nombre_tipo AS Servicio,
                    ec.estado AS Estado_cita,
                    d.descripcion AS Diagnostico
                    FROM cita c
                    INNER JOIN medico m ON c.id_medico = m.id_cedula
                    INNER JOIN paciente p ON c.id_paciente = p.id_cedula
                    INNER JOIN tipo_cita tc ON c.id_tipo_cita = tc.id_tipo_cita
                    INNER JOIN estado_cita ec ON c.id_estado = ec.id_estado
                    LEFT JOIN historial_cita_medica h ON h.id_cita = c.id_cita
                    LEFT JOIN diagnostico d ON h.id_diagnostico = d.id_diagnostico
                    WHERE m.id_usuario = @id_usuario AND c.id_estado = 2
                    ORDER BY c.fecha_cita, c.hora_cita;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mysqlError = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);
            }
            if (mysqlError)
            {
                try
                {
                    // ================= MongoDB optimizado =================
                    var database = _conexion.ObtenerConexionMongo();

                    // 1️⃣ Buscar el médico a partir del usuario
                    var colMedicos = database.GetCollection<BsonDocument>("medico");
                    var medico = colMedicos.Find(Builders<BsonDocument>.Filter.Eq("usuario_id", idUsuario)).FirstOrDefault();
                    if (medico == null) return new DataTable();

                    string idCedulaMedico = medico["_id"].AsString;

                    // 2️⃣ Pipeline para traer citas con datos de paciente, tipo_cita y estado_cita
                    var colCitas = database.GetCollection<BsonDocument>("cita");
                    var pipeline = new[]
        {
    // 1️⃣ Filtrar solo las citas del médico y con estado = 2
    new BsonDocument("$match", new BsonDocument
    {
        { "medico_id", idCedulaMedico },
        { "estado_id", 2 } // citas pendientes
    }),

    // 2️⃣ Traer información del paciente
    new BsonDocument("$lookup", new BsonDocument
    {
        { "from", "paciente" },
        { "localField", "paciente_id" },
        { "foreignField", "_id" },
        { "as", "paciente" }
    }),
    new BsonDocument("$unwind", new BsonDocument
    {
        { "path", "$paciente" },
        { "preserveNullAndEmptyArrays", true }
    }),

    // 3️⃣ Traer tipo de cita
    new BsonDocument("$lookup", new BsonDocument
    {
        { "from", "tipo_cita" },
        { "localField", "tipo_cita_id" },
        { "foreignField", "_id" },
        { "as", "tipo_cita" }
    }),
    new BsonDocument("$unwind", new BsonDocument
    {
        { "path", "$tipo_cita" },
        { "preserveNullAndEmptyArrays", true }
    }),

    // 4️⃣ Traer estado de cita
    new BsonDocument("$lookup", new BsonDocument
    {
        { "from", "estado_cita" },
        { "localField", "estado_id" },
        { "foreignField", "_id" },
        { "as", "estado_cita" }
    }),
    new BsonDocument("$unwind", new BsonDocument
    {
        { "path", "$estado_cita" },
        { "preserveNullAndEmptyArrays", true }
    }),

    // 5️⃣ Traer historial de la cita (puede no existir)
    new BsonDocument("$lookup", new BsonDocument
    {
        { "from", "historial_cita_medica" },
        { "localField", "_id" },
        { "foreignField", "cita_id" },
        { "as", "historial" }
    }),
    new BsonDocument("$unwind", new BsonDocument
    {
        { "path", "$historial" },
        { "preserveNullAndEmptyArrays", true }
    }),

    // 6️⃣ Traer diagnóstico (puede no existir)
    new BsonDocument("$lookup", new BsonDocument
    {
        { "from", "diagnostico" },
        { "localField", "historial.diagnostico_id" },
        { "foreignField", "_id" },
        { "as", "diagnostico" }
    }),
    new BsonDocument("$unwind", new BsonDocument
    {
        { "path", "$diagnostico" },
        { "preserveNullAndEmptyArrays", true }
    }),

    // 7️⃣ Seleccionar solo los campos necesarios
    new BsonDocument("$project", new BsonDocument
    {
        { "id_cita", "$_id" },
        { "fecha_cita", "$fecha_cita" },
        { "hora_cita", "$hora_cita" },
        { "nombre_paciente", "$paciente.nombres" },
        { "apellido_paciente", "$paciente.apellidos" },
        { "tipo_cita", "$tipo_cita.nombre_tipo" },
        { "estado_cita", "$estado_cita.estado" },
        { "diagnostico", new BsonDocument("$ifNull", new BsonArray { "$diagnostico.descripcion", "Sin diagnóstico" }) }
    }),

    // 8️⃣ Ordenar por fecha y hora
    new BsonDocument("$sort", new BsonDocument
    {
        { "fecha_cita", 1 },
        { "hora_cita", 1 }
    })
};

                    var results = colCitas.Aggregate<BsonDocument>(pipeline).ToList();

                    // 3️⃣ Llenar DataTable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_cita", typeof(int));
                    dt.Columns.Add("Fecha", typeof(string));
                    dt.Columns.Add("Hora", typeof(string));
                    dt.Columns.Add("Nombres", typeof(string));
                    dt.Columns.Add("Apellidos", typeof(string));
                    dt.Columns.Add("Servicio", typeof(string));
                    dt.Columns.Add("Estado_cita", typeof(string));
                    dt.Columns.Add("Diagnostico", typeof(string));

                    foreach (var item in results)
                    {
                        string fechaStr = item["fecha_cita"].BsonType == BsonType.DateTime
                            ? item["fecha_cita"].ToUniversalTime().ToString("yyyy-MM-dd")
                            : item["fecha_cita"].AsString;

                        dt.Rows.Add(
                            item["id_cita"].ToInt32(),
                            fechaStr,
                            item["hora_cita"].AsString,
                            item["nombre_paciente"].AsString,
                            item["apellido_paciente"].AsString,
                            item["tipo_cita"].AsString,
                            item["estado_cita"].AsString,
                            item["diagnostico"]?.AsString ?? "Sin diagnóstico"
                        );
                    }

                    return dt;
                }
                catch (Exception exMongo)
                {
                    MessageBox.Show("Error al conectar con la base de datos, intente mas tarde", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine("Error al conectar con MongoDB: " + exMongo.Message);
                    return new DataTable();
                }

            }
            return new DataTable();

        }


    }
}
