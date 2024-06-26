﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using Negocio;

namespace TP_WebForm_Equipo_20
{
    public partial class Carrito : System.Web.UI.Page
    {
        public List<Articulo> listaProductos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                // Cantidad de articulos en el navbar
                mostrarCantidadNavBar();

                // Mostrar los productos en el carrito
                MostrarProductosEnCarrito();
            }
        }

        protected void mostrarCantidadNavBar(int cant = 0)
        {
            Literal carritoCantidad = Master.FindControl("carritoCantidad") as Literal;

            if (carritoCantidad != null)
            {
                // Muestra la cantidad almacenada en la sesión
                int cantidadActual = Convert.ToInt32(Session["CantidadCarrito"] ?? "0");
                cantidadActual += cant;
                carritoCantidad.Text = Convert.ToString(cantidadActual);
                if (cant != 0)
                {
                    Session["CantidadCarrito"] = cantidadActual;
                }
            }
        }
        private void MostrarProductosEnCarrito()
        {
            // Obtener la lista de IDs de artículos en el carrito
            List<int> idArticulosCarrito = Session["Carrito"] as List<int>;
            decimal precioTotal = 0;

            // Si hay artículos en el carrito
            if (idArticulosCarrito != null && idArticulosCarrito.Any())
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                List<Articulo> listaProductos = new List<Articulo>();

                // Obtener los detalles de los productos en el carrito
                foreach (int idArticulo in idArticulosCarrito)
                {
                    Articulo articulo = articuloNegocio.BuscarArticuloPorId(idArticulo);
                    if (articulo != null)
                    {
                        // Evaluar si el articulo ya existe en la lista
                        if (listaProductos.Exists(p => p.ID == articulo.ID))
                        {
                            // Si ya existe incrementar la cantidad del producto
                            Articulo articuloExistente = listaProductos.Find(p => p.ID == articulo.ID);
                            articuloExistente.Cantidad++;
                        }
                        else
                        {
                            // Agregar el producto a la lista
                            articulo.Cantidad = 1;
                            listaProductos.Add(articulo);
                        }
                    }
                }

                // Mostrar los productos en el GridView
                dgvProductos.DataSource = listaProductos;
                dgvProductos.DataBind();

                // Calcular el precio total
                precioTotal = listaProductos.Sum(p => p.Precio * p.Cantidad);
                lblImporte.Text = precioTotal.ToString("C");
            }
            else
            {
                Session.Add("error", "No hay productos en el carrito");
            }
        }

        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                /// Muestra los productos en el carrito cuando se cambia de página de la grilla
                dgvProductos.PageIndex = e.NewPageIndex;
                MostrarProductosEnCarrito();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            lblCompra.Text = "SIN SALDO";
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtiene el ID del producto COMPLETO a eliminar
            LinkButton btnEliminar = (LinkButton)sender;
            // Convierte el ID obtenido del CommandArgument a entero
            int idArticulo = int.Parse(btnEliminar.CommandArgument);

            // Obtiene la lista de IDs de artículos en el carrito
            List<int> idArticulosCarrito = Session["Carrito"] as List<int>;

            // Obtiene la cantidad del producto eliminado en el carrito
            // Contando cuántas veces se repite el ID del producto en la lista
            // para restar la cantidad del producto eliminado en el navbar
            int cantidadEliminada = idArticulosCarrito.Count(id => id == idArticulo);

            // Eliminar el producto del carrito
            idArticulosCarrito.RemoveAll(id => id == idArticulo);

            // Actualizar la lista de IDs de artículos en el carrito en la sesión
            Session["Carrito"] = idArticulosCarrito;

            // Restar la cantidad del producto eliminado en el navbar
            mostrarCantidadNavBar(-(cantidadEliminada));

            // Mostrar nuevamente los productos en el carrito
            if (idArticulosCarrito.Count == 0)
            {
                Response.Redirect("Default.aspx", false);
            }
            else
            {
                MostrarProductosEnCarrito();
            }
        }

        protected void btnIncrease_Click(object sender, EventArgs e)
        {
            Button btnIncrease = (Button)sender;
            int idArticulo = int.Parse(btnIncrease.CommandArgument);
            // Obtiene la lista de IDs de artículos en el carrito
            List<int> idArticulosCarrito = Session["Carrito"] as List<int>;
            // Agregar el producto al carrito
            idArticulosCarrito.Add(idArticulo);
            // Actualizar la lista de IDs de artículos en el carrito en la sesión
            Session["Carrito"] = idArticulosCarrito;
            mostrarCantidadNavBar(1);
            MostrarProductosEnCarrito();
        }

        protected void btnDecrease_Click(object sender, EventArgs e)
        {
            // Cambiar el tipo de sender a Button
            // para poder acceder al CommandArgument del Button (el ID del producto)
            Button btnDecrease = (Button)sender;
            // Obtiene el ID del producto a eliminar desde el CommandArgument del Button
            int idArticulo = int.Parse(btnDecrease.CommandArgument);

            // Obtiene la lista de IDs de artículos en el carrito
            List<int> idArticulosCarrito = Session["Carrito"] as List<int>;

            // Eliminar el producto del carrito
            idArticulosCarrito.Remove(idArticulo);
            // Actualizar la lista de IDs de artículos en el carrito en la sesión
            Session["Carrito"] = idArticulosCarrito;

            /// Mostrar los productos en el carrito
            // Si no hay productos en el carrito, redirecciona a la página principal
            if (idArticulosCarrito.Count == 0)
            {
                mostrarCantidadNavBar(-1);
                Response.Redirect("Default.aspx", false);
                // Restar la cantidad de productos en el carrito en el navbar
            }
            else
            {
                mostrarCantidadNavBar(-1);
                MostrarProductosEnCarrito();
                // Restar la cantidad de productos en el carrito en el navbar
            }
        }
    }
}