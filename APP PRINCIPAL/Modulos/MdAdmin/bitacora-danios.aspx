﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="bitacora-danios.aspx.cs" Inherits="Modulos_MdAdmin_bitacora_danios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

        td, th {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 2px;
        }

        tr:nth-child(even) {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="container-fluid">
        <div class="form-inline">
            <div class="form-group" style="width: 15%">
                <label for="message-text" class="control-label">Fecha Inicio:</label>
                <asp:TextBox ID="fechaInicio" Style="width: 100%" CssClass="form-control" Height="34px" type="date" runat="server"></asp:TextBox>
            </div>
            <div class="form-group" style="width: 15%">
                <label>Fecha Final:</label>
                <asp:TextBox ID="fechaFinal" Style="width: 100%" CssClass="form-control" Height="34px" type="date" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#bitacora">Ver Detalle</button>
            </div>
        </div>
        <br />
        <div class="panel panel-info" style="min-height: 400px;">
            <div class="panel-heading">Registro de Reclamos Daños</div>
            <div class="panel-body">
                <div class="scrolling-table-container">
                    <asp:GridView ID="GridBitacoras" CssClass="table bs-table tablaDetalleAuto table-responsive table-hover"
                        runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="true" DataKeyNames="id"
                        OnSelectedIndexChanged="GridBitacoras_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="modal fade bs-example-modal-lg" id="bitacora">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"><b>Bitacora De Reclamo</b></h4>
                    </div>
                    <div class="modal-body">
                        <table style="width: 100%">
                            <tr>
                                <td><b>Departamento:</b></td>
                                <td>Reclamos Daños</td>
                                <td><b>Reportante:</b></td>
                                <td>
                                    <asp:Label ID="lblReportante" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Numero Reclamo:</b></td>
                                <td>
                                    <asp:Label ID="lblId" runat="server"></asp:Label></td>
                                <td><b>Telefono Reportante:</b></td>
                                <td>
                                    <asp:Label ID="lblTelefonoReportante" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Poliza:</b></td>
                                <td>
                                    <asp:Label ID="lblPoliza" runat="server"></asp:Label></td>
                                <td><b>Fecha Incidente:</b></td>
                                <td>
                                    <asp:Label ID="lblHora" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Asegurado:</b></td>
                                <td>
                                    <asp:Label ID="lblAsegurado" runat="server"></asp:Label></td>
                                <td><b>Tipo Servicio:</b></td>
                                <td>
                                    <asp:Label ID="lblTipoServicio" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Ejecutivo:</b></td>
                                <td>
                                    <asp:Label ID="lblEjecutivo" runat="server"></asp:Label></td>
                                <td><b>Estado:</b></td>
                                <td>
                                    <asp:Label ID="lblEstado" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Contratante:</b></td>
                                <td>
                                    <asp:Label ID="lblContratante" runat="server"></asp:Label></td>
                                <td><b>Aseguradora:</b></td>
                                <td>
                                    <asp:Label ID="lblAseguradora" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Boleta:</b></td>
                                <td>
                                    <asp:Label ID="lblBoleta" runat="server"></asp:Label></td>
                                <td><b>Ajustador:</b></td>
                                <td>
                                    <asp:Label ID="lblAjustador" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Ramo:</b></td>
                                <td>
                                    <asp:Label ID="lblRamo" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <br />
                        <p>
                            <asp:Label runat="server" ID="lblUbicacion"></asp:Label>
                        </p>
                        <p>
                            <asp:Label runat="server" ID="lblversion"></asp:Label>
                        </p>
                        <br />
                        <div class="scrolling-table-container">
                            <asp:GridView ID="Gridllamadas" CssClass="table bs-table table-responsive table-hover" runat="server" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-warning" data-dismiss="modal">Cerrar</button>
                        <button type="button" onclick="printDiv('imprimirBitacora')" class="btn btn-primary" data-toggle="modal" data-target="#bitacora">Imprimir</button>
                    </div>
                </div>
            </div>
        </div>
        <%------------------------------------------------ imprimir bitacora de seguimiento del reclamo ------------------------------------------%>
        <div id="imprimirBitacora" style="display: none" class="form-inline">
            <br />
            <div class="img-float-right" style="float: right; padding-top: 50px;">
                <img src="../../imgUnity/Unity%20Promotores%20transparente.png" style="margin-top: -100px; width: 235px;">
            </div>
            <div class="img-float-left" style="float: left; padding-top: 10px;">
                <p>Avenida Las Americas 22-23, Zona 14</p>
                <p>PBX: 2326-3700, 2386-3700</p>
                <p>www.unitypromotores.com</p>
            </div>
            <asp:Label ID="TituloMemo" Style="font-size: 16px; padding-left: 70px;" runat="server">Bitacora del reclamo</asp:Label>
            <div class="form-inline" style="padding-top: 90px;">
                <table style="width: 100%">
                    <tr>
                        <td><b>Departamento:</b></td>
                        <td>Reclamos Autos</td>
                        <td><b>Reportante:</b></td>
                        <td>
                            <asp:Label ID="ImpReportante" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Numero Reclamo:</b></td>
                        <td>
                            <asp:Label ID="ImpId" runat="server"></asp:Label></td>
                        <td><b>Telefono Reportante:</b></td>
                        <td>
                            <asp:Label ID="ImpTelefono" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Poliza:</b></td>
                        <td>
                            <asp:Label ID="ImpPoliza" runat="server"></asp:Label></td>
                        <td><b>Fecha Incidente:</b></td>
                        <td>
                            <asp:Label ID="ImpFecha" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Asegurado:</b></td>
                        <td>
                            <asp:Label ID="ImpAsegurado" runat="server"></asp:Label></td>
                        <td><b>Tipo Servicio:</b></td>
                        <td>
                            <asp:Label ID="ImpTipo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Ejecutivo:</b></td>
                        <td>
                            <asp:Label ID="ImpEjecutivo" runat="server"></asp:Label></td>
                        <td><b>Estado:</b></td>
                        <td>
                            <asp:Label ID="ImpEstado" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Contratante:</b></td>
                        <td>
                            <asp:Label ID="ImpContratante" runat="server"></asp:Label></td>
                        <td><b>Aseguradora:</b></td>
                        <td>
                            <asp:Label ID="ImpAseguradora" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Boleta:</b></td>
                        <td>
                            <asp:Label ID="ImpBoleta" runat="server"></asp:Label></td>
                        <td><b>Ajustador:</b></td>
                        <td>
                            <asp:Label ID="ImpAjustador" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Ramo:</b></td>
                        <td>
                            <asp:Label ID="ImpRamo" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <br />
                <p>
                    <asp:Label runat="server" ID="impUbicacion"></asp:Label>
                </p>
                <p>
                    <asp:Label runat="server" ID="impversion"></asp:Label>
                </p>

                <hr />
                <div id="bitacoras">
                    <p><b>Detalle llamadas en cabina:</b></p>
                    <asp:GridView ID="Bitllamadas" CssClass="table detalle" runat="server" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AutoGenerateColumns="True">
                    </asp:GridView>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <script>
        function printDiv(imprimir) {
            var contenido = document.getElementById(imprimir).innerHTML;
            var contenidoOriginal = document.body.innerHTML;
            document.body.innerHTML = contenido;
            window.print();
            document.body.innerHTML = contenidoOriginal;
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentJS" runat="Server">
</asp:Content>

