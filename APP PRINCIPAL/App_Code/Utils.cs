﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net.Http;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data.OleDb;

public class Utils
{
    conexionBD conexion = new conexionBD();
    public static ReclamosEntities DBReclamos = new ReclamosEntities();

    public Utils()
    {

    }

    public string buscador(String search, String table, String[] columSearch = null, String[] columView = null, string sWhere = null, String[] tJoin = null)
    {
        string[] arreglo = search.Split(" ".ToCharArray());
        string sql = "";
        string colSearch = "";
        //string join = "";
        string colView = "*";

        if (arreglo.Length > 0)
        {
            if (columSearch.Length > 1)
            {
                colSearch = " CONCAT(" + String.Join(",", columSearch) + ") ";
            }
            else
            {
                colSearch = columSearch[0];
            }
            if (columView != null)
            {
                colView = String.Join(", ", columView);
            }

            sql = "SELECT " + colView + " FROM " + table + " WHERE " + colSearch + " LIKE '%" + arreglo[0] + "%' ";

            if (arreglo.Length > 1)
            {
                for (int i = 1; i < arreglo.Length; i++)
                {
                    if (!String.IsNullOrEmpty(arreglo[i].Trim()))
                    {
                        sql += " and  " + colSearch + " like '%" + arreglo[i] + "%' ";
                    }
                }
            }

            if (sWhere != null)
            {
                sql += sWhere;
            }
        }

        return sql;
    }

    public void llenarGrid(String Consulta, GridView tabla)
    {
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(Consulta, this.conexion.ObtenerConexionReclamos());
            DataTable dt = new DataTable();
            da.Fill(dt);
            tabla.DataSource = dt;
            tabla.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.conexion.DescargarConexion();
        }
    }

    public void llenarGridRenovaciones(String Consulta, GridView tabla)
    {
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(Consulta, this.conexion.ObtenerConexionRenovaciones());
            DataTable dt = new DataTable();
            da.Fill(dt);
            tabla.DataSource = dt;
            tabla.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.conexion.DescargarConexion();
        }
    }

    public static void Reportes(TextBox fechainicio, TextBox fechafin, String reporte, GridView gridRepore)
    {
        try
        {
            conexionBD obj = new conexionBD();
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand(reporte, obj.ObtenerConexionReclamos());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@fechaInicio", fechainicio.Text);
            comando.Parameters.AddWithValue("@fechaFin", fechafin.Text);
           
            comando.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(comando);
            sda.Fill(dt);
            gridRepore.DataSource = dt;
            gridRepore.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void ReportesSeguro(String fechainicio, String fechafin, String reporte, GridView gridRepore, DropDownList area)
    {
        try
        {
            conexionBD obj = new conexionBD();
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand(reporte, obj.ObtenerConexionSeguro());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@fechaInicio", fechainicio);
            comando.Parameters.AddWithValue("@fechaFin", fechafin);
            comando.Parameters.AddWithValue("@i_opcion", area.SelectedValue);
            comando.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(comando);
            sda.Fill(dt);
            gridRepore.DataSource = dt;
            gridRepore.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void Ciclos_Reclamos(TextBox fechainicio, TextBox fechafin, String reporte, GridView gridRepore, int tipo, int kpi)
    {
        try
        {
            conexionBD obj = new conexionBD();
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand(reporte, obj.ObtenerConexionReclamos());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@fechaInicio", fechainicio.Text);
            comando.Parameters.AddWithValue("@fechaFin", fechafin.Text);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@kpi", kpi);
            comando.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(comando);
            sda.Fill(dt);
            gridRepore.DataSource = dt;
            gridRepore.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static void Ciclos_Reclamos_tipo(TextBox fechainicio, TextBox fechafin, String reporte, GridView gridRepore, int tipo, int kpi,int filtro)
    {
        try
        {
            conexionBD obj = new conexionBD();
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand(reporte, obj.ObtenerConexionReclamos());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@fechaInicio", fechainicio.Text);
            comando.Parameters.AddWithValue("@fechaFin", fechafin.Text);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@kpi", kpi);
            comando.Parameters.AddWithValue("@filtro", filtro);
            comando.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(comando);
            sda.Fill(dt);
            gridRepore.DataSource = dt;
            gridRepore.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void llenarGrid2(String Consulta, GridView tabla)
    {
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(Consulta, this.conexion.ObtenerConexionSeguro());
            DataTable dt = new DataTable();
            da.Fill(dt);
            tabla.DataSource = dt;
            tabla.DataBind();
            this.conexion.DescargarConexion();
        }
        catch (Exception)
        {
            //throw ex;
        }
        finally
        {
            this.conexion.DescargarConexion();
        }
    }

    public bool verificarUsuario(String usuario)
    {
        bool existe = false;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sdr;
        cmd.Connection = conexion.ObtenerConexionReclamos();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "select nombre from usuario where nombre = '" + usuario + "' ";
        try
        {
            sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                existe = true;
            }

            else
            {
                existe = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conexion.DescargarConexion();
        }

        return existe;
    }

    public DataTable autocompletar(string query, int id)
    {
        try
        {
            query += query + " where id = " + id + " ";
            SqlDataAdapter da = new SqlDataAdapter(query, this.conexion.ObtenerConexionReclamos());
            DataTable datosDevueltos = new DataTable();
            da.Fill(datosDevueltos);
            return datosDevueltos;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.conexion.DescargarConexion();
        }
    }

    public bool actualizarDatos(String actualizar, int identificador)
    {
        bool exito = true;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = actualizar + " where id =  " + identificador + " ";
            cmd.Connection = this.conexion.ObtenerConexionReclamos();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
            exito = false;
        }
        finally
        {
            this.conexion.DescargarConexion();
        }

        return exito;
    }

    public static void ShowMessage(Page page, string message, string title, string type = "info")
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
              String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    public static void ExportarExcel(Control ctrl, HttpResponse Response, string nombre)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + nombre + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            ctrl.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public static void SMS_gastos_medicos(String telefono, String mensaje, String usuario, String estado, long id, string tipo)
    {
        int numero = Convert.ToInt32(telefono);

        if (numero > 30000000)
        {
            if (tipo == "I" && telefono != "")
            {
                HttpClient client = new HttpClient();
                var values = new System.Collections.Generic.Dictionary<string, string>
                 {
                   { "token", "Un!ty2018" },
                   { "numero", telefono },
                   { "mensaje", mensaje}
                 };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("http://192.168.81.127:9900/movistar/enviar", content);
                bool estado_envio = response.IsFaulted;

                comentarios_reclamos_medicos comentario = new comentarios_reclamos_medicos();
                comentario.descripcion = "Notificacion SMS: " + mensaje;
                comentario.fecha = DateTime.Now;
                comentario.usuario = usuario;
                comentario.estado = estado;
                comentario.id_reclamo_medico = id;
                DBReclamos.comentarios_reclamos_medicos.Add(comentario);
                DBReclamos.SaveChanges();
            }
        }
    }

    public static void SMS_reclamos_danios(String telefono, String mensaje, String usuario, int id)
    {
        int numero = Convert.ToInt32(telefono);

        if (numero > 30000000)
        {
            if (telefono != "")
            {
                HttpClient client = new HttpClient();
                var values = new System.Collections.Generic.Dictionary<string, string>
                 {
                   { "token", "Un!ty2018" },
                   { "numero", telefono },
                   { "mensaje", mensaje}
                 };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("http://192.168.81.127:9900/movistar/enviar", content);
                bool estado_envio = response.IsFaulted;

                comentarios_reclamos_varios comentario = new comentarios_reclamos_varios();
                comentario.descripcion = "Notificacion SMS: " + mensaje;
                comentario.fecha = DateTime.Now;
                comentario.usuario = usuario;
                comentario.id_reclamos_varios = id;
                DBReclamos.comentarios_reclamos_varios.Add(comentario);
                DBReclamos.SaveChanges();
            }
        }
    }

    public static void SMS_reclamos_autos(String telefono, String mensaje, String usuario, int id)
    {
        int numero = Convert.ToInt32(telefono);

        if (numero > 30000000)
        {
            if (telefono != "")
            {
                HttpClient client = new HttpClient();
                var values = new System.Collections.Generic.Dictionary<string, string>
                 {
                   { "token", "Un!ty2018" },
                   { "numero", telefono },
                   { "mensaje", mensaje}
                 };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("http://192.168.81.127:9900/movistar/enviar", content);
                bool estado_envio = response.IsFaulted;

                comentarios_reclamos_autos comentario = new comentarios_reclamos_autos();
                comentario.descripcion = "Notificacion SMS: " + mensaje;
                comentario.fecha = DateTime.Now;
                comentario.usuario = usuario;
                comentario.id_reclamo_auto = id;
                DBReclamos.comentarios_reclamos_autos.Add(comentario);
                DBReclamos.SaveChanges();
            }
        }
    }

    public static void ENVIOSMS(String telefono, String mensaje)
    {
        int numero = Convert.ToInt32(telefono);

        if (numero > 30000000)
        {
            if (telefono != "")
            {
                HttpClient client = new HttpClient();
                var values = new System.Collections.Generic.Dictionary<string, string>
                 {
                   { "token", "Un!ty2018" },
                   { "numero", telefono },
                   { "mensaje", mensaje}
                 };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("http://192.168.81.127:9900/movistar/enviar", content);
                bool estado_envio = response.IsFaulted;
            }
        }
    }

    public static void Guardar_cartas(TextBox contenido, string tipo, string modulo, int id, DropDownList tipoCarta, HttpResponse Response)
    {
        try
        {
            var bandera = DBReclamos.reclamos_varios.Find(id);

            if (tipoCarta.SelectedValue =="cierre interno")
            {
                if (bandera.b_carta_cierre_interno == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_cierre_interno = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (tipoCarta.SelectedValue =="declinado")
            {
                if (bandera.b_carta_declinado == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_declinado = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (tipoCarta.SelectedValue=="envio cheque")
            {
                if (bandera.b_carta_envio_cheque == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_envio_cheque = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if(tipoCarta.SelectedValue =="cierre deducible")
            {
                if (bandera.b_carta_deducible_anual == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_deducible_anual = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (tipoCarta.SelectedValue == "cierre reclamo")
            {
                if (bandera.b_carta_cierre_reclamo == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_cierre_reclamo = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (tipoCarta.SelectedValue == "alerta tiempo")
            {
                if (bandera.b_carta_alerta_tiempo == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_alerta_tiempo = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            DBReclamos.SaveChanges();

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    public static void Guardar_cartas_autos(TextBox contenido, string tipo, string modulo, int id, CheckBox chCierreInterno, 
        CheckBox chDeclinado, CheckBox chEnvioCheque, CheckBox chAlertaTiempo)
    {
        try
        {
            var bandera = DBReclamos.reclamo_auto.Find(id);
            if (chCierreInterno.Checked)
            {
                if (bandera.b_carta_cierre_interno == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_cierre_interno = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (chDeclinado.Checked)
            {
                if (bandera.b_carta_declinado == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_declinado = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (chEnvioCheque.Checked)
            {
                if (bandera.b_carta_envio_cheque == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_envio_cheque = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            if (chAlertaTiempo.Checked)
            {
                if (bandera.b_carta_alerta_tiempo == false)
                {
                    cartas carta = new cartas();
                    var sec_registro = DBReclamos.pa_sec_cartas();
                    int? id_registro = sec_registro.Single();
                    carta.id = Convert.ToInt32(id_registro);
                    carta.contenido = contenido.Text;
                    carta.tipo = tipo;
                    carta.modulo = modulo;
                    carta.id_reclamo = id;
                    bandera.b_carta_alerta_tiempo = true;
                    DBReclamos.cartas.Add(carta);
                }

                else
                {
                    var actualizar = DBReclamos.cartas.Where(ca => ca.id_reclamo == id && ca.tipo == tipo && ca.modulo == modulo).First();
                    actualizar.contenido = contenido.Text;
                    actualizar.tipo = tipo;
                    actualizar.modulo = modulo;
                    actualizar.id_reclamo = id;
                }
            }

            DBReclamos.SaveChanges();
        }
        catch (Exception)
        {

        }
    }

    public static string TelefonoGestor(DropDownList gestor)
    {
        string telefono;
        var numero = DBReclamos.gestores.Where(tel => tel.nombre == gestor.SelectedItem.Text).First();
        telefono = numero.telefono.ToString();
        return telefono;
    }

    public static string TelefonoEjecutivo(int codigo)
    {
        string telefono;
        var numero = DBReclamos.ejecutivos.Where(tel => tel.codigo == codigo).First();
        telefono = numero.telefono.ToString();
        return telefono;
    }

    //seleccionar correo de ejecutivos que tienen asignado una poliza
    public static string seleccionarCorreo(int cod)
    {
        try
        {
            string correo;
            var selectCorreo = DBReclamos.ejecutivos.Where(e => e.codigo == cod).First();
            return correo = selectCorreo.correo;
        }

        catch (Exception)
        {
            return "no existe";
        }
    }

    public static string seleccionarCorreoGestor(string usuario)
    {
        try
        {
            string correoGestor;
            var correo_gestor = DBReclamos.gestores.Select(g => new { g.correo, g.usuario }).Where(usu => usu.usuario == usuario).First();
            return correoGestor = correo_gestor.correo.ToString();
        }

        catch (Exception)
        {
            return "no existe";
        }
    }

    public static void TituloReporte(Panel PanelPrincipal, Label lblPeriodo, Label lblFechaGeneracion, Label lblUsuario, Label lblTitulo,
                              String Titulo, String Usuario, TextBox txtFechaInicio, TextBox txtFechaFin, String KPI)
    {
        try
        {
            PanelPrincipal.Visible = true;
            lblPeriodo.Text = "Periodo del " + Convert.ToDateTime(txtFechaInicio.Text).ToString("dd/MM/yyyy") + " al " + Convert.ToDateTime(txtFechaFin.Text).ToString("dd/MM/yyyy");
            lblFechaGeneracion.Text = "Generado: " + DateTime.Now.ToString();
            lblUsuario.Text = "Usuario: " + Usuario;
            lblTitulo.Text = Titulo;
        }
        catch (Exception)
        {

        }
    }

    public static void actividades(int id_reclamo, int id_tipo, int id_movimiento, String usuario)
    {
        actividades nueva = new actividades();
        var sec_registro = DBReclamos.pa_sec_actividades();
        long? id_registro = sec_registro.Single();
        nueva.id = Convert.ToInt64(id_registro);
        nueva.id_reclamo = id_reclamo;
        nueva.id_tipo = id_tipo;
        nueva.id_movimiento = id_movimiento;
        nueva.fecha = DateTime.Now;
        nueva.usuario = usuario;
        DBReclamos.actividades.Add(nueva);
        DBReclamos.SaveChanges();
    }

    //funcion para cargar datos desde un archivo de excel
    public void importar(string FilePath, string Extension, string isHDR, GridView cargas)
    {
        string conStr = "";
        //aqui selecciona un excel por la extension que sea y la asigna dependiendo con la cadena de conexion en el archivo web.config
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //obtiene la conexion hacia la hoja de excel
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //lee los datos desde la hoja de excel
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        //muestra y carga los datos en el gridview
        cargas.Caption = Path.GetFileName(FilePath);
        cargas.DataSource = dt;
        cargas.DataBind();
    }


    public static int CODIGO_GESTOR(String usuario)
    {
        var codigo = DBReclamos.usuario.Where(US => US.nombre ==usuario).First();
        return Convert.ToInt32(codigo.numero_gestor);
    }

    public static String CORREO_GESTOR(string ejecutivo)
    {
        if (String.IsNullOrEmpty(ejecutivo))
        {
            return  "reclamosgt@unitypromotores.com";
        }
        var correo = DBReclamos.ejecutivos.Where(ej => ej.gestor == ejecutivo).First();
        return correo.correo;
    }

    //procedimiento para insertar registro en el envio de correos automatico en el servidor 192.168.5.199
    public static void EmailRenovacion(String procedimiento, String cliente, String cuerpo, String gestor, int codigo_gestor, int grupo_economico)
    {
        try
        {
            conexionBD obj = new conexionBD();
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand(procedimiento, obj.ObtenerConexionSeguro());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@emailCliente", cliente);
            comando.Parameters.AddWithValue("@cuerpo", cuerpo);
            comando.Parameters.AddWithValue("@correoGestor",gestor);
            comando.Parameters.AddWithValue("@ejecutivo", codigo_gestor);
            comando.Parameters.AddWithValue("@grupo_economico", grupo_economico);
            comando.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    //procedimiento para insertar registro en el envio de correos automatico en el servidor 192.168.5.199
    public static void notificacion_email(String procedimiento, String cliente, String cuerpo, String gestor, String asunto)
    {
        try
        {
            conexionBD obj = new conexionBD();
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand(procedimiento, obj.ObtenerConexionSeguro());
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@emailCliente", cliente);
            comando.Parameters.AddWithValue("@cuerpo", cuerpo);
            comando.Parameters.AddWithValue("@correoGestor", gestor);
            comando.Parameters.AddWithValue("@asunto", asunto);
            comando.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static void MoverArchivos(string poliza,string destino, string rutaOrigen)
    {
        String pathPrincipal = @"E:\ReclamosScanner\files\RenovacionesElRoble\OneDrive - Unity Seguros\Renovaciones\"+rutaOrigen+"";
        String pathDestino = @"E:\ReclamosScanner\files\RenovacionesElRoble\OneDrive - Unity Seguros\Renovaciones";

        //String pathPrincipal = @"C:\Renovaciones\Polizas";
        //String pathDestino = @"C:\Renovaciones";

        String origen = Path.Combine(pathPrincipal, poliza);
        String destinoArchivo = Path.Combine(pathDestino, destino + "\\" +poliza);

        if (File.Exists(origen))
        {
            if (File.Exists(destinoArchivo))
            {
                File.Delete(destinoArchivo);
            }

            File.Move(origen, destinoArchivo);
        }

        else
        {

        }
    }

    public static void CopiarArchivos(string poliza, string destino, string rutaOrigen)
    {
        String pathPrincipal = @"E:\ReclamosScanner\files\RenovacionesElRoble\OneDrive - Unity Seguros\Renovaciones\" + rutaOrigen + "";
        String pathDestino = @"E:\ReclamosScanner\files\RenovacionesElRoble\OneDrive - Unity Seguros\Renovaciones";

        //String pathPrincipal = @"C:\Renovaciones\Polizas";
        //String pathDestino = @"C:\Renovaciones";

        String origen = Path.Combine(pathPrincipal, poliza);
        String destinoArchivo = Path.Combine(pathDestino, destino + "\\" + poliza);

        if (File.Exists(origen))
        {
            if (File.Exists(destinoArchivo))
            {
                File.Delete(destinoArchivo);
            }

            File.Copy(origen, destinoArchivo);
        }

        else
        {

        }
    }


    //agregar comentario a reclamos de gastos medicos
    public static void insertarComentario(int id, string descripcion, string estado)
    {
        try
        {
            comentarios_reclamos_medicos comentario = new comentarios_reclamos_medicos();
            comentario.descripcion = descripcion;
            comentario.estado = estado;
            comentario.usuario = Constantes.USER();
            comentario.fecha = DateTime.Now;
            comentario.id_reclamo_medico = id;
            DBReclamos.comentarios_reclamos_medicos.Add(comentario);
            DBReclamos.SaveChanges();
        }

        catch (Exception)
        {

        }

    }

    public static string backOffice(string usuario)
    {
        try
        {
            string backOffice;
            var back = DBReclamos.usuario.Select(u => new { u.backOffice, u.nombre}).Where(usu => usu.nombre == usuario).First();
            return backOffice = back.backOffice.ToString();
        }

        catch (Exception)
        {
            return "no existe";
        }
    }
}