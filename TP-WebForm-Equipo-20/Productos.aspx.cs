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
        public bool filtroAvanzado { get; set; }
        public List<Articulo> listaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            filtroAvanzado = false;
            lblValidarFiltro.Text = "";
            if (!IsPostBack)
            {

                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", negocio.listarConImagenes());
                repRepeater.DataSource = Session["listaArticulos"];
                repRepeater.DataBind();
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
                filtroAvanzado = chkFiltroAvanzado.Checked;
                txtFiltro.Enabled = !filtroAvanzado;
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




    }
}