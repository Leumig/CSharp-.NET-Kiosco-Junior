﻿using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FrmInfoUsuario : Form
    {
        private Usuario usuarioIngresado;

        public FrmInfoUsuario(Usuario usuarioIngresado)
        {
            InitializeComponent();
            this.usuarioIngresado = usuarioIngresado;
        }

        private void FrmInfoUsuario_Load(object sender, EventArgs e)
        {
            if(usuarioIngresado is not null)
            {
                lbl_Nombre.Text = usuarioIngresado.Nombre;
                lbl_Apellido.Text = usuarioIngresado.Apellido;
                lbl_Dni.Text = usuarioIngresado.Dni.ToString();
                lbl_NombreDeUsuario.Text = usuarioIngresado.NombreUsuario;
                lbl_Rol.Text = usuarioIngresado.Rol.ToString();
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {

        }
    }
}
