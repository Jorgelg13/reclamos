﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ReclamosUnity.master" AutoEventWireup="true" CodeFile="wbTipoDocumentos.aspx.cs" Inherits="Modulos_MdCatalogos_wbTipoDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="container-fluid">
        <div class="col-lg-3">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title"><b style="font-size: 16px;">Buscar y Actualizar</b></h3>
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <asp:TextBox CssClass="form-control" Style="width: 70%" runat="server" ID="txtbuscar"></asp:TextBox>
                        <asp:Button CssClass="btn btn-primary" runat="server" ID="buscar"  Text="Buscar" />
                    </div>
                    <br />
                    <div class="form-group col-sm-12 col-md-12 col-lg-12">
                        <asp:TextBox runat="server" ID="txtDescripcion" Style="width: 100%" autocomplete="off" CssClass="form-control" placeholder="nombre"></asp:TextBox>
                    </div>
                    <div class="form-group col-sm-12 col-md-12 col-lg-12">
                        <asp:DropDownList ID="ddlTipo" CssClass="form-control" Style="width: 100%" runat="server">
                            <asp:ListItem Value="Autos">Autos</asp:ListItem>
                            <asp:ListItem Value="Daños">Daños</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-sm-12 col-md-12 col-lg-12">
                        <asp:DropDownList ID="ddlestado" CssClass="form-control" Style="width: 100%" runat="server">
                            <asp:ListItem Value="True">Activo</asp:ListItem>
                            <asp:ListItem Value="False">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton runat="server" OnClick="Actualizar_Click" Visible="false" ID="Actualizar" ToolTip="Actualizar Mensajero" Style="font-size: 40px; text-align: center;"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                        <asp:LinkButton runat="server" OnClick="Guardar_Click" ID="Guardar" ToolTip="Guardar Tipo Documento" Style="font-size: 40px; text-align: center;"><i class="fa fa-floppy-o" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-9">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title"><b style="font-size: 16px;">Tipos de documentos</b></h3>
                </div>
                <div class="panel-body" style="height: 430px;">
                    <div class="scrolling-table-container" style="overflow-y: auto;">
                        <asp:GridView ID="GridDocumentos" OnSelectedIndexChanged="GridGeneral_SelectedIndexChanged" runat="server" CssClass="table table-responsive table-hover" AutoGenerateColumns="True" GridLines="None" AllowCustomPaging="True" AllowPaging="True" PageSize="300">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Editar">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentJs" Runat="Server">
     <script>
        try {
            $('#ContentPlaceHolder1_GridDocumentos tr').each(function (index) {
                $tr = $(this);
                   $td = $tr[0].cells[1];
                   $td.remove();
            });
        } catch (ex) {
        }
    </script>
</asp:Content>

