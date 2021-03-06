﻿<%@ Page Title="Reclamos Unity" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DashboardCabina.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="container menu-cuadrado btn-acciones-laterales">
        <div class="col-md-6 col-lg-6">
            <div class="panel panel-info" style="height: 200px;">
                <div class="panel-heading">
                    <p style="text-align: center; font-size: 16px;"><b>Asistencias y accidentes de autos</b></p>
                </div>
                <div class="panel-body">
                    <h1>
                        <a href="/Modulos/MdReclamos/wbFrmReclamosAutosGeneral.aspx" style="font-size: 90px;" class="fa fa-car "><asp:Label ID="totalReclamosAutos" runat="server" ForeColor="#797D7F"></asp:Label></a>
                    </h1>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-lg-6">
            <div class="panel panel-info" style="height: 200px;">
                <div class="panel-heading">
                    <p style="text-align: center; font-size: 16px;"><b>Asistencias de daños varios</b></p>
                </div>
                <div class="panel-body">
                    <h1>
                        <a href="/Modulos/MdReclamos/wbFrmReclamosDañosGeneral.aspx" style="font-size: 90px;" class="fa fa-exclamation-triangle"><asp:Label ID="totalReclamosDaños" runat="server" ForeColor="#797D7F"></asp:Label></a>
                    </h1>
                </div>
            </div>
        </div>
    </div>
    <div class="container menu-cuadrado btn-acciones-laterales">
        <div class="col-md-6 col-lg-6" >
            <div class="panel panel-info" style="height: 200px;">
                <div class="panel-heading">
                    <p style="text-align: center; font-size: 16px;"><b>Reclamos Medicos en recepcion</b></p>
                </div>
                <div class="panel-body">
                    <h1>
                        <a href="/Modulos/MdReclamos/wbFrmReclamosMedicosGeneral.aspx" style="font-size: 90px;" class="fa fa-heartbeat"><asp:Label ID="totalReclamosMedicos" runat="server" aria-hidden="true" ForeColor="#797D7F"></asp:Label></a>
                    </h1>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-lg-6" >
            <div class="panel panel-info" style="height: 200px;">
                <div class="panel-heading">
                    <p style="text-align: center; font-size: 16px;"><b>Autorizaciones reportadas</b></p>
                </div>
                <div class="panel-body">
                    <h1>
                        <a href="/Modulos/MdReclamos/wbFrmAutorizacionesGenerales.aspx" style="font-size: 90px;" class=" fa fa-medkit"><asp:Label ID="totalReclamosAutorizaciones" runat="server" Style="padding-left: 10px;"  ForeColor="#797D7F"></asp:Label></a>
                    </h1>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
