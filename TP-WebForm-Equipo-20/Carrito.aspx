<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TP_WebForm_Equipo_20.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .total {
            font-size: 30px;
            font-weight: bold;
            margin: 20px;
        }

        .importe {
            font-size: 30px;
            font-weight: bold;
            margin: 20px;
        }

        .btn {
            margin: 20px;
        }
    </style>
    <h1>Carrito de compras </h1>

    <asp:GridView ID="dgvProductos" DataKeyNames="ID"
        CssClass="table" AutoGenerateColumns="false" OnPageIndexChanging="dgvProductos_PageIndexChanging"
        AllowPaging="true" PageSize="5" runat="server">
        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Art{iculo" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <div class="input-group">
                        <asp:LinkButton ID="btnDecrease" runat="server" CommandName="Decrease" CommandArgument='<%# Eval("ID") %>' OnClick="btnMenos_Click" CssClass="btn btn-danger" >-</asp:LinkButton>
                        <asp:Label ID="lblQuantity" runat="server" Text="1" CssClass="form-control text-center" />                         
                        <asp:LinkButton ID="btnIncrease" runat="server" CommandName="Increase" CommandArgument='<%# Eval("ID") %>' OnClick="btnMas_Click" CssClass="btn btn-success" >+</asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>                   
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEliminar" OnClick="btnEliminar_Click" runat="server" CssClass="btn btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("ID") %>'>Eliminar</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div>
        <asp:Label ID="lblTotal" runat="server" Text="Total a pagar: " CssClass="h3 total"></asp:Label>
        <asp:Label ID="lblImporte" runat="server" Text="" CssClass="h3 importe"></asp:Label>
    </div>
    <div>
        <asp:Button ID="btnVolver" Text="Volver" CssClass="btn btn-primary" OnClick="btnVolver_Click" runat="server" />
        <asp:Button ID="btnComprar" Text="Comprar" CssClass="btn btn-primary" OnClick="btnComprar_Click" runat="server" />
        <asp:Label ID="lblCompra" Text="" ForeColor="Red" runat="server" />
    </div>

</asp:Content>
