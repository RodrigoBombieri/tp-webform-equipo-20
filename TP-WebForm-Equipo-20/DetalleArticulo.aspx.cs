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
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Articulo articulo = new Articulo();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string id;
            
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            if (!IsPostBack)
            {
                articulo = null;                
                try
                {
                    if (Request.QueryString["id"] != null)
                    {
                        id = Request.QueryString["id"].ToString();
                        articulo = articuloNegocio.buscar(id);
                        if (articulo != null)
                        {
                            txtId.Text = articulo.ID.ToString();
                            txtNombre.Text = articulo.Nombre.ToString();
                            txtDescripcion.Text = articulo.Descripcion.ToString();
                            txtCodigo.Text = articulo.Codigo.ToString();
                            txtMarca.Text = articulo.Marca.Descripcion;
                            txtCategoria.Text = articulo.Categoria.Descripcion;
                            txtPrecio.Text = articulo.Precio.ToString();

                            repImagenes.DataSource = articulo.Imagenes;
                            repImagenes.DataBind();
                        }
                    }                
                }
                catch (Exception ex)
                {
                }                
            }
        }
    }
}