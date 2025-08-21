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
    public partial class PacienteCitas : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private ControladorPersona _pacienteController;

        public PacienteCitas(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;
            _pacienteController = new ControladorPersona(new Conexion());
            CargarDatos();
        }

        private void PacienteCitas_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado al evento Paint
            this.Paint += PacienteCitas_Paint;
        }

        // Método para dibujar el degradado de fondo
        private void PacienteCitas_Paint(object sender, PaintEventArgs e)
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

        private void CargarDatos()
        {
            // Cargar citas del paciente
            DataTable citas = _pacienteController.ObtenerCitasPaciente(idUsuario);
            dgvCitas.DataSource = citas;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Paciente pacienteForm = new Paciente(Nombre, Apellido, idUsuario, Rol);
            pacienteForm.Show();
            this.Hide();
        }

        private void dgvCitas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Para evitar cabecera
            {
                DataGridViewRow fila = dgvCitas.Rows[e.RowIndex];

                int idCita = Convert.ToInt32(fila.Cells["id_cita"].Value);

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (dgvCitas.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro que desea continuar?",     // Mensaje
                    "Confirmación",                          // Título de la ventana
                    MessageBoxButtons.YesNo,                 // Botones: Sí y No
                    MessageBoxIcon.Question                  // Ícono: signo de interrogación
                );
                if (resultado != DialogResult.Yes)
                {
                    return;
                }
                DataGridViewRow filaSeleccionada = dgvCitas.SelectedRows[0];
                int idCita = Convert.ToInt32(filaSeleccionada.Cells["id_cita"].Value);
                if (_pacienteController.CancelarCita(idCita, idUsuario))
                {
                    MessageBox.Show("Cita cancelada exitosamente.");
                }

                CargarDatos();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una cita para cancelar.");
            }
        }
    }
}