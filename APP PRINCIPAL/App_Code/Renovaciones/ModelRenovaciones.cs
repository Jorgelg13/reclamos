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
    using System.Collections.Generic;
    
    public partial class archivos
    {
        public int id { get; set; }
        public string poliza { get; set; }
    }
}
namespace Renovaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class contenido_correo
    {
        public int id { get; set; }
        public string contenido { get; set; }
    }
}
namespace Renovaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class estados
    {
        public estados()
        {
            this.renovaciones_polizas = new HashSet<renovaciones_polizas>();
            this.renovaciones_log = new HashSet<renovaciones_log>();
        }
    
        public int id { get; set; }
        public string estado { get; set; }
    
        public virtual ICollection<renovaciones_polizas> renovaciones_polizas { get; set; }
        public virtual ICollection<renovaciones_log> renovaciones_log { get; set; }
    }
}
namespace Renovaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class poliza
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public Nullable<int> codigo_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string primer_apellido { get; set; }
        public string poliza1 { get; set; }
        public Nullable<short> secren { get; set; }
        public Nullable<System.DateTime> vigi { get; set; }
        public Nullable<System.DateTime> vigf { get; set; }
        public Nullable<System.DateTime> fechareg { get; set; }
        public Nullable<short> no_ramo { get; set; }
        public string descripcion_ramo { get; set; }
        public Nullable<int> cod_gestor { get; set; }
        public string gestor { get; set; }
        public string correo_gestor { get; set; }
        public Nullable<short> cia { get; set; }
        public string aseguradora { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public Nullable<int> grupo_economico { get; set; }
    }
}
namespace Renovaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class renovaciones_log
    {
        public int id { get; set; }
        public Nullable<int> poliza { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual estados estados { get; set; }
        public virtual renovaciones_polizas renovaciones_polizas { get; set; }
    }
}
namespace Renovaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class renovaciones_polizas
    {
        public renovaciones_polizas()
        {
            this.renovaciones_log = new HashSet<renovaciones_log>();
        }
    
        public int id { get; set; }
        public Nullable<int> ramo { get; set; }
        public string poliza { get; set; }
        public string certificado { get; set; }
        public string cod_aseg { get; set; }
        public string asegurado { get; set; }
        public string cod_agen { get; set; }
        public string vigf { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string tipo_vehiculo { get; set; }
        public string pasajeros { get; set; }
        public string tarifa { get; set; }
        public string placa { get; set; }
        public string motor { get; set; }
        public string chasis { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string telefono3 { get; set; }
        public string telefono4 { get; set; }
        public string sin_incurrido { get; set; }
        public string prima_pendiente { get; set; }
        public string porc_pri_pend { get; set; }
        public string porc_siniestralidad { get; set; }
        public string porc_desc_pol { get; set; }
        public string porc_regc_pol { get; set; }
        public string porc_regc_sin { get; set; }
        public string porc_recg_m1821 { get; set; }
        public string porc_recg_m1618 { get; set; }
        public string porc_bonif { get; set; }
        public string monto_bonif { get; set; }
        public string suma_aseg_vence { get; set; }
        public string suma_aseg_renov { get; set; }
        public string deduc_danos { get; set; }
        public string deduc_min_danos { get; set; }
        public string deduc_robo { get; set; }
        public string deduc_min_robo { get; set; }
        public string prima_neta_vence { get; set; }
        public string prima_neta_renov { get; set; }
        public string prima_menos_bonif { get; set; }
        public string prima_minima_renov { get; set; }
        public string asisto_vence { get; set; }
        public string asisto_renova { get; set; }
        public string asistencia_fun { get; set; }
        public string beneficiario { get; set; }
        public string pagos { get; set; }
        public string monto_por_pago { get; set; }
        public string prima_anual { get; set; }
        public string forma_pago { get; set; }
        public string cod_pagador { get; set; }
        public string nombre_pagador { get; set; }
        public string gastos_emision { get; set; }
        public string recargo_por_frac { get; set; }
        public string valor_iva { get; set; }
        public string confirmar { get; set; }
        public string cob_may18_men_25_anos { get; set; }
        public string cob_may16_men_18_anos { get; set; }
        public string cob_may16 { get; set; }
        public string endoso_renov { get; set; }
        public string correo { get; set; }
        public string dpi { get; set; }
        public string direccion_cobro { get; set; }
        public string pasaporte { get; set; }
        public string nit { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> codigo_gestor { get; set; }
        public string nombre_gestor { get; set; }
        public string correo_cliente { get; set; }
        public string telefono_cliente { get; set; }
        public string poliza_unity { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public string vigf_acs { get; set; }
        public string correo_gestor { get; set; }
        public Nullable<int> grupo_economico { get; set; }
        public string contenido_correo { get; set; }
        public string comentario_renovacion { get; set; }
        public string comentario_invalida { get; set; }
        public string aclaracion { get; set; }
        public string facturador { get; set; }
        public Nullable<System.DateTime> fecha_facturacion { get; set; }
    
        public virtual estados estados { get; set; }
        public virtual ICollection<renovaciones_log> renovaciones_log { get; set; }
    }
}
namespace Renovaciones
{
    using System;
    using System.Collections.Generic;
    
    public partial class requerimientos
    {
        public int id { get; set; }
        public string poliza { get; set; }
        public string renovacion { get; set; }
        public string requerimiento { get; set; }
        public string fecha { get; set; }
        public string prima_neta { get; set; }
        public string comision { get; set; }
        public string prima_total { get; set; }
    }
}
