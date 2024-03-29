﻿using Entidades;
using Helper;
using LogicaSQL.EntidadesDerivadas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FrmProductos : Form
    {
        private ProductoDB controladorDB;
        private Usuario usuario;

        public FrmProductos(Usuario usuarioActual)
        {
            InitializeComponent();
            controladorDB = new ProductoDB();
            usuario = usuarioActual;            
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            cmb_OrdenarPor.SelectedItem = "Original";
            ActualizarDataGrid(Sistema.ListaDeProductos);
            Eventos.SeImportaronDatos += ActualizarDatosPorImportacion;
        }

        private void btn_Detalles_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeProductos.Count > 0)
            {
                Producto productoSeleccionado = SeleccionarProductoEspecifico(Sistema.ListaDeProductos);

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"{productoSeleccionado.Nombre} \n");
                sb.AppendLine("Descripción");
                sb.AppendLine($"{productoSeleccionado.Descripcion}");

                MessageBox.Show(sb.ToString(), "DETALLES");
            }
        }

        private void btn_AltaProducto_Click(object sender, EventArgs e)
        {
            Producto productoNuevo = new Producto();

            FrmAltaProducto formAlta = new FrmAltaProducto(productoNuevo, true);

            if (formAlta.ShowDialog() == DialogResult.OK)
            {
                Sistema.ListaDeProductos.Add(formAlta.ProductoIngresado);

                controladorDB.Agregar(formAlta.ProductoIngresado);

                Logs.CrearRegistro(usuario.NombreUsuario, $"Agregó un producto");

                ActualizarDataGrid(Sistema.ListaDeProductos);
            }else
                formAlta.Close();
        }

        private void btn_BajaProducto_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeProductos.Count > 0)
            {
                Producto productoSeleccionado = SeleccionarProductoEspecifico(Sistema.ListaDeProductos);
                Sistema.ListaDeProductos.Remove(productoSeleccionado);

                controladorDB.Eliminar(productoSeleccionado, productoSeleccionado.Id.ToString());

                Logs.CrearRegistro(usuario.NombreUsuario, $"Eliminó un producto [{productoSeleccionado.Id}]");

                ActualizarDataGrid(Sistema.ListaDeProductos);
            }
        }

        private void btn_Modificar_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeProductos.Count > 0)
            {
                Producto productoSeleccionado = SeleccionarProductoEspecifico(Sistema.ListaDeProductos);
                FrmAltaProducto formModificar = new FrmAltaProducto(productoSeleccionado, false);

                if (formModificar.ShowDialog() == DialogResult.OK)
                {
                    controladorDB.Modificar(productoSeleccionado);

                    Logs.CrearRegistro(usuario.NombreUsuario, $"Modificó un producto [{productoSeleccionado.Id}]");

                    ActualizarDataGrid(Sistema.ListaDeProductos);
                }
                else
                    formModificar.Close();
            }
        }

        private void btn_Stockear_Click(object sender, EventArgs e)
        {
            if (num_Stockear.Value >= 1)
            {
                ReestablecerProductos();
                Logs.CrearRegistro(usuario.NombreUsuario, $"Reestableció Stock");
                ActualizarDataGrid(Sistema.ListaDeProductos);
            }
        }

        private void cmb_OrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();

            if (itemSeleccionado is not null)
            {
                List<Producto> listaOrdenada = OrdenarListaProductos(itemSeleccionado);
                ActualizarDataGrid(listaOrdenada);
            }
        }

        /// <summary>
        /// En este caso, antes de actualizar el DataSource, se ordena la lista.
        /// </summary>
        /// <param name="lista"></param>
        public void ActualizarDataGrid(List<Producto> lista)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();
            lista = OrdenarListaProductos(itemSeleccionado);

            dtg_Productos.DataSource = null;
            dtg_Productos.DataSource = lista;
        }

        /// <summary>
        /// Se ordena la lista antes de retornar al producto.
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        private Producto SeleccionarProductoEspecifico(List<Producto> lista)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();
            lista = OrdenarListaProductos(itemSeleccionado);

            return lista[dtg_Productos.CurrentRow.Index];
        }

        /// <summary>
        /// Dependiendo del criterio string que recibe, se crea una lista nueva de 
        /// tipo Producto, la cual va a ser ordenada por el criterio elegido.
        /// </summary>
        /// <param name="criterio"></param>
        /// <returns>Retorna la lista ordenada</returns>
        private static List<Producto> OrdenarListaProductos(string? criterio)
        {
            ProductoDB controladorDB = new ProductoDB();
            Sistema.ListaDeProductos = controladorDB.TraerTodosLosRegistros();

            List<Producto> listaOrdenada = new List<Producto>();

            switch (criterio)
            {
                case "Original":
                    listaOrdenada = Sistema.ListaDeProductos;
                    break;
                case "ID":
                    listaOrdenada = Sistema.ListaDeProductos.OrderBy(p => p.Id).ToList();
                    break;
                case "Tipo":
                    listaOrdenada = Sistema.ListaDeProductos.OrderBy(p => p.Tipo).ToList();
                    break;
                case "Nombre":
                    listaOrdenada = Sistema.ListaDeProductos.OrderBy(p => p.Nombre).ToList();
                    break;
                case "Marca":
                    listaOrdenada = Sistema.ListaDeProductos.OrderBy(p => p.Marca).ToList();
                    break;
                case "Precio":
                    listaOrdenada = Sistema.ListaDeProductos.OrderByDescending(p => p.Precio).ToList();
                    break;
                case "Stock":
                    listaOrdenada = Sistema.ListaDeProductos.OrderBy(p => p.Stock).ToList();
                    break;
                default:
                    listaOrdenada = Sistema.ListaDeProductos;
                    break;
            }

            return listaOrdenada;
        }

        private void ActualizarDatosPorImportacion()
        {
            dtg_Productos.DataSource = null;
            dtg_Productos.DataSource = Sistema.ListaDeProductos;
            cmb_OrdenarPor.SelectedItem = "Original";
        }

        private void ReestablecerProductos()
        {
            foreach (Producto producto in Sistema.ListaDeProductos)
            {
                producto.Stock += (int)num_Stockear.Value;
                controladorDB.Modificar(producto);
            }
        }
    }
}
