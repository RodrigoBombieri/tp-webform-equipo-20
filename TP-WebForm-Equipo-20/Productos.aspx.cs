using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TP_WebForm_Equipo_20
{
    public partial class Productos : System.Web.UI.Page
    {
        public bool filtroAvanzado { get; set; }
        public List<Articulo> listaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaProductos = negocio.listarConImagenes();

            // Inicializar las imágenes para cada producto
            foreach (var producto in listaProductos)
            {
                if (producto.UrlImagenes != null)
                {
                    // Crear una lista de objetos Imagen a partir de las URLs de imagen
                    producto.Imagenes = producto.UrlImagenes.Select(url => new Imagen { UrlImagen = url }).ToList();
                }
            }
            repRepeater.DataSource = listaProductos;
            repRepeater.DataBind();
        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }


    }
}