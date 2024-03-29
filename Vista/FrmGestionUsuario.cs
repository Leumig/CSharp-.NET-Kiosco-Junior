﻿using Entidades;
using Helper;
using LogicaSQL.EntidadesDerivadas;
using System;
using System.Collections;
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
        private Usuario adminActual;
        private UsuarioDB controladorDB;

        public FrmGestionUsuario(Usuario adminActual)
        {
            InitializeComponent();
            this.adminActual = adminActual;
            this.controladorDB = new UsuarioDB();
        }

        private void FrmGestionUsuario_Load(object sender, EventArgs e)
        {
            cmb_OrdenarPor.SelectedItem = "Original";
            ActualizarDataGrid(Sistema.ListaDeUsuarios);
            Eventos.SeImportaronDatos += ActualizarDatosPorImportacion;
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            Usuario usuarioNuevo = new Usuario();

            FrmAltaUsuario formAlta = new FrmAltaUsuario(usuarioNuevo, true);

            if (formAlta.ShowDialog() == DialogResult.OK)
            {
                Sistema.ListaDeUsuarios.Add(formAlta.UsuarioIngresado);

                controladorDB.Agregar(formAlta.UsuarioIngresado);

                Logs.CrearRegistro(adminActual.NombreUsuario, $"Agregó un usuario [{formAlta.UsuarioIngresado.NombreUsuario}]");

                ActualizarDataGrid(Sistema.ListaDeUsuarios);
            }
            else
                formAlta.Close();
        }

        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (!UsuarioSeleccionadoASiMismo())
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro de eliminar a este usuario?" +
                    "\nNo podrás recuperarlo.", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (respuesta == DialogResult.OK)
                {
                    Usuario usuarioSeleccionado = SeleccionarUsuarioEspecifico(Sistema.ListaDeUsuarios);
                    Sistema.ListaDeUsuarios.Remove(usuarioSeleccionado);

                    controladorDB.Eliminar(usuarioSeleccionado, usuarioSeleccionado.Dni.ToString());

                    Logs.CrearRegistro(adminActual.NombreUsuario, $"Eliminó un usuario [{usuarioSeleccionado.NombreUsuario}]");

                    ActualizarDataGrid(Sistema.ListaDeUsuarios);
                }
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeUsuarios.Count > 0 && !UsuarioSeleccionadoASiMismo())
            {
                Usuario usuarioSeleccionado = SeleccionarUsuarioEspecifico(Sistema.ListaDeUsuarios);
                FrmAltaUsuario formModificar = new FrmAltaUsuario(usuarioSeleccionado, false);

                if (formModificar.ShowDialog() == DialogResult.OK)
                {
                    controladorDB.Modificar(usuarioSeleccionado);

                    Logs.CrearRegistro(adminActual.NombreUsuario, $"Modificó un usuario [{usuarioSeleccionado.NombreUsuario}]");

                    ActualizarDataGrid(Sistema.ListaDeUsuarios);
                }
                else
                    formModificar.Close();
            }
        }

        private void cmb_OrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();

            if (itemSeleccionado is not null)
            {
                List<Usuario> listaOrdenada = OrdenarListaUsuarios(itemSeleccionado);
                ActualizarDataGrid(listaOrdenada);
            }
        }

        /// <summary>
        /// En este caso, antes de actualizar el DataSource, se ordena la lista.
        /// </summary>
        /// <param name="lista"></param>
        public void ActualizarDataGrid(List<Usuario> lista)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();
            lista = OrdenarListaUsuarios(itemSeleccionado);

            dtg_Usuarios.DataSource = null;
            dtg_Usuarios.DataSource = lista;
        }

        /// <summary>
        /// Se ordena la lista antes de retornar al usuario.
        /// </summary>
        /// <param name="lista"></param>
        /// <returns>Retorna al usuario</returns>
        private Usuario SeleccionarUsuarioEspecifico(List<Usuario> lista)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();
            lista = OrdenarListaUsuarios(itemSeleccionado);

            return lista[dtg_Usuarios.CurrentRow.Index];
        }

        /// <summary>
        /// Verifica que el usuario seleccionado no sea el mismo que está administrando actualmente.
        /// </summary>
        /// <returns>Retorna true si es el mismo, y false si no lo es.</returns>
        private bool UsuarioSeleccionadoASiMismo()
        {
            Usuario usuarioSeleccionado = SeleccionarUsuarioEspecifico(Sistema.ListaDeUsuarios);

            if (usuarioSeleccionado == adminActual)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Dependiendo del criterio string que recibe, se crea una lista nueva de 
        /// tipo Usuario, la cual va a ser ordenada por el criterio elegido.
        /// </summary>
        /// <param name="criterio"></param>
        /// <returns>Retorna la lista ordenada</returns>
        private static List<Usuario> OrdenarListaUsuarios(string? criterio)
        {
            UsuarioDB usuarioDB = new UsuarioDB();
            Sistema.ListaDeUsuarios = usuarioDB.TraerTodosLosRegistros();

            List<Usuario> listaOrdenada = new List<Usuario>();

            switch (criterio)
            {
                case "Original":
                    listaOrdenada = Sistema.ListaDeUsuarios;
                    break;
                case "Rol":
                    listaOrdenada = Sistema.ListaDeUsuarios.OrderBy(u => u.Rol).ToList();
                    break;                
                case "Nombre":
                    listaOrdenada = Sistema.ListaDeUsuarios.OrderBy(u => u.Nombre).ToList();
                    break;
                case "Apellido":
                    listaOrdenada = Sistema.ListaDeUsuarios.OrderBy(u => u.Apellido).ToList();
                    break;
                default:
                    listaOrdenada = Sistema.ListaDeUsuarios;
                    break;
            }

            return listaOrdenada;
        }

        private void ActualizarDatosPorImportacion()
        {
            dtg_Usuarios.DataSource = null;
            dtg_Usuarios.DataSource = Sistema.ListaDeUsuarios;
            cmb_OrdenarPor.SelectedItem = "Original";
        }
    }
}