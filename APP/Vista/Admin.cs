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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            // Vincular el método de pintura para el degradado al evento Paint
            this.Paint += Admin_Paint;
        }

        // Método para dibujar un degradado formal de fondo
        private void Admin_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
              this.ClientRectangle,
              Color.FromArgb(44, 62, 80), // Azul oscuro
                      Color.FromArgb(52, 73, 94), // Azul más claro (Acero)
                      90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}