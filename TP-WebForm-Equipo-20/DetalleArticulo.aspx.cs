using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

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
                            if (articulo.Imagenes.Count == 0)                            
                            {
                                Imagen imagen = new Imagen();
                                imagen.ID = 0;
                                imagen.IdArticulo = 0;
                                imagen.UrlImagen = "https://img.freepik.com/vector-premium/no-hay-foto-disponible-icono-vector-simbolo-imagen-predeterminado-imagen-proximamente-sitio-web-o-aplicacion-movil_87543-10615.jpg?w=826";
                                articulo.Imagenes.Add(imagen);
                            }
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