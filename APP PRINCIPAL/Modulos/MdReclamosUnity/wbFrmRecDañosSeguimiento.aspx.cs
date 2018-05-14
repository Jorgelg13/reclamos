﻿using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Modulos_MdReclamosUnity_wbFrmRecDañosSeguimiento : System.Web.UI.Page
{
    String userlogin = HttpContext.Current.User.Identity.Name; //usuario que esta en session
    conexionBD objeto = new conexionBD();
    SqlCommand cmd = new SqlCommand();
    Utils llenado = new Utils();
    String selectGeneral;

    protected void Page_Load(object sender, EventArgs e)
    {
        selectGeneral = "SELECT " +
                "dbo.reclamos_varios.id as ID," +
                "dbo.reg_reclamo_varios.poliza as Poliza," +
                "dbo.reg_reclamo_varios.asegurado as Asegurado," +
                "dbo.reg_reclamo_varios.cliente as Cliente," +
                "dbo.reg_reclamo_varios.aseguradora as Aseguradora," + //4
                "dbo.reg_reclamo_varios.contratante as Contratante," +
                "dbo.reg_reclamo_varios.ejecutivo as Ejecutivo," +//6
                "dbo.reg_reclamo_varios.ramo as Ramo," +
                "dbo.reg_reclamo_varios.status as Estatus," +//8
                "dbo.reg_reclamo_varios.tipo as Tipo," +
                //"dbo.reg_reclamo_varios.direccion as Direccion," +//10
                //"dbo.reg_reclamo_varios.vip as VIP," +
                //"dbo.reg_reclamo_varios.suma_asegurada as [Suma Asegurada]," +//12
                //"dbo.reg_reclamo_varios.moneda as Moneda," +
                //"dbo.reclamos_varios.boleta as boleta," +
                //"dbo.reclamos_varios.titular as Titular," +
                "dbo.reclamos_varios.reportante as Reportante," +
                //"dbo.reclamos_varios.telefono as Telefono," +
                //"dbo.reclamos_varios.ajustador as Ajustador," +
                //"dbo.reclamos_varios.version as Version," +
                //"dbo.reclamos_varios.ubicacion as Ubicacion," +
                //"dbo.reclamos_varios.hora as Hora," +
                //"dbo.reclamos_varios.fecha as Fecha," +
                //"dbo.reclamos_varios.hora_commit as [Hora Commit]," +
                "Convert(varchar(20),dbo.reclamos_varios.fecha_commit, 103) as [Fecha Creacion]," +
                //"dbo.reclamos_varios.hora_cierre as [Hora Cierre]," +
                //"dbo.reclamos_varios.fecha_cierre as [Fecha Cierre]," +
                //"dbo.reclamos_varios.id_gestor as Gestor, " + //27
                //"dbo.reclamos_varios.id_taller as Taller, " +
                //"dbo.reclamos_varios.id_analista as Analista, " +
                //"dbo.reclamos_varios.observaciones as Observaciones," + //30
                //"dbo.reclamos_varios.estado_reclamo_unity as [Estado Reclamo], " + //31
                //"dbo.reclamos_varios.prioritario, " + //32
                //"dbo.reclamos_varios.complicado, " +//33
                //"dbo.reclamos_varios.compromiso_pago, " + //34
                //"dbo.reclamos_varios.num_reclamo, " +
                "gestores.nombre as Gestor," +//37
                //"gestores.telefono, " +//38
                //"gestores.correo, " +//39
                //"dbo.cabina.nombre as cabina," +
                //"dbo.sucursal.nombre as sucursal," +
                //"dbo.empresa.nombre as empresa," +
                //"dbo.pais.nombre as pais," +
                //"dbo.usuario.nombre as usuario, " +
                "Convert(varchar(20),dbo.reclamos_varios.fecha_visualizar, 103) as [Fecha Visualizar], " +
                "CASE WHEN CONVERT(date, reclamos_varios.fecha_visualizar, 110) < CONVERT(date, GETDATE(), 110)  THEN 0 ELSE 1 END AS mostrar " +
                "FROM " +
                "dbo.reg_reclamo_varios " +
                "INNER JOIN dbo.reclamos_varios ON dbo.reclamos_varios.id_reg_reclamos_varios = dbo.reg_reclamo_varios.id "+
                "INNER JOIN gestores on reclamos_varios.id_gestor = gestores.id ";
                //"INNER JOIN dbo.cabina ON dbo.reclamos_varios.id_cabina = dbo.cabina.id " +
                //"INNER JOIN dbo.sucursal ON dbo.cabina.id_sucursal = dbo.sucursal.id " +
                //"INNER JOIN dbo.empresa ON dbo.sucursal.id_empresa = dbo.empresa.id " +
                //"INNER JOIN dbo.pais ON dbo.empresa.id_pais = dbo.pais.id " +
                //"INNER JOIN dbo.usuario ON dbo.reclamos_varios.id_usuario = dbo.usuario.id ";

        string reclamosSeguimiento = selectGeneral +
                " where (reclamos_varios.usuario_unity = '" + userlogin + "' and reclamos_varios.fecha_visualizar <= getdate() and reclamos_varios.estado_unity != 'Cerrado' ) ";

        string reclamosPrioritarios = selectGeneral +
               " where ((reclamos_varios.prioritario = 'true') and (reclamos_varios.usuario_unity = '" + userlogin + "' and reclamos_varios.estado_unity != 'Cerrado' ))";

        string reclamosComplicados = selectGeneral +
               " where ((reclamos_varios.complicado = 'true') and (reclamos_varios.usuario_unity = '" + userlogin + "' and reclamos_varios.estado_unity != 'Cerrado' ))";

        if(!IsPostBack)
        {
            llenado.llenarGrid(reclamosSeguimiento, GridReclamosSeguimiento);
            llenado.llenarGrid(reclamosPrioritarios, GridPrioritarios);
            llenado.llenarGrid(reclamosComplicados, GridComplicados);
        }
    }
    //metodo para actualizar la fecha a la actual
    private void fechaVisualizar(int id)
    {
        try
        {
            cmd.CommandText = "update reclamos_varios set fecha_visualizar = getdate() where id =" + id + " ";
            cmd.Connection = objeto.ObtenerConexionReclamos();
            cmd.ExecuteNonQuery();
            objeto.conexion.Close();
        }
        catch (Exception)
        {
            //Response.Write();
        }
        finally
        {
            objeto.DescargarConexion();
        }
    }

    protected void GridReclamosSeguimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id1;
        id1 = Convert.ToInt32(GridReclamosSeguimiento.SelectedRow.Cells[1].Text);
        fechaVisualizar(id1);
        Response.Redirect("/Modulos/MdReclamosUnity/wbFrmReclamosDañosSeguimiento.aspx?ID_reclamo=" + id1, false);
    }

    protected void GridPrioritarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id2;
        id2 = Convert.ToInt32(GridPrioritarios.SelectedRow.Cells[1].Text);
        Response.Redirect("/Modulos/MdReclamosUnity/wbFrmReclamosDañosSeguimiento.aspx?ID_reclamo=" + id2, false);
    }

    protected void GridComplicados_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id3;
        id3 = Convert.ToInt32(GridComplicados.SelectedRow.Cells[1].Text);
        Response.Redirect("/Modulos/MdReclamosUnity/wbFrmReclamosDañosSeguimiento.aspx?ID_reclamo=" + id3, false);
    }

    protected void GridReclamosEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id4;
        id4 = Convert.ToInt32(GridReclamosEstado.SelectedRow.Cells[1].Text);
        Response.Redirect("/Modulos/MdReclamosUnity/wbFrmReclamosDañosSeguimiento.aspx?ID_reclamo=" + id4, false);
    }

    protected void GridReclamosGeneral_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id5;
        id5 = Convert.ToInt32(GridReclamosGeneral.SelectedRow.Cells[1].Text);
        Response.Redirect("/Modulos/MdReclamosUnity/wbFrmReclamosDañosSeguimiento.aspx?ID_reclamo=" + id5, false);
    }

    //funcion que coloca en rojo los registros que no se an abierto en el dia
    protected void GridReclamosSeguimiento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            if (e.Row.Cells[15].Text == "0")
            {
                for (int _xCell = 0; _xCell <= e.Row.Cells.Count - 1; _xCell++)
                {
                    e.Row.Cells[_xCell].ForeColor = System.Drawing.Color.Red;
                }
            }
    }

    protected void DDLTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string reclamosPorEstado = selectGeneral +
              " where (reclamos_varios.estado_reclamo_unity = '" + DDLTipo.SelectedItem + "' and reclamos_varios.usuario_unity = '" + userlogin + "' and reclamos_varios.estado_unity != 'Cerrado' ) ";

        llenado.llenarGrid(reclamosPorEstado, GridReclamosEstado);
        GridReclamosSeguimiento.Visible = false;
    }

    protected void ddlgestor_SelectedIndexChanged(object sender, EventArgs e)
    {
        String reclamosGestor = selectGeneral +
              "where ( reclamos_varios.id_gestor = " + ddlgestor.SelectedValue + " and reclamos_varios.estado_unity != 'Cerrado')";

        String Prioritarios = selectGeneral +
             " where ((reclamos_varios.prioritario = 'true') and (reclamos_varios.id_gestor = " + ddlgestor.SelectedValue + " and reclamos_varios.estado_unity != 'Cerrado' ))";

        String Complicados = selectGeneral +
             " where ((reclamos_varios.complicado = 'true') and (reclamos_varios.id_gestor = " + ddlgestor.SelectedValue + " and reclamos_varios.estado_unity != 'Cerrado' ))";

        llenado.llenarGrid(reclamosGestor, GridReclamosGeneral);
        llenado.llenarGrid(Prioritarios, GridPrioritarios);
        llenado.llenarGrid(Complicados, GridComplicados);
    }
}