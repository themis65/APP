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
    public partial class PacienteHistorial : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private ControladorPersona _pacienteController;

        public PacienteHistorial(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;                                                            
            _pacienteController = new ControladorPersona(new Conexion());
            String[] historiaClinica = new String[3];

            historiaClinica = _pacienteController.obtenerHistoriaClinica(idUsuario);
            lblHistoriaClinica.Text = "Nro Historia: " + historiaClinica[0];
            lblCedulaPaciente.Text = "C.I: " + historiaClinica[1];
            lblDecripcion.Text = "Descripción: " + historiaClinica[2];
            CargarDatos();
        }

        private void dGHistorial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Este evento está vacío, no se requiere acción específica aquí.
        }

        private void CargarDatos()
        {
            // Cargar historial del paciente
            DataTable citas = _pacienteController.ObtenerHistorialPaciente(idUsuario);
            dgvHistorial.DataSource = citas;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            // Regresar al formulario principal de Paciente
            Paciente pacienteForm = new Paciente(Nombre, Apellido, idUsuario, Rol);
            pacienteForm.Show();
            this.Hide();
        }

        private void PacienteHistorial_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado al evento Paint
            this.Paint += PacienteHistorial_Paint;
        }

        // Método para dibujar el degradado de fondo
        private void PacienteHistorial_Paint(object sender, PaintEventArgs e)
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
    }
}