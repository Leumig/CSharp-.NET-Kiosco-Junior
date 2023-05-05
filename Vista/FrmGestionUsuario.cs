﻿using Entidades;
using Helper;
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
    public partial class FrmGestionUsuario : Form
    {
        public FrmGestionUsuario()
        {
            InitializeComponent();
        }

        private void FrmGestionUsuario_Load(object sender, EventArgs e)
        {
            ActualizarDataGrid(Sistema.ListaDeUsuarios);
        }

        public void ActualizarDataGrid(List<Usuario> listaUsuarios)
        {
            dtg_Usuarios.DataSource = null;
            dtg_Usuarios.DataSource = listaUsuarios;
        }
        private Usuario SeleccionarUsuarioEspecifico()
        {
            return Sistema.ListaDeUsuarios[dtg_Usuarios.CurrentRow.Index];
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            FrmAltaUsuario formAltaUsuario = new FrmAltaUsuario();

            if (formAltaUsuario.ShowDialog() == DialogResult.OK)
            {
                Sistema.ListaDeUsuarios.Add(formAltaUsuario.UsuarioCreado);
                this.ActualizarDataGrid(Sistema.ListaDeUsuarios);
            }
            else
            {
                formAltaUsuario.Close();
            }
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeUsuarios.Count > 0)
            {
                Usuario usuarioSeleccionado = SeleccionarUsuarioEspecifico();
                Sistema.ListaDeUsuarios.Remove(usuarioSeleccionado);
                this.ActualizarDataGrid(Sistema.ListaDeUsuarios);
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeUsuarios.Count > 0)
            {
                FrmAltaUsuario formAltaUsuario = new FrmAltaUsuario();
                Usuario productoSeleccionado = SeleccionarUsuarioEspecifico();

                if (formAltaUsuario.ShowDialog() == DialogResult.OK)
                {
                    int indice = Sistema.ListaDeUsuarios.IndexOf(productoSeleccionado);
                    Sistema.ListaDeUsuarios.Remove(productoSeleccionado);
                    //formAltaUsuario.UsuarioCreado.Id = productoSeleccionado.Id;
                    Sistema.ListaDeUsuarios.Insert(indice, formAltaUsuario.UsuarioCreado);
                    this.ActualizarDataGrid(Sistema.ListaDeUsuarios);
                }
                else
                {
                    formAltaUsuario.Close();
                }
            }
        }
    }
}