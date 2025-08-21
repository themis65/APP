namespace APP.Vista
{
    partial class Auditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.bttCerrarSesion = new System.Windows.Forms.Button();
            this.btnrCitas = new System.Windows.Forms.Button();
            this.btnCActividad = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Perpetua Titling MT", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(247, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auditor";
            // 
            // bttCerrarSesion
            // 
            this.bttCerrarSesion.BackColor = System.Drawing.Color.Red;
            this.bttCerrarSesion.Font = new System.Drawing.Font("Kanit Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttCerrarSesion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bttCerrarSesion.Location = new System.Drawing.Point(265, 194);
            this.bttCerrarSesion.Margin = new System.Windows.Forms.Padding(2);
            this.bttCerrarSesion.Name = "bttCerrarSesion";
            this.bttCerrarSesion.Size = new System.Drawing.Size(159, 56);
            this.bttCerrarSesion.TabIndex = 18;
            this.bttCerrarSesion.Text = "Cerrar sesiòn";
            this.bttCerrarSesion.UseVisualStyleBackColor = false;
            this.bttCerrarSesion.Click += new System.EventHandler(this.bttCerrarSesion_Click_1);
            // 
            // btnrCitas
            // 
            this.btnrCitas.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrCitas.Location = new System.Drawing.Point(26, 107);
            this.btnrCitas.Name = "btnrCitas";
            this.btnrCitas.Size = new System.Drawing.Size(199, 47);
            this.btnrCitas.TabIndex = 19;
            this.btnrCitas.Text = "Ver reportes citas";
            this.btnrCitas.UseVisualStyleBackColor = true;
            // 
            // btnCActividad
            // 
            this.btnCActividad.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCActividad.Location = new System.Drawing.Point(249, 107);
            this.btnCActividad.Name = "btnCActividad";
            this.btnCActividad.Size = new System.Drawing.Size(195, 47);
            this.btnCActividad.TabIndex = 20;
            this.btnCActividad.Text = "Consultar Actividad";
            this.btnCActividad.UseVisualStyleBackColor = true;
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.Location = new System.Drawing.Point(461, 107);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(202, 47);
            this.btnUsuarios.TabIndex = 21;
            this.btnUsuarios.Text = "Listados de Usuario";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            // 
            // Auditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 303);
            this.Controls.Add(this.btnUsuarios);
            this.Controls.Add(this.btnCActividad);
            this.Controls.Add(this.btnrCitas);
            this.Controls.Add(this.bttCerrarSesion);
            this.Controls.Add(this.label1);
            this.Name = "Auditor";
            this.Text = "Auditor";
            this.Load += new System.EventHandler(this.Auditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttCerrarSesion;
        private System.Windows.Forms.Button btnrCitas;
        private System.Windows.Forms.Button btnCActividad;
        private System.Windows.Forms.Button btnUsuarios;
    }
}