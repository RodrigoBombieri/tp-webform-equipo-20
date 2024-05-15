<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TP_WebForm_Equipo_20.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .card-img-top {
            width: 500px;
            height: 500px;
        }  
        .carousel-control-prev-icon, .carousel-control-next-icon {
            background-color: black;
        }
    </style>

    <h1>Detalle producto</h1>
    <%if (articulo != null)
        {

    %>
    <div class="row">
        <%--<div class="col-6">--%>
        <div class="col">
            <div class="card">
                <%-- <img src="<%:articulo.Imagenes[0].UrlImagen %>" class="img-fluid" alt="...">--%>
                <div id="carouselExampleControls" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="repImagenes" runat="server">
                            <ItemTemplate>
                                <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                    <img src='<%# Eval("UrlImagen") %>' class="d-block mx-auto card-img-top" alt="Problemas al cargar imagen" onerror="this.onerror=null; this.src='https://img.freepik.com/vector-premium/no-hay-foto-disponible-icono-vector-simbolo-imagen-predeterminado-imagen-proximamente-sitio-web-o-aplicacion-movil_87543-10615.jpg?w=826';">
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3">
                <asp:Label ID="txtNombre" runat="server" Text="" CssClass="h1"></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtId" class="h3">Id: </label>
                <asp:Label ID="txtId" runat="server" Text="" CssClass="h3"></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="h3">Codigo: </label>
                <asp:Label ID="txtCodigo" runat="server" Text="" CssClass="h3"></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtMarca" class="h3">Marca: </label>
                <asp:Label ID="txtMarca" runat="server" Text="" CssClass="h3"></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtCategoria" class="h3">Categoria: </label>
                <%--<asp:DropDownList ID="ddlCategoria" CssClass="form-select" DataTextField="Descripcion" DataValueField="Id" runat="server"></asp:DropDownList>--%>
                <asp:Label ID="txtCategoria" runat="server" Text="" CssClass="h3"></asp:Label>
            </div>
            <div class="mb-3">
                <label for="txtPrecio" class="h3">Precio: </label>
                <asp:Label ID="txtPrecio" runat="server" Text="" CssClass="h3"></asp:Label>
            </div>
            <div class="mb-3">
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripción: </label>
                    <asp:TextBox runat="server" ID="txtDescripcion" TextMode="MultiLine" CssClass="form-control" />
                </div>
            </div>
        </div>
    </div>
    <%}
        else
        {%>
    <h1>Artículo no encontrado</h1>
    <a href="Default.aspx">Ir a productos</a>
    <%} %>
</asp:Content>
