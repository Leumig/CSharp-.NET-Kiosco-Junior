﻿namespace Vista
{
    partial class FrmLogin
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
            this.lbl_Nombre = new System.Windows.Forms.Label();
            this.lbl_Contrasenia = new System.Windows.Forms.Label();
            this.btn_Ingresar = new System.Windows.Forms.Button();
            this.btn_Autocompletar = new System.Windows.Forms.Button();
            this.txt_Nombre = new System.Windows.Forms.TextBox();
            this.txt_Contrasenia = new System.Windows.Forms.TextBox();
            this.lbl_Error = new System.Windows.Forms.Label();
            this.pic_Login = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Login)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Nombre
            // 
            this.lbl_Nombre.AutoSize = true;
            this.lbl_Nombre.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_Nombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(102)))), ((int)(((byte)(230)))));
            this.lbl_Nombre.Location = new System.Drawing.Point(50, 135);
            this.lbl_Nombre.Name = "lbl_Nombre";
            this.lbl_Nombre.Size = new System.Drawing.Size(67, 20);
            this.lbl_Nombre.TabIndex = 0;
            this.lbl_Nombre.Text = "Nombre";
            // 
            // lbl_Contrasenia
            // 
            this.lbl_Contrasenia.AutoSize = true;
            this.lbl_Contrasenia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_Contrasenia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(102)))), ((int)(((byte)(230)))));
            this.lbl_Contrasenia.Location = new System.Drawing.Point(52, 192);
            this.lbl_Contrasenia.Name = "lbl_Contrasenia";
            this.lbl_Contrasenia.Size = new System.Drawing.Size(88, 20);
            this.lbl_Contrasenia.TabIndex = 1;
            this.lbl_Contrasenia.Text = "Contraseña";
            // 
            // btn_Ingresar
            // 
            this.btn_Ingresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(180)))), ((int)(((byte)(49)))));
            this.btn_Ingresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Ingresar.FlatAppearance.BorderSize = 0;
            this.btn_Ingresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Ingresar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Ingresar.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Ingresar.Location = new System.Drawing.Point(255, 140);
            this.btn_Ingresar.Name = "btn_Ingresar";
            this.btn_Ingresar.Size = new System.Drawing.Size(128, 54);
            this.btn_Ingresar.TabIndex = 2;
            this.btn_Ingresar.Text = "INGRESAR";
            this.btn_Ingresar.UseVisualStyleBackColor = false;
            this.btn_Ingresar.Click += new System.EventHandler(this.btn_Ingresar_Click);
            // 
            // btn_Autocompletar
            // 
            this.btn_Autocompletar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(100)))), ((int)(((byte)(27)))));
            this.btn_Autocompletar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Autocompletar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Autocompletar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Autocompletar.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_Autocompletar.Location = new System.Drawing.Point(255, 200);
            this.btn_Autocompletar.Name = "btn_Autocompletar";
            this.btn_Autocompletar.Size = new System.Drawing.Size(128, 38);
            this.btn_Autocompletar.TabIndex = 3;
            this.btn_Autocompletar.Text = "Auto.";
            this.btn_Autocompletar.UseVisualStyleBackColor = false;
            this.btn_Autocompletar.Click += new System.EventHandler(this.btn_Autocompletar_Click);
            // 
            // txt_Nombre
            // 
            this.txt_Nombre.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Nombre.Location = new System.Drawing.Point(50, 158);
            this.txt_Nombre.Name = "txt_Nombre";
            this.txt_Nombre.Size = new System.Drawing.Size(137, 23);
            this.txt_Nombre.TabIndex = 4;
            // 
            // txt_Contrasenia
            // 
            this.txt_Contrasenia.Location = new System.Drawing.Point(50, 215);
            this.txt_Contrasenia.Name = "txt_Contrasenia";
            this.txt_Contrasenia.Size = new System.Drawing.Size(137, 23);
            this.txt_Contrasenia.TabIndex = 5;
            this.txt_Contrasenia.UseSystemPasswordChar = true;
            // 
            // lbl_Error
            // 
            this.lbl_Error.AutoSize = true;
            this.lbl_Error.BackColor = System.Drawing.Color.OrangeRed;
            this.lbl_Error.ForeColor = System.Drawing.Color.SeaShell;
            this.lbl_Error.Location = new System.Drawing.Point(52, 254);
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Padding = new System.Windows.Forms.Padding(2);
            this.lbl_Error.Size = new System.Drawing.Size(99, 19);
            this.lbl_Error.TabIndex = 6;
            this.lbl_Error.Text = "Mensaje de error";
            this.lbl_Error.Visible = false;
            // 
            // pic_Login
            // 
            this.pic_Login.Image = global::Vista.Properties.Resources.login;
            this.pic_Login.Location = new System.Drawing.Point(140, 12);
            this.pic_Login.Name = "pic_Login";
            this.pic_Login.Size = new System.Drawing.Size(126, 87);
            this.pic_Login.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Login.TabIndex = 7;
            this.pic_Login.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Candara", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(102)))), ((int)(((byte)(230)))));
            this.label1.Location = new System.Drawing.Point(130, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "BIENVENIDO";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 290);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pic_Login);
            this.Controls.Add(this.lbl_Error);
            this.Controls.Add(this.txt_Contrasenia);
            this.Controls.Add(this.txt_Nombre);
            this.Controls.Add(this.btn_Autocompletar);
            this.Controls.Add(this.btn_Ingresar);
            this.Controls.Add(this.lbl_Contrasenia);
            this.Controls.Add(this.lbl_Nombre);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Login)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbl_Nombre;
        private Label lbl_Contrasenia;
        private Button btn_Ingresar;
        private Button btn_Autocompletar;
        private TextBox txt_Nombre;
        private TextBox txt_Contrasenia;
        private Label lbl_Error;
        private PictureBox pic_Login;
        private Label label1;
    }
}