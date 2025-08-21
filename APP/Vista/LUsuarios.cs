using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D; // Necesario para LinearGradientBrush

namespace APP.Vista
{
    public partial class LUsuarios : Form
    {
        public LUsuarios()
        {
            InitializeComponent();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            // Este evento está vacío, no se requiere acción específica aquí.
        }

        private void LUsuarios_Load(object sender, EventArgs e)
        {
            // Vincula el método de pintura para el degradado al evento Paint
            this.Paint += LUsuarios_Paint;
        }

        // Método para dibujar el degradado de fondo
        private void LUsuarios_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(230, 240, 245), // Un gris muy claro con un toque de azul
                Color.FromArgb(170, 200, 215), // Un gris azulado más oscuro
                90F)) // 90F para un degradado de arriba hacia abajo
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}