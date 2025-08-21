using APP.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP.Controlador
{
    internal class ControladorPersona
    {
        private Conexion _conexion;

        public ControladorPersona(Conexion conexion)
        {
            _conexion = conexion;
        }

        public bool CancelarCita(int idCita, int idUsuario)
        {
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    string query = @"UPDATE cita 
                             SET id_estado = 3, 
                                 id_modificado = @usuario,
                                 fecha_modificado = NOW()
                             WHERE id_cita = @idCita AND id_estado = 1"; // solo si está pendiente

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCita", idCita);
                        cmd.Parameters.AddWithValue("@usuario", idUsuario);

                        int filas = cmd.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            // Actualizar también en Mongo
                            ActualizarEstadoCitaEnMongo(idCita, 3, idUsuario);
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MySQL no disponible, usando solo MongoDB: " + ex.Message);
            }

            // ================= MongoDB =================
            return ActualizarEstadoCitaEnMongo(idCita, 3, idUsuario);
        }

        private bool ActualizarEstadoCitaEnMongo(int idCita, int nuevoEstado, int idUsuario)
        {
            try
            {
                var database = _conexion.ObtenerConexionMongo();
                var colCitas = database.GetCollection<BsonDocument>("cita");

                var filter = Builders<BsonDocument>.Filter.Eq("_id", idCita) &
                             Builders<BsonDocument>.Filter.Eq("estado_id", 1); // solo si estaba pendiente

                var update = Builders<BsonDocument>.Update
                    .Set("estado_id", nuevoEstado)
                    .Set("modificado_por", idUsuario)
                    .Set("fecha_modificado", DateTime.Now);

                var result = colCitas.UpdateOne(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar cita en MongoDB: " + ex.Message);
                return false;
            }
        }


        public DataTable ObtenerCitasPaciente(int idUsuario)
        {
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    c.id_cita,
                    c.fecha_cita,
                    c.hora_cita,
                    CONCAT(m.nombres, ' ', m.apellidos) AS Medico,
                    tc.nombre_tipo AS tipo_cita,
                    ec.estado AS estado_cita
                FROM cita c
                INNER JOIN paciente p ON c.id_paciente = p.id_cedula
                INNER JOIN medico m ON c.id_medico = m.id_cedula
                INNER JOIN tipo_cita tc ON c.id_tipo_cita = tc.id_tipo_cita
                INNER JOIN estado_cita ec ON c.id_estado = ec.id_estado
                WHERE p.id_usuario = @id_usuario AND c.id_estado = 1;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
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
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);

                // ================= MongoDB =================
                try
                {
                    // ================= MongoDB Optimizado =================
                    var database = _conexion.ObtenerConexionMongo();
                    var colPacientes = database.GetCollection<BsonDocument>("paciente");

                    var pipeline = new[]
                    {
                new BsonDocument("$match", new BsonDocument("usuario_id", idUsuario)),
                new BsonDocument("$lookup", new BsonDocument
                    {
            { "from", "cita" },
            { "let", new BsonDocument("pacienteId", "$_id") },
            { "pipeline", new BsonArray
                {
                    new BsonDocument("$match", new BsonDocument
                        {
                            { "$expr", new BsonDocument("$and", new BsonArray
                                {
                                    new BsonDocument("$eq", new BsonArray { "$paciente_id", "$$pacienteId" }),
                                    new BsonDocument("$eq", new BsonArray { "$estado_id", 1 }) // solo pendientes
                                })
                            }
                        })
                }
            },
            { "as", "citas" }
        }),
                new BsonDocument("$unwind", "$citas"),
                new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "medico" },
                        { "localField", "citas.medico_id" },
                        { "foreignField", "_id" },
                        { "as", "medico" }
                    }),
                new BsonDocument("$unwind", "$medico"),
                new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "tipo_cita" },
                        { "localField", "citas.tipo_cita_id" },
                        { "foreignField", "_id" },
                        { "as", "tipo_cita" }
                    }),
                new BsonDocument("$unwind", "$tipo_cita"),
                new BsonDocument("$lookup", new BsonDocument
                    {
                        { "from", "estado_cita" },
                        { "localField", "citas.estado_id" },
                        { "foreignField", "_id" },
                        { "as", "estado_cita" }
                    }),
                new BsonDocument("$unwind", "$estado_cita"),
                new BsonDocument("$project", new BsonDocument
                    {
                        { "id_cita", "$citas._id" },
                        { "fecha_cita", "$citas.fecha_cita" },
                        { "hora_cita", "$citas.hora_cita" },
                        { "nombre_medico", "$medico.nombres" },
                        { "apellido_medico", "$medico.apellidos" },
                        { "tipo_cita", "$tipo_cita.nombre_tipo" },
                        { "estado_cita", "$estado_cita.estado" }
                    })
            };

                    var results = colPacientes.Aggregate<BsonDocument>(pipeline).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_cita", typeof(int));
                    dt.Columns.Add("fecha_cita", typeof(string));
                    dt.Columns.Add("hora_cita", typeof(string));
                    dt.Columns.Add("nombre_medico", typeof(string));
                    dt.Columns.Add("apellido_medico", typeof(string));
                    dt.Columns.Add("tipo_cita", typeof(string));
                    dt.Columns.Add("estado_cita", typeof(string));

                    foreach (var item in results)
                    {
                        string fechaStr = item["fecha_cita"].BsonType == BsonType.DateTime
                            ? item["fecha_cita"].ToUniversalTime().ToString("yyyy-MM-dd")
                            : item["fecha_cita"].AsString;

                        dt.Rows.Add(
                            item["id_cita"].ToInt32(),
                            fechaStr,
                            item["hora_cita"].AsString,
                            item["nombre_medico"].AsString,
                            item["apellido_medico"].AsString,
                            item["tipo_cita"].AsString,
                            item["estado_cita"].AsString
                        );
                    }

                    return dt;
                }
                catch (Exception exMongo)
                {
                    Console.WriteLine("Error al conectar con MongoDB: " + exMongo.Message);
                    return new DataTable();
                }
            }
        }

        public String[] obtenerHistoriaClinica(int idUsuario)
        {
            string idPaciente = ObtenerIdCedulaPaciente(idUsuario);
            String[] historiaClinica = new String[3];
            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    string query = "SELECT id_historial, id_paciente, descripcion_general FROM historial_medico WHERE id_paciente = @idPaciente";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                historiaClinica[0] = reader["id_historial"].ToString();
                                historiaClinica[1] = reader["id_paciente"].ToString();
                                historiaClinica[2] = reader["descripcion_general"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener historia clínica desde MySQL: " + ex.Message);
            }
            historiaClinica = obtenerHistoriaClinicaMongo(idUsuario);
            return historiaClinica;

        }

        public String[] obtenerHistoriaClinicaMongo(int idUsuario)
        {
            string idPaciente = ObtenerIdCedulaPaciente(idUsuario); // Mismo método para obtener la cédula
            String[] historiaClinica = new String[3];

            try
            {
                var database = _conexion.ObtenerConexionMongo();
                var colHistorial = database.GetCollection<BsonDocument>("historial_medico");

                // Filtrar por paciente_id
                var filter = Builders<BsonDocument>.Filter.Eq("paciente_id", idPaciente);

                // Tomar el primer historial que coincida
                var historial = colHistorial.Find(filter).FirstOrDefault();

                if (historial != null)
                {
                    historiaClinica[0] = historial["_id"].ToString(); // id_historial en Mongo suele ser _id
                    historiaClinica[1] = historial["paciente_id"].AsString;
                    historiaClinica[2] = historial.Contains("descripcion_general") ? historial["descripcion_general"].AsString : "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener historia clínica desde MongoDB: " + ex.Message);
            }

            return historiaClinica;
        }

        public DataTable ObtenerHistorialPaciente(int idUsuario)
        {
            Boolean errormysql = false;
            try
            {
                // ======== MySQL ========
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                 
                    c.fecha_cita AS Fecha,
                    c.hora_cita AS Hora,
                    CONCAT(m.nombres, ' ', m.apellidos) AS Medico,
                    tc.nombre_tipo AS Servicio,
                    ec.estado AS Estado,
                    d.descripcion AS Diagnostico
                FROM historial_cita_medica h
                INNER JOIN cita c ON h.id_cita = c.id_cita
                INNER JOIN paciente p ON c.id_paciente = p.id_cedula
                INNER JOIN medico m ON c.id_medico = m.id_cedula
                INNER JOIN estado_cita ec ON c.id_estado = ec.id_estado
                INNER JOIN tipo_cita tc ON tc.id_tipo_cita = c.id_tipo_cita
                LEFT JOIN diagnostico d ON h.id_diagnostico = d.id_diagnostico
                WHERE p.id_usuario = @id_usuario AND c.id_estado = 2;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
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
                errormysql = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);
            }

            if (errormysql)
            {
                // ======== MongoDB ========
                try
                {
                    // ======== MongoDB optimizado con $lookup ========
                    var database = _conexion.ObtenerConexionMongo();
                    var colPacientes = database.GetCollection<BsonDocument>("paciente");

                    var pipeline = new[]
                    {
                new BsonDocument("$match", new BsonDocument("usuario_id", idUsuario)),
                new BsonDocument("$lookup", new BsonDocument
{
    { "from", "cita" },
    { "let", new BsonDocument("pacienteId", "$_id") },
    { "pipeline", new BsonArray
        {
            new BsonDocument("$match", new BsonDocument
            {
                { "$expr", new BsonDocument("$and", new BsonArray
                    {
                        new BsonDocument("$eq", new BsonArray { "$paciente_id", "$$pacienteId" }),
                        new BsonDocument("$eq", new BsonArray { "$estado_id", 1 })
                    })
                }
            })
        }
    },
    { "as", "citas" }
}),

                new BsonDocument("$unwind", "$citas"),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "historial_cita_medica" },
                    { "localField", "citas._id" },
                    { "foreignField", "cita_id" },
                    { "as", "historial" }
                }),
                new BsonDocument("$unwind", "$historial"),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "medico" },
                    { "localField", "citas.medico_id" },
                    { "foreignField", "_id" },
                    { "as", "medico" }
                }),
                new BsonDocument("$unwind", "$medico"),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "tipo_cita" },
                    { "localField", "citas.tipo_cita_id" },
                    { "foreignField", "_id" },
                    { "as", "tipo_cita" }
                }),
                new BsonDocument("$unwind", "$tipo_cita"),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "estado_cita" },
                    { "localField", "citas.estado_id" },
                    { "foreignField", "_id" },
                    { "as", "estado_cita" }
                }),
                new BsonDocument("$unwind", "$estado_cita"),
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
                new BsonDocument("$project", new BsonDocument
                {
                    { "id_historial_cita", "$historial._id" },
                    { "fecha_cita", "$citas.fecha_cita" },
                    { "hora_cita", "$citas.hora_cita" },
                    { "nombre_medico", "$medico.nombres" },
                    { "apellido_medico", "$medico.apellidos" },
                    { "tipo_cita", "$tipo_cita.nombre_tipo" },
                    { "estado_cita", "$estado_cita.estado" },
                    { "diagnostico", "$diagnostico.descripcion" }
                })
            };

                    var results = colPacientes.Aggregate<BsonDocument>(pipeline).ToList();

                    // Preparar DataTable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_historial_cita", typeof(int));
                    dt.Columns.Add("fecha_cita", typeof(string));
                    dt.Columns.Add("hora_cita", typeof(string));
                    dt.Columns.Add("nombre_medico", typeof(string));
                    dt.Columns.Add("apellido_medico", typeof(string));
                    dt.Columns.Add("tipo_cita", typeof(string));
                    dt.Columns.Add("estado_cita", typeof(string));
                    dt.Columns.Add("diagnostico", typeof(string));

                    foreach (var item in results)
                    {
                        string fechaStr = item["fecha_cita"].BsonType == BsonType.DateTime
                            ? item["fecha_cita"].ToUniversalTime().ToString("yyyy-MM-dd")
                            : item["fecha_cita"].AsString;

                        dt.Rows.Add(
                            item["id_historial_cita"].ToInt32(),
                            fechaStr,
                            item["hora_cita"].AsString,
                            item["nombre_medico"].AsString,
                            item["apellido_medico"].AsString,
                            item["tipo_cita"].AsString,
                            item["estado_cita"].AsString,
                            item.Contains("diagnostico") && !item["diagnostico"].IsBsonNull ? item["diagnostico"].AsString : ""
                        );
                    }

                    return dt;
                }
                catch (Exception exMongo)
                {
                    Console.WriteLine("Error al conectar con MongoDB: " + exMongo.Message);

                }
            }
            return new DataTable();

        }

        public DataTable ObtenerTiposDeCita()
        {
            Boolean errormysql = false;
            try
            {
                // ======== MySQL ========
                string query = "SELECT id_tipo_cita, CONCAT(nombre_tipo,' - ',descripcion) AS descripcion_completa FROM tipo_cita";
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                errormysql = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);
            }
            if (errormysql)
            {
                try
                {
                    var database = _conexion.ObtenerConexionMongo();
                    var colTipoCita = database.GetCollection<BsonDocument>("tipo_cita");

                    var listaTipos = colTipoCita.Find(new BsonDocument()).ToList();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_tipo_cita", typeof(int));
                    dt.Columns.Add("descripcion_completa", typeof(string));

                    foreach (var tipo in listaTipos)
                    {
                        int id = tipo["_id"].AsInt32;
                        string nombre = tipo["nombre_tipo"].AsString;
                        string descripcion = tipo.Contains("descripcion") ? tipo["descripcion"].AsString : "";

                        dt.Rows.Add(id, $"{nombre} - {descripcion}");
                    }

                    return dt;
                }
                catch (Exception exMongo)
                {
                    Console.WriteLine("Error al obtener tipos de cita en MongoDB: " + exMongo.Message);

                }

            }
            return new DataTable();
        }

        public DataTable ObtenerMedicosPorEspecialidad(string especialidad)
        {
            bool errormysql = false;
            try
            {
                string query = "SELECT id_cedula, CONCAT(nombres,' ',apellidos) AS nombre_completo FROM medico WHERE especialidad=@especialidad";
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@especialidad", especialidad);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                errormysql = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);

            }
            if (errormysql)
            {
                try
                {
                    var database = _conexion.ObtenerConexionMongo();
                    var colMedicos = database.GetCollection<BsonDocument>("medico");
                    var filter = Builders<BsonDocument>.Filter.Eq("especialidad", especialidad);
                    var medicos = colMedicos.Find(filter).ToList();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("id_cedula", typeof(string));
                    dt.Columns.Add("nombre_completo", typeof(string));
                    foreach (var medico in medicos)
                    {
                        string idCedula = medico["_id"].AsString;
                        string nombreCompleto = $"{medico["nombres"].AsString} {medico["apellidos"].AsString}";
                        dt.Rows.Add(idCedula, nombreCompleto);
                    }
                    return dt;
                }
                catch (Exception exMongo)
                {
                    Console.WriteLine("Error al obtener médicos por especialidad en MongoDB: " + exMongo.Message);
                }

            }
            return new DataTable();
        }

        public DataTable ObtenerHorariosDisponibles(string medicoId, DateTime fecha)
        {
            bool errormysql = false;
            string diaSemana = fecha.ToString("dddd"); // devuelve "Monday", "Tuesday", etc.

            // Opcional: traducir a español si tu base tiene "Lunes", "Martes", etc.
            switch (diaSemana)
            {
                case "Monday":
                    diaSemana = "Lunes";
                    break;
                case "Tuesday":
                    diaSemana = "Martes";
                    break;
                case "Wednesday":
                    diaSemana = "Miércoles";
                    break;
                case "Thursday":
                    diaSemana = "Jueves";
                    break;
                case "Friday":
                    diaSemana = "Viernes";
                    break;
                case "Saturday":
                    diaSemana = "Sábado";
                    break;
                case "Sunday":
                    diaSemana = "Domingo";
                    break;
                default:
                    break;
            }
            try
            {

                string query = @"
                SELECT h.id_horario, h.hora_inicio
                FROM horario h
                WHERE h.id_medico = @medico
                    AND h.dia_semana = @diaSemana
                    AND h.disponible = 1
                    AND h.id_horario NOT IN (
                        SELECT c.id_horario
                        FROM cita c
                        WHERE c.id_medico = @medico
                            AND c.fecha_cita = @fecha
                            AND c.id_estado = 1
                    )
                    ORDER BY h.hora_inicio ASC
            ";

                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@medico", medicoId);
                    cmd.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@diaSemana", diaSemana);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                errormysql = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);
            }

            try
            {
                var database = _conexion.ObtenerConexionMongo();
                var colHorarios = database.GetCollection<BsonDocument>("horario");
                var colCitas = database.GetCollection<BsonDocument>("cita");
                var filterCitas = Builders<BsonDocument>.Filter.Eq("medico_id", medicoId) &
              Builders<BsonDocument>.Filter.Eq("fecha_cita", fecha.ToString("yyyy-MM-dd")) &
              Builders<BsonDocument>.Filter.Eq("estado_id", 1);


                var horariosOcupados = colCitas.Find(filterCitas)
                                               .Project(c => c["horario_id"])
                                               .ToList()
                                               .Select(h => h.AsInt32)
                                               .ToHashSet();

                // Filtrar horarios disponibles
                var filterHorarios = Builders<BsonDocument>.Filter.Eq("medico_id", medicoId) &
                                     Builders<BsonDocument>.Filter.Eq("dia_semana", diaSemana) &
                                     Builders<BsonDocument>.Filter.Eq("disponible", true) &
                                     Builders<BsonDocument>.Filter.Nin("_id", horariosOcupados);

                var horariosDisponibles = colHorarios.Find(filterHorarios)
                                                     .Sort(Builders<BsonDocument>.Sort.Ascending("hora_inicio"))
                                                     .ToList();
                DataTable dt = new DataTable();
                dt.Columns.Add("id_horario", typeof(int));
                dt.Columns.Add("hora_inicio", typeof(string));
                foreach (var horario in horariosDisponibles)
                {
                    dt.Rows.Add(horario["_id"].AsInt32, horario["hora_inicio"].AsString);
                }
                return dt;
            }
            catch (Exception exMongo)
            {
                Console.WriteLine("Error al obtener horarios en MongoDB: " + exMongo.Message);
            }

            return new DataTable();
        }



        public bool AgendarCita(string medicoId, int tipoCitaId, int horarioId, DateTime fechaCita, int usuarioLogueado)
        {
            string idCedulaPaciente = ObtenerIdCedulaPaciente(usuarioLogueado);
            if (idCedulaPaciente == null)
            {
                MessageBox.Show("No se encontró el paciente.");
                return false;
            }

            try
            {
                // ================= MySQL =================
                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();

                    // Validar disponibilidad
                    string validar = "SELECT COUNT(*) FROM cita WHERE id_horario=@horario AND fecha_cita=@fecha AND id_tipo_cita=1";
                    MySqlCommand cmdVal = new MySqlCommand(validar, conn);
                    cmdVal.Parameters.AddWithValue("@horario", horarioId);
                    cmdVal.Parameters.AddWithValue("@fecha", fechaCita.ToString("yyyy-MM-dd"));

                    int count = Convert.ToInt32(cmdVal.ExecuteScalar());
                    if (count > 0) return false;

                    // Insertar cita en MySQL
                    string queryInsert = @"INSERT INTO cita 
                (id_paciente, id_medico, id_tipo_cita, id_estado, id_horario, fecha_cita, hora_cita, fecha_registro, id_modificado, fecha_modificado) 
                VALUES (@paciente, @medico, @tipoCita, 1, @horario, @fechaCita, @horaCita, @fechaRegistro, @usuarioLogueado, @fechaRegistro)";

                    MySqlCommand cmd = new MySqlCommand(queryInsert, conn);
                    cmd.Parameters.AddWithValue("@paciente", idCedulaPaciente);
                    cmd.Parameters.AddWithValue("@medico", medicoId);
                    cmd.Parameters.AddWithValue("@tipoCita", tipoCitaId);
                    cmd.Parameters.AddWithValue("@horario", horarioId);
                    cmd.Parameters.AddWithValue("@fechaCita", fechaCita.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@horaCita", fechaCita.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@fechaRegistro", DateTime.Now);
                    cmd.Parameters.AddWithValue("@usuarioLogueado", usuarioLogueado);

                    bool insertadoMySQL = cmd.ExecuteNonQuery() > 0;

                    // También guardamos en Mongo si se insertó en MySQL
                    if (insertadoMySQL)
                    {
                        long idCita = cmd.LastInsertedId;
                        InsertarCitaEnMongo((int)idCita, idCedulaPaciente, medicoId, tipoCitaId, horarioId, fechaCita, usuarioLogueado);
                    }

                    return insertadoMySQL;
                }
            }
            catch (Exception ex)
            {
                int idCita = -1;
                Console.WriteLine("MySQL no disponible, usando solo MongoDB: " + ex.Message);
                return (InsertarCitaEnMongo(idCita, idCedulaPaciente, medicoId, tipoCitaId, horarioId, fechaCita, usuarioLogueado));
            }


        }

        private bool InsertarCitaEnMongo(int idCita, string idCedulaPaciente, string medicoId, int tipoCitaId, int horarioId, DateTime fechaCita, int idUsuario)
        {
            try
            {
                var database = _conexion.ObtenerConexionMongo();
                var colCitas = database.GetCollection<BsonDocument>("cita");

                if (idCita == -1)
                {
                    var sort = Builders<BsonDocument>.Sort.Descending("_id");
                    var ultimaCita = colCitas.Find(new BsonDocument())
                                             .Sort(sort)
                                             .Limit(1)
                                             .FirstOrDefault();


                    if (ultimaCita != null && ultimaCita.Contains("_id"))
                    {
                        idCita = ultimaCita["_id"].AsInt32 + 1;
                    }
                }

                // Validar disponibilidad en Mongo
                var filter = Builders<BsonDocument>.Filter.Eq("horario_id", horarioId) &
                             Builders<BsonDocument>.Filter.Eq("fecha_cita", fechaCita.ToString("yyyy-MM-dd"));
                if (colCitas.Find(filter).Any())
                    return false;

                // Insertar cita
                var nuevaCita = new BsonDocument
        {
            { "_id", idCita },
            { "paciente_id", idCedulaPaciente },
            { "medico_id", medicoId },
            { "tipo_cita_id", tipoCitaId },
            { "estado_id", 1 }, // pendiente
            { "horario_id", horarioId },
            { "fecha_cita", fechaCita.ToString("yyyy-MM-dd") },
            { "hora_cita", fechaCita.ToString("HH:mm:ss") },
            { "fecha_registro", DateTime.Now },
            { "usuario_modifico", idUsuario },
            { "fecha_modificado", DateTime.Now }
        };

                colCitas.InsertOne(nuevaCita);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar en MongoDB: " + ex.Message);
                return false;
            }
        }



        public string ObtenerIdCedulaPaciente(int idUsuario)
        {
            bool errormysql = false;
            string idCedula = null;
            try
            {
                string query = "SELECT id_cedula FROM paciente WHERE id_usuario = @id_usuario";

                using (MySqlConnection conn = _conexion.ObtenerConexionXampp())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idCedula = result.ToString();
                        return idCedula;
                    }
                }
            }
            catch (Exception ex)
            {
                errormysql = true;
                Console.WriteLine("MySQL no disponible, usando MongoDB: " + ex.Message);
            }
            if (errormysql)
            {
                try
                {
                    var database = _conexion.ObtenerConexionMongo();
                    var colPacientes = database.GetCollection<BsonDocument>("paciente");
                    var filter = Builders<BsonDocument>.Filter.Eq("usuario_id", idUsuario);
                    var paciente = colPacientes.Find(filter).FirstOrDefault();
                    if (paciente != null && paciente.Contains("_id"))
                    {
                        idCedula = paciente["_id"].AsString;
                        return idCedula;
                    }
                }
                catch (Exception exMongo)
                {
                    Console.WriteLine("Error al obtener paciente en MongoDB: " + exMongo.Message);
                }
            }
            return idCedula;
        }

    }

}
