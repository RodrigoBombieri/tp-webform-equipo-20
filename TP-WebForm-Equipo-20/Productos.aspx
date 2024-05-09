<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="TP_WebForm_Equipo_20.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /*Estilos específicos*/
    </style>
    <h1>Productos </h1>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Filtrar" runat="server" />
                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox ID="chkFiltroAvanzado" Text="Filtro Avanzado" AutoPostBack="true" OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" runat="server" />
            </div>
        </div>

        <%if (chkFiltroAvanzado.Checked)
            {%>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label ID="lblCampo" Text="Campo" runat="server" />
                    <asp:DropDownList ID="ddlCampo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                        <asp:ListItem Text="Precio" />
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Descripcion" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label ID="lblCriterio" Text="Criterio" runat="server" />
                    <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-control" AutoPostBack="true">
                        <asp:ListItem Text="Igual a" />
                        <asp:ListItem Text="Mayor a" />
                        <asp:ListItem Text="Menor a" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label ID="lblFiltroAvanzado" Text="Filtro" runat="server" />
                    <asp:TextBox ID="txtFiltroAvanzado" CssClass="form-control" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>
        <% } %>
    </div>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repRepeater" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <%--<img src="<%#Eval("UrlImagen") %>" class="card-img-top mx-auto d-block" alt="Imagen" />--%>
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text">Código: <%#Eval("Codigo") %></p>
                            <p class="card-text">Descripción: <%#Eval("Descripcion") %></p>
                            <p class="card-text">Marca: <%#Eval("Marca.Descripcion") %></p>
                            <p class="card-text">Categoría: <%#Eval("Categoria.Descripcion") %></p>
                            <p class="card-text">Precio: $<%#Eval("Precio") %></p>
                            <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>" class="btn btn-primary">Ver Detalle</a>
                            <a href="Carrito.aspx?id=<%#Eval("Id") %>" class="btn btn-primary">Agregar al Carrito</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
