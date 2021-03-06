﻿using System;
using System.Web.UI;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class Modulos_MdReclamosUnity_wbFrmReportesDaños : System.Web.UI.Page
{
    String userlogin = HttpContext.Current.User.Identity.Name;
    Utils llenado = new Utils();
    String Join;
    String buscar;
    String EficienciaGestor;
    Double Pendientes, Nuevos, Cerrados, Ejecucion,CCRFT,CSRFT,PCRFT,PSRFT,EficienciaCierre,Anejamiento;
    int Promedio, Total, KPISinReasaeguro, KpiReaseguro, EjecucionCiclos;
    int Total2, Promedio2, EjecucionCiclos2;
    conexionBD obj = new conexionBD();
    ReclamosEntities DBReclamos = new ReclamosEntities();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DateTime primerDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);
            txtFechaInicio.Text = primerDia.ToString("yyyy/MM/dd").Replace("/", "-");
            txtFechaFin.Text = ultimoDia.ToString("yyyy/MM/dd").Replace("/", "-");
            checkSinFiltro_CheckedChanged(sender, e);
        }

        if(userlogin == "jlaj" || userlogin == "jwiesner"  || userlogin =="cmejia" || userlogin =="mbarrios" || 
           userlogin == "hvillacinda" || userlogin == "lgarcia" || userlogin == "mguillen")
        {
            btnMostrarEficiencia.Visible = true;
        }
        else
        {
            btnMostrarEficiencia.Visible = false;
        }

        EficienciaGestor = "select rs.nombre as Usuario, rs.Pendientes, rs.Nuevos, rs.Cerrados , " +
            "CAST(cast((rs.Cerrados * 100) / (rs.Pendientes + nuevos) as decimal) as varchar) as Ejecucion " +
            "from(select r.nombre," +
            "Pendientes = (select COUNT(*) from reclamos_varios where estado_unity = 'Seguimiento' and id_gestor = r.id and convert(date, fecha_apertura_reclamo, 112) < '"+txtFechaInicio.Text+"')," + // colocar rango en fecha de inicio que sea menor
            "Nuevos = (select COUNT(*) from reclamos_varios where convert(date, fecha_apertura_reclamo, 112) between '"+txtFechaInicio.Text+"' and '"+txtFechaFin.Text+"' and id_gestor = r.id), " +
            "Cerrados = (select COUNT(*) from reclamos_varios where convert(date, fecha_cierre_reclamo, 112) between '"+txtFechaInicio.Text+"' and '"+txtFechaFin.Text+"' and estado_unity = 'Cerrado' and id_gestor = r.id) " +
            "from(select id, usuario, nombre from gestores where tipo = 'daños varios') as r) rs where rs.Pendientes !=0";

        //variable que contiene todos los joins que se hacen en el query del reporte
        Join = " from reg_reclamo_varios " +
                  " INNER JOIN reclamos_varios ON reclamos_varios.id_reg_reclamos_varios = reg_reclamo_varios.id " +
                  " INNER JOIN gestores on reclamos_varios.id_gestor = gestores.id" +
                  " INNER JOIN talleres on reclamos_varios.id_taller = talleres.id" +
                  " INNER JOIN analistas on reclamos_varios.id_analista = analistas.id " +
                  " LEFT JOIN contactos_reclamos_varios on reclamos_varios.id = contactos_reclamos_varios.id_reclamos_varios " +
                  " LEFT JOIN detalle_pagos_reclamos_varios on reclamos_varios.id = detalle_pagos_reclamos_varios.id_reclamos_varios " +
                  " INNER JOIN cabina ON reclamos_varios.id_cabina = cabina.id " +
                  " INNER JOIN sucursal ON cabina.id_sucursal = sucursal.id " +
                  " INNER JOIN empresa ON sucursal.id_empresa = empresa.id " +
                  " INNER JOIN pais ON empresa.id_pais = pais.id " +
                  " INNER JOIN usuario ON reclamos_varios.id_usuario = usuario.id";
    }

    protected void btnGenerarTabla_Click(object sender, EventArgs e)
    {
        PanelCamposSeleccion.Visible = true;
        PanelEficiencia.Visible = false;
        PnCiclos.Visible = false;

        //si el check esta chequeado entra aqui 
        //este check sirve para no filtrar el reporte por alguna seleccion
        if (checkSinFiltro.Checked)
        {
            string listado;
            listado = "Select reclamos_varios.id, ";
            //recorre la lista de checks que seleccionaron para generar el reporte
            for (int i = 0; i < checkCampos.Items.Count; i++)
            {
                if (checkCampos.Items[i].Selected)
                {
                    listado += checkCampos.Items[i].Value + ", ";
                }
            }
            //si seleccionarion cerrado ejecuta este query 
            if (ddlEstado.SelectedItem.Text == "Cerrado")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join + " where (convert(date, reclamos_varios.fecha_cierre_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') " +
                    "and (reclamos_varios.estado_unity = '" + ddlEstado.SelectedItem + "') and reclamos_varios.reserva >= "+txtMonto.Text+" ", GridCamposSeleccion);
            }
            else if(ddlEstado.SelectedItem.Text == "Seguimiento")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (Convert(date,reclamos_varios.fecha_apertura_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') " +
                  "and (reclamos_varios.estado_unity = '" + ddlEstado.SelectedItem + "') and reclamos_varios.reserva >= " + txtMonto.Text + " " +
                 "", GridCamposSeleccion);
            }
            else if (ddlEstado.SelectedItem.Text == "Todos")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (Convert(date, reclamos_varios.fecha_apertura_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') " +
                 " and reclamos_varios.reserva >= " + txtMonto.Text + "", GridCamposSeleccion);
            }
            else if (ddlEstado.SelectedItem.Text == "Pendientes")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join + " where "+ ddlEstado.SelectedValue + " ", GridCamposSeleccion);
            }

            Conteo();
            //Eficiencia();
        }

        else
        {
            if (ddlElegir.SelectedItem.Text == "Gestor" || ddlElegir.SelectedItem.Text == "Ejecutivo")
            {
                buscar = ddlBuscar.SelectedItem.Text;
            }
            else
            {
                buscar = txtBuscar.Text;
            }

            string listado;
            listado = "Select distinct reclamos_varios.id, ";
            for (int i = 0; i < checkCampos.Items.Count; i++)
            {
                if (checkCampos.Items[i].Selected)
                {
                    listado += checkCampos.Items[i].Value + ", ";
                }
            }

            if (ddlEstado.SelectedItem.Text == "Cerrado")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (" + ddlElegir.SelectedValue + " like '%" + buscar + "%') and (convert(date,reclamos_varios.fecha_cierre_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') " +
                  "and (reclamos_varios.estado_unity = '" + ddlEstado.SelectedItem + "')  ", GridCamposSeleccion);
            }

            else if(ddlEstado.SelectedItem.Text == "Seguimiento")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (" + ddlElegir.SelectedValue + " like '%" + buscar + "%') and (convert(date, reclamos_varios.fecha_apertura_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') " +
                  "and (reclamos_varios.estado_unity = '" + ddlEstado.SelectedItem + "')  ", GridCamposSeleccion);
            }

            else if (ddlEstado.SelectedItem.Text == "Todos")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (" + ddlElegir.SelectedValue + " like '%" + buscar + "%') and (convert(date, reclamos_varios.fecha_apertura_reclamo,112) between '" + txtFechaInicio.Text + "' " +
                  "and '" + txtFechaFin.Text + "') ", GridCamposSeleccion);
            }

            else if (ddlEstado.SelectedItem.Text == "Pendientes")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (" + ddlElegir.SelectedValue + " like  '%"+ buscar +"%') and ("+ddlEstado.SelectedValue+") ", GridCamposSeleccion);
            }

            else if (ddlEstado.SelectedItem.Text == "Estado")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (" + ddlElegir.SelectedValue + " like  '%" + ddlBuscar.SelectedItem.Text + "%') and (estado_unity = 'Seguimiento') ", GridCamposSeleccion);

                Utils.TituloReporte(PanelPrincipal, lblPeriodo, lblFechaGeneracion, lblUsuario, lblTitulo, "Reporte de Reclamos / Depto. Reclamos Daños / " + ddlBuscar.SelectedItem.Text + " ", userlogin, txtFechaInicio, txtFechaFin, "");
            }

            Conteo();
        }

        if (ddlEstado.SelectedItem.Text != "Estado")
        {
            Utils.TituloReporte(PanelPrincipal, lblPeriodo, lblFechaGeneracion, lblUsuario, lblTitulo, "Reporte de Reclamos de Daños", userlogin, txtFechaInicio, txtFechaFin, "");
        }

        Utils.actividades(0, Constantes.DANIOS(), 29, Constantes.USER());
    }

    public void Eficiencia()
    {
        try
        {
            llenado.llenarGrid(EficienciaGestor, GridEficiencia);
        }

        catch (Exception ex)
        {
            Utils.ShowMessage(this.Page, "No se a podido generar la eficiencia debido a un error " + ex.Message, "Error..!", "success");
        }
    }

    //funcion para exportar a un archivo de excel lo que aparece en el gridview
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (PanelCamposSeleccion.Visible == true)
        {
            Utils.actividades(0, Constantes.DANIOS(), 35, Constantes.USER());
            Utils.ExportarExcel(PanelPrincipal, Response, "Reporte Reclamos Daños");
        }

        else if (PanelEficiencia.Visible == true)
        {
            Utils.actividades(0, Constantes.DANIOS(), 36, Constantes.USER());
            Utils.ExportarExcel(PanelPrincipal, Response, "Reporte de Eficiencia");
        }

        else if (PnCiclos.Visible == true)
        {
            if(ddlCiclos.SelectedValue == "Ciclo Total")
            {
                Utils.actividades(0, Constantes.DANIOS(), 40, Constantes.USER());
            }

            else if (ddlCiclos.SelectedValue == "Ciclo Unity")
            {
                Utils.actividades(0, Constantes.DANIOS(), 39, Constantes.USER());
            }

            else if (ddlCiclos.SelectedValue == "Ciclo Cliente")
            {
                Utils.actividades(0, Constantes.DANIOS(), 38, Constantes.USER());
            }

            else if (ddlCiclos.SelectedValue == "Ciclo Aseguradora")
            {
                Utils.actividades(0, Constantes.DANIOS(), 37, Constantes.USER());
            }

            Utils.ExportarExcel(PanelPrincipal, Response, "Reporte de "+ddlCiclos.SelectedValue+"");
        }
    }

    public void Conteo()
    {
        lblConteo.Text = this.GridCamposSeleccion.Rows.Count.ToString();
    }

    protected void linkSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Modulos/MdReclamosUnity/wbFrmRecDañosSeguimiento.aspx", false);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    protected void checkSinFiltro_CheckedChanged(object sender, EventArgs e)
    {
        if (checkSinFiltro.Checked)
        {
            txtBuscar.Enabled = false;
            ddlElegir.Enabled = false;
        }
        else
        {
            txtBuscar.Enabled = true;
            ddlElegir.Enabled = true;
        }
    }

    protected void checkTodos_CheckedChanged(object sender, EventArgs e)
    {
        if (checkTodos.Checked)
        {
            for (int i = 0; i < checkCampos.Items.Count; i++)
            {
                if (!checkCampos.Items[i].Selected)
                {
                    checkCampos.Items[i].Selected = true;
                }
            }
        }

        else
        {
            for (int i = 0; i < checkCampos.Items.Count; i++)
            {
                if (checkCampos.Items[i].Selected)
                {
                    checkCampos.Items[i].Selected = false;
                }
            }
        }
    }

    protected void ddlElegir_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBuscar.Visible = false;
        ddlBuscar.Visible = true;

        if(ddlElegir.SelectedItem.Text == "Estado Reclamo")
        {
            ddlBuscar.DataSource = DBReclamos.estados_reclamos_unity.ToList().Where(es => es.tipo == "daños");
            ddlBuscar.DataTextField = "descripcion";
            ddlBuscar.DataValueField = "id";
            ddlBuscar.DataBind();

            txtBuscar.Visible = false;
            ddlBuscar.Visible = true;
        }

        else if (ddlElegir.SelectedItem.Text == "Taller")
        {
            ddlBuscar.DataSource = DBReclamos.talleres.ToList();
            ddlBuscar.DataValueField = "id";
            ddlBuscar.DataTextField = "nombre";
            ddlBuscar.DataBind();
        }

        else if (ddlElegir.SelectedItem.Text == "Aseguradora")
        {
            ddlBuscar.DataSource = DBReclamos.aseguradoras.ToList();
            ddlBuscar.DataValueField = "id";
            ddlBuscar.DataTextField = "aseguradora";
            ddlBuscar.DataBind();
        }

        else if (ddlElegir.SelectedItem.Text == "Ejecutivo")
        {
            ddlBuscar.DataSource = DBReclamos.ejecutivos.ToList();
            ddlBuscar.DataValueField = "codigo";
            ddlBuscar.DataTextField = "gestor";
            ddlBuscar.DataBind();
            txtBuscar.Visible = false;
            ddlBuscar.Visible = true;
        }

        else if (ddlElegir.SelectedItem.Text == "Gestor")
        {
            ddlBuscar.DataSource = DBReclamos.gestores.ToList().Where(ge => ge.tipo == "Daños varios");
            ddlBuscar.DataValueField = "id";
            ddlBuscar.DataTextField = "nombre";
            ddlBuscar.DataBind();
        }

        else
        {
            txtBuscar.Visible = true;
            ddlBuscar.Visible = false;
        }
    }

    protected void Mostrar_Click(object sender, EventArgs e)
    {
        PanelCamposSeleccion.Visible = false;
        PanelEficiencia.Visible = false;
        PnCiclos.Visible = true;

        if (ddlCiclos.SelectedValue == "Ciclo Total")
        {
            MostrarLabel(true);
            KPISinReasaeguro = 115;
            KpiReaseguro = 195;
            Utils.Ciclos_Reclamos_tipo(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios_reaseguro", GridCiclos, 4, KpiReaseguro, 1);
            Utils.Ciclos_Reclamos_tipo(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios_reaseguro", GridCiclos2, 4, KPISinReasaeguro, 0);
            Titulo("Total", KPISinReasaeguro, KpiReaseguro);
            Utils.actividades(0, Constantes.DANIOS(), 34, Constantes.USER());
        }

        else if (ddlCiclos.SelectedValue == "Ciclo Unity")
        {
            MostrarLabel(false);
            KpiReaseguro = 15;
            KPISinReasaeguro = 15;
            Utils.Ciclos_Reclamos(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios", GridCiclos, 1,KpiReaseguro);
            Utils.Ciclos_Reclamos(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios", GridCiclos2, 5,KPISinReasaeguro);
            lblTitulo.Text = "Ciclo Unity, KPI sobre " + KpiReaseguro.ToString() + " dias";
            Utils.actividades(0, Constantes.DANIOS(), 31, Constantes.USER());
        }
                
        else if (ddlCiclos.SelectedValue == "Ciclo Cliente")
        {
            MostrarLabel(true);
            KPISinReasaeguro = 68;
            KpiReaseguro = 80;
            Utils.Ciclos_Reclamos_tipo(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios_reaseguro", GridCiclos, 3, KpiReaseguro, 1);
            Utils.Ciclos_Reclamos_tipo(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios_reaseguro", GridCiclos2, 3, KPISinReasaeguro, 0);
            Titulo("Cliente", KPISinReasaeguro, KpiReaseguro);
            Utils.actividades(0, Constantes.DANIOS(), 33, Constantes.USER());
        }

        else if (ddlCiclos.SelectedValue == "Ciclo Aseguradora")
        {
            MostrarLabel(true);
            KPISinReasaeguro = 32;
            KpiReaseguro = 100;
            Utils.Ciclos_Reclamos_tipo(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios_reaseguro", GridCiclos, 2, KpiReaseguro,1);
            Utils.Ciclos_Reclamos_tipo(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios_reaseguro", GridCiclos2, 2, KPISinReasaeguro,0);
            Titulo("Aseguradora", KPISinReasaeguro, KpiReaseguro);
            Utils.actividades(0, Constantes.DANIOS(), 32, Constantes.USER());
        }
    }

    public void MostrarLabel(bool valor)
    {
        KpiConReaseguro.Visible = valor;
        KpiSinReaseguro.Visible = valor;
    }

    private void Titulo(string titulo, int KPI, int KpiReaseguro)
    {
        lblTitulo.Text = "Ciclo "+titulo+", KPI sobre " + KPI + " dias sin reaseguro y " + KpiReaseguro + " con reaseguro.";
        KpiConReaseguro.Text = "Con Reaseguro Evaluado sobre " + KpiReaseguro + " Dias";
        KpiSinReaseguro.Text = "Sin Reaseguro Evaluado sobre " + KPI + " Dias";
    }

    protected void GridEficiencia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pendientes       += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Pendientes]"));
                Nuevos           += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Nuevos]"));
                Cerrados         += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Cerrados]"));
                CCRFT            += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Cerrados Con Reaseguro_Fuera_Tiempo]"));
                CSRFT            += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Cerrados Sin Reaseguro_Fuera_Tiempo]"));
                PCRFT            += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Pendientes Con Reaseguro_Fuera_Tiempo]"));
                PSRFT            += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Pendientes Sin Reaseguro_Fuera_Tiempo]"));
                EficienciaCierre += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Ejecucion Cierre]"));
                Anejamiento      += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Añejamiento]"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTALES:";
                e.Row.Cells[1].Text = Pendientes.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[2].Text = Nuevos.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[3].Text = Cerrados.ToString();
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[4].Text = CCRFT.ToString();
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[5].Text = CSRFT.ToString();
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[6].Text = PCRFT.ToString();
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[7].Text = PSRFT.ToString();
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[8].Text = ((1 - ((CCRFT + CSRFT) / Cerrados)) * 100).ToString("N2");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                e.Row.Cells[9].Text = ((1 - ((PCRFT + PSRFT) / Pendientes)) * 100).ToString("N2");
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }
        }
        catch (Exception err)
        {
            Response.Write(err);
        }
    }
    protected void GridCiclos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "[Total_Reclamos]"));
                Promedio += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "[Promedio_dias]"));
                EjecucionCiclos += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Ejecucion"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTALES:";

                e.Row.Cells[1].Text = Total.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[2].Text = (Promedio / GridCiclos.Rows.Count).ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[3].Text = ((Convert.ToDouble(KpiReaseguro) / (Promedio / GridCiclos.Rows.Count)) * 100).ToString("N2");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;
            }
        }
        catch (Exception err)
        {
            Response.Write(err);
        }
    }
    protected void GridCiclos2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Total2 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "[Total_Reclamos]"));
                Promedio2 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "[Promedio_dias]"));
                EjecucionCiclos2 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Ejecucion"));

                if (Convert.ToInt32(e.Row.Cells[3].Text) >= 90)
                {
                    e.Row.Attributes.Add("style", "background-color: #8ace8e"); //verdes
                }

                if (Convert.ToInt32(e.Row.Cells[3].Text) < 90)
                {
                    e.Row.Attributes.Add("style", "background-color: #f7c6be"); //rojos
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTALES:";

                e.Row.Cells[1].Text = Total2.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[2].Text = (Promedio2 / GridCiclos2.Rows.Count).ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[3].Text = ((Convert.ToDouble(KPISinReasaeguro) / (Promedio2 / GridCiclos2.Rows.Count)) * 100 ).ToString("N2");
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;
            }
        }
        catch (Exception err)
        {
            Response.Write(err);
        }
    }

    protected void btnMostrarEficiencia_Click(object sender, EventArgs e)
    {
        PanelCamposSeleccion.Visible = false;
        PnCiclos.Visible = false;
        PanelEficiencia.Visible = true;
        //Eficiencia();
        Utils.Reportes(txtFechaInicio,txtFechaFin, "pa_eficiencia_danos_varios",GridEficiencia);
        Utils.TituloReporte(PanelPrincipal, lblPeriodo, lblFechaGeneracion, lblUsuario, lblTitulo, "Reporte de Efectividad / Depto. Reclamos Daños", userlogin, txtFechaInicio, txtFechaFin, "");
        Utils.actividades(0, Constantes.DANIOS(), 30, Constantes.USER());
    }
}

