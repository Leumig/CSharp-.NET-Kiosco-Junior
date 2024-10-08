﻿using Entidades;
using Helper;
using LogicaSQL;
using LogicaSQL.EntidadesDerivadas;
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
    public partial class FrmLogin : Form
    {
        private Usuario usuarioIngresado;
        public Usuario UsuarioIngresado { get => usuarioIngresado; }

        public FrmLogin()
        {
            InitializeComponent();

            try
            {
                Consultas conexionBD = new Consultas();
                conexionBD.ProbarConexion();
                usuarioIngresado = new Usuario();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                FrmErrorConexion formErrorConexion = new FrmErrorConexion();
                formErrorConexion.ShowDialog();
                Close();
            }
        }

        private void btn_Ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                usuarioIngresado = Autenticacion.ConfirmarIngresoOrThrow(txt_Nombre.Text, txt_Contrasenia.Text);
                FrmPrincipal formPrincipal = new FrmPrincipal(this);
                formPrincipal.Show();
                Hide();
                Logs.CrearRegistro(usuarioIngresado.NombreUsuario, $"Inició sesión");
            }
            catch (Exception ex)
            {
                lbl_Error.Text = ex.Message;
                lbl_Error.Visible = true;
            }
        }

        private void btn_Autocompletar_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeUsuarios.Count > 0)
            {
                Random rnd = new Random();
                int numeroRandom = rnd.Next(0, Sistema.ListaDeUsuarios.Count);

                txt_Contrasenia.UseSystemPasswordChar = true;
                txt_Nombre.Text = Sistema.ListaDeUsuarios[numeroRandom].NombreUsuario;
                txt_Contrasenia.Text = Sistema.ListaDeUsuarios[numeroRandom].Contrasenia;
            }
        }

        private void btn_Ver_Click(object sender, EventArgs e)
        {
            if (!txt_Contrasenia.UseSystemPasswordChar)
                txt_Contrasenia.UseSystemPasswordChar = true;
            else
                txt_Contrasenia.UseSystemPasswordChar = false;
        }

        private void btn_IngresarAdmin_Click(object sender, EventArgs e)
        {
            foreach (Usuario usuario in Sistema.ListaDeUsuarios)
            {
                if (usuario.Rol == ERol.SuperUsuario)
                {
                    IngresarRapidoPorUsuario(usuario);
                    break;
                }
            }
        }

        private void btn_IngresarEmpleado_Click(object sender, EventArgs e)
        {
            foreach (Usuario usuario in Sistema.ListaDeUsuarios)
            {
                if (usuario.Rol == ERol.Empleado)
                {
                    IngresarRapidoPorUsuario(usuario);
                    break;
                }
            }
        }

        private void btn_IngresarCliente_Click(object sender, EventArgs e)
        {
            foreach (Usuario usuario in Sistema.ListaDeUsuarios)
            {
                if (usuario.Rol == ERol.Cliente)
                {
                    IngresarRapidoPorUsuario(usuario);
                    break;
                }
            }
        }

        public void LimpiarDatos()
        {
            txt_Nombre.Text = string.Empty;
            txt_Contrasenia.Text = string.Empty;
            lbl_Error.Visible = false;
            txt_Contrasenia.UseSystemPasswordChar = true;
        }

        /// <summary>
        /// Instancia un formulario principal pasandole por parámetro a este mismo formulario,
        /// y lo muestra. Después, se oculta a si mismo. El usuario ingresado será el recibido
        /// por parámetro.
        /// </summary>
        /// <param name="usuario"></param>
        private void IngresarRapidoPorUsuario(Usuario usuario)
        {
            usuarioIngresado = usuario;
            FrmPrincipal formPrincipal = new FrmPrincipal(this);
            formPrincipal.Show();
            Hide();
            Logs.CrearRegistro(usuario.NombreUsuario, $"Inició sesión");
        }
    }
}
