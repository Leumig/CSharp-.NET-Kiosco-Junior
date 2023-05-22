﻿using Entidades;
using Helper;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class FrmCompras : Form
    {
        private Cliente clienteActual;
        private List<Producto> menu;
        private float precioTotal;
        private Venta ventaActual;

        public FrmCompras(Usuario usuarioActual)
        {
            InitializeComponent();
            clienteActual = ConvertirUsuarioACliente(usuarioActual);
            ventaActual = new Venta();
            menu = new List<Producto>();
            menu.AddRange(Sistema.ListaDeProductos);
        }

        private void FrmCompras_Load(object sender, EventArgs e)
        {
            dtg_Productos.DataSource = null;
            dtg_Productos.DataSource = menu;
            OcultarProductosAgotados();
            ReiniciarCantidadDeProductos();
        }

        private void btn_Agregar_Click(object sender, EventArgs e)
        {
            if (dtg_Productos.Rows.Count > 0)
            {
                try
                {
                    VerificarProductoStock();

                    AgregarProductoAlCarrito();

                    EscribirPrecioTotal();

                    ActualizarDataGrids(menu, clienteActual.Carrito);

                    lbl_Error.Visible = false;
                }
                catch (Exception ex)
                {
                    lbl_Error.Text = ex.Message;
                    lbl_Error.Visible = true;
                }
            }
        }

        private void btn_Confirmar_Click(object sender, EventArgs e)
        {
            if (clienteActual.Carrito.Count > 0)
            {
                ventaActual = new Venta(clienteActual.NombreCompleto, precioTotal);

                Sistema.ListaDeVentas.Add(ventaActual);

                MostrarMensajeVentaExitosa();

                ReducirStockProducto();

                OcultarProductosAgotados();

                ReiniciarCarrito();
            }
        }
        private void btn_Sacar_Click(object sender, EventArgs e)
        {
            if (clienteActual.Carrito.Count > 0)
            {
                Producto productoSeleccionado = SeleccionarProductoEspecifico(true);

                productoSeleccionado.CantidadEnCarrito = 0;
                clienteActual.Carrito.Remove(productoSeleccionado);

                ActualizarDataGrids(menu, clienteActual.Carrito);
                EscribirPrecioTotal();
            }
        }

        private void btn_VaciarCarrito_Click(object sender, EventArgs e)
        {
            if (clienteActual.Carrito.Count > 0)
                ReiniciarCarrito();
        }

        private void OcultarProductosAgotados()
        {
            foreach (Producto producto in Sistema.ListaDeProductos)
            {
                if (producto.Stock == 0)
                    menu.Remove(producto);
            }
        }

        private static void ReiniciarCantidadDeProductos()
        {
            foreach (Producto producto in Sistema.ListaDeProductos)
            {
                producto.CantidadEnCarrito = 0;
            }
        }

        private void VerificarProductoStock()
        {
            Producto productoSeleccionado = SeleccionarProductoEspecifico(false);

            if (productoSeleccionado.CantidadEnCarrito > (productoSeleccionado.Stock - 1))
                throw new Exception("Producto agotado");
        }

        private Producto SeleccionarProductoEspecifico(bool esCarrito)
        {
            if (esCarrito)
                return clienteActual.Carrito[dtg_Carrito.CurrentRow.Index];

            return menu[dtg_Productos.CurrentRow.Index];
        }

        private void AgregarProductoAlCarrito()
        {
            Producto productoSeleccionado = SeleccionarProductoEspecifico(false);

            if (clienteActual.Carrito.Contains(productoSeleccionado))
                productoSeleccionado.CantidadEnCarrito++;
            else
            {
                clienteActual.Carrito.Add(productoSeleccionado);
                productoSeleccionado.CantidadEnCarrito++;
                lbl_Error.Visible = false;
            }
        }

        private void EscribirPrecioTotal()
        {
            precioTotal = 0;

            foreach (Producto producto in clienteActual.Carrito)
            {
                precioTotal += producto.Precio * producto.CantidadEnCarrito;
            }

            lbl_Total.Text = $"Precio TOTAL: $ {precioTotal:0.00}";
        }

        private void ActualizarDataGrids(List<Producto> menuLista, List<Producto> carrito)
        {
            dtg_Productos.DataSource = null;
            dtg_Productos.DataSource = menuLista;

            dtg_Carrito.DataSource = null;
            dtg_Carrito.DataSource = carrito;
        }

        private void ReiniciarCarrito()
        {
            ReiniciarCantidadDeProductos();
            clienteActual.Carrito.Clear();
            ActualizarDataGrids(menu, clienteActual.Carrito);
            precioTotal = 0;
            lbl_Total.Text = $"Precio TOTAL: $ {precioTotal:0.00}";
        }

        private static void ReducirStockProducto()
        {
            foreach (Producto producto in Sistema.ListaDeProductos)
            {
                producto.Stock -= producto.CantidadEnCarrito;
            }
        }





        private void MostrarMensajeVentaExitosa()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("¡Compra realizada con éxito!\n");
            sb.AppendLine($"ID de la transacción: {ventaActual.Id}");
            sb.AppendLine($"Cantidad de productos distintos comprados: {clienteActual.TamañoDeCarrito}");
            sb.AppendLine($"Comprador: {clienteActual.NombreCompleto}\n");
            sb.AppendLine($"Importe Total: $ {ventaActual.ValorTotal.ToString("0.00")}"); ;

            MessageBox.Show(sb.ToString(), "Kiosco Junior");
        }

        private Cliente ConvertirUsuarioACliente(Usuario usuarioActual)
        {
            Cliente clienteCreado = new Cliente(usuarioActual.Nombre, usuarioActual.Apellido, usuarioActual.Dni,
                                        usuarioActual.NombreUsuario, usuarioActual.Contrasenia, usuarioActual.Rol);

            return clienteCreado;
        }
    }
}