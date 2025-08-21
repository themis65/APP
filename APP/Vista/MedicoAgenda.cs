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
    public partial class MedicoAgenda : Form
    {
        private string Nombre;
        private string Apellido;
        private int idUsuario;
        private string Rol;
        private ControladorMedico _medicoController;

        public MedicoAgenda(string nombre, string apellido, int id_usuario, string rol)
        {
            InitializeComponent();
            Nombre = nombre;
            Apellido = apellido;
            idUsuario = id_usuario;
            Rol = rol;
            _medicoController = new ControladorMedico(new Conexion());
            CargarDatos();
        }

        private void MedicoAgenda_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado al evento Paint
            this.Paint += MedicoAgenda_Paint;
        }

        // Método para dibujar el degradado de fondo
        private void MedicoAgenda_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
              this.ClientRectangle,
              Color.FromArgb(180, 220, 255), // Color superior
                      Color.White,                   // Color inferior
                      90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Medico medicoForm = new Medico(Nombre, Apellido, idUsuario, Rol);
            medicoForm.Show();
            this.Hide();
        }

        private void CargarDatos()
        {
            // Cargar citas del paciente
            DataTable agenda = _medicoController.ObtenerAgendaMedico(idUsuario);
            dgvAgenda.DataSource = agenda;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila
            if (dgvAgenda.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cita para continuar.");
                return;
            }
            // Obtener la fila seleccionada
            DataGridViewRow filaSeleccionada = dgvAgenda.SelectedRows[0];
            // Obtener el ID de la cita
            int idCita = Convert.ToInt32(filaSeleccionada.Cells["id_cita"].Value);
            MedicoDiagnostico diagnosticoForm = new MedicoDiagnostico(idCita, Nombre, Apellido, idUsuario, Rol);
            diagnosticoForm.Show();
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}