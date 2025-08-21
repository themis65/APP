namespace APP.Vista
{
    partial class MedicoAgregarPaciente
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
            this.lblNombre1 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblNombre2 = new System.Windows.Forms.Label();
            this.lblApellido1 = new System.Windows.Forms.Label();
            this.lblApellido2 = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.lblFnacimiento = new System.Windows.Forms.Label();
            this.lblContacto = new System.Windows.Forms.Label();
            this.lblAntecedentes = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.txtNombre1 = new System.Windows.Forms.TextBox();
            this.txtNombre2 = new System.Windows.Forms.TextBox();
            this.txtApellido1 = new System.Windows.Forms.TextBox();
            this.txtApellido2 = new System.Windows.Forms.TextBox();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.txtCelular = new System.Windows.Forms.TextBox();
            this.txtAntecedentes = new System.Windows.Forms.TextBox();
            this.dtpFnacimiento = new System.Windows.Forms.DateTimePicker();
            this.mySqlDataAdapter1 = new MySql.Data.MySqlClient.MySqlDataAdapter();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(184, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 62);
            this.label1.TabIndex = 19;
            this.label1.Text = "Hospital del Día";
            // 
            // lblNombre1
            // 
            this.lblNombre1.AutoSize = true;
            this.lblNombre1.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre1.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre1.Location = new System.Drawing.Point(124, 154);
            this.lblNombre1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre1.Name = "lblNombre1";
            this.lblNombre1.Size = new System.Drawing.Size(150, 36);
            this.lblNombre1.TabIndex = 21;
            this.lblNombre1.Text = "Primer nombre:";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(234, 92);
            this.lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(230, 36);
            this.lbl1.TabIndex = 22;
            this.lbl1.Text = "Informacion del Paciente";
            // 
            // lblNombre2
            // 
            this.lblNombre2.AutoSize = true;
            this.lblNombre2.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre2.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre2.Location = new System.Drawing.Point(102, 190);
            this.lblNombre2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre2.Name = "lblNombre2";
            this.lblNombre2.Size = new System.Drawing.Size(172, 36);
            this.lblNombre2.TabIndex = 23;
            this.lblNombre2.Text = "Segundo nombre:";
            // 
            // lblApellido1
            // 
            this.lblApellido1.AutoSize = true;
            this.lblApellido1.BackColor = System.Drawing.Color.Transparent;
            this.lblApellido1.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido1.Location = new System.Drawing.Point(122, 226);
            this.lblApellido1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApellido1.Name = "lblApellido1";
            this.lblApellido1.Size = new System.Drawing.Size(152, 36);
            this.lblApellido1.TabIndex = 24;
            this.lblApellido1.Text = "Primer apellido:";
            // 
            // lblApellido2
            // 
            this.lblApellido2.AutoSize = true;
            this.lblApellido2.BackColor = System.Drawing.Color.Transparent;
            this.lblApellido2.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido2.Location = new System.Drawing.Point(102, 262);
            this.lblApellido2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApellido2.Name = "lblApellido2";
            this.lblApellido2.Size = new System.Drawing.Size(174, 36);
            this.lblApellido2.TabIndex = 25;
            this.lblApellido2.Text = "Segundo apellido:";
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.BackColor = System.Drawing.Color.Transparent;
            this.lblCedula.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCedula.Location = new System.Drawing.Point(128, 298);
            this.lblCedula.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(146, 36);
            this.lblCedula.TabIndex = 26;
            this.lblCedula.Text = "Nro. de cedula:";
            // 
            // lblFnacimiento
            // 
            this.lblFnacimiento.AutoSize = true;
            this.lblFnacimiento.BackColor = System.Drawing.Color.Transparent;
            this.lblFnacimiento.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFnacimiento.Location = new System.Drawing.Point(78, 334);
            this.lblFnacimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFnacimiento.Name = "lblFnacimiento";
            this.lblFnacimiento.Size = new System.Drawing.Size(198, 36);
            this.lblFnacimiento.TabIndex = 27;
            this.lblFnacimiento.Text = "Fecha de nacimiento:";
            // 
            // lblContacto
            // 
            this.lblContacto.AutoSize = true;
            this.lblContacto.BackColor = System.Drawing.Color.Transparent;
            this.lblContacto.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.Location = new System.Drawing.Point(195, 370);
            this.lblContacto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.Size = new System.Drawing.Size(79, 36);
            this.lblContacto.TabIndex = 28;
            this.lblContacto.Text = "Celular:";
            // 
            // lblAntecedentes
            // 
            this.lblAntecedentes.AutoSize = true;
            this.lblAntecedentes.BackColor = System.Drawing.Color.Transparent;
            this.lblAntecedentes.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntecedentes.Location = new System.Drawing.Point(137, 406);
            this.lblAntecedentes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAntecedentes.Name = "lblAntecedentes";
            this.lblAntecedentes.Size = new System.Drawing.Size(137, 36);
            this.lblAntecedentes.TabIndex = 29;
            this.lblAntecedentes.Text = "Antecedentes:";
            this.lblAntecedentes.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Chartreuse;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(343, 466);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(149, 42);
            this.btnGuardar.TabIndex = 30;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.Location = new System.Drawing.Point(28, 466);
            this.btnInicio.Margin = new System.Windows.Forms.Padding(4);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(149, 42);
            this.btnInicio.TabIndex = 31;
            this.btnInicio.Text = "Cancelar";
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // txtNombre1
            // 
            this.txtNombre1.Location = new System.Drawing.Point(281, 154);
            this.txtNombre1.Name = "txtNombre1";
            this.txtNombre1.Size = new System.Drawing.Size(246, 22);
            this.txtNombre1.TabIndex = 32;
            // 
            // txtNombre2
            // 
            this.txtNombre2.Location = new System.Drawing.Point(281, 190);
            this.txtNombre2.Name = "txtNombre2";
            this.txtNombre2.Size = new System.Drawing.Size(246, 22);
            this.txtNombre2.TabIndex = 33;
            // 
            // txtApellido1
            // 
            this.txtApellido1.Location = new System.Drawing.Point(281, 226);
            this.txtApellido1.Name = "txtApellido1";
            this.txtApellido1.Size = new System.Drawing.Size(246, 22);
            this.txtApellido1.TabIndex = 34;
            // 
            // txtApellido2
            // 
            this.txtApellido2.Location = new System.Drawing.Point(281, 262);
            this.txtApellido2.Name = "txtApellido2";
            this.txtApellido2.Size = new System.Drawing.Size(246, 22);
            this.txtApellido2.TabIndex = 35;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(281, 298);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(246, 22);
            this.txtCedula.TabIndex = 36;
            // 
            // txtCelular
            // 
            this.txtCelular.Location = new System.Drawing.Point(281, 370);
            this.txtCelular.Name = "txtCelular";
            this.txtCelular.Size = new System.Drawing.Size(246, 22);
            this.txtCelular.TabIndex = 38;
            // 
            // txtAntecedentes
            // 
            this.txtAntecedentes.Location = new System.Drawing.Point(281, 406);
            this.txtAntecedentes.Name = "txtAntecedentes";
            this.txtAntecedentes.Size = new System.Drawing.Size(246, 22);
            this.txtAntecedentes.TabIndex = 39;
            // 
            // dtpFnacimiento
            // 
            this.dtpFnacimiento.Location = new System.Drawing.Point(281, 334);
            this.dtpFnacimiento.Name = "dtpFnacimiento";
            this.dtpFnacimiento.Size = new System.Drawing.Size(246, 22);
            this.dtpFnacimiento.TabIndex = 40;
            // 
            // mySqlDataAdapter1
            // 
            this.mySqlDataAdapter1.DeleteCommand = null;
            this.mySqlDataAdapter1.InsertCommand = null;
            this.mySqlDataAdapter1.SelectCommand = null;
            this.mySqlDataAdapter1.UpdateCommand = null;
            // 
            // MedicoAgregarPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 530);
            this.Controls.Add(this.dtpFnacimiento);
            this.Controls.Add(this.txtAntecedentes);
            this.Controls.Add(this.txtCelular);
            this.Controls.Add(this.txtCedula);
            this.Controls.Add(this.txtApellido2);
            this.Controls.Add(this.txtApellido1);
            this.Controls.Add(this.txtNombre2);
            this.Controls.Add(this.txtNombre1);
            this.Controls.Add(this.btnInicio);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblAntecedentes);
            this.Controls.Add(this.lblContacto);
            this.Controls.Add(this.lblFnacimiento);
            this.Controls.Add(this.lblCedula);
            this.Controls.Add(this.lblApellido2);
            this.Controls.Add(this.lblApellido1);
            this.Controls.Add(this.lblNombre2);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lblNombre1);
            this.Controls.Add(this.label1);
            this.Name = "MedicoAgregarPaciente";
            this.Text = "MedicoAgregarPaciente";
            this.Load += new System.EventHandler(this.MedicoAgregarPaciente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombre1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblNombre2;
        private System.Windows.Forms.Label lblApellido1;
        private System.Windows.Forms.Label lblApellido2;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label lblFnacimiento;
        private System.Windows.Forms.Label lblContacto;
        private System.Windows.Forms.Label lblAntecedentes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.TextBox txtNombre1;
        private System.Windows.Forms.TextBox txtNombre2;
        private System.Windows.Forms.TextBox txtApellido1;
        private System.Windows.Forms.TextBox txtApellido2;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.TextBox txtCelular;
        private System.Windows.Forms.TextBox txtAntecedentes;
        private System.Windows.Forms.DateTimePicker dtpFnacimiento;
        private MySql.Data.MySqlClient.MySqlDataAdapter mySqlDataAdapter1;
    }
}