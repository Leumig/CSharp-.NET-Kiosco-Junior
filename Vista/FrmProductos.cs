﻿using Entidades;
using Helper;
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
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            cmb_OrdenarPor.SelectedItem = "Original";
            ActualizarDataGrid(Sistema.ListaDeProductos);
        }

        public void ActualizarDataGrid(List<Producto> lista)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();
            lista = OrdenarListaProductos(itemSeleccionado);

            dtg_Productos.DataSource = null;
            dtg_Productos.DataSource = lista;
        }

        private Producto SeleccionarProductoEspecifico(List<Producto> lista)
        {
            string? itemSeleccionado = cmb_OrdenarPor.SelectedItem.ToString();
            lista = OrdenarListaProductos(itemSeleccionado);

            return lista[dtg_Productos.CurrentRow.Index];
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

            FrmAltaProducto formAltaProducto = new FrmAltaProducto(productoNuevo, true);

            if (formAltaProducto.ShowDialog() == DialogResult.OK)
            {
                Sistema.ListaDeProductos.Add(formAltaProducto.ProductoIngresado);
                ActualizarDataGrid(Sistema.ListaDeProductos);
            }else
                formAltaProducto.Close();
        }

        private void btn_BajaProducto_Click(object sender, EventArgs e)
        {
            if (Sistema.ListaDeProductos.Count > 0)
            {
                Producto productoSeleccionado = SeleccionarProductoEspecifico(Sistema.ListaDeProductos);
                Sistema.ListaDeProductos.Remove(productoSeleccionado);
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
                    ActualizarDataGrid(Sistema.ListaDeProductos);
                else
                    formModificar.Close();
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

        private List<Producto> OrdenarListaProductos(string? criterio)
        {
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
                    listaOrdenada = Sistema.ListaDeProductos.OrderBy(p => p.Precio).ToList();
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
    }
}
