﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Renovaciones
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class RenovacionesEntities : DbContext
    {
        public RenovacionesEntities()
            : base("name=RenovacionesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<poliza> poliza { get; set; }
        public DbSet<renovaciones_polizas> renovaciones_polizas { get; set; }
    
        public virtual int pa_cargar_polizas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pa_cargar_polizas");
        }
    
        public virtual ObjectResult<Nullable<int>> pa_sq_renovaciones()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("pa_sq_renovaciones");
        }
    }
}
