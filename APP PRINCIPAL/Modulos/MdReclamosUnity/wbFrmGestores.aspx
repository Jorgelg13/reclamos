﻿<%@ Page Title="Creacion Gestores" Language="C#" MasterPageFile="~/ReclamosUnity.master" AutoEventWireup="true" CodeFile="wbFrmGestores.aspx.cs" Inherits="Modulos_MdReclamosUnity_wbFrmGestores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="panel panel-info col-sm-12">
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">Buscar Gestor</a></li>
                <li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">Ingresar Gestor </a></li>
            </ul>
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane" id="home">
                    <div class="panel-body">
                        <div class=" form-inline">
                            <asp:TextBox runat="server" autocomplete="off" ID="txtBusqueda" Style="width: 30%" class="form-control" placeholder="Nombre del Gestor"></asp:TextBox>
                            <asp:Button runat="server" Text="Buscar" ID="btnBuscar" class="btn btn-primary" />
                        </div>
                        <br />
                        <div class="scrolling-table-container">
                            <asp:GridView ID="GridGestores" CssClass="table bs-table tablaDetalleAuto table-responsive table-hover" runat="server" GridLines="None" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SqlDataSourceGestores">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:BoundField DataField="id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="id">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefono" HeaderText="Telefono" SortExpression="telefono">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="correo" HeaderText="Correo" SortExpression="correo">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:CheckBoxField DataField="estado" HeaderText="Estado" SortExpression="estado" />
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <%--segundo tab--%>
                <div role="tabpanel" class="tab-pane" id="profile">
                    <div class="panel-body form-inline">
                        <br />
                        <asp:TextBox runat="server" ID="txtNombre" Style="width: 25%" autocomplete="off" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtTelefono" Style="width: 25%" autocomplete="off" class="form-control" placeholder="Telefono"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtCorreo" type="email" Style="width: 35%" autocomplete="off" class="form-control" placeholder="Correo Electronico"></asp:TextBox>
                        <asp:DropDownList ID="ddlTipo" CssClass="form-control" Style="width: 20%" runat="server">
                            <asp:ListItem>Autos</asp:ListItem>
                            <asp:ListItem>Daños varios</asp:ListItem>
                            <asp:ListItem>Medicos</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Button runat="server" Text="Guardar" ID="btnguardar" class="btn btn-primary" OnClick="btnguardar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="SqlDataSourceGestores" runat="server" ConnectionString="<%$ ConnectionStrings:reclamosConnectionString %>" SelectCommand="SELECT *FROM gestores where (nombre like '%' + @nombre + '%') " UpdateCommand=" UPDATE gestores SET nombre= @nombre, telefono= @telefono, correo = @correo, estado = @estado where id = @id">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtBusqueda" Name="nombre" PropertyName="Text" DefaultValue=" " />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="nombre" />
            <asp:Parameter Name="telefono" />
            <asp:Parameter Name="correo" />
            <asp:Parameter Name="estado" />
            <asp:Parameter Name="id" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

