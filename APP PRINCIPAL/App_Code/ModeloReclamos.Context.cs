﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

public partial class ReclamosEntities : DbContext
{
    public ReclamosEntities()
        : base("name=ReclamosEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public DbSet<ajustadores> ajustadores { get; set; }
    public DbSet<analistas> analistas { get; set; }
    public DbSet<aseguradoras> aseguradoras { get; set; }
    public DbSet<auto_reclamo> auto_reclamo { get; set; }
    public DbSet<autorizaciones> autorizaciones { get; set; }
    public DbSet<bitacora_autorizaciones> bitacora_autorizaciones { get; set; }
    public DbSet<bitacora_autos_unity> bitacora_autos_unity { get; set; }
    public DbSet<bitacora_consulta_movil> bitacora_consulta_movil { get; set; }
    public DbSet<bitacora_estados_autos> bitacora_estados_autos { get; set; }
    public DbSet<bitacora_estados_reclamos_varios> bitacora_estados_reclamos_varios { get; set; }
    public DbSet<bitacora_reclamo_auto> bitacora_reclamo_auto { get; set; }
    public DbSet<bitacora_reclamos_medicos> bitacora_reclamos_medicos { get; set; }
    public DbSet<bitacora_reclamos_varios> bitacora_reclamos_varios { get; set; }
    public DbSet<busqCoberturasPolizasDaños> busqCoberturasPolizasDaños { get; set; }
    public DbSet<cabina> cabina { get; set; }
    public DbSet<cartas> cartas { get; set; }
    public DbSet<coberturas> coberturas { get; set; }
    public DbSet<coberturas_afectadas> coberturas_afectadas { get; set; }
    public DbSet<coberturas_afectadas_danios> coberturas_afectadas_danios { get; set; }
    public DbSet<comentarios_reclamos_autos> comentarios_reclamos_autos { get; set; }
    public DbSet<comentarios_reclamos_medicos> comentarios_reclamos_medicos { get; set; }
    public DbSet<comentarios_reclamos_varios> comentarios_reclamos_varios { get; set; }
    public DbSet<contacto_auto> contacto_auto { get; set; }
    public DbSet<contactos_reclamos_varios> contactos_reclamos_varios { get; set; }
    public DbSet<detalle_gasto_medico> detalle_gasto_medico { get; set; }
    public DbSet<detalle_pagos_reclamos_autos> detalle_pagos_reclamos_autos { get; set; }
    public DbSet<detalle_pagos_reclamos_medicos> detalle_pagos_reclamos_medicos { get; set; }
    public DbSet<detalle_pagos_reclamos_varios> detalle_pagos_reclamos_varios { get; set; }
    public DbSet<direcciones> direcciones { get; set; }
    public DbSet<direcciones_clientes_gm> direcciones_clientes_gm { get; set; }
    public DbSet<documentos_medicos> documentos_medicos { get; set; }
    public DbSet<documentos_solicitados> documentos_solicitados { get; set; }
    public DbSet<ejecutivos> ejecutivos { get; set; }
    public DbSet<empresa> empresa { get; set; }
    public DbSet<encuesta> encuesta { get; set; }
    public DbSet<errores_insercion> errores_insercion { get; set; }
    public DbSet<estado> estado { get; set; }
    public DbSet<estados_reclamos_unity> estados_reclamos_unity { get; set; }
    public DbSet<formulario_colectivo> formulario_colectivo { get; set; }
    public DbSet<gestores> gestores { get; set; }
    public DbSet<pais> pais { get; set; }
    public DbSet<ramos> ramos { get; set; }
    public DbSet<recibos_medicos> recibos_medicos { get; set; }
    public DbSet<reclamo_auto> reclamo_auto { get; set; }
    public DbSet<reclamos_medicos> reclamos_medicos { get; set; }
    public DbSet<reclamos_varios> reclamos_varios { get; set; }
    public DbSet<reg_reclamo_varios> reg_reclamo_varios { get; set; }
    public DbSet<reg_reclamos_medicos> reg_reclamos_medicos { get; set; }
    public DbSet<sucursal> sucursal { get; set; }
    public DbSet<sysdiagrams> sysdiagrams { get; set; }
    public DbSet<talleres> talleres { get; set; }
    public DbSet<tipo_documentos> tipo_documentos { get; set; }
    public DbSet<usuario> usuario { get; set; }
    public DbSet<viewCoberturasAutos> viewCoberturasAutos { get; set; }
    public DbSet<vistaBusquedaPolizaMovil> vistaBusquedaPolizaMovil { get; set; }
    public DbSet<vistaReclamosDaños> vistaReclamosDaños { get; set; }
    public DbSet<vistaReclamosMedicos> vistaReclamosMedicos { get; set; }
    public DbSet<ViewBusquedaAuto> ViewBusquedaAuto { get; set; }
    public DbSet<v_producto_no_conforme> v_producto_no_conforme { get; set; }

    public virtual ObjectResult<pa_reclamos_autos_Result> pa_reclamos_autos()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_reclamos_autos_Result>("pa_reclamos_autos");
    }

    public virtual ObjectResult<pa_reclamos_varios_Result> pa_reclamos_varios()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_reclamos_varios_Result>("pa_reclamos_varios");
    }

    public virtual ObjectResult<pa_reportesAutorizaciones_Result> pa_reportesAutorizaciones(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_reportesAutorizaciones_Result>("pa_reportesAutorizaciones", fechaInicioParameter, fechaFinParameter);
    }

    public virtual ObjectResult<pa_reportesAutorizacionesAbiertas_Result> pa_reportesAutorizacionesAbiertas(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_reportesAutorizacionesAbiertas_Result>("pa_reportesAutorizacionesAbiertas", fechaInicioParameter, fechaFinParameter);
    }

    public virtual ObjectResult<pa_ReportesAutos_Result> pa_ReportesAutos(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_ReportesAutos_Result>("pa_ReportesAutos", fechaInicioParameter, fechaFinParameter);
    }

    public virtual ObjectResult<pa_ReportesDaños_Result> pa_ReportesDaños(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_ReportesDaños_Result>("pa_ReportesDaños", fechaInicioParameter, fechaFinParameter);
    }

    public virtual int pa_ReportesMedicos(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_ReportesMedicos", fechaInicioParameter, fechaFinParameter);
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_auto_reclamo()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_auto_reclamo");
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_autorizaciones()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_autorizaciones");
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_reclamo_auto()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_reclamo_auto");
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_reclamos_medicos()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_reclamos_medicos");
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_registros_medicos()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_registros_medicos");
    }

    public virtual ObjectResult<promedio_aseguradoras_Result> promedio_aseguradoras()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<promedio_aseguradoras_Result>("promedio_aseguradoras");
    }

    public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        var versionParameter = version.HasValue ?
            new ObjectParameter("version", version) :
            new ObjectParameter("version", typeof(int));

        var definitionParameter = definition != null ?
            new ObjectParameter("definition", definition) :
            new ObjectParameter("definition", typeof(byte[]));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
    }

    public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        var versionParameter = version.HasValue ?
            new ObjectParameter("version", version) :
            new ObjectParameter("version", typeof(int));

        var definitionParameter = definition != null ?
            new ObjectParameter("definition", definition) :
            new ObjectParameter("definition", typeof(byte[]));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
    }

    public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
    }

    public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
    }

    public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
    }

    public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
    {
        var diagramnameParameter = diagramname != null ?
            new ObjectParameter("diagramname", diagramname) :
            new ObjectParameter("diagramname", typeof(string));

        var owner_idParameter = owner_id.HasValue ?
            new ObjectParameter("owner_id", owner_id) :
            new ObjectParameter("owner_id", typeof(int));

        var new_diagramnameParameter = new_diagramname != null ?
            new ObjectParameter("new_diagramname", new_diagramname) :
            new ObjectParameter("new_diagramname", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
    }

    public virtual int sp_upgraddiagrams()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
    }

    public virtual int pa_kpi_autirizaciones(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_kpi_autirizaciones", fechaInicioParameter, fechaFinParameter);
    }

    public virtual ObjectResult<Nullable<int>> pa_sec_cartas()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("pa_sec_cartas");
    }

    public virtual int pa_kpi_autorizaciones_aseguradora(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin, string aseguradora)
    {
        var fechaInicioParameter = fechaInicio.HasValue ?
            new ObjectParameter("fechaInicio", fechaInicio) :
            new ObjectParameter("fechaInicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        var aseguradoraParameter = aseguradora != null ?
            new ObjectParameter("aseguradora", aseguradora) :
            new ObjectParameter("aseguradora", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_kpi_autorizaciones_aseguradora", fechaInicioParameter, fechaFinParameter, aseguradoraParameter);
    }

    public virtual ObjectResult<pa_cargar_asegurados_Result> pa_cargar_asegurados()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_cargar_asegurados_Result>("pa_cargar_asegurados");
    }

    public virtual ObjectResult<pa_cargar_autos_Result> pa_cargar_autos()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_cargar_autos_Result>("pa_cargar_autos");
    }

    public virtual ObjectResult<pa_cargar_danios_varios_Result> pa_cargar_danios_varios()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_cargar_danios_varios_Result>("pa_cargar_danios_varios");
    }

    public virtual ObjectResult<pa_vista_movil_Result> pa_vista_movil()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pa_vista_movil_Result>("pa_vista_movil");
    }

    public virtual ObjectResult<Nullable<int>> pa_sec_tipo_documentos()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("pa_sec_tipo_documentos");
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_documentos_solicitados()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_documentos_solicitados");
    }

    public virtual int pa_eficiencia_pnc(Nullable<System.DateTime> fechainicio, Nullable<System.DateTime> fechaFin)
    {
        var fechainicioParameter = fechainicio.HasValue ?
            new ObjectParameter("fechainicio", fechainicio) :
            new ObjectParameter("fechainicio", typeof(System.DateTime));

        var fechaFinParameter = fechaFin.HasValue ?
            new ObjectParameter("fechaFin", fechaFin) :
            new ObjectParameter("fechaFin", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_eficiencia_pnc", fechainicioParameter, fechaFinParameter);
    }

    public virtual ObjectResult<Nullable<long>> pa_sec_estados_autos()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("pa_sec_estados_autos");
    }
}
