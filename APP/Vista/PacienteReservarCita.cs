using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APP.Controlador;
using APP.Modelo;
using System.Drawing.Drawing2D; // Necesario para LinearGradientBrush

namespace APP.Vista
{
    public partial class PacienteReservarCita : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private ControladorPersona _pacienteController;

        public PacienteReservarCita(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;
            _pacienteController = new ControladorPersona(new Conexion());
        }

        private void PacienteReservarCita_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado
            this.Paint += PacienteReservarCita_Paint;

            cbEspecialidad.DataSource = _pacienteController.ObtenerTiposDeCita();
            cbEspecialidad.DisplayMember = "descripcion_completa";
            cbEspecialidad.ValueMember = "id_tipo_cita";

            // Opcional: limpiar médicos y horarios
            cbMedico.DataSource = null;
            cbHorario.DataSource = null;
        }

        // Método para dibujar el degradado de fondo
        private void PacienteReservarCita_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(180, 220, 255), // Color superior
                Color.White,                  // Color inferior
                90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Este evento está vacío, no se requiere acción específica aquí.
        }

        private void cbEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEspecialidad.SelectedValue == null) return;

            // Asegúrate de que "descripcion_completa" sea el formato esperado: "Especialidad - Precio"
            // Y que la especialidad sea la parte antes del primer '-'
            string especialidad = cbEspecialidad.Text.Split('-')[0].Trim();
            cbMedico.DataSource = _pacienteController.ObtenerMedicosPorEspecialidad(especialidad);
            cbMedico.DisplayMember = "nombre_completo";
            cbMedico.ValueMember = "id_cedula";

            cbHorario.DataSource = null;
        }

        private void cbMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarHorarios();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            CargarHorarios();
        }

        private void CargarHorarios()
        {
            cbHorario.DataSource = null;
            cbHorario.Items.Clear(); // Limpia los ítems existentes
            if (cbMedico.SelectedValue == null) return;

            string medicoId = cbMedico.SelectedValue.ToString();
            DataTable dtHorarios = _pacienteController.ObtenerHorariosDisponibles(medicoId, dtpFecha.Value);

            if (dtHorarios.Rows.Count == 0)
            {
                cbHorario.Enabled = false; // si no hay horarios, deshabilitar
                return;
            }
            cbHorario.Enabled = true; // habilitar si hay horarios disponibles
            cbHorario.DataSource = dtHorarios;
            cbHorario.DisplayMember = "hora_inicio"; // muestra solo la hora
            cbHorario.ValueMember = "id_horario";    // id interno
            cbHorario.SelectedIndex = -1; // Deselecciona cualquier elemento por defecto
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cbEspecialidad.SelectedValue == null || cbMedico.SelectedValue == null || cbHorario.SelectedValue == null)
            {
                MessageBox.Show("Seleccione tipo de cita, médico y horario", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tomar la hora de inicio del horario seleccionado
            // Asegúrate de que SelectedItem sea de tipo DataRowView si tu DataSource es un DataTable
            if (cbHorario.SelectedItem is DataRowView drv)
            {
                TimeSpan horaInicio = (TimeSpan)drv["hora_inicio"];
                DateTime fechaHoraCita = dtpFecha.Value.Date + horaInicio;

                bool exito = _pacienteController.AgendarCita(
                    cbMedico.SelectedValue.ToString(),
                    Convert.ToInt32(cbEspecialidad.SelectedValue),
                    Convert.ToInt32(cbHorario.SelectedValue),
                    fechaHoraCita,
                    idUsuario
                );

                MessageBox.Show(exito ? "Cita agendada correctamente" : "Horario no disponible o error al agendar la cita. Por favor, intente de nuevo o seleccione otro horario.", "Agendar Cita", MessageBoxButtons.OK, exito ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                // Opcional: Después de agendar, podrías limpiar los campos
                // CargarHorarios(); // Esto refrescaría los horarios para ese médico/fecha
                // Puedes también deseleccionar los comboboxes si es necesario.
            }
            else
            {
                MessageBox.Show("Error al obtener la hora del horario seleccionado. Por favor, reintente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Este evento está vacío, no se requiere acción específica aquí por ahora.
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            // Regresar al formulario principal de Paciente
            Paciente pacienteForm = new Paciente(Nombre, Apellido, idUsuario, Rol);
            pacienteForm.Show();
            this.Hide(); // Oculta el formulario actual
        }
    }
}