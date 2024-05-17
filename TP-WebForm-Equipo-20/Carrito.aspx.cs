using System;
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
                Literal carritoCantidad = Master.FindControl("carritoCantidad") as Literal;
                int cantidadInicial = Convert.ToInt32(Session["CantidadCarrito"] ?? "0");
                carritoCantidad.Text = cantidadInicial.ToString();

                // Verificar si se está agregando un artículo al carrito
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                // Si el ID no es nulo y es un número
                if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int idArticulo))
                {
                    // Obtiene la lista de IDs de artículos en el carrito desde la sesión
                    List<int> idArticulosCarrito = Session["Carrito"] as List<int>;
                    if (idArticulosCarrito == null )
                    {
                        idArticulosCarrito = new List<int>();
                    }

                    // Agregar el nuevo artículo al carrito
                    idArticulosCarrito.Add(idArticulo);
                    Session["Carrito"] = idArticulosCarrito;
                }

                // Mostrar los productos en el carrito
                MostrarProductosEnCarrito();
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
                precioTotal = listaProductos.Sum(p => p.Precio);
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
            lblCompra.Text = "NO HAY PLATA";
        }
    }
}