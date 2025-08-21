namespace APP.Vista
{
    partial class PacienteHistorial
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
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.mySqlDataAdapter1 = new MySql.Data.MySqlClient.MySqlDataAdapter();
            this.lbl = new System.Windows.Forms.Label();
            this.btnInicio = new System.Windows.Forms.Button();
            this.lblHistoriaClinica = new System.Windows.Forms.Label();
            this.lblCedulaPaciente = new System.Windows.Forms.Label();
            this.lblDecripcion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(26, 209);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.Size = new System.Drawing.Size(875, 135);
            this.dgvHistorial.TabIndex = 14;
            this.dgvHistorial.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGHistorial_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Perpetua Titling MT", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(224, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(493, 56);
            this.label1.TabIndex = 15;
            this.label1.Text = "Hospital del Día";
            // 
            // mySqlDataAdapter1
            // 
            this.mySqlDataAdapter1.DeleteCommand = null;
            this.mySqlDataAdapter1.InsertCommand = null;
            this.mySqlDataAdapter1.SelectCommand = null;
            this.mySqlDataAdapter1.UpdateCommand = null;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(380, 80);
            this.lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(178, 36);
            this.lbl.TabIndex = 16;
            this.lbl.Text = "Tu Historial Clínico";
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.Location = new System.Drawing.Point(396, 369);
            this.btnInicio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(149, 42);
            this.btnInicio.TabIndex = 17;
            this.btnInicio.Text = "Inicio";
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // lblHistoriaClinica
            // 
            this.lblHistoriaClinica.AutoSize = true;
            this.lblHistoriaClinica.BackColor = System.Drawing.Color.Transparent;
            this.lblHistoriaClinica.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoriaClinica.Location = new System.Drawing.Point(47, 142);
            this.lblHistoriaClinica.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoriaClinica.Name = "lblHistoriaClinica";
            this.lblHistoriaClinica.Size = new System.Drawing.Size(131, 36);
            this.lblHistoriaClinica.TabIndex = 18;
            this.lblHistoriaClinica.Text = "Nro Historia: ";
            // 
            // lblCedulaPaciente
            // 
            this.lblCedulaPaciente.AutoSize = true;
            this.lblCedulaPaciente.BackColor = System.Drawing.Color.Transparent;
            this.lblCedulaPaciente.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCedulaPaciente.Location = new System.Drawing.Point(302, 142);
            this.lblCedulaPaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCedulaPaciente.Name = "lblCedulaPaciente";
            this.lblCedulaPaciente.Size = new System.Drawing.Size(45, 36);
            this.lblCedulaPaciente.TabIndex = 19;
            this.lblCedulaPaciente.Text = "C.I :";
            // 
            // lblDecripcion
            // 
            this.lblDecripcion.AutoSize = true;
            this.lblDecripcion.BackColor = System.Drawing.Color.Transparent;
            this.lblDecripcion.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecripcion.Location = new System.Drawing.Point(545, 142);
            this.lblDecripcion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDecripcion.Name = "lblDecripcion";
            this.lblDecripcion.Size = new System.Drawing.Size(151, 36);
            this.lblDecripcion.TabIndex = 20;
            this.lblDecripcion.Text = "Observaciones: ";
            // 
            // PacienteHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 450);
            this.Controls.Add(this.lblDecripcion);
            this.Controls.Add(this.lblCedulaPaciente);
            this.Controls.Add(this.lblHistoriaClinica);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvHistorial);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PacienteHistorial";
            this.Text = "PacienteHistorial";
            this.Load += new System.EventHandler(this.PacienteHistorial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Label label1;
        private MySql.Data.MySqlClient.MySqlDataAdapter mySqlDataAdapter1;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Label lblHistoriaClinica;
        private System.Windows.Forms.Label lblCedulaPaciente;
        private System.Windows.Forms.Label lblDecripcion;
    }
}