﻿using Helper;
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
    public partial class FrmCompraExitosa : Form
    {
        private Eventos eventosCompras;

        public FrmCompraExitosa(Eventos eventos)
        {
            InitializeComponent();
            this.eventosCompras = eventos;
            eventosCompras.SeConfirmoLaCompra += MostrarMensaje;
        }

        private void MostrarMensaje(string mensaje)
        {
            rtb_Mensaje.Text = mensaje;
            this.ShowDialog();
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
