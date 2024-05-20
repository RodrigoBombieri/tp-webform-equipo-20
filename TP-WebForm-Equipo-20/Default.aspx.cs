using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_WebForm_Equipo_20
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Articulo> listaProductos { get; set; }
        public List<Imagen> listaImagenes { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblValidarFiltro.Text = "";
                //carga del repeater con listado
                cargarRepeaterCards();

                // Busca el control Literal en el MasterPage
                mostrarCantidadNavBar();
            }
        }
        protected void cargarRepeaterCards()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session.Add("listaArticulos", negocio.listarConImagenes());
            repRepeater.DataSource = Session["listaArticulos"];
            repRepeater.DataBind();
        }
        protected void mostrarCantidadNavBar(int cant=0)
        {
            Literal carritoCantidad = Master.FindControl("carritoCantidad") as Literal;

            if (carritoCantidad != null)
            {
                // Muestra la cantidad almacenada en la sesión
                int cantidadActual = Convert.ToInt32(Session["CantidadCarrito"] ?? "0");
                cantidadActual += cant; 
                carritoCantidad.Text = Convert.ToString(cantidadActual);
                if (cant>0)
                {
                    Session["CantidadCarrito"] = cantidadActual;
                }
            }
        }
        
        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
                List<Articulo> listaFiltrada = lista.FindAll(k => k.Nombre.ToLower().Contains(txtFiltro.Text.ToLower()) || k.Codigo.ToLower().Contains(txtFiltro.Text.ToLower()) || k.Descripcion.ToLower().Contains(txtFiltro.Text.ToLower()));
                repRepeater.DataSource = listaFiltrada;
                repRepeater.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtFiltro.Enabled = !chkFiltroAvanzado.Checked;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlCriterio.Items.Clear();
                if(ddlCampo.SelectedItem.ToString() == "Precio")
                {
                    ddlCriterio.Items.Add("Mayor a");
                    ddlCriterio.Items.Add("Menor a");
                    ddlCriterio.Items.Add("Igual a");
                }
                else
                {
                    ddlCriterio.Items.Add("Contiene");
                    ddlCriterio.Items.Add("Empieza con");
                    ddlCriterio.Items.Add("Termina con");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool validarFiltro()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                if (txtFiltroAvanzado.Text.ToString() == "")
                {
                    return false;
                    
                }
                else if (!double.TryParse(txtFiltroAvanzado.Text, out double precio))
                {
                    lblValidarFiltro.Text = "El precio debe ser un número";
                    return true;
                }
            }

            return false;
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            txtFiltro.Enabled = true;
            lblValidarFiltro.Text = "";
            chkFiltroAvanzado.Checked = false;
            cargarRepeaterCards();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                if (validarFiltro())
                    return;

                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    repRepeater.DataSource = negocio.listarConImagenes();
                }
                else
                {
                    repRepeater.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text);
                }

                repRepeater.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            mostrarCantidadNavBar(1);
            string productId = ((Button)sender).CommandArgument;
            agregarArticulo(productId);
            //Redirige a la página del carrito con el ID del producto
            Response.Redirect("Carrito.aspx?id=" + productId);
        }
        protected void agregarArticulo(string id)
        {
            // Verificar si se está agregando un artículo al carrito
            // Si el ID no es nulo y es un número
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int idArticulo))
            {
                // Obtiene la lista de IDs de artículos en el carrito desde la sesión
                List<int> listaArticulosCarrito = Session["Carrito"] as List<int>;
                if (listaArticulosCarrito == null)
                {
                    listaArticulosCarrito = new List<int>();
                }
                // Agregar el nuevo artículo al carrito
                listaArticulosCarrito.Add(idArticulo);
                Session["Carrito"] = listaArticulosCarrito;
            }
        }
        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalleArticulo.aspx?id=" + ((Button)sender).CommandArgument);
        }
    }
}