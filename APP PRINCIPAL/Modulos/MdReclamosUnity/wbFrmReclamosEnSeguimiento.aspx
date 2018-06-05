﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ReclamosUnity.master" AutoEventWireup="true" CodeFile="wbFrmReclamosEnSeguimiento.aspx.cs" Inherits="Modulos_MdReclamosUnity_wbFrmReclamosEnSeguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <div class="panel panel-default col-sm-12">
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="home">
                    <a href="#home" aria-controls="home" role="tab" data-toggle="tab" style="font-size: 16px;">R. En Seguimiento</a>
                </li>
                <li role="presentation" class="profile">
                    <a href="#profile" aria-controls="profile" role="tab" data-toggle="tab" style="font-size: 16px;">R. Prioritarios</a>
                </li>
                <li role="presentation" class="coberturas">
                    <a href="#coberturas" aria-controls="coberturas" role="tab" data-toggle="tab" style="font-size: 18px;">R. Complicados</a>
                </li>
                <li role="presentation" class="ingreso-datos">
                    <a href="#ingreso-datos" aria-controls="ingreso-datos" role="tab" data-toggle="tab" style="font-size: 18px;">R. por Gestor</a>
                </li>
            </ul>
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane" id="home">
                    <div class="col-sm-12 col-md-3 col-lg-3 scrolling-table-container">
                        <asp:GridView ID="GridReclamosSeguimiento" runat="server" ShowFooter="true" CssClass="table table-hover" GridLines="None" OnRowDataBound="GridReclamosSeguimiento_RowDataBound1" OnSelectedIndexChanged="GridReclamosSeguimiento_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Mostrar">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <RowStyle HorizontalAlign="Left" Wrap="False" />
                            <FooterStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" />
                            <SelectedRowStyle BackColor="#afcaf7" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 col-md-9 col-lg-9 scrolling-table-container">
                        <asp:GridView ID="GridReclamosEstado" OnRowDataBound="GridReclamosSeguimiento_RowDataBound" runat="server" CssClass="table table-responsive table-hover" AutoGenerateColumns="True" GridLines="None" OnSelectedIndexChanged="GridReclamosEstado_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle HorizontalAlign="Left" Wrap="False" />
                        </asp:GridView>
                    </div>
                </div>
                <%--    seccion de reclamos Prioritarios--%>
                <div role="tabpanel" class="tab-pane" id="profile">
                    <div class="scrolling-table-container">
                        <asp:GridView ID="GridPrioritarios" OnRowDataBound="GridReclamosSeguimiento_RowDataBound" runat="server" CssClass="table bs-table table-responsive table-hover" AutoGenerateColumns="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridPrioritarios_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                        </asp:GridView>
                    </div>
                </div>
                <%-- seccion de reclamos complicados--%>
                <div role="tabpanel" class="tab-pane" id="coberturas">
                    <div class="scrolling-table-container">
                        <asp:GridView ID="GridComplicados" OnRowDataBound="GridReclamosSeguimiento_RowDataBound" runat="server" CssClass="table bs-table table-responsive table-hover" AutoGenerateColumns="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridComplicados_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                        </asp:GridView>
                    </div>
                </div>
                <%--seccion de reclamos por gestor --%>
                <div role="tabpanel" class="tab-pane" id="ingreso-datos">
                    <br />
                    <div class="form-inline">
                        <asp:DropDownList CssClass="form-control" AutoPostBack="True" ID="ddlgestor" Style="width: 15%" Height="34px" runat="server" CausesValidation="True" OnSelectedIndexChanged="ddlGestor_SelectedIndexChanged" DataSourceID="SqlDataSourceGestores" DataTextField="nombre" DataValueField="id">
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div class="scrolling-table-container">
                        <asp:GridView ID="GridReclamosGeneral" OnRowDataBound="GridReclamosSeguimiento_RowDataBound" runat="server" CssClass="table bs-table table-responsive table-hover" AutoGenerateColumns="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridReclamosGeneral_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle BackColor="#131B4D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
       <a title="Simbologia de colores" style="font-size:50px;" data-toggle="modal" role="button" data-target="#ModalColores"><i class="fa fa-info-circle" aria-hidden="true"></i></a>
    </div>
       <%-- Informacion de colores --%>
        <div class="modal fade " id="ModalColores" data-backdrop="static">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title"><b>Simbología de colores...</b></h4>
                    </div>
                    <div class="modal-body form-inline">
                        <label>Pendientes</label>
                        <asp:TextBox ID="txtrojo" style="background-color:#f7c6be; width:100%;" class="form-control" runat="server"></asp:TextBox>
                         <label>En tiempo</label>
                        <asp:TextBox ID="txtverde" style="background-color:#8ace8e; width:100%;" class="form-control" runat="server"></asp:TextBox>
                         <label>Proximos atrasados</label>
                        <asp:TextBox ID="txtAmarillo" style="background-color:#f9f595; width:100%;" class="form-control" runat="server"></asp:TextBox>
                         <label>Reclamos a ver hoy</label>
                        <asp:TextBox ID="txtAzul" style="background-color:#afcaf7; width:100%;" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-warning" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    <asp:SqlDataSource ID="SqlDataSourceGestores" runat="server" ConnectionString="<%$ ConnectionStrings:reclamosConnectionString %>" SelectCommand="SELECT [id], [nombre] FROM [gestores] where tipo = 'autos' "></asp:SqlDataSource>
</asp:Content>






