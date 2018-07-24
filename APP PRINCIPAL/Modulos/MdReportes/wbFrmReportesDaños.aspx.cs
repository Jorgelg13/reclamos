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
    Double Pendientes, Nuevos, Cerrados, Ejecucion;
    int Promedio, Total;
    conexionBD obj = new conexionBD();
    ReclamosEntities DBReclamos = new ReclamosEntities();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Gestores();
            checkSinFiltro_CheckedChanged(sender, e);
        }

        if(userlogin == "jsagastume" || userlogin == "jlaj" || userlogin == "jwiesner" || userlogin == "jpazos" || userlogin =="cmejia")
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
                    "and (reclamos_varios.estado_unity = '" + ddlEstado.SelectedItem + "') ", GridCamposSeleccion);
            }
            else if(ddlEstado.SelectedItem.Text == "Seguimiento")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (Convert(date,reclamos_varios.fecha_apertura_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') and (reclamos_varios.estado_unity = '" + ddlEstado.SelectedItem + "') " +
                 "", GridCamposSeleccion);
            }
            else if (ddlEstado.SelectedItem.Text == "Todos")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join +
                  " where (Convert(date, reclamos_varios.fecha_apertura_reclamo,112) between '" + txtFechaInicio.Text + "' and '" + txtFechaFin.Text + "') " +
                 "", GridCamposSeleccion);
            }
            else if (ddlEstado.SelectedItem.Text == "Pendientes")
            {
                llenado.llenarGrid(listado.Substring(0, (listado.Length - 2)) + Join + " where "+ ddlEstado.SelectedValue + " ", GridCamposSeleccion);
            }

            Conteo();
            Eficiencia();
        }

        else
        {
            if (ddlElegir.SelectedItem.Text == "Gestor")
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
            }

            Conteo();
            Eficiencia();
        }
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
        Utils.ExportarExcel(GridCamposSeleccion, Response, "Reporte Daños");
    }

    protected void btnExportarEficiencia_Click(object sender, EventArgs e)
    {
        Utils.ExportarExcel(GridEficiencia, Response, "Eficiencia Reclamos Daños");
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
        if (ddlElegir.SelectedItem.Text == "Gestor")
        {
            txtBuscar.Visible = false;
            ddlBuscar.Visible = true;
        }
        else
        {
            ddlBuscar.Visible = false;
            txtBuscar.Visible = true;
        }

        if(ddlElegir.SelectedItem.Text == "Estado Reclamo")
        {
            ddlBuscar.DataSource = DBReclamos.estados_reclamos_unity.ToList().Where(es => es.tipo == "daños");
            ddlBuscar.DataTextField = "descripcion";
            ddlBuscar.DataValueField = "id";
            ddlBuscar.DataBind();
            txtBuscar.Visible = false;
            ddlBuscar.Visible = true;
        }
        else if(ddlElegir.SelectedItem.Text == "Gestor")
        {
            Gestores();
        }
    }

    public void Gestores()
    {
        ddlBuscar.DataSource = DBReclamos.gestores.ToList().Where(ge => ge.tipo == "Daños varios");
        ddlBuscar.DataValueField = "id";
        ddlBuscar.DataTextField = "nombre";
        ddlBuscar.DataBind();
    }

    protected void GridEficiencia_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Pendientes += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Pendientes]"));
                Nuevos     += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Nuevos]"));
                Cerrados   += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Cerrados]"));
                Ejecucion  += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "[Ejecucion]"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTALES:";
                e.Row.Cells[1].Text = Pendientes.ToString();
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[2].Text = Nuevos.ToString();
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[3].Text = Cerrados.ToString();
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;

                e.Row.Cells[4].Text = ((Cerrados / (Pendientes + Nuevos))*100).ToString("N2");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Font.Bold = true;
            }
        }
        catch (Exception err)
        {
            Response.Write(err);
        }
    }

    protected void Mostrar_Click(object sender, EventArgs e)
    {
        if (ddlCiclos.SelectedValue == "Ciclo Total")
        {
            PnReporte.Visible = false;
            PnCiclos.Visible = true;
            Utils.Ciclos_Reclamos(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios", GridCiclos, 4);
            lblTituloCiclo.Text = "Ciclo Total";
        }

        else if (ddlCiclos.SelectedValue == "Ciclo Unity")
        {
            PnReporte.Visible = false;
            PnCiclos.Visible = true;
            Utils.Ciclos_Reclamos(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios", GridCiclos, 1);
            lblTituloCiclo.Text = "Ciclo Unity";
        }

        else if (ddlCiclos.SelectedValue == "Ciclo Cliente")
        {
            PnReporte.Visible = false;
            PnCiclos.Visible = true;
            Utils.Ciclos_Reclamos(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios", GridCiclos, 2);
            lblTituloCiclo.Text = "Ciclo Cliente";
        }

        else if (ddlCiclos.SelectedValue == "Ciclo Aseguradora")
        {
            PnReporte.Visible = false;
            PnCiclos.Visible = true;
            Utils.Ciclos_Reclamos(txtFechaInicio, txtFechaFin, "pa_ciclos_reclamos_danios", GridCiclos, 3);
            lblTituloCiclo.Text = "Ciclo Aseguradora";
        }


        else if (ddlCiclos.SelectedValue == "Eficiencia")
        {
            Eficiencia();
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "show_modal", "$('#ModalDetalle').modal('show');", addScriptTags: true);
        }
    }

    protected void linKRegresar_Click(object sender, EventArgs e)
    {
        PnReporte.Visible = true;
        PnCiclos.Visible = false;
    }

    protected void linkDescarPromedio_Click(object sender, EventArgs e)
    {
        Utils.ExportarExcel(GridCiclos, Response, lblTituloCiclo.Text + " Reclamos autos del " + txtFechaInicio.Text + " al " + txtFechaFin.Text);
    }

    protected void GridCiclos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "[Total_Reclamos]"));
                Promedio += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "[Promedio_dias]"));
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
            }
        }
        catch (Exception err)
        {
            Response.Write(err);
        }
    }
}

