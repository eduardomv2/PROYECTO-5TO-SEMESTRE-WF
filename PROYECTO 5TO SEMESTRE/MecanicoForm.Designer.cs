namespace PROYECTO_5TO_SEMESTRE
{
    partial class MecanicoForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewMantenimiento = new System.Windows.Forms.DataGridView();
            this.comboBoxVehiculos = new System.Windows.Forms.ComboBox();
            this.vehiculo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMantenimientoTipo = new System.Windows.Forms.ComboBox();
            this.dateTimePickerInicio = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerProximoMantenimiento = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEstatus = new System.Windows.Forms.ComboBox();
            this.labelIdMantenimiento = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxEstatusVendido = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerProximoMantenimientoVendido = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMantenimientoTipoVendido = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxVehiculosVendido = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMantenimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1236, 586);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelIdMantenimiento);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBoxEstatus);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.dateTimePickerProximoMantenimiento);
            this.tabPage1.Controls.Add(this.dateTimePickerInicio);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.comboBoxMantenimientoTipo);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.vehiculo);
            this.tabPage1.Controls.Add(this.comboBoxVehiculos);
            this.tabPage1.Controls.Add(this.dataGridViewMantenimiento);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1228, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.comboBoxEstatusVendido);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.dateTimePicker1);
            this.tabPage2.Controls.Add(this.dateTimePickerProximoMantenimientoVendido);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.comboBoxMantenimientoTipoVendido);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.comboBoxVehiculosVendido);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1228, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMantenimiento
            // 
            this.dataGridViewMantenimiento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMantenimiento.Location = new System.Drawing.Point(530, 48);
            this.dataGridViewMantenimiento.Name = "dataGridViewMantenimiento";
            this.dataGridViewMantenimiento.Size = new System.Drawing.Size(692, 476);
            this.dataGridViewMantenimiento.TabIndex = 0;
            this.dataGridViewMantenimiento.SelectionChanged += new System.EventHandler(this.dataGridViewMantenimiento_SelectionChanged);
            // 
            // comboBoxVehiculos
            // 
            this.comboBoxVehiculos.FormattingEnabled = true;
            this.comboBoxVehiculos.Location = new System.Drawing.Point(18, 76);
            this.comboBoxVehiculos.Name = "comboBoxVehiculos";
            this.comboBoxVehiculos.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVehiculos.TabIndex = 1;
            // 
            // vehiculo
            // 
            this.vehiculo.AutoSize = true;
            this.vehiculo.Location = new System.Drawing.Point(15, 60);
            this.vehiculo.Name = "vehiculo";
            this.vehiculo.Size = new System.Drawing.Size(35, 13);
            this.vehiculo.TabIndex = 2;
            this.vehiculo.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 55);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // comboBoxMantenimientoTipo
            // 
            this.comboBoxMantenimientoTipo.FormattingEnabled = true;
            this.comboBoxMantenimientoTipo.Location = new System.Drawing.Point(18, 133);
            this.comboBoxMantenimientoTipo.Name = "comboBoxMantenimientoTipo";
            this.comboBoxMantenimientoTipo.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMantenimientoTipo.TabIndex = 4;
            // 
            // dateTimePickerInicio
            // 
            this.dateTimePickerInicio.Location = new System.Drawing.Point(18, 222);
            this.dateTimePickerInicio.Name = "dateTimePickerInicio";
            this.dateTimePickerInicio.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerInicio.TabIndex = 6;
            // 
            // dateTimePickerProximoMantenimiento
            // 
            this.dateTimePickerProximoMantenimiento.Location = new System.Drawing.Point(277, 222);
            this.dateTimePickerProximoMantenimiento.Name = "dateTimePickerProximoMantenimiento";
            this.dateTimePickerProximoMantenimiento.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerProximoMantenimiento.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(277, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 55);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            // 
            // comboBoxEstatus
            // 
            this.comboBoxEstatus.FormattingEnabled = true;
            this.comboBoxEstatus.Location = new System.Drawing.Point(18, 337);
            this.comboBoxEstatus.Name = "comboBoxEstatus";
            this.comboBoxEstatus.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEstatus.TabIndex = 9;
            // 
            // labelIdMantenimiento
            // 
            this.labelIdMantenimiento.AutoSize = true;
            this.labelIdMantenimiento.Location = new System.Drawing.Point(414, 292);
            this.labelIdMantenimiento.Name = "labelIdMantenimiento";
            this.labelIdMantenimiento.Size = new System.Drawing.Size(35, 13);
            this.labelIdMantenimiento.TabIndex = 11;
            this.labelIdMantenimiento.Text = "label3";
            this.labelIdMantenimiento.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "label4";
            // 
            // comboBoxEstatusVendido
            // 
            this.comboBoxEstatusVendido.FormattingEnabled = true;
            this.comboBoxEstatusVendido.Location = new System.Drawing.Point(14, 331);
            this.comboBoxEstatusVendido.Name = "comboBoxEstatusVendido";
            this.comboBoxEstatusVendido.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEstatusVendido.TabIndex = 21;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(273, 436);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 55);
            this.button3.TabIndex = 20;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(273, 216);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // dateTimePickerProximoMantenimientoVendido
            // 
            this.dateTimePickerProximoMantenimientoVendido.Location = new System.Drawing.Point(14, 216);
            this.dateTimePickerProximoMantenimientoVendido.Name = "dateTimePickerProximoMantenimientoVendido";
            this.dateTimePickerProximoMantenimientoVendido.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerProximoMantenimientoVendido.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "label5";
            // 
            // comboBoxMantenimientoTipoVendido
            // 
            this.comboBoxMantenimientoTipoVendido.FormattingEnabled = true;
            this.comboBoxMantenimientoTipoVendido.Location = new System.Drawing.Point(14, 127);
            this.comboBoxMantenimientoTipoVendido.Name = "comboBoxMantenimientoTipoVendido";
            this.comboBoxMantenimientoTipoVendido.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMantenimientoTipoVendido.TabIndex = 16;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(137, 436);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 55);
            this.button4.TabIndex = 15;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "label1";
            // 
            // comboBoxVehiculosVendido
            // 
            this.comboBoxVehiculosVendido.FormattingEnabled = true;
            this.comboBoxVehiculosVendido.Location = new System.Drawing.Point(14, 70);
            this.comboBoxVehiculosVendido.Name = "comboBoxVehiculosVendido";
            this.comboBoxVehiculosVendido.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVehiculosVendido.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(526, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(692, 476);
            this.dataGridView1.TabIndex = 12;
            // 
            // MecanicoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 610);
            this.Controls.Add(this.tabControl1);
            this.Name = "MecanicoForm";
            this.Text = "MecanicoForm";
            this.Load += new System.EventHandler(this.MecanicoForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMantenimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewMantenimiento;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label vehiculo;
        private System.Windows.Forms.ComboBox comboBoxVehiculos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePickerProximoMantenimiento;
        private System.Windows.Forms.DateTimePicker dateTimePickerInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMantenimientoTipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEstatus;
        private System.Windows.Forms.Label labelIdMantenimiento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxEstatusVendido;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePickerProximoMantenimientoVendido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMantenimientoTipoVendido;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxVehiculosVendido;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}